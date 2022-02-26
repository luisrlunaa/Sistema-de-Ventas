using CapaEnlaceDatos;
using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class FrmLogin : DevComponents.DotNetBar.Metro.MetroForm
    {
        clsUsuarios U = new clsUsuarios();
        clsManejador Cx = new clsManejador();

        public FrmLogin()
        {
            InitializeComponent();
        }

        public string idCaja;
        public DateTime FechaVenc;
        bool tienefila = false;
        public void obtenerFiladeCaja()
        {
            Cx.Desconectar();
            string cadSql = "SELECT id_caja, monto_inicial,fecha  FROM Caja where monto_final =0 AND fecha = convert(datetime,CONVERT(varchar(10), getdate(), 103),103)";
            Cx.Conectar();
            SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
            {
                tienefila = true;
            }
            Cx.Desconectar();
        }

        public void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Trim() != "")
            {
                if (txtPassword.Text.Trim() != "")
                {
                    string Mensaje = "";
                    U.User = txtUser.Text;
                    U.Password = txtPassword.Text;

                    Mensaje = U.IniciarSesion();
                    if (Mensaje == "Su Contraseña es Incorrecta.")
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show(Mensaje, "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        txtPassword.Clear();
                        txtPassword.Focus();
                    }
                    else
                        if (Mensaje == "El Nombre de Usuario no Existe.")
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show(Mensaje, "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        txtUser.Clear();
                        txtPassword.Clear();
                        txtUser.Focus();
                    }
                    else
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show(Mensaje, "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        FrmMenuPrincipal MP = new FrmMenuPrincipal();

                        if (FechaVenc.Date <= DateTime.Today.Date)
                        {
                            if (DevComponents.DotNetBar.MessageBoxEx.Show("Licencia del producto ha Cadudado, Favor ponerse en contacto con su suplidor para Renovar la misma, Desea Renovar Ahora?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                            {
                                FrmLogin login = new FrmLogin();
                                frmRenovar renovar = new frmRenovar();
                                renovar.Show();
                                login.Hide();
                            }
                        }
                        else
                        {
                            CargarListados();
                            if (rbInventario.Checked)
                            {
                                Program.LoginStatus = "Inventario";
                                RecuperarDatosSesion();
                                MP.Show();
                                this.Hide();
                            }
                            else if (rbVentas.Checked)
                            {
                                Program.LoginStatus = "Ventas";
                                RecuperarDatosSesion();
                                MP.Show();
                                this.Hide();
                            }
                            else if (rbNCF.Checked)
                            {
                                RecuperarDatosSesion();
                                if (Program.isAdminUser)
                                {
                                    Program.LoginStatus = "NCF";
                                    MP.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    DevComponents.DotNetBar.MessageBoxEx.Show("No tiene Cargo de Administrador para poder usar esa función.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                if (tienefila)
                                {
                                    RecuperarDatosSesion();
                                    MP.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    circularProgressBar1.Visible = true;
                                    circularProgressBar1.Value = 0;
                                    circularProgressBar1.Minimum = 0;
                                    circularProgressBar1.Maximum = 100;
                                    timer1.Start();

                                    if (circularProgressBar1.Visible == true)
                                        CargarListados();

                                    if (rbInventario.Checked)
                                    {
                                        Program.LoginStatus = "Inventario";
                                        RecuperarDatosSesion();
                                    }
                                    else if (rbVentas.Checked)
                                    {
                                        Program.LoginStatus = "Ventas";
                                        RecuperarDatosSesion();

                                        #region Calculo de ganancias
                                        List<int> ventasIds = new List<int>();
                                        if (clsGenericList.listVentas.Count > 0)
                                            clsGenericList.totalGanancia = clsGenericList.Ganancias(ventasIds);
                                        #endregion
                                    }
                                    else if (rbNCF.Checked)
                                    {
                                        RecuperarDatosSesion();
                                        if (Program.isAdminUser)
                                        {
                                            Program.LoginStatus = "NCF";
                                        }
                                        else
                                        {
                                            DevComponents.DotNetBar.MessageBoxEx.Show("No tiene Cargo de Administrador para poder usar esa función.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        if (tienefila)
                                        {
                                            RecuperarDatosSesion();
                                        }
                                        else
                                        {
                                            if (panelmontoinicial.Visible)
                                            {
                                                if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Desea ingresar Monto Inicial de Caja?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                                                {
                                                    panelmontoinicial.Visible = false;
                                                }
                                                else
                                                {
                                                    Cx.Desconectar();
                                                    using (SqlCommand cmd = new SqlCommand("abrir_caja", Cx.conexion))
                                                    {
                                                        string id_var = "";
                                                        if (Program.idcaja.ToString() == "" || Program.idcaja.ToString() == null)
                                                            id_var = "0";
                                                        else
                                                            id_var = Program.idcaja.ToString();

                                                        cmd.CommandType = CommandType.StoredProcedure;

                                                        cmd.Parameters.Add("@id_caja", SqlDbType.Int).Value = id_var;
                                                        cmd.Parameters.Add("@monto", SqlDbType.Decimal).Value = 0;
                                                        cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Today;

                                                        Cx.Conectar();
                                                        cmd.ExecuteNonQuery();
                                                        Cx.Desconectar();
                                                    }

                                                    obtenerFiladeCaja();
                                                    RecuperarDatosSesion();

                                                    Program.idcaja = Program.idcaja + 1;
                                                }
                                            }
                                        }

                                        #region Calculo de ganancias
                                        List<int> ventasIds = new List<int>();
                                        if (clsGenericList.listVentas.Count > 0)
                                            clsGenericList.totalGanancia = clsGenericList.Ganancias(ventasIds);
                                        #endregion
                                    }
                                }
                            }
                        }

                    }
                }
                else
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese su Contraseña.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Focus();
                }
            }
            else
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Nombre de Usuario.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUser.Focus();
            }
        }
        public void CargarListados()
        {
            #region Listado Ventas
            if (clsGenericList.listVentas is null)
            {
                clsGenericList.listVentas = new List<Venta>();
                clsGenericList.idsVentas = new List<int>();

                clsVentas V = new clsVentas();
                DataTable dtV = new DataTable();
                dtV = V.Listado();

                try
                {
                    foreach (DataRow reader in dtV.Rows)
                    {
                        Venta venta = new Venta();
                        venta.IdVenta = reader["IdVenta"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdVenta"]);
                        venta.IdEmpleado = reader["IdEmpleado"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdEmpleado"]);
                        venta.TipoDocumento = reader["TipoDocumento"] == DBNull.Value ? string.Empty : reader["TipoDocumento"].ToString();
                        venta.NroComprobante = reader["NroDocumento"] == DBNull.Value ? string.Empty : reader["NroDocumento"].ToString();
                        venta.Total = reader["Total"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Total"]);
                        venta.Tipofactura = reader["Tipofactura"] == DBNull.Value ? string.Empty : reader["Tipofactura"].ToString();
                        venta.Restante = reader["Restante"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Restante"]);
                        venta.FechaVenta = reader["FechaVenta"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(reader["FechaVenta"]);
                        venta.NombreCliente = reader["NombreCliente"] == DBNull.Value ? string.Empty : reader["NombreCliente"].ToString();
                        venta.UltimaFechaPago = reader["UltimaFechaPago"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(reader["UltimaFechaPago"]);
                        //venta.borrador = reader["borrado"] == DBNull.Value ? 0 : Convert.ToInt32(reader["borrado"]);
                        venta.Direccion = reader["Direccion"] == DBNull.Value ? string.Empty : reader["Direccion"].ToString();
                        //venta.rncCliente = reader["rncCliente"] == DBNull.Value ? string.Empty : reader["rncCliente"].ToString();

                        clsGenericList.listVentas.Add(venta);
                        clsGenericList.idsVentas.Add(venta.IdVenta);
                    }
                }
                catch (Exception ex)
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
                }
            }
            #endregion

            #region Listado Productos
            if (clsGenericList.listProducto is null)
            {
                clsGenericList.listProducto = new List<Producto>();

                clsProducto P = new clsProducto();
                DataTable dtP = new DataTable();
                dtP = P.Listar();

                try
                {
                    foreach (DataRow reader in dtP.Rows)
                    {
                        Producto product = new Producto();

                        product.m_IdP = reader["IdProducto"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdProducto"]);
                        product.m_IdCategoria = reader["IdCategoria"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdCategoria"]);
                        product.m_Producto = reader["Nombre"] == DBNull.Value ? string.Empty : reader["Nombre"].ToString();
                        product.m_tipoGoma = reader["tipoGOma"] == DBNull.Value ? string.Empty : reader["tipoGOma"].ToString();
                        product.m_itbis = reader["itbis"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["itbis"]);
                        product.m_PrecioVenta = reader["PrecioVenta"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PrecioVenta"]);
                        product.m_PrecioCompra = reader["PrecioCompra"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PrecioCompra"]);
                        product.m_Preciomax = reader["Pmax"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Pmax"]);
                        product.m_Preciomin = reader["Pmin"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Pmin"]);
                        product.m_FechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"]);
                        product.m_Stock = reader["Stock"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Stock"]);
                        product.m_FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
                        product.m_Marca = reader["Marca"] == DBNull.Value ? string.Empty : reader["Marca"].ToString();

                        clsGenericList.listProducto.Add(product);
                    }
                }
                catch (Exception ex)
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
                }
            }
            #endregion
        }
        public void llenarid()
        {
            Cx.Desconectar();
            string cadSql = "select top(1) id_caja  from Caja order by id_caja desc";

            SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);
            Cx.Conectar();

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
            {
                idCaja = leer["id_caja"].ToString();
            }
            Cx.Desconectar();
        }

        public void RecuperarDatosSesion()
        {
            DataRow row;
            DataTable dt = new DataTable();
            dt = U.DevolverDatosSesion(txtUser.Text, txtPassword.Text);
            if (dt.Rows.Count == 1)
            {
                row = dt.Rows[0];
                Program.IdEmpleadoLogueado = Convert.ToInt32(row[0].ToString());
                Program.NombreEmpleadoLogueado = row[1].ToString();
                Program.isAdminUser = row[2].ToString() == "Administrador";
            }
        }
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnIngresar.PerformClick();
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRecuperar r = new frmRecuperar();
            r.Show();
        }
      
        public void fechaVenc()
        {
            Cx.Desconectar();
            string cadSql = "select top(1) FechaVenc  from NomEmp order by idEmp desc";

            SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);
            Cx.Conectar();

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
            {
                FechaVenc = Convert.ToDateTime(leer["FechaVenc"]);
            }
            Cx.Desconectar();
        }
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            panelmontoinicial.Visible = true;
            fechaVenc();
            llenarid();
            obtenerFiladeCaja();
            circularProgressBar1.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cx.Desconectar();
            FrmMenuPrincipal MP = new FrmMenuPrincipal();
            using (SqlCommand cmd = new SqlCommand("abrir_caja", Cx.conexion))
            {
                string id_var = "";
                if (idCaja == "" || idCaja == null)
                    id_var = "0";
                else
                    id_var = idCaja;

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_caja", SqlDbType.Int).Value = id_var;
                cmd.Parameters.Add("@monto", SqlDbType.Decimal).Value = Convert.ToDecimal(txtmontoinicial.Text);
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Today;

                Cx.Conectar();
                cmd.ExecuteNonQuery();
                Cx.Desconectar();
            }
            RecuperarDatosSesion();
            MP.Show();
            this.Hide();
        }


        private void label5_Click_1(object sender, EventArgs e)
        {
            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Salir.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FrmMenuPrincipal MP = new FrmMenuPrincipal();
            if (circularProgressBar1.Value < 100)
            {
                circularProgressBar1.Value += 1;
                circularProgressBar1.Text = circularProgressBar1.Value.ToString();
            }

            if (circularProgressBar1.Value == 100)
            {
                timer1.Stop();
                MP.Show();
                circularProgressBar1.Visible = false;
                this.Hide();
            }
        }
    }
}

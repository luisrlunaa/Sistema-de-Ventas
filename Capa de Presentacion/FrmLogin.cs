using CapaEnlaceDatos;
using CapaLogicaNegocio;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class FrmLogin : DevComponents.DotNetBar.Metro.MetroForm
    {
        clsUsuarios U = new clsUsuarios();
        clsManejador M = new clsManejador();

        public FrmLogin()
        {
            InitializeComponent();
        }

        public DateTime FechaVenc;
        bool tienefila = false;
        public void llenar_data()
        {
            M.Desconectar();
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            comando.Connection = M.conexion;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "SELECT id_caja, monto_inicial,fecha  FROM Caja where monto_final =0 AND fecha = convert(datetime,CONVERT(varchar(10), getdate(), 103),103)";
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            M.Conectar();
            //limpiamos los renglones de la datagridview
            dgvCaja.Rows.Clear();
            //a la variable DataReader asignamos  el la variable de tipo SqlCommand
            dr = comando.ExecuteReader();
            while (dr.Read())
            {
                //variable de tipo entero para ir enumerando los la filas del datagridview
                int renglon = dgvCaja.Rows.Add();
                // especificamos en que fila se mostrará cada registro
                // nombredeldatagrid.filas[numerodefila].celdas[nombrdelacelda].valor=\

                dgvCaja.Rows[renglon].Cells["id_caja"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id_caja")));
                dgvCaja.Rows[renglon].Cells["monto"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("monto_inicial")).ToString("C2"));
                dgvCaja.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("fecha"));

                tienefila = true;
            }
            M.Desconectar();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Salir.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                Application.Exit();
            }
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

                        if (FechaVenc == DateTime.Today)
                        {
                            if (DevComponents.DotNetBar.MessageBoxEx.Show("Licencia del producto ha Cadudado, Favor ponerse en contacto con su suplidor para Renovar la misma, Desea Renovar Ahora?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                            {
                                frmRenovar renovar = new frmRenovar();
                                renovar.Show();
                            }
                        }
                        else
                        {
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
                                if (Program.CargoEmpleadoLogueado == "Administrador")
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
                                    if (panelmontoinicial.Visible)
                                    {
                                        if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Desea ingresar Monto Inicial de Caja?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                                        {
                                            panelmontoinicial.Visible = false;
                                        }
                                        else
                                        {
                                            M.Desconectar();
                                            using (SqlCommand cmd = new SqlCommand("abrir_caja", M.conexion))
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

                                                M.Conectar();
                                                cmd.ExecuteNonQuery();
                                                M.Desconectar();
                                            }

                                            llenar_data();
                                            RecuperarDatosSesion();

                                            MP.Show();
                                            this.Hide();
                                        }
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
        public void llenarid()
        {
            M.Desconectar();
            string cadSql = "select top(1) id_caja,monto_inicial  from Caja order by id_caja desc";
            M.Conectar();
            SqlCommand comando = new SqlCommand(cadSql, M.conexion);

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
            {
                Program.idcaja = Convert.ToInt32(leer["id_caja"]);
                Program.MontoInicial = Convert.ToDecimal(leer["monto_inicial"]);
            }
            M.Desconectar();
        }

        public void fechaVenc()
        {
            M.Desconectar();
            string cadSql = "select top(1) FechaVenc  from NomEmp order by idEmp desc";

            SqlCommand comando = new SqlCommand(cadSql, M.conexion);
            M.Conectar();

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
            {
                FechaVenc = Convert.ToDateTime(leer["FechaVenc"]);
            }
            M.Desconectar();
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
                Program.CargoEmpleadoLogueado = row[2].ToString();
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
        bool activo;
        private void txtUser_Leave(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                activo = true;
            }
            else
            {
                activo = false;
            }

            if (activo == false)
            {
                M.Desconectar();
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Empleado INNER JOIN " +
                "dbo.Usuario ON dbo.Empleado.IdEmpleado = dbo.Usuario.IdEmpleado AND dbo.Empleado.IdEmpleado = " +
                "dbo.Usuario.IdEmpleado WHERE dbo.Usuario.Usuario = @Clave", M.conexion);
                command.Parameters.AddWithValue("@Clave", txtUser.Text);

                M.Conectar();
                SqlDataReader leer = command.ExecuteReader();

                if (leer.Read() == false)
                {
                    MessageBox.Show("Usuario No Existente");
                    txtUser.Clear();
                    txtUser.Focus();

                    M.conexion.Close();
                }
                M.Desconectar();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmMenuPrincipal MP = new FrmMenuPrincipal();
            Program.LoginStatus = "Inventario";
            MP.Show();
            this.Hide();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            fechaVenc();
            llenarid();
            llenar_data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            M.Desconectar();
            FrmMenuPrincipal MP = new FrmMenuPrincipal();
            using (SqlCommand cmd = new SqlCommand("abrir_caja", M.conexion))
            {
                string id_var = "";
                if (Program.idcaja.ToString() == "" || Program.idcaja.ToString() == null)
                    id_var = "0";
                else
                    id_var = Program.idcaja.ToString();

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_caja", SqlDbType.Int).Value = id_var;
                cmd.Parameters.Add("@monto", SqlDbType.Decimal).Value = Convert.ToDecimal(txtmontoinicial.Text);
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Today;

                M.Conectar();
                cmd.ExecuteNonQuery();
                M.Desconectar();
            }

            llenar_data();
            RecuperarDatosSesion();
            MP.Show();
            this.Hide();
        }
    }
}

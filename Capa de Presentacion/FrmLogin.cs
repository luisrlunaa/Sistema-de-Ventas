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
        clsManejador Cx = new clsManejador();

        public FrmLogin()
        {
            InitializeComponent();
        }

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
                    else if (Mensaje == "El Nombre de Usuario no Existe.")
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show(Mensaje, "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        txtUser.Clear();
                        txtPassword.Clear();
                        txtUser.Focus();
                    }
                    else
                    {
                        if (FechaVenc.Date <= DateTime.Today.Date)
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
                            }
                            else if (rbVentas.Checked)
                            {
                                Program.LoginStatus = "Ventas";
                                RecuperarDatosSesion();
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
                                RecuperarDatosSesion();
                                if (!tienefila && panelmontoinicial.Visible)
                                {
                                    if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Desea ingresar Monto Inicial de Caja?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                                    {
                                        panelmontoinicial.Visible = false;
                                    }
                                    else
                                    {
                                        Cx.Desconectar();
                                        insertCaja();
                                    }
                                }
                                else if (tienefila && panelmontoinicial.Visible)
                                {
                                    FrmMenuPrincipal MP = new FrmMenuPrincipal();
                                    MP.Show();
                                    this.Hide();
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
            Cx.Desconectar();
            string cadSql = "select top(1) id_caja  from Caja order by id_caja desc";

            SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);
            Cx.Conectar();

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
            {
                var id = string.IsNullOrWhiteSpace(leer["id_caja"].ToString()) ? 0 : Convert.ToInt32(leer["id_caja"]);
                Program.idcaja = id;
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
            Program.idcaja = 0;
            panelmontoinicial.Visible = true;
            fechaVenc();
            llenarid();
            obtenerFiladeCaja();
        }

        public void insertCaja()
        {
            using (SqlCommand cmd = new SqlCommand("abrir_caja", Cx.conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@id_caja", SqlDbType.Int).Value = Program.idcaja;
                cmd.Parameters.Add("@monto", SqlDbType.Decimal).Value = string.IsNullOrWhiteSpace(txtmontoinicial.Text) ? 0 : Convert.ToDecimal(txtmontoinicial.Text);
                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Today;

                Cx.Conectar();
                cmd.ExecuteNonQuery();
                Cx.Desconectar();
            }

            Program.idcaja = Program.idcaja + 1;

            FrmMenuPrincipal MP = new FrmMenuPrincipal();
            MP.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cx.Desconectar();
            RecuperarDatosSesion();
            panelmontoinicial.Show();
            insertCaja();
        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Salir.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using DevComponents.DotNetBar;
using DevComponents.WinForms.Drawing;
using CapaLogicaNegocio;

namespace Capa_de_Presentacion
{
    public partial class FrmLogin : DevComponents.DotNetBar.Metro.MetroForm
    {
        clsUsuarios U = new clsUsuarios();
		clsCx Cx = new clsCx();

        public FrmLogin()
        {
            InitializeComponent();
        }

        public string idCaja;
        bool tienefila = false;
        public void llenar_data()
        {
            //declaramos la cadena  de conexion
            string cadenaconexion = Cx.conet;
            //variable de tipo Sqlconnection
            SqlConnection con = new SqlConnection();
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            con.ConnectionString = cadenaconexion;
            comando.Connection = con;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "SELECT id_caja, monto_inicial,fecha  FROM Caja where monto_final =0 AND fecha = convert(datetime,CONVERT(varchar(10), getdate(), 103),103)";
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            con.Open();
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
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Salir.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes) {
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

                        if (tienefila)
                        {
                            MP.Show();
                            this.Hide();
                            RecuperarDatosSesion();
                        }
                        else
                        {
                            using (SqlCommand cmd = new SqlCommand("abrir_caja", Cx.conexion))
                            {
                                string id_var = "";
                                if (idCaja == "")
                                    id_var = "0";
                                else
                                    id_var = idCaja;

                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("@id_caja", SqlDbType.Int).Value = id_var;
                                cmd.Parameters.Add("@monto", SqlDbType.Decimal).Value = 0;
                                cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Today;

                                Cx.conexion.Open();
                                cmd.ExecuteNonQuery();
                                Cx.conexion.Close();
                            }
                            llenar_data();
                            MP.Show();
                            this.Hide();
                            RecuperarDatosSesion();
                        }
					}
				}
				else {
                    DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese su Contraseña.","Sistema de Ventas.",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtPassword.Focus();
                }
            }else{
                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Nombre de Usuario.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUser.Focus();
                }
        }
        public void llenarid()
        {
            string cadSql = "select top(1) id_caja  from Caja order by id_caja desc";

            SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);
            Cx.conexion.Open();

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
            {
                idCaja = leer["id_caja"].ToString();
            }
            Cx.conexion.Close();
        }
        public void RecuperarDatosSesion() {
            DataRow row;
            DataTable dt = new DataTable();
            dt = U.DevolverDatosSesion(txtUser.Text,txtPassword.Text);
            if (dt.Rows.Count == 1) {
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
        if (txtUser.Text== "")
			{
				activo = true;
			}
		else
			{
				activo = false;
			}

		if(activo==false)
			{
				SqlCommand command = new SqlCommand("SELECT * FROM dbo.Empleado INNER JOIN " +
				"dbo.Usuario ON dbo.Empleado.IdEmpleado = dbo.Usuario.IdEmpleado AND dbo.Empleado.IdEmpleado = " +
				"dbo.Usuario.IdEmpleado WHERE dbo.Usuario.Usuario = @Clave", Cx.conexion);
				command.Parameters.AddWithValue("@Clave", txtUser.Text);

                Cx.conexion.Open();
                SqlDataReader leer = command.ExecuteReader();
               
                if (leer.Read() == false)
                {
                    MessageBox.Show("Usuario No Existente");
                    txtUser.Clear();
                    txtUser.Focus();
       
                    Cx.conexion.Close();
                }
                Cx.conexion.Close();
            }
		}

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            llenarid();
            llenar_data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmMenuPrincipal MP = new FrmMenuPrincipal();
            Program.inventario = "Inventario";
            MP.Show();
            this.Hide();
        }
    }
}

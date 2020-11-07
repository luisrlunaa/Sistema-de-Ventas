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
		frmPagar pa = new frmPagar();
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
						pa.gbCierre.Visible = false;
						pa.gbPagar.Visible = false;
						pa.btnCerrar.Visible = false;
						pa.Show();

						this.Hide();
						RecuperarDatosSesion();
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
	}
}

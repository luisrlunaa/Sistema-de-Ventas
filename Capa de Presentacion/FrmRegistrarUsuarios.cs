using CapaLogicaNegocio;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Capa_de_Presentacion
{
    public partial class FrmRegistrarUsuarios : DevComponents.DotNetBar.Metro.MetroForm

    {
        clsUsuarios U = new clsUsuarios();
        clsCx Cx = new clsCx();
        public FrmRegistrarUsuarios()
        {
            InitializeComponent();
        }

        private void FrmRegistrarUsuarios_Load(object sender, EventArgs e)
        {
            txtEmp.Text = Program.IdEmpleado + "";
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Trim() != "")
            {
                if (txtPassword.Text.Trim() != "")
                {
                    if (Program.IdEmpleado != 0)
                    {
                        U.IdEmpleado = Program.IdEmpleado;
                        U.User = txtUser.Text;
                        U.Password = txtPassword.Text;

                        DevComponents.DotNetBar.MessageBoxEx.Show(U.RegistrarUsuarios(), "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        Limpiar();
                    }
                    else
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show("Lo Sentimos, Pero Usted debe Eligir un \n Empleado para Crear una Cuenta de Usuario.\n \n G R A C I A S.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese su Usuario.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUser.Focus();
            }
        }

        private void Limpiar()
        {
            txtPassword.Clear();
            txtUser.Clear();
            Program.IdEmpleado = 0;
            txtUser.Focus();
            lblCargo.Text = "";
            lblDni.Text = "";
            lblEmpleado.Text = "";
        }
        private void label9_Click(object sender, EventArgs e)
        {
            Program.abiertosecundarias = false;
            Program.abierto = false;
            this.Close();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void FrmRegistrarUsuarios_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

    }
}

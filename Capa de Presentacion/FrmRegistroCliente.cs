using CapaEnlaceDatos;
using CapaLogicaNegocio;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class FrmRegistroCliente : DevComponents.DotNetBar.Metro.MetroForm
    {
        private clsCliente C = new clsCliente();
        clsManejador M = new clsManejador();

        public FrmRegistroCliente()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            M.Desconectar();
            if (txtDni.Text.CleanSpace() != "")
            {
                if (txtApellidos.Text.CleanSpace() != "")
                {
                    if (txtNombres.Text.CleanSpace() != "")
                    {
                        if (txtDireccion.Text.CleanSpace() != "")
                        {
                            if (txtTelefono.Text.CleanSpace() != "")
                            {
                                if (Program.Evento == 0)
                                {
                                    using (SqlCommand cmd = new SqlCommand("RegistrarCliente", M.conexion))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        cmd.Parameters.Add("@Dni", SqlDbType.NVarChar).Value = txtDni.Text;
                                        cmd.Parameters.Add("@Apellidos", SqlDbType.NVarChar).Value = txtApellidos.Text;
                                        cmd.Parameters.Add("@Nombres", SqlDbType.NVarChar).Value = txtNombres.Text;
                                        cmd.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = txtDireccion.Text;
                                        cmd.Parameters.Add("@Telefono", SqlDbType.NVarChar).Value = txtTelefono.Text;
                                        cmd.Parameters.Add("@estado", SqlDbType.Int).Value = 1;

                                        DevComponents.DotNetBar.MessageBoxEx.Show("Se Registro Correctamente", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        M.Conectar();
                                        cmd.ExecuteNonQuery();
                                        M.Desconectar();

                                        Limpiar();
                                    }
                                }
                            }
                            else
                            {
                                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese N° de Teléfono o Celular.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtTelefono.Focus();
                            }
                        }
                        else
                        {
                            DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Dirección del Cliente.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtDireccion.Focus();
                        }
                    }
                    else
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Nombre(s) del Cliente.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtNombres.Focus();
                    }
                }
                else
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Apellidos del Cliente.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtApellidos.Focus();
                }
            }
            else
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese N° de D.N.I del Cliente.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDni.Focus();
            }
        }

        private void Limpiar()
        {
            txtDni.Text = "";
            txtApellidos.Clear();
            txtNombres.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtDni.Focus();
        }

        private void FrmRegistroCliente_Load(object sender, EventArgs e)
        {
            txtDni.Focus();
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }

        private void txtApellidos_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.sololetras(e);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Program.abiertosecundarias = false;
            Program.abierto = false;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            M.Desconectar();
            if (txtDni.Text.CleanSpace() != "")
            {
                if (txtApellidos.Text.CleanSpace() != "")
                {
                    if (txtNombres.Text.CleanSpace() != "")
                    {
                        if (txtDireccion.Text.CleanSpace() != "")
                        {
                            if (txtTelefono.Text.CleanSpace() != "")
                            {
                                if (Program.Evento == 1)
                                {
                                    using (SqlCommand cmd = new SqlCommand("ActualizarCliente", M.conexion))
                                    {
                                        cmd.CommandType = CommandType.StoredProcedure;

                                        cmd.Parameters.Add("@Dni", SqlDbType.NVarChar).Value = txtDni.Text;
                                        cmd.Parameters.Add("@Apellidos", SqlDbType.NVarChar).Value = txtApellidos.Text;
                                        cmd.Parameters.Add("@Nombres", SqlDbType.NVarChar).Value = txtNombres.Text;
                                        cmd.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = txtDireccion.Text;
                                        cmd.Parameters.Add("@Telefono", SqlDbType.NVarChar).Value = txtTelefono.Text;
                                        cmd.Parameters.Add("@estado", SqlDbType.Int).Value = 1;

                                        DevComponents.DotNetBar.MessageBoxEx.Show("Se Actualizo Correctamente", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        M.Conectar();
                                        cmd.ExecuteNonQuery();
                                        M.Desconectar();
                                        Limpiar();
                                    }
                                }
                            }
                            else
                            {
                                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese N° de Teléfono o Celular.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtTelefono.Focus();
                            }
                        }
                        else
                        {
                            DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Dirección del Cliente.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtDireccion.Focus();
                        }
                    }
                    else
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Nombre(s) del Cliente.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtNombres.Focus();
                    }
                }
                else
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Apellidos del Cliente.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtApellidos.Focus();
                }
            }
            else
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese N° de D.N.I del Cliente.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDni.Focus();
            }

        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void FrmRegistroCliente_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}

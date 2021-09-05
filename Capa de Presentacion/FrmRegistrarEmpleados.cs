using CapaEnlaceDatos;
using CapaLogicaNegocio;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class FrmRegistrarEmpleados : DevComponents.DotNetBar.Metro.MetroForm
    {
        clsCargo C = new clsCargo();
        clsEmpleado Em = new clsEmpleado();
        clsManejador Cx = new clsManejador();
        int Listado = 0;
        public FrmRegistrarEmpleados()
        {
            InitializeComponent();
        }

        private void FrmRegistrarEmpleados_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Interval = 1000;
            CargarComboBox();
        }

        private void CargarComboBox()
        {
            comboBox1.DataSource = C.Listar();
            comboBox1.DisplayMember = "Descripcion";
            comboBox1.ValueMember = "IdCargo";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.abiertosecundarias == false)
            {
                FrmRegistrarCargo C = new FrmRegistrarCargo();
                Program.abiertosecundarias = true;
                C.Show();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Char EstadoCivil = 'S';
            switch (cbxEstadoCivil.SelectedIndex)
            {
                case 1: EstadoCivil = 'S'; break;
                case 2: EstadoCivil = 'C'; break;
                case 3: EstadoCivil = 'D'; break;
                case 4: EstadoCivil = 'V'; break;
            }

            if (txtDni.Text.Trim() != "")
            {
                if (txtApellidos.Text.Trim() != "")
                {
                    if (txtNombres.Text.Trim() != "")
                    {
                        if (txtDireccion.Text.Trim() != "")
                        {
                            if (comboBox1.SelectedValue != null)
                            {
                                Cx.Desconectar();
                                using (SqlCommand cmd = new SqlCommand("MantenimientoEmpleados", Cx.conexion))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.Add("@IdCargo", SqlDbType.NVarChar).Value = Convert.ToInt32(comboBox1.SelectedValue);
                                    cmd.Parameters.Add("@Dni", SqlDbType.NVarChar).Value = txtDni.Text;
                                    cmd.Parameters.Add("@Apellidos", SqlDbType.NVarChar).Value = txtApellidos.Text;
                                    cmd.Parameters.Add("@Nombres", SqlDbType.NVarChar).Value = txtNombres.Text;
                                    cmd.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = txtDireccion.Text;
                                    cmd.Parameters.Add("@EstadoCivil", SqlDbType.Char).Value = EstadoCivil;
                                    cmd.Parameters.Add("@Sexo", SqlDbType.Char).Value = rbnMasculino.Checked == true ? 'M' : 'F';
                                    cmd.Parameters.Add("@FechaNac", SqlDbType.Date).Value = dateTimePicker1.Text;

                                    DevComponents.DotNetBar.MessageBoxEx.Show("Se Realizo Correctamente", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    Cx.Conectar();
                                    cmd.ExecuteNonQuery();
                                    Cx.Desconectar();
                                    Limpiar();
                                }
                            }
                            else
                            {
                                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese el cargo que ejercera este Empleado", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            cbxEstadoCivil.SelectedIndex = 0;
            txtApellidos.Clear();
            txtDireccion.Clear();
            txtDni.Clear();
            txtNombres.Clear();
            rbnMasculino.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            txtIdE.Clear();
            Program.IdCargo = 0;
            comboBox1.SelectedIndex = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (Listado)
            {
                case 0: CargarComboBox(); break;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }
        private void txtApellidos_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.sololetras(e);
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
        private void FrmRegistrarEmpleados_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

    }
}


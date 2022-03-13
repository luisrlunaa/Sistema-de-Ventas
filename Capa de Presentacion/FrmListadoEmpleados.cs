using CapaEnlaceDatos;
using CapaLogicaNegocio;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class FrmListadoEmpleados : DevComponents.DotNetBar.Metro.MetroForm
    {
        clsEmpleado E = new clsEmpleado();
        clsManejador Cx = new clsManejador();
        int Listado = 0;
        public FrmListadoEmpleados()
        {
            InitializeComponent();
        }

        private void FrmListadoEmpleados_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            timer1.Start();
            timer1.Interval = 1000;
            MostrarListadoEmpleados();
            btnActualizar.Enabled = Program.isAdminUser;
        }

        private void MostrarListadoEmpleados()
        {
            DataTable dt = new DataTable();
            dt = E.ListadoEmpleados();
            dataGridView1.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(dt.Rows[i][0]);
                dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();
                dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i][2].ToString();
                dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i][3].ToString();
                dataGridView1.Rows[i].Cells[4].Value = dt.Rows[i][4].ToString();
                dataGridView1.Rows[i].Cells[5].Value = dt.Rows[i][5].ToString();
                dataGridView1.Rows[i].Cells[6].Value = Convert.ToDateTime(dt.Rows[i][6].ToString()).ToShortDateString();
                dataGridView1.Rows[i].Cells[7].Value = dt.Rows[i][7].ToString();
                dataGridView1.Rows[i].Cells[8].Value = dt.Rows[i][8].ToString();
                dataGridView1.Rows[i].Cells[9].Value = dt.Rows[i][9].ToString();
            }
            dataGridView1.ClearSelection();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmRegistrarEmpleados E = new FrmRegistrarEmpleados();
            E.txtIdE.Text = "0";
            Program.IdCargo = 0;
            E.Show();
            dataGridView1.ClearSelection();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    FrmRegistrarEmpleados E = new FrmRegistrarEmpleados();
                    E.txtIdE.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    Program.IdCargo = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                    E.txtDni.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    E.txtApellidos.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    E.txtNombres.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    E.label11.Text = "Actualizar Empleado";
                    E.btnGrabar.Text = "Actualizar";
                    if (dataGridView1.CurrentRow.Cells[5].Value.ToString() == "M")
                        E.rbnMasculino.Checked = true;
                    else
                        E.rbnFemenino.Checked = true;
                    E.dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[6].Value.ToString());
                    E.txtDireccion.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    if (dataGridView1.CurrentRow.Cells[8].Value.ToString() == "S")
                        E.cbxEstadoCivil.SelectedIndex = 1;
                    else
                        if (dataGridView1.CurrentRow.Cells[8].Value.ToString() == "C")
                        E.cbxEstadoCivil.SelectedIndex = 2;
                    else
                        if (dataGridView1.CurrentRow.Cells[8].Value.ToString() == "D")
                        E.cbxEstadoCivil.SelectedIndex = 3;
                    else
                        E.cbxEstadoCivil.SelectedIndex = 4;
                    E.Show();
                }
                dataGridView1.ClearSelection();
                timer1.Start();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Salir.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (Listado)
            {
                case 0: MostrarListadoEmpleados(); break;
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            if (dataGridView1.RowCount > 0)
            {
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;
                Program.Eid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                textBox1.Text = Program.Eid + "";
                timer1.Stop();
            }
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Desea Crear Una Cuenta de Usuario Para este Empleado.?", "Sistema de Ventas.", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                FrmRegistrarUsuarios U = new FrmRegistrarUsuarios();
                Program.IdEmpleado = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                U.lblEmpleado.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString() + ", " +
                                     dataGridView1.CurrentRow.Cells[4].Value.ToString();
                U.lblDni.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                U.lblCargo.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                U.Show();
            }
        }
        private void txtDatos_TextChanged(object sender, EventArgs e)
        {
            if (txtDatos.TextLength > 0)
            {
                DataTable dt = new DataTable();
                E.Nombres = txtDatos.Text;
                dt = E.BuscarEmpleado(E.Nombres);
                try
                {
                    dataGridView1.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add(dt.Rows[i][0]);
                        dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                        dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();
                        dataGridView1.Rows[i].Cells[2].Value = dt.Rows[i][2].ToString();
                        dataGridView1.Rows[i].Cells[3].Value = dt.Rows[i][3].ToString();
                        dataGridView1.Rows[i].Cells[4].Value = dt.Rows[i][4].ToString();
                        dataGridView1.Rows[i].Cells[5].Value = dt.Rows[i][5].ToString();
                        dataGridView1.Rows[i].Cells[6].Value = Convert.ToDateTime(dt.Rows[i][6].ToString()).ToShortDateString();
                        dataGridView1.Rows[i].Cells[7].Value = dt.Rows[i][7].ToString();
                        dataGridView1.Rows[i].Cells[8].Value = dt.Rows[i][8].ToString();
                        dataGridView1.Rows[i].Cells[9].Value = dt.Rows[i][9].ToString();
                    }
                    dataGridView1.ClearSelection();
                    timer1.Stop();
                }
                catch (Exception ex)
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
                }
            }
            else
            {
                MostrarListadoEmpleados();
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {
            Program.abiertosecundarias = false;
            Program.abierto = false;
            btnActualizar.Enabled = Program.isAdminUser;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cx.Desconectar();
            Program.IdEmpleado = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            if (Program.IdEmpleado > 0)
            {
                if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Eliminar este Empleado.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    using (SqlCommand cmd = new SqlCommand("eliminarUsuario", Cx.conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = Program.IdEmpleado;

                        Cx.Conectar();
                        cmd.ExecuteNonQuery();
                        Cx.Desconectar();

                        using (SqlCommand cmd1 = new SqlCommand("eliminarEmpleado", Cx.conexion))
                        {
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = Program.IdEmpleado;

                            Cx.Conectar();
                            cmd1.ExecuteNonQuery();
                            Cx.Desconectar();
                            MostrarListadoEmpleados();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por Favor Seleccione un producto antes de eliminarlo");
            }
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void FrmListadoEmpleados_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}

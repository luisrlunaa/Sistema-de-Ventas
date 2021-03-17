using CapaLogicaNegocio;
using System;
using System.Data;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class FrmListadoUsuario : DevComponents.DotNetBar.Metro.MetroForm
    {
        private clsUsuarios C = new clsUsuarios();
        int Listado = 0;
        public FrmListadoUsuario()
        {
            InitializeComponent();
        }

        private void FrmListadoCargos_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Interval = 5000;
            ListarElementos();
            dataGridView1.ClearSelection();
        }

        private void ListarElementos()
        {
            DataTable dt = new DataTable();
            dt = C.Listar();
            try
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(dt.Rows[i][0]);
                    dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][1].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][2].ToString();
                }
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
            }
        }

        private void txtBuscarCargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    BusquedaUsuario();
                }
                else
                {
                    ListarElementos();
                }
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
            }
        }

        private void BusquedaUsuario()
        {
            try
            {
                DataTable dt = new DataTable();
                C.User = txtBuscarCargo.Text;
                dt = C.BusquedaUsuario(C.User);
                dataGridView1.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(dt.Rows[i][0]);
                    dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][1].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][2].ToString();
                }
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (Listado)
            {
                case 0: ListarElementos(); break;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Program.abiertosecundario = false;
            Program.abierto = false;
            this.Close();
        }
    }
}

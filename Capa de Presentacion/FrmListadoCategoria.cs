using CapaEnlaceDatos;
using CapaLogicaNegocio;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class FrmListadoCategoria : DevComponents.DotNetBar.Metro.MetroForm
    {
        private clsCategoria C = new clsCategoria();
        clsManejador M = new clsManejador();

        public FrmListadoCategoria()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                MessageBox.Show("La Fila no debe Estar Seleccionada.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                FrmRegistrarCategoria C = new FrmRegistrarCategoria();
                C.Show();
                if (dataGridView1.SelectedRows.Count > 0)
                    Program.Evento = 1;
                else
                    Program.Evento = 0;
                dataGridView1.ClearSelection();
            }
        }

        private void FrmListadoCategoria_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            ListarElementos();
            dataGridView1.ClearSelection();
            btnEditar.Enabled = Program.isAdminUser;
        }

        private void ListarElementos()
        {
            dataGridView1.ClearSelection();
            DataTable dt = new DataTable();
            dt = C.Listar();
            try
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(dt.Rows[i][0]);
                    dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;
            button2.Enabled = true;
        }

        private void txtBuscarCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ListarBusqueda();
            }
            else
            {
                ListarElementos();
            }
        }

        private void ListarBusqueda()
        {
            try
            {
                DataTable dt = new DataTable();
                clsCategoria C = new clsCategoria();
                C.Descripcion = txtBuscarCategoria.Text;
                dt = C.BuscarCategoria(C.Descripcion);
                dataGridView1.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(dt.Rows[i][0]);
                    dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                FrmRegistrarCategoria C = new FrmRegistrarCategoria();
                C.IdC.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                C.txtCategoria.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                if (dataGridView1.SelectedRows.Count > 0)
                    Program.Evento = 1;
                else
                    Program.Evento = 0;
                dataGridView1.ClearSelection();
                C.Show();
            }
            else
            {
                MessageBox.Show("Debe Seleccionar la Fila a Editar Datos.", "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Program.abiertosecundarias = false;
            Program.abierto = false;
            btnEditar.Enabled = Program.isAdminUser;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Eliminar esta Categoria.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    var id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    string sql = "delete Categoria where IdCategoria = @id";
                    using (SqlCommand cmd = new SqlCommand(sql, M.conexion))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                        try
                        {
                            M.Conectar();
                            cmd.ExecuteNonQuery();
                            M.Desconectar();
                            ListarElementos();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al realizar el eliminar. \n" + ex);
                            return;
                        }
                    }
                }
            }
        }
    }
}

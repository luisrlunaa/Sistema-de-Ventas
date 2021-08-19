﻿using CapaLogicaNegocio;
using System;
using System.Data;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class FrmListadoCargos : DevComponents.DotNetBar.Metro.MetroForm
    {
        private clsCargo C = new clsCargo();
        int Listado = 0;
        public FrmListadoCargos()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmRegistrarCargo C = new FrmRegistrarCargo();
            if (dataGridView1.SelectedRows.Count > 0)
                Program.Evento = 1;
            else
                Program.Evento = 0;
            dataGridView1.ClearSelection();
            C.Show();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                FrmRegistrarCargo C = new FrmRegistrarCargo();
                C.txtIdC.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                C.txtCargo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                if (dataGridView1.SelectedRows.Count > 0)
                    Program.Evento = 1;
                else
                    Program.Evento = 0;
                dataGridView1.ClearSelection();
                C.Show();
            }
            else
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Debe Seleccionar la Fila a Editar Datos.", "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void FrmListadoCargos_Load(object sender, EventArgs e)
        {
            ListarElementos();
            dataGridView1.ClearSelection();
            if (Program.CargoEmpleadoLogueado != "Administrador")
            {
                btnEditar.Enabled = false;
            }
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
                    dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0].ToString();
                    dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1].ToString();
                }
                dataGridView1.ClearSelection();
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;
                timer1.Stop();
            }
        }

        private void txtBuscarCargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    BusquedaCargo();
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

        private void BusquedaCargo()
        {
            try
            {
                DataTable dt = new DataTable();
                C.Descripcion = txtBuscarCargo.Text;
                dt = C.BusquedaCargo(C.Descripcion);
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
                DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (e.KeyChar == 13)
                {
                    DialogResult Resultado = new DialogResult();
                    Resultado = DevComponents.DotNetBar.MessageBoxEx.Show("Está Seguro que Desea Editar Está Fila.", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (Resultado == DialogResult.Yes)
                    {
                        if (dataGridView1.SelectedRows.Count > 0)
                        {
                            FrmRegistrarCargo C = new FrmRegistrarCargo();
                            C.txtIdC.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                            C.txtCargo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                            if (dataGridView1.SelectedRows.Count > 0)
                                Program.Evento = 1;
                            else
                                Program.Evento = 0;
                            dataGridView1.ClearSelection();
                            C.Show();
                        }
                    }
                }
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
            Program.abierto = false;
            Program.abiertosecundarias = false;
            if (Program.CargoEmpleadoLogueado != "Administrador")
            {
                btnEditar.Enabled = false;
            }
            this.Close();
        }
    }
}

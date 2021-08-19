using CapaLogicaNegocio;
using DevComponents.DotNetBar;
using System;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class FrmRegistrarCategoria : DevComponents.DotNetBar.Metro.MetroForm
    {
        private clsCategoria C = new clsCategoria();

        public FrmRegistrarCategoria()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("¿Está Seguro que Desea Salir.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            FrmRegistroProductos product = new FrmRegistroProductos();
            clsCategoria C = new clsCategoria();
            String Mensaje = "";

            try
            {
                if (txtCategoria.Text.Trim() != "")
                {
                    if (Program.Evento == 0)
                    {
                        C.Descripcion = txtCategoria.Text;
                        Mensaje = C.RegistrarCategoria();
                        if (Mensaje == "Categoria ya se encuentra Registrada.")
                        {
                            MessageBoxEx.Show(Mensaje, "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBoxEx.Show(Mensaje, "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            product.ListarElementos();
                            product.Refresh();
                            Limpiar();
                        }
                    }
                    else
                    {
                        C.IdC = Convert.ToInt32(IdC.Text);
                        C.Descripcion = txtCategoria.Text;
                        MessageBoxEx.Show(C.ActualizarCategoria(), "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        product.ListarElementos();
                        Limpiar();
                    }
                }
                else
                {
                    MessageBoxEx.Show("Por Favor Digíte Datos.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCategoria.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show(ex.Message);
            }
        }

        private void Limpiar()
        {
            txtCategoria.Clear();
            txtCategoria.Focus();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Program.abiertosecundarias = false;
            Program.abierto = false;
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmListadoCategoria C = new FrmListadoCategoria();
            C.Show();
        }
    }
}

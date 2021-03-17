using System;
using System.Windows.Forms;

#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
using CapaLogicaNegocio;

namespace Capa_de_Presentacion
{
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
    public partial class FrmRegistrarCargo : DevComponents.DotNetBar.Metro.MetroForm
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
    {
        private clsCargo C = new clsCargo();

        public FrmRegistrarCargo()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Program.abierto = false;
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            String Mensaje = "";
            try
            {
                if (txtCargo.Text.Trim() != "")
                {
                    if (Program.Evento == 0)
                    {
                        C.Descripcion = txtCargo.Text;
                        Mensaje = C.RegistrarCargo();
                        if (Mensaje == "El Cargo ya se Encuentra Registrado.")
                        {
                            DevComponents.DotNetBar.MessageBoxEx.Show(Mensaje, "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            DevComponents.DotNetBar.MessageBoxEx.Show(Mensaje, "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Limpiar();
                        }
                    }
                    else
                    {
                        C.IdCargo = Convert.ToInt32(txtIdC.Text);
                        C.Descripcion = txtCargo.Text;
                        DevComponents.DotNetBar.MessageBoxEx.Show(C.ActualizarCargo(), "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                    }
                }
                else
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese el Cargo a Registrar.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCargo.Focus();
                }
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
            }

        }

        private void Limpiar()
        {
            txtCargo.Text = "";
            txtIdC.Clear();
            txtCargo.Focus();
        }

        private void txtCargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.sololetras(e);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Program.abiertosecundarias = false;
            Program.abierto = false;
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmListadoCargos C = new FrmListadoCargos();
            C.Show();
        }
    }
}

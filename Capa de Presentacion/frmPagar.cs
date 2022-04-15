using CapaEnlaceDatos;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class frmPagar : DevComponents.DotNetBar.Metro.MetroForm
    {
        public frmPagar()
        {
            InitializeComponent();
        }

        clsManejador Cx = new clsManejador();
        //Correo c = new Correo();
        public void llenaridP()
        {
            Cx.Desconectar();
            string cadSql = "select top(1) id_caja,montoactual from Caja order by id_caja desc";

            SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);
            Cx.Conectar();

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
            {
                txtId.Text = Program.idcaja.ToString();
                txtCaja1.Text = leer["montoactual"].ToString();
            }
            Cx.Desconectar();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Cx.Desconectar();
            Program.turno = 0;
            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Desea realizar una copia de seguridad de la base de datos?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                ////////////////////Borrar copia de seguridad de base de datos anterior
                string direccion = @"C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Backup\SalesSystem.bak";
                File.Delete(direccion);

                ////////////////////Creando copia de seguridad de base de datos nueva
                string comand_query = "BACKUP DATABASE [SalesSystem] TO  DISK = N'C:\\Program Files\\Microsoft SQL Server\\MSSQL13.MSSQLSERVER\\MSSQL\\Backup\\SalesSystem.bak'WITH NOFORMAT, NOINIT,  NAME = N'SalesSystem-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
                SqlCommand comando = new SqlCommand(comand_query, Cx.conexion);
                try
                {
                    Cx.Conectar();
                    comando.ExecuteNonQuery();

                    //////////////////////Enviando al correo copia de seguridad de base de datos nueva
                    //c.enviarCorreo("sendingsystembackup@gmail.com", "evitarperdidadedatos/0", "Realizando la creacion diaria de respaldo de base de datos para evitar perdidas de datos en caso de algun problema con el equipo.",
                    //	"Backup de base de datos", "cepedaimport2715@hotmail.com", direccion);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    throw;
                }
                finally
                {
                    Cx.Desconectar();
                    Cx.conexion.Dispose();
                    Application.Exit();
                }
            }
        }
        private void txtpaga_Leave(object sender, EventArgs e)
        {
            if (txtpaga.Text != "")
            {
                decimal paga = decimal.Parse(txtpaga.Text);
                decimal total = decimal.Parse(txtmonto.Text);
                decimal devuelta = Math.Round(paga - total, 2);
                txtDev.Text = devuelta.ToString();
            }
        }
        private void frmPagar_Load_1(object sender, EventArgs e)
        {
            llenaridP();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Program.tipo != "Credito" && Convert.ToDecimal(txtmonto.Text) > Convert.ToDecimal(txtpaga.Text))
            {
                Program.pagoRealizado = 0;
                MessageBox.Show("Por Favor Cambiar tipo de Factura, Las Facturas a Debito no Aceptan Deudas.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtpaga.Text))
                {
                    MessageBox.Show("Debe Ingresar un Monto");
                }
                else
                {
                    if (txtIdp.Text == "")
                        Program.idPago = 0;
                    else
                        Program.idPago = Convert.ToInt32(txtIdp.Text);
                    Program.Devuelta = Convert.ToDecimal(txtDev.Text);
                    Program.Fechapago = dateTimePicker1.Text;

                    decimal dev = decimal.Parse(txtDev.Text);
                    decimal paga = decimal.Parse(txtpaga.Text);

                    if (dev <= 0)
                    {
                        Program.pagoRealizado = paga;
                    }
                    else
                    {
                        Program.pagoRealizado = paga - dev;
                    }

                    Program.realizopago = true;
                    if (DevComponents.DotNetBar.MessageBoxEx.Show("Pago Realizado", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                    {
                        FrmRegistroVentas venta = new FrmRegistroVentas();
                        venta.lbltotal.Text = Program.total + "";
                        venta.lbligv.Text = Program.igv + "";
                        venta.lblsubt.Text = Program.ST + "";
                        this.Hide();
                    }
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            frmMovimientoCaja mo = new frmMovimientoCaja();
            mo.Show();
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            Program.abiertosecundario = false;
            Program.abierto = false;
            FrmRegistroVentas venta = new FrmRegistroVentas();
            venta.lbltotal.Text = Program.total + "";
            venta.lbligv.Text = Program.igv + "";
            venta.lblsubt.Text = Program.ST + "";
            this.Hide();
        }

        private void txtFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }
    }
}

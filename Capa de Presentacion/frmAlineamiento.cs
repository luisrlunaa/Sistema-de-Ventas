using CapaEnlaceDatos;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class frmAlineamiento : DevComponents.DotNetBar.Metro.MetroForm
    {
        public frmAlineamiento()
        {
            InitializeComponent();
        }

        clsManejador M = new clsManejador();
        public void cargar_combo_Tipo(ComboBox tipo)
        {
            M.Desconectar();
            SqlCommand cm = new SqlCommand("CARGARcomboTipotrabajo", M.conexion);
            cm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            tipo.DisplayMember = "descripcion";
            tipo.ValueMember = "id";
            tipo.DataSource = dt;
        }


        public void clean()
        {
            Program.pagoRealizado = 0;
            Program.Id = 0;
            Program.Aros = "";
            Program.total = 0;
            Program.nota = "";
            Program.descripcion = "";
            Program.marca = "";
            Program.modelo = "";

            txtaros.Clear();
            txtTotal.Clear();
            txtnota.Clear();
            txtMarca.Clear();
            txtmodelo.Clear();
            Program.realizopago = false;
            Program.ReImpresion = "";
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            FrmBuscarAlineacionyBalanceo C = new FrmBuscarAlineacionyBalanceo();
            C.Show();
        }

        private void frmTaller_Activated(object sender, EventArgs e)
        {
            txtMarca.Text = Program.marca;
            txtmodelo.Text = Program.modelo;
            cbtipo.Text = Program.descripcion;
            txtaros.Text = Program.Aros;
            txtTotal.Text = Convert.ToString(Program.total);
            txtnota.Text = Program.nota;
            lblidAliBal.Text = Program.Id + "";

            if (Program.Id > 0)
            {
                btnpagar.Hide();
                button1.Show();
                button1.Text = "Imprimir";
                button1.BackColor = Color.Khaki;
            }
        }


        public void tickEstiloP()
        {
            CrearTiket ticket = new CrearTiket();

            //cabecera del ticket.
            //Image img =Image.FromFile("LogoCepeda.png");
            //ticket.HeaderImage = img;
            ticket.TextoCentro(lblLogo.Text);
            ticket.TextoIzquierda(lbldir.Text);
            ticket.TextoIzquierda("TELEFONOS:" + lbltel.Text + "/" + lblTel2.Text);
            ticket.TextoIzquierda("RNC: " + lblrnc.Text);
            ticket.TextoIzquierda("EMAIL:" + lblCorreo.Text);
            ticket.lineasGuio();

            //SUB CABECERA.
            ticket.TextoIzquierda("ATENDIDO: " + txtUsu.Text);
            ticket.TextoIzquierda("FECHA: " + DateTime.Now.ToShortDateString());
            ticket.TextoIzquierda("HORA: " + DateTime.Now.ToShortTimeString());

            //ARTICULOS A VENDER.
            ticket.lineasGuio();

            ticket.TextoIzquierda("TIPO DE TRABAJO: " + cbtipo.Text);
            ticket.TextoIzquierda("MARCA: " + txtMarca.Text);
            ticket.TextoIzquierda("MODELO: " + txtmodelo.Text);
            ticket.TextoIzquierda("AROS No.: " + txtaros.Text);
            ticket.TextoIzquierda("NOTA: " + txtnota.Text);
            ticket.TextoIzquierda("");
            //resumen de la venta
            ticket.AgregarTotales("       COSTO TOTAL DEL SERVICIO : ", decimal.Parse(txtTotal.Text));

            //TEXTO FINAL DEL TICKET
            ticket.TextoIzquierda("EXTRA");
            ticket.TextoIzquierda("-FAVOR REVISE MUY BIEN EL TRABAJO AL RECIBIRLO");
            ticket.TextoIzquierda("-SOLO GARANTIZAMOS EL TRABAJO REALIZADO POR NOSOTROS");
            ticket.TextoCentro("!GRACIAS POR VISITARNOS");

            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.CortaTicket();//CORTAR TICKET
            ticket.ImprimirTicket("POS-80 (copy 1)");//NOMBRE DE LA IMPRESORA
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clean();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            M.Desconectar();
            if (Program.Id > 0)
            {
                tickEstiloP();
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand("RegistrarAlineamientoBalanceo", M.conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = Convert.ToInt32(txtidEmp.Text);
                    cmd.Parameters.Add("@tipoDeTrabajo", SqlDbType.VarChar).Value = cbtipo.Text.ToUpper();
                    cmd.Parameters.Add("@vehiculo", SqlDbType.NVarChar).Value = (txtMarca.Text + " " + txtmodelo.Text).ToUpper();
                    cmd.Parameters.Add("@AroGoma", SqlDbType.Int).Value = !string.IsNullOrWhiteSpace(txtaros.Text) ? Convert.ToInt32(txtaros.Text) : 0;
                    cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Convert.ToDateTime(dtpFecha.Text);
                    cmd.Parameters.Add("@precio", SqlDbType.Decimal).Value = Convert.ToDecimal(txtTotal.Text);
                    cmd.Parameters.Add("@nota", SqlDbType.NVarChar).Value = txtnota.Text;

                    M.Conectar();
                    cmd.ExecuteNonQuery();
                    M.Desconectar();

                    using (SqlCommand cmd2 = new SqlCommand("pagos_re", M.conexion))
                    {
                        cmd2.CommandType = CommandType.StoredProcedure;

                        string idVenta = (Program.idcaja.ToString() + DateTime.Today.Second.ToString() + Program.IdEmpleadoLogueado.ToString());
                        //Tabla de pago
                        cmd2.Parameters.Add("@IdVenta", SqlDbType.Int).Value = Convert.ToInt32(idVenta);
                        cmd2.Parameters.Add("@id_pago", SqlDbType.Int).Value = Program.idPago;
                        cmd2.Parameters.Add("@id_caja", SqlDbType.Int).Value = Program.idcaja;
                        cmd2.Parameters.Add("@monto", SqlDbType.Decimal).Value = Program.Caja;
                        cmd2.Parameters.Add("@ingresos", SqlDbType.Decimal).Value = Program.pagoRealizado;

                        if (Program.Devuelta > 0)
                        {
                            cmd2.Parameters.Add("@egresos", SqlDbType.Decimal).Value = Program.Devuelta;
                        }
                        else
                        {
                            cmd2.Parameters.Add("@egresos", SqlDbType.Decimal).Value = 0;
                        }

                        cmd2.Parameters.Add("@fecha", SqlDbType.DateTime).Value = !string.IsNullOrWhiteSpace(Program.Fechapago) ? Convert.ToDateTime(Program.Fechapago) : DateTime.Today;
                        cmd2.Parameters.Add("@deuda", SqlDbType.Decimal).Value = 0;

                        M.Conectar();
                        cmd2.ExecuteNonQuery();
                        M.Desconectar();
                    }

                    Program.pagoRealizado = 0;
                    tickEstiloP();
                    clean();
                }
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Program.abiertosecundarias = false;
            Program.abierto = false;
            clean();
            this.Close();
        }

        private void txtmodelo_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBuscarAlineacionyBalanceo F = new FrmBuscarAlineacionyBalanceo();
            F.lblLogo.Text = lblLogo.Text;
            F.lblDir.Text = lbldir.Text;
            F.Show();
        }

        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.sololetras(e);
        }

        private void frmAlineamiento_Load(object sender, EventArgs e)
        {
            cargar_combo_Tipo(cbtipo);
            cbtipo.SelectedIndex = 0;
            button1.Hide();
        }

        private void btnpagar_Click(object sender, EventArgs e)
        {
            frmPagar pa = new frmPagar();
            pa.txtmonto.Text = txtTotal.Text;
            pa.gbAbrir.Visible = false;
            pa.btnCerrar.Visible = false;
            button1.Show();

            Program.modelo = txtmodelo.Text;
            Program.descripcion = cbtipo.Text;
            Program.marca = txtMarca.Text;
            Program.Aros = txtaros.Text;
            Program.total = Convert.ToDecimal(txtTotal.Text);
            Program.nota = txtnota.Text;

            pa.Show();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void frmAlineamiento_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}

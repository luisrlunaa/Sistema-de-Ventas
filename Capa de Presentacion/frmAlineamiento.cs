using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using CapaLogicaNegocio;
using System.Drawing;

namespace Capa_de_Presentacion
{
    public partial class frmAlineamiento : DevComponents.DotNetBar.Metro.MetroForm
	{
		public frmAlineamiento()
		{
			InitializeComponent();
		}

		clsCx Cx = new clsCx();
		public void cargar_combo_Tipo(ComboBox tipo)
		{
			SqlCommand cm = new SqlCommand("CARGARcombotrabajoTipo", Cx.conexion);
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
			Program.Modelo = "";
			Program.total = 0;
			Program.nota = "";
			Program.descripcion = "";
			Program.marca = "";
			Program.Imei = "";
			Program.NombreCliente = "";
			Program.averia = "";
			Program.telefono = "";

			txtTelefono.Clear();
			txtModelo.Clear();
			txtprecio.Clear();
			txtnota.Clear();
			txtMarca.Clear();
			txtImei.Clear();
			txtCliente.Clear();
			txtAveria.Clear();
			Program.realizopago = false;
		}

		private void btnBusqueda_Click(object sender, EventArgs e)
		{
			FrmBuscarAlineacionyBalanceo C = new FrmBuscarAlineacionyBalanceo();
			C.Show();
		}

		private void frmTaller_Activated(object sender, EventArgs e)
		{
			txtCliente.Text = Program.NombreCliente.ToUpper();
			txtMarca.Text = Program.marca;
			txtImei.Text = Program.Imei;
			cbtipo.Text = Program.descripcion;
;			txtModelo.Text=	Program.Modelo;
			txtprecio.Text= Convert.ToString(Program.total);
			txtnota.Text=	Program.nota;
			lblidAliBal.Text = Program.Id+"";
			txtAveria.Text=Program.averia.ToUpper();
			txtTelefono.Text = Program.telefono;

			if (Program.Id>0 && Program.descripcion.ToLower()=="entrada")
            {
				btnpagar.Show();
				button1.Hide();
			}

			if(Program.Id > 0 && Program.descripcion.ToLower() == "salida" && Program.pagoRealizado == 0)
            {
				btnpagar.Hide();
				button1.Show();
				button1.Text = "Imprimir";
				button1.BackColor = Color.Khaki;
			}

			if (Program.Id > 0 && Program.descripcion.ToLower() == "salida" && Program.pagoRealizado > 0)
			{
				btnpagar.Hide();
				button1.Show();
			}

		}


		public void tickEstiloP()
		{
			CrearTiket ticket = new CrearTiket();

			//cabecera del ticket.
			//Image img = Image.FromFile("Logo.png");
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
			ticket.TextoIzquierda("CLIENTE: " + txtCliente.Text);
			ticket.TextoIzquierda("NUMERO: " + txtTelefono.Text);
			ticket.TextoIzquierda("MARCA: " + txtMarca.Text);
			ticket.TextoIzquierda("IMEI: " + txtImei.Text);
			ticket.TextoIzquierda("MODELO: " + txtModelo.Text);
			ticket.TextoIzquierda("NOTA: " + txtnota.Text);
			ticket.TextoIzquierda("");
			//resumen de la venta
			ticket.AgregarTotales("COSTO TOTAL DEL SERVICIO : ", decimal.Parse(txtprecio.Text));

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
			ticket.ImprimirTicket("POS80 Printer");//NOMBRE DE LA IMPRESORA
		}

		private void button1_Click(object sender, EventArgs e)
		{
			clean();
		}

        private void button1_Click_1(object sender, EventArgs e)
        {
			if (Program.Id > 0 && Program.descripcion.ToLower() == "salida" && Program.pagoRealizado == 0)
			{
				tickEstiloP();
			}
			else
            {
				using (SqlConnection con = new SqlConnection(Cx.conet))
				{
					using (SqlCommand cmd = new SqlCommand("Registrartaller", con))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(Program.Id);
						cmd.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = Convert.ToInt32(txtidEmp.Text);
						cmd.Parameters.Add("@cliente", SqlDbType.NVarChar).Value = (txtCliente.Text + "." + txtTelefono.Text).ToLower();
						cmd.Parameters.Add("@averia", SqlDbType.NVarChar).Value = txtAveria.Text.ToLower();
						cmd.Parameters.Add("@tipoDeTrabajo", SqlDbType.VarChar).Value = cbtipo.Text.ToUpper();
						cmd.Parameters.Add("@Datos", SqlDbType.NVarChar).Value = (txtModelo.Text + "." + txtImei.Text).ToUpper();
						cmd.Parameters.Add("@Marca", SqlDbType.NVarChar).Value = txtMarca.Text;
						cmd.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Convert.ToDateTime(dtpFecha.Text);
						cmd.Parameters.Add("@precio", SqlDbType.Decimal).Value = Convert.ToDecimal(txtprecio.Text);
						cmd.Parameters.Add("@nota", SqlDbType.NVarChar).Value = txtnota.Text;

						con.Open();
						cmd.ExecuteNonQuery();
						con.Close();

						if(cbtipo.Text.ToLower()=="salida")
                        {
							using (SqlCommand cmd2 = new SqlCommand("pagos_re", con))
							{
								cmd2.CommandType = CommandType.StoredProcedure;

								//Tabla de pago
								cmd2.Parameters.Add("@id_pago", SqlDbType.Int).Value = Program.idPago;
								cmd2.Parameters.Add("@id_caja", SqlDbType.Int).Value = Program.idcaja;
								cmd2.Parameters.Add("@monto", SqlDbType.Decimal).Value = Program.Caja;
								cmd2.Parameters.Add("@ingresos", SqlDbType.Decimal).Value = Program.pagoRealizado;
								cmd2.Parameters.Add("@idVenta", SqlDbType.Int).Value = Convert.ToInt32(Program.Id);

								if (Program.Devuelta > 0)
								{
									cmd2.Parameters.Add("@egresos", SqlDbType.Decimal).Value = Program.Devuelta;
								}
								else
								{
									cmd2.Parameters.Add("@egresos", SqlDbType.Decimal).Value = 0;
								}

								cmd2.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Convert.ToDateTime(Program.Fechapago);
								cmd2.Parameters.Add("@deuda", SqlDbType.Decimal).Value = 0;

								con.Open();
								cmd2.ExecuteNonQuery();
								con.Close();
								MessageBox.Show("Salida Registrada y Pago Confirmado");
							}
						}
                        else
                        {
							MessageBox.Show("Guardado en Taller");
						}

						Program.pagoRealizado = 0;
						tickEstiloP();
						clean();
					}
				}
			}
		}

        private void label18_Click(object sender, EventArgs e)
        {
			Program.abierto = false;
			clean();
			this.Close();
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
			if (Program.Id == 0 && cbtipo.Text.ToLower() == "entrada")
			{
				btnpagar.Hide();
				button1.Show();
				button1.Text = "Guardar";
			}

			clean();
		}

        private void btnpagar_Click(object sender, EventArgs e)
        {
			frmPagar pa = new frmPagar();
			pa.txtmonto.Text = txtprecio.Text;
			button1.Show();

			Program.averia = txtAveria.Text;
			Program.NombreCliente = txtCliente.Text;
			Program.Imei = txtImei.Text;
			Program.descripcion = cbtipo.Text;
			Program.marca = txtMarca.Text;
			Program.Modelo = txtModelo.Text;
			Program.telefono = txtTelefono.Text;
			Program.total = Convert.ToDecimal(txtprecio.Text);
			Program.nota = txtnota.Text;

			pa.Show();
		}

        private void txtprecio_KeyPress(object sender, KeyPressEventArgs e)
        {
			validar.solonumeros(e);
        }
    }
}

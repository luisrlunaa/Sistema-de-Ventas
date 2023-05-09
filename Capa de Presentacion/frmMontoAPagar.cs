using CapaEnlaceDatos;
using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class frmMontoAPagar : DevComponents.DotNetBar.Metro.MetroForm
    {
        public List<Topay> idsAmountToPay;
        public frmMontoAPagar()
        {
            InitializeComponent();
        }

        clsManejador M = new clsManejador();
        private void button2_Click(object sender, EventArgs e)
        {
            var monto = Convert.ToDecimal(txtmonto.Text);
            if (idsAmountToPay != null && idsAmountToPay.Any())
            {
                foreach (var item in idsAmountToPay.Where(x => x.pagada == false && x.abono == false))
                {
                    var nuevoMonto = monto - item.Total;
                    if (nuevoMonto > 0)
                    {
                        using (SqlCommand cmd2 = new SqlCommand("pagoporcliente", M.conexion))
                        {
                            M.Desconectar();
                            cmd2.CommandType = CommandType.StoredProcedure;

                            //Tabla de pago
                            cmd2.Parameters.Add("@IdVenta", SqlDbType.Int).Value = item.IdVenta;
                            cmd2.Parameters.Add("@id_caja", SqlDbType.Int).Value = Program.idcaja;
                            cmd2.Parameters.Add("@monto", SqlDbType.Decimal).Value = item.Total;
                            cmd2.Parameters.Add("@ingresos", SqlDbType.Decimal).Value = item.Total;
                            cmd2.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Today;

                            try
                            {
                                M.Conectar();
                                cmd2.ExecuteNonQuery();
                                M.Desconectar();

                                monto = nuevoMonto;
                                var newItem = item;
                                newItem.pagada = true;
                                idsAmountToPay.Where(x => x.IdVenta == item.IdVenta)
                                              .ToList()
                                              .ForEach(y => y = newItem);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al guardar el pago. \n" + ex);
                            }
                        }
                    }
                    else
                    {
                        if (monto > 0 && monto < item.Total)
                        {
                            using (SqlCommand cmd2 = new SqlCommand("pagoporcliente", M.conexion))
                            {
                                M.Desconectar();
                                cmd2.CommandType = CommandType.StoredProcedure;

                                //Tabla de pago
                                cmd2.Parameters.Add("@IdVenta", SqlDbType.Int).Value = item.IdVenta;
                                cmd2.Parameters.Add("@id_caja", SqlDbType.Int).Value = Program.idcaja;
                                cmd2.Parameters.Add("@monto", SqlDbType.Decimal).Value = monto;
                                cmd2.Parameters.Add("@ingresos", SqlDbType.Decimal).Value = monto;
                                cmd2.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Today;

                                try
                                {
                                    M.Conectar();
                                    cmd2.ExecuteNonQuery();
                                    M.Desconectar();

                                    var newItem = item;
                                    newItem.abono = true;
                                    newItem.montoPagado = monto;
                                    idsAmountToPay.Where(x => x.IdVenta == item.IdVenta)
                                                  .ToList()
                                                  .ForEach(y => y = newItem);
                                    monto = 0;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Error al guardar el pago. \n" + ex);
                                }
                            }
                        }
                    }
                }

                tickEstilo(idsAmountToPay.FirstOrDefault().cliente, decimal.Parse(txtmonto.Text), decimal.Parse(txtRestante.Text));
            }
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txtmonto_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTotal.Text) && !string.IsNullOrWhiteSpace(txtmonto.Text))
            {
                var total = Convert.ToDecimal(txtTotal.Text);
                var monto = Convert.ToDecimal(txtmonto.Text);
                var rest = (total - monto);

                txtRestante.Text = rest.ToString();
            }
        }

        public void tickEstilo(string nombre, decimal monto, decimal restante)
        {
            CrearTiket ticket = new CrearTiket();
            ticket.TextoCentro(lblLogo.Text);
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda(lblDir.Text);
            ticket.TextoIzquierda("Tel : " + lblTel1.Text + "/" + lblTel2.Text);
            ticket.TextoIzquierda("RNC : " + lblrnc.Text);
            ticket.TextoIzquierda("Atendido Por : " + lblUsuario.Text);
            ticket.TextoIzquierda("Cliente : " + nombre);
            ticket.TextoIzquierda("Fecha Abono : " + DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year);

            foreach (var fila in idsAmountToPay)
            {
                ticket.lineasGuio();
                ticket.TextoIzquierda("Tipo de Comprobante : " + fila.tipoNCF);
                ticket.TextoIzquierda("Numero de Comprobante : " + fila.ncf);
                ticket.TextoIzquierda("ID VENTA : " + fila.IdVenta);
                ticket.TextoIzquierda("Monto Total de Factura : " + fila.Total);
                
                if (fila.pagada) {
                    ticket.TextoIzquierda("Factura Pagada Completa");
                } else if (fila.abono) {
                    ticket.TextoIzquierda("Abono Realizado");
                    ticket.TextoIzquierda("Total Abonado : " + fila.montoPagado);
                }

                ticket.lineasGuio();
            }

            ticket.TextoIzquierda(" ");

            ticket.AgregarTotales("TOTAL PAGADO : ", monto);
            ticket.AgregarTotales("RESTANTE : ", restante);
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
            ticket.ImprimirTicket(Program.ImpresonaPeq);//NOMBRE DE LA IMPRESORA

            txtTotal.Text = restante.ToString();
            txtmonto.Text = 0.ToString();
            txtRestante.Text = 0.ToString();
        }

        private void frmMontoAPagar_Load(object sender, EventArgs e)
        {
            if (idsAmountToPay != null && idsAmountToPay.Any())
            {
                txtTotal.Text = idsAmountToPay.Sum(x => x.Total).ToString();
            }
        }
    }
}

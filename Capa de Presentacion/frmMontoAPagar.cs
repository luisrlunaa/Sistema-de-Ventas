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
            var idCaja = buscaridcaja();
            var monto = Program.GetTwoNumberAfterPointWithOutRound(txtmonto.Text);
            if (idsAmountToPay != null && idsAmountToPay.Any())
            {
                foreach (var item in idsAmountToPay.Where(x => !x.pagada && !x.abono).OrderBy(y => y.IdVenta).ToList())
                {
                    var nuevoMonto = monto - item.Total;
                    if (nuevoMonto >= 0)
                    {
                        var waspay = paysales(item.IdVenta, item.Total, 0, idCaja);
                        if (waspay)
                        {
                            monto = nuevoMonto;
                            var newItem = item;
                            newItem.pagada = true;
                            idsAmountToPay.Where(x => x.IdVenta == item.IdVenta)
                                          .ToList()
                                          .ForEach(y => y = newItem);
                        }
                    }
                    else
                    {
                        if (monto > 0 && monto < item.Total)
                        {
                            var restante = item.Total - monto;
                            var wasabonado = paysales(item.IdVenta, monto, restante, idCaja);
                            if (wasabonado)
                            {
                                var newItem = item;
                                newItem.abono = true;
                                newItem.montoPagado = monto;
                                idsAmountToPay.Where(x => x.IdVenta == item.IdVenta)
                                              .ToList()
                                              .ForEach(y => y = newItem);
                                monto = 0;
                            }
                        }
                    }
                }

                tickEstilo(idsAmountToPay?.FirstOrDefault()?.cliente, Program.GetTwoNumberAfterPointWithOutRound(txtmonto.Text), Program.GetTwoNumberAfterPointWithOutRound(txtRestante.Text));
            }
            else
            {
                MessageBox.Show("Error al cargar el listado de facturas, favor volver a intentar");
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
                var total = Program.GetTwoNumberAfterPointWithOutRound(txtTotal.Text);
                var monto = Program.GetTwoNumberAfterPointWithOutRound(txtmonto.Text);
                var rest = (total - monto);

                txtRestante.Text = rest.ToString();
            }
        }

        public void tickEstilo(string nombre, decimal monto, decimal restante)
        {
            CrearTiket ticket = new CrearTiket();
            //cabecera del ticket.
            ticket.TextoCentro(lblLogo.Text);
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda(lblDir.Text);
            ticket.TextoIzquierda("Tel : " + lblTel1.Text + "/" + lblTel2.Text);
            ticket.TextoIzquierda("RNC : " + lblrnc.Text);
            ticket.TextoIzquierda("Atendido Por : " + lblUsuario.Text);
            ticket.TextoIzquierda("Cliente : " + nombre);
            ticket.TextoIzquierda("Fecha Abono : " + DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year);

            foreach (var fila in idsAmountToPay.Where(x => x.pagada || x.abono))
            {
                ticket.TextoIzquierda("");
                ticket.TextoIzquierda("Tipo de Comprobante : " + fila.tipoNCF);
                ticket.TextoIzquierda("Numero de Comprobante : " + fila.ncf);
                ticket.TextoIzquierda("ID VENTA : " + fila.IdVenta);
                ticket.TextoIzquierda("Monto Total de Factura : " + fila.Total);

                if (fila.pagada)
                {
                    ticket.TextoIzquierda("Factura Pagada Completa");
                }
                else if (fila.abono)
                {
                    ticket.TextoIzquierda("Abono Realizado");
                    ticket.TextoIzquierda("Total Abonado : " + fila.montoPagado);
                }

                ticket.TextoIzquierda("");
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

        private bool paysales(int idVenta, decimal pago, decimal restante, int idCaja)
        {
            M.Desconectar();
            using (SqlCommand cmd = new SqlCommand("AbonaraVenta", M.conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                //tabla Ventas
                cmd.Parameters.Add("@IdVenta", SqlDbType.Int).Value = idVenta;
                cmd.Parameters.Add("@Restante", SqlDbType.Decimal).Value = restante;

                try
                {
                    M.Conectar();
                    cmd.ExecuteNonQuery();
                    M.Desconectar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al realizar el abono. \n" + ex);
                    return false;
                }
            }

            using (SqlCommand cmd2 = new SqlCommand("Actualizarpagos_re", M.conexion))
            {
                cmd2.CommandType = CommandType.StoredProcedure;

                //Tabla de pago
                cmd2.Parameters.Add("@IdVenta", SqlDbType.Int).Value = idVenta;
                cmd2.Parameters.Add("@id_pago", SqlDbType.Int).Value = idpago();
                cmd2.Parameters.Add("@id_caja", SqlDbType.Int).Value = idCaja;
                cmd2.Parameters.Add("@id_cajaAnterior", SqlDbType.Int).Value = idCaja;
                cmd2.Parameters.Add("@monto", SqlDbType.Decimal).Value = pago;
                cmd2.Parameters.Add("@ingresos", SqlDbType.Decimal).Value = pago;
                cmd2.Parameters.Add("@egresos", SqlDbType.Decimal).Value = 0;
                cmd2.Parameters.Add("@fecha", SqlDbType.DateTime).Value = DateTime.Today;
                cmd2.Parameters.Add("@deuda", SqlDbType.Decimal).Value = restante;

                try
                {
                    M.Conectar();
                    cmd2.ExecuteNonQuery();
                    M.Desconectar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Factura actualizada, pero hubo un Error al actualizar el pago en la caja. \n" + ex);
                    return false;
                }

                return true;
            }
        }

        public int buscaridcaja()
        {
            int id = 0;
            M.Desconectar();
            M.Conectar();
            string sql = "select id_caja from Caja where fecha = convert(datetime,CONVERT(varchar(10), @fecha, 103),103)";
            SqlCommand cmd = new SqlCommand(sql, M.conexion);
            cmd.Parameters.AddWithValue("@fecha", DateTime.Today);

            SqlDataReader reade = cmd.ExecuteReader();
            if (reade.Read())
            {
                id = Convert.ToInt32(reade["id_caja"]);
            }

            M.Desconectar();
            return id;
        }

        public int idpago()
        {
            int id = 0;
            M.Desconectar();
            M.Conectar();
            string sql = "select (MAX(id_pago)+1) AS idpago from Pagos";
            SqlCommand cmd = new SqlCommand(sql, M.conexion);
            SqlDataReader reade = cmd.ExecuteReader();
            if (reade.Read())
            {
                id = Convert.ToInt32(reade["idpago"]);
            }

            M.Desconectar();
            return id;
        }
    }
}

using CapaEnlaceDatos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class frmMovimientoCaja : DevComponents.DotNetBar.Metro.MetroForm
    {
        public frmMovimientoCaja()
        {
            InitializeComponent();
        }

        clsManejador M = new clsManejador();
        private void frmMovimientoCaja_Load(object sender, EventArgs e)
        {
            llenar_data();

            txtmonto_inicial.Text = Program.MontoInicial.ToString();
            txtBuscarCaja.Text = Program.idcaja.ToString();
        }

        public void llenar_datagastos()
        {
            M.Desconectar();
            decimal gastos = 0;
            decimal pagos = 0;
            decimal total = 0;
            int idcajaActual = 0;

            if (txtBuscarCaja.Text != "")
            {
                idcajaActual = Convert.ToInt32(txtBuscarCaja.Text);
            }

            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            comando.Connection = M.conexion;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "select G.id,G.monto,G.descripcion,G.fecha from Gastos G inner join Caja C on G.fecha= C.fecha where C.id_caja=" + idcajaActual;
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            M.Conectar();
            //limpiamos los renglones de la datagridview
            dataGridView2.Rows.Clear();
            //a la variable DataReader asignamos  el la variable de tipo SqlCommand
            dr = comando.ExecuteReader();
            while (dr.Read())
            {
                //variable de tipo entero para ir enumerando los la filas del datagridview
                int renglon = dataGridView2.Rows.Add();
                // especificamos en que fila se mostrará cada registro
                // nombredeldatagrid.filas[numerodefila].celdas[nombrdelacelda].valor=\
                dataGridView2.Rows[renglon].Cells["id"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id")));
                dataGridView2.Rows[renglon].Cells["descripcion"].Value = dr.GetString(dr.GetOrdinal("descripcion"));
                dataGridView2.Rows[renglon].Cells["montogasto"].Value = dr.GetDecimal(dr.GetOrdinal("monto")).ToString();

                gastos += Program.GetTwoNumberAfterPointWithOutRound(dr.GetDecimal(dr.GetOrdinal("monto")).ToString());
            }

            lbldeu.Text = gastos.ToString();

            if (lbling.Text != "...")
            {
                pagos = Program.GetTwoNumberAfterPointWithOutRound(lbling.Text);
            }

            if (lbldeu.Text != "...")
            {
                gastos = Program.GetTwoNumberAfterPointWithOutRound(lbldeu.Text);
            }

            total = pagos - gastos;

            lbltotal.Text = total.ToString();

            M.Desconectar();
        }

        public void llenar_data()
        {
            M.Desconectar();
            decimal devuelta = 0, pagos = 0;
            int idcajaActual = 0;

            if (txtBuscarCaja.Text != "")
            {
                idcajaActual = Convert.ToInt32(txtBuscarCaja.Text);
            }
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            comando.Connection = M.conexion;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "select id_caja,id_pago,monto,ingresos,egresos from Pagos WHERE dbo.Pagos.id_caja =" + idcajaActual;
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            M.Conectar();
            //limpiamos los renglones de la datagridview
            dataGridView1.Rows.Clear();
            //a la variable DataReader asignamos  el la variable de tipo SqlCommand
            dr = comando.ExecuteReader();
            while (dr.Read())
            {
                //variable de tipo entero para ir enumerando los la filas del datagridview
                int renglon = dataGridView1.Rows.Add();
                // especificamos en que fila se mostrará cada registro
                // nombredeldatagrid.filas[numerodefila].celdas[nombrdelacelda].valor=\
                //pagos
                dataGridView1.Rows[renglon].Cells["id_caja"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id_caja")));
                dataGridView1.Rows[renglon].Cells["id_pago"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id_pago")));
                dataGridView1.Rows[renglon].Cells["monto"].Value = dr.GetDecimal(dr.GetOrdinal("monto")).ToString();
                dataGridView1.Rows[renglon].Cells["ingresos"].Value = dr.GetDecimal(dr.GetOrdinal("ingresos")).ToString();
                dataGridView1.Rows[renglon].Cells["egresos"].Value = dr.GetDecimal(dr.GetOrdinal("egresos")).ToString();

                devuelta += dr.GetDecimal(dr.GetOrdinal("egresos"));
                pagos += dr.GetDecimal(dr.GetOrdinal("ingresos"));
            }

            lblegr.Text = Program.GetTwoNumberAfterPointWithOutRound(devuelta.ToString()).ToString();
            lbling.Text = Program.GetTwoNumberAfterPointWithOutRound(pagos.ToString()).ToString();

            llenar_datagastos();
        }

        private void To_pdf()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:";
            saveFileDialog1.Title = "Guardar Reporte";
            saveFileDialog1.DefaultExt = "pdf";
            saveFileDialog1.Filter = "pdf Files (*.pdf)|*.pdf| All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = "Reporte de Movimiento de Caja" + DateTime.Now.ToString();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileName.Trim() != "")
                {
                    Document doc = new Document(PageSize.A4, 10f, 10f, 5f, 0f);
                    FileStream file = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    PdfWriter.GetInstance(doc, file);
                    doc.Open();
                    string remito = lblLogo.Text;
                    string ubicado = lblDir.Text;
                    string envio = "Fecha : " + DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year;

                    Chunk chunk = new Chunk(remito, FontFactory.GetFont("ARIAL", 18, iTextSharp.text.Font.BOLD, color: BaseColor.BLUE));
                    doc.Add(new Paragraph("                                                                                                                                                                                                                                                     " + envio, FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.ITALIC)));
                    iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance("LogoCepeda.png");
                    image1.ScaleAbsoluteWidth(140);
                    image1.ScaleAbsoluteHeight(70);
                    doc.Add(image1);

                    doc.Add(new Paragraph(chunk));
                    doc.Add(new Paragraph(ubicado, FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("Reporte de Movimientos de Caja                      "));
                    doc.Add(new Paragraph("                       "));
                    GenerarDocumento(doc);
                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("Reporte de Gastos del Dia                           "));
                    doc.Add(new Paragraph("                       "));
                    GenerarDocumentogastos(doc);
                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("Totales de Pagos :" + lbling.Text));
                    doc.Add(new Paragraph("Totales de Gastos :" + lbldeu.Text));
                    doc.Add(new Paragraph("Totales Final :" + lbltotal.Text));
                    doc.AddCreationDate();
                    doc.Close();
                    Process.Start(saveFileDialog1.FileName);//Esta parte se puede omitir, si solo se desea guardar el archivo, y que este no se ejecute al instante
                }
            }
            else
            {
                MessageBox.Show("No guardo el Archivo");
            }
        }

        public void GenerarDocumento(Document document)
        {
            Pdf.GenerarDocumento(document, dataGridView1);
        }
        public void GenerarDocumentogastos(Document document)
        {
            Pdf.GenerarDocumento(document, dataGridView2);
        }

        private void btnimprimir_Click(object sender, EventArgs e)
        {
            To_pdf();
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            Program.idgastos = dataGridView2.CurrentRow.Cells["id"].Value != null ? Convert.ToInt32(dataGridView2.CurrentRow.Cells["id"].Value.ToString()) : 0;
            txtdescripciondegasto.Text = dataGridView2.CurrentRow.Cells["descripcion"].Value.ToString();
            txtmontogasto.Text = dataGridView2.CurrentRow.Cells["montogasto"].Value.ToString();
        }

        private void txtBuscarCaja_TextChanged(object sender, EventArgs e)
        {
            llenar_data();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Program.abiertosecundarias = false;
            Program.abierto = false;
            limpiar();
            this.Close();
        }

        public void limpiar()
        {
            Program.idgastos = 0;
            txtdescripciondegasto.Text = "";
            txtmontogasto.Text = "";
            dataGridView2.Rows.Clear();
        }

        private void agregargasto_Click_1(object sender, EventArgs e)
        {
            M.Desconectar();
            if (agregargasto.Text == "Eliminar")
            {
                if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Eliminar este Gasto.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    using (SqlCommand cmd = new SqlCommand("EliminarGasto", M.conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        //tabla gastos
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Program.idgastos;

                        M.Conectar();
                        cmd.ExecuteNonQuery();
                        M.Desconectar();

                        limpiar();
                        llenar_datagastos();
                    }
                }
                else
                {
                    llenar_datagastos();
                    agregargasto.Text = "Agregar";
                    agregargasto.BackColor = Color.CornflowerBlue;
                    agregargasto.ForeColor = Color.Black;
                }
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand("RegistrarGasto", M.conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //tabla gastos
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Program.idgastos;
                    cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar).Value = txtdescripciondegasto.Text;
                    cmd.Parameters.Add("@monto", SqlDbType.Decimal).Value = Program.GetTwoNumberAfterPointWithOutRound(txtmontogasto.Text);
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = DateTime.Today;

                    M.Conectar();
                    cmd.ExecuteNonQuery();
                    M.Desconectar();
                    limpiar();
                    llenar_datagastos();
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Program.idgastos = Convert.ToInt32(dataGridView2.CurrentRow.Cells["id"].Value.ToString());
            if (Program.idgastos > 0)
            {
                agregargasto.Text = "Eliminar";
                agregargasto.BackColor = Color.Red;
                agregargasto.ForeColor = Color.White;
            }
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void frmMovimientoCaja_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}

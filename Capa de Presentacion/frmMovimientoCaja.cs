using CapaLogicaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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

        clsCx Cx = new clsCx();
        private void frmMovimientoCaja_Load(object sender, EventArgs e)
        {
            llenarid();
            llenar_data();
        }

        public void llenarid()
        {
            string cadSql = "select top(1) id_caja,monto_inicial from Caja order by id_caja desc";

            SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);
            Cx.conexion.Open();

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
            {
                txtmonto_inicial.Text = leer["monto_inicial"].ToString();
                txtBuscarCaja.Text = leer["id_caja"].ToString();
            }
            Cx.conexion.Close();
        }

        public void llenar_datagastos()
        {
            decimal gastos = 0;
            decimal pagos = 0;
            decimal total = 0;
            int idcajaActual = 0;
            if (txtBuscarCaja.Text != "")
            {
                idcajaActual = Convert.ToInt32(txtBuscarCaja.Text);
            }
            //declaramos la cadena  de conexion
            string cadenaconexion = Cx.conet;
            //variable de tipo Sqlconnection
            SqlConnection con = new SqlConnection();
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            con.ConnectionString = cadenaconexion;
            comando.Connection = con;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "select G.id,G.monto,G.descripcion,G.fecha from Gastos G inner join Caja C on G.fecha= C.fecha where C.id_caja="+ idcajaActual;
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            con.Open();
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

                gastos += Math.Round(Convert.ToDecimal(dr.GetDecimal(dr.GetOrdinal("monto"))), 2);
            }

            lbldeu.Text = gastos.ToString();

            if (lbling.Text != "...")
            {
                pagos = Convert.ToDecimal(lbling.Text);
            }

            if (lbldeu.Text != "...")
            {
                gastos = Convert.ToDecimal(lbldeu.Text);
            }

            total = pagos - gastos;

            lbltotal.Text = total.ToString();

            con.Close();
        }
        public void llenar_data()
        {
            decimal devuelta = 0, pagos = 0;
            int idcajaActual = 0;
            if (txtBuscarCaja.Text != "")
            {
                idcajaActual = Convert.ToInt32(txtBuscarCaja.Text);
            }
            //declaramos la cadena  de conexion
            string cadenaconexion = Cx.conet;
            //variable de tipo Sqlconnection
            SqlConnection con = new SqlConnection();
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            con.ConnectionString = cadenaconexion;
            comando.Connection = con;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "select id_caja,id_pago,monto,ingresos,egresos from Pagos WHERE dbo.Pagos.id_caja =" + idcajaActual;
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            con.Open();
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

                devuelta += Math.Round(dr.GetDecimal(dr.GetOrdinal("egresos")), 2);
                pagos += Math.Round(dr.GetDecimal(dr.GetOrdinal("ingresos")), 2);
            }

            lblegr.Text = devuelta.ToString();
            lbling.Text = pagos.ToString();

            con.Close();
            llenar_datagastos();
        }

        private void To_pdf()
        {
            Document doc = new Document(PageSize.LETTER, 10f, 10f, 10f, 0f);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            Image image1 = Image.GetInstance("Logo.png");
            image1.ScaleAbsoluteWidth(140);
            image1.ScaleAbsoluteHeight(70);
            saveFileDialog1.InitialDirectory = @"C:";
            saveFileDialog1.Title = "Guardar Reporte";
            saveFileDialog1.DefaultExt = "pdf";
            saveFileDialog1.Filter = "pdf Files (*.pdf)|*.pdf| All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            string filename = "Reporte de Movimiento de Caja" + DateTime.Now.ToString();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = saveFileDialog1.FileName;
                if (filename.Trim() != "")
                {
                    FileStream file = new FileStream(filename,
                    FileMode.OpenOrCreate,
                    FileAccess.ReadWrite,
                    FileShare.ReadWrite);
                    PdfWriter.GetInstance(doc, file);
                    doc.Open();
                    string remito = lblLogo.Text;
                    string ubicado = lblDir.Text;
                    string envio = "Fecha : " + DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year;

                    Chunk chunk = new Chunk(remito, FontFactory.GetFont("ARIAL", 16, iTextSharp.text.Font.BOLD, color: BaseColor.BLUE));
                    var fecha = new Paragraph(envio, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.ITALIC));
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    image1.Alignment = Image.TEXTWRAP | Image.ALIGN_CENTER;
                    doc.Add(image1);
                    var chuckalign = new Paragraph(chunk);
                    chuckalign.Alignment = Element.ALIGN_CENTER;
                    doc.Add(chuckalign);
                    var ubicacionalign = new Paragraph(ubicado, FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.NORMAL));
                    ubicacionalign.Alignment = Element.ALIGN_CENTER;
                    doc.Add(ubicacionalign);
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
                    Process.Start(filename);//Esta parte se puede omitir, si solo se desea guardar el archivo, y que este no se ejecute al instante
                }
            }
            else
            {
                MessageBox.Show("No guardo el Archivo");
            }
        }
        public void GenerarDocumento(Document document)
        {
            int i, j;
            PdfPTable datatable = new PdfPTable(dataGridView1.ColumnCount);
            float[] headerwidths = GetTamañoColumnas(dataGridView1);
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 100;
            datatable.DefaultCell.BorderWidth = 1;
            datatable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            for (i = 0; i < dataGridView1.ColumnCount; i++)
            {
                datatable.AddCell(new Phrase(dataGridView1.Columns[i].HeaderText, FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD)));
            }
            datatable.HeaderRows = 1;
            datatable.DefaultCell.BorderWidth = 1;
            for (i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1[j, i].Value != null)
                    {
                        datatable.AddCell(new Phrase(dataGridView1[j, i].Value.ToString(), FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));//En esta parte, se esta agregando un renglon por cada registro en el datagrid
                    }
                }
                datatable.CompleteRow();
            }
            document.Add(datatable);
        }
        public void GenerarDocumentogastos(Document document)
        {
            int i, j;
            PdfPTable datatable = new PdfPTable(dataGridView2.ColumnCount);
            float[] headerwidths = GetTamañoColumnas(dataGridView2);
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 100;
            datatable.DefaultCell.BorderWidth = 1;
            datatable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            for (i = 0; i < dataGridView2.ColumnCount; i++)
            {
                datatable.AddCell(new Phrase(dataGridView2.Columns[i].HeaderText, FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD)));
            }
            datatable.HeaderRows = 1;
            datatable.DefaultCell.BorderWidth = 1;
            for (i = 0; i < dataGridView2.Rows.Count; i++)
            {
                for (j = 0; j < dataGridView2.Columns.Count; j++)
                {
                    if (dataGridView2[j, i].Value != null)
                    {
                        datatable.AddCell(new Phrase(dataGridView2[j, i].Value.ToString(), FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));//En esta parte, se esta agregando un renglon por cada registro en el datagrid
                    }
                }
                datatable.CompleteRow();
            }
            document.Add(datatable);
        }

        public float[] GetTamañoColumnasgastos(DataGridView dg)
        {
            float[] values = new float[dg.ColumnCount];
            for (int i = 0; i < dg.ColumnCount; i++)
            {
                values[i] = (float)dg.Columns[i].Width;
            }
            return values;
        }

        public float[] GetTamañoColumnas(DataGridView dg)
        {
            float[] values = new float[dg.ColumnCount];
            for (int i = 0; i < dg.ColumnCount; i++)
            {
                values[i] = (float)dg.Columns[i].Width;
            }
            return values;
        }

        private void btnimprimir_Click(object sender, EventArgs e)
        {
            To_pdf();
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            Program.idgastos = Convert.ToInt32(dataGridView2.CurrentRow.Cells["id"].Value.ToString());
            txtdescripciondegasto.Text = dataGridView2.CurrentRow.Cells["descripcion"].Value.ToString();
            txtmontogasto.Text = dataGridView2.CurrentRow.Cells["montogasto"].Value.ToString();
        }

        private void txtBuscarCaja_TextChanged(object sender, EventArgs e)
        {
            llenar_data();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Program.abiertosecundario = false;
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

        private void agregargasto_Click(object sender, EventArgs e)
        {
            if (agregargasto.Text == "Eliminar")
            {
                if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Eliminar este Gasto.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection(Cx.conet))
                    {
                        using (SqlCommand cmd = new SqlCommand("EliminarGasto", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            //tabla gastos
                            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Program.idgastos;

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            limpiar();
                            llenar_datagastos();
                        }
                    }
                }
                else
                {
                    llenar_datagastos();
                    agregargasto.Text = "Agregar";
                    agregargasto.BackColor = System.Drawing.Color.CornflowerBlue;
                    agregargasto.ForeColor = System.Drawing.Color.Black;
                }
            }
            else
            {
                using (SqlConnection con = new SqlConnection(Cx.conet))
                {
                    using (SqlCommand cmd = new SqlCommand("RegistrarGasto", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        //tabla gastos
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Program.idgastos;
                        cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar).Value = txtdescripciondegasto.Text;
                        cmd.Parameters.Add("@monto", SqlDbType.Decimal).Value = Convert.ToDecimal(txtmontogasto.Text);
                        cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = DateTime.Today;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        limpiar();
                        llenar_datagastos();
                    }
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Program.idgastos = Convert.ToInt32(dataGridView2.CurrentRow.Cells["id"].Value.ToString());
            if (Program.idgastos > 0)
            {
                agregargasto.Text = "Eliminar";
                agregargasto.BackColor = System.Drawing.Color.Red;
                agregargasto.ForeColor = System.Drawing.Color.White;
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

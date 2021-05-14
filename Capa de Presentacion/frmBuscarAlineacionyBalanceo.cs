using CapaLogicaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class FrmBuscarAlineacionyBalanceo : DevComponents.DotNetBar.Metro.MetroForm
    {
        public FrmBuscarAlineacionyBalanceo()
        {
            InitializeComponent();
        }

        clsCx Cx = new clsCx();
        public void cargardata()
        {
            double total = 0;
            limpiar();
            //declaramos la cadena  de conexion
            string cadenaconexion = Cx.conet;
            //variable de tipo Sqlconnection
            SqlConnection conexion = new SqlConnection();
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            conexion.ConnectionString = cadenaconexion;
            comando.Connection = conexion;
            if (textBox1.Text != "")
            {
                //declaramos el comando para realizar la busqueda
                comando.CommandText = "select * from AlineamientoYBalanceo where vehiculo like '%" + textBox1.Text.ToUpper() + "%' and fecha BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) " +
                "AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) ORDER BY fecha";
                comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
                comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
            }
            else
            {
                //declaramos el comando para realizar la busqueda
                comando.CommandText = "select * from AlineamientoYBalanceo";
            }
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            conexion.Open();
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
                dataGridView1.Rows[renglon].Cells["id"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id")));
                dataGridView1.Rows[renglon].Cells["tipoDeTrabajo"].Value = dr.GetString(dr.GetOrdinal("tipoDeTrabajo"));
                dataGridView1.Rows[renglon].Cells["vehiculo"].Value = dr.GetString(dr.GetOrdinal("vehiculo"));
                dataGridView1.Rows[renglon].Cells["AroGoma"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("AroGoma")));
                dataGridView1.Rows[renglon].Cells["precio"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("precio")));
                dataGridView1.Rows[renglon].Cells["nota"].Value = dr.GetString(dr.GetOrdinal("nota"));
                dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("fecha"));

                total += Convert.ToDouble(dataGridView1.Rows[renglon].Cells["precio"].Value);
                txttotalG.Text = Convert.ToString(total);
            }
            conexion.Close();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            var marca = dataGridView1.CurrentRow.Cells["vehiculo"].Value.ToString();
            string cadena = marca;
            char delimitador = ' ';
            string[] valores = cadena.Split(delimitador);

            Program.descripcion = dataGridView1.CurrentRow.Cells["tipoDeTrabajo"].Value.ToString();
            Program.marca = valores[0].Trim();
            Program.modelo = valores[1].Trim();
            Program.Aros = dataGridView1.CurrentRow.Cells["AroGoma"].Value.ToString();
            Program.total = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["precio"].Value.ToString());
            Program.nota = dataGridView1.CurrentRow.Cells["nota"].Value.ToString();
            Program.Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());

            Program.abiertosecundarias = false;
            Program.abierto = false;
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Program.abiertosecundarias = false;
            Program.abierto = false;
            this.Close();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            cargardata();
        }
        public void cargar_combo_Tipo(ComboBox tipo)
        {
            SqlCommand cm = new SqlCommand("CARGARcomboTipotrabajo", Cx.conexion);
            cm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            tipo.DisplayMember = "descripcion";
            tipo.ValueMember = "id";
            tipo.DataSource = dt;
        }
        public void limpiar()
        {
            dataGridView1.Rows.Clear();
        }

        public void buscarporfecha()
        {
            double total = 0;
            limpiar();
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
            comando.CommandText = "select * from AlineamientoYBalanceo where fecha BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) " +
                "AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) ORDER BY fecha";
            comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
            comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
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
                dataGridView1.Rows[renglon].Cells["id"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id")));
                dataGridView1.Rows[renglon].Cells["tipoDeTrabajo"].Value = dr.GetString(dr.GetOrdinal("tipoDeTrabajo"));
                dataGridView1.Rows[renglon].Cells["vehiculo"].Value = dr.GetString(dr.GetOrdinal("vehiculo"));
                dataGridView1.Rows[renglon].Cells["AroGoma"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("AroGoma")));
                dataGridView1.Rows[renglon].Cells["precio"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("precio")));
                dataGridView1.Rows[renglon].Cells["nota"].Value = dr.GetString(dr.GetOrdinal("nota"));
                dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("fecha"));

                total += Convert.ToDouble(dataGridView1.Rows[renglon].Cells["precio"].Value);
                txttotalG.Text = Convert.ToString(total);
            }
            con.Close();
        }
        private void frmBuscarAlineacionyBalanceo_Load(object sender, EventArgs e)
        {
            cargardata();
            cargar_combo_Tipo(cbtipo);
            cbtipo.SelectedIndex = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            To_pdf();
        }
        private void To_pdf()
        {
            Document doc = new Document(PageSize.LETTER, 10f, 10f, 10f, 0f);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance("LogoCepeda.png");
            image1.ScaleAbsoluteWidth(100);
            image1.ScaleAbsoluteHeight(50);
            saveFileDialog1.InitialDirectory = @"C:";
            saveFileDialog1.Title = "Guardar Reporte";
            saveFileDialog1.DefaultExt = "pdf";
            saveFileDialog1.Filter = "pdf Files (*.pdf)|*.pdf| All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            string filename = "Reporte" + DateTime.Now.ToString();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = saveFileDialog1.FileName;
            }

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
                doc.Add(new Paragraph("                                                                                                                                                                                                                                                     " + envio, FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.ITALIC)));
                doc.Add(image1);
                doc.Add(new Paragraph(chunk));
                doc.Add(new Paragraph(ubicado, FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.NORMAL)));
                doc.Add(new Paragraph("                       "));
                doc.Add(new Paragraph("Reporte de Listado de Alineaciones y Balanceos                       "));
                doc.Add(new Paragraph("                       "));
                GenerarDocumento(doc);
                doc.AddCreationDate();
                doc.Add(new Paragraph("                       "));
                doc.Add(new Paragraph("Total de Ventas      : " + txttotalG.Text));
                doc.Add(new Paragraph("                       "));
                doc.Add(new Paragraph("____________________________________"));
                doc.Add(new Paragraph("                         Firma              "));
                doc.Close();
                Process.Start(filename);//Esta parte se puede omitir, si solo se desea guardar el archivo, y que este no se ejecute al instante
            }
        }
        public void GenerarDocumento(Document document)
        {
            int i, j;
            PdfPTable datatable = new PdfPTable(dataGridView1.ColumnCount);
            datatable.DefaultCell.Padding = 3;
            float[] headerwidths = GetTamañoColumnas(dataGridView1);
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 100;
            datatable.DefaultCell.BorderWidth = 1;
            datatable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            for (i = 0; i < dataGridView1.ColumnCount; i++)
            {
                datatable.AddCell(dataGridView1.Columns[i].HeaderText);
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
        public float[] GetTamañoColumnas(DataGridView dg)
        {
            float[] values = new float[dg.ColumnCount];
            for (int i = 0; i < dg.ColumnCount; i++)
            {
                values[i] = (float)dg.Columns[i].Width;
            }
            return values;
        }

        private void agregargasto_Click(object sender, EventArgs e)
        {
            buscarporfecha();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double total = 0;
            //declaramos la cadena  de conexion
            string cadenaconexion = Cx.conet;
            //variable de tipo Sqlconnection
            SqlConnection conexion = new SqlConnection();
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            conexion.ConnectionString = cadenaconexion;
            comando.Connection = conexion;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "select * from AlineamientoYBalanceo where tipoDeTrabajo like '%" + cbtipo.Text + "%' AND fecha BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) " +
                "AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) ORDER BY fecha";
            comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
            comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            conexion.Open();
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
                dataGridView1.Rows[renglon].Cells["id"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id")));
                dataGridView1.Rows[renglon].Cells["tipoDeTrabajo"].Value = dr.GetString(dr.GetOrdinal("tipoDeTrabajo"));
                dataGridView1.Rows[renglon].Cells["vehiculo"].Value = dr.GetString(dr.GetOrdinal("vehiculo"));
                dataGridView1.Rows[renglon].Cells["AroGoma"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("AroGoma")));
                dataGridView1.Rows[renglon].Cells["precio"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("precio")));
                dataGridView1.Rows[renglon].Cells["nota"].Value = dr.GetString(dr.GetOrdinal("nota"));
                dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("fecha"));

                total += Convert.ToDouble(dataGridView1.Rows[renglon].Cells["precio"].Value);
                txttotalG.Text = Convert.ToString(total);
            }
            conexion.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            cargardata();
        }
    }
}

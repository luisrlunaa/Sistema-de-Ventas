using CapaEnlaceDatos;
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
    public partial class frmlistadodecotizacion : Form
    {
        public frmlistadodecotizacion()
        {
            InitializeComponent();
        }

        clsManejador M = new clsManejador();
        private void button3_Click(object sender, EventArgs e)
        {
            Program.Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Eliminar esta Cotizacion?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                using (SqlCommand cmd = new SqlCommand("eliminarcotizacion", M.conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Program.Id;

                    M.conexion.Open();
                    cmd.ExecuteNonQuery();
                    M.conexion.Close();
                    Program.Id = 0;
                    button3.Enabled = false;
                }
            }
        }

        private void frmlistadodecotizacion_Load(object sender, EventArgs e)
        {
            llenar_data("");
            button3.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;
                button3.Enabled = Program.isAdminUser;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Program.abiertosecundario = false;
            Program.abierto = false;
            seleccion_data();
            this.Close();
        }

        bool isallowNumber = false;
        private void chkid_CheckedChanged(object sender, EventArgs e)
        {
            if (chkid.Checked && !chknombre.Checked)
            {
                txtBuscarid.Enabled = true;
                isallowNumber = true;
            }
            else
            {
                txtBuscarid.Enabled = false;
            }
        }

        private void chknombre_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkid.Checked && chknombre.Checked)
            {
                txtBuscarid.Enabled = true;
                isallowNumber = false;
            }
            else
            {
                txtBuscarid.Enabled = false;
            }
        }

        private void txtBuscarid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isallowNumber)
            {
                validar.solonumeros(e);
            }
            else
            {
                validar.sololetras(e);
            }
        }

        private void txtBuscarid_KeyUp(object sender, KeyEventArgs e)
        {
            if (chkid.Checked && chknombre.Checked == false)
            {
                if (txtBuscarid.Text.Length >= 1)
                {
                    llenar_data(txtBuscarid.Text);
                }
            }
            else if (chknombre.Checked && chkid.Checked == false)
            {
                if (txtBuscarid.Text.Length >= 4)
                {
                    llenar_data(txtBuscarid.Text);
                }
            }
            else
            {
                llenar_data("");
            }
        }

        public void llenar_data(string id)
        {
            M.Desconectar();
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            comando.Connection = M.conexion;

            if (chkid.Checked && chknombre.Checked == false && id != null)
            {
                comando.CommandText = "select IdCotizacion,IdCliente = COALESCE(IdCliente, '0'),FechaCotizacion,Total,IdEmpleado," +
                    "NombreCliente = COALESCE(NombreCliente, 'Sin Cliente') from Cotizacion WHERE " +
                    "IdCotizacion = " + id + " ORDER BY IdCotizacion";
            }
            else if (chknombre.Checked && chkid.Checked == false && id != null)
            {
                comando.CommandText = "select IdCotizacion,IdCliente = COALESCE(IdCliente, '0'),FechaCotizacion,Total,IdEmpleado," +
                    "NombreCliente = COALESCE(NombreCliente, 'Sin Cliente') from Cotizacion WHERE " +
                    "NombreCliente LIKE '%" + id + "%' ORDER BY IdCotizacion";
            }
            else
            {
                comando.CommandText = "select IdCotizacion,IdCliente = COALESCE(IdCliente, '0'),FechaCotizacion,Total,IdEmpleado," +
                    "NombreCliente = COALESCE(NombreCliente, 'Sin Cliente') from Cotizacion ORDER BY IdCotizacion";
            }

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

                dataGridView1.Rows[renglon].Cells["id"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdCotizacion")));
                dataGridView1.Rows[renglon].Cells["idEm"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdEmpleado")));
                dataGridView1.Rows[renglon].Cells["total"].Value = dr.GetDecimal(dr.GetOrdinal("Total")).ToString();
                dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("FechaCotizacion"));
                dataGridView1.Rows[renglon].Cells["nombrecliente"].Value = dr.GetString(dr.GetOrdinal("NombreCliente"));
            }

            M.Desconectar();
        }

        public void seleccion_data()
        {
            Program.Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
            llenaridCliente(Program.Id);
            if (Program.IdCliente > 0)
            {
                M.Desconectar();
                M.Conectar();
                string sql = "SELECT DNI,Apellidos,Nombres from Cliente where IdCliente = '" + Program.IdCliente + "'";
                SqlCommand comando = new SqlCommand(sql, M.conexion);
                SqlDataReader dr = comando.ExecuteReader();
                if (dr.Read())
                {
                    Program.DocumentoIdentidad = dr.GetString(dr.GetOrdinal("DNI"));
                    Program.NombreCliente = dr.GetString(dr.GetOrdinal("Nombres"));
                    Program.ApellidosCliente = dr.GetString(dr.GetOrdinal("Apellidos"));
                }
                M.Desconectar();
            }
            else
            {
                Program.datoscliente = dataGridView1.CurrentRow.Cells["nombrecliente"].Value.ToString();
                Program.DocumentoIdentidad = "Sin Documento de Identificacion";
            }

            Program.total = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["total"].Value.ToString());
            Program.fecha = dataGridView1.CurrentRow.Cells["fecha"].Value.ToString();
            Program.IdEmpleado = Convert.ToInt32(dataGridView1.CurrentRow.Cells["idEm"].Value.ToString());

            FrmRegistroVentas V = new FrmRegistroVentas();
            V.button3.Visible = true;
        }

        public void llenaridCliente(int idventa)
        {
            M.Desconectar();
            string cadSql = "select IdCliente =COALESCE(dbo.Cotizacion.IdCliente,0) from Cotizacion where idCotizacion=" + idventa;
            M.Conectar();
            SqlCommand comando = new SqlCommand(cadSql, M.conexion);

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
            {
                Program.IdCliente = Convert.ToInt32(leer["IdCliente"]);
            }
            M.Desconectar();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Program.abierto = false;
            Program.abiertosecundario = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            M.Desconectar();
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            comando.Connection = M.conexion;

            if (txtBuscarid.Text != "" && txtBuscarid.Text != null)
            {
                //declaramos el comando para realizar la busqueda
                comando.CommandText = "select IdCotizacion,IdCliente = COALESCE(IdCliente, '0'),FechaCotizacion,Total,IdEmpleado," +
                "NombreCliente = COALESCE(NombreCliente, 'Sin Cliente') from Cotizacion where NombreCliente  LIKE '%" + txtBuscarid.Text
                + "%' and FechaCotizacion BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) AND " +
                "convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) ORDER BY dbo.Cotizacion.IdCotizacion";

                comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
                comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
            }
            else
            {
                //declaramos el comando para realizar la busqueda
                comando.CommandText = "select IdCotizacion,IdCliente = COALESCE(IdCliente, '0'),FechaCotizacion,Total,IdEmpleado," +
                "NombreCliente = COALESCE(NombreCliente, 'Sin Cliente') from Cotizacion where FechaCotizacion BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) " +
                "AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) ORDER BY dbo.Cotizacion.IdCotizacion";

                comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
                comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
            }

            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            M.Conectar();
            //limpiamos los renglones de la datagridview
            dataGridView1.Rows.Clear();
            //a la variable DataReader asignamos  el la variable de tipo SqlCommand
            dr = comando.ExecuteReader();
            //el ciclo while se ejecutará mientras lea registros en la tabla
            while (dr.Read())
            {
                //variable de tipo entero para ir enumerando los la filas del datagridview
                int renglon = dataGridView1.Rows.Add();
                // especificamos en que fila se mostrará cada registro
                // nombredeldatagrid.filas[numerodefila].celdas[nombrdelacelda].valor=\

                dataGridView1.Rows[renglon].Cells["id"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdCotizacion")));
                dataGridView1.Rows[renglon].Cells["idEm"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdEmpleado")));
                dataGridView1.Rows[renglon].Cells["total"].Value = dr.GetDecimal(dr.GetOrdinal("Total")).ToString();
                dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("FechaCotizacion"));
                dataGridView1.Rows[renglon].Cells["nombrecliente"].Value = dr.GetString(dr.GetOrdinal("NombreCliente"));
            }
            M.Desconectar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            To_pdf();
        }

        private void To_pdf()
        {
            Document doc = new Document(PageSize.LETTER, 10f, 10f, 10f, 0f);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            Image image1 = Image.GetInstance("LogoCepeda.png");
            image1.ScaleAbsoluteWidth(140);
            image1.ScaleAbsoluteHeight(70);
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

                    doc.Add(new Paragraph("Reporte de General de Ventas Realizadas"));
                    doc.Add(new Paragraph("Desde la Fecha: " + (dtpfecha1.Value.Day + "/" + dtpfecha1.Value.Month + "/" + dtpfecha1.Value.Year).ToString() + ", "
                        + "Hasta la Fecha: " + (dtpfecha2.Value.Day + "/" + dtpfecha2.Value.Month + "/" + dtpfecha2.Value.Year).ToString(), FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("                       "));
                    GenerarDocumento(doc);
                    doc.AddCreationDate();
                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("____________________________________"));
                    doc.Add(new Paragraph("                         Firma              "));
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

        public float[] GetTamañoColumnas(DataGridView dg)
        {
            float[] values = new float[dg.ColumnCount];
            for (int i = 0; i < dg.ColumnCount; i++)
            {
                values[i] = (float)dg.Columns[i].Width;
            }
            return values;
        }
    }
}

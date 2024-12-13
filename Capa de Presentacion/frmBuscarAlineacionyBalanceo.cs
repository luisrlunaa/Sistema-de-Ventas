using CapaEnlaceDatos;
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
    public partial class FrmBuscarAlineacionyBalanceo : DevComponents.DotNetBar.Metro.MetroForm
    {
        public FrmBuscarAlineacionyBalanceo()
        {
            InitializeComponent();
        }

        clsManejador M = new clsManejador();
        public void cargardata()
        {
            M.Desconectar();
            double total = 0;
            limpiar();
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            comando.Connection = M.conexion;
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
                dataGridView1.Rows[renglon].Cells["id"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id")));
                dataGridView1.Rows[renglon].Cells["tipoDeTrabajo"].Value = dr.GetString(dr.GetOrdinal("tipoDeTrabajo"));
                dataGridView1.Rows[renglon].Cells["vehiculo"].Value = dr.GetString(dr.GetOrdinal("vehiculo"));
                dataGridView1.Rows[renglon].Cells["AroGoma"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("AroGoma")));
                dataGridView1.Rows[renglon].Cells["precio"].Value = dr.GetDecimal(dr.GetOrdinal("precio")).ToString("C2");
                dataGridView1.Rows[renglon].Cells["nota"].Value = dr.GetString(dr.GetOrdinal("nota"));
                dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("fecha"));

                total += Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("precio")));
                txttotalG.Text = total.ToString("C2");
            }
            M.Desconectar();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            var marca = dataGridView1.CurrentRow.Cells["vehiculo"].Value.ToString();
            string cadena = marca;
            char delimitador = ' ';
            string[] valores = cadena.Split(delimitador);

            Program.descripcion = dataGridView1.CurrentRow.Cells["tipoDeTrabajo"].Value.ToString();
            Program.marca = valores[0].CleanSpace();
            Program.modelo = valores[1].CleanSpace();
            Program.Aros = dataGridView1.CurrentRow.Cells["AroGoma"].Value.ToString();
            Program.total = Program.GetTwoNumberAfterPointWithOutRound(dataGridView1.CurrentRow.Cells["precio"].Value.ToString());
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
        public void limpiar()
        {
            dataGridView1.Rows.Clear();
        }

        public void buscarporfecha()
        {
            M.Desconectar();
            double total = 0;
            limpiar();
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            comando.Connection = M.conexion;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "select * from AlineamientoYBalanceo where fecha BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) " +
                "AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) ORDER BY fecha";
            comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
            comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
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
                dataGridView1.Rows[renglon].Cells["id"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id")));
                dataGridView1.Rows[renglon].Cells["tipoDeTrabajo"].Value = dr.GetString(dr.GetOrdinal("tipoDeTrabajo"));
                dataGridView1.Rows[renglon].Cells["vehiculo"].Value = dr.GetString(dr.GetOrdinal("vehiculo"));
                dataGridView1.Rows[renglon].Cells["AroGoma"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("AroGoma")));
                dataGridView1.Rows[renglon].Cells["precio"].Value = dr.GetDecimal(dr.GetOrdinal("precio")).ToString("C2");
                dataGridView1.Rows[renglon].Cells["nota"].Value = dr.GetString(dr.GetOrdinal("nota"));
                dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("fecha"));

                total += Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("precio")));
                txttotalG.Text = total.ToString("C2");
            }
            M.Desconectar();
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
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:";
            saveFileDialog1.Title = "Guardar Reporte";
            saveFileDialog1.DefaultExt = "pdf";
            saveFileDialog1.Filter = "pdf Files (*.pdf)|*.pdf| All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = "Reporte" + DateTime.Now.ToString();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog1.FileName.CleanSpace() != "")
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
                    Image image1 = Image.GetInstance("LogoCepeda.png");
                    image1.ScaleAbsoluteWidth(100);
                    image1.ScaleAbsoluteHeight(50);
                    doc.Add(image1);
                    doc.Add(new Paragraph(chunk));
                    doc.Add(new Paragraph(ubicado, FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL)));
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
                    Process.Start(saveFileDialog1.FileName);//Esta parte se puede omitir, si solo se desea guardar el archivo, y que este no se ejecute al instante
                }
            }
        }
        public void GenerarDocumento(Document document)
        {
            Pdf.GenerarDocumento(document, dataGridView1);
        }

        private void agregargasto_Click(object sender, EventArgs e)
        {
            buscarporfecha();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            M.Desconectar();
            double total = 0;
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            comando.Connection = M.conexion;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "select * from AlineamientoYBalanceo where tipoDeTrabajo like '%" + cbtipo.Text + "%' AND fecha BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) " +
                "AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) ORDER BY fecha";
            comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
            comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
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
                dataGridView1.Rows[renglon].Cells["id"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id")));
                dataGridView1.Rows[renglon].Cells["tipoDeTrabajo"].Value = dr.GetString(dr.GetOrdinal("tipoDeTrabajo"));
                dataGridView1.Rows[renglon].Cells["vehiculo"].Value = dr.GetString(dr.GetOrdinal("vehiculo"));
                dataGridView1.Rows[renglon].Cells["AroGoma"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("AroGoma")));
                dataGridView1.Rows[renglon].Cells["precio"].Value = dr.GetDecimal(dr.GetOrdinal("precio")).ToString("C2");
                dataGridView1.Rows[renglon].Cells["nota"].Value = dr.GetString(dr.GetOrdinal("nota"));
                dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("fecha"));

                total += Convert.ToDouble(dr.GetDecimal(dr.GetOrdinal("precio")));
                txttotalG.Text = total.ToString("C2");
            }
            M.Desconectar();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            cargardata();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void FrmBuscarAlineacionyBalanceo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}

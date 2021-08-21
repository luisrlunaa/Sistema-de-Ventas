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

        clsManejador Cx = new clsManejador();
        public void cargardata()
        {
            Cx.Desconectar();
            double total = 0;
            limpiar();

            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            comando.Connection = Cx.conexion;
            if (textBox1.Text != "")
            {
                //declaramos el comando para realizar la busqueda
                comando.CommandText = "select * from Taller where Datos like '%" + textBox1.Text.ToUpper() + "%' and fecha BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) ORDER BY fecha";
                comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
                comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
            }
            else
            {
                //declaramos el comando para realizar la busqueda
                comando.CommandText = "select * from Taller";
            }
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            Cx.Conectar();
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
                dataGridView1.Rows[renglon].Cells["cliente"].Value = dr.GetString(dr.GetOrdinal("cliente"));
                dataGridView1.Rows[renglon].Cells["marca"].Value = dr.GetString(dr.GetOrdinal("Marca"));
                dataGridView1.Rows[renglon].Cells["modelo"].Value = dr.GetString(dr.GetOrdinal("Datos"));
                dataGridView1.Rows[renglon].Cells["averia"].Value = dr.GetString(dr.GetOrdinal("averia"));
                dataGridView1.Rows[renglon].Cells["precio"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("precio")));
                dataGridView1.Rows[renglon].Cells["nota"].Value = dr.GetString(dr.GetOrdinal("nota"));
                dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("fecha"));

                total += Convert.ToDouble(dataGridView1.Rows[renglon].Cells["precio"].Value);
                txttotalG.Text = Convert.ToString(total);
            }
            Cx.Desconectar();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            char delimitador = '.';

            var marca = dataGridView1.CurrentRow.Cells["modelo"].Value.ToString();
            string cadena = marca;
            string[] valores = cadena.Split(delimitador);

            var cliente = dataGridView1.CurrentRow.Cells["cliente"].Value.ToString();
            string cadena1 = cliente;
            string[] valores1 = cadena1.Split(delimitador);

            Program.descripcion = dataGridView1.CurrentRow.Cells["tipoDeTrabajo"].Value.ToString();
            Program.marca = dataGridView1.CurrentRow.Cells["marca"].Value.ToString();
            Program.averia = dataGridView1.CurrentRow.Cells["averia"].Value.ToString();
            Program.NombreCliente = valores1[0].Trim();
            Program.telefono = valores1[1].Trim();
            Program.Imei = valores[1].Trim();
            Program.Modelo = valores[0].Trim();
            Program.total = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["precio"].Value.ToString());
            Program.nota = dataGridView1.CurrentRow.Cells["nota"].Value.ToString();
            Program.Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());

            Program.abiertosecundario = false;
            Program.abierto = false;
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Program.abiertosecundario = false;
            Program.abierto = false;
            this.Close();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            cargardata();
        }
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
        public void limpiar()
        {
            dataGridView1.Rows.Clear();
        }

        public void buscarporfecha()
        {
            Cx.Desconectar();
            double total = 0;
            limpiar();
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            comando.Connection = Cx.conexion;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "select * from Taller where fecha BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) ORDER BY fecha";
            comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
            comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            Cx.Conectar();;
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
                dataGridView1.Rows[renglon].Cells["cliente"].Value = dr.GetString(dr.GetOrdinal("cliente"));
                dataGridView1.Rows[renglon].Cells["marca"].Value = dr.GetString(dr.GetOrdinal("Marca"));
                dataGridView1.Rows[renglon].Cells["modelo"].Value = dr.GetString(dr.GetOrdinal("Datos"));
                dataGridView1.Rows[renglon].Cells["averia"].Value = dr.GetString(dr.GetOrdinal("averia"));
                dataGridView1.Rows[renglon].Cells["precio"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("precio")));
                dataGridView1.Rows[renglon].Cells["nota"].Value = dr.GetString(dr.GetOrdinal("nota"));
                dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("fecha"));

                total += Convert.ToDouble(dataGridView1.Rows[renglon].Cells["precio"].Value);
                txttotalG.Text = Convert.ToString(total);
            }
           Cx.Desconectar();
        }
        private void frmBuscarAlineacionyBalanceo_Load(object sender, EventArgs e)
        {
            limpiar();
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
            iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance("Logo.png");
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
                string envio = "Fecha : " + DateTime.Now.ToString();

                Chunk chunk = new Chunk(remito, FontFactory.GetFont("ARIAL", 16, iTextSharp.text.Font.BOLD, color: BaseColor.BLUE));
                doc.Add(new Paragraph("                                                                                                                                                                                                                                                     " + envio, FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.ITALIC)));
                doc.Add(image1);
                doc.Add(new Paragraph(chunk));
                doc.Add(new Paragraph(ubicado, FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.NORMAL)));
                doc.Add(new Paragraph("                       "));
                doc.Add(new Paragraph("Reporte de Listado de Celulares en Reparacion                       "));
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
            Cx.Desconectar();
            double total = 0;

            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            comando.Connection = Cx.conexion;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "select * from taller where tipoDeTrabajo like '%" + cbtipo.Text + "%' AND fecha BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) ORDER BY fecha";
            comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
            comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            Cx.Conectar();
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
                dataGridView1.Rows[renglon].Cells["cliente"].Value = dr.GetString(dr.GetOrdinal("cliente"));
                dataGridView1.Rows[renglon].Cells["marca"].Value = dr.GetString(dr.GetOrdinal("Marca"));
                dataGridView1.Rows[renglon].Cells["modelo"].Value = dr.GetString(dr.GetOrdinal("Datos"));
                dataGridView1.Rows[renglon].Cells["averia"].Value = dr.GetString(dr.GetOrdinal("averia"));
                dataGridView1.Rows[renglon].Cells["precio"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("precio")));
                dataGridView1.Rows[renglon].Cells["nota"].Value = dr.GetString(dr.GetOrdinal("nota"));
                dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("fecha"));

                total += Convert.ToDouble(dataGridView1.Rows[renglon].Cells["precio"].Value);
                txttotalG.Text = Convert.ToString(total);
            }
            Cx.Desconectar();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            cargardata();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cx.Desconectar();
            Program.Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
            Program.tipogoma = dataGridView1.CurrentRow.Cells["tipoDeTrabajo"].Value.ToString();
            if (Program.Id > 0)
            {
                decimal caja = 0, monto = 0;
                if (Program.tipogoma.ToUpper() == "SALIDA")
                {
                    Cx.Conectar();
                    string sql = "select * FROM pagos where idVenta=" + Program.Id;
                    SqlCommand cmd1 = new SqlCommand(sql, Cx.conexion);
                    SqlDataReader reade = cmd1.ExecuteReader();
                    if (reade.Read())
                    {
                        caja = Convert.ToInt32(reade["id_caja"]);

                        decimal ingresos = Convert.ToDecimal(reade["ingresos"]);
                        decimal egresos = Convert.ToDecimal(reade["egresos"]);
                        monto = ingresos - egresos;
                    }
                    Cx.Desconectar();
                }

                if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Eliminar?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    using (SqlCommand cmd = new SqlCommand("eliminarTaller", Cx.conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Program.Id;
                        cmd.Parameters.Add("@idCaja", SqlDbType.Int).Value = caja;
                        cmd.Parameters.Add("@monto", SqlDbType.Int).Value = monto;
                        cmd.Parameters.Add("@tipoDeTrabajo", SqlDbType.NVarChar).Value = Program.tipogoma;

                        Cx.Conectar();
                        cmd.ExecuteNonQuery();
                        Cx.Desconectar();
                        Program.Id = 0;
                        Program.tipogoma = "";
                    }
                }
                cargardata();
            }
            else
            {
                MessageBox.Show("Por Favor Seleccione una fila antes de eliminarla");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            limpiar();
            textBox1.Text = "";
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


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
    public partial class frmListadoVentas : DevComponents.DotNetBar.Metro.MetroForm
    {
        public frmListadoVentas()
        {
            InitializeComponent();
        }

        clsCx Cx = new clsCx();
        private void frmListadoVentas_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = false;
            Program.rncClient = "";
            txtBuscarid.Enabled = false;
            cargar_combo_NCF(combo_tipo_NCF);
            cargar_combo_Tipofactura(cbtipofactura);
            llenarganancia(DateTime.MinValue, DateTime.MinValue);
            //repetitivo();
            llenar_data("");
            //llenar_data_V();
            //buscarprod();
            gridforcategoryandquantity(DateTime.MinValue, DateTime.MinValue);
        }
        public int borrado = 0;
        public void cargar_combo_NCF(ComboBox combo_tipo_NCF)
        {
            SqlCommand cm = new SqlCommand("CARGARcomboNCF", Cx.conexion);
            cm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            combo_tipo_NCF.DisplayMember = "descripcion_ncf";
            combo_tipo_NCF.ValueMember = "id_ncf";
            combo_tipo_NCF.DataSource = dt;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmMenuPrincipal menu = new FrmMenuPrincipal();
            FrmRegistroVentas V = new FrmRegistroVentas();
            V.txtUsu.Text = menu.lblUsuario.Text;
            V.txtidEmp.Text = Convert.ToString(Program.IdEmpleadoLogueado);
            V.lblLogo.Text = menu.lblLogo.Text;
            V.lblDir.Text = menu.lblDir.Text;
            V.lblTel1.Text = menu.lblTel1.Text;
            V.lblTel2.Text = menu.lblTel2.Text;
            V.lblCorreo.Text = menu.lblCorreo.Text;
            V.lblrnc.Text = menu.lblrnc.Text;
            V.Show();
            Hide();
        }
        public void cargar_combo_Tipofactura(ComboBox tipofactura)
        {
            SqlCommand cm = new SqlCommand("CARGARcomboTipoFactura", Cx.conexion);
            cm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            tipofactura.DisplayMember = "descripcion";
            tipofactura.ValueMember = "id";
            tipofactura.DataSource = dt;
        }
        public void llenar_data(string id)
        {
            decimal total = 0;
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

            if (chkid.Checked && chknombre.Checked == false && id != null)
            {
                comando.CommandText = "select dbo.Venta.IdVenta,rcnClient=COALESCE(dbo.Venta.rcnClient, 'Sin rnc cliente'),dbo.Venta.Restante,dbo.Venta.Tipofactura," +
                    "dbo.Venta.Total,Direccion=COALESCE(dbo.Venta.Direccion, ' '),dbo.Venta.IdEmpleado,dbo.Venta.TipoDocumento,dbo.Venta.NroDocumento,dbo.Venta.FechaVenta," +
                    "NombreCliente=COALESCE(dbo.Venta.NombreCliente, ' ') from venta WHERE IdVenta = " + id + " and dbo.Venta.borrado = " + borrado + " ORDER BY IdVenta";
            }
            else if (chknombre.Checked && chkid.Checked == false && id != null)
            {
                comando.CommandText = "select dbo.Venta.IdVenta,rcnClient=COALESCE(dbo.Venta.rcnClient, 'Sin rnc cliente'),dbo.Venta.Restante,dbo.Venta.Tipofactura," +
                    "dbo.Venta.Total,Direccion=COALESCE(dbo.Venta.Direccion, ' '),dbo.Venta.IdEmpleado,dbo.Venta.TipoDocumento,dbo.Venta.NroDocumento,dbo.Venta.FechaVenta," +
                    "NombreCliente=COALESCE(dbo.Venta.NombreCliente, ' ') from venta WHERE NombreCliente LIKE '%" + id + "%' and dbo.Venta.borrado = " + borrado + " ORDER BY IdVenta";
            }
            else
            {
                comando.CommandText = "select dbo.Venta.IdVenta,rcnClient=COALESCE(dbo.Venta.rcnClient, 'Sin rnc cliente'),dbo.Venta.Restante,dbo.Venta.Tipofactura," +
                    "dbo.Venta.Total,Direccion=COALESCE(dbo.Venta.Direccion, ' '),dbo.Venta.IdEmpleado,dbo.Venta.TipoDocumento,dbo.Venta.NroDocumento,dbo.Venta.FechaVenta," +
                    "NombreCliente=COALESCE(dbo.Venta.NombreCliente, ' ') from venta ORDER BY IdVenta";
            }

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

                dataGridView1.Rows[renglon].Cells["id"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdVenta")));
                dataGridView1.Rows[renglon].Cells["idEm"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdEmpleado")));
                dataGridView1.Rows[renglon].Cells["NCF"].Value = dr.GetString(dr.GetOrdinal("TipoDocumento"));
                dataGridView1.Rows[renglon].Cells["nroComprobante"].Value = dr.GetString(dr.GetOrdinal("NroDocumento"));
                dataGridView1.Rows[renglon].Cells["total"].Value = dr.GetDecimal(dr.GetOrdinal("Total")).ToString();
                dataGridView1.Rows[renglon].Cells["Tipo"].Value = dr.GetString(dr.GetOrdinal("Tipofactura"));
                dataGridView1.Rows[renglon].Cells["restante"].Value = dr.GetDecimal(dr.GetOrdinal("Restante")).ToString();
                dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("FechaVenta"));
                dataGridView1.Rows[renglon].Cells["nombrecliente"].Value = dr.GetString(dr.GetOrdinal("NombreCliente"));
                dataGridView1.Rows[renglon].Cells["ultimafecha"].Value = dr.GetDateTime(dr.GetOrdinal("UltimaFechaPago"));

                total += Convert.ToDecimal(dr.GetDecimal(dr.GetOrdinal("total")));
                txtTtal.Text = Math.Round(total, 2).ToString("C2");
            }
            con.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (DevComponents.DotNetBar.MessageBoxEx.Show("Imprimir Reporte \n Si=Especifico \n No=General ", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                To_pdf1();
            }
            else
            {
                To_pdf();
            }
        }
        public void llenarid(int idventa)
        {
            string cadSql = "select IdCliente =COALESCE(dbo.Venta.IdCliente,0) from Venta where idventa=" + idventa;

            SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);
            Cx.conexion.Open();

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
            {
                Program.IdCliente = Convert.ToInt32(leer["IdCliente"]);
            }
            Cx.conexion.Close();
        }
        public void llenarganancia(DateTime fecha1, DateTime fecha2)
        {
            string cadSql = "";
            bool confecha = false;
            if ((fecha1 != DateTime.MinValue && fecha2 != DateTime.MinValue) && (fecha2.Date != DateTime.Today || fecha1.Date != DateTime.Today))
            {
                cadSql = "select Sum(GananciaVenta) as ganancia from DetalleVenta inner join Venta on DetalleVenta.IdVenta= Venta.IdVenta where Venta.FechaVenta " +
                    "BETWEEN convert(datetime, CONVERT(varchar(10),@fecha1, 103), 103) AND convert(datetime, CONVERT(varchar(10),@fecha2, 103), 103)";
                confecha = true;
            }
            else
            {
                cadSql = "select Sum(GananciaVenta) as ganancia from DetalleVenta";
            }


            SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);
            if (confecha)
            {
                comando.Parameters.AddWithValue("@fecha1", fecha1);
                comando.Parameters.AddWithValue("@fecha2", fecha2);
                comando.CommandType = CommandType.Text;
            }

            Cx.conexion.Open();

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
            {
                txtGanancias.Text = (Convert.ToDecimal(leer["ganancia"])).ToString("C2");
            }
            Cx.conexion.Close();
        }
        public void seleccion_data()
        {
            Program.Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
            llenarid(Program.Id);
            if (Program.IdCliente > 0)
            {
                Cx.conexion.Open();
                string sql = "SELECT DNI,Apellidos,Nombres from Cliente where IdCliente = '" + Program.IdCliente + "'";
                SqlCommand comando = new SqlCommand(sql, Cx.conexion);
                SqlDataReader dr = comando.ExecuteReader();
                if (dr.Read())
                {
                    Program.DocumentoIdentidad = dr.GetString(dr.GetOrdinal("DNI"));
                    Program.NombreCliente = dr.GetString(dr.GetOrdinal("Nombres"));
                    Program.ApellidosCliente = dr.GetString(dr.GetOrdinal("Apellidos"));
                }
                Cx.conexion.Close();
            }
            else
            {
                Program.datoscliente = dataGridView1.CurrentRow.Cells["nombrecliente"].Value.ToString();
                Program.DocumentoIdentidad = "";
            }

            Program.tipo = dataGridView1.CurrentRow.Cells["Tipo"].Value.ToString();
            Program.NCF = dataGridView1.CurrentRow.Cells["NCF"].Value.ToString();
            Program.NroComprobante = dataGridView1.CurrentRow.Cells["nroComprobante"].Value.ToString();
            Program.total = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["total"].Value.ToString());
            Program.Direccion = dataGridView1.CurrentRow.Cells["Direccion"].Value.ToString();
            Program.fecha = dataGridView1.CurrentRow.Cells["fecha"].Value.ToString();
            Program.IdEmpleado = Convert.ToInt32(dataGridView1.CurrentRow.Cells["idEm"].Value.ToString());
            var rnc = dataGridView1.CurrentRow.Cells["rnccliente"].Value.ToString();
            if (rnc != "sin rcn del Cliente")
            {
                Program.rncClient = rnc;
            }

            if (Program.tipo != "Credito")
            {
                Program.Esabono = "";
                Program.total = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["total"].Value.ToString());
            }
            else
            {
                Program.Esabono = "Es Abono";
                Program.total = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["restante"].Value.ToString());
                Program.ultimafechapago = dataGridView1.CurrentRow.Cells["ultimafecha"].Value.ToString();
            }

            Program.ReImpresion = "Copia Factura";
        }
        private void label2_Click(object sender, EventArgs e)
        {
            Program.abierto = false;
            Program.abiertosecundarias = false;
            FrmRegistroVentas V = new FrmRegistroVentas();
            V.txtidEmp.Text = Convert.ToString(Program.IdEmpleadoLogueado);
            this.Close();
        }
        private void To_pdf1()
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

                    doc.Add(new Paragraph("Reporte Especifico de Ventas Realizadas"));
                    doc.Add(new Paragraph("Desde la Fecha: " + (dtpfecha1.Value.Day + "/" + dtpfecha1.Value.Month + "/" + dtpfecha1.Value.Year).ToString() + ", " + "Hasta la Fecha: " + (dtpfecha2.Value.Day + "/" + dtpfecha2.Value.Month + "/" + dtpfecha2.Value.Year).ToString(), FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("                       "));
                    GenerarDocumento1(doc);
                    doc.AddCreationDate();
                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("Total de Ventas      : " + txttotalventaespecifica.Text));
                    doc.Add(new Paragraph("Total de Ganancias   : " + txtGanancias.Text));
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
        private void To_pdf()
        {
            Document doc = new Document(PageSize.LETTER, 10f, 10f, 10f, 0f);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            Image image1 = Image.GetInstance("ferreteria.png");
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

                    if (cbPendiente.Checked)
                    {
                        doc.Add(new Paragraph("Reporte de Deudas"));
                        doc.Add(new Paragraph("Desde la Fecha: " + (dtpfecha1.Value.Day + "/" + dtpfecha1.Value.Month + "/" + dtpfecha1.Value.Year).ToString() + ", " + "Hasta la Fecha: " + (dtpfecha2.Value.Day + "/" + dtpfecha2.Value.Month + "/" + dtpfecha2.Value.Year).ToString(), FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.NORMAL)));
                        doc.Add(new Paragraph("                       "));
                        GenerarDocumento(doc);
                        doc.AddCreationDate();
                        doc.Add(new Paragraph("                       "));
                        doc.Add(new Paragraph("Total de Deuda      : " + txttotalpendiente.Text));
                        doc.Add(new Paragraph("                       "));
                        doc.Add(new Paragraph("                       "));
                        doc.Add(new Paragraph("                       "));
                        doc.Add(new Paragraph("____________________________________"));
                        doc.Add(new Paragraph("                         Firma              "));
                        doc.Close();
                    }
                    else
                    {
                        doc.Add(new Paragraph("Reporte de General de Ventas Realizadas"));
                        doc.Add(new Paragraph("Desde la Fecha: " + (dtpfecha1.Value.Day + "/" + dtpfecha1.Value.Month + "/" + dtpfecha1.Value.Year).ToString() + ", " + "Hasta la Fecha: " + (dtpfecha2.Value.Day + "/" + dtpfecha2.Value.Month + "/" + dtpfecha2.Value.Year).ToString(), FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.NORMAL)));
                        doc.Add(new Paragraph("                       "));
                        GenerarDocumento(doc);
                        doc.AddCreationDate();
                        doc.Add(new Paragraph("                       "));
                        doc.Add(new Paragraph("Reporte Especifico de Ventas Realizadas"));
                        GenerarDocumento1(doc);
                        doc.Add(new Paragraph("                       "));
                        doc.Add(new Paragraph("Total de Ventas      : " + txtTtal.Text));
                        doc.Add(new Paragraph("Total de Ganancias   : " + txtGanancias.Text));
                        //doc.Add(new Paragraph("Producto Mas Vendido : " + txtRepi.Text));
                        doc.Add(new Paragraph("                       "));
                        doc.Add(new Paragraph("                       "));
                        doc.Add(new Paragraph("                       "));
                        doc.Add(new Paragraph("____________________________________"));
                        doc.Add(new Paragraph("                         Firma              "));
                        doc.Close();
                    }

                    Process.Start(filename);//Esta parte se puede omitir, si solo se desea guardar el archivo, y que este no se ejecute al instante
                }
            }
            else
            {
                MessageBox.Show("No guardo el Archivo");
            }
        }
        public void GenerarDocumento1(Document document)
        {
            int i, j;
            PdfPTable datatable = new PdfPTable(dataGridView3.ColumnCount);
            datatable.DefaultCell.Padding = 2;
            float[] headerwidths = GetTamañoColumnas(dataGridView3);
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 100;
            datatable.DefaultCell.BorderWidth = 1;
            datatable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            for (i = 0; i < dataGridView3.ColumnCount; i++)
            {
                datatable.AddCell(new Phrase(dataGridView3.Columns[i].HeaderText, FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD)));
            }
            datatable.HeaderRows = 1;
            datatable.DefaultCell.BorderWidth = 1;
            for (i = 0; i < dataGridView3.Rows.Count; i++)
            {
                for (j = 0; j < dataGridView3.Columns.Count; j++)
                {
                    if (dataGridView3[j, i].Value != null)
                    {
                        datatable.AddCell(new Phrase(dataGridView3[j, i].Value.ToString(), FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));//En esta parte, se esta agregando un renglon por cada registro en el datagrid
                    }
                }
                datatable.CompleteRow();
            }
            document.Add(datatable);
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
        private void button1_Click(object sender, EventArgs e)
        {
            decimal total = 0; decimal totalpendiente = 0;
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

            if (txtBuscarid.Text != "" && txtBuscarid.Text != null)
            {
                if (cbtipodocumento.Checked == true)
                {
                    //declaramos el comando para realizar la busqueda
                    comando.CommandText = "select dbo.Venta.IdVenta,rcnClient=COALESCE(dbo.Venta.rcnClient, 'Sin rnc cliente'),dbo.Venta.Restante,dbo.Venta.Tipofactura," +
                    "dbo.Venta.Total,Direccion=COALESCE(dbo.Venta.Direccion, ' '),dbo.Venta.IdEmpleado,dbo.Venta.TipoDocumento,dbo.Venta.NroDocumento,dbo.Venta.FechaVenta," +
                    "NombreCliente=COALESCE(dbo.Venta.NombreCliente, ' ') from venta where NombreCliente  LIKE '%" + txtBuscarid.Text + "%' and FechaVenta BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) " +
                    "AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) AND dbo.Venta.TipoDocumento = @TipoDocumento and dbo.Venta.borrado=" + borrado + " ORDER BY dbo.Venta.IdVenta";
                    comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
                    comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
                    comando.Parameters.AddWithValue("@TipoDocumento", combo_tipo_NCF.Text);
                }
                else if (cktipofactura.Checked == true && cbPendiente.Checked == false)
                {
                    //declaramos el comando para realizar la busqueda
                    comando.CommandText = "select dbo.Venta.IdVenta,rcnClient=COALESCE(dbo.Venta.rcnClient, 'Sin rnc cliente'),dbo.Venta.Restante,dbo.Venta.Tipofactura," +
                    "dbo.Venta.Total,Direccion=COALESCE(dbo.Venta.Direccion, ' '),dbo.Venta.IdEmpleado,dbo.Venta.TipoDocumento,dbo.Venta.NroDocumento,dbo.Venta.FechaVenta," +
                    "NombreCliente=COALESCE(dbo.Venta.NombreCliente, ' ') from venta where NombreCliente  LIKE '%" + txtBuscarid.Text + "%' and FechaVenta BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) " +
                    "AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) AND dbo.Venta.Tipofactura = @Tipofactura and dbo.Venta.borrado=" + borrado + " ORDER BY dbo.Venta.IdVenta";
                    comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
                    comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
                    comando.Parameters.AddWithValue("@Tipofactura", cbtipofactura.Text);
                }
                else if (cktipofactura.Checked == false && cbPendiente.Checked == true)
                {
                    //declaramos el comando para realizar la busqueda
                    comando.CommandText = "select dbo.Venta.IdVenta,rcnClient=COALESCE(dbo.Venta.rcnClient, 'Sin rnc cliente'),dbo.Venta.Restante,dbo.Venta.Tipofactura," +
                    "dbo.Venta.Total,Direccion=COALESCE(dbo.Venta.Direccion, ' '),dbo.Venta.IdEmpleado,dbo.Venta.TipoDocumento,dbo.Venta.NroDocumento,dbo.Venta.FechaVenta," +
                    "NombreCliente=COALESCE(dbo.Venta.NombreCliente, ' ') from venta where NombreCliente  LIKE '%" + txtBuscarid.Text + "%' and FechaVenta BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) " +
                    "AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) and dbo.Venta.borrado=" + borrado + " and dbo.Venta.Restante > " + 0 + " ORDER BY dbo.Venta.IdVenta";
                    comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
                    comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
                }
                else if (cktipofactura.Checked == true && cbPendiente.Checked == true)
                {
                    //declaramos el comando para realizar la busqueda
                    comando.CommandText = "select dbo.Venta.IdVenta,rcnClient=COALESCE(dbo.Venta.rcnClient, 'Sin rnc cliente'),dbo.Venta.Restante,dbo.Venta.Tipofactura," +
                    "dbo.Venta.Total,Direccion=COALESCE(dbo.Venta.Direccion, ' '),dbo.Venta.IdEmpleado,dbo.Venta.TipoDocumento,dbo.Venta.NroDocumento,dbo.Venta.FechaVenta," +
                    "NombreCliente=COALESCE(dbo.Venta.NombreCliente, ' ') from venta where NombreCliente  LIKE '%" + txtBuscarid.Text + "%' and FechaVenta BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) " +
                    "AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) AND dbo.Venta.Tipofactura = @Tipofactura and dbo.Venta.Restante > " + 0 + " and dbo.Venta.borrado=" + borrado + " ORDER BY dbo.Venta.IdVenta";
                    comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
                    comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
                    comando.Parameters.AddWithValue("@Tipofactura", cbtipofactura.Text);
                }
                else
                {
                    //declaramos el comando para realizar la busqueda
                    comando.CommandText = "select dbo.Venta.IdVenta,rcnClient=COALESCE(dbo.Venta.rcnClient, 'Sin rnc cliente'),dbo.Venta.Restante,dbo.Venta.Tipofactura," +
                    "dbo.Venta.Total,Direccion=COALESCE(dbo.Venta.Direccion, ' '),dbo.Venta.IdEmpleado,dbo.Venta.TipoDocumento,dbo.Venta.NroDocumento,dbo.Venta.FechaVenta," +
                    "NombreCliente=COALESCE(dbo.Venta.NombreCliente, ' ') from venta where NombreCliente  LIKE '%" + txtBuscarid.Text + "%' and FechaVenta BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) " +
                    "AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) and dbo.Venta.borrado=" + borrado + " ORDER BY dbo.Venta.IdVenta";
                    comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
                    comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
                }
            }
            else
            {
                if (cbtipodocumento.Checked == true)
                {
                    //declaramos el comando para realizar la busqueda
                    comando.CommandText = "select dbo.Venta.IdVenta,rcnClient=COALESCE(dbo.Venta.rcnClient, 'Sin rnc cliente'),dbo.Venta.Restante,dbo.Venta.Tipofactura," +
                    "dbo.Venta.Total,Direccion=COALESCE(dbo.Venta.Direccion, ' '),dbo.Venta.IdEmpleado,dbo.Venta.TipoDocumento,dbo.Venta.NroDocumento,dbo.Venta.FechaVenta," +
                    "NombreCliente=COALESCE(dbo.Venta.NombreCliente, ' ') from venta where FechaVenta BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) " +
                    "AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) AND dbo.Venta.TipoDocumento = @TipoDocumento and dbo.Venta.borrado=" + borrado + " ORDER BY dbo.Venta.IdVenta";
                    comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
                    comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
                    comando.Parameters.AddWithValue("@TipoDocumento", combo_tipo_NCF.Text);
                }
                else if (cbtipodocumento.Checked == true && cbPendiente.Checked == true)
                {
                    //declaramos el comando para realizar la busqueda
                    comando.CommandText = "select dbo.Venta.IdVenta,rcnClient=COALESCE(dbo.Venta.rcnClient, 'Sin rnc cliente'),dbo.Venta.Restante,dbo.Venta.Tipofactura," +
                    "dbo.Venta.Total,Direccion=COALESCE(dbo.Venta.Direccion, ' '),dbo.Venta.IdEmpleado,dbo.Venta.TipoDocumento,dbo.Venta.NroDocumento,dbo.Venta.FechaVenta," +
                    "NombreCliente=COALESCE(dbo.Venta.NombreCliente, ' ') from venta where FechaVenta BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) " +
                    "AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) AND dbo.Venta.TipoDocumento = @TipoDocumento and dbo.Venta.borrado=" + borrado +
                    " and dbo.Venta.Restante > " + 0 + " ORDER BY dbo.Venta.IdVenta";
                    comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
                    comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
                    comando.Parameters.AddWithValue("@TipoDocumento", combo_tipo_NCF.Text);
                }
                else if (cktipofactura.Checked == true)
                {
                    //declaramos el comando para realizar la busqueda
                    comando.CommandText = "select dbo.Venta.IdVenta,rcnClient=COALESCE(dbo.Venta.rcnClient, 'Sin rnc cliente'),dbo.Venta.Restante,dbo.Venta.Tipofactura," +
                    "dbo.Venta.Total,Direccion=COALESCE(dbo.Venta.Direccion, ' '),dbo.Venta.IdEmpleado,dbo.Venta.TipoDocumento,dbo.Venta.NroDocumento,dbo.Venta.FechaVenta," +
                    "NombreCliente=COALESCE(dbo.Venta.NombreCliente, ' ') from venta where FechaVenta BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) " +
                    "AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) AND dbo.Venta.Tipofactura = @Tipofactura and dbo.Venta.borrado=" + borrado + " ORDER BY dbo.Venta.IdVenta";
                    comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
                    comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
                    comando.Parameters.AddWithValue("@Tipofactura", cbtipofactura.Text);
                }
                else if (cktipofactura.Checked == false && cbtipodocumento.Checked == false && vereliminadas.Checked == false && cbPendiente.Checked == true)
                {
                    //declaramos el comando para realizar la busqueda
                    comando.CommandText = "select dbo.Venta.IdVenta,rcnClient=COALESCE(dbo.Venta.rcnClient, 'Sin rnc cliente'),dbo.Venta.Restante,dbo.Venta.Tipofactura," +
                    "dbo.Venta.Total,Direccion=COALESCE(dbo.Venta.Direccion, ' '),dbo.Venta.IdEmpleado,dbo.Venta.TipoDocumento,dbo.Venta.NroDocumento,dbo.Venta.FechaVenta," +
                    "NombreCliente=COALESCE(dbo.Venta.NombreCliente, ' ') from venta where FechaVenta BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) " +
                    "AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) and dbo.Venta.Restante > " + 0 + " and dbo.Venta.borrado=" + borrado + " ORDER BY dbo.Venta.IdVenta";
                    comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
                    comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
                }
                else
                {
                    //declaramos el comando para realizar la busqueda
                    comando.CommandText = "select dbo.Venta.IdVenta,rcnClient=COALESCE(dbo.Venta.rcnClient, 'Sin rnc cliente'),dbo.Venta.Restante,dbo.Venta.Tipofactura," +
                    "dbo.Venta.Total,Direccion=COALESCE(dbo.Venta.Direccion, ' '),dbo.Venta.IdEmpleado,dbo.Venta.TipoDocumento,dbo.Venta.NroDocumento,dbo.Venta.FechaVenta," +
                    "NombreCliente=COALESCE(dbo.Venta.NombreCliente, ' ') from venta where FechaVenta BETWEEN convert(datetime, CONVERT(varchar(10), @fecha1, 103), 103) " +
                    "AND convert(datetime, CONVERT(varchar(10), @fecha2, 103), 103) and dbo.Venta.borrado=" + borrado + " ORDER BY dbo.Venta.IdVenta";
                    comando.Parameters.AddWithValue("@fecha1", dtpfecha1.Value);
                    comando.Parameters.AddWithValue("@fecha2", dtpfecha2.Value);
                }
            }

            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            con.Open();
            //limpiamos los renglones de la datagridview
            dataGridView1.Rows.Clear();
            //a la variable DataReader asignamos  el la variable de tipo SqlCommand
            dr = comando.ExecuteReader();
            //el ciclo while se ejecutará mientras lea registros en la tabla

            int idanterior = 0;
            while (dr.Read())
            {
                //variable de tipo entero para ir enumerando los la filas del datagridview
                int renglon = dataGridView1.Rows.Add();
                // especificamos en que fila se mostrará cada registro
                int idVentaactual = dr.GetInt32(dr.GetOrdinal("IdVenta"));
                dataGridView1.Rows[renglon].Cells["id"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdVenta")));
                dataGridView1.Rows[renglon].Cells["idEm"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdEmpleado")));
                dataGridView1.Rows[renglon].Cells["NCF"].Value = dr.GetString(dr.GetOrdinal("TipoDocumento"));
                dataGridView1.Rows[renglon].Cells["nroComprobante"].Value = dr.GetString(dr.GetOrdinal("NroDocumento"));
                dataGridView1.Rows[renglon].Cells["total"].Value = dr.GetDecimal(dr.GetOrdinal("Total")).ToString();
                dataGridView1.Rows[renglon].Cells["Tipo"].Value = dr.GetString(dr.GetOrdinal("Tipofactura"));
                dataGridView1.Rows[renglon].Cells["restante"].Value = dr.GetDecimal(dr.GetOrdinal("Restante")).ToString();
                dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("FechaVenta"));
                dataGridView1.Rows[renglon].Cells["nombrecliente"].Value = dr.GetString(dr.GetOrdinal("NombreCliente"));
                dataGridView1.Rows[renglon].Cells["ultimafecha"].Value = dr.GetDateTime(dr.GetOrdinal("UltimaFechaPago"));

                total += Convert.ToDecimal(dr.GetDecimal(dr.GetOrdinal("Total")));
                txtTtal.Text = Math.Round(total, 2).ToString("C2");

                if (cbPendiente.Checked == true)
                {
                    label7.Visible = true;
                    txttotalpendiente.Visible = true;

                    if (idanterior != idVentaactual)
                    {
                        totalpendiente += Convert.ToDecimal(dr.GetDecimal(dr.GetOrdinal("Restante")));
                        txttotalpendiente.Text = Math.Round(totalpendiente, 2).ToString("C2");
                        idanterior = idVentaactual;
                    }
                }
                else
                {
                    label7.Visible = false;
                    txttotalpendiente.Visible = false;
                }
            }

            dataGridView3.ClearSelection();
            llenarganancia(dtpfecha1.Value, dtpfecha2.Value);
            gridforcategoryandquantity(dtpfecha1.Value, dtpfecha2.Value);
            con.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            txtBuscarid.Clear();
            button3.Enabled = false;
            chkid.Checked = false;
            chknombre.Checked = false;
            cktipofactura.Checked = false;
            cbtipodocumento.Checked = false;
            cbPendiente.Checked = false;
            vereliminadas.Checked = false;
            Program.Id = 0;
            Program.tipo = "";
            gridforcategoryandquantity(DateTime.MinValue, DateTime.MinValue);
            llenar_data("");
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Program.abiertosecundarias = false;
            Program.abierto = false;
            seleccion_data();
            FrmRegistroVentas V = new FrmRegistroVentas();
            V.btnSalir.Visible = false;
            this.Close();
        }
        private void gridforcategoryandquantity(DateTime fecha1, DateTime fecha2)
        {
            decimal total = 0;
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

            if (fecha1 != DateTime.MinValue && fecha2 != DateTime.MinValue)
            {
                //declaramos el comando para realizar la busqueda
                comando.CommandText = "SELECT Categoria.Descripcion AS CategoryOfProducts,sum(DetalleVenta.Cantidad) AS CantidadOfProducts,sum(DetalleVenta.SubTotal) AS PrecioOfProducts" +
                    " FROM DetalleVenta INNER JOIN Producto ON DetalleVenta.IdProducto = Producto.IdProducto INNER JOIN Categoria ON Producto.IdCategoria = Categoria.IdCategoria " +
                    "INNER JOIN Venta ON DetalleVenta.IdVenta = Venta.IdVenta  where venta.FechaVenta BETWEEN convert(datetime, CONVERT(varchar(10),@fecha1, 103), 103) AND " +
                    "convert(datetime, CONVERT(varchar(10),@fecha2, 103), 103) and dbo.Venta.borrado=" + borrado + "group by Categoria.Descripcion ORDER BY sum(DetalleVenta.Cantidad) DESC";
                comando.Parameters.AddWithValue("@fecha1", fecha1);
                comando.Parameters.AddWithValue("@fecha2", fecha2);
            }
            else
            {
                //declaramos el comando para realizar la busqueda
                comando.CommandText = "SELECT Categoria.Descripcion AS CategoryOfProducts,sum(DetalleVenta.Cantidad) AS CantidadOfProducts,sum(DetalleVenta.SubTotal) AS PrecioOfProducts" +
                    " FROM DetalleVenta INNER JOIN Producto ON DetalleVenta.IdProducto = Producto.IdProducto INNER JOIN Categoria ON Producto.IdCategoria = Categoria.IdCategoria " +
                    "INNER JOIN Venta ON DetalleVenta.IdVenta = Venta.IdVenta and dbo.Venta.borrado=" + borrado + "group by Categoria.Descripcion ORDER BY sum(DetalleVenta.Cantidad) DESC";
            }
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            con.Open();
            //limpiamos los renglones de la datagridview
            dataGridView3.Rows.Clear();
            //a la variable DataReader asignamos  el la variable de tipo SqlCommand
            dr = comando.ExecuteReader();
            //el ciclo while se ejecutará mientras lea registros en la tabla
            while (dr.Read())
            {
                //variable de tipo entero para ir enumerando los la filas del datagridview
                int renglon = dataGridView3.Rows.Add();
                // especificamos en que fila se mostrará cada registro

                dataGridView3.Rows[renglon].Cells["PrecioOfProducts"].Value = dr.GetDecimal(dr.GetOrdinal("PrecioOfProducts")).ToString();
                dataGridView3.Rows[renglon].Cells["CantidadOfProducts"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("CantidadOfProducts")));
                dataGridView3.Rows[renglon].Cells["CategoryOfProducts"].Value = dr.GetString(dr.GetOrdinal("CategoryOfProducts"));

                total += dr.GetDecimal(dr.GetOrdinal("PrecioOfProducts"));

                txttotalventaespecifica.Text = Math.Round(total, 2).ToString("C2");
            }
            con.Close();
        }
        private void txtBuscarid_KeyUp(object sender, KeyEventArgs e)
        {
            if (chkid.Checked && chknombre.Checked == false)
            {
                if (txtBuscarid.Text.Length >= 1 && cktipofactura.Checked == false && cbtipodocumento.Checked == false)
                {
                    llenar_data(txtBuscarid.Text);
                }
            }
            else if (chknombre.Checked && chkid.Checked == false)
            {
                if (txtBuscarid.Text.Length >= 4 && cktipofactura.Checked == false && cbtipodocumento.Checked == false)
                {
                    llenar_data(txtBuscarid.Text);
                }
            }
            else
            {
                llenar_data("");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Program.Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
            Program.tipo = dataGridView1.CurrentRow.Cells["Tipo"].Value.ToString();
            if (Program.Id > 0)
            {
                string sql = "select DetalleVenta.Cantidad,DetalleVenta.IdProducto, Venta.TipoFactura,DetalleVenta.SubTotal,Caja.id_caja,Venta.Restante,Venta.Total from DetalleVenta" +
                " inner join Venta on DetalleVenta.IdVenta= Venta.IdVenta inner join Caja on Caja.fecha=Venta.FechaVenta where DetalleVenta.IdVenta=" + Program.Id;
                SqlCommand cmd1 = new SqlCommand(sql, Cx.conexion);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(dt);

                var i = 1;
                foreach (DataRow data in dt.Rows)
                {
                    SqlCommand sqlCommand = new SqlCommand("DevolucionVenta", Cx.conexion);
                    using (SqlCommand cmd3 = sqlCommand)
                    {
                        cmd3.CommandType = CommandType.StoredProcedure;

                        decimal cantidadDV = Convert.ToDecimal(data[0]);
                        int idProductoDV = Convert.ToInt32(data[1]);
                        string tipofacturaDV = data[2].ToString();
                        decimal subtotalDV = Convert.ToDecimal(data[3]);
                        int idcajaDV = Convert.ToInt32(data[4]);
                        decimal restanteDV = Convert.ToDecimal(data[5]);
                        decimal TotalDV = Convert.ToDecimal(data[6]);

                        if (tipofacturaDV.ToLower() == "credito")
                        {
                            subtotalDV = restanteDV / dt.Rows.Count;
                        }

                        //UpdateStock
                        cmd3.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = cantidadDV;
                        cmd3.Parameters.Add("@IdProducto", SqlDbType.Int).Value = idProductoDV;
                        cmd3.Parameters.Add("@TipoFactura", SqlDbType.NVarChar).Value = tipofacturaDV;
                        cmd3.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = subtotalDV;
                        cmd3.Parameters.Add("@id_caja", SqlDbType.Int).Value = idcajaDV;

                        Cx.conexion.Open();
                        cmd3.ExecuteNonQuery();
                        Cx.conexion.Close();

                        if (i == dt.Rows.Count)
                        {
                            if (restanteDV != TotalDV)
                            {
                                SqlCommand sqlCommand2 = new SqlCommand("DevolucionVenta", Cx.conexion);
                                using (SqlCommand cmd4 = sqlCommand2)
                                {
                                    cmd4.CommandType = CommandType.StoredProcedure;

                                    decimal cantidadDV1 = 0;
                                    int idProductoDV1 = Convert.ToInt32(data[1]);
                                    string tipofacturaDV1 = "Debito";
                                    decimal subtotalDV1 = TotalDV - restanteDV;
                                    int idcajaDV1 = Convert.ToInt32(data[4]);

                                    //UpdateStock
                                    cmd4.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = cantidadDV1;
                                    cmd4.Parameters.Add("@IdProducto", SqlDbType.Int).Value = idProductoDV1;
                                    cmd4.Parameters.Add("@TipoFactura", SqlDbType.NVarChar).Value = tipofacturaDV1;
                                    cmd4.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = subtotalDV1;
                                    cmd4.Parameters.Add("@id_caja", SqlDbType.Int).Value = idcajaDV1;

                                    Cx.conexion.Open();
                                    cmd4.ExecuteNonQuery();
                                    Cx.conexion.Close();
                                }
                            }

                            decimal caja = 0, monto = 0, montoingresos = 0;
                            string sql1 = "select * FROM pagos where idVenta=" + Program.Id;
                            SqlCommand cmd2 = new SqlCommand(sql1, Cx.conexion);
                            SqlDataReader reade = cmd1.ExecuteReader();
                            if (reade.Read())
                            {
                                caja = Convert.ToInt32(reade["id_caja"]);
                                if (Program.tipo != "Debito")
                                {
                                    decimal ingresos = Convert.ToDecimal(reade["ingresos"]);
                                    decimal egresos = Convert.ToDecimal(reade["egresos"]);
                                    monto = Convert.ToDecimal(reade["monto"]);

                                    montoingresos = ingresos - egresos;
                                }
                                else
                                {
                                    decimal ingresos = Convert.ToDecimal(reade["ingresos"]);
                                    decimal egresos = Convert.ToDecimal(reade["egresos"]);

                                    monto = ingresos - egresos;
                                }
                            }

                            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Eliminar esta Venta?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                            {
                                using (SqlCommand cmd = new SqlCommand("eliminarVenta", Cx.conexion))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Program.Id;
                                    cmd.Parameters.Add("@monto", SqlDbType.Decimal).Value = monto;
                                    cmd.Parameters.Add("@ingresos", SqlDbType.Decimal).Value = montoingresos;
                                    cmd.Parameters.Add("@idCaja", SqlDbType.Int).Value = caja;
                                    cmd.Parameters.Add("@tipoFactura", SqlDbType.NVarChar).Value = Program.tipo;
                                    Cx.conexion.Open();
                                    cmd.ExecuteNonQuery();
                                    Cx.conexion.Close();
                                    Program.Id = 0;
                                    Program.tipo = "";
                                    button3.Enabled = false;
                                }
                            }
                        }
                        i = i + 1;
                    }
                }
            }
            else
            {
                MessageBox.Show("Por Favor Seleccione una venta antes de eliminarla");
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;

                if (Program.CargoEmpleadoLogueado == "Administrador")
                {
                    button3.Enabled = true;
                    button4.Enabled = true;
                }
            }
        }
        private void vereliminadas_CheckedChanged(object sender, EventArgs e)
        {
            if (vereliminadas.Checked)
            {
                borrado = 1;
                //repetitivo();
                llenar_data("");
                //llenar_data_V();
            }
            else
            {
                borrado = 0;
                //repetitivo();
                llenar_data("");
                //llenar_data_V();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //devolucion de venta
            if (DevComponents.DotNetBar.MessageBoxEx.Show("Nota: se devolveran todos los producto que contenga la venta y la misma se eliminara por completo del sistema" +
                           "\n ¿Está Seguro que Desea hacer una devolucion de esta Venta? ", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                Program.Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                Cx.conexion.Open();
                string sql = "select DetalleVenta.Cantidad,DetalleVenta.IdProducto, Venta.TipoFactura,DetalleVenta.SubTotal,Caja.id_caja,Venta.Restante,Venta.Total from DetalleVenta" +
                    " inner join Venta on DetalleVenta.IdVenta= Venta.IdVenta inner join Caja on Caja.fecha=Venta.FechaVenta where DetalleVenta.IdVenta=" + Program.Id;
                SqlCommand cmd1 = new SqlCommand(sql, Cx.conexion);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(dt);

                var i = 1;
                foreach (DataRow data in dt.Rows)
                {
                    SqlCommand sqlCommand = new SqlCommand("DevolucionVenta", Cx.conexion);
                    using (SqlCommand cmd3 = sqlCommand)
                    {
                        cmd3.CommandType = CommandType.StoredProcedure;

                        decimal cantidadDV = Convert.ToDecimal(data[0]);
                        int idProductoDV = Convert.ToInt32(data[1]);
                        string tipofacturaDV = data[2].ToString();
                        decimal subtotalDV = Convert.ToDecimal(data[3]);
                        int idcajaDV = Convert.ToInt32(data[4]);
                        decimal restanteDV = Convert.ToDecimal(data[5]);
                        decimal TotalDV = Convert.ToDecimal(data[6]);

                        if (tipofacturaDV.ToLower() == "credito")
                        {
                            subtotalDV = restanteDV / dt.Rows.Count;
                        }

                        //UpdateStock
                        cmd3.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = cantidadDV;
                        cmd3.Parameters.Add("@IdProducto", SqlDbType.Int).Value = idProductoDV;
                        cmd3.Parameters.Add("@TipoFactura", SqlDbType.NVarChar).Value = tipofacturaDV;
                        cmd3.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = subtotalDV;
                        cmd3.Parameters.Add("@id_caja", SqlDbType.Int).Value = idcajaDV;

                        cmd3.ExecuteNonQuery();

                        if (i == dt.Rows.Count)
                        {
                            if (restanteDV != TotalDV)
                            {
                                SqlCommand sqlCommand2 = new SqlCommand("DevolucionVenta", Cx.conexion);
                                using (SqlCommand cmd4 = sqlCommand2)
                                {
                                    cmd4.CommandType = CommandType.StoredProcedure;

                                    decimal cantidadDV1 = 0;
                                    int idProductoDV1 = Convert.ToInt32(data[1]);
                                    string tipofacturaDV1 = "Debito";
                                    decimal subtotalDV1 = TotalDV - restanteDV;
                                    int idcajaDV1 = Convert.ToInt32(data[4]);

                                    //UpdateStock
                                    cmd4.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = cantidadDV1;
                                    cmd4.Parameters.Add("@IdProducto", SqlDbType.Int).Value = idProductoDV1;
                                    cmd4.Parameters.Add("@TipoFactura", SqlDbType.NVarChar).Value = tipofacturaDV1;
                                    cmd4.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = subtotalDV1;
                                    cmd4.Parameters.Add("@id_caja", SqlDbType.Int).Value = idcajaDV1;

                                    cmd4.ExecuteNonQuery();
                                }
                            }

                            SqlCommand sqlCommand1 = new SqlCommand("BorrarVentaDV", Cx.conexion);
                            using (SqlCommand cmd = sqlCommand1)
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                //Borrar venta luego de devolver todos los productos
                                cmd.Parameters.Add("@IdVenta", SqlDbType.Decimal).Value = Program.Id;

                                cmd.ExecuteNonQuery();
                            }
                        }
                        i = i + 1;
                    }
                }

                Cx.conexion.Close();
            }
        }
        private void chkid_CheckedChanged(object sender, EventArgs e)
        {
            if (chkid.Checked || chknombre.Checked)
            {
                txtBuscarid.Enabled = true;
            }
            else
            {
                txtBuscarid.Enabled = false;
            }
        }
        private void chknombre_CheckedChanged(object sender, EventArgs e)
        {
            if (chknombre.Checked || chkid.Checked)
            {
                txtBuscarid.Enabled = true;
            }
            else
            {
                txtBuscarid.Enabled = false;
            }
        }
    }
}

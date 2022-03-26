using CapaEnlaceDatos;
using CapaLogicaNegocio;
using CapaLogicaNegocio.ViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class frmListadoVentas : DevComponents.DotNetBar.Metro.MetroForm
    {
        public frmListadoVentas()
        {
            InitializeComponent();
        }

        clsManejador M = new clsManejador();
        private void frmListadoVentas_Load(object sender, EventArgs e)
        {
            if (clsGenericList.listVentas is null)
            {
                clsGenericList.listVentas = new List<Venta>();
            }

            button3.Enabled = false;
            button4.Enabled = false;
            txtBuscarid.Enabled = false;

            cargar_combo_NCF(combo_tipo_NCF);
            cargar_combo_Tipofactura(cbtipofactura);

            if (TempData.tempSalesData is null || TempData.tempSalesData.Count == 0)
                TempData.tempSalesData = clsGenericList.listVentas.OrderBy(x => x.IdVenta).ToList();

            if (TempData.tempSalesData.Count > 0)
            {
                llenar_data(TempData.tempSalesData);
            }

            if (clsGenericList.listVentasPorCategoria.Count > 0 && TempData.tempSalesData.Count > 0)
            {
                llenar_categoryandquantity(clsGenericList.listVentasPorCategoria);
            }
        }

        public int borrado = 0;
        public void cargar_combo_NCF(ComboBox combo_tipo_NCF)
        {
            M.Desconectar();
            SqlCommand cm = new SqlCommand("CARGARcomboNCF", M.conexion);
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
            V.BackColor = Color.CadetBlue;
            Program.isSaler = true;
            V.Show();
            Hide();
        }

        public void cargar_combo_Tipofactura(ComboBox tipofactura)
        {
            M.Desconectar();
            SqlCommand cm = new SqlCommand("CARGARcomboTipoFactura", M.conexion);
            cm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            tipofactura.DisplayMember = "descripcion";
            tipofactura.ValueMember = "id";
            tipofactura.DataSource = dt;
        }

        public void llenar_data(List<Venta> listaventas)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in listaventas)
            {
                int renglon = dataGridView1.Rows.Add();

                dataGridView1.Rows[renglon].Cells["id"].Value = item.IdVenta.ToString();
                dataGridView1.Rows[renglon].Cells["idEm"].Value = item.IdEmpleado.ToString();
                dataGridView1.Rows[renglon].Cells["NCF"].Value = item.TipoDocumento.ToString();
                dataGridView1.Rows[renglon].Cells["nroComprobante"].Value = item.NroComprobante.ToString();
                dataGridView1.Rows[renglon].Cells["total"].Value = item.Total.ToString();
                dataGridView1.Rows[renglon].Cells["Tipo"].Value = item.Tipofactura.ToString();
                dataGridView1.Rows[renglon].Cells["restante"].Value = item.Restante.ToString();
                dataGridView1.Rows[renglon].Cells["fecha"].Value = item.FechaVenta;
                dataGridView1.Rows[renglon].Cells["Direccion"].Value = item.Direccion.ToString();
                dataGridView1.Rows[renglon].Cells["nombrecliente"].Value = item.NombreCliente.ToString();
                dataGridView1.Rows[renglon].Cells["ultimafecha"].Value = item.UltimaFechaPago;
            }

            clsGenericList.totalVendido = listaventas.Sum(x => x.Total);

            txtTtal.Text = string.Empty;
            txtTtal.Text = Math.Round(clsGenericList.totalVendido, 2).ToString("C2");
            if (string.IsNullOrWhiteSpace(txtGanancias.Text))
            {
                GananciaTotal(clsGenericList.totalGanancia);
            }
        }
        public void GananciaTotal(decimal monto)
        {
            txtGanancias.Text = string.Empty;
            txtGanancias.Text = Math.Round(monto, 2).ToString("C2");
        }
        public void llenar_categoryandquantity(List<VentasPorCategoria> listaventas)
        {
            decimal total = 0;
            dataGridView3.Rows.Clear();
            foreach (var item in listaventas)
            {
                int renglon = dataGridView3.Rows.Add();
                // especificamos en que fila se mostrará cada registro

                dataGridView3.Rows[renglon].Cells["PrecioOfProducts"].Value = item.PrecioOfProducts.ToString();
                dataGridView3.Rows[renglon].Cells["CantidadOfProducts"].Value = item.CantidadOfProducts.ToString();
                dataGridView3.Rows[renglon].Cells["CategoryOfProducts"].Value = item.CategoryOfProducts;

                total += item.PrecioOfProducts;
                txttotalventaespecifica.Text = Math.Round(total, 2).ToString("C2");
            }
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

        public void llenaridCliente(int idventa)
        {
            M.Desconectar();
            string cadSql = "select IdCliente =COALESCE(dbo.Venta.IdCliente,0) from Venta where idventa=" + idventa;
            M.Conectar();
            SqlCommand comando = new SqlCommand(cadSql, M.conexion);

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
            {
                Program.IdCliente = Convert.ToInt32(leer["IdCliente"]);
            }
            M.Desconectar();
        }

        public void seleccion_data()
        {
            M.Desconectar();
            Program.Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
            llenaridCliente(Program.Id);

            if (Program.IdCliente > 0)
            {
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

                    doc.Add(new Paragraph("Reporte Especifico de Ventas Realizadas"));
                    doc.Add(new Paragraph("Desde la Fecha: " + (dtpfecha1.Value.Date.Day + "/" + dtpfecha1.Value.Date.Month + "/" + dtpfecha1.Value.Date.Year).ToString() + ", " + "Hasta la Fecha: " + (dtpfecha2.Value.Date.Day + "/" + dtpfecha2.Value.Date.Month + "/" + dtpfecha2.Value.Date.Year).ToString(), FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.NORMAL)));
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
                        doc.Add(new Paragraph("Desde la Fecha: " + (dtpfecha1.Value.Date.Day + "/" + dtpfecha1.Value.Date.Month + "/" + dtpfecha1.Value.Date.Year).ToString() + ", " + "Hasta la Fecha: " + (dtpfecha2.Value.Date.Day + "/" + dtpfecha2.Value.Date.Month + "/" + dtpfecha2.Value.Date.Year).ToString(), FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.NORMAL)));
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
                        doc.Add(new Paragraph("Desde la Fecha: " + (dtpfecha1.Value.Date.Day + "/" + dtpfecha1.Value.Date.Month + "/" + dtpfecha1.Value.Date.Year).ToString() + ", " + "Hasta la Fecha: " + (dtpfecha2.Value.Date.Day + "/" + dtpfecha2.Value.Date.Month + "/" + dtpfecha2.Value.Date.Year).ToString(), FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.NORMAL)));
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
            var listFind = new List<Venta>();
            var isSameDate = dtpfecha2.Value.Date == dtpfecha1.Value.Date;
            var isDiferentWeek = dtpfecha1.Value.Date < DateTime.Today.AddDays(-8);

            if (!isDiferentWeek)
            {
                TempData.tempSalesData = clsGenericList.listVentas.OrderBy(x => x.IdVenta).ToList();
            }
            else
            {
                var (date1, date2) = GetDaysToFilter(isSameDate, dtpfecha1.Value.Date, dtpfecha2.Value.Date);
                var isOnTempData = TempData.tempSalesData.Count > 0 ? TempData.tempSalesData.Where(x => isSameDate
                                                              ? x.FechaVenta.Date == date1 && x.FechaVenta.Date < date2
                                                              : x.FechaVenta.Date >= date1 && x.FechaVenta.Date <= date2)
                                                              .ToList().Count > 0
                                                          : false;

                var searchOnDB = isDiferentWeek || !isOnTempData;
                if (searchOnDB)
                {
                    clsVentas V = new clsVentas();
                    listFind = V.GetListadoVentas(date1, date2);
                }
                else
                {
                    listFind = TempData.tempSalesData.Where(x => isSameDate
                                                              ? x.FechaVenta.Date == date1 && x.FechaVenta.Date < date2
                                                              : x.FechaVenta.Date >= date1 && x.FechaVenta.Date <= date2)
                                                              .ToList();
                }
            }

            var newlist = new List<Venta>();
            if (!string.IsNullOrWhiteSpace(txtBuscarid.Text))
            {
                if (cbtipodocumento.Checked == true && cbPendiente.Checked == false)
                {
                    newlist = listFind.Where(x => x.TipoDocumento == combo_tipo_NCF.Text && x.borrador == borrado && x.NombreCliente.ToLower().Contains(txtBuscarid.Text.ToLower())).ToList();
                    llenar_data(newlist.OrderBy(x => x.IdVenta).ToList());
                }
                else if (cbtipodocumento.Checked == true && cbPendiente.Checked == true)
                {
                    newlist = listFind.Where(x => x.TipoDocumento == combo_tipo_NCF.Text && x.borrador == borrado && x.NombreCliente.ToLower().Contains(txtBuscarid.Text.ToLower()) && x.Restante > 0).ToList();
                    llenar_data(newlist.OrderBy(x => x.IdVenta).ToList());
                }
                else if (cktipofactura.Checked == true && cbPendiente.Checked == false)
                {
                    newlist = listFind.Where(x => x.Tipofactura == cbtipofactura.Text && x.borrador == borrado && x.NombreCliente.ToLower().Contains(txtBuscarid.Text.ToLower())).ToList();
                    llenar_data(newlist.OrderBy(x => x.IdVenta).ToList());
                }
                else if (cktipofactura.Checked == true && cbPendiente.Checked == true)
                {
                    newlist = listFind.Where(x => x.Tipofactura == cbtipofactura.Text && x.Restante > 0 && x.borrador == borrado && x.NombreCliente.ToLower().Contains(txtBuscarid.Text.ToLower())).ToList();
                    llenar_data(newlist.OrderBy(x => x.IdVenta).ToList());
                }
                else if (cbPendiente.Checked == true)
                {
                    newlist = listFind.Where(x => x.borrador == borrado && x.NombreCliente.ToLower().Contains(txtBuscarid.Text.ToLower()) && x.Restante > 0).ToList();
                    llenar_data(newlist.OrderBy(x => x.IdVenta).ToList());
                }
                else
                {
                    newlist = listFind.Where(x => x.borrador == borrado && x.NombreCliente.ToLower().Contains(txtBuscarid.Text.ToLower())).ToList();
                    llenar_data(newlist.OrderBy(x => x.IdVenta).ToList());
                }
            }
            else
            {
                if (cbtipodocumento.Checked == true && cbPendiente.Checked == false)
                {
                    newlist = listFind.Where(x => x.TipoDocumento == combo_tipo_NCF.Text && x.borrador == borrado).ToList();
                    llenar_data(newlist.OrderBy(x => x.IdVenta).ToList());
                }
                else if (cbtipodocumento.Checked == true && cbPendiente.Checked == true)
                {
                    newlist = listFind.Where(x => x.TipoDocumento == combo_tipo_NCF.Text && x.borrador == borrado && x.Restante > 0).ToList();
                    llenar_data(newlist.OrderBy(x => x.IdVenta).ToList());
                }
                else if (cktipofactura.Checked == true && cbPendiente.Checked == false)
                {
                    newlist = listFind.Where(x => x.Tipofactura == cbtipofactura.Text && x.borrador == borrado).ToList();
                    llenar_data(newlist.OrderBy(x => x.IdVenta).ToList());
                }
                else if (cktipofactura.Checked == true && cbPendiente.Checked == true)
                {
                    newlist = listFind.Where(x => x.Tipofactura == cbtipofactura.Text && x.Restante > 0 && x.borrador == borrado).ToList();
                    llenar_data(newlist.OrderBy(x => x.IdVenta).ToList());
                }
                else if (cbPendiente.Checked == true)
                {
                    newlist = listFind.Where(x => x.borrador == borrado && x.Restante > 0).ToList();
                    llenar_data(newlist.OrderBy(x => x.IdVenta).ToList());
                }
                else
                {
                    newlist = listFind.Where(x => x.borrador == borrado).ToList();
                    llenar_data(newlist.OrderBy(x => x.IdVenta).ToList());
                }
            }

            var newlistVentasPorCategoria = clsGenericList.ListaPorCatergoria(dtpfecha1.Value.Date, dtpfecha2.Value.Date, borrado);
            llenar_categoryandquantity(newlistVentasPorCategoria);

            List<int> ventasIds = new List<int>();
            newlist.ForEach(x => ventasIds.Add(x.IdVenta));
            var ganancias = clsGenericList.Ganancias(ventasIds);
            GananciaTotal(ganancias);

            if (cbPendiente.Checked == true)
            {
                label7.Visible = true;
                txttotalpendiente.Visible = true;

                var totalpendiente = newlist.Sum(x => x.Restante);
                txttotalpendiente.Text = Math.Round(totalpendiente, 2).ToString("C2");
            }

            TempData.tempSalesData = newlist;
            TempData.DateIn = DateTime.Now;
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
            TempData.tempSalesData = new List<Venta>();
            llenar_data(clsGenericList.listVentas.OrderBy(x => x.IdVenta).ToList());
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

        private void txtBuscarid_KeyUp(object sender, KeyEventArgs e)
        {
            if (chkid.Checked && chknombre.Checked == false)
            {
                if (txtBuscarid.Text.Length >= 1 && cktipofactura.Checked == false && cbtipodocumento.Checked == false)
                {
                    int id = Convert.ToInt32(txtBuscarid.Text);
                    var newlist = TempData.tempSalesData.Where(x => x.IdVenta == id).ToList();
                    llenar_data(newlist.OrderBy(x => x.IdVenta).ToList());
                }
            }
            else if (chknombre.Checked && chkid.Checked == false)
            {
                if (txtBuscarid.Text.Length >= 4 && cktipofactura.Checked == false && cbtipodocumento.Checked == false)
                {
                    string name = txtBuscarid.Text;
                    var newlist = TempData.tempSalesData.Where(x => x.NombreCliente.ToLower().Contains(name.ToLower())).ToList();
                    llenar_data(newlist.OrderBy(x => x.IdVenta).ToList());
                }
            }
            else
            {
                llenar_data(TempData.tempSalesData.OrderBy(x => x.IdVenta).ToList());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            M.Desconectar();
            Program.Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
            Program.tipo = dataGridView1.CurrentRow.Cells["Tipo"].Value.ToString();
            if (Program.Id > 0)
            {
                string sql = "select DetalleVenta.Cantidad,DetalleVenta.IdProducto, Venta.TipoFactura,DetalleVenta.SubTotal,Caja.id_caja,Venta.Restante,Venta.Total from DetalleVenta" +
                " inner join Venta on DetalleVenta.IdVenta= Venta.IdVenta inner join Caja on Caja.fecha=Venta.FechaVenta where DetalleVenta.IdVenta=" + Program.Id;
                SqlCommand cmd1 = new SqlCommand(sql, M.conexion);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(dt);

                var i = 1;
                foreach (DataRow data in dt.Rows)
                {
                    SqlCommand sqlCommand = new SqlCommand("DevolucionVenta", M.conexion);
                    using (SqlCommand cmd3 = sqlCommand)
                    {
                        cmd3.CommandType = CommandType.StoredProcedure;

                        decimal cantidadDV = Convert.ToDecimal(data[0]);
                        int idProductoDV = Convert.ToInt32(data[1]);
                        string tipofacturaDV = data[2].ToString().ToLower();
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

                        M.Conectar();
                        cmd3.ExecuteNonQuery();
                        M.Desconectar();

                        if (i == dt.Rows.Count)
                        {
                            if (restanteDV != TotalDV)
                            {
                                SqlCommand sqlCommand2 = new SqlCommand("DevolucionVenta", M.conexion);
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

                                    M.Conectar();
                                    cmd4.ExecuteNonQuery();
                                    M.Desconectar();
                                }
                            }

                            decimal caja = 0, monto = 0, montoingresos = 0;
                            string sql1 = "select * FROM pagos where idVenta=" + Program.Id;

                            M.Conectar();
                            SqlCommand cmd2 = new SqlCommand(sql1, M.conexion);
                            SqlDataReader reade = cmd2.ExecuteReader();
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
                            M.Desconectar();

                            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Eliminar esta Venta?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                            {
                                using (SqlCommand cmd = new SqlCommand("eliminarVenta", M.conexion))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Program.Id;
                                    cmd.Parameters.Add("@monto", SqlDbType.Decimal).Value = monto;
                                    cmd.Parameters.Add("@ingresos", SqlDbType.Decimal).Value = montoingresos;
                                    cmd.Parameters.Add("@idCaja", SqlDbType.Int).Value = caja;
                                    cmd.Parameters.Add("@tipoFactura", SqlDbType.NVarChar).Value = Program.tipo;

                                    M.Conectar();
                                    cmd.ExecuteNonQuery();
                                    M.Desconectar();

                                    if (clsGenericList.idsVentas.Contains(Program.Id))
                                    {
                                        var venta = clsGenericList.listVentas.FirstOrDefault(x => x.IdVenta == Program.Id);
                                        clsGenericList.listVentas.Remove(venta);
                                    }

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

                button3.Enabled = Program.isAdminUser;
                button4.Enabled = Program.isAdminUser;
            }
        }

        private void vereliminadas_CheckedChanged(object sender, EventArgs e)
        {
            if (vereliminadas.Checked)
            {
                borrado = 1;
                var newlist = TempData.tempSalesData.Where(x => x.borrador == borrado).ToList();
                llenar_data(newlist.OrderBy(x => x.IdVenta).ToList());
            }
            else
            {
                borrado = 0;
                var newlist = TempData.tempSalesData.Where(x => x.borrador == borrado).ToList();
                llenar_data(newlist.OrderBy(x => x.IdVenta).ToList());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            M.Desconectar();
            //devolucion de venta
            if (DevComponents.DotNetBar.MessageBoxEx.Show("Nota: se devolveran todos los producto que contenga la venta y la misma se eliminara por completo del sistema" +
                           "\n ¿Está Seguro que Desea hacer una devolucion de esta Venta? ", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                Program.Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());

                M.Conectar();
                string sql = "select DetalleVenta.Cantidad,DetalleVenta.IdProducto, Venta.TipoFactura,DetalleVenta.SubTotal,Caja.id_caja,Venta.Restante,Venta.Total from DetalleVenta" +
                    " inner join Venta on DetalleVenta.IdVenta= Venta.IdVenta inner join Caja on Caja.fecha=Venta.FechaVenta where DetalleVenta.IdVenta=" + Program.Id;
                SqlCommand cmd1 = new SqlCommand(sql, M.conexion);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(dt);

                var i = 1;
                foreach (DataRow data in dt.Rows)
                {
                    SqlCommand sqlCommand = new SqlCommand("DevolucionVenta", M.conexion);
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

                        var deleteProduct = clsGenericList.listProducto.FirstOrDefault(x => x.m_IdP == idProductoDV);
                        var updateProduct = clsGenericList.listProducto.FirstOrDefault(x => x.m_IdP == idProductoDV);
                        updateProduct.m_Stock = updateProduct.m_Stock + cantidadDV;

                        clsGenericList.listProducto.Remove(deleteProduct);
                        clsGenericList.listProducto.Add(updateProduct);

                        cmd3.ExecuteNonQuery();

                        if (i == dt.Rows.Count)
                        {
                            if (restanteDV != TotalDV)
                            {
                                SqlCommand sqlCommand2 = new SqlCommand("DevolucionVenta", M.conexion);
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

                                    var deleteProduct1 = clsGenericList.listProducto.FirstOrDefault(x => x.m_IdP == idProductoDV1);
                                    var updateProduct1 = clsGenericList.listProducto.FirstOrDefault(x => x.m_IdP == idProductoDV1);
                                    updateProduct1.m_Stock = updateProduct1.m_Stock + cantidadDV1;

                                    clsGenericList.listProducto.Remove(deleteProduct1);
                                    clsGenericList.listProducto.Add(updateProduct1);

                                    cmd4.ExecuteNonQuery();
                                }
                            }

                            SqlCommand sqlCommand1 = new SqlCommand("BorrarVentaDV", M.conexion);
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

                    var venta = clsGenericList.listVentas.FirstOrDefault(x => x.IdVenta == Program.Id);
                    clsGenericList.listVentas.Remove(venta);
                }

                M.Desconectar();
            }
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

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void frmListadoVentas_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void txtBuscarid_KeyPress_1(object sender, KeyPressEventArgs e)
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
        private (DateTime, DateTime) GetDaysToFilter(bool isSameDate, DateTime date1, DateTime date2)
        {
            if (isSameDate)
            {
                return (date1.Date, date2.Date.AddDays(1));
            }

            return (date1.Date, date2.Date);
        }
    }
}

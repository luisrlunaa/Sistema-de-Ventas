using CapaEnlaceDatos;
using CapaLogicaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class frmCotizar : DevComponents.DotNetBar.Metro.MetroForm
    {
        public class PrecioCompraProducto
        {
            public int ID;
            public decimal Precio;
        }

        private List<clsVentas> lst = new List<clsVentas>();
        private List<PrecioCompraProducto> listProducts = new List<PrecioCompraProducto>();
        clsManejador M = new clsManejador();

        public frmCotizar()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FrmListadoClientes C = new FrmListadoClientes();
            Program.whoCallme = "Cotizar";
            C.Show();
        }

        bool activar;
        private void frmCotizar_Load(object sender, EventArgs e)
        {
            txtidCli.Text = null;
            Program.IdCliente = 0;
            cbidentificacion.Checked = false;

            if (cbidentificacion.Checked == true)
            {
                btnBuscar.Show();
                txtDatos.ReadOnly = true;
            }
            else
            {
                btnBuscar.Hide();
                txtDatos.ReadOnly = false;
            }

            button2.Show();
            button3.Hide();

            if (Program.CargoEmpleadoLogueado != "Administrador")
            {
                txtPVenta.Enabled = false;
                txtIgv.Enabled = false;
                txtDivisor.Enabled = false;
                txtPorcentaje.Enabled = false;
            }

            txtDivisor.Text = "1.18";
            txtPorcentaje.Text = "";
            Program.ReImpresion = "";
            Program.datoscliente = "";
            btnAgregar.Visible = false;
            btnEliminarItem.Enabled = false;

            llenar();
            Limpiar1();
        }

        public void llenar()
        {
            M.Desconectar();
            string cadSql = "select top(1) IdCotizacion from Cotizacion order by IdCotizacion desc";
            M.Conectar();

            SqlCommand comando = new SqlCommand(cadSql, M.conexion);

            SqlDataReader leer = comando.ExecuteReader();

            int varcodigo;

            if (leer.Read() == true)
            {
                varcodigo = Convert.ToInt32(leer["IdCotizacion"]) + 1;
                txtIdVenta.Text = varcodigo.ToString();
            }
            else
            {
                txtIdVenta.Text = "1";
            }
            M.Desconectar();
        }

        private void btnBusquedaProducto_Click(object sender, EventArgs e)
        {
            if (Program.abiertosecundarias == false)
            {
                FrmListadoProductos P = new FrmListadoProductos();
                btnAgregar.Visible = true;
                Program.datoscliente = txtDatos.Text;
                Program.abiertosecundarias = true;
                Program.whoCallme = "Cotizar";
                P.Show();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsVentas V = new clsVentas();
            PrecioCompraProducto PCP = new PrecioCompraProducto();
            decimal precio = 0;
            decimal itbis = 0;

            if (txtPorcentaje.Text.Trim() != "" && txtPVenta.Text.Trim() != "")
            {
                decimal precioreal = Convert.ToDecimal(txtPVenta.Text);
                decimal porcentaje = Convert.ToInt32(txtPorcentaje.Text);
                decimal valortotalporcentaje = 100;
                decimal divisor = Convert.ToDecimal(txtDivisor.Text);

                decimal calculoporcentaje = Math.Round(porcentaje / valortotalporcentaje, 2);

                precio = Math.Round(precioreal / divisor, 2);
                itbis = Math.Round(precio * calculoporcentaje, 2);
            }
            else
            {
                precio = Convert.ToDecimal(txtPVenta.Text);
                itbis = Convert.ToDecimal(txtIgv.Text);
            }

            if (txtDescripcion.Text.Trim() != "")
            {
                if (txtCantidad.Text.Trim() != "")
                {
                    if (Convert.ToInt32(txtCantidad.Text) >= 0)
                    {
                        if (Convert.ToInt32(txtCantidad.Text) <= Convert.ToInt32(txtStock.Text))
                        {
                            V.IdProducto = Convert.ToInt32(txtIdProducto.Text);
                            V.IdVenta = Convert.ToInt32(txtIdVenta.Text);
                            V.Descripcion = (txtDescripcion.Text + "-" + txtMarca.Text).Trim();
                            V.Cantidad = Convert.ToInt32(txtCantidad.Text);
                            V.PrecioCompra = Program.PrecioCompra;
                            V.Igv = itbis;
                            V.PrecioVenta = precio;
                            V.SubTotal = Math.Round((precio + itbis) * Convert.ToInt32(txtCantidad.Text), 2);
                            btnAgregar.Visible = false;
                            lst.Add(V);

                            PCP.ID = Convert.ToInt32(txtIdProducto.Text);
                            PCP.Precio = Program.PrecioCompra;
                            listProducts.Add(PCP);

                            LlenarGri();

                            if (cbidentificacion.Checked == false && txtDatos.Text != "" && Program.IdCliente == 0)
                            {
                                txtDocIdentidad.Text = "Sin identificación";
                                txtDatos.Text = Program.datoscliente;
                            }

                            Limpiar();
                        }
                        else
                        {
                            DevComponents.DotNetBar.MessageBoxEx.Show("Stock Insuficiente para Realizar la Venta.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show("Cantidad Ingresada no Válida.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        txtCantidad.Clear();
                        txtCantidad.Focus();
                    }
                }
                else
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Cantidad a Vender.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    txtCantidad.Focus();
                }
            }
            else
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor buscar un Producto.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }
        private void LlenarGri()
        {
            decimal SumaSubTotal = 0; decimal SumaIgv = 0; decimal SumaTotal = 0;
            dgvVenta.Rows.Clear();
            for (int i = 0; i < lst.Count; i++)
            {
                dgvVenta.Rows.Add();
                dgvVenta.Rows[i].Cells["idD"].Value = lst[i].IdVenta;
                dgvVenta.Rows[i].Cells["DescripcionP"].Value = lst[i].Descripcion;
                dgvVenta.Rows[i].Cells["cantidadP"].Value = lst[i].Cantidad;
                dgvVenta.Rows[i].Cells["IGV"].Value = lst[i].Igv;
                dgvVenta.Rows[i].Cells["PrecioU"].Value = lst[i].PrecioVenta;
                dgvVenta.Rows[i].Cells["SubtoTal"].Value = lst[i].SubTotal;
                dgvVenta.Rows[i].Cells["IDP"].Value = lst[i].IdProducto;

                var preciounidad = Convert.ToDecimal(dgvVenta.Rows[i].Cells["PrecioU"].Value);
                var cantidad = Convert.ToInt32(dgvVenta.Rows[i].Cells["cantidadP"].Value);
                var igv = Convert.ToDecimal(dgvVenta.Rows[i].Cells["IGV"].Value);

                SumaSubTotal += preciounidad * cantidad;
                SumaIgv += igv * cantidad;

                SumaTotal += Math.Round(Convert.ToDecimal(dgvVenta.Rows[i].Cells["SubtoTal"].Value), 2);

                lblsubt.Text = Convert.ToString(SumaSubTotal);
                lbligv.Text = Convert.ToString(SumaIgv);
                txttotal.Text = Convert.ToString(SumaTotal);

                Program.igv = Convert.ToDecimal(lbligv.Text);
                Program.ST = Convert.ToDecimal(lblsubt.Text);
                Program.total = Convert.ToDecimal(txttotal.Text);
                Program.PrecioCompra = 0;
            }
        }
        private void btnEliminarItem_Click(object sender, EventArgs e)
        {
            List<clsVentas> lista = new List<clsVentas>();

            Program.IdProducto = Convert.ToInt32(dgvVenta.CurrentRow.Cells["IDP"].Value.ToString());
            if (Program.IdProducto > 0)
            {
                decimal Igv = 0;
                decimal subtotal = 0;
                decimal SumaSubTotal = 0;
                decimal SumaIgv = 0;
                decimal SumaTotal = 0;

                foreach (var item in lst)
                {
                    if (item.IdProducto != Program.IdProducto)
                    {
                        lista.Add(item);

                        Igv = item.Igv;
                        subtotal = item.SubTotal;

                        SumaSubTotal += subtotal;
                        SumaIgv += Igv;
                        SumaTotal = SumaSubTotal + SumaIgv;
                    }
                    else
                    {
                        lista.Remove(item);
                    }

                    lblsubt.Text = Convert.ToString(SumaSubTotal);
                    lst = lista;
                }

                btnEliminarItem.Enabled = false;
                dgvVenta.Rows.RemoveAt(dgvVenta.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Por Favor Seleccione un producto antes de eliminarlo");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Limpiar();
            Limpiar1();

            button2.Show();
            button3.Hide();
        }

        private void Limpiar1()
        {
            cbidentificacion.Visible = true;
            cbidentificacion.Checked = false;
            Program.Esabono = "";
            txtIgv.Clear();
            txtDocIdentidad.Clear();
            txtDatos.Clear();
            dgvVenta.Rows.Clear();
            Program.IdCliente = 0;
            Program.pagoRealizado = 0;
            Program.DocumentoIdentidad = "";
            Program.ApellidosCliente = "";
            Program.NombreCliente = "";
            Program.Id = 0;
            Program.total = 0;
            Program.ST = 0;
            Program.igv = 0;
            Program.fecha = "";
            Program.NroComprobante = "";
            Program.NCF = "";
            lst.Clear();
            txtIgv.Text = "";
            Program.realizopago = false;
            Program.ReImpresion = "";
            Program.datoscliente = "";
        }
        private void Limpiar()
        {
            txtDescripcion.Clear();
            txtMarca.Clear();
            txtStock.Clear();
            txtPVenta.Clear();
            txtCantidad.Clear();
            txtCantidad.Focus();
            txtIgv.Text = "";
            txtPorcentaje.Text = "";
            Program.Descripcion = "";
            Program.Stock = 0;
            Program.Marca = "";
            Program.PrecioVenta = 0;
            Program.IdProducto = 0;
            Program.itbis = 0;
            Program.realizopago = false;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dgvVenta.Rows.Count > 0)
            {
                if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Que tipo de Cotizacion desea? \n Si=Pequeña \n No=Grande ", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    tickEstilo();
                }
                else
                {
                    To_pdf();
                }
            }
            else
            {
                MessageBox.Show("Debe Agregar al menos un producto para poder imprimir la cotizacion");
            }

            Program.whoCallme = "Cotizar";
            Limpiar1();
        }

        public void tickEstilo()
        {
            string nombre = "";
            string cedula = "";
            CrearTiket ticket = new CrearTiket();

            //cabecera del ticket.
            ticket.TextoDerecha("COTIZACION");

            //System.Drawing.Image img = System.Drawing.Image.FromFile("LogoCepeda.png");
            //ticket.HeaderImage = img;
            ticket.TextoCentro(lblLogo.Text);
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda(lblDir.Text);
            ticket.TextoIzquierda("Tel: " + lblTel1.Text + "/" + lblTel2.Text);
            ticket.TextoIzquierda("Correo: " + lblCorreo.Text);
            ticket.TextoIzquierda("RNC: " + lblrnc.Text);
            ticket.lineasGuio();

            if (Program.datoscliente != "" && Program.IdCliente == 0)
            {
                nombre = Program.datoscliente;
                cedula = "Sin Identificación";
            }
            else
            {
                nombre = txtDatos.Text;
                cedula = txtDocIdentidad.Text;
            }

            //SUB CABECERA.
            ticket.TextoIzquierda("Atendido Por: " + txtUsu.Text);
            ticket.TextoIzquierda("Cliente: " + nombre);
            ticket.TextoIzquierda("Documento de Identificación: " + cedula);
            ticket.TextoIzquierda("Fecha: " + DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year);

            //ARTICULOS A VENDER.
            ticket.EncabezadoVenta();// NOMBRE DEL ARTICULO, CANT, PRECIO, IMPORTE
            ticket.lineasGuio();

            //SI TIENE UN DATAGRIDVIEW DONDE ESTAN SUS ARTICULOS A VENDER PUEDEN USAR ESTA MANERA PARA AGREARLOS
            foreach (DataGridViewRow fila in dgvVenta.Rows)
            {
                ticket.AgregaArticulo((fila.Cells["DescripcionP"].Value.ToString()).Trim(), int.Parse((fila.Cells["cantidadP"].Value.ToString()).Trim()),
                decimal.Parse((fila.Cells["SubtoTal"].Value.ToString()).Trim()), decimal.Parse((fila.Cells["IGV"].Value.ToString()).Trim()));
            }
            ticket.TextoIzquierda(" ");

            //resumen de la venta
            ticket.AgregarTotales("SUB-TOTAL    : ", decimal.Parse(lblsubt.Text));
            ticket.AgregarTotales("ITBIS     : ", decimal.Parse(lbligv.Text));
            ticket.AgregarTotales("TOTAL A PAGAR    : ", decimal.Parse(txttotal.Text));

            ticket.TextoIzquierda(" ");
            ticket.TextoCentro("__________________________________");

            //TEXTO FINAL DEL TICKET
            ticket.TextoIzquierda("EXTRA");
            ticket.TextoIzquierda("FAVOR REVISE SU MERCANCIA AL RECIBIRLA");
            ticket.TextoCentro("!GRACIAS POR SU COMPRA!");
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
            ticket.ImprimirTicket("POS-80");//NOMBRE DE LA IMPRESORA
        }

        private void To_pdf()
        {
            string nombre = "";
            string cedula = "";
            Document doc = new Document(PageSize.LETTER, 10f, 10f, 10f, 0f);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            Image image1 = Image.GetInstance("LogoCepeda.png");
            image1.ScaleAbsoluteWidth(140);
            image1.ScaleAbsoluteHeight(70);
            saveFileDialog1.InitialDirectory = @"C:";
            saveFileDialog1.Title = "Factura para " + txtDatos.Text;
            saveFileDialog1.DefaultExt = "pdf";
            saveFileDialog1.Filter = "pdf Files (*.pdf)|*.pdf| All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            string filename = "Venta" + DateTime.Now.ToString();
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
                    if (Program.ReImpresion != null)
                    {
                        doc.Add(new Paragraph("                                                                                                                                                                                                                                                                                                                                                                                            " + Program.ReImpresion, FontFactory.GetFont("ARIAL", 5, iTextSharp.text.Font.ITALIC, color: BaseColor.RED)));
                    }

                    if (Program.datoscliente != "" && Program.IdCliente == 0)
                    {
                        nombre = Program.datoscliente;
                        cedula = "Sin Identificación";
                    }
                    else
                    {
                        nombre = txtDatos.Text;
                        cedula = txtDocIdentidad.Text;
                    }

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
                    var cotizacion = new Paragraph("COTIZACION", FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.NORMAL));
                    cotizacion.Alignment = Element.ALIGN_CENTER;
                    doc.Add(cotizacion);
                    var telefonos = new Paragraph("Tel: " + lblTel1.Text + " / " + lblTel2.Text, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL));
                    telefonos.Alignment = Element.ALIGN_CENTER;
                    doc.Add(telefonos);
                    var RNC = new Paragraph("RNC: " + lblrnc.Text, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL));
                    RNC.Alignment = Element.ALIGN_CENTER;
                    doc.Add(RNC);

                    doc.Add(new Paragraph(" "));
                    doc.Add(new Paragraph("Atendido por: " + txtUsu.Text, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("Cliente: " + nombre, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("Documento de Identificación: " + cedula, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph(" "));
                    GenerarDocumento(doc);
                    doc.AddCreationDate();
                    if (dgvVenta.Rows.Count >= 1)
                    {
                        int filas = 20 - dgvVenta.Rows.Count;
                        if (filas > 1)
                        {
                            for (int i = 1; i < filas; i++)
                            {
                                doc.Add(new Paragraph("                       "));
                            }
                        }
                    }

                    decimal Sub = Convert.ToDecimal(lblsubt.Text);
                    decimal ITBIS = Convert.ToDecimal(lbligv.Text);
                    decimal total = Convert.ToDecimal(txttotal.Text);

                    doc.Add(new Paragraph("Sub-Total   : " + Sub.ToString("C2"), FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("ITBIS   : " + ITBIS.ToString("C2"), FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("Total a Pagar   : " + total.ToString("C2"), FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));

                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("_________________________" + "                                                                                                                                                 " + "_________________________", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("      Facturado Por      " + "                                                                                                                                                                         " + "     Recibido Por  ", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("                       "));

                    var Nota = new Paragraph("Nota: Los productos con garantia pierden su garantia si deja perder la factura.", FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.ITALIC, color: BaseColor.RED));
                    Nota.Alignment = Element.ALIGN_RIGHT;
                    var favor = new Paragraph("FAVOR REVISE SU MERCANCIA AL RECIBIRLA", FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.ITALIC, color: BaseColor.RED));
                    favor.Alignment = Element.ALIGN_RIGHT;
                    var gracias = new Paragraph("!GRACIAS POR SU COMPRA!", FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.ITALIC, color: BaseColor.RED));
                    gracias.Alignment = Element.ALIGN_RIGHT;

                    doc.Add(Nota);
                    doc.Add(favor);
                    doc.Add(gracias);

                    doc.Close();
                    Process.Start(filename);//Esta parte se puede omitir, si solo se desea guardar el archivo, y que este no se ejecute al instante
                }
            }
            else
            {
                MessageBox.Show("No guardo el Archivo");
            }
        }

        private void txtPVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Program.abiertosecundarias = false;
            Program.abierto = false;
            btnImprimir.Visible = false;
            btnAgregar.Visible = false;
            Program.pagoRealizado = 0;
            Limpiar();
            Limpiar1();

            if (Program.CargoEmpleadoLogueado != "Administrador")
            {
                txtPVenta.Enabled = false;
                txtIgv.Enabled = false;
                txtDivisor.Enabled = false;
                txtPorcentaje.Enabled = false;
            }

            this.Close();
        }

        private void dgvVenta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVenta.Rows.Count > 0)
            { dgvVenta.Rows[dgvVenta.CurrentRow.Index].Selected = true; }
        }

        public void GenerarDocumento(Document document)
        {
            int i, j;
            PdfPTable datatable = new PdfPTable(dgvVenta.ColumnCount);
            float[] headerwidths = GetTamañoColumnas(dgvVenta);
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 100;
            datatable.DefaultCell.BorderWidth = 1;
            datatable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            for (i = 0; i < dgvVenta.ColumnCount; i++)
            {
                datatable.AddCell(new Phrase(dgvVenta.Columns[i].HeaderText, FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD)));
            }
            datatable.HeaderRows = 1;
            datatable.DefaultCell.BorderWidth = 1;
            for (i = 0; i < dgvVenta.Rows.Count; i++)
            {
                for (j = 0; j < dgvVenta.Columns.Count; j++)
                {
                    if (dgvVenta[j, i].Value != null)
                    {
                        datatable.AddCell(new Phrase(dgvVenta[j, i].Value.ToString(), FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));//En esta parte, se esta agregando un renglon por cada registro en el datagrid
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

        private void dgvVenta_Click(object sender, EventArgs e)
        {
            btnEliminarItem.Enabled = true;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void cbidentificacion_CheckedChanged(object sender, EventArgs e)
        {
            if (cbidentificacion.Checked == true)
            {
                btnBuscar.Show();
                txtDatos.ReadOnly = true;
                cbidentificacion.Visible = false;
            }
            else
            {
                btnBuscar.Hide();
                txtDatos.ReadOnly = false;
            }
        }

        private void frmCotizar_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void frmCotizar_Activated(object sender, EventArgs e)
        {
            if (Program.whoCallme == "Cotizar")
            {
                if (Program.IdCliente != 0)
                {
                    txtDatos.Text = Program.NombreCliente + " " + Program.ApellidosCliente;
                    txtidCli.Text = Program.IdCliente + "";
                    txtDocIdentidad.Text = Program.DocumentoIdentidad;
                }
                else
                {
                    if (txtDatos.Text != null && txtDatos.Text != "")
                    {
                        Program.datoscliente = txtDatos.Text;

                    }
                    if (Program.datoscliente != null && Program.datoscliente != "")
                    {
                        txtDatos.Text = Program.datoscliente;
                    }
                }

                txtIdProducto.Text = Program.IdProducto + "";
                txtDescripcion.Text = Program.Descripcion;
                txtMarca.Text = Program.Marca;
                txtStock.Text = Program.Stock + "";
                txtPVenta.Text = Program.PrecioVenta + "";
                txtIgv.Text = Program.itbis + "";
                txttotal.Text = Program.total + "";
                lblsubt.Text = Program.ST + "";
                lbligv.Text = Program.igv + "";

                if (Program.Id == 0)
                {
                    button2.Show();
                    button3.Hide();
                    activar = false;
                }
                else
                {
                    button2.Hide();
                    button3.Show();
                    activar = true;
                }

                if (activar == true)
                {
                    txtIdV.Text = Program.Id + "";
                    txttotal.Text = Program.total + "";
                    lblsubt.Text = Program.ST + "";
                    lbligv.Text = Program.igv + "";
                    txtidEmp.Text = Program.IdEmpleado + "";

                    decimal subtotal = 0;
                    decimal igv = 0;

                    M.Desconectar();
                    SqlCommand comando = new SqlCommand();
                    //variable SqlDataReader para leer los datos
                    SqlDataReader dr;
                    comando.Connection = M.conexion;
                    //declaramos el comando para realizar la busqueda
                    comando.CommandText = "SELECT * from DetalleCotizacion WHERE DetalleCotizacion.IdCotizacion ='" + txtIdV.Text + "'";
                    //especificamos que es de tipo Text
                    comando.CommandType = CommandType.Text;
                    //se abre la conexion
                    M.Conectar();
                    //limpiamos los renglones de la datagridview
                    dgvVenta.Rows.Clear();
                    //a la variable DataReader asignamos  el la variable de tipo SqlCommand
                    dr = comando.ExecuteReader();
                    while (dr.Read())
                    {
                        //variable de tipo entero para ir enumerando los la filas del datagridview
                        int renglon = dgvVenta.Rows.Add();
                        // especificamos en que fila se mostrará cada registro
                        // nombredeldatagrid.filas[numerodefila].celdas[nombrdelacelda].valor=\

                        string idVenta = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdCotizacion")));
                        if (idVenta == txtIdV.Text)
                        {
                            dgvVenta.Rows[renglon].Cells["IdD"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdCotizacion")));
                            dgvVenta.Rows[renglon].Cells["cantidadP"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("Cantidad")));
                            dgvVenta.Rows[renglon].Cells["DescripcionP"].Value = dr.GetString(dr.GetOrdinal("detalles_P"));
                            dgvVenta.Rows[renglon].Cells["PrecioU"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("PrecioUnitario")));
                            dgvVenta.Rows[renglon].Cells["SubtoTal"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("SubTotal")));
                            dgvVenta.Rows[renglon].Cells["IDP"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdProducto")));
                            dgvVenta.Rows[renglon].Cells["IGV"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("Igv")));

                            subtotal += (dr.GetDecimal(dr.GetOrdinal("PrecioUnitario")) * dr.GetInt32(dr.GetOrdinal("Cantidad")));
                            igv += (dr.GetDecimal(dr.GetOrdinal("Igv")) * dr.GetInt32(dr.GetOrdinal("Cantidad")));
                        }
                    }

                    Program.ST = subtotal;
                    Program.igv = igv;

                    M.Desconectar();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            M.Desconectar();
            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Que tipo de Cotizacion desea? \n Si=Pequeña \n No=Grande ", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                tickEstilo();
            }
            else
            {
                To_pdf();
            }

            Program.whoCallme = "Cotizar";

            string procedure = "";

            if (Program.IdCliente > 0)
            {
                procedure = "RegistrarCotizacion";
            }
            else
            {
                procedure = "RegistrarCotizacionsinIDcliente";
            }

            using (SqlCommand cmd = new SqlCommand(procedure, M.conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                //tabla Ventas
                if (Program.IdCliente != 0)
                {
                    cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = Convert.ToInt32(txtidCli.Text);
                    cmd.Parameters.Add("@NombreCliente", SqlDbType.VarChar).Value = txtDatos.Text;
                }
                else
                {
                    cmd.Parameters.Add("@NombreCliente", SqlDbType.VarChar).Value = Program.datoscliente;
                }

                cmd.Parameters.Add("@IdCotizacion", SqlDbType.Int).Value = Convert.ToInt32(txtIdVenta.Text);
                cmd.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = txtidEmp.Text;
                cmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = Convert.ToDecimal(txttotal.Text);

                M.Conectar();
                cmd.ExecuteNonQuery();
                M.Desconectar();
            }

            using (SqlCommand cmd1 = new SqlCommand("RegistrarDetalleCotizacion", M.conexion))
                foreach (DataGridViewRow row in dgvVenta.Rows)
                {
                    cmd1.CommandType = CommandType.StoredProcedure;
                    int idProducto = Convert.ToInt32(row.Cells["IDP"].Value);
                    decimal preciocompra = listProducts.FirstOrDefault(x => x.ID == idProducto).Precio;
                    decimal subtotal = Convert.ToDecimal(row.Cells["SubtoTal"].Value);
                    int cantidad = Convert.ToInt32(row.Cells["cantidadP"].Value);
                    decimal Ganancia = Math.Round(subtotal - (preciocompra * cantidad));

                    //Tabla detalles ventas
                    cmd1.Parameters.Add("@IdCotizacion", SqlDbType.Int).Value = Convert.ToInt32(row.Cells["IdD"].Value);
                    cmd1.Parameters.Add("@Cantidad", SqlDbType.Int).Value = Convert.ToInt32(row.Cells["cantidadP"].Value);
                    cmd1.Parameters.Add("@detalles", SqlDbType.NVarChar).Value = Convert.ToString(row.Cells["DescripcionP"].Value);
                    cmd1.Parameters.Add("@PrecioUnitario", SqlDbType.Float).Value = Convert.ToDouble(row.Cells["PrecioU"].Value);
                    cmd1.Parameters.Add("@SubTotal", SqlDbType.Float).Value = Convert.ToDouble(row.Cells["SubtoTal"].Value);
                    cmd1.Parameters.Add("@IdProducto", SqlDbType.Int).Value = idProducto;
                    cmd1.Parameters.Add("@Igv", SqlDbType.Float).Value = Convert.ToDouble(row.Cells["IGV"].Value);
                    cmd1.Parameters.Add("@GananciaVenta", SqlDbType.Float).Value = Ganancia;

                    M.Conectar();
                    cmd1.ExecuteNonQuery();
                    cmd1.Parameters.Clear();
                    M.Desconectar();
                }
            Program.pagoRealizado = 0;

            Limpiar();
            Limpiar1();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Program.abiertosecundarias == false)
            {
                frmlistadodecotizacion F = new frmlistadodecotizacion();
                F.lblLogo.Text = lblLogo.Text;
                F.lblDir.Text = lblDir.Text;
                F.lbltel.Text = lblTel1.Text;
                F.lbltel1.Text = lblTel2.Text;
                F.lblCorreo.Text = lblCorreo.Text;
                F.lblrnc.Text = lblrnc.Text;
                F.btnCancelar.Visible = false;

                Program.abiertosecundarias = false;
                Program.abierto = false;

                F.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.whoCallme = "Vender Cotizacion";
            Program.itbis = Convert.ToDecimal(txtIgv.Text);
            Program.total = Convert.ToDecimal(txttotal.Text);
            Program.ST = Convert.ToDecimal(lblsubt.Text);
            Program.igv = Convert.ToDecimal(lbligv.Text);

            if (txtidCli.Text != "")
            {
                Program.IdCliente = Convert.ToInt32(txtidCli.Text);
                Program.DocumentoIdentidad = txtDocIdentidad.Text;
            }

            Program.datoscliente = txtDatos.Text;

            FrmRegistroVentas V = new FrmRegistroVentas();
            V.txtUsu.Text = txtUsu.Text;
            V.txtidEmp.Text = Convert.ToString(Program.IdEmpleadoLogueado);
            V.lblLogo.Text = lblLogo.Text;
            V.lblDir.Text = lblDir.Text;
            V.lblTel1.Text = lblTel1.Text;
            V.lblTel2.Text = lblTel2.Text;
            V.lblCorreo.Text = lblCorreo.Text;
            V.lblrnc.Text = lblrnc.Text;
            V.Show();

            Program.abiertosecundarias = false;
            Program.abierto = false;

            this.Close();
        }
    }
}

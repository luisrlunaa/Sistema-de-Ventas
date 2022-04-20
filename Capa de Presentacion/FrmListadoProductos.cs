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
    public partial class FrmListadoProductos : DevComponents.DotNetBar.Metro.MetroForm

    {
        private clsProducto P = new clsProducto();
        private clsCategoria C = new clsCategoria();
        private clsManejador M = new clsManejador();
        public FrmListadoProductos()
        {
            InitializeComponent();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            if (clsGenericList.listProducto is null)
            {
                clsGenericList.listProducto = new List<Producto>();
            }

            textBox5.Text = "";
            id.Text = "";

            cbTipoGoma.Enabled = false;
            cbxCategoria.Enabled = false;
            button2.Enabled = Program.isAdminUser;
            btnEditar.Enabled = Program.isAdminUser;

            clear();
            //repetitivo();
            //Mrepetitivo();

            ListarElementos();
            ListarElementostipo();
        }
        public void buscarid()
        {
            M.Desconectar();
            M.Conectar();
            string sql = "select IdCategoria from Categoria where Descripcion =@id";
            SqlCommand cmd = new SqlCommand(sql, M.conexion);
            cmd.Parameters.AddWithValue("@id", cbxCategoria.Text);

            SqlDataReader reade = cmd.ExecuteReader();
            if (reade.Read())
            {
                id.Text = Convert.ToString(reade["IdCategoria"]);
                txtdesc.Text = cbxCategoria.Text;
                rbbuena.Checked = false;
                rdmedia.Checked = false;
                rbCero.Checked = false;
                rbtodos.Checked = false;
                radioButton1.Checked = false;
            }
            M.Desconectar();
        }

        public void buscardesc()
        {
            M.Desconectar();
            M.Conectar();
            string sql = "select descripcion from tipoGOma where id =@id";
            SqlCommand cmd = new SqlCommand(sql, M.conexion);
            cmd.Parameters.AddWithValue("@id", cbTipoGoma.SelectedValue);
            if (cbTipoGoma.SelectedValue != null)
            {
                SqlDataReader reade = cmd.ExecuteReader();
                if (reade.Read())
                {
                    id.Text = cbTipoGoma.SelectedIndex.ToString();
                    textBox5.Text = Convert.ToString(reade["descripcion"]);
                    rbbuena.Checked = false;
                    rdmedia.Checked = false;
                    rbCero.Checked = false;
                    rbtodos.Checked = false;
                    radioButton1.Checked = false;
                }
            }
            M.Desconectar();
        }

        private void ListarElementostipo()
        {
            if (id.Text.Trim() != "")
            {
                cbTipoGoma.DisplayMember = "descripcion";
                cbTipoGoma.ValueMember = "id";
                cbTipoGoma.DataSource = C.ListarC();
                cbTipoGoma.Text = textBox5.Text;
            }
            else
            {
                cbTipoGoma.DisplayMember = "descripcion";
                cbTipoGoma.ValueMember = "id";
                cbTipoGoma.DataSource = C.ListarC();
            }
        }

        private void ListarElementos()
        {
            if (id.Text.Trim() != "")
            {
                cbxCategoria.DisplayMember = "Descripcion";
                cbxCategoria.ValueMember = "IdCategoria";
                cbxCategoria.DataSource = C.Listar();
                cbxCategoria.SelectedItem = id.Text;
            }
            else
            {
                cbxCategoria.DisplayMember = "Descripcion";
                cbxCategoria.ValueMember = "IdCategoria";
                cbxCategoria.DataSource = C.Listar();
            }

        }

        public void clear()
        {
            id.Clear();
            rbbuena.Checked = false;
            rdmedia.Checked = false;
            rbCero.Checked = false;
            rbtodos.Checked = false;
            radioButton1.Checked = false;
            rbfechaing.Checked = false;
            rbfechamod.Checked = false;
            button2.Enabled = false;
            txtdesc.Text = "";
            GetAllProduct();
            dataGridView1.ClearSelection();
        }

        public void GetAllProduct()
        {
            if (clsGenericList.listProducto.Count > 0)
            {
                CargarListado(clsGenericList.listProducto.OrderBy(x => x.m_IdP).ToList());
            }
            else
            {
                DataTable dt = new DataTable();
                dt = P.Listar();

                try
                {
                    foreach (DataRow reader in dt.Rows)
                    {
                        Producto product = new Producto();

                        product.m_IdP = reader["IdProducto"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdProducto"]);
                        product.m_IdCategoria = reader["IdCategoria"] == DBNull.Value ? 0 : Convert.ToInt32(reader["IdCategoria"]);
                        product.m_Producto = reader["Nombre"] == DBNull.Value ? string.Empty : reader["Nombre"].ToString();
                        product.m_tipoGoma = reader["tipoGOma"] == DBNull.Value ? string.Empty : reader["tipoGOma"].ToString();
                        product.m_itbis = reader["itbis"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["itbis"]);
                        product.m_PrecioVenta = reader["PrecioVenta"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PrecioVenta"]);
                        product.m_PrecioCompra = reader["PrecioCompra"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PrecioCompra"]);
                        product.m_Preciomax = reader["Pmax"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Pmax"]);
                        product.m_Preciomin = reader["Pmin"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Pmin"]);
                        product.m_FechaVencimiento = Convert.ToDateTime(reader["FechaVencimiento"]);
                        product.m_Stock = reader["Stock"] == DBNull.Value ? 0 : Convert.ToInt32(reader["Stock"]);
                        product.m_FechaModificacion = Convert.ToDateTime(reader["FechaModificacion"]);
                        product.m_Marca = reader["Marca"] == DBNull.Value ? string.Empty : reader["Marca"].ToString();

                        clsGenericList.listProducto.Add(product);
                    }

                    CargarListado(clsGenericList.listProducto.OrderBy(x => x.m_IdP).ToList());
                }
                catch (Exception ex)
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show(ex.Message);
                }
            }
        }
        public void CargarListado(List<Producto> listaproductos)
        {
            try
            {
                decimal compras = 0, total = 0, ventas = 0, totalproducto = 0;

                dataGridView1.Rows.Clear();
                foreach (var item in listaproductos)
                {
                    int renglon = dataGridView1.Rows.Add();

                    dataGridView1.Rows[renglon].Cells[0].Value = item.m_IdP.ToString();
                    dataGridView1.Rows[renglon].Cells[1].Value = item.m_IdCategoria.ToString();
                    dataGridView1.Rows[renglon].Cells[2].Value = item.m_Producto.ToString();
                    dataGridView1.Rows[renglon].Cells[3].Value = item.m_Marca.ToString();
                    dataGridView1.Rows[renglon].Cells[4].Value = item.m_PrecioCompra.ToString();
                    dataGridView1.Rows[renglon].Cells[5].Value = item.m_PrecioVenta.ToString();
                    dataGridView1.Rows[renglon].Cells[6].Value = item.m_Stock.ToString();
                    dataGridView1.Rows[renglon].Cells[7].Value = item.m_FechaVencimiento.ToString();
                    dataGridView1.Rows[renglon].Cells[8].Value = item.m_FechaModificacion.ToString();
                    dataGridView1.Rows[renglon].Cells[9].Value = item.m_itbis.ToString();
                    dataGridView1.Rows[renglon].Cells[10].Value = item.m_tipoGoma.ToString();
                    dataGridView1.Rows[renglon].Cells[11].Value = item.m_Preciomax.ToString();
                    dataGridView1.Rows[renglon].Cells[12].Value = item.m_Preciomin.ToString();
                }

                compras = listaproductos.Sum(x => x.m_PrecioCompra);
                ventas = listaproductos.Sum(x => x.m_PrecioVenta);
                totalproducto = listaproductos.Sum(x => x.m_Stock);
                total = ventas - compras;
                txttotalG.Text = Convert.ToString(total);
                lbltotalproductos.Text = Convert.ToString(totalproducto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Selected = true;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FrmRegistroProductos P = new FrmRegistroProductos();
            if (dataGridView1.SelectedRows.Count > 0)
                Program.Evento = 1;
            else
                Program.Evento = 0;
            dataGridView1.ClearSelection();
            P.label6.Text = "Fecha de Ingreso";
            P.Show();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (Program.isAdminUser)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    FrmRegistroProductos P = new FrmRegistroProductos();
                    P.txtIdP.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    P.IdC.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    P.txtProducto.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    P.txtMarca.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    P.txtPCompra.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    P.txtPVenta.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    P.txtStock.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    P.txtitbis.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                    P.cbtipo.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                    P.txtPmax.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                    P.txtPmin.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
                    P.dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[8].Value.ToString());
                    P.button1.Visible = true;
                    P.btnGuardar.Visible = false;
                    P.label6.Text = "Fecha de Modificacion";
                    P.label11.Text = "Actualizar Producto";
                    P.Show();

                    if (dataGridView1.SelectedRows.Count > 0)
                        Program.Evento = 1;
                    else
                        Program.Evento = 0;
                    dataGridView1.ClearSelection();
                }
                else
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show("Debe Seleccionar la Fila a Editar.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                    dataGridView1.ClearSelection();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Program.abiertosecundario = false;
            Program.abierto = false;
            Program.IdProducto = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Column1"].Value.ToString());
            Program.Descripcion = dataGridView1.CurrentRow.Cells["description"].Value.ToString();
            Program.Marca = dataGridView1.CurrentRow.Cells["marca"].Value.ToString();
            Program.PrecioVenta = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["pVenta"].Value.ToString());
            Program.PrecioCompra = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["pCompra"].Value.ToString());
            Program.Stock = Convert.ToInt32(dataGridView1.CurrentRow.Cells["cantidad"].Value.ToString());
            Program.IdCategoria = Convert.ToInt32(dataGridView1.CurrentRow.Cells["IdC"].Value.ToString());
            Program.itbis = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["itbis"].Value.ToString());
            Program.tipo = dataGridView1.CurrentRow.Cells["tipoGOma"].Value.ToString();
            Program.Pmax = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["Pmax"].Value.ToString());
            Program.Pmin = Convert.ToDecimal(dataGridView1.CurrentRow.Cells["Pmin"].Value.ToString());
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string Reporte;
        private void To_pdf()
        {
            if (textBox5.Text != "")
            {
                Reporte = "Inventario Por Tipo Productos " + cbTipoGoma.Text;
            }

            if (txtdesc.Text != "")
            {
                Reporte = "Inventario de Productos por Categoria " + cbxCategoria.Text;
            }

            if (rbtodos.Checked == true)
            {
                Reporte = "Inventario de Todos los Productos";
            }

            if (rbCero.Checked == true)
            {
                Reporte = "Inventario de Todos los Productos en Cero";
            }

            if (radioButton1.Checked == true)
            {
                Reporte = "Inventario de Todos los Productos con Cantidad Minima";
            }

            if (rdmedia.Checked == true)
            {
                Reporte = "Inventario de Todos los Productos con Cantidad Media";
            }

            if (rbbuena.Checked == true)
            {
                Reporte = "Inventario de Todos los Productos con Cantidad Suficiente";
            }

            if (txtdesc.Text != "")
            {
                Reporte = "Inventario de " + txtdesc.Text;
            }

            if (rbfechaing.Checked == true)
            {
                var fecha1 = Convert.ToDateTime(dtpfecha1.Text);
                var fecha2 = Convert.ToDateTime(dtpfecha2.Text);
                Reporte = "Inventario de Fecha de Ingreso \n" +
                    "Desde " + fecha1.Day + "/" + fecha1.Month + "/" + fecha1.Year + "\n" +
                    "Hasta " + fecha2.Day + "/" + fecha2.Month + "/" + fecha2.Year;
            }

            if (rbfechamod.Checked == true)
            {
                var fecha1 = Convert.ToDateTime(dtpfecha1.Text);
                var fecha2 = Convert.ToDateTime(dtpfecha2.Text);
                Reporte = "Inventario de Fecha de Modificacion \n" +
                    "Desde " + fecha1.Day + "/" + fecha1.Month + "/" + fecha1.Year + "\n" +
                    "Hasta " + fecha2.Day + "/" + fecha2.Month + "/" + fecha2.Year;
            }

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

                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("Reporte de Inventario de Productos   "));
                    doc.Add(new Paragraph("                       "));
                    GenerarDocumento(doc);
                    doc.AddCreationDate();
                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("Total de Productos = " + lbltotalproductos.Text));
                    doc.Add(new Paragraph("Ganancias Total de Ventas= " + txttotalG.Text));
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
                        datatable.AddCell(new Phrase(dataGridView1[j, i].Value.ToString(), FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL)));//En esta parte, se esta agregando un renglon por cada registro en el datagrid
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

        private void btnimpimir_Click(object sender, EventArgs e)
        {
            To_pdf();
        }

        //public void repetitivo()
        //{
        //    M.Desconectar();
        //    M.Conectar();
        //    string sql = "select top(1) detalles_P, Sum(Cantidad) AS total FROM  dbo.DetalleVenta GROUP BY detalles_P ORDER BY total DESC";
        //    SqlCommand cmd = new SqlCommand(sql, M.conexion);
        //    SqlDataReader reade = cmd.ExecuteReader();
        //    if (reade.Read())
        //    {
        //        txtRep.Text = reade["detalles_P"].ToString();

        //    }
        //    M.Desconectar();
        //}

        //public void Mrepetitivo()
        //{
        //    M.Desconectar();
        //    M.Conectar();
        //    string sql = "select top(1) detalles_P, Sum( Cantidad ) AS total FROM  dbo.DetalleVenta GROUP BY detalles_P ORDER BY total ASC";
        //    SqlCommand cmd = new SqlCommand(sql, M.conexion);
        //    SqlDataReader reade = cmd.ExecuteReader();
        //    if (reade.Read())
        //    {
        //        txtMrep.Text = reade["detalles_P"].ToString();
        //    }
        //    M.Desconectar();
        //}

        #region radiobutton area
        private void rbCero_CheckedChanged(object sender, EventArgs e)
        {
            decimal compras = 0, total = 0, ventas = 0, totalproducto = 0;
            if (rbCero.Checked == true)
            {
                var newlist = clsGenericList.listProducto.Where(x => x.m_Stock == 0).ToList();
                CargarListado(newlist.OrderBy(x => x.m_IdP).ToList());

                compras = newlist.Sum(x => x.m_PrecioCompra);
                ventas = newlist.Sum(x => x.m_PrecioVenta); ;
                totalproducto = newlist.Sum(x => x.m_Stock);
                lbltotalproductos.Text = Convert.ToString(totalproducto);
                total = ventas - compras;
                txttotalG.Text = Convert.ToString(total);
            }
            else
            {
                CargarListado(clsGenericList.listProducto.OrderBy(x => x.m_IdP).ToList());
            }
        }
        private void rdmedia_CheckedChanged(object sender, EventArgs e)
        {
            decimal compras = 0, total = 0, ventas = 0, totalproducto = 0;
            if (rdmedia.Checked == true)
            {
                var newlist = clsGenericList.listProducto.Where(x => x.m_Stock > 4 && x.m_Stock < 15).ToList();
                CargarListado(newlist.OrderBy(x => x.m_IdP).ToList());

                compras = newlist.Sum(x => x.m_PrecioCompra);
                ventas = newlist.Sum(x => x.m_PrecioVenta); ;
                totalproducto = newlist.Sum(x => x.m_Stock);
                lbltotalproductos.Text = Convert.ToString(totalproducto);
                total = ventas - compras;
                txttotalG.Text = Convert.ToString(total);
            }
            else
            {
                CargarListado(clsGenericList.listProducto.OrderBy(x => x.m_IdP).ToList());
            }
        }
        private void rbbuena_CheckedChanged(object sender, EventArgs e)
        {
            decimal compras = 0, total = 0, ventas = 0, totalproducto = 0;
            if (rbbuena.Checked == true)
            {
                var newlist = clsGenericList.listProducto.Where(x => x.m_Stock > 15).ToList();
                CargarListado(newlist.OrderBy(x => x.m_IdP).ToList());

                compras = newlist.Sum(x => x.m_PrecioCompra);
                ventas = newlist.Sum(x => x.m_PrecioVenta); ;
                totalproducto = newlist.Sum(x => x.m_Stock);
                lbltotalproductos.Text = Convert.ToString(totalproducto);
                total = ventas - compras;
                txttotalG.Text = Convert.ToString(total);
            }
            else
            {
                CargarListado(clsGenericList.listProducto.OrderBy(x => x.m_IdP).ToList());
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            decimal compras = 0, total = 0, ventas = 0, totalproducto = 0;
            if (radioButton1.Checked == true)
            {
                var newlist = clsGenericList.listProducto.Where(x => x.m_Stock > 0 && x.m_Stock < 5).ToList();
                CargarListado(newlist.OrderBy(x => x.m_IdP).ToList());

                compras = newlist.Sum(x => x.m_PrecioCompra);
                ventas = newlist.Sum(x => x.m_PrecioVenta); ;
                totalproducto = newlist.Sum(x => x.m_Stock);
                lbltotalproductos.Text = Convert.ToString(totalproducto);
                total = ventas - compras;
                txttotalG.Text = Convert.ToString(total);
            }
            else
            {
                CargarListado(clsGenericList.listProducto.OrderBy(x => x.m_IdP).ToList());
            }
        }
        private void rbtodos_CheckedChanged(object sender, EventArgs e)
        {
            CargarListado(clsGenericList.listProducto.OrderBy(x => x.m_IdP).ToList());
        }
        private void rbfechaing_CheckedChanged(object sender, EventArgs e)
        {
            decimal compras = 0, total = 0, ventas = 0, totalproducto = 0;

            var newlist = clsGenericList.listProducto.Where(x => x.m_FechaVencimiento >= dtpfecha1.Value.Date && x.m_FechaVencimiento <= dtpfecha2.Value.Date).ToList();
            CargarListado(newlist.OrderBy(x => x.m_IdP).ToList());

            compras = newlist.Sum(x => x.m_PrecioCompra);
            ventas = newlist.Sum(x => x.m_PrecioVenta); ;
            totalproducto = newlist.Sum(x => x.m_Stock);
            lbltotalproductos.Text = Convert.ToString(totalproducto);
            total = ventas - compras;
            txttotalG.Text = Convert.ToString(total);
        }
        private void rbfechamod_CheckedChanged(object sender, EventArgs e)
        {
            decimal compras = 0, total = 0, ventas = 0, totalproducto = 0;

            var newlist = clsGenericList.listProducto.Where(x => x.m_FechaModificacion >= dtpfecha1.Value.Date && x.m_FechaModificacion <= dtpfecha2.Value.Date).ToList();
            CargarListado(newlist.OrderBy(x => x.m_IdP).ToList());

            compras = newlist.Sum(x => x.m_PrecioCompra);
            ventas = newlist.Sum(x => x.m_PrecioVenta); ;
            totalproducto = newlist.Sum(x => x.m_Stock);
            lbltotalproductos.Text = Convert.ToString(totalproducto);
            total = ventas - compras;
            txttotalG.Text = Convert.ToString(total);
        }
        #endregion

        private void cbxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbcategoriafiltro.Checked)
            {
                buscarid();
                decimal compras = 0, total = 0, ventas = 0, totalproducto = 0;
                if (id.Text != "")
                {
                    var newlist = clsGenericList.listProducto.Where(x => x.m_IdCategoria == Convert.ToInt32(id.Text)).ToList();
                    CargarListado(newlist.OrderBy(x => x.m_IdP).ToList());

                    compras = newlist.Sum(x => x.m_PrecioCompra);
                    ventas = newlist.Sum(x => x.m_PrecioVenta); ;
                    totalproducto = newlist.Sum(x => x.m_Stock);
                    lbltotalproductos.Text = Convert.ToString(totalproducto);
                    total = ventas - compras;
                    txttotalG.Text = Convert.ToString(total);
                }
                else
                {
                    CargarListado(clsGenericList.listProducto.OrderBy(x => x.m_IdP).ToList());
                }
            }
        }
        private void cbTipoGoma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbtipogomafiltro.Checked)
            {
                buscardesc();
                decimal compras = 0, total = 0, ventas = 0, totalproducto = 0;
                if (textBox5.Text != "")
                {
                    var newlist = clsGenericList.listProducto.Where(x => x.m_tipoGoma == textBox5.Text).ToList();
                    CargarListado(newlist.OrderBy(x => x.m_IdP).ToList());

                    compras = newlist.Sum(x => x.m_PrecioCompra);
                    ventas = newlist.Sum(x => x.m_PrecioVenta); ;
                    totalproducto = newlist.Sum(x => x.m_Stock);
                    lbltotalproductos.Text = Convert.ToString(totalproducto);
                    total = ventas - compras;
                    txttotalG.Text = Convert.ToString(total);
                }
                else
                {
                    CargarListado(clsGenericList.listProducto.OrderBy(x => x.m_IdP).ToList());
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "cantidad")
            {
                if (Convert.ToInt32(e.Value) == 0)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.White;
                    e.CellStyle.BackColor = System.Drawing.Color.Red;
                }

                if (Convert.ToInt32(e.Value) > 0 && Convert.ToInt32(e.Value) < 5)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.Black;
                    e.CellStyle.BackColor = System.Drawing.Color.Yellow;
                }

                if (Convert.ToInt32(e.Value) > 4 && Convert.ToInt32(e.Value) < 11)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.Black;
                    e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
                }

                if (Convert.ToInt32(e.Value) > 10)
                {
                    e.CellStyle.ForeColor = System.Drawing.Color.Black;
                    e.CellStyle.BackColor = System.Drawing.Color.CornflowerBlue;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            id.Text = "";
            cbxCategoria.Text = "";
            clear();
            txtBuscarProducto.Text = "";
            CargarListado(clsGenericList.listProducto.OrderBy(x => x.m_IdP).ToList());
            Refresh();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (Program.isAdminUser)
            {
                M.Desconectar();
                Program.IdProducto = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                if (Program.IdProducto > 0)
                {
                    if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Eliminar este Producto.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        using (SqlCommand cmd = new SqlCommand("eliminarProducto", M.conexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@IdProducto", SqlDbType.Int).Value = Program.IdProducto;

                            M.Conectar();
                            cmd.ExecuteNonQuery();
                            M.Desconectar();

                            var producto = clsGenericList.listProducto.FirstOrDefault(x => x.m_IdP == Program.IdProducto);
                            clsGenericList.listProducto.Remove(producto);

                            GetAllProduct();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Por Favor Seleccione un producto antes de eliminarlo");
                }
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            button2.Enabled = Program.isAdminUser;
            btnEditar.Enabled = Program.isAdminUser;
        }

        private void txtBuscarProducto_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtBuscarProducto.Text.Length >= 3)
            {
                string id = txtBuscarProducto.Text;
                var newlist = clsGenericList.listProducto.Where(x => x.m_Producto.ToLower().Contains(id.ToLower()) || x.m_Marca.ToLower().Contains(id.ToLower())).ToList();
                CargarListado(newlist.OrderBy(x => x.m_IdP).ToList());
            }
            else
            {
                GetAllProduct();
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            Program.abiertosecundario = false;
            Program.abierto = false;
            btnEditar.Enabled = Program.isAdminUser;

            this.Close();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void FrmListadoProductos_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void cbtipogomafiltro_CheckedChanged_1(object sender, EventArgs e)
        {
            if (cbtipogomafiltro.Checked)
            {
                cbTipoGoma.Enabled = true;
            }
            else
            {
                cbTipoGoma.Enabled = false;
            }
        }

        private void cbcategoriafiltro_CheckedChanged_1(object sender, EventArgs e)
        {
            if (cbcategoriafiltro.Checked)
            {
                cbxCategoria.Enabled = true;
            }
            else
            {
                cbxCategoria.Enabled = false;
            }
        }
    }
}


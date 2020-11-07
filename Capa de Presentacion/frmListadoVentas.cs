using System;
using System.Data;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using CapaLogicaNegocio;

namespace Capa_de_Presentacion
{
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
    public partial class frmListadoVentas : DevComponents.DotNetBar.Metro.MetroForm
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
	{
		public frmListadoVentas()
		{
			InitializeComponent();
		}

		clsCx Cx = new clsCx();
		private void frmListadoVentas_Load(object sender, EventArgs e)
		{
			repetitivo();
			llenar_data();
			llenar_data_V();
			buscarprod();
		}
		public void llenar_data_V()
		{
			decimal montovendido = 0;
			int Cantvendido = 0, idprod=0;
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
			comando.CommandText = "SELECT dbo.DetalleVenta.IdProducto,Sum( Cantidad ) as total ," +
				"SUM(dbo.DetalleVenta.SubTotal) as subt FROM dbo.DetalleVenta INNER JOIN dbo.Venta ON dbo.DetalleVenta.IdVenta = " +
				"dbo.Venta.IdVenta where FechaVenta = convert(datetime,CONVERT(varchar(10), getdate(), 103),103) GROUP BY IdProducto ORDER BY total DESC";
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

				dataGridView2.Rows[renglon].Cells["id_p"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdProducto")));
				dataGridView2.Rows[renglon].Cells["sub"].Value = Convert.ToDecimal(dr.GetDecimal(dr.GetOrdinal("subt")));
				dataGridView2.Rows[renglon].Cells["cant"].Value = Convert.ToDouble(dr.GetInt32(dr.GetOrdinal("total")));

				dataGridView2.Rows[0].Selected = true;
				dataGridView2.CurrentCell = dataGridView2.Rows[0].Cells["cant"];
				Cantvendido = Convert.ToInt32(dataGridView2.Rows[0].Cells["cant"].Value);

				dataGridView2.Rows[0].Selected = true;
				dataGridView2.CurrentCell = dataGridView2.Rows[0].Cells["id_p"];
				idprod = Convert.ToInt32(dataGridView2.Rows[0].Cells["id_p"].Value);

				montovendido += Math.Round(Convert.ToDecimal(dataGridView2.Rows[renglon].Cells["sub"].Value),2);
		
				txtidprod.Text= Convert.ToString(idprod);
				txtCantvend.Text = Convert.ToString(Cantvendido);
				txtMontvend.Text = Convert.ToString(montovendido);
			}
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
		public void llenar_data()
		{
			double total = 0;
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
			comando.CommandText = "SELECT dbo.Cliente.DNI, dbo.DetalleVenta.detalles_P, dbo.DetalleVenta.SubTotal, dbo.DetalleVenta.PrecioUnitario, dbo.DetalleVenta.Igv," +
				" dbo.DetalleVenta.Cantidad, dbo.DetalleVenta.IdProducto, dbo.Venta.IdVenta,dbo.Venta.Restante,dbo.Venta.Tipofactura, dbo.Venta.Total,dbo.Venta.IdEmpleado,dbo.Venta.TipoDocumento,dbo.Venta.NroDocumento, dbo.Venta.FechaVenta, dbo.Cliente.Nombres, dbo.Cliente.Apellidos " +
				"FROM  dbo.Cliente INNER JOIN dbo.Venta ON dbo.Cliente.IdCliente = dbo.Venta.IdCliente INNER JOIN dbo.DetalleVenta ON dbo.Venta.IdVenta = dbo.DetalleVenta.IdVenta AND " +
				"dbo.DetalleVenta.IdVenta = dbo.Venta.IdVenta WHERE dbo.DetalleVenta.IdVenta  like '%" + txtBuscarid.Text + "%'";
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
				dataGridView1.Rows[renglon].Cells["doc"].Value = dr.GetString(dr.GetOrdinal("DNI"));
				dataGridView1.Rows[renglon].Cells["nombre"].Value = dr.GetString(dr.GetOrdinal("Nombres"));
				dataGridView1.Rows[renglon].Cells["NCF"].Value = dr.GetString(dr.GetOrdinal("TipoDocumento"));
				dataGridView1.Rows[renglon].Cells["nroComprobante"].Value = dr.GetString(dr.GetOrdinal("NroDocumento"));
				dataGridView1.Rows[renglon].Cells["apellido"].Value = dr.GetString(dr.GetOrdinal("Apellidos"));
				dataGridView1.Rows[renglon].Cells["descripcion"].Value = dr.GetString(dr.GetOrdinal("detalles_P"));
				dataGridView1.Rows[renglon].Cells["idp"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdProducto")));
				dataGridView1.Rows[renglon].Cells["can"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("Cantidad")));
				dataGridView1.Rows[renglon].Cells["pre"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("PrecioUnitario")));
				dataGridView1.Rows[renglon].Cells["igv"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("Igv")));
				dataGridView1.Rows[renglon].Cells["subtotal"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("SubTotal")));
				dataGridView1.Rows[renglon].Cells["total"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("Total")));
				dataGridView1.Rows[renglon].Cells["Tipo"].Value = dr.GetString(dr.GetOrdinal("Tipofactura"));
				dataGridView1.Rows[renglon].Cells["restante"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("Restante")));
				dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("FechaVenta"));

				total += Convert.ToDouble(dataGridView1.Rows[renglon].Cells["subtotal"].Value);

				txtTtal.Text = Convert.ToString(total);
			}
		}
		private void btnCancelar_Click(object sender, EventArgs e)
		{
			To_pdf();
		}
		private void txtBuscarCliente_TextChanged(object sender, EventArgs e)
		{
			llenar_data();
		}

		public void seleccion_data()
		{
			Program.NombreCliente = dataGridView1.CurrentRow.Cells["nombre"].Value.ToString();
			Program.ApellidosCliente = dataGridView1.CurrentRow.Cells["apellido"].Value.ToString();
			Program.tipo = dataGridView1.CurrentRow.Cells["Tipo"].Value.ToString();
			Program.NCF = dataGridView1.CurrentRow.Cells["NCF"].Value.ToString();
			Program.NroComprobante = dataGridView1.CurrentRow.Cells["nroComprobante"].Value.ToString();
			Program.Id =Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
			Program.total =Convert.ToDecimal(dataGridView1.CurrentRow.Cells["total"].Value.ToString());
			Program.ST +=Convert.ToDecimal(dataGridView1.CurrentRow.Cells["subtotal"].Value.ToString());
			Program.igv +=Convert.ToDecimal(dataGridView1.CurrentRow.Cells["igv"].Value.ToString());
			Program.DocumentoIdentidad = dataGridView1.CurrentRow.Cells["doc"].Value.ToString();
			Program.fecha = dataGridView1.CurrentRow.Cells["fecha"].Value.ToString();
			Program.IdEmpleado = Convert.ToInt32(dataGridView1.CurrentRow.Cells["idEm"].Value.ToString());
			Program.ReImpresion = "Factura ReImpresa";
		}

		private void label2_Click(object sender, EventArgs e)
		{
			FrmRegistroVentas V = new FrmRegistroVentas();
			V.txtidEmp.Text=Convert.ToString(Program.IdEmpleadoLogueado);
			this.Close();
		}

		public void buscarprod()
		{
			Cx.conexion.Open();
			string sql = "select Nombre from Producto where IdProducto =@id";
			SqlCommand cmd = new SqlCommand(sql, Cx.conexion);
			cmd.Parameters.AddWithValue("@id", txtidprod.Text);

			SqlDataReader reade = cmd.ExecuteReader();
			if (reade.Read())
			{
				txtprod.Text = Convert.ToString(reade["Nombre"]);
			}
			Cx.conexion.Close();
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
				string envio = "Fecha : " + DateTime.Now.ToString();

				Chunk chunk = new Chunk("Reporte de Listado de Ventas Realizadas", FontFactory.GetFont("ARIAL", 16, iTextSharp.text.Font.BOLD));
				doc.Add(image1);
				doc.Add(new Paragraph(chunk));
				doc.Add(new Paragraph("                       "));
				doc.Add(new Paragraph(""));
				doc.Add(new Paragraph(remito));
				doc.Add(new Paragraph(ubicado));
				doc.Add(new Paragraph(envio));
				doc.Add(new Paragraph("                       "));
				doc.Add(new Paragraph("                       "));
				GenerarDocumento(doc);
				doc.AddCreationDate();
				doc.Add(new Paragraph("                       "));
				doc.Add(new Paragraph("Total de Ventas      : " + txtTtal.Text));
				doc.Add(new Paragraph("Producto Mas Vendido : " + txtRepi.Text));
				doc.Add(new Paragraph("                       "));
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
		string repeto;
		public void repetitivo()
		{
			Cx.conexion.Open();
			string sql = "select top(1) Nombre, Sum( Cantidad ) AS total FROM  dbo.DetalleVenta INNER JOIN " +
				"dbo.Producto ON dbo.DetalleVenta.IdProducto = dbo.Producto.IdProducto where Producto.IdProducto = " +
				"DetalleVenta.IdProducto GROUP BY Nombre ORDER BY total DESC";
			SqlCommand cmd = new SqlCommand(sql, Cx.conexion);
			SqlDataReader reade = cmd.ExecuteReader();
			if (reade.Read())
			{
				repeto = reade["Nombre"].ToString();
				txtRepi.Text = repeto;
			}
			Cx.conexion.Close();
		}
		private void button1_Click(object sender, EventArgs e)
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
			//declaramos el comando para realizar la busqueda
			comando.CommandText = "SELECT dbo.Cliente.DNI, dbo.DetalleVenta.detalles_P, dbo.DetalleVenta.SubTotal, dbo.DetalleVenta.PrecioUnitario, dbo.DetalleVenta.Igv," +
				" dbo.DetalleVenta.Cantidad, dbo.DetalleVenta.IdProducto, dbo.Venta.IdVenta,dbo.Venta.Restante,dbo.Venta.Tipofactura, dbo.Venta.Total,dbo.Venta.IdEmpleado,dbo.Venta.TipoDocumento,dbo.Venta.NroDocumento, dbo.Venta.FechaVenta, dbo.Cliente.Nombres, dbo.Cliente.Apellidos " +
				"FROM  dbo.Cliente INNER JOIN dbo.Venta ON dbo.Cliente.IdCliente = dbo.Venta.IdCliente INNER JOIN dbo.DetalleVenta ON dbo.Venta.IdVenta = dbo.DetalleVenta.IdVenta AND " +
				"dbo.DetalleVenta.IdVenta = dbo.Venta.IdVenta where FechaVenta BETWEEN '" + dtpfecha1.Value.ToString("yyyy-MM-dd") + "' AND '" + dtpfecha2.Value.ToString("yyyy-MM-dd") + "'";
			//especificamos que es de tipo Text
			comando.CommandType = CommandType.Text;
			//se abre la conexion
			con.Open();
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

				dataGridView1.Rows[renglon].Cells["id"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdVenta")));
				dataGridView1.Rows[renglon].Cells["idEm"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdEmpleado")));
				dataGridView1.Rows[renglon].Cells["doc"].Value = dr.GetString(dr.GetOrdinal("DNI"));
				dataGridView1.Rows[renglon].Cells["nombre"].Value = dr.GetString(dr.GetOrdinal("Nombres"));
				dataGridView1.Rows[renglon].Cells["NCF"].Value = dr.GetString(dr.GetOrdinal("TipoDocumento"));
				dataGridView1.Rows[renglon].Cells["nroComprobante"].Value = dr.GetString(dr.GetOrdinal("NroDocumento"));
				dataGridView1.Rows[renglon].Cells["apellido"].Value = dr.GetString(dr.GetOrdinal("Apellidos"));
				dataGridView1.Rows[renglon].Cells["descripcion"].Value = dr.GetString(dr.GetOrdinal("detalles_P"));
				dataGridView1.Rows[renglon].Cells["idp"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdProducto")));
				dataGridView1.Rows[renglon].Cells["can"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("Cantidad")));
				dataGridView1.Rows[renglon].Cells["pre"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("PrecioUnitario")));
				dataGridView1.Rows[renglon].Cells["igv"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("Igv")));
				dataGridView1.Rows[renglon].Cells["subtotal"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("SubTotal")));
				dataGridView1.Rows[renglon].Cells["total"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("Total")));
				dataGridView1.Rows[renglon].Cells["Tipo"].Value = dr.GetString(dr.GetOrdinal("Tipofactura"));
				dataGridView1.Rows[renglon].Cells["restante"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("Restante")));
				dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("FechaVenta"));

				total += Convert.ToDecimal(dataGridView1.Rows[renglon].Cells["subtotal"].Value);

				txtTtal.Text = Convert.ToString(Math.Round(total,2));
			}
		}
		private void button2_Click(object sender, EventArgs e)
		{
			txtBuscarid.Clear();
			llenar_data();
		}

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
			seleccion_data();
			FrmRegistroVentas V = new FrmRegistroVentas();
			V.btnSalir.Visible = false;
			this.Close();
		}
    }
}

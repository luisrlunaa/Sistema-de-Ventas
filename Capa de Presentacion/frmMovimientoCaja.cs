using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using CapaLogicaNegocio;

namespace Capa_de_Presentacion
{
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
	public partial class frmMovimientoCaja : DevComponents.DotNetBar.Metro.MetroForm
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
	{
		public frmMovimientoCaja()
		{
			InitializeComponent();
		}

		clsCx Cx = new clsCx();
		private void frmMovimientoCaja_Load(object sender, EventArgs e)
		{
			llenar_data();
			llenarid();
			llenar();
		}

		public void llenarid()
		{
			string cadSql = "select top(1) id_caja from Caja order by id_caja desc";

			SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);
			Cx.conexion.Open();

			SqlDataReader leer = comando.ExecuteReader();

			if (leer.Read() == true)
			{
				txtBuscarCaja.Text = leer["id_caja"].ToString();
			}
			Cx.conexion.Close();
		}
		public void llenar_data()
		{
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
			comando.CommandText = "SELECT dbo.Caja.id_caja, dbo.Caja.fecha, dbo.Caja.monto_inicial, dbo.Caja.monto_final, dbo.Caja.perdidas, dbo.Caja.motivo_perdida," +
				" dbo.Caja.ganancias,dbo.Pagos.monto, dbo.Pagos.id_pago, dbo.Pagos.ingresos, dbo.Pagos.egresos FROM   dbo.Caja INNER JOIN dbo.Pagos ON " +
				"dbo.Caja.id_caja = dbo.Pagos.id_caja WHERE dbo.Caja.id_caja like '%" + txtBuscarCaja.Text + "%'";
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

				//caja
				dataGridView1.Rows[renglon].Cells["id"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id_caja")));
				dataGridView1.Rows[renglon].Cells["montoini"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("monto_inicial")));
				dataGridView1.Rows[renglon].Cells["montofin"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("monto_final")));
				dataGridView1.Rows[renglon].Cells["perdidas"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("perdidas")));
				dataGridView1.Rows[renglon].Cells["ganancias"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("ganancias")));
				dataGridView1.Rows[renglon].Cells["motivo"].Value = dr.GetString(dr.GetOrdinal("motivo_perdida"));
				dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("fecha"));
				//pagos
				dataGridView1.Rows[renglon].Cells["id_pago"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id_pago")));
				dataGridView1.Rows[renglon].Cells["monto"].Value = Convert.ToString(dr.GetDouble(dr.GetOrdinal("monto")));
				dataGridView1.Rows[renglon].Cells["ingresos"].Value = Convert.ToString(dr.GetDouble(dr.GetOrdinal("ingresos")));
				dataGridView1.Rows[renglon].Cells["egresos"].Value = Convert.ToString(dr.GetDouble(dr.GetOrdinal("egresos")));
			}
		}

		public void llenar()
		{
			string cadSql = "select * from NomEmp";

			SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);
			Cx.conexion.Open();

			SqlDataReader leer = comando.ExecuteReader();

			if (leer.Read() == true)
			{
				lblDir.Text = leer["DirEmp"].ToString();
				lblLogo.Text = leer["NombreEmp"].ToString();

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
			string filename = "Reporte de Movimiento de Caja" + DateTime.Now.ToString();
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

				Chunk chunk = new Chunk("Reporte de Movimiento de Caja", FontFactory.GetFont("ARIAL", 16, iTextSharp.text.Font.BOLD));
				doc.Add(image1);
				doc.Add(new Paragraph(chunk));
				doc.Add(new Paragraph("                       "));
				doc.Add(new Paragraph("                       "));
				doc.Add(new Paragraph("------------------------------------------------------------------------------------------"));
				doc.Add(new Paragraph(""));
				doc.Add(new Paragraph(remito));
				doc.Add(new Paragraph(ubicado));
				doc.Add(new Paragraph(envio));
				doc.Add(new Paragraph("------------------------------------------------------------------------------------------"));
				doc.Add(new Paragraph("                       "));
				doc.Add(new Paragraph("                       "));
				doc.Add(new Paragraph("                       "));
				GenerarDocumento(doc);
				doc.AddCreationDate();
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

		private void btnimprimir_Click(object sender, EventArgs e)
		{
			To_pdf();
		}

		private void txtBuscarCaja_TextChanged(object sender, EventArgs e)
		{
			llenar_data();
		}

		private void label2_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}

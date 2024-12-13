using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public static class Pdf
    {
        public static void GenerarDocumento(Document document, DataGridView dataGridView)
        {
            PdfPTable datatable = new PdfPTable(dataGridView.ColumnCount) { WidthPercentage = 100 };
            float[] headerWidths = GetTamañoColumnas(dataGridView);
            datatable.SetWidths(headerWidths);

            // Encabezado de la tabla
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, FontFactory.GetFont("ARIAL", 9, Font.BOLD)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BackgroundColor = BaseColor.LIGHT_GRAY
                };
                datatable.AddCell(cell);
            }

            // Datos
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    datatable.AddCell(new Phrase(cell.Value?.ToString() ?? "", FontFactory.GetFont("ARIAL", 8)));
                }
            }

            document.Add(datatable);
        }

        public static float[] GetTamañoColumnas(DataGridView dg)
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
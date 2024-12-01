using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public class CrearTiket
    {
        private readonly StringBuilder _linea = new StringBuilder();
        private Image _headerImage = null;

        private const int MaxCar = 48; // Máximo de caracteres por línea.
        private const int ColArticuloWidth = 24; // Ancho máximo para la columna de artículos.
        private const int ColCantxPrecioWidth = 15;
        private const int ColSubtotalWidth = 10;
        private const int ColItbisWidth = 8;

        public Image HeaderImage
        {
            get => _headerImage;
            set
            {
                if (_headerImage != value)
                {
                    _headerImage = value;
                }
            }
        }

        public string LineasGuion() => new string('-', MaxCar) + Environment.NewLine;
        public string LineasAsteriscos() => new string('*', MaxCar) + Environment.NewLine;
        public string LineasIgual() => new string('=', MaxCar) + Environment.NewLine;

        public void TextoCentro(string texto)
        {
            var espacios = (MaxCar - texto.Length) / 2;
            _linea.AppendLine(new string(' ', espacios) + texto);
        }

        public void TextoIzquierda(string texto)
        {
            _linea.AppendLine(texto);
        }

        public void TextoDerecha(string texto)
        {
            var espacios = MaxCar - texto.Length;
            _linea.AppendLine(new string(' ', espacios) + texto);
        }

        public void TextoExtremos(string textoIzq, string textoDer)
        {
            var espacios = MaxCar - textoIzq.Length - textoDer.Length;
            _linea.AppendLine(textoIzq + new string(' ', espacios) + textoDer);
        }

        public void EncabezadoVenta()
        {
            _linea.AppendLine("ARTICULOS            |CANTxPRECIO |SUBT |ITBIS");
        }

        public void AgregaArticulo(string articulo, string cantxprecio, decimal subtotal, decimal itbis)
        {
            string AjustarLargoTexto(string texto, int maximo)
            {
                var resultado = new StringBuilder();
                var posicion = 0;
                while (texto.Length - posicion > maximo)
                {
                    resultado.AppendLine(texto.Substring(posicion, maximo));
                    posicion += maximo;
                }
                resultado.Append(texto.Substring(posicion));
                return resultado.ToString();
            }

            var articuloAjustado = AjustarLargoTexto(articulo, ColArticuloWidth);
            var articuloLineas = articuloAjustado.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < articuloLineas.Length; i++)
            {
                var lineaArticulo = articuloLineas[i];
                if (i == 0)
                {
                    _linea.AppendLine(
                        $"{lineaArticulo.PadRight(ColArticuloWidth)}" +
                        $"{cantxprecio.PadLeft(ColCantxPrecioWidth)}" +
                        $"{subtotal.ToString("#.##").PadLeft(ColSubtotalWidth)}" +
                        $"{itbis.ToString("#.##").PadLeft(ColItbisWidth)}");
                }
                else
                {
                    _linea.AppendLine(lineaArticulo.PadRight(MaxCar));
                }
            }
        }

        public void AgregarTotales(string texto, decimal total)
        {
            var resumen = texto.Length > 29 ? texto.Substring(0, 29) : texto;
            var valor = total.ToString("#,##0.00");
            var espacios = MaxCar - resumen.Length - valor.Length;

            _linea.AppendLine(resumen + new string(' ', espacios) + valor);
        }

        public void CortaTicket()
        {
            _linea.AppendLine("\x1B" + "m");
            _linea.AppendLine("\x1B" + "d" + "\x00");
        }

        public void AbreCajon()
        {
            _linea.AppendLine("\x1B" + "p" + "\x00" + "\x0F" + "\x96");
        }

        public void ImprimirTicket(string impresora)
        {
            RawPrinterHelper.SendStringToPrinter(impresora, _linea.ToString());
            _linea.Clear();
        }

        public void ImprimirImagen(string rutaImagen, string impresora)
        {
            if (File.Exists(rutaImagen))
            {
                PrintDocument printDoc = new PrintDocument();
                printDoc.PrinterSettings.PrinterName = impresora;

                printDoc.PrintPage += (sender, e) =>
                {
                    Image img = Image.FromFile(rutaImagen);
                    // Redimensionar la imagen para ajustarla al ancho del ticket
                    int ticketWidth = 280; // Ancho del ticket en píxeles
                    int newHeight = (int)((double)ticketWidth / img.Width * img.Height);
                    Bitmap resizedImg = new Bitmap(img, new Size(ticketWidth, newHeight));

                    e.Graphics.DrawImage(resizedImg, new Point(0, 0));
                };

                printDoc.Print();
            }
            else
            {
                return;
            }
        }

        public void VistaPreviaTicket()
        {
            // Crear un formulario para mostrar la vista previa
            Form vistaPreviaForm = new Form
            {
                Text = "Vista Previa del Ticket",
                Size = new Size(600, 800),
                StartPosition = FormStartPosition.CenterScreen
            };

            // Agregar un RichTextBox para mostrar el contenido del ticket
            RichTextBox vistaPreviaTextBox = new RichTextBox
            {
                Text = _linea.ToString(),
                Dock = DockStyle.Fill,
                Font = new Font("Courier New", 10), // Fuente monoespaciada para alinear texto
                ReadOnly = true
            };

            // Agregar un botón para cerrar la vista previa
            Button btnCerrar = new Button
            {
                Text = "Cerrar",
                Dock = DockStyle.Bottom,
                Height = 40
            };
            btnCerrar.Click += (sender, e) => vistaPreviaForm.Close();

            // Agregar controles al formulario
            vistaPreviaForm.Controls.Add(vistaPreviaTextBox);
            vistaPreviaForm.Controls.Add(btnCerrar);

            // Mostrar el formulario
            vistaPreviaForm.ShowDialog();
        }
    }

    public static class RawPrinterHelper
    {
        public static bool SendStringToPrinter(string printerName, string document)
        {
            var docBytes = Encoding.ASCII.GetBytes(document);
            return SendBytesToPrinter(printerName, docBytes, docBytes.Length);
        }

        private static bool SendBytesToPrinter(string szPrinterName, byte[] pBytes, int dwCount)
        {
            // Implementación del envío de bytes a la impresora omitida.
            return true; // Aquí puedes manejar el resultado real.
        }
    }
}

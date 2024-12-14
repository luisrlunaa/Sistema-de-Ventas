using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

public class CrearTiket
{
    private readonly StringBuilder _linea = new StringBuilder();
    private Image _headerImage = null;

    private const int MaxCar = 48; // Máximo de caracteres por línea.
    private const int ColArticuloWidth = 21; // Ancho máximo para la columna de artículos.
    private const int ColCantxPrecioWidth = 21; // Ancho máximo para la columna de CantxPrecio.
    private const int ColSubtotalWidth = 14;
    private const int ColItbisWidth = 12;

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
        _linea.AppendLine(LineasGuion());
        _linea.AppendLine("DESCRIPCION           ITBIS               PRECIO");
        _linea.AppendLine(LineasGuion());
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

        _linea.AppendLine($"{cantxprecio.PadRight(ColCantxPrecioWidth)}" +
                          $"{itbis.ToString("#.##").PadRight(ColItbisWidth)}" +
                          $"{subtotal.ToString("#.##").PadLeft(ColSubtotalWidth)}");

        for (var i = 0; i < articuloLineas.Length; i++)
        {
            var lineaArticulo = articuloLineas[i];
            _linea.AppendLine(lineaArticulo.PadRight(MaxCar));
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
        Form vistaPreviaForm = new Form
        {
            Text = "Vista Previa del Ticket",
            Size = new Size(600, 800),
            StartPosition = FormStartPosition.CenterScreen
        };

        RichTextBox vistaPreviaTextBox = new RichTextBox
        {
            Text = _linea.ToString(),
            Dock = DockStyle.Fill,
            Font = new Font("Courier New", 10),
            ReadOnly = true
        };

        Button btnCerrar = new Button
        {
            Text = "Cerrar",
            Dock = DockStyle.Bottom,
            Height = 40
        };
        btnCerrar.Click += (sender, e) => vistaPreviaForm.Close();

        vistaPreviaForm.Controls.Add(vistaPreviaTextBox);
        vistaPreviaForm.Controls.Add(btnCerrar);

        vistaPreviaForm.ShowDialog();
    }

    //Clase para mandara a imprimir texto plano a la impresora
    public class RawPrinterHelper
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "Ticket de Venta";//Este es el nombre con el que guarda el archivo en caso de no imprimir a la impresora fisica.
            di.pDataType = "RAW";//de tipo texto plano
                                 //di.pOutputFile = "D:\\ticket.txt";

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }
    }
}

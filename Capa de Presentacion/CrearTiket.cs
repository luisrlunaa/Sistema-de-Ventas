using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
   public class CrearTiket
    {
        //objeto de la clase, para las lineas del ticket
        StringBuilder linea = new StringBuilder();
        //variable que guarda el numero maximo de caracteres

        int maxcar = 40, cortar; //depende de la impresora.
        // la variable cortar es para cortar el texto.


        //lineas guion

        public string lineasGuio()
        {
            string lineasGuion = "";
            for (int i = 0; i < maxcar; i++)
            {
                lineasGuion += "-";// este es el guion

            }
            return linea.AppendLine(lineasGuion).ToString(); //retorno al principio
        }

        //mismo metodo pero para dibujar asteriscos
        public string lineasAsteriscos()
        {
            string lineasAsterisco = "";
            for (int i = 0; i < maxcar; i++)
            {
                lineasAsterisco += "*";// este es el asterisco

            }
            return linea.AppendLine(lineasAsterisco).ToString(); //retorno al principio
        }

        //mismo procedimiento para linea con signo de igual

        public string lineasIgual()
        {
            string lineasIgual = "";
            for (int i = 0; i < maxcar; i++)
            {
                lineasIgual += "=";// este es el guion

            }
            return linea.AppendLine(lineasIgual).ToString(); //retorno al principio
        }
        //encabezado para los articulos

        public void EncabezadoVentas()
        {
            linea.AppendLine("ARTICULOS                 |CANT|PRECIO|IMPORTE");// espacios que mostrara el articulo, en total 40 caracteres
        }

        //creamos un metodo para poner el texto a la izquierda
        public void textoIzquierda(string texto)
        {
            if (texto.Length > maxcar)
            {
                int caracterActual = 0; //indica el caracter se quedo al bajar texto linea
                for (int longitudTexto = texto.Length; longitudTexto > maxcar; longitudTexto -= maxcar)
                {// primer fragmento de linea
                    linea.AppendLine(texto.Substring(caracterActual, maxcar));
                    caracterActual += maxcar;
                }


                //agregamos el fragmento restante
                linea.AppendLine(texto.Substring(caracterActual, texto.Length - caracterActual));


            }
            else
            {//si no es mayor solo agregarlo
                linea.AppendLine(texto);
            }
        }

        // metodo para texto a la derecha

        public void textoDerecha(string texto)
        {
            if (texto.Length > maxcar)
            {
                int caracterActual = 0; //indica el caracter se quedo al bajar texto linea
                for (int longitudTexto = texto.Length; longitudTexto > maxcar; longitudTexto -= maxcar)
                {// primer fragmento de linea
                    linea.AppendLine(texto.Substring(caracterActual, maxcar));
                    caracterActual += maxcar;
                }

                //variable para poner espacios retantes
                string espacios = "";
                for (int i = 0; i < (maxcar - texto.Substring(caracterActual, texto.Length - caracterActual).Length); i++)
                {
                    espacios += " "; //agregar espacios para alinear a la derecha
                }

                //agregamos el fragmento restante
                linea.AppendLine(espacios + texto.Substring(caracterActual, texto.Length - caracterActual));


            }
            else
            {//variable para poner espacios retantes
                string espacios = "";
                for (int i = 0; i < (maxcar - texto.Length); i++)
                {
                    espacios += " "; //agregar espacios para alinear a la derecha
                }


                //si no es mayor solo agregarlo
                linea.AppendLine(espacios + texto);
            }
        }

        //metodo para centrar el texto
        public void textoCentro(string texto)
        {
			if (texto.Length > maxcar)
            {
                int caracterActual = 0; //indica el caracter se quedo al bajar texto linea
                for (int longitudTexto = texto.Length; longitudTexto > maxcar; longitudTexto -= maxcar)
                {// primer fragmento de linea
                    linea.AppendLine(texto.Substring(caracterActual, maxcar));
                    caracterActual += maxcar;

                }

                //variable para poner espacios retantes
                string espacios = "";

                //sacamos la cantidad de espacios libres y el resultado se divide entre dos
                int centrar = (maxcar - texto.Substring(caracterActual, texto.Length - caracterActual).Length) / 2;

                for (int i = 0; i < centrar; i++)
                {
                    espacios += " "; //agregar espacios para centrar
                }

                //agregamos el fragmento restante
                linea.AppendLine(espacios + texto.Substring(caracterActual, texto.Length - caracterActual));


            }
            else
            {
                //variable para poner espacios retantes
                string espacios = "";

                //sacamos la cantidad de espacios libres y el resultado se divide entre dos
                int centrar = (maxcar - texto.Length) / 2;

                for (int i = 0; i < centrar; i++)
                {
                    espacios += " "; //agregar espacios para centrar
                }

                //agregamos el fragmento restante
                linea.AppendLine(espacios + texto);

            }
        }
        // metodo para poner texto a los extremos
        public void textoExtremos(string textoizquierdo, string textoderecho)
        {
            //variables a utilizar
            string textoizq, textoder, textocompleto = "", espacios = "";

            //si el texto que va a la izquierda es mayor a 18, cortamos el texto.
            if (textoizquierdo.Length > 18)
            {
                cortar = textoizquierdo.Length - 18;
                textoizq = textoizquierdo.Remove(18, cortar);
            }
            else
            {
                textoizq = textoizquierdo;
            }
            textocompleto = textoizq;// agregamos el primer texto


            if (textoderecho.Length > 20) // si es mayor a 20 lo cortamos
            {
                cortar = textoderecho.Length - 20;
                textoder = textoderecho.Remove(20, cortar);
            }
            else
            {
                textoder = textoderecho;
            }
            //obtenemos el numero de espacios restantes para poner texto derecho al final
            int nroEspacios = maxcar - (textoizq.Length + textoder.Length);
            for (int i = 0; i < nroEspacios; i++)
            {
                espacios += " ";
            }

            textocompleto += espacios + textoderecho;// se agrega el segundo texto
            linea.AppendLine(textocompleto);//agregarmos la linea ticket al objeto


        }
        // metodo para agregar los totales de la venta
        public void agregarTotales(string texto, decimal total)
        {
            //variables que usamos
            string resumen, valor, textocompleto, espacios = "";
            if (texto.Length > 25)// si es mayor a 25 lo cortamos
            {
                cortar = texto.Length - 25;
                resumen = texto.Remove(25, cortar);
            }
            else
            { resumen = texto; }
            textocompleto = resumen;
            valor = total.ToString("#,#.00");//agregamos el total previo formateo.

            //obtenemos el numero de espacios restantes para alinear a la derecha
            int nrpEspacios = maxcar - (resumen.Length + valor.Length);
            //agregamos los espacios
            for (int i = 0; i < nrpEspacios; i++)
            {
                espacios += " ";
            }
            textocompleto += espacios + valor;
            linea.AppendLine(textocompleto);
        }


        // agregar los articulos a la factura
        public void AgregarArticulo(string articulo, int cant, decimal precio, decimal importe)
        {
            // valida que cant precio e importe esten dentro del rango
            if (cant.ToString().Length <= 5 && precio.ToString().Length <= 7 && importe.ToString().Length <= 8)
            {
                string elemento = "", espacios = "";
                bool bandera = false;
                int nroEspacios = 0;

                if (articulo.Length > 20)
                {
                    //colocar la cantidad a la derecha
                    nroEspacios = (5 - cant.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }

                    elemento += espacios + cant.ToString();

                    //colocar el precio a la derecha.
                    nroEspacios = (7 - precio.ToString().Length);
                    espacios = "";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }


                    elemento += espacios + precio.ToString();

                    //colocar el importe a la derecha
                    nroEspacios = (8 - importe.ToString().Length);
                    espacios = " ";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }

                    elemento += espacios + importe.ToString(); //se agrega el importe

                    int caracterActual = 0;
                    for (int longitudTexto = articulo.Length; longitudTexto > 20; longitudTexto -= 20)
                    {
                        if (bandera == false)
                        {
                            linea.AppendLine(articulo.Substring(caracterActual, 20) + elemento);
                            bandera = true;
                        }
                        else//-----------------------

                            linea.AppendLine(articulo.Substring(caracterActual, 20));
                        caracterActual += 20;
                    }

                    linea.AppendLine(articulo.Substring(caracterActual, articulo.Length - caracterActual));
                }
                else
                {
                    for (int i = 0; i < (20 - articulo.Length); i++)
                    {
                        espacios += " ";
                    }
                    elemento = articulo + espacios;

                    //colocar la cantidad a la derecha
                    nroEspacios = (5 - cant.ToString().Length);
                    espacios = " ";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }

                    elemento += espacios + cant.ToString();

                    //colocar el precio a la derecha
                    nroEspacios = (7 - cant.ToString().Length);
                    espacios = " ";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }

                    elemento += espacios + precio.ToString();

                    //colocar el importe a la derecha
                    nroEspacios = (8 - cant.ToString().Length);
                    espacios = " ";
                    for (int i = 0; i < nroEspacios; i++)
                    {
                        espacios += " ";
                    }

                    elemento += espacios + importe.ToString();
                    linea.AppendLine(elemento); // se agrega el elemento
                }
            }
            else
            {
                linea.AppendLine("valores ingresados para esta fila");
                linea.AppendLine("superan las columnas soportadas por este");
                throw new Exception("los valores ingresados para algunas filas del ticket/no superan las soportadas por este");
            }
        }


        //metodos para enviar secuencias de escape a la impresora
        //para cortar el ticket


        public void CortaTicket()
        {
            linea.AppendLine("\x1B" + "m"); //comando de corte, el cual cambia dependiendo de la impresora
            linea.AppendLine("\x1B" + "d" + "\x09"); // avanza 9 renglones, varial por impresora

        }

        public void AbreCajon()
        {
            // este codigo varia segun la impresora, se necesita leer el manual para saber
            linea.AppendLine("\x1B" + "p" + "\x00" + "\x0F" + "\x96"); // codigo para abrir cajon, varia segun la impresora

        }
        public void ImprimirTicket(string impresora)
        {
            // este metodo recibe el nombre de la impresora a la cual se mandara a imprimir y el texto 
            //se utiliza un codigo que saque de la paguina de microsoft

            RawPrinterHelper.SendStringToPrinter(impresora, linea.ToString());//imprime texto
            linea.Clear();//al acabar de imprimir limpia la linea de todo el texto agregado
        }

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

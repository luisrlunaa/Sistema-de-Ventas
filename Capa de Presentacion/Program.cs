using System;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.      
        /// </summary>
        /// 
        public static bool isSaler;
        public static bool isAdminUser;
        public static bool abierto;
        public static bool abiertosecundarias;
        public static bool pagarcotizacion;
        public static int Evento;
        public static int Eid;
        public static int idgastos;
        public static string LoginStatus;
        public static string ReImpresion;
        public static string Esabono;
        public static string WindUser;
        public static string SqlFolder;
        public static string ImpresonaPeq;

        //Datos del Cliente
        public static int IdCliente;
        public static string DocumentoIdentidad;
        public static string NombreCliente;
        public static string ApellidosCliente;
        public static string datoscliente;
        public static string Telefono;
        public static string Vehiculo;

        //Datos del Cliente2
        public static int IdCliente2;

        //Datos del Equipo
        public static int Id;
        public static string cedula;
        public static string nombres;
        public static string descripcion;
        public static string marca;
        public static string modelo;
        public static string Aros;
        public static decimal precio;
        public static decimal pago;
        public static decimal total;
        public static decimal ST;
        public static decimal igv;
        public static string nota;
        public static string fecha;
        public static string ultimafechapago;

        //Datos del Comprobante
        public static string tipo;
        public static string NCF;
        public static string NroComprobante;

        //Datos del Producto
        public static int IdProducto;
        public static int IdCategoria;
        public static string Descripcion;
        public static string Marca;
        public static int Stock;
        public static decimal itbis;
        public static decimal PrecioVenta;
        public static decimal PrecioCompra;
        public static string tipogoma;

        //Datos del Empleado
        public static int IdCargo;
        public static string EstadoCivil;
        public static int IdEmpleado;

        //Variables de Sesion
        public static int IdEmpleadoLogueado;
        public static string NombreEmpleadoLogueado;

        public static int IdEmpleadoLogueado1;
        public static string NombreEmpleadoLogueado1;
        public static string CargoEmpleadoLogueado1;
        public static bool realizopago;

        //turno
        public static double turno;

        //Pagos
        public static decimal pagocon;
        public static decimal pagoRealizado;
        public static decimal Devuelta;
        public static decimal Monto;
        public static decimal Caja;
        public static int idPago;
        public static int idcaja;
        public static decimal MontoInicial;
        public static string Fechapago;

        [STAThread]
        static void Main()
        {
            //var culture = new System.Globalization.CultureInfo("en-US");
            //System.Globalization.CultureInfo.DefaultThreadCurrentCulture = culture;
            //System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = culture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
        }

        public static T AnyNullValue<T>(object obj)
        {
            foreach (var property in obj.GetType().GetProperties())
            {
                var isStringNull = property.PropertyType == typeof(string)
                                 && (property.GetValue(obj) == null
                                 || string.IsNullOrWhiteSpace(property.GetValue(obj)?.ToString()));

                if (property.GetValue(obj) == null || isStringNull)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        property.SetValue(obj, "Sin " + property.Name);
                    }
                    else if (property.PropertyType == typeof(int?)
                            || property.PropertyType == typeof(decimal?)
                            || property.PropertyType == typeof(double?)
                            || property.PropertyType == typeof(float?))
                    {
                        property.SetValue(obj, 0);
                    }
                    else if (property.PropertyType == typeof(char?))
                    {
                        property.SetValue(obj, '\0');
                    }
                    else if (property.PropertyType == typeof(DateTime?))
                    {
                        property.SetValue(obj, DateTime.MinValue);
                    }
                }
            }

            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static decimal GetTwoNumberAfterPointWithOutRound(string number)
        {
            decimal output = 0;
            if (!string.IsNullOrWhiteSpace(number) && number.Contains("."))
            {
                var firtsPartOfNumber = number.Split('.')[0];
                var secondPartOfNumber = number.Split('.')[1].Substring(0, 2);
                var newNumber = firtsPartOfNumber + "." + secondPartOfNumber;
                output = decimal.Parse(newNumber);
            }
            else
            {
                output = decimal.Parse(number);
            }
            return output;
        }
    }
}

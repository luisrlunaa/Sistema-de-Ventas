using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.      
        /// </summary>
        public static int Evento;
		public static int Eid;
		public static string inventario;

		//Datos del Cliente
		public static int IdCliente;
        public static string DocumentoIdentidad;
        public static string NombreCliente;
        public static string ApellidosCliente;

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
		public static int precio;
		public static int pago;
		public static double total;
		public static double ST;
		public static double igv;
		public static string nota;
		public static string fecha;

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
		public static int itbis;
		public static decimal PrecioVenta;
		public static string tipogoma;

		//Datos del Empleado
		public static int IdCargo;
        public static string EstadoCivil ="";
        public static int IdEmpleado;

        //Variables de Sesion
        public static int IdEmpleadoLogueado;
        public static string NombreEmpleadoLogueado;
		public static string CargoEmpleadoLogueado;

		public static int IdEmpleadoLogueado1;
		public static string NombreEmpleadoLogueado1;
		public static string CargoEmpleadoLogueado1;

		//turno
		public static double turno;

		//Pagos
		public static double pagocon;
		public static double pagoRealizado;
		public static double Devuelta;
		public static double Monto;
		public static double Caja;
		public static int idPago;
		public static int idcaja;
		public static string Fechapago;

		[STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
        }
    }
}

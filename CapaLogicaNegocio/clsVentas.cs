using CapaEnlaceDatos;
using System;
using System.Data;

namespace CapaLogicaNegocio
{
    public class clsVentas
    {
        clsManejador M = new clsManejador();

        public int IdEmpleado { get; set; }
        public int IdCliente { get; set; }
        public string Serie { get; set; }
        public string NroComprobante { get; set; }
        public string TipoDocumento { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }

        public DataTable Listado()
        {
            return M.Listado("ListadoVentas", null);
        }
    }

    public class Venta
    {
        public int IdVenta { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCliente { get; set; }
        public string Serie { get; set; }
        public string NroComprobante { get; set; }
        public string TipoDocumento { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public string Tipofactura { get; set; }
        public decimal Restante { get; set; }
        public string NombreCliente { get; set; }
        public DateTime UltimaFechaPago { get; set; }
        public int borrador{ get; set; }
    }

}

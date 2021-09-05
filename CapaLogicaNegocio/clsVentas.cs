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
        public string rncCliente { get; set; }
        public string Direccion { get; set; }
        public int IdVenta { get; set; }
        public decimal Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioCompra { get; set; }
        public int IdProducto { get; set; }
        public decimal Igv { get; set; }
        public decimal SubTotal { get; set; }
        public clsVentas()
        {
            Cantidad = 0;
            Descripcion = "";
            PrecioVenta = 0;
            PrecioCompra = 0;
            IdVenta = 0;
            IdProducto = 0;
            Igv = 0;
            SubTotal = 0;
        }
        public clsVentas(int objIdVenta, int objCantidad, string objDescripcion, string objimei, decimal objPVenta, decimal objPCompra,
            int objIdProducto, decimal objIgv, decimal objSubTotal)
        {
            IdVenta = objIdVenta;
            Cantidad = objCantidad;
            Descripcion = objDescripcion;
            PrecioVenta = objPVenta;
            PrecioCompra = objPCompra;
            IdProducto = objIdProducto;
            Igv = objIgv;
            SubTotal = objSubTotal;
        }

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
        public string rncCliente { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public string Tipofactura { get; set; }
        public decimal Restante { get; set; }
        public string NombreCliente { get; set; }
        public DateTime UltimaFechaPago { get; set; }
        public int borrador { get; set; }
    }

}

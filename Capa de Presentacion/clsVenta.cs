﻿namespace Capa_de_Presentacion
{
    public class clsVenta
    {
        public int IdVenta { get; set; }
        public decimal Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioCompra { get; set; }
        public int IdProducto { get; set; }
        public decimal Igv { get; set; }
        public decimal SubTotal { get; set; }
        public clsVenta()
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
        public clsVenta(int objIdVenta, decimal objCantidad, string objDescripcion, string objimei, decimal objPVenta, decimal objPCompra,
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
    }
}

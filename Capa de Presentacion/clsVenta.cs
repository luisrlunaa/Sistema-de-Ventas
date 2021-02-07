using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_Presentacion
{
   public class clsVenta
    {
		public int IdVenta { get; set; }
		public int Cantidad { get; set; }
		public string Descripcion { get; set; }
		public string imei { get; set; }
		public decimal PrecioVenta { get; set; }
		public int IdProducto { get; set; }
		public decimal Igv { get; set; }
		public decimal SubTotal { get; set; }
		public clsVenta()
		{
			Cantidad = 0;
			Descripcion = "";
			PrecioVenta = 0;
			IdVenta = 0;
			IdProducto = 0;
			Igv = 0;
			imei = "";
			SubTotal = 0;
		}
		public clsVenta(int objIdVenta, int objCantidad, string objDescripcion, string objimei, decimal objPVenta,
			int objIdProducto, decimal objIgv, decimal objSubTotal, string Imei)
		{
			IdVenta = objIdVenta;
			Cantidad = objCantidad;
			Descripcion = objDescripcion;
			PrecioVenta = objPVenta;
			IdProducto = objIdProducto;
			Igv = objIgv;
			SubTotal = objSubTotal;
			imei = Imei;
		}
	}
}

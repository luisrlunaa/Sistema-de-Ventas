using CapaEnlaceDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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

        public List<Venta> GetListadoVentas(DateTime date, DateTime date1)
        {
            M.Desconectar();
            var newlist = new List<Venta>();
            try
            {
                //variable de tipo Sqlcommand
                SqlCommand comando = new SqlCommand();
                comando.Connection = M.conexion;
                //variable SqlDataReader para leer los datos
                SqlDataReader dr;
                //declaramos el comando para realizar la busqueda
                comando.CommandText = "select IdVenta,Direccion = COALESCE(Direccion, 'Entregado en Local'),rcnClient = COALESCE(rcnClient, 'Sin Rnc Cliente'),IdCliente= COALESCE(IdCliente, '0'),Serie,NroDocumento,TipoDocumento,FechaVenta,Total,IdEmpleado,Restante,TipoFactura,NombreCliente = COALESCE(NombreCliente, 'Sin Cliente'),borrado,UltimaFechaPago from venta where FechaVenta BETWEEN convert(datetime,CONVERT(varchar(10), @fecha, 103),103) AND convert(datetime,CONVERT(varchar(10), @fecha1, 103),103) order by IdVenta";
                //especificamos que es de tipo Text
                comando.CommandType = CommandType.Text;
                //sustituyendo variables por data
                comando.Parameters.AddWithValue("@fecha", date);
                comando.Parameters.AddWithValue("@fecha1", date1);
                //se abre la conexion
                M.Conectar();
                //a la variable DataReader asignamos  el la variable de tipo SqlCommand
                dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    Venta venta = new Venta();
                    venta.IdVenta = dr.GetInt32(dr.GetOrdinal("IdVenta"));
                    venta.IdEmpleado = dr.GetInt32(dr.GetOrdinal("IdEmpleado"));
                    venta.rncCliente = dr.GetString(dr.GetOrdinal("rcnClient"));
                    venta.Direccion = dr.GetString(dr.GetOrdinal("Direccion"));
                    venta.TipoDocumento = dr.GetString(dr.GetOrdinal("TipoDocumento"));
                    venta.NroComprobante = dr.GetString(dr.GetOrdinal("NroDocumento"));
                    venta.Total = dr.GetDecimal(dr.GetOrdinal("Total"));
                    venta.Tipofactura = dr.GetString(dr.GetOrdinal("Tipofactura"));
                    venta.Restante = dr.GetDecimal(dr.GetOrdinal("Restante"));
                    venta.FechaVenta = dr.GetDateTime(dr.GetOrdinal("FechaVenta"));
                    venta.NombreCliente = dr.GetString(dr.GetOrdinal("NombreCliente"));
                    venta.UltimaFechaPago = dr.GetDateTime(dr.GetOrdinal("UltimaFechaPago"));
                    venta.borrador = dr.GetBoolean(dr.GetOrdinal("borrado")) ? 1 : 0;

                    newlist.Add(venta);
                }
                M.Desconectar();

                return newlist;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return newlist;
            }
        }
    }

    public class Venta
    {
        public int IdVenta { get; set; }
        public int IdEmpleado { get; set; }
        public int IdCliente { get; set; }
        public string Serie { get; set; } = string.Empty;
        public string NroComprobante { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string rncCliente { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public string Tipofactura { get; set; } = string.Empty;
        public decimal Restante { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public DateTime UltimaFechaPago { get; set; }
        public int borrador { get; set; }
    }

}

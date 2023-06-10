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

        public int IdVenta { get; set; }
        public int Cantidad { get; set; }
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
            var newlist = new List<Venta>();
            var isSame = date.Date == date1.Date;
            var query = string.Empty;
            if (isSame)
                query = @"select venta.IdVenta,IdCliente= COALESCE(venta.IdCliente, 0),venta.Serie,venta.NroDocumento,venta.TipoDocumento,venta.FechaVenta,
                          COALESCE((select sum(DetalleVenta.Igv * DetalleVenta.Cantidad) from DetalleVenta where IdVenta = venta.IdVenta), 0 ) as Itbis,
                          COALESCE((select sum(DetalleVenta.PrecioUnitario * DetalleVenta.Cantidad) from DetalleVenta where IdVenta = venta.IdVenta), 0) as SubTotal, venta.Total,
                          venta.IdEmpleado,Restante = COALESCE(venta.Restante, 0),venta.TipoFactura,NombreCliente = COALESCE(venta.NombreCliente, 'Sin Cliente'),
                          COALESCE((select DNI from Cliente where IdCliente = venta.IdCliente), 'Sin Identidad') as Identidad,
                          Telefono = COALESCE(venta.Telefono, 'Sin Telefono'),Vehiculo = COALESCE(venta.Vehiculo, 'Sin Vehiculo'),venta.borrado,venta.UltimaFechaPago 
                          from venta where venta.FechaVenta = convert(datetime,CONVERT(varchar(10), @fecha, 103),103) order by venta.IdVenta";
            else
                query = @"select venta.IdVenta,IdCliente= COALESCE(venta.IdCliente, 0),venta.Serie,venta.NroDocumento,venta.TipoDocumento,venta.FechaVenta,
                          COALESCE((select sum(DetalleVenta.Igv * DetalleVenta.Cantidad) from DetalleVenta where IdVenta = venta.IdVenta), 0 ) as Itbis,
                          COALESCE((select sum(DetalleVenta.PrecioUnitario * DetalleVenta.Cantidad) from DetalleVenta where IdVenta = venta.IdVenta), 0) as SubTotal, venta.Total,
                          venta.IdEmpleado,Restante = COALESCE(venta.Restante, 0),venta.TipoFactura,NombreCliente = COALESCE(venta.NombreCliente, 'Sin Cliente'),
                          COALESCE((select DNI from Cliente where IdCliente = venta.IdCliente), 'Sin Identidad') as Identidad,
                          Telefono = COALESCE(venta.Telefono, 'Sin Telefono'),Vehiculo = COALESCE(venta.Vehiculo, 'Sin Vehiculo'),venta.borrado,venta.UltimaFechaPago 
                          from venta where venta.FechaVenta BETWEEN convert(datetime,CONVERT(varchar(10), @fecha, 103),103) 
                          AND convert(datetime,CONVERT(varchar(10), @fecha1, 103),103) order by IdVenta";

            M.Desconectar();
            try
            {
                //variable de tipo Sqlcommand
                SqlCommand comando = new SqlCommand();
                comando.Connection = M.conexion;
                //variable SqlDataReader para leer los datos
                SqlDataReader dr;
                //declaramos el comando para realizar la busqueda
                comando.CommandText = query;
                //especificamos que es de tipo Text
                comando.CommandType = CommandType.Text;
                //sustituyendo variables por data
                comando.Parameters.AddWithValue("@fecha", date);
                if (!isSame)
                    comando.Parameters.AddWithValue("@fecha1", date1);
                //se abre la conexion
                M.Conectar();
                //a la variable DataReader asignamos  el la variable de tipo SqlCommand
                dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    Venta venta = new Venta();
                    venta.IdVenta = dr.GetInt32(dr.GetOrdinal("IdVenta"));
                    venta.IdCliente = dr.GetInt32(dr.GetOrdinal("IdCliente"));
                    venta.IdEmpleado = dr.GetInt32(dr.GetOrdinal("IdEmpleado"));
                    venta.TipoDocumento = dr.GetString(dr.GetOrdinal("TipoDocumento"));
                    venta.NroComprobante = dr.GetString(dr.GetOrdinal("NroDocumento"));
                    venta.Itbis = dr.GetDecimal(dr.GetOrdinal("Itbis"));
                    venta.SubTotal = dr.GetDecimal(dr.GetOrdinal("SubTotal"));
                    venta.Total = dr.GetDecimal(dr.GetOrdinal("Total"));
                    venta.Tipofactura = dr.GetString(dr.GetOrdinal("Tipofactura"));
                    venta.Restante = dr.GetDecimal(dr.GetOrdinal("Restante"));
                    venta.FechaVenta = dr.GetDateTime(dr.GetOrdinal("FechaVenta"));
                    venta.NombreCliente = dr.GetString(dr.GetOrdinal("NombreCliente"));
                    venta.UltimaFechaPago = dr.GetDateTime(dr.GetOrdinal("UltimaFechaPago"));
                    venta.borrador = dr.GetBoolean(dr.GetOrdinal("borrado")) ? 1 : 0;
                    venta.Telefono = dr.GetString(dr.GetOrdinal("Telefono"));
                    venta.Vehiculo = dr.GetString(dr.GetOrdinal("Vehiculo"));
                    venta.Identidad = dr.GetString(dr.GetOrdinal("Identidad"));

                    venta.NombreCliente = string.IsNullOrWhiteSpace(venta.NombreCliente) ? "Sin Cliente" : venta.NombreCliente;
                    venta.Telefono = string.IsNullOrWhiteSpace(venta.Telefono) ? "Sin Telefono" : venta.Telefono;
                    venta.Vehiculo = string.IsNullOrWhiteSpace(venta.Vehiculo) ? "Sin Vehiculo" : venta.Vehiculo;

                    if(venta.SubTotal == venta.Total && venta.Itbis > 0)
                    {
                        venta.SubTotal = venta.Total - venta.Itbis;
                    }

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

        public List<Venta> GetListadoVentasporIdCliente(int id)
        {
            var newlist = new List<Venta>();
            var query = @"select IdVenta,IdCliente= COALESCE(IdCliente, '0'),Serie,NroDocumento,TipoDocumento,
                        FechaVenta,Total,IdEmpleado,Restante,TipoFactura,NombreCliente = COALESCE(NombreCliente, 'Sin Cliente'),
                        Telefono = COALESCE(Telefono, 'Sin Telefono'),Vehiculo = COALESCE(Vehiculo, 'Sin Vehiculo'),borrado,
                        ultimaFechaPago from venta where IdCliente = @id AND TipoFactura = 'Credito' And Restante > 0 AND borrado = 0 
                        order by IdVenta";

            M.Desconectar();
            try
            {
                //variable de tipo Sqlcommand
                SqlCommand comando = new SqlCommand();
                comando.Connection = M.conexion;
                //variable SqlDataReader para leer los datos
                SqlDataReader dr;
                //declaramos el comando para realizar la busqueda
                comando.CommandText = query;
                //especificamos que es de tipo Text
                comando.CommandType = CommandType.Text;
                //sustituyendo variables por data
                comando.Parameters.AddWithValue("@id", id);
                //se abre la conexion
                M.Conectar();
                //a la variable DataReader asignamos  el la variable de tipo SqlCommand
                dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    Venta venta = new Venta();
                    venta.IdVenta = dr.GetInt32(dr.GetOrdinal("IdVenta"));
                    venta.IdCliente = dr.GetInt32(dr.GetOrdinal("IdCliente"));
                    venta.IdEmpleado = dr.GetInt32(dr.GetOrdinal("IdEmpleado"));
                    venta.TipoDocumento = dr.GetString(dr.GetOrdinal("TipoDocumento"));
                    venta.NroComprobante = dr.GetString(dr.GetOrdinal("NroDocumento"));
                    venta.Total = dr.GetDecimal(dr.GetOrdinal("Total"));
                    venta.Tipofactura = dr.GetString(dr.GetOrdinal("Tipofactura"));
                    venta.Restante = dr.GetDecimal(dr.GetOrdinal("Restante"));
                    venta.FechaVenta = dr.GetDateTime(dr.GetOrdinal("FechaVenta"));
                    venta.NombreCliente = dr.GetString(dr.GetOrdinal("NombreCliente"));
                    venta.UltimaFechaPago = dr.GetDateTime(dr.GetOrdinal("UltimaFechaPago"));
                    venta.borrador = dr.GetBoolean(dr.GetOrdinal("borrado")) ? 1 : 0;
                    venta.Telefono = dr.GetString(dr.GetOrdinal("Telefono"));
                    venta.Vehiculo = dr.GetString(dr.GetOrdinal("Vehiculo"));

                    venta.NombreCliente = string.IsNullOrWhiteSpace(venta.NombreCliente) ? "Sin Cliente" : venta.NombreCliente;
                    venta.Telefono = string.IsNullOrWhiteSpace(venta.Telefono) ? "Sin Telefono" : venta.Telefono;
                    venta.Vehiculo = string.IsNullOrWhiteSpace(venta.Vehiculo) ? "Sin Vehiculo" : venta.Vehiculo;

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

        public List<Venta> GetListadoVentasporTelefono(string telefono)
        {
            var newlist = new List<Venta>();
            var query = @"select IdVenta,IdCliente= COALESCE(IdCliente, '0'),Serie,NroDocumento,TipoDocumento,
                        FechaVenta,Total,IdEmpleado,Restante,TipoFactura,NombreCliente = COALESCE(NombreCliente, 'Sin Cliente'),
                        Telefono = COALESCE(Telefono, 'Sin Telefono'),Vehiculo = COALESCE(Vehiculo, 'Sin Vehiculo'),borrado,
                        ultimaFechaPago from venta where Telefono = @telefono AND TipoFactura = 'Credito' And Restante > 0 AND borrado = 0 
                        order by IdVenta";

            M.Desconectar();
            try
            {
                //variable de tipo Sqlcommand
                SqlCommand comando = new SqlCommand();
                comando.Connection = M.conexion;
                //variable SqlDataReader para leer los datos
                SqlDataReader dr;
                //declaramos el comando para realizar la busqueda
                comando.CommandText = query;
                //especificamos que es de tipo Text
                comando.CommandType = CommandType.Text;
                //sustituyendo variables por data
                comando.Parameters.AddWithValue("@telefono", telefono);
                //se abre la conexion
                M.Conectar();
                //a la variable DataReader asignamos  el la variable de tipo SqlCommand
                dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    Venta venta = new Venta();
                    venta.IdVenta = dr.GetInt32(dr.GetOrdinal("IdVenta"));
                    venta.IdCliente = dr.GetInt32(dr.GetOrdinal("IdCliente"));
                    venta.IdEmpleado = dr.GetInt32(dr.GetOrdinal("IdEmpleado"));
                    venta.TipoDocumento = dr.GetString(dr.GetOrdinal("TipoDocumento"));
                    venta.NroComprobante = dr.GetString(dr.GetOrdinal("NroDocumento"));
                    venta.Total = dr.GetDecimal(dr.GetOrdinal("Total"));
                    venta.Tipofactura = dr.GetString(dr.GetOrdinal("Tipofactura"));
                    venta.Restante = dr.GetDecimal(dr.GetOrdinal("Restante"));
                    venta.FechaVenta = dr.GetDateTime(dr.GetOrdinal("FechaVenta"));
                    venta.NombreCliente = dr.GetString(dr.GetOrdinal("NombreCliente"));
                    venta.UltimaFechaPago = dr.GetDateTime(dr.GetOrdinal("UltimaFechaPago"));
                    venta.borrador = dr.GetBoolean(dr.GetOrdinal("borrado")) ? 1 : 0;
                    venta.Telefono = dr.GetString(dr.GetOrdinal("Telefono"));
                    venta.Vehiculo = dr.GetString(dr.GetOrdinal("Vehiculo"));

                    venta.NombreCliente = string.IsNullOrWhiteSpace(venta.NombreCliente) ? "Sin Cliente" : venta.NombreCliente;
                    venta.Telefono = string.IsNullOrWhiteSpace(venta.Telefono) ? "Sin Telefono" : venta.Telefono;
                    venta.Vehiculo = string.IsNullOrWhiteSpace(venta.Vehiculo) ? "Sin Vehiculo" : venta.Vehiculo;

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
        public string Serie { get; set; }
        public string NroComprobante { get; set; }
        public string TipoDocumento { get; set; }
        public DateTime? FechaVenta { get; set; }
        public decimal Total { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Itbis { get; set; }
        public string Tipofactura { get; set; }
        public decimal Restante { get; set; }
        public string NombreCliente { get; set; }
        public DateTime? UltimaFechaPago { get; set; }
        public int? borrador { get; set; }
        public string Vehiculo { get; set; }
        public string Telefono { get; set; }
        public string Identidad { get; set; }
    }

    public class Topay
    {
        public int IdVenta { get; set; }
        public decimal Total { get; set; }
        public string tipoNCF { get; set; }
        public string ncf { get; set; }
        public string cliente { get; set; }
        public decimal montoPagado { get; set; }
        public bool pagada { get; set; } = false;
        public bool abono { get; set; } = false;

    }
}

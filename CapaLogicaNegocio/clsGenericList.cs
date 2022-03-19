using CapaEnlaceDatos;
using CapaLogicaNegocio.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaLogicaNegocio
{
    public class clsGenericList
    {
        public static List<Venta> listVentas;
        public static List<Producto> listProducto;
        public static List<VentasPorCategoria> listVentasPorCategoria;
        public static List<int> idsVentas;

        #region Variables listado de ventas
        public static decimal totalPendiente { get; set; }
        public static decimal totalGanancia { get; set; }
        public static decimal totalVendido { get; set; }
        #endregion

        #region Calculos
        public static List<VentasPorCategoria> ListaPorCatergoria(DateTime fecha1, DateTime fecha2, int borrador)
        {
            var listVentCateg = new List<VentasPorCategoria>();
            clsManejador M = new clsManejador();
            try
            {
                string sql = "SELECT Categoria.Descripcion AS CategoryOfProducts,sum(DetalleVenta.Cantidad) AS CantidadOfProducts,sum(DetalleVenta.SubTotal) AS PrecioOfProducts" +
                    " FROM DetalleVenta INNER JOIN Producto ON DetalleVenta.IdProducto = Producto.IdProducto INNER JOIN Categoria ON Producto.IdCategoria = Categoria.IdCategoria " +
                    "INNER JOIN Venta ON DetalleVenta.IdVenta = Venta.IdVenta  where venta.FechaVenta BETWEEN convert(datetime, CONVERT(varchar(10),@fecha1, 103), 103) AND " +
                    "convert(datetime, CONVERT(varchar(10),@fecha2, 103), 103) and dbo.Venta.borrado=" + borrador + "group by Categoria.Descripcion ORDER BY sum(DetalleVenta.Cantidad) DESC";

                SqlCommand cmd1 = new SqlCommand(sql, M.conexion);
                cmd1.Parameters.AddWithValue("@fecha1", fecha1);
                cmd1.Parameters.AddWithValue("@fecha2", fecha2);

                DataTable dtPC = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(dtPC);

                foreach (DataRow reader in dtPC.Rows)
                {
                    VentasPorCategoria ventaPC = new VentasPorCategoria();
                    ventaPC.PrecioOfProducts = reader["PrecioOfProducts"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["PrecioOfProducts"]);
                    ventaPC.CantidadOfProducts = reader["CantidadOfProducts"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["CantidadOfProducts"]);
                    ventaPC.CategoryOfProducts = reader["CategoryOfProducts"] == DBNull.Value ? string.Empty : reader["CategoryOfProducts"].ToString();
                    listVentCateg.Add(ventaPC);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return listVentCateg;
        }


        public static decimal Ganancias(List<int> ventasIds)
        {
            decimal ganancia = 0;
            try
            {
                if (ventasIds is null || ventasIds.Count == 0)
                {
                    ventasIds = idsVentas;
                }

                if (ventasIds.Count > 0 && listVentas.Count > 0)
                {
                    clsManejador M = new clsManejador();
                    foreach (var id in ventasIds)
                    {
                        string cadSql = $"select Sum(GananciaVenta) as ganancia from DetalleVenta where DetalleVenta.IdVenta ={id} group by DetalleVenta.IdVenta";
                        SqlCommand comando = new SqlCommand(cadSql, M.conexion);
                        M.Conectar();
                        SqlDataReader leer = comando.ExecuteReader();
                        if (leer.Read() == true)
                        {
                            var monto = Convert.ToDecimal(leer["ganancia"]) > 0 ? Convert.ToDecimal(leer["ganancia"]) : 0;
                            ganancia += monto;
                        }
                        M.Desconectar();
                    }
                }
                return ganancia;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return ganancia;
        }
        #endregion
    }

    public class TempData
    {
        public static List<Venta> tempSalesData { get; set; }
        public static DateTime DateIn { get; set; }
    }
}

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
            if (listVentasPorCategoria is null)
            {
                clsManejador M = new clsManejador();
                listVentasPorCategoria = new List<VentasPorCategoria>();
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

                        listVentasPorCategoria.Add(ventaPC);
                    }
                    return listVentasPorCategoria;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            return new List<VentasPorCategoria>();
        }

        public static decimal Ganancias()
        {
            try
            {
                if (idsVentas.Count > 0 && clsGenericList.listVentas.Count > 0)
                {
                    clsManejador M = new clsManejador();
                    foreach (var item in idsVentas)
                    {
                        string cadSql = $"select Sum(GananciaVenta) as ganancia from DetalleVenta where DetalleVenta.IdVenta ={item} group by DetalleVenta.IdVenta";
                        SqlCommand comando = new SqlCommand(cadSql, M.conexion);
                        M.Conectar();
                        SqlDataReader leer = comando.ExecuteReader();
                        if (leer.Read() == true)
                        {
                            totalGanancia += (Convert.ToDecimal(leer["ganancia"]));
                        }
                        M.Desconectar();
                    }
                }
                return totalGanancia;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return totalGanancia;
        }
        #endregion
    }
}

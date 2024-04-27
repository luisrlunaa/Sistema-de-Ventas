using CapaEnlaceDatos;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaLogicaNegocio
{
    public class clsDetalleVenta
    {
        clsManejador M = new clsManejador();
        public int IdProducto { get; set; }
        public int IdVenta { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PUnitario { get; set; }
        public decimal Igv { get; set; }
        public decimal SubTotal { get; set; }

        public string RegistrarDetalleVenta()
        {
            List<clsParametro> lst = new List<clsParametro>();
            string Mensaje = "";
            try
            {
                lst.Add(new clsParametro("@IdProducto", IdProducto));
                lst.Add(new clsParametro("@IdVenta", IdVenta));
                lst.Add(new clsParametro("@Cantidad", Cantidad));
                lst.Add(new clsParametro("@PrecioUnitario", PUnitario));
                lst.Add(new clsParametro("@Igv", Igv));
                lst.Add(new clsParametro("@SubTotal", SubTotal));
                lst.Add(new clsParametro("@Mensaje", "", SqlDbType.VarChar, ParameterDirection.Output, 100));
                M.EjecutarSP("RegistrarDetalleVenta", ref lst);
                Mensaje = lst[7].Valor.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Mensaje;
        }
    }
}

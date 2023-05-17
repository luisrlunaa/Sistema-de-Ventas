using CapaEnlaceDatos;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaLogicaNegocio
{
    public class clsCargo
    {
        private clsManejador M = new clsManejador();

        int m_IdCargo;
        string m_Descripcion;

        public int IdCargo
        {
            get { return m_IdCargo; }
            set { m_IdCargo = value; }
        }

        public string Descripcion
        {
            get { return m_Descripcion; }
            set { m_Descripcion = value; }
        }

        public DataTable Listar()
        {
            return M.Listado("ListarCargo", null);
        }

        public string RegistrarCargo()
        {
            string Mensaje = "";
            List<clsParametro> lst = new List<clsParametro>();
            try
            {
                lst.Add(new clsParametro("@Descripcion", m_Descripcion));
                lst.Add(new clsParametro("@Mensaje", "", SqlDbType.VarChar, ParameterDirection.Output, 50));
                M.EjecutarSP("RegistrarCargo", ref lst);
                Mensaje = lst[1].Valor.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Mensaje;
        }

        public string ActualizarCargo()
        {
            string Mensaje = "";
            List<clsParametro> lst = new List<clsParametro>();
            try
            {
                lst.Add(new clsParametro("@IdCargo", m_IdCargo));
                lst.Add(new clsParametro("@Descripcion", m_Descripcion));
                lst.Add(new clsParametro("@Mensaje", "", SqlDbType.VarChar, ParameterDirection.Output, 100));
                M.EjecutarSP("ActualizarCargo", ref lst);
                Mensaje = lst[2].Valor.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Mensaje;
        }

        public DataTable BusquedaCargo(string objDescripcion)
        {
            DataTable dt = new DataTable();
            List<clsParametro> lst = new List<clsParametro>();
            try
            {
                lst.Add(new clsParametro("@Descripcion", objDescripcion));
                return dt = M.Listado("BuscarCargo", lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using CapaEnlaceDatos;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaLogicaNegocio
{
    public class clsCategoria
    {
        private clsManejador M = new clsManejador();

        private int m_IdC;
        private int m_IdCategoria;
        private string m_Descripcion;

        public int IdC
        {
            get { return m_IdC; }
            set { m_IdC = value; }
        }
        public int IdCategoria
        {
            get { return m_IdCategoria; }
            set { m_IdCategoria = value; }
        }

        public string Descripcion
        {
            get { return m_Descripcion; }
            set { m_Descripcion = value; }
        }

        public DataTable Listar()
        {
            return M.Listado("ListarCategoria", null);
        }

        public DataTable ListarC()
        {
            return M.Listado("ListartipoGoma", null);
        }

        public string RegistrarCategoria()
        {
            List<clsParametro> lst = new List<clsParametro>();
            string Mensaje = "";
            try
            {
                lst.Add(new clsParametro("@Descripcion", m_Descripcion));
                lst.Add(new clsParametro("@Mensaje", "", SqlDbType.VarChar, ParameterDirection.Output, 50));
                M.EjecutarSP("RegistrarCategoria", ref lst);
                Mensaje = lst[1].Valor.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Mensaje;
        }

        public DataTable BuscarCategoria(string objDescripcin)
        {
            List<clsParametro> lst = new List<clsParametro>();
            DataTable dt = new DataTable();
            try
            {
                lst.Add(new clsParametro("@Datos", objDescripcin));
                return dt = M.Listado("BuscarCategoria", lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ActualizarCategoria()
        {
            List<clsParametro> lst = new List<clsParametro>();
            string Mensaje = "";
            try
            {
                lst.Add(new clsParametro("@IdC", m_IdC));
                lst.Add(new clsParametro("@Descripcion", m_Descripcion));
                lst.Add(new clsParametro("@Mensaje", "", SqlDbType.VarChar, ParameterDirection.Output, 50));
                M.EjecutarSP("ActualizarCategoria", ref lst);
                Mensaje = lst[2].Valor.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Mensaje;
        }
    }
}

using CapaEnlaceDatos;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaLogicaNegocio
{
    public class clsCliente
    {
        private clsManejador M = new clsManejador();

        private string m_Dni;
        private string m_Apellidos;
        private string m_Nombres;
        private string m_Direccion;
        private string m_Telefono;
        private int m_estado;


        public string Dni
        {
            get { return m_Dni; }
            set { m_Dni = value; }
        }

        public string Apellidos
        {
            get { return m_Apellidos; }
            set { m_Apellidos = value; }
        }

        public string Nombres
        {
            get { return m_Nombres; }
            set { m_Nombres = value; }
        }

        public string Telefono
        {
            get { return m_Telefono; }
            set { m_Telefono = value; }
        }

        public string Direccion
        {
            get { return m_Direccion; }
            set { m_Direccion = value; }
        }

        public int estado
        {
            get { return m_estado; }
            set { m_estado = value; }
        }

        public DataTable Listado()
        {
            return M.Listado("ListarClientes", null);
        }

        public DataTable BuscarCliente(string objDatos)
        {
            DataTable dt = new DataTable();
            List<clsParametro> lst = new List<clsParametro>();
            lst.Add(new clsParametro("@Datos", objDatos));
            return dt = M.Listado("FiltrarDatosCliente", lst);
        }

        public string RegistrarCliente()
        {
            List<clsParametro> lst = new List<clsParametro>();
            string Mensaje = "";
            try
            {
                lst.Add(new clsParametro("@DNI", m_Dni));
                lst.Add(new clsParametro("@Apellidos", m_Apellidos));
                lst.Add(new clsParametro("@Nombres", m_Nombres));
                lst.Add(new clsParametro("@Direccion", m_Direccion));
                lst.Add(new clsParametro("@Telefono", m_Telefono));
                lst.Add(new clsParametro("@estado", m_estado));
                lst.Add(new clsParametro("@Mensaje", "", SqlDbType.VarChar, ParameterDirection.Output, 50));
                M.EjecutarSP("RegistrarCliente", ref lst);
                Mensaje = lst[5].Valor.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Mensaje;
        }

        public string ActualizarCliente()
        {
            List<clsParametro> lst = new List<clsParametro>();
            string Mensaje = "";
            try
            {
                lst.Add(new clsParametro("@DNI", m_Dni));
                lst.Add(new clsParametro("@Apellidos", m_Apellidos));
                lst.Add(new clsParametro("@Nombres", m_Nombres));
                lst.Add(new clsParametro("@Direccion", m_Direccion));
                lst.Add(new clsParametro("@Telefono", m_Telefono));
                lst.Add(new clsParametro("@estado", m_estado));
                lst.Add(new clsParametro("@Mensaje", "", SqlDbType.VarChar, ParameterDirection.Output, 50));
                M.EjecutarSP("ActualizarCliente", ref lst);
                Mensaje = lst[5].Valor.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Mensaje;
        }
    }
}

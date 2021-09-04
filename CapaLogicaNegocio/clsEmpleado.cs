using CapaEnlaceDatos;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaLogicaNegocio
{

    public class clsEmpleado
    {
        clsManejador M = new clsManejador();

        public int IdCargo { get; set; }
        public String Dni { get; set; }
        public String Apellidos { get; set; }
        public String Nombres { get; set; }
        public Char Sexo { get; set; }
        public DateTime FechaNac { get; set; }
        public String Direccion { get; set; }
        public Char EstadoCivil { get; set; }



        public String MantenimientoEmpleados()
        {
            List<clsParametro> lst = new List<clsParametro>();
            String Mensaje = "";
            try
            {
                lst.Add(new clsParametro("@IdCargo", IdCargo));
                lst.Add(new clsParametro("@Dni", Dni));
                lst.Add(new clsParametro("@Apellidos", Apellidos));
                lst.Add(new clsParametro("@Nombres", Nombres));
                lst.Add(new clsParametro("@Sexo", Sexo));
                lst.Add(new clsParametro("@FechaNac", FechaNac.ToString("dd/MM/yyyy")));
                lst.Add(new clsParametro("@Direccion", Direccion));
                lst.Add(new clsParametro("@EstadoCivil", EstadoCivil));

                lst.Add(new clsParametro("@Mensaje", "", SqlDbType.VarChar, ParameterDirection.Output, 100));
                M.EjecutarSP("MantenimientoEmpleados", ref lst);
                return Mensaje = lst[9].Valor.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ListadoEmpleados()
        {
            return M.Listado("ListadoEmpleados", null);
        }

        public DataTable BuscarEmpleado(String objDatos)
        {
            DataTable dt = new DataTable();
            List<clsParametro> lst = new List<clsParametro>();
            lst.Add(new clsParametro("@Datos", objDatos));
            return dt = M.Listado("Buscar_Empleado", lst);
        }

    }
}

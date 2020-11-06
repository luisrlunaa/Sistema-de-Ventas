using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using CapaEnlaceDatos;

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
    }

}

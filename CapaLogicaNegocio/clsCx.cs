using System.Data.SqlClient;

namespace CapaLogicaNegocio
{
    public class clsCx
    {
        public string conet = @"Data Source=DESKTOP-9KOT2KP\SQLEXPRESS;Initial Catalog=SalesSystemGomera;Integrated Security=True";
        public SqlConnection conexion = new SqlConnection(@"Data Source=DESKTOP-9KOT2KP\SQLEXPRESS;Initial Catalog=SalesSystemGomera;Integrated Security=True");
    }
}

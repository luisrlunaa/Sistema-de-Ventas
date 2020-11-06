using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace CapaLogicaNegocio
{
	public class clsCx
	{
		public string conet = "Data Source=.;Initial Catalog=SalesSystem;Integrated Security=True";
		public SqlConnection conexion = new SqlConnection("Data Source=.;Initial Catalog=SalesSystem;Integrated Security=True");
	}
}

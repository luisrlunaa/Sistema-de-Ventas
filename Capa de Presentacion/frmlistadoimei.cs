using CapaLogicaNegocio;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class frmlistadoimei : Form
	{
		clsCx Cx = new clsCx();
		public frmlistadoimei()
        {
            InitializeComponent();
        }

        private void frmlistadoimei_Load(object sender, EventArgs e)
        {
			llenar_data(txtIdP.Text);
		}
		public void llenar_data(string id)
		{
			if (id != "")
			{
				//declaramos la cadena  de conexion
				string cadenaconexion = Cx.conet;
				//variable de tipo Sqlconnection
				SqlConnection con = new SqlConnection();
				//variable de tipo Sqlcommand
				SqlCommand comando = new SqlCommand();
				//variable SqlDataReader para leer los datos
				SqlDataReader dr;
				con.ConnectionString = cadenaconexion;
				comando.Connection = con;
				//declaramos el comando para realizar la busqueda
				comando.CommandText = "select * from ImeiList where IdProducto =" + id + "and activo=" + 1; 
				//especificamos que es de tipo Text
				comando.CommandType = CommandType.Text;
				//se abre la conexion
				con.Open();
				//limpiamos los renglones de la datagridview
				dgvimei.Rows.Clear();
				//a la variable DataReader asignamos  el la variable de tipo SqlCommand
				dr = comando.ExecuteReader();
				while (dr.Read())
				{
					//variable de tipo entero para ir enumerando los la filas del datagridview
					int renglon = dgvimei.Rows.Add();
					// especificamos en que fila se mostrará cada registro
					// nombredeldatagrid.filas[numerodefila].celdas[nombrdelacelda].valor=\

					dgvimei.Rows[renglon].Cells["id"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("idproducto")));
					dgvimei.Rows[renglon].Cells["IMEI"].Value = dr.GetString(dr.GetOrdinal("IMEI"));
					dgvimei.Rows[renglon].Cells["idImei"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("idImei")));
					dgvimei.Rows[renglon].Cells["Fecha"].Value = dr.GetDateTime(dr.GetOrdinal("fechaingreso"));
				}
				con.Close();
			}
		}

        private void dgvimei_DoubleClick(object sender, EventArgs e)
        {
			Program.Imei = dgvimei.CurrentRow.Cells["IMEI"].Value.ToString();
			Program.idImei = dgvimei.CurrentRow.Cells["idImei"].Value.ToString();
			Program.abiertoimei = false;
			this.Close();
		}

        private void label18_Click(object sender, EventArgs e)
        {
			Program.abiertoimei = false;
			this.Close();
        }
    }
}

using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using CapaLogicaNegocio;

namespace Capa_de_Presentacion
{
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
    public partial class FrmBuscarAlineacionyBalanceo : DevComponents.DotNetBar.Metro.MetroForm
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
	{
		public FrmBuscarAlineacionyBalanceo()
		{
			InitializeComponent();
        }

        clsCx Cx = new clsCx();
		public void cargardata()
		{
			//declaramos la cadena  de conexion
			string cadenaconexion = Cx.conet;
			//variable de tipo Sqlconnection
			SqlConnection conexion = new SqlConnection();
			//variable de tipo Sqlcommand
			SqlCommand comando = new SqlCommand();
			//variable SqlDataReader para leer los datos
			SqlDataReader dr;
			conexion.ConnectionString = cadenaconexion;
			comando.Connection = conexion;
			if(txtBuscar.Text != null && txtBuscar.Text != "")
            {
				//declaramos el comando para realizar la busqueda
				comando.CommandText = "select * from AlineamientoYBalanceo where vehiculo like '%" + txtBuscar.Text.ToUpper() + "%' And tipoDeTrabajo like '%" + cbtipo.Text.ToUpper() + "%'";
			}
			else
            {
				comando.CommandText = "select * from AlineamientoYBalanceo";
			}
			//especificamos que es de tipo Text
			comando.CommandType = CommandType.Text;
			//se abre la conexion
			conexion.Open();
			//limpiamos los renglones de la datagridview
			dataGridView1.Rows.Clear();
			//a la variable DataReader asignamos  el la variable de tipo SqlCommand
			dr = comando.ExecuteReader();
			while (dr.Read())
			{
				//variable de tipo entero para ir enumerando los la filas del datagridview
				int renglon = dataGridView1.Rows.Add();

				// especificamos en que fila se mostrará cada registro
				// nombredeldatagrid.filas[numerodefila].celdas[nombrdelacelda].valor=\
				dataGridView1.Rows[renglon].Cells["id"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id")));
				dataGridView1.Rows[renglon].Cells["tipoDeTrabajo"].Value =dr.GetString(dr.GetOrdinal("tipoDeTrabajo"));
				dataGridView1.Rows[renglon].Cells["vehiculo"].Value = dr.GetString(dr.GetOrdinal("vehiculo"));
				dataGridView1.Rows[renglon].Cells["AroGoma"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("AroGoma")));
				dataGridView1.Rows[renglon].Cells["precio"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("precio")));
				dataGridView1.Rows[renglon].Cells["nota"].Value = dr.GetString(dr.GetOrdinal("nota"));
				dataGridView1.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("fecha"));

                double total = 0;
				total += Convert.ToDouble(dataGridView1.Rows[renglon].Cells["precio"].Value);
				txttotalG.Text = Convert.ToString(total);
			}
			conexion.Close();
		}

		private void dataGridView1_DoubleClick(object sender, EventArgs e)
		{
			var marca= dataGridView1.CurrentRow.Cells["vehiculo"].Value.ToString();
			string cadena = marca;
			char delimitador = ' ';
			string[] valores = cadena.Split(delimitador);

			Program.descripcion = dataGridView1.CurrentRow.Cells["tipoDeTrabajo"].Value.ToString();
			Program.marca = valores[0].Trim();
			Program.modelo = valores[1].Trim();
			Program.Aros= dataGridView1.CurrentRow.Cells["AroGoma"].Value.ToString(); 
			Program.total= Convert.ToDecimal(dataGridView1.CurrentRow.Cells["precio"].Value.ToString());
			Program.nota= dataGridView1.CurrentRow.Cells["nota"].Value.ToString();
			Program.Id= Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
			this.Close();
		}

		private void label2_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
			if (e.KeyChar == 13)
			{
				cargardata();
			}
		}
		public void cargar_combo_Tipo(ComboBox tipo)
		{
			SqlCommand cm = new SqlCommand("CARGARcomboTipotrabajo", Cx.conexion);
			cm.CommandType = CommandType.StoredProcedure;
			SqlDataAdapter da = new SqlDataAdapter(cm);
			DataTable dt = new DataTable();
			da.Fill(dt);

			tipo.DisplayMember = "descripcion";
			tipo.ValueMember = "id";
			tipo.DataSource = dt;
		}

        private void frmBuscarAlineacionyBalanceo_Load(object sender, EventArgs e)
        {
			cargardata();
			cargar_combo_Tipo(cbtipo);
			cbtipo.SelectedIndex = 0;
		}
    }
}

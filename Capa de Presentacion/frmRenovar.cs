using CapaEnlaceDatos;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class frmRenovar : Form
    {
        public frmRenovar()
        {
            InitializeComponent();
        }

        clsManejador M = new clsManejador();
        FrmLogin Login = new FrmLogin();
        private void frmRenovar_Load(object sender, EventArgs e)
        {
            Licencia();
            licienciaPre();
        }

        public string empresa, proveedor, palabraclave, secuenciaini, secuenciacent, secuenciafin;

        private void label6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string licenciaAnterior = "";
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            M.Desconectar();
            var nuevafecha = DateTime.Today.AddYears(50);
            if (licenciaAnterior == "")
            {
                licenciaAnterior = Guid.NewGuid().ToString();
            }

            if (txtrenovar.Text != "" && txtrenovar.Text != null)
            {
                if (txtrenovar.Text == txtlicencia.Text && licenciaAnterior != txtrenovar.Text)
                {
                    using (SqlCommand cmdup = new SqlCommand("ActualizarLicencia", M.conexion))
                    {
                        cmdup.CommandType = CommandType.StoredProcedure;
                        cmdup.Parameters.Add("@licencia", SqlDbType.NVarChar).Value = licenciaAnterior;
                        cmdup.Parameters.Add("@licenciaNew", SqlDbType.NVarChar).Value = txtlicencia.Text;
                        cmdup.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = nuevafecha;

                        M.Conectar();
                        cmdup.ExecuteNonQuery();
                        M.Desconectar();

                        Login.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Numero de Licencia Incorrecta");
                }
            }
        }

        public void Licencia()
        {
            M.Desconectar();
            int num = 0;
            string numfin = "";
            string cadSql = "select * from Licencia";

            SqlCommand comando = new SqlCommand(cadSql, M.conexion);
            M.Conectar();

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read())
            {
                empresa = leer["empresa"].ToString();
                proveedor = leer["proveedor"].ToString();
                palabraclave = leer["palabraclave"].ToString();

                secuenciaini = leer["secuenciaini"].ToString().PadLeft(3, '0');
                secuenciacent = leer["secuenciacent"].ToString().PadLeft(2, '0');
                secuenciafin = leer["secuenciafin"].ToString().PadLeft(4, '0');

                num = Convert.ToInt32(leer["secuenciafin"]);
                numfin = (num + 1).ToString();

                txtlicencia.Text = empresa + secuenciaini + secuenciacent + secuenciafin + proveedor + palabraclave + numfin;
            }
            M.Desconectar();
        }

        public void licienciaPre()
        {
            M.Desconectar();
            string cadSql = "select top(1) Licencia_Post  from NomEmp order by idEmp desc";

            SqlCommand comando = new SqlCommand(cadSql, M.conexion);
            M.Conectar();

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read())
            {
                licenciaAnterior = leer["Licencia_Post"].ToString();
            }
            M.Desconectar();
        }
    }
}

using CapaLogicaNegocio;
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
        clsCx Cx = new clsCx();
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
            var nuevafecha = DateTime.Today.AddYears(1);
            if (licenciaAnterior == "")
            {
                licenciaAnterior = (new Guid()).ToString();
            }

            if (txtrenovar.Text != "" && txtrenovar.Text != null)
            {
                if (txtrenovar.Text == txtlicencia.Text && licenciaAnterior != txtrenovar.Text)
                {
                    using (SqlConnection con = new SqlConnection(Cx.conet))
                    {
                        using (SqlCommand cmdup = new SqlCommand("ActualizarLicencia", con))
                        {
                            cmdup.CommandType = CommandType.StoredProcedure;
                            cmdup.Parameters.Add("@licencia", SqlDbType.NVarChar).Value = licenciaAnterior;
                            cmdup.Parameters.Add("@licenciaNew", SqlDbType.NVarChar).Value = txtlicencia.Text;
                            cmdup.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = nuevafecha;

                            con.Open();
                            cmdup.ExecuteNonQuery();
                            con.Close();

                            Login.Show();
                            this.Hide();
                        }
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
            int num = 0;
            string numfin = "";
            string cadSql = "select * from Licencia";

            SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);
            Cx.conexion.Open();

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
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
            Cx.conexion.Close();
        }

        public void licienciaPre()
        {
            string cadSql = "select top(1) Licencia_Post  from NomEmp order by idEmp desc";

            SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);
            Cx.conexion.Open();

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
            {
                licenciaAnterior = leer["Licencia_Post"].ToString();
            }
            Cx.conexion.Close();
        }
    }
}

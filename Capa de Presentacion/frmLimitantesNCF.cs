using CapaEnlaceDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class frmLimitantesNCF : DevComponents.DotNetBar.Metro.MetroForm
    {
        public frmLimitantesNCF()
        {
            InitializeComponent();
        }

        clsManejador Cx = new clsManejador();
        private void frmLimitantesNCF_Load(object sender, EventArgs e)
        {
            actualzarestadoscomprobantes();
            llenar_data_ncf();
            llenar_data_comprobante();
        }

        public void llenardatoscomprobantes(int id)
        {
            Cx.Desconectar();
            Cx.Conectar();
            string sql = "SELECT * FROM ncf INNER JOIN Comprobantes ON ncf.id_ncf = Comprobantes.id_comprobante where ncf.id_ncf=@id";
            SqlCommand cmd = new SqlCommand(sql, Cx.conexion);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reade = cmd.ExecuteReader();
            if (reade.Read())
            {
                txtfinal.Text = Convert.ToString(reade["secuenciaF"]);
                txtinicio.Text = Convert.ToString(reade["secuenciaIni"]);

                dtpinicio.Value = Convert.ToDateTime(reade["fecha_inicio"]);
                dtpfinal.Value = Convert.ToDateTime(reade["fecha_final"]);
            }
            Cx.Desconectar();
        }

        public void actualzarestadoscomprobantes()
        {
            Cx.Desconectar();
            var listaidint = new List<int>();

            for (int i = 0; i <= 9; i++)
            {
                listaidint.Add(i);
            }

            foreach (var item in listaidint)
            {
                Cx.Conectar();
                string sql = "SELECT * FROM ncf INNER JOIN Comprobantes ON ncf.id_ncf = Comprobantes.id_comprobante where ncf.id_ncf=@id order by id_ncf";
                SqlCommand cmd = new SqlCommand(sql, Cx.conexion);
                cmd.Parameters.AddWithValue("@id", item);

                SqlDataReader reade = cmd.ExecuteReader();
                if (reade.Read())
                {
                    int secuf = Convert.ToInt32(reade["secuenciaF"]);
                    int secui = Convert.ToInt32(reade["secuenciaIni"]);

                    DateTime fechaini = DateTime.Today;
                    DateTime fechafin = Convert.ToDateTime(reade["fecha_final"]);

                    if (secui > secuf || fechaini >= fechafin)
                    {
                        Cx.Desconectar();
                        using (SqlCommand cmdup = new SqlCommand("UpdateState", Cx.conexion))
                        {
                            cmdup.CommandType = CommandType.StoredProcedure;
                            cmdup.Parameters.Add("@id", SqlDbType.Int).Value = item;
                            Cx.Conectar(); ;
                            cmdup.ExecuteNonQuery();
                            Cx.Desconectar();
                        }
                    }
                }
                Cx.Desconectar();
            }
        }
        public void llenar_data_ncf()
        {
            Cx.Desconectar();
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            comando.Connection = Cx.conexion;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "SELECT * FROM ncf INNER JOIN Comprobantes ON ncf.id_ncf = Comprobantes.id_comprobante order by ncf.id_ncf";
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            Cx.Conectar(); ;
            //limpiamos los renglones de la datagridview
            data_ncf.Rows.Clear();
            //a la variable DataReader asignamos  el la variable de tipo SqlCommand
            dr = comando.ExecuteReader();
            //el ciclo while se ejecutará mientras lea registros en la tabla
            while (dr.Read())
            {
                //variable de tipo entero para ir enumerando los la filas del datagridview
                int renglon = data_ncf.Rows.Add();
                // especificamos en que fila se mostrará cada registro
                // nombredeldatagrid.filas[numerodefila].celdas[nombrdelacelda].valor=\

                data_ncf.Rows[renglon].Cells["id_ncf"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id_ncf")));
                data_ncf.Rows[renglon].Cells["tipo"].Value = dr.GetString(dr.GetOrdinal("descripcion_ncf"));
                bool activo = dr.GetBoolean(dr.GetOrdinal("Activo"));
                if (activo == true)
                {
                    data_ncf.Rows[renglon].Cells["Activo"].Value = "No";
                }
                else
                {
                    data_ncf.Rows[renglon].Cells["Activo"].Value = "Si";
                }
            }
            Cx.Desconectar();
        }

        public void llenar_data_comprobante()
        {
            Cx.Desconectar();
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            comando.Connection = Cx.conexion;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "SELECT  * from Comprobantes";
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            Cx.Conectar(); ;
            //limpiamos los renglones de la datagridview
            data_comprobante.Rows.Clear();
            //a la variable DataReader asignamos  el la variable de tipo SqlCommand
            dr = comando.ExecuteReader();
            //el ciclo while se ejecutará mientras lea registros en la tabla
            while (dr.Read())
            {
                //variable de tipo entero para ir enumerando los la filas del datagridview
                int renglon = data_comprobante.Rows.Add();
                // especificamos en que fila se mostrará cada registro
                // nombredeldatagrid.filas[numerodefila].celdas[nombrdelacelda].valor=\

                data_comprobante.Rows[renglon].Cells["id_comprobante"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id_comprobante")));
                data_comprobante.Rows[renglon].Cells["secuenciai"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("secuenciai")));
                data_comprobante.Rows[renglon].Cells["secuenciaf"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("secuenciaf")));
                data_comprobante.Rows[renglon].Cells["fecha_inicio"].Value = dr.GetDateTime(dr.GetOrdinal("fecha_inicio"));
                data_comprobante.Rows[renglon].Cells["fecha_final"].Value = dr.GetDateTime(dr.GetOrdinal("fecha_final"));
            }
            Cx.Desconectar();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            Cx.Desconectar();
            using (SqlCommand cmd = new SqlCommand("limitantes", Cx.conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id_comprobante", SqlDbType.Int).Value = Convert.ToInt32(txtid.Text);
                cmd.Parameters.Add("@secuenciai", SqlDbType.NVarChar).Value = txtinicio.Text;
                cmd.Parameters.Add("@secuenciaf", SqlDbType.NVarChar).Value = txtfinal.Text;
                cmd.Parameters.Add("@fecha_inicio", SqlDbType.DateTime).Value = Convert.ToDateTime(dtpinicio.Text);
                cmd.Parameters.Add("@fecha_final", SqlDbType.DateTime).Value = Convert.ToDateTime(dtpfinal.Text);

                Cx.Conectar();
                cmd.ExecuteNonQuery();
                Cx.Desconectar();

                MessageBox.Show("Aplicado Correctamente");
                llenar_data_comprobante();
                llenar_data_ncf();
            }
        }

        public void seleccion_data()
        {
            txtid.Text = data_ncf.CurrentRow.Cells[0].Value.ToString();
            lblcomp.Text = data_ncf.CurrentRow.Cells[1].Value.ToString();

            if (txtid.Text != "")
            {
                int id = Convert.ToInt32(txtid.Text);
                llenardatoscomprobantes(id);
            }
        }

        private void txtinicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Program.abiertosecundario = false;
            Program.abierto = false;
            this.Close();
        }

        private void data_ncf_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (data_ncf.Columns[e.ColumnIndex].Name == "Activo")
            {
                if (Convert.ToString(e.Value).ToLower() == "no")
                {
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.Red;
                }

                if (Convert.ToString(e.Value).ToLower() == "si")
                {
                    e.CellStyle.ForeColor = Color.Black;
                    e.CellStyle.BackColor = Color.White;
                }
            }
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void frmLimitantesNCF_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void data_ncf_DoubleClick_1(object sender, EventArgs e)
        {
            seleccion_data();
        }
    }
}

using CapaEnlaceDatos;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class frmRecuperar : Form
    {
        public frmRecuperar()
        {
            InitializeComponent();
        }
        clsManejador Cx = new clsManejador();
        private void label6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void limpiar()
        {
            txtCed.Clear();
            txtCon.Clear();
            txtConf.Clear();
            txtUsu.Clear();
            txtUsuarioCed.Clear();
        }
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtCed.Text.Trim() != "")
            {
                if (txtUsu.Text.Trim() != "")
                {
                    if (txtCon.Text.Trim() != "")
                    {
                        if (txtConf.Text == txtCon.Text)
                        {
                            Cx.Desconectar();
                            using (SqlCommand cmd = new SqlCommand("recuperarUsu", Cx.conexion))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@usu", SqlDbType.VarChar).Value = txtUsu.Text;
                                cmd.Parameters.Add("@cedula", SqlDbType.VarChar).Value = Convert.ToString(txtCed.Text);
                                cmd.Parameters.Add("@clave", SqlDbType.VarChar).Value = Convert.ToString(txtConf.Text);

                                Cx.Conectar();
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Felicidades Ya Cambiaste la Contraseña");
                                Cx.Desconectar();
                                limpiar();
                                panel1.Visible = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("La Confirmacion de la contraseña es incorrecta");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe Introducir la contraseña nueva del Usuario");
                    }
                }
                else
                {
                    MessageBox.Show("Debe Introducir el Nombre del Usuario que desea Recuperar");
                }
            }
            else
            {
                MessageBox.Show("Debe Introducir la Cedula del Usuario que desea Recuperar");
            }
        }
        bool activo;
        private void TxtCed_Leave(object sender, EventArgs e)
        {
            if (txtCed.Text == "")
            {
                activo = true;
            }
            else
            {
                activo = false;
            }

            if (activo == false)
            {
                Cx.Desconectar();
                SqlCommand command = new SqlCommand("SELECT  dbo.Usuario.Usuario FROM dbo.Empleado INNER JOIN " +
                    "dbo.Usuario ON dbo.Empleado.IdEmpleado = dbo.Usuario.IdEmpleado where dbo.Empleado.Dni = @Clave", Cx.conexion);
                command.Parameters.AddWithValue("@Clave", txtCed.Text);
                Cx.Conectar();
                SqlDataReader leer = command.ExecuteReader();

                if (leer.Read() == true)
                {
                    txtUsuarioCed.Text = leer["Usuario"].ToString();
                }
                else
                {
                    MessageBox.Show("Esta Cedula No tiene Usuario Registrado");
                    txtCed.Clear();
                }
                Cx.Desconectar();
            }
            else
            {
                MessageBox.Show("No Hay Cedula Introducida");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (txtUsu.Text != "")
            {
                if (txtUsu.Text == txtUsuarioCed.Text)
                {
                    MessageBox.Show("Usuario y Cedula Coinsiden. PUEDE REALIZAR LOS CAMBIOS");
                    panel1.Visible = false;
                }
                else
                {
                    MessageBox.Show("Usuario y Cedula No Coinsiden. INTENTE DE NUEVO");
                    txtUsu.Clear();
                }
            }
        }
    }
}

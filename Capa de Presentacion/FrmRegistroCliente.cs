using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CapaLogicaNegocio;
using System.Data.SqlClient;

namespace Capa_de_Presentacion
{
    public partial class FrmRegistroCliente : DevComponents.DotNetBar.Metro.MetroForm
    {
        private clsCliente C = new clsCliente();
        clsCx Cx = new clsCx();
        public FrmRegistroCliente()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtDni.Text.Trim() != "")
            {
                if (txtApellidos.Text.Trim() != "")
                {
                    if (txtNombres.Text.Trim() != "")
                    {
                        if (txtDireccion.Text.Trim() != "")
                        {
                            if (txtTelefono.Text.Trim() != "")
                            {

								if (Program.Evento == 0)
								{
                                    
                                        using (SqlConnection con = new SqlConnection(Cx.conet))
                                        {
                                            using (SqlCommand cmd = new SqlCommand("RegistrarCliente", con))
                                            {
                                                cmd.CommandType = CommandType.StoredProcedure;

                                                cmd.Parameters.Add("@Dni", SqlDbType.NVarChar).Value = txtDni.Text;
                                                cmd.Parameters.Add("@Apellidos", SqlDbType.NVarChar).Value = txtApellidos.Text;
                                                cmd.Parameters.Add("@Nombres", SqlDbType.NVarChar).Value = txtNombres.Text;
                                                cmd.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = txtDireccion.Text;
                                                cmd.Parameters.Add("@Telefono", SqlDbType.NVarChar).Value = txtTelefono.Text;
                                                cmd.Parameters.Add("@estado", SqlDbType.Int).Value = 1;

                                                DevComponents.DotNetBar.MessageBoxEx.Show("Se Registro Correctamente", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                con.Open();
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                                Limpiar();
                                            }
                                        }
                                  
								}
							}
                            else
                            {
                                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese N° de Teléfono o Celular.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtTelefono.Focus();
                            }
                        }
                        else
                        {
                            DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Dirección del Cliente.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtDireccion.Focus();
                        }
                    }
                    else
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Nombre(s) del Cliente.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtNombres.Focus();
                    }
                }
                else
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Apellidos del Cliente.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtApellidos.Focus();
                }
            }
            else {
                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese N° de D.N.I del Cliente.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDni.Focus();
            }
       }

        private void Limpiar() {
            txtDni.Text = "";
            txtApellidos.Clear();
            txtNombres.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtDni.Focus();
        }
        //bool activo;
        private void FrmRegistroCliente_Load(object sender, EventArgs e)
        {
            txtDni.Focus();
        }
		//private void FrmRegistroCliente_Activated(object sender, EventArgs e)
  //      {
  //          if (txtIdC.Text == "")
  //          {
  //              activo = true;
  //          }
  //          else
  //          {
  //              activo = false;
  //          }

  //          if (activo == false)
  //          {
  //              SqlCommand command = new SqlCommand("SELECT dbo.Cliente.imagen FROM dbo.Cliente WHERE dbo.Cliente.imagen IS NOT NULL AND  dbo.Cliente.Cliente = @Clave", Cx.conexion);
  //              command.Parameters.AddWithValue("@Clave", txtIdC.Text);

  //              Cx.conexion.Open();
  //              SqlDataReader leer = command.ExecuteReader();

  //              if (leer.Read() == false)
  //              {
  //                  pictureBox1.Image = null;
  //              }

  //              else
  //              {//Representa un set de comandos que es utilizado para llenar un DataSet
  //                  SqlDataAdapter dp = new SqlDataAdapter(command);
  //                  Cx.conexion.Close();

  //                  //Representa un caché (un espacio) en memoria de los datos.
  //                  DataSet ds = new DataSet("Cliente");
  //                    //Arreglo de byte en donde se almacenara la foto en bytes
  //                      byte[] MyData = new byte[0];

  //                      //Llenamosel DataSet con la tabla. 
  //                      dp.Fill(ds, "Cliente");

  //                      //Inicializamos una fila de datos en la cual se almacenaran todos los datos de la fila seleccionada
  //                      DataRow myRow = ds.Tables["Cliente"].Rows[0];

  //                  if (myRow["imagen"] != DBNull.Value)
  //                  {
  //                      //Se almacena el campo foto de la tabla en el arreglo de bytes
  //                      MyData = (byte[])myRow["imagen"];

  //                      //Se inicializa un flujo en memoria del arreglo de bytes
  //                      MemoryStream stream = new MemoryStream(MyData);

  //                      //En el picture box se muestra la imagen que esta almacenada en el flujo en memoria 
  //                      //el cual contiene el arreglo de bytes
  //                      pictureBox1.Image = System.Drawing.Image.FromStream(stream);
  //                  }
  //                  else
  //                  {
  //                      pictureBox1.Image = null;
  //                  }
                
  //              }
  //          }
  //      }

		private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
		{
			validar.solonumeros(e);
		}

		private void txtApellidos_KeyPress(object sender, KeyPressEventArgs e)
		{
			validar.sololetras(e);
		}

		private void label6_Click(object sender, EventArgs e)
		{
            Program.abiertosecundario = false;
            Program.abierto = false;
            this.Close();
		}

		//private void button2_Click_1(object sender, EventArgs e)
		//{
		//	OpenFileDialog flg = new OpenFileDialog();
		//	flg.InitialDirectory = "C:\\";
		//	flg.Filter = "Archivos jpg (*.jpg)|*.jpg|Archivos png (*.png)|*.png";
		//	if (flg.ShowDialog() == DialogResult.OK) pictureBox1.Load(flg.FileName);
		//}

        private void button3_Click(object sender, EventArgs e)
        {

            if (txtDni.Text.Trim() != "")
            {
                if (txtApellidos.Text.Trim() != "")
                {
                    if (txtNombres.Text.Trim() != "")
                    {
                        if (txtDireccion.Text.Trim() != "")
                        {
                            if (txtTelefono.Text.Trim() != "")
                            {

                                if (Program.Evento == 1)
                                {
                                    
                                        using (SqlConnection con = new SqlConnection(Cx.conet))
                                        {
                                            using (SqlCommand cmd = new SqlCommand("ActualizarCliente", con))
                                            {
                                                cmd.CommandType = CommandType.StoredProcedure;

                                                cmd.Parameters.Add("@Dni", SqlDbType.NVarChar).Value = txtDni.Text;
                                                cmd.Parameters.Add("@Apellidos", SqlDbType.NVarChar).Value = txtApellidos.Text;
                                                cmd.Parameters.Add("@Nombres", SqlDbType.NVarChar).Value = txtNombres.Text;
                                                cmd.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = txtDireccion.Text;
                                                cmd.Parameters.Add("@Telefono", SqlDbType.NVarChar).Value = txtTelefono.Text;
                                                cmd.Parameters.Add("@estado", SqlDbType.Int).Value = 1;

                                                DevComponents.DotNetBar.MessageBoxEx.Show("Se Actualizo Correctamente", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                con.Open();
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                                Limpiar();
                                            }
                                        }
                                  
                                }
                            }
                            else
                            {
                                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese N° de Teléfono o Celular.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtTelefono.Focus();
                            }
                        }
                        else
                        {
                            DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Dirección del Cliente.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtDireccion.Focus();
                        }
                    }
                    else
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Nombre(s) del Cliente.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtNombres.Focus();
                    }
                }
                else
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Apellidos del Cliente.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtApellidos.Focus();
                }
            }
            else
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese N° de D.N.I del Cliente.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDni.Focus();
            }

        }
    }
}

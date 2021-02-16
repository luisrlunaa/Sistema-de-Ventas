using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevComponents.DotNetBar;
using System.IO;
using CapaLogicaNegocio;


namespace Capa_de_Presentacion
{
    public partial class FrmRegistrarUsuarios : DevComponents.DotNetBar.Metro.MetroForm

    {
        clsUsuarios U = new clsUsuarios();
		clsCx Cx = new clsCx();
        public FrmRegistrarUsuarios()
        {
            InitializeComponent();
        }

        private void FrmRegistrarUsuarios_Load(object sender, EventArgs e)
        {
			txtEmp.Text = Program.IdEmpleado + "";
		}

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Trim() != "")
            {
                if (txtPassword.Text.Trim() != "")
                {
                    if (Program.IdEmpleado != 0)
                    {
							U.IdEmpleado = Program.IdEmpleado;
							U.User = txtUser.Text;
							U.Password = txtPassword.Text;

						DevComponents.DotNetBar.MessageBoxEx.Show(U.RegistrarUsuarios(), "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        Limpiar();
                    }
                    else {
                        DevComponents.DotNetBar.MessageBoxEx.Show("Lo Sentimos, Pero Usted debe Eligir un \n Empleado para Crear una Cuenta de Usuario.\n \n G R A C I A S.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese su Contraseña.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Focus();
                }
            }
            else {
                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese su Usuario.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUser.Focus();
            }
        }

        private void Limpiar() {
            txtPassword.Clear();
            txtUser.Clear();
            Program.IdEmpleado = 0;
            txtUser.Focus();
            lblCargo.Text = "";
            lblDni.Text = "";
            lblEmpleado.Text = "";
        }
		private void label9_Click(object sender, EventArgs e)
		{
            Program.abierto = false;
            this.Close();
		}
       // bool activo;
		//private void FrmRegistrarUsuarios_Activated(object sender, EventArgs e)
		//{
	
  //          if (txtEmp.Text == "")
  //          {
  //              activo = true;
  //          }
  //          else
  //          {
  //              activo = false;
  //          }

  //          if (activo == false)
  //          {
  //              SqlCommand command = new SqlCommand("SELECT dbo.Empleado.imagen FROM dbo.Empleado WHERE dbo.Empleado.imagen IS NOT NULL AND  dbo.Empleado.IdEmpleado = @Clave", Cx.conexion);
  //              command.Parameters.AddWithValue("@Clave", txtEmp.Text);

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
  //                  DataSet ds = new DataSet("Empleado");
                    
  //                      //Arreglo de byte en donde se almacenara la foto en bytes
  //                      byte[] MyData = new byte[0];

  //                      //Llenamosel DataSet con la tabla. 
  //                      dp.Fill(ds, "Empleado");

  //                      //Inicializamos una fila de datos en la cual se almacenaran todos los datos de la fila seleccionada
  //                      DataRow myRow = ds.Tables["Empleado"].Rows[0];

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
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DevComponents.DotNetBar;
using System.IO;
using System.Windows.Forms;
using CapaLogicaNegocio;

namespace Capa_de_Presentacion
{
    public partial class FrmRegistrarEmpleados : DevComponents.DotNetBar.Metro.MetroForm
    {
        clsCargo C = new clsCargo();
        clsEmpleado Em = new clsEmpleado();
		clsCx Cx = new clsCx();
        int Listado = 0;
        public FrmRegistrarEmpleados()
        {
            InitializeComponent();
        }

        private void FrmRegistrarEmpleados_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer1.Interval = 1000;
            CargarComboBox();
		}

        private void CargarComboBox(){
            comboBox1.DataSource = C.Listar();
            comboBox1.DisplayMember = "Descripcion";
            comboBox1.ValueMember = "IdCargo";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Program.abiertosecundario == false)
            {
                FrmRegistrarCargo C = new FrmRegistrarCargo();
                Program.abiertosecundario = true;
                C.Show();
            }
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Char EstadoCivil = 'S';
            switch (cbxEstadoCivil.SelectedIndex)
            {
                case 1: EstadoCivil = 'S'; break;
                case 2: EstadoCivil = 'C'; break;
                case 3: EstadoCivil = 'D'; break;
                case 4: EstadoCivil = 'V'; break;
            }

            if (txtDni.Text.Trim() != "")
            {
                if (txtApellidos.Text.Trim() != "")
                {
                    if (txtNombres.Text.Trim() != "")
                    {
                        if (txtDireccion.Text.Trim() != "")
                        {
                            if (comboBox1.SelectedValue != null)
                            {
                                        using (SqlConnection con = new SqlConnection(Cx.conet))
                                        {
                                            using (SqlCommand cmd = new SqlCommand("MantenimientoEmpleados", con))
                                            {
                                                cmd.CommandType = CommandType.StoredProcedure;

                                                cmd.Parameters.Add("@IdCargo", SqlDbType.NVarChar).Value = Convert.ToInt32(comboBox1.SelectedValue);
                                                cmd.Parameters.Add("@Dni", SqlDbType.NVarChar).Value = txtDni.Text;
                                                cmd.Parameters.Add("@Apellidos", SqlDbType.NVarChar).Value = txtApellidos.Text;
                                                cmd.Parameters.Add("@Nombres", SqlDbType.NVarChar).Value = txtNombres.Text;
                                                cmd.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = txtDireccion.Text;
                                                cmd.Parameters.Add("@EstadoCivil", SqlDbType.Char).Value = EstadoCivil;
                                                cmd.Parameters.Add("@Sexo", SqlDbType.Char).Value = rbnMasculino.Checked == true ? 'M' : 'F';
                                                cmd.Parameters.Add("@FechaNac", SqlDbType.Date).Value = dateTimePicker1.Text;

                                                DevComponents.DotNetBar.MessageBoxEx.Show("Se Realizo Correctamente", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                con.Open();
                                                cmd.ExecuteNonQuery();
                                                con.Close();
                                                Limpiar();
                                            }
                                        }
                              
                            }
                            else
                            {
                                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese el cargo que ejercera este Empleado", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void Limpiar() {
            cbxEstadoCivil.SelectedIndex = 0;
            txtApellidos.Clear();
            txtDireccion.Clear();
            txtDni.Clear();
            txtNombres.Clear();
            rbnMasculino.Checked = true;
            dateTimePicker1.Value = DateTime.Now;
            txtIdE.Clear();
            Program.IdCargo = 0;
            comboBox1.SelectedIndex = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (Listado) {
                case 0: CargarComboBox(); break;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Stop();
        }
        
        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
		{
			validar.solonumeros(e);
		}
		private void txtApellidos_KeyPress(object sender, KeyPressEventArgs e)
		{
			validar.sololetras(e);
		}
		private void label9_Click(object sender, EventArgs e)
		{
            Program.abiertosecundario = false;
            Program.abierto = false;
            this.Close();
		}
		//private void button2_Click(object sender, EventArgs e)
		//{
		//	OpenFileDialog flg = new OpenFileDialog();
		//	flg.InitialDirectory = "C:\\";
		//	flg.Filter = "Archivos jpg (*.jpg)|*.jpg|Archivos png (*.png)|*.png";
		//	if (flg.ShowDialog() == DialogResult.OK) pictureBox1.Load(flg.FileName);
		//}

        //private void FrmRegistrarEmpleados_Activated_1(object sender, EventArgs e)
        //{
        //    if (txtIdE.Text == "")
        //    {
        //        activo = true;
        //    }
        //    else
        //    {
        //        activo = false;
        //    }

        //    if (activo == false)
        //    {
        //        SqlCommand command = new SqlCommand("SELECT dbo.Empleado.imagen FROM dbo.Empleado WHERE dbo.Empleado.imagen IS NOT NULL AND  dbo.Empleado.idEmpleado = @Clave", Cx.conexion);
        //        command.Parameters.AddWithValue("@Clave", txtIdE.Text);

        //        Cx.conexion.Open();
        //        SqlDataReader leer = command.ExecuteReader();
        //            //Representa un set de comandos que es utilizado para llenar un DataSet
        //            SqlDataAdapter dp = new SqlDataAdapter(command);
        //            Cx.conexion.Close();

        //            //Representa un caché (un espacio) en memoria de los datos.
        //            DataSet ds = new DataSet("Empleado");
        //                //Arreglo de byte en donde se almacenara la foto en bytes
        //                byte[] MyData = new byte[0];

        //                //Llenamosel DataSet con la tabla. 
        //                dp.Fill(ds, "Empleado");

        //                //Inicializamos una fila de datos en la cual se almacenaran todos los datos de la fila seleccionada
        //                DataRow myRow = ds.Tables["Empleado"].Rows[0];

        //            if (myRow["imagen"] != DBNull.Value)
        //            {
        //                //Se almacena el campo foto de la tabla en el arreglo de bytes
        //                MyData = (byte[])myRow["imagen"];

        //                //Se inicializa un flujo en memoria del arreglo de bytes
        //                MemoryStream stream = new MemoryStream(MyData);

        //                //En el picture box se muestra la imagen que esta almacenada en el flujo en memoria 
        //                //el cual contiene el arreglo de bytes
        //                pictureBox1.Image = System.Drawing.Image.FromStream(stream);
        //            }
        //            else
        //            {
        //                pictureBox1.Image = null;
        //            }
        //        }
        //    }
        }
    }


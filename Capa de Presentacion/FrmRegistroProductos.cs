using CapaEnlaceDatos;
using CapaLogicaNegocio;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class FrmRegistroProductos : DevComponents.DotNetBar.Metro.MetroForm
    {

        private clsCategoria C = new clsCategoria();
        private clsProducto P = new clsProducto();
        clsManejador Cx = new clsManejador();

        public FrmRegistroProductos()
        {
            InitializeComponent();
        }

        private void FrmRegistroProductos_Load(object sender, EventArgs e)
        {
            ListarElementos();
            cargar_combo_Tipo(cbtipo);
            llenar_data(txtIdP.Text);

            cbtipo.SelectedIndex = 0;
            txtidImei.Text = "";
            if (txtPmin.Text.Trim() == "")
            {
                txtPmin.Text = "0";
            }
            if (txtPmax.Text.Trim() == "")
            {
                txtPmax.Text = "0";
            }

            chkimei.Checked = tienefila;
        }

        public void cargar_combo_Tipo(ComboBox tipo)
        {
            Cx.Desconectar();
            SqlCommand cm = new SqlCommand("CARGARcomboTipogoma", Cx.conexion);
            cm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            tipo.DisplayMember = "descripcion";
            tipo.ValueMember = "id";
            tipo.DataSource = dt;
        }
        public void ListarElementos()
        {
            if (IdC.Text.Trim() != "")
            {
                cbxCategoria.DisplayMember = "Descripcion";
                cbxCategoria.ValueMember = "IdCategoria";
                cbxCategoria.DataSource = C.Listar();
                cbxCategoria.SelectedValue = IdC.Text;
            }
            else
            {
                cbxCategoria.DisplayMember = "Descripcion";
                cbxCategoria.ValueMember = "IdCategoria";
                cbxCategoria.DataSource = C.Listar();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Cx.Desconectar();
            if (Cx.conexion != null && Cx.conexion.State == ConnectionState.Open)
            {
                Cx.Desconectar();
            }

            clsProducto P = new clsProducto();
            if (txtProducto.Text.Trim() != "")
            {
                if (txtMarca.Text.Trim() != "")
                {
                    if (txtPCompra.Text.Trim() != "")
                    {
                        if (txtPVenta.Text.Trim() != "")
                        {
                            if (txtStock.Text.Trim() != "")
                            {
                                using (SqlCommand cmd = new SqlCommand("RegistrarProducto", Cx.conexion))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = cbxCategoria.SelectedValue;
                                    cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = txtProducto.Text.ToUpper();
                                    cmd.Parameters.Add("@Marca", SqlDbType.NVarChar).Value = txtMarca.Text.ToUpper();
                                    cmd.Parameters.Add("@Stock", SqlDbType.Int).Value = txtStock.Text;
                                    cmd.Parameters.Add("@PrecioCompra", SqlDbType.Decimal).Value = txtPCompra.Text;
                                    cmd.Parameters.Add("@PrecioVenta", SqlDbType.Decimal).Value = txtPVenta.Text;
                                    cmd.Parameters.Add("@itbis", SqlDbType.Decimal).Value = txtitbis.Text;
                                    cmd.Parameters.Add("@TipoGoma", SqlDbType.NVarChar).Value = cbtipo.Text;
                                    cmd.Parameters.Add("@FechaVencimiento", SqlDbType.Date).Value = dateTimePicker1.Text;
                                    cmd.Parameters.Add("@FechaModificacion", SqlDbType.Date).Value = dateTimePicker1.Text;
                                    cmd.Parameters.Add("@Pmax", SqlDbType.Decimal).Value = txtPmax.Text;
                                    cmd.Parameters.Add("@Pmin", SqlDbType.Decimal).Value = txtPmin.Text;

                                    Cx.Conectar();
                                    cmd.ExecuteNonQuery();
                                    Cx.Desconectar();
                                    P.Listar();
                                    ListarElementos();
                                    Limpiar();
                                }
                            }
                            else
                            {
                                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Stock del Producto.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtStock.Focus();
                            }
                        }
                        else
                        {
                            DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Precio de Venta del Producto.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPVenta.Focus();
                        }
                    }
                    else
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Precio de Compra del Producto.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPCompra.Focus();
                    }
                }
                else
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Marca del Producto.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMarca.Focus();
                }
            }
            else
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Nombre del Producto.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProducto.Focus();
            }
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {
            if (Program.abiertosecundario == false)
            {
                FrmRegistrarCategoria C = new FrmRegistrarCategoria();
                Program.abiertosecundario = true;
                C.Show();
            }
        }

        private void Limpiar()
        {
            Program.IdProducto = 0;
            Program.Descripcion = "";
            Program.Marca = "";
            Program.PrecioVenta = 0;
            Program.Stock = 0;
            Program.IdCategoria = 0;
            Program.itbis = 0;
            Program.tipo = "";

            txtIMEI.Text = "";
            txtidImei.Text = "";
            txtProducto.Text = "";
            txtidImei.Clear();
            txtMarca.Clear();
            txtPCompra.Clear();
            txtPVenta.Clear();
            IdC.Clear();
            txtIdP.Clear();
            txtStock.Clear();
            dateTimePicker1.Value = DateTime.Now;
            txtProducto.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Salir.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cx.Desconectar();
            if (txtProducto.Text.Trim() != "")
            {
                if (txtMarca.Text.Trim() != "")
                {
                    if (txtPCompra.Text.Trim() != "")
                    {
                        if (txtPVenta.Text.Trim() != "")
                        {
                            if (txtStock.Text.Trim() != "")
                            {
                                using (SqlCommand cmd = new SqlCommand("ActualizarProducto", Cx.conexion))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = cbxCategoria.SelectedValue;
                                    cmd.Parameters.Add("@IdProducto", SqlDbType.Int).Value = txtIdP.Text;
                                    cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar).Value = txtProducto.Text.ToUpper();
                                    cmd.Parameters.Add("@Marca", SqlDbType.NVarChar).Value = txtMarca.Text.ToUpper();
                                    cmd.Parameters.Add("@Stock", SqlDbType.Int).Value = txtStock.Text;
                                    cmd.Parameters.Add("@PrecioCompra", SqlDbType.Decimal).Value = txtPCompra.Text;
                                    cmd.Parameters.Add("@PrecioVenta", SqlDbType.Decimal).Value = txtPVenta.Text;
                                    cmd.Parameters.Add("@itbis", SqlDbType.Decimal).Value = txtitbis.Text;
                                    cmd.Parameters.Add("@TipoGoma", SqlDbType.NVarChar).Value = cbtipo.Text;
                                    cmd.Parameters.Add("@FechaModificacion", SqlDbType.Date).Value = dateTimePicker1.Text;
                                    cmd.Parameters.Add("@Pmax", SqlDbType.Decimal).Value = txtPmax.Text;
                                    cmd.Parameters.Add("@Pmin", SqlDbType.Decimal).Value = txtPmin.Text;

                                    Cx.Conectar();
                                    cmd.ExecuteNonQuery();
                                    Cx.Desconectar();
                                    ListarElementos();
                                    Limpiar();
                                    this.Close();
                                }
                            }
                            else
                            {
                                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Stock del Producto.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtStock.Focus();
                            }
                        }
                        else
                        {
                            DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Precio de Venta del Producto.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPVenta.Focus();
                        }
                    }
                    else
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Precio de Compra del Producto.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPCompra.Focus();
                    }
                }
                else
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Marca del Producto.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMarca.Focus();
                }
            }
            else
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Nombre del Producto.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtProducto.Focus();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Program.abiertosecundario = false;
            Program.abierto = false;
            this.Close();
        }

        private void txtPCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {

            }
        }

        private void chkimei_CheckedChanged(object sender, EventArgs e)
        {
            if (chkimei.Checked == true)
            {
                this.Size = new System.Drawing.Size(1033, 484);
            }
            else
            {
                this.Size = new System.Drawing.Size(524, 484);
            }
        }

        bool tienefila = false;
        public void llenar_data(string id)
        {
            Cx.Desconectar();
            if (id != "")
            {

                //variable de tipo Sqlcommand
                SqlCommand comando = new SqlCommand();
                //variable SqlDataReader para leer los datos
                SqlDataReader dr;
                comando.Connection = Cx.conexion;
                //declaramos el comando para realizar la busqueda
                comando.CommandText = "select * from ImeiList where IdProducto =" + id + "and activo=" + 1;
                //especificamos que es de tipo Text
                comando.CommandType = CommandType.Text;
                //se abre la conexion
                Cx.Conectar();
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

                    tienefila = true;
                }
                Cx.Desconectar();
            }
        }

        public void idProducto()
        {
            Cx.Desconectar();

            string cadSql = "select top(1) IdProducto from Producto order by IdProducto desc";

            SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);
            Cx.Conectar();

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
            {
                txtIdPNew.Text = leer["IdProducto"].ToString();
            }
            Cx.Desconectar();
        }

        public string newimeiID;
        public void idIMEI(string id)
        {
            Cx.Desconectar();

            string cadSql = "select top(1) idImei from ImeiList where IdProducto =" + id + "order by idImei desc";

            SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);
            Cx.Conectar();

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read() == true)
            {
                if (leer["idImei"].ToString() != "" || leer["idImei"].ToString() != null)
                {
                    newimeiID = leer["idImei"].ToString();
                }
            }
            else
            {
                newimeiID = "1";
            }
            Cx.Desconectar();
        }

        private void dgvimei_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvimei.Rows.Count > 0)
            {
                dgvimei.Rows[dgvimei.CurrentRow.Index].Selected = true;

                btnsuma.Text = "-";
                btnsuma.ForeColor = Color.White;
                btnsuma.BackColor = Color.Red;
            }
        }

        private void btnsuma_Click(object sender, EventArgs e)
        {
            Cx.Desconectar();

            idIMEI(txtIdP.Text);
            if (txtIdP.Text == "")
            {
                txtIdP.Text = txtIdPNew.Text;
            }

            if (btnsuma.Text == "-")
            {
                txtidImei.Text = dgvimei.CurrentRow.Cells["idImei"].Value.ToString();
                txtIdP.Text = dgvimei.CurrentRow.Cells["id"].Value.ToString();

                if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Está Seguro que Desea Eliminar este IMEI.?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {

                    using (SqlCommand cmd = new SqlCommand("eliminarimei", Cx.conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idImei", SqlDbType.Int).Value = Convert.ToInt32(txtidImei.Text);
                        cmd.Parameters.Add("@idproducto", SqlDbType.Int).Value = Convert.ToInt32(txtIdP.Text);

                        Cx.Conectar();
                        cmd.ExecuteNonQuery();
                        Cx.Desconectar();
                        llenar_data(txtIdP.Text);


                        btnsuma.Text = "+";
                        btnsuma.ForeColor = Color.White;
                        btnsuma.BackColor = Color.CornflowerBlue;
                    }
                }
            }
            else
            {
                using (SqlCommand cmd = new SqlCommand("Registrarimei", Cx.conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idImei", SqlDbType.Int).Value = Convert.ToInt32(newimeiID);
                    cmd.Parameters.Add("@IdProducto", SqlDbType.Int).Value = Convert.ToInt32(txtIdP.Text);
                    cmd.Parameters.Add("@IMEI", SqlDbType.NVarChar).Value = txtIMEI.Text;
                    cmd.Parameters.Add("@activo", SqlDbType.Char).Value = 1;
                    cmd.Parameters.Add("@FechaModificacion", SqlDbType.Date).Value = dateTimePicker1.Value = DateTime.Now;

                    Cx.Conectar(); ;
                    cmd.ExecuteNonQuery();
                    Cx.Desconectar();
                    llenar_data(txtIdP.Text);
                    ListarElementos();
                }

                newimeiID = "";
                txtIMEI.Text = "";
            }
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void FrmRegistroProductos_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}

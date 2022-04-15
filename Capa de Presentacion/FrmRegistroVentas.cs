using CapaEnlaceDatos;
using CapaLogicaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class FrmRegistroVentas : DevComponents.DotNetBar.Metro.MetroForm
    {
        public class PrecioCompraProducto
        {
            public int ID;
            public decimal Precio;
        }

        private List<clsVentas> lst = new List<clsVentas>();
        private List<PrecioCompraProducto> listProducts = new List<PrecioCompraProducto>();
        clsManejador M = new clsManejador();

        public FrmRegistroVentas()
        {
            InitializeComponent();
        }
        private void FrmVentas_Load(object sender, EventArgs e)
        {
            M.Desconectar();
            txtidCli.Text = null;
            listProducts.Clear();
            btnSalir.Enabled = false;
            txtdireccion.Text = "";
            Program.IdCliente = 0;
            cbidentificacion.Checked = false;
            txtrcnClient.Text = "sin rcn del Cliente";
            txtid.Text = "0";
            
            chkComprobante.Checked = false;
            if (cbidentificacion.Checked == true)
            {
                btnBuscar.Show();
                txtDatos.ReadOnly = true;
            }
            else
            {
                btnBuscar.Hide();
                txtDatos.ReadOnly = false;
            }

            txtPVenta.Enabled = Program.isAdminUser;
            txtIgv.Enabled = Program.isAdminUser;

            txtid.Text = "0";
            Program.ReImpresion = "";
            Program.datoscliente = "";
            Program.realizopago = false;
            actualzarestadoscomprobantes();
            cargar_combo_NCF(combo_tipo_NCF);
            cargar_combo_Tipofactura(cbtipofactura);
            btnRegistrarVenta.Hide();
            btnImprimir.Visible = !Program.isSaler;
            btnAgregar.Visible = !Program.isSaler;
            button2.Visible = false;
            llenar();
            btnEliminarItem.Enabled = false;
            frmPagar pa = new frmPagar();
            txttotal.Text = Convert.ToString(pa.txtmonto.Text);

            if (combo_tipo_NCF.Items.Count > 0)
            {
                combo_tipo_NCF.SelectedValue = 2;
                var comconsumo = combo_tipo_NCF.SelectedValue;
                if (comconsumo != null)
                {
                    comboselectNCF(2);
                }
                else
                {
                    txtNCF.Text = "Sin NCF";
                    combo_tipo_NCF.Text = "Ningun Tipo de Comprobante";
                }
            }
        }

        public void actualzarestadoscomprobantes()
        {
            var listaidint = new List<int>();

            for (int i = 0; i <= 9; i++)
            {
                listaidint.Add(i);
            }

            foreach (var item in listaidint)
            {
                M.Desconectar();
                M.Conectar();
                string sql = "SELECT * FROM ncf INNER JOIN Comprobantes ON ncf.id_ncf = Comprobantes.id_comprobante where ncf.id_ncf=@id order by id_ncf";
                SqlCommand cmd = new SqlCommand(sql, M.conexion);
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
                        M.Desconectar();
                        using (SqlCommand cmdup = new SqlCommand("UpdateState", M.conexion))
                        {
                            cmdup.CommandType = CommandType.StoredProcedure;
                            cmdup.Parameters.Add("@id", SqlDbType.Int).Value = item;

                            M.Conectar();
                            cmdup.ExecuteNonQuery();
                            M.Desconectar();
                        }
                    }
                }
            }
        }

        public void cargar_combo_NCF(ComboBox combo_tipo_NCF)
        {
            M.Desconectar();
            SqlCommand cm = new SqlCommand("CARGARcomboNCF", M.conexion);
            cm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            combo_tipo_NCF.DisplayMember = "descripcion_ncf";
            combo_tipo_NCF.ValueMember = "id_ncf";
            combo_tipo_NCF.DataSource = dt;
        }

        public void cargar_combo_Tipofactura(ComboBox tipofactura)
        {
            M.Desconectar();
            SqlCommand cm = new SqlCommand("CARGARcomboTipoFactura", M.conexion);
            cm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            tipofactura.DisplayMember = "descripcion";
            tipofactura.ValueMember = "id";
            tipofactura.DataSource = dt;
        }

        public void llenar_data_ncf()
        {
            M.Desconectar();
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            comando.Connection = M.conexion;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "SELECT  id_secuencia, secuenciaNCF, fecha from NCFGenerados";
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            M.Conectar();
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

                data_ncf.Rows[renglon].Cells["id_secuencia"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id_secuencia")));
                data_ncf.Rows[renglon].Cells["secuencia"].Value = dr.GetString(dr.GetOrdinal("secuenciaNCF"));
                data_ncf.Rows[renglon].Cells["fecha"].Value = dr.GetDateTime(dr.GetOrdinal("fecha"));
            }
            M.Desconectar();
        }

        public void llenar()
        {
            M.Desconectar();
            string cadSql = "select top(1) IdVenta from Venta order by IdVenta desc";
            SqlCommand comando = new SqlCommand(cadSql, M.conexion);
            M.Conectar();

            SqlDataReader leer = comando.ExecuteReader();
            int varcodigo;

            if (leer.Read() == true)
            {
                varcodigo = Convert.ToInt32(leer["IdVenta"]) + 1;
                txtIdVenta.Text = varcodigo.ToString();
            }
            else
            {
                txtIdVenta.Text = "1";
            }
            M.Desconectar();
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            FrmListadoClientes C = new FrmListadoClientes();
            C.Show();
        }

        bool activar;
        bool entro = false;
        private void FrmVentas_Activated(object sender, EventArgs e)
        {
            if (Program.IdCliente != 0)
            {
                txtDatos.Text = Program.NombreCliente + " " + Program.ApellidosCliente;
                txtidCli.Text = Program.IdCliente + "";
                txtDocIdentidad.Text = Program.DocumentoIdentidad;
            }
            else
            {
                if (txtDatos.Text != null && txtDatos.Text != "")
                {
                    Program.datoscliente = txtDatos.Text;

                }
                if (Program.datoscliente != null && Program.datoscliente != "")
                {
                    txtDatos.Text = Program.datoscliente;
                }
            }

            txtIdProducto.Text = Program.IdProducto + "";
            txtDescripcion.Text = Program.Descripcion;
            txtMarca.Text = Program.Marca;
            if (txtdireccion.Text != "" && txtdireccion.Text != null)
            {
                Program.Direccion = txtdireccion.Text;
            }

            if (Program.Direccion != "" && Program.Direccion != null)
            {
                txtdireccion.Text = Program.Direccion;
            }

            txtStock.Text = Program.Stock + "";
            txtPVenta.Text = Program.PrecioVenta + "";
            txtIgv.Text = Program.itbis + "";
            txtIdV.Text = Program.Id + "";
            txttotal.Text = Program.total + "";
            lblsubt.Text = Program.ST + "";
            lbligv.Text = Program.igv + "";
            txtpmax.Text = Program.Pmax + "";
            txtpmin.Text = Program.Pmin + "";

            if (Program.Esabono != "" && Program.Esabono != null && Program.pagoRealizado >= 0 && Program.realizopago == true)
            {
                button2.Visible = true;
                btnSalir.Visible = false;
            }
            else if (Program.pagoRealizado >= 0 && Program.realizopago == true)
            {
                btnRegistrarVenta.Visible = true;
                btnSalir.Visible = false;
            }
            else
            {
                btnRegistrarVenta.Visible = !Program.isSaler;
                btnSalir.Visible = Program.isSaler;
            }

            if (Program.Esabono != "" && Program.Esabono != null && Program.tipo.ToLower() == "credito")
            {
                activar = true;
                btnImprimir.Visible = false;
                btnSalir.Visible = true;

                lblabono.Visible = true;
                lbltituloabono.Visible = true;
                var fecha = Convert.ToDateTime(Program.ultimafechapago);
                lblabono.Text = fecha.Day + "/" + fecha.Month + "/" + fecha.Year;
            }
            else if (Program.Id == 0)
            {
                activar = false;
            }
            else
            {
                activar = true;
                btnImprimir.Visible = true;
            }

            if (Program.rncClient != "sin rcn del Cliente" && Program.rncClient != "" && Program.rncClient != null)
            {
                chkComprobante.Checked = true;
                txtrcnClient.Text = Program.rncClient;
            }

            if (Program.isSaler)
            {
                if (activar == true && entro == false)
                {
                    entro = true;
                    cbtipofactura.Text = Program.tipo;
                    combo_tipo_NCF.Text = Program.NCF;
                    txtNCF.Text = Program.NroComprobante;
                    txttotal.Text = Program.total + "";
                    lblsubt.Text = Program.ST + "";
                    lbligv.Text = Program.igv + "";
                    txtidEmp.Text = Program.IdEmpleado + "";
                    dateTimePicker1.Text = Program.fecha;

                    decimal subtotal = 0;
                    decimal igv = 0;

                    M.Desconectar();
                    //variable de tipo Sqlcommand
                    SqlCommand comando = new SqlCommand();
                    //variable SqlDataReader para leer los datos
                    SqlDataReader dr;
                    comando.Connection = M.conexion;
                    //declaramos el comando para realizar la busqueda
                    comando.CommandText = "SELECT * from DetalleVenta WHERE DetalleVenta.IdVenta ='" + txtIdV.Text + "'";
                    //especificamos que es de tipo Text
                    comando.CommandType = CommandType.Text;
                    //se abre la conexion
                    M.Conectar();
                    //limpiamos los renglones de la datagridview
                    dgvVenta.Rows.Clear();
                    //a la variable DataReader asignamos  el la variable de tipo SqlCommand
                    dr = comando.ExecuteReader();
                    while (dr.Read())
                    {
                        //variable de tipo entero para ir enumerando los la filas del datagridview
                        int renglon = dgvVenta.Rows.Add();
                        // especificamos en que fila se mostrará cada registro
                        // nombredeldatagrid.filas[numerodefila].celdas[nombrdelacelda].valor=\

                        string idVenta = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdVenta")));
                        if (idVenta == txtIdV.Text)
                        {
                            dgvVenta.Rows[renglon].Cells["IdD"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdVenta")));
                            dgvVenta.Rows[renglon].Cells["cantidadP"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("Cantidad")));
                            dgvVenta.Rows[renglon].Cells["DescripcionP"].Value = dr.GetString(dr.GetOrdinal("detalles_P"));
                            dgvVenta.Rows[renglon].Cells["PrecioU"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("PrecioUnitario")));
                            dgvVenta.Rows[renglon].Cells["SubtoTal"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("SubTotal")));
                            dgvVenta.Rows[renglon].Cells["IDP"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdProducto")));
                            dgvVenta.Rows[renglon].Cells["IGV"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("Igv")));

                            subtotal += (dr.GetDecimal(dr.GetOrdinal("PrecioUnitario")) * dr.GetInt32(dr.GetOrdinal("Cantidad")));
                            igv += (dr.GetDecimal(dr.GetOrdinal("Igv")) * dr.GetInt32(dr.GetOrdinal("Cantidad")));
                        }
                    }

                    Program.ST = subtotal;
                    Program.igv = igv;

                    M.Desconectar();
                    buscaridcaja();
                }
            }
            else
            {
                if (activar == true && entro == false)
                {
                    entro = true;
                    txtIdV.Text = Program.Id + "";
                    txttotal.Text = Program.total + "";
                    lblsubt.Text = Program.ST + "";
                    lbligv.Text = Program.igv + "";
                    txtidEmp.Text = Program.IdEmpleado + "";

                    decimal subtotal = 0;
                    decimal igv = 0;

                    M.Desconectar();
                    //variable de tipo Sqlcommand
                    SqlCommand comando = new SqlCommand();
                    //variable SqlDataReader para leer los datos
                    SqlDataReader dr;
                    comando.Connection = M.conexion;
                    //declaramos el comando para realizar la busqueda
                    comando.CommandText = "SELECT * from DetalleCotizacion WHERE DetalleCotizacion.IdCotizacion ='" + txtIdV.Text + "'";
                    //especificamos que es de tipo Text
                    comando.CommandType = CommandType.Text;
                    //se abre la conexion
                    M.Conectar();
                    //limpiamos los renglones de la datagridview
                    dgvVenta.Rows.Clear();
                    //a la variable DataReader asignamos  el la variable de tipo SqlCommand
                    dr = comando.ExecuteReader();
                    while (dr.Read())
                    {
                        //variable de tipo entero para ir enumerando los la filas del datagridview
                        int renglon = dgvVenta.Rows.Add();
                        // especificamos en que fila se mostrará cada registro
                        // nombredeldatagrid.filas[numerodefila].celdas[nombrdelacelda].valor=\

                        string idVenta = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdCotizacion")));
                        if (idVenta == txtIdV.Text)
                        {
                            dgvVenta.Rows[renglon].Cells["IdD"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdCotizacion")));
                            dgvVenta.Rows[renglon].Cells["cantidadP"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("Cantidad")));
                            dgvVenta.Rows[renglon].Cells["DescripcionP"].Value = dr.GetString(dr.GetOrdinal("detalles_P"));
                            dgvVenta.Rows[renglon].Cells["PrecioU"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("PrecioUnitario")));
                            dgvVenta.Rows[renglon].Cells["SubtoTal"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("SubTotal")));
                            dgvVenta.Rows[renglon].Cells["IDP"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("IdProducto")));
                            dgvVenta.Rows[renglon].Cells["IGV"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("Igv")));

                            subtotal += (dr.GetDecimal(dr.GetOrdinal("PrecioUnitario")) * dr.GetInt32(dr.GetOrdinal("Cantidad")));
                            igv += (dr.GetDecimal(dr.GetOrdinal("Igv")) * dr.GetInt32(dr.GetOrdinal("Cantidad")));

                            PrecioCompraProducto PCP = new PrecioCompraProducto();
                            PCP.ID = dr.GetInt32(dr.GetOrdinal("IdProducto"));
                            PCP.Precio = dr.GetDecimal(dr.GetOrdinal("GananciaVenta"));
                            listProducts.Add(PCP);
                        }
                    }

                    Program.ST = subtotal;
                    Program.igv = igv;

                    M.Desconectar();
                }
            }
        }

        public int idcaja = 0;
        public void buscaridcaja()
        {
            M.Desconectar();
            M.Conectar();
            string sql = "select id_caja from Caja where fecha = convert(datetime,CONVERT(varchar(10), @fecha, 103),103)";
            SqlCommand cmd = new SqlCommand(sql, M.conexion);
            cmd.Parameters.AddWithValue("@fecha", dateTimePicker1.Value);

            SqlDataReader reade = cmd.ExecuteReader();
            if (reade.Read())
            {
                idcaja = Convert.ToInt32(reade["id_caja"]);
            }

            M.Desconectar();
        }

        private void btnBusquedaProducto_Click(object sender, EventArgs e)
        {
            if (Program.abiertosecundarias == false)
            {
                FrmListadoProductos P = new FrmListadoProductos();
                btnAgregar.Visible = true;
                Program.datoscliente = txtDatos.Text;
                Program.abiertosecundarias = true;
                P.Show();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsVentas V = new clsVentas();
            PrecioCompraProducto PCP = new PrecioCompraProducto();

            if (txtDescripcion.Text.Trim() != "")
            {
                if (txtCantidad.Text.Trim() != "")
                {
                    if (Convert.ToDecimal(txtCantidad.Text) >= 0)
                    {
                        if (Convert.ToDecimal(txtCantidad.Text) <= Convert.ToDecimal(txtStock.Text))
                        {
                            V.IdProducto = Convert.ToInt32(txtIdProducto.Text);
                            V.IdVenta = Convert.ToInt32(txtIdVenta.Text);
                            V.Descripcion = (txtDescripcion.Text + "-" + txtMarca.Text).Trim();
                            V.Cantidad = Convert.ToDecimal(txtCantidad.Text);

                            if (Convert.ToDecimal(txtIgv.Text) > 0)
                            {
                                V.Igv = Convert.ToDecimal(txtIgv.Text);
                            }
                            V.PrecioCompra = Program.PrecioCompra;
                            V.PrecioVenta = Convert.ToDecimal(txtPVenta.Text);

                            V.SubTotal = Math.Round((Convert.ToDecimal(txtPVenta.Text) + Convert.ToDecimal(txtIgv.Text)) * Convert.ToDecimal(txtCantidad.Text), 2);
                            btnAgregar.Visible = false;
                            lst.Add(V);

                            PCP.ID = Convert.ToInt32(txtIdProducto.Text);
                            PCP.Precio = Program.PrecioCompra;
                            listProducts.Add(PCP);

                            LlenarGri();

                            if (cbidentificacion.Checked == false && txtDatos.Text != "" && Program.IdCliente == 0)
                            {
                                txtDocIdentidad.Text = "Sin identificacion";
                                txtDatos.Text = Program.datoscliente;
                            }

                            if (btnSalir.Enabled == false)
                                btnSalir.Enabled = true;

                            Limpiar();
                        }
                        else
                        {
                            DevComponents.DotNetBar.MessageBoxEx.Show("Stock Insuficiente para Realizar la Venta.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        }
                    }
                    else
                    {
                        DevComponents.DotNetBar.MessageBoxEx.Show("Cantidad Ingresada no Válida.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                        txtCantidad.Clear();
                        txtCantidad.Focus();
                    }
                }
                else
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor Ingrese Cantidad a Vender.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    txtCantidad.Focus();
                }
            }
            else
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Por Favor buscar un Producto.", "Sistema de Ventas.", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void LlenarGri()
        {
            decimal SumaSubTotal = 0; decimal SumaIgv = 0; decimal SumaTotal = 0;
            dgvVenta.Rows.Clear();
            for (int i = 0; i < lst.Count; i++)
            {
                dgvVenta.Rows.Add();
                dgvVenta.Rows[i].Cells["idD"].Value = lst[i].IdVenta;
                dgvVenta.Rows[i].Cells["DescripcionP"].Value = lst[i].Descripcion;
                dgvVenta.Rows[i].Cells["cantidadP"].Value = lst[i].Cantidad;
                dgvVenta.Rows[i].Cells["IGV"].Value = lst[i].Igv;
                dgvVenta.Rows[i].Cells["PrecioU"].Value = lst[i].PrecioVenta;
                dgvVenta.Rows[i].Cells["SubtoTal"].Value = lst[i].SubTotal;
                dgvVenta.Rows[i].Cells["IDP"].Value = lst[i].IdProducto;
                var preciounidad = Convert.ToDecimal(dgvVenta.Rows[i].Cells["PrecioU"].Value);
                var cantidad = Convert.ToDecimal(dgvVenta.Rows[i].Cells["cantidadP"].Value);
                var igv = Convert.ToDecimal(dgvVenta.Rows[i].Cells["IGV"].Value);

                SumaSubTotal += preciounidad * cantidad;
                SumaIgv += igv * cantidad;

                SumaTotal += Math.Round(Convert.ToDecimal(dgvVenta.Rows[i].Cells["SubtoTal"].Value), 2);

                lblsubt.Text = Convert.ToString(SumaSubTotal);
                lbligv.Text = Convert.ToString(SumaIgv);
                txttotal.Text = Convert.ToString(SumaTotal);

                Program.igv = Convert.ToDecimal(lbligv.Text);
                Program.ST = Convert.ToDecimal(lblsubt.Text);
                Program.total = Convert.ToDecimal(txttotal.Text);
                Program.PrecioCompra = 0;
            }
        }

        private void Limpiar()
        {
            txtDescripcion.Clear();
            txtMarca.Clear();
            txtStock.Clear();
            txtPVenta.Clear();
            txtCantidad.Clear();
            txtCantidad.Focus();
            txtIgv.Text = "";
            Program.Descripcion = "";
            Program.Stock = 0;
            Program.Marca = "";
            Program.PrecioVenta = 0;
            Program.IdProducto = 0;
            Program.itbis = 0;
            Program.realizopago = false;
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            frmPagar pa = new frmPagar();
            Program.total = Convert.ToDecimal(txttotal.Text);
            Program.igv = Convert.ToDecimal(lbligv.Text);
            Program.ST = Convert.ToDecimal(lblsubt.Text);
            pa.txtmonto.Text = txttotal.Text;

            if (chkComprobante.Checked == false && !string.IsNullOrEmpty(txtNCF.Text))
            {
                if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Desea Agregar Comprobantes a la Factura?", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    chkComprobante.Checked = true;
                }
                else
                {
                    chkComprobante.Checked = false;
                    txtNCF.Text = "Sin NCF";
                    combo_tipo_NCF.Text = "Ningun Tipo de Comprobante";
                    txtid.Text = "0";
                }
            }

            if (cbidentificacion.Checked == false && Program.IdCliente == 0)
            {
                txtDatos.Text = Program.datoscliente;
                txtDocIdentidad.Text = "Sin identificacion";
            }

            pa.Show();

            Program.tipo = cbtipofactura.Text;
        }
        private void btnEliminarItem_Click(object sender, EventArgs e)
        {
            List<clsVentas> lista = new List<clsVentas>();
            List<PrecioCompraProducto> listapreciocompra = new List<PrecioCompraProducto>();
            Program.IdProducto = Convert.ToInt32(dgvVenta.CurrentRow.Cells["IDP"].Value.ToString());

            if (Program.IdProducto > 0)
            {
                decimal Igv = 0;
                decimal subtotal = 0;
                decimal SumaSubTotal = 0;
                decimal SumaIgv = 0;
                decimal SumaTotal = 0;

                foreach (var item in lst)
                {
                    if (item.IdProducto != Program.IdProducto)
                    {
                        lista.Add(item);

                        Igv = item.Igv;
                        subtotal = item.SubTotal;

                        SumaSubTotal += subtotal;
                        SumaIgv += Igv;
                        SumaTotal = SumaSubTotal + SumaIgv;
                    }
                    else
                    {
                        lista.Remove(item);
                    }

                    lst = lista;
                }

                foreach (var item in listProducts)
                {
                    if (item.ID != Program.IdProducto)
                    {
                        listapreciocompra.Add(item);
                    }
                    else
                    {
                        listapreciocompra.Remove(item);
                    }
                }

                lblsubt.Text = Convert.ToString(SumaSubTotal);
                txtIgv.Text = Convert.ToString(SumaIgv);
                txttotal.Text = Convert.ToString(SumaTotal);

                btnEliminarItem.Enabled = false;
                dgvVenta.Rows.RemoveAt(dgvVenta.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Por Favor Seleccione un producto antes de eliminarlo");
            }
        }

        decimal restante = 0;
        public void VentaRealizada()
        {
            M.Desconectar();
            string procedure = "";

            if (Program.IdCliente > 0)
            {
                procedure = Program.isSaler ? "RegistrarVenta" : "RegistrarCotizacion";
            }
            else
            {
                procedure = Program.isSaler ? "RegistrarVentasinIDcliente" : "RegistrarCotizacionsinIDcliente";
            }

            using (SqlCommand cmd = new SqlCommand(procedure, M.conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                //tabla Ventas
                if (Program.IdCliente != 0)
                {
                    cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = Convert.ToInt32(txtidCli.Text);
                    cmd.Parameters.Add("@NombreCliente", SqlDbType.VarChar).Value = txtDatos.Text;
                }
                else
                {
                    cmd.Parameters.Add("@NombreCliente", SqlDbType.VarChar).Value = Program.datoscliente;
                }

                cmd.Parameters.Add(Program.isSaler ? "@IdVenta" : "@IdCotizacion", SqlDbType.Int).Value = Convert.ToInt32(txtIdVenta.Text);
                cmd.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = txtidEmp.Text;
                cmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = Convert.ToDecimal(txttotal.Text);
                if (Program.isSaler)
                {
                    cmd.Parameters.Add("@TipoFactura", SqlDbType.NVarChar).Value = cbtipofactura.Text;

                    if (cbtipofactura.Text == "Credito")
                    {
                        restante = Convert.ToDecimal(txttotal.Text) - Program.pagoRealizado;
                        cmd.Parameters.Add("@Restante", SqlDbType.Decimal).Value = restante;
                    }
                    else
                    {
                        cmd.Parameters.Add("@Restante", SqlDbType.Decimal).Value = 0;
                    }

                    cmd.Parameters.Add("@Serie", SqlDbType.Int).Value = Convert.ToInt32(txtid.Text);
                    cmd.Parameters.Add("@NroDocumento", SqlDbType.NVarChar).Value = txtNCF.Text;
                    cmd.Parameters.Add("@TipoDocumento", SqlDbType.VarChar).Value = combo_tipo_NCF.Text;
                    cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = Program.Direccion ?? "Entregado en el Local";
                    cmd.Parameters.Add("@rcnClient", SqlDbType.VarChar).Value = txtrcnClient.Text;
                    cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = dateTimePicker1.Text;
                }

                M.Conectar();
                cmd.ExecuteNonQuery();
                M.Desconectar();

                Venta venta = new Venta();

                venta.IdVenta = Convert.ToInt32(txtIdVenta.Text);
                if (txtidCli.Text != "" && txtidCli.Text != null)
                {
                    venta.IdCliente = Convert.ToInt32(txtidCli.Text);
                }

                venta.IdEmpleado = Convert.ToInt32(txtidEmp.Text);
                venta.TipoDocumento = combo_tipo_NCF.Text;
                venta.NroComprobante = txtNCF.Text;
                venta.Total = Convert.ToDecimal(txttotal.Text);
                venta.Tipofactura = cbtipofactura.Text;
                venta.Restante = restante;
                venta.Direccion = Program.Direccion ?? "Entregado en el Local";
                venta.rncCliente = txtrcnClient.Text;
                venta.FechaVenta = dateTimePicker1.Value;
                venta.UltimaFechaPago = dateTimePicker1.Value;
                venta.NombreCliente = Program.datoscliente;
                venta.borrador = 0;

                if (TempData.tempSalesData != null && Program.isSaler)
                {
                    TempData.tempSalesData.Add(venta);

                    clsGenericList.idsVentas.Add(venta.IdVenta);
                    if (TempData.tempSalesData.Count > 0)
                        clsGenericList.totalGanancia = clsGenericList.Ganancias(clsGenericList.idsVentas);
                }
            }

            var procedureDetalle = Program.isSaler ? "RegistrarDetalleVenta" : "RegistrarDetalleCotizacion";
            using (SqlCommand cmd1 = new SqlCommand(procedureDetalle, M.conexion))
                foreach (DataGridViewRow row in dgvVenta.Rows)
                {
                    M.Desconectar();
                    cmd1.CommandType = CommandType.StoredProcedure;

                    decimal Ganancia = 0;
                    int idProducto = Convert.ToInt32(row.Cells["IDP"].Value);
                    int idventa = 0;

                    if (!Program.isSaler)
                    {
                        Ganancia = listProducts.FirstOrDefault(x => x.ID == idProducto).Precio;
                        idventa = Convert.ToInt32(txtIdVenta.Text);
                    }
                    else
                    {
                        decimal preciocompra = listProducts.FirstOrDefault(x => x.ID == idProducto).Precio;
                        decimal precioUnitario = Convert.ToDecimal(row.Cells["PrecioU"].Value);
                        decimal cantidad = Convert.ToDecimal(row.Cells["cantidadP"].Value);

                        Ganancia = Math.Round((precioUnitario - preciocompra) * cantidad);
                        idventa = Convert.ToInt32(row.Cells["IdD"].Value);
                    }

                    //Tabla detalles ventas
                    cmd1.Parameters.Add(Program.isSaler ? "@IdVenta" : "@IdCotizacion", SqlDbType.Int).Value = idventa;
                    cmd1.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = Convert.ToDecimal(row.Cells["cantidadP"].Value);
                    cmd1.Parameters.Add("@detalles", SqlDbType.NVarChar).Value = Convert.ToString(row.Cells["DescripcionP"].Value);
                    cmd1.Parameters.Add("@PrecioUnitario", SqlDbType.Float).Value = Convert.ToDouble(row.Cells["PrecioU"].Value);
                    cmd1.Parameters.Add("@SubTotal", SqlDbType.Float).Value = Convert.ToDouble(row.Cells["SubtoTal"].Value);
                    cmd1.Parameters.Add("@IdProducto", SqlDbType.Int).Value = idProducto;
                    cmd1.Parameters.Add("@Igv", SqlDbType.Float).Value = Convert.ToDouble(row.Cells["IGV"].Value);
                    cmd1.Parameters.Add("@GananciaVenta", SqlDbType.Float).Value = Ganancia;

                    M.Conectar();
                    cmd1.ExecuteNonQuery();
                    cmd1.Parameters.Clear();
                    M.Desconectar();
                }

            if (Program.isSaler)
            {
                foreach (DataGridViewRow row in dgvVenta.Rows)
                {
                    M.Desconectar();
                    SqlCommand sqlCommand = new SqlCommand("UpdateStock", M.conexion);
                    using (SqlCommand cmd3 = sqlCommand)
                    {
                        cmd3.CommandType = CommandType.StoredProcedure;

                        //UpdateStock
                        cmd3.Parameters.Add("@Cantidad", SqlDbType.Decimal).Value = Convert.ToDecimal(row.Cells["cantidadP"].Value);
                        cmd3.Parameters.Add("@IdProducto", SqlDbType.Int).Value = Convert.ToInt32(row.Cells["IDP"].Value);

                        M.Conectar();
                        cmd3.ExecuteNonQuery();
                        M.Desconectar();

                        if (clsGenericList.listProducto != null)
                        {
                            var producto = clsGenericList.listProducto.FirstOrDefault(x => x.m_IdP == Convert.ToInt32(row.Cells["IDP"].Value));
                            producto.m_Stock = producto.m_Stock - Convert.ToDecimal(row.Cells["cantidadP"].Value);

                            Producto updateproducto = new Producto();
                            updateproducto = producto;

                            clsGenericList.listProducto.Remove(producto);
                            clsGenericList.listProducto.Add(updateproducto);
                        }
                    }
                }

                using (SqlCommand cmd2 = new SqlCommand("pagos_re", M.conexion))
                {
                    M.Desconectar();
                    cmd2.CommandType = CommandType.StoredProcedure;

                    //Tabla de pago
                    cmd2.Parameters.Add("@IdVenta", SqlDbType.Int).Value = Convert.ToInt32(txtIdVenta.Text);
                    cmd2.Parameters.Add("@id_pago", SqlDbType.Int).Value = Program.idPago;
                    cmd2.Parameters.Add("@id_caja", SqlDbType.Int).Value = Program.idcaja;
                    cmd2.Parameters.Add("@monto", SqlDbType.Decimal).Value = Convert.ToDecimal(txttotal.Text);
                    cmd2.Parameters.Add("@ingresos", SqlDbType.Decimal).Value = Program.pagoRealizado;

                    if (Program.Devuelta > 0)
                    {
                        cmd2.Parameters.Add("@egresos", SqlDbType.Decimal).Value = Program.Devuelta;
                    }
                    else
                    {
                        cmd2.Parameters.Add("@egresos", SqlDbType.Decimal).Value = 0;
                    }

                    cmd2.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Convert.ToDateTime(Program.Fechapago);

                    if (cbtipofactura.Text == "Credito")
                    {
                        cmd2.Parameters.Add("@deuda", SqlDbType.Decimal).Value = restante;
                    }
                    else
                    {
                        cmd2.Parameters.Add("@deuda", SqlDbType.Decimal).Value = 0;
                    }


                    M.Conectar();
                    cmd2.ExecuteNonQuery();
                    M.Desconectar();
                }

                Program.pagoRealizado = 0;
            }
        }

        private void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            RegistrarVenta();
        }

        private void Limpiar1()
        {
            cbidentificacion.Visible = true;
            txtidCli.Text = "0";
            cbidentificacion.Checked = false;
            Program.Esabono = "";
            txtIgv.Clear();
            txtDocIdentidad.Clear();
            txtDatos.Clear();
            dgvVenta.Rows.Clear();
            Program.IdCliente = 0;
            txtIdProducto.Clear();
            Program.pagoRealizado = 0;
            Program.DocumentoIdentidad = "";
            Program.ApellidosCliente = "";
            Program.NombreCliente = "";
            Program.Id = 0;
            Program.total = 0;
            Program.ST = 0;
            Program.igv = 0;
            Program.fecha = "";
            Program.rncClient = "";
            Program.NroComprobante = "";
            Program.NCF = "";
            txtNCF.Clear();
            lst.Clear();
            txtIgv.Text = "";
            txtdireccion.Text = "";
            Program.Direccion = "";
            Program.realizopago = false;
            Program.ReImpresion = "";
            Program.datoscliente = "";
            lblabono.Visible = false;
            lbltituloabono.Visible = false;
            lblabono.Text = null;
            listProducts.Clear();
        }

        public void tickEstilo()
        {
            string nombre = "";
            string cedula = "";
            CrearTiket ticket = new CrearTiket();

            //cabecera del ticket.
            if (Program.ReImpresion != "")
            {
                ticket.TextoDerecha(Program.ReImpresion);
            }

            //System.Drawing.Image img = System.Drawing.Image.FromFile("LogoCepeda.png");
            //ticket.HeaderImage = img;
            ticket.TextoCentro(lblLogo.Text);
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda(lblDir.Text);
            ticket.TextoIzquierda("Tel: " + lblTel1.Text + "/" + lblTel2.Text);
            ticket.TextoIzquierda("Correo: " + lblCorreo.Text);
            ticket.TextoIzquierda("Tipo de Comprobante: " + combo_tipo_NCF.Text);
            ticket.TextoIzquierda("Tipo de Factura: " + cbtipofactura.Text.ToUpper());
            ticket.TextoIzquierda("Numero de Comprobante: " + txtNCF.Text);
            ticket.TextoIzquierda("RNC: " + lblrnc.Text);
            ticket.TextoExtremos("CAJA #1", Program.isSaler ? "ID VENTA: " + txtIdVenta.Text : "ID Cotizacion: " + txtIdVenta.Text);
            ticket.lineasGuio();

            if (Program.datoscliente != "" && Program.IdCliente == 0)
            {
                nombre = Program.datoscliente;
                cedula = "Sin identificacion";
            }
            else
            {
                nombre = txtDatos.Text;
                cedula = txtDocIdentidad.Text;
            }

            //SUB CABECERA.
            ticket.TextoIzquierda("Atendido Por: " + txtUsu.Text);
            ticket.TextoIzquierda("Cliente: " + nombre);
            ticket.TextoIzquierda("Direccion de la Entrega: " + Program.Direccion);
            ticket.TextoIzquierda("Documento de identificacion: " + cedula);
            if (txtrcnClient.Text != "sin rcn del Cliente")
            {
                ticket.TextoIzquierda("RNC Cliente: " + txtrcnClient.Text);
            }
            ticket.TextoIzquierda("Fecha: " + dateTimePicker1.Value.Day + "/" + dateTimePicker1.Value.Month + "/" + dateTimePicker1.Value.Year);

            if (cbtipofactura.Text.ToLower() == "credito" && Program.Esabono == "Es Abono")
            {
                ticket.TextoIzquierda("Fecha Abono: " + DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year);
            }

            //ARTICULOS A VENDER.
            ticket.EncabezadoVenta();// NOMBRE DEL ARTICULO, CANT, PRECIO, IMPORTE
            ticket.lineasGuio();

            //SI TIENE UN DATAGRIDVIEW DONDE ESTAN SUS ARTICULOS A VENDER PUEDEN USAR ESTA MANERA PARA AGREARLOS
            foreach (DataGridViewRow fila in dgvVenta.Rows)
            {
                ticket.AgregaArticulo((fila.Cells["DescripcionP"].Value.ToString()).Trim(), int.Parse((fila.Cells["cantidadP"].Value.ToString()).Trim()),
                decimal.Parse((fila.Cells["SubtoTal"].Value.ToString()).Trim()), decimal.Parse((fila.Cells["IGV"].Value.ToString()).Trim()));
            }
            ticket.TextoIzquierda(" ");

            //resumen de la venta
            ticket.AgregarTotales("SUB-TOTAL    : ", decimal.Parse(lblsubt.Text));
            ticket.AgregarTotales("ITBIS     : ", decimal.Parse(lbligv.Text));
            ticket.AgregarTotales("TOTAL A PAGAR    : ", decimal.Parse(txttotal.Text));

            if (cbtipofactura.Text.ToLower() == "credito")
            {
                ticket.AgregarTotales("RESTANTE : ", decimal.Parse(restante.ToString()));
            }

            ticket.TextoIzquierda(" ");
            ticket.TextoCentro("__________________________________");

            //TEXTO FINAL DEL TICKET
            ticket.TextoIzquierda("EXTRA");
            ticket.TextoIzquierda("Nota: Los productos con garantia pierden su garantia si deja perder la factura");
            ticket.TextoIzquierda("FAVOR REVISE SU MERCANCIA AL RECIBIRLA");
            ticket.TextoCentro("!GRACIAS POR SU COMPRA!");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.CortaTicket();//CORTAR TICKET
            ticket.ImprimirTicket("POS-80");//NOMBRE DE LA IMPRESORA
        }

        private void txtPVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Program.abiertosecundarias == false)
            {
                if (Program.isSaler)
                {
                    frmListadoVentas F = new frmListadoVentas();
                    F.btnCancelar.Visible = false;
                    Program.abiertosecundarias = false;
                    Program.abierto = false;
                    F.Show();
                }
                else
                {
                    frmlistadodecotizacion F = new frmlistadodecotizacion();
                    Program.abiertosecundarias = false;
                    Program.abierto = false;
                    button3.Visible = true;
                    btnRegistrarVenta.Visible = false;
                    F.Show();
                }
            }
        }

        public void buscarid()
        {
            M.Conectar();
            string sql = "SELECT id_ncf FROM ncf WHERE descripcion_ncf =@id";
            SqlCommand cmd = new SqlCommand(sql, M.conexion);
            cmd.Parameters.AddWithValue("@id", combo_tipo_NCF.Text);

            SqlDataReader reade = cmd.ExecuteReader();
            if (reade.Read())
            {
                txtid.Text = Convert.ToString(reade["id_ncf"]);
            }

            M.Desconectar();
            actualzarestadoscomprobantes();
        }

        private void comboselectNCF(int id_ncf)
        {
            M.Desconectar();
            int secuencia = 0;
            try
            {
                SqlDataReader LectorSecuencia;

                M.Conectar();
                SqlCommand Comando = new SqlCommand();
                Comando.Connection = M.conexion;
                Comando.CommandText = "Select * From ncf where id_ncf like '%" + id_ncf + "%'";
                LectorSecuencia = Comando.ExecuteReader();

                if (LectorSecuencia.Read() == true)
                {
                    txtNCF.Text = LectorSecuencia.GetString(2);
                    secuencia = LectorSecuencia.GetInt32(3);
                    txtNCF.Text = txtNCF.Text + secuencia.ToString("00000000");

                    chkComprobante.Checked = true;
                }
                else
                {
                    MessageBox.Show("No existe un registro con este codigo, verifique y trate de nuevo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    return;
                }

                M.Desconectar();
                LectorSecuencia.Close();
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
        }

        private void combo_tipo_NCF_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboselectNCF(Convert.ToInt32(combo_tipo_NCF.SelectedValue));
        }

        private void combo_tipo_NCF_SelectedIndexChanged(object sender, EventArgs e)
        {
            buscarid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Limpiar();
            Limpiar1();

            btnRegistrarVenta.Hide();
            btnImprimir.Visible = false;
            btnAgregar.Visible = false;
            button2.Visible = false;
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Program.abiertosecundarias = false;
            Program.abierto = false;
            btnImprimir.Visible = false;
            btnAgregar.Visible = false;
            Program.pagoRealizado = 0;
            Limpiar();
            Limpiar1();

            txtPVenta.Enabled = Program.isAdminUser;
            txtIgv.Enabled = Program.isAdminUser;

            this.Close();
        }

        private void dgvVenta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVenta.Rows.Count > 0)
            { dgvVenta.Rows[dgvVenta.CurrentRow.Index].Selected = true; }
        }

        private void btnImprimir_Click_1(object sender, EventArgs e)
        {
            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Que tipo de factura desea? \n Si=Pequeña \n No=Grande ", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                tickEstilo();
            }
            else
            {
                To_pdf();
            }
            btnImprimir.Visible = false;
            Limpiar1();
        }

        private void To_pdf()
        {
            string nombre = "";
            string cedula = "";
            Document doc = new Document(PageSize.LETTER, 10f, 10f, 10f, 0f);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            Image image1 = Image.GetInstance("ferreteria.png");
            image1.ScaleAbsoluteWidth(140);
            image1.ScaleAbsoluteHeight(70);
            saveFileDialog1.InitialDirectory = @"C:";
            saveFileDialog1.Title = "Factura para " + txtDatos.Text;
            saveFileDialog1.DefaultExt = "pdf";
            saveFileDialog1.Filter = "pdf Files (*.pdf)|*.pdf| All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            string filename = "Venta" + DateTime.Now.ToString();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = saveFileDialog1.FileName;

                if (filename.Trim() != "")
                {
                    FileStream file = new FileStream(filename,
                    FileMode.OpenOrCreate,
                    FileAccess.ReadWrite,
                    FileShare.ReadWrite);
                    PdfWriter.GetInstance(doc, file);
                    doc.Open();
                    string remito = lblLogo.Text;
                    string ubicado = lblDir.Text;
                    string envio = "Fecha : " + +dateTimePicker1.Value.Day + "/" + dateTimePicker1.Value.Month + "/" + dateTimePicker1.Value.Year;

                    Chunk chunk = new Chunk(remito, FontFactory.GetFont("ARIAL", 16, iTextSharp.text.Font.BOLD, color: BaseColor.BLUE));
                    if (Program.ReImpresion != null)
                    {
                        doc.Add(new Paragraph("                                                                                                                                                                                                                                                                                                                                                                                            " + Program.ReImpresion, FontFactory.GetFont("ARIAL", 5, iTextSharp.text.Font.ITALIC, color: BaseColor.RED)));
                    }

                    if (Program.datoscliente != "" && Program.IdCliente == 0)
                    {
                        nombre = Program.datoscliente;
                        cedula = "Sin identificacion";
                    }
                    else
                    {
                        nombre = txtDatos.Text;
                        cedula = txtDocIdentidad.Text;
                    }

                    var fecha = new Paragraph(envio, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.ITALIC));

                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    image1.Alignment = Image.TEXTWRAP | Image.ALIGN_CENTER;
                    doc.Add(image1);
                    var chuckalign = new Paragraph(chunk);
                    chuckalign.Alignment = Element.ALIGN_CENTER;
                    doc.Add(chuckalign);
                    var ubicacionalign = new Paragraph(ubicado, FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.NORMAL));
                    ubicacionalign.Alignment = Element.ALIGN_CENTER;
                    doc.Add(ubicacionalign);
                    var telefonos = new Paragraph("Tel: " + lblTel1.Text + " / " + lblTel2.Text, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL));
                    telefonos.Alignment = Element.ALIGN_CENTER;
                    doc.Add(telefonos);
                    var RNC = new Paragraph("RNC: " + lblrnc.Text, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL));
                    RNC.Alignment = Element.ALIGN_CENTER;
                    doc.Add(RNC);

                    doc.Add(new Paragraph(" "));
                    if (cbtipofactura.Text.ToLower() == "credito" && Program.Esabono == "Es Abono")
                    {
                        var fechaabono = new Paragraph("Fecha Abono: " + DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.ITALIC));
                        doc.Add(fechaabono);
                    }
                    doc.Add(new Paragraph("Atendido por: " + txtUsu.Text, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    if (Program.isSaler)
                    {
                        doc.Add(new Paragraph("Tipo de Factura: " + cbtipofactura.Text.ToUpper(), FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                        doc.Add(new Paragraph("Tipo de Comprobante: " + combo_tipo_NCF.Text, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                        doc.Add(new Paragraph("Numero de Comprobante: " + txtNCF.Text, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                        if (txtrcnClient.Text != "sin rcn del Cliente")
                        {
                            doc.Add(new Paragraph("RNC Cliente: " + txtrcnClient.Text, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                        }
                        doc.Add(new Paragraph("Cliente: " + nombre, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                        doc.Add(new Paragraph("Direccion de la Entrega: " + Program.Direccion, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                        doc.Add(new Paragraph("Documento de identificacion: " + cedula, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                        doc.Add(new Paragraph(" "));
                    }
                    else
                    {
                        doc.Add(new Paragraph("COTIZACION", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    }
                    doc.Add(new Paragraph(" "));

                    GenerarDocumento(doc);
                    doc.AddCreationDate();
                    if (dgvVenta.Rows.Count >= 1)
                    {
                        int filas = 20 - dgvVenta.Rows.Count;
                        if (filas > 1)
                        {
                            for (int i = 1; i < filas; i++)
                            {
                                doc.Add(new Paragraph("                       "));
                            }
                        }
                    }

                    decimal Sub = Convert.ToDecimal(lblsubt.Text);
                    decimal ITBIS = Convert.ToDecimal(lbligv.Text);
                    decimal total = Convert.ToDecimal(txttotal.Text);

                    doc.Add(new Paragraph("Sub-Total   : " + Sub.ToString("C2"), FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("ITBIS   : " + ITBIS.ToString("C2"), FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("Total a Pagar   : " + total.ToString("C2"), FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));

                    if (cbtipofactura.Text.ToLower() == "credito")
                    {
                        doc.Add(new Paragraph("Total de Restante : " + restante.ToString("C2"), FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    }

                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("_________________________" + "                                                                                                                                                 " + "_________________________", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("      Facturado Por      " + "                                                                                                                                                                         " + "     Recibido Por  ", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("                       "));

                    var Nota = new Paragraph("Nota: Los productos con garantia pierden su garantia si deja perder la factura.", FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.ITALIC, color: BaseColor.RED));
                    Nota.Alignment = Element.ALIGN_RIGHT;
                    var favor = new Paragraph("FAVOR REVISE SU MERCANCIA AL RECIBIRLA", FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.ITALIC, color: BaseColor.RED));
                    favor.Alignment = Element.ALIGN_RIGHT;
                    var gracias = new Paragraph("!GRACIAS POR PREFERIRNOS!", FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.ITALIC, color: BaseColor.RED));
                    gracias.Alignment = Element.ALIGN_RIGHT;

                    doc.Add(Nota);
                    doc.Add(favor);
                    doc.Add(gracias);

                    doc.Close();
                    Process.Start(filename);//Esta parte se puede omitir, si solo se desea guardar el archivo, y que este no se ejecute al instante
                }
            }
            else
            {
                MessageBox.Show("No guardo el Archivo");
            }
        }

        public void GenerarDocumento(Document document)
        {
            int i, j;
            PdfPTable datatable = new PdfPTable(dgvVenta.ColumnCount);
            float[] headerwidths = GetTamañoColumnas(dgvVenta);
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 100;
            datatable.DefaultCell.BorderWidth = 1;
            datatable.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
            for (i = 0; i < dgvVenta.ColumnCount; i++)
            {
                datatable.AddCell(new Phrase(dgvVenta.Columns[i].HeaderText, FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD)));
            }
            datatable.HeaderRows = 1;
            datatable.DefaultCell.BorderWidth = 1;
            for (i = 0; i < dgvVenta.Rows.Count; i++)
            {
                for (j = 0; j < dgvVenta.Columns.Count; j++)
                {
                    if (dgvVenta[j, i].Value != null)
                    {
                        datatable.AddCell(new Phrase(dgvVenta[j, i].Value.ToString(), FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));//En esta parte, se esta agregando un renglon por cada registro en el datagrid
                    }
                }
                datatable.CompleteRow();
            }
            document.Add(datatable);
        }

        public float[] GetTamañoColumnas(DataGridView dg)
        {
            float[] values = new float[dg.ColumnCount];
            for (int i = 0; i < dg.ColumnCount; i++)
            {
                values[i] = (float)dg.Columns[i].Width;
            }
            return values;
        }

        private void dgvVenta_Click(object sender, EventArgs e)
        {
            btnEliminarItem.Enabled = true;
        }

        private void txtPVenta_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            M.Desconectar();
            restante = Convert.ToDecimal(txttotal.Text) - Program.pagoRealizado;

            using (SqlCommand cmd = new SqlCommand("AbonaraVenta", M.conexion))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                //tabla Ventas
                cmd.Parameters.Add("@IdVenta", SqlDbType.Int).Value = Program.Id;
                cmd.Parameters.Add("@Restante", SqlDbType.Decimal).Value = restante;

                M.Conectar();
                cmd.ExecuteNonQuery();
                M.Desconectar();

                if (clsGenericList.idsVentas.Contains(Program.Id))
                {
                    var venta = TempData.tempSalesData.FirstOrDefault(x => x.IdVenta == Program.Id);
                    venta.Restante = restante;

                    Venta ventaup = new Venta();
                    ventaup = venta;

                    TempData.tempSalesData.Remove(venta);
                    TempData.tempSalesData.Add(ventaup);
                }
            }

            using (SqlCommand cmd2 = new SqlCommand("Actualizarpagos_re", M.conexion))
            {
                cmd2.CommandType = CommandType.StoredProcedure;

                //Tabla de pago
                cmd2.Parameters.Add("@IdVenta", SqlDbType.Int).Value = Program.Id;
                cmd2.Parameters.Add("@id_pago", SqlDbType.Int).Value = Program.idPago;
                cmd2.Parameters.Add("@id_caja", SqlDbType.Int).Value = Program.idcaja;
                cmd2.Parameters.Add("@id_cajaAnterior", SqlDbType.Int).Value = idcaja;
                cmd2.Parameters.Add("@monto", SqlDbType.Decimal).Value = Program.Caja;
                cmd2.Parameters.Add("@ingresos", SqlDbType.Decimal).Value = Program.pagoRealizado;
                if (Program.Devuelta > 0)
                {
                    cmd2.Parameters.Add("@egresos", SqlDbType.Decimal).Value = Program.Devuelta;
                }
                else
                {
                    cmd2.Parameters.Add("@egresos", SqlDbType.Decimal).Value = 0;
                }
                cmd2.Parameters.Add("@fecha", SqlDbType.DateTime).Value = Convert.ToDateTime(Program.Fechapago);
                cmd2.Parameters.Add("@deuda", SqlDbType.Decimal).Value = restante;

                M.Conectar();
                cmd2.ExecuteNonQuery();
                M.Desconectar();
            }

            Program.pagoRealizado = 0;

            if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Que tipo de factura desea? \n Si=Pequeña \n No=Grande ", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
            {
                tickEstilo();
            }
            else
            {
                To_pdf();
            }

            llenar();
            Limpiar();
            Limpiar1();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbidentificacion.Checked == true)
            {
                btnBuscar.Show();
                txtDatos.ReadOnly = true;
                cbidentificacion.Visible = false;
            }
            else
            {
                btnBuscar.Hide();
                txtDatos.ReadOnly = false;
            }
        }

        private void chkComprobante_CheckedChanged(object sender, EventArgs e)
        {
            if (chkComprobante.Checked)
            {
                txtrcnClient.Text = "";
                txtrcnClient.Show();
                lblrcnClient.Show();
            }
            else
            {
                txtrcnClient.Text = "sin rcn del Cliente";
                txtrcnClient.Hide();
                lblrcnClient.Hide();
                txtNCF.Text = "Sin NCF";
                combo_tipo_NCF.Text = "Ningun Tipo de Comprobante";
            }
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void FrmRegistroVentas_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void RegistrarVenta()
        {
            if (dgvVenta.Rows.Count > 0)
            {
                if (chkComprobante.Checked == true && Program.isSaler)
                {
                    using (SqlCommand cmd = new SqlCommand("generar", M.conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_ncf", SqlDbType.Int).Value = Convert.ToInt32(txtid.Text);
                        cmd.Parameters.Add("@id_secuencia", SqlDbType.Int).Value = Convert.ToInt32(combo_tipo_NCF.SelectedIndex);
                        cmd.Parameters.Add("@secuencia", SqlDbType.NVarChar).Value = txtNCF.Text;

                        M.Conectar();
                        cmd.ExecuteNonQuery();
                        M.Desconectar();

                        buscarid();
                    }
                }
                else
                {
                    txtNCF.Text = "Sin NCF";
                    combo_tipo_NCF.Text = "Ningun Tipo de Comprobante";
                }

                VentaRealizada();

                if (DevComponents.DotNetBar.MessageBoxEx.Show("¿Que tipo de factura desea? \n Si=Pequeña \n No=Grande ", "Sistema de Ventas.", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    tickEstilo();
                }
                else
                {
                    To_pdf();
                }

                llenar();
                Limpiar();
                Limpiar1();
            }
            else
            {
                MessageBox.Show("Debe Agregar al menos una Venta");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.isSaler = false;
            RegistrarVenta();
        }
    }
}

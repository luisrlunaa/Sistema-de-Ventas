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

        private List<clsVenta> lst = new List<clsVenta>();
        private List<PrecioCompraProducto> listProducts = new List<PrecioCompraProducto>();
        clsCx Cx = new clsCx();
        public FrmRegistroVentas()
        {
            InitializeComponent();
        }
        private void FrmVentas_Load(object sender, EventArgs e)
        {
            txtidCli.Text = null;
            listProducts.Clear();
            Program.IdCliente = 0;
            cbidentificacion.Checked = false;
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

            if (Program.CargoEmpleadoLogueado != "Administrador")
            {
                txtPVenta.Enabled = false;
                txtIgv.Enabled = false;
            }

            txtid.Text = "0";
            Program.ReImpresion = "";
            Program.datoscliente = "";
            Program.realizopago = false;
            actualzarestadoscomprobantes();
            //llenar_data_ncf();
            cargar_combo_NCF(combo_tipo_NCF);
            cargar_combo_Tipofactura(cbtipofactura);
            btnRegistrarVenta.Hide();
            btnImprimir.Visible = false;
            btnAgregar.Visible = false;
            button2.Visible = false;
            llenar();
            btnEliminarItem.Enabled = false;
            frmPagar pa = new frmPagar();
            lbltotal.Text = Convert.ToString(pa.txtmonto.Text);

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
                    combo_tipo_NCF.Text = "Ningún Tipo de Comprobante";
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
                Cx.conexion.Open();
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
                        Cx.conexion.Close();
                        using (SqlConnection con = new SqlConnection(Cx.conet))
                        {
                            using (SqlCommand cmdup = new SqlCommand("UpdateState", con))
                            {
                                cmdup.CommandType = CommandType.StoredProcedure;
                                cmdup.Parameters.Add("@id", SqlDbType.Int).Value = item;
                                con.Open();
                                cmdup.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
                Cx.conexion.Close();
            }
        }

        public void cargar_combo_NCF(ComboBox combo_tipo_NCF)
        {
            SqlCommand cm = new SqlCommand("CARGARcomboNCF", Cx.conexion);
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
            SqlCommand cm = new SqlCommand("CARGARcomboTipoFactura", Cx.conexion);
            cm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            da.Fill(dt);

            tipofactura.DisplayMember = "descripcion";
            tipofactura.ValueMember = "id";
            tipofactura.DataSource = dt;
        }

        public void llenar_data_ncf()
        {   //declaramos la cadena  de conexion
            string cadenaconexion = Cx.conet;
            //variable de tipo Sqlconnection
            SqlConnection con = new SqlConnection();
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            con.ConnectionString = cadenaconexion;
            comando.Connection = con;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "SELECT  id_secuencia, secuenciaNCF, fecha from NCFGenerados";
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            con.Open();
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
            con.Close();
        }
        public void llenar()
        {
            string cadSql = "select top(1) IdVenta from Venta order by IdVenta desc";

            SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);
            Cx.conexion.Open();

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
            Cx.conexion.Close();
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            FrmListadoClientes C = new FrmListadoClientes();
            C.Show();
        }

        bool activar;
        private void FrmVentas_Activated(object sender, EventArgs e)
        {
            if (Program.IdCliente != 0)
            {
                txtDatos.Text = Program.ApellidosCliente + " " + Program.NombreCliente;
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
            txtStock.Text = Program.Stock + "";
            txtPVenta.Text = Program.PrecioVenta + "";
            txtIgv.Text = Program.itbis + "";
            txtIdV.Text = Program.Id + "";
            lbltotal.Text = Program.total + "";
            lblsubt.Text = Program.ST + "";
            lbligv.Text = Program.igv + "";
            txtpmax.Text = Program.Pmax + "";
            txtpmin.Text = Program.Pmin + "";
            txtImei.Text = Program.Imei;

            if (Program.IdProducto > 0)
            {
                llenar_data(Program.IdProducto + "");
            }

            if (tienefila && Program.abiertoimei == true)
            {
                frmlistadoimei imei = new frmlistadoimei();
                imei.txtIdP.Text = Program.IdProducto + "";
                imei.Show();
            }

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
                btnRegistrarVenta.Visible = false;
                btnSalir.Visible = true;
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


            if (activar == true)
            {
                cbtipofactura.Text = Program.tipo;
                combo_tipo_NCF.Text = Program.NCF;
                txtNCF.Text = Program.NroComprobante;
                lbltotal.Text = Program.total + "";
                lblsubt.Text = Program.ST + "";
                lbligv.Text = Program.igv + "";
                txtidEmp.Text = Program.IdEmpleado + "";
                dateTimePicker1.Text = Program.fecha;

                decimal subtotal = 0;
                decimal igv = 0;

                //declaramos la cadena  de conexion
                string cadenaconexion = Cx.conet;
                //variable de tipo Sqlconnection
                SqlConnection con = new SqlConnection();
                //variable de tipo Sqlcommand
                SqlCommand comando = new SqlCommand();
                //variable SqlDataReader para leer los datos
                SqlDataReader dr;
                con.ConnectionString = cadenaconexion;
                comando.Connection = con;
                //declaramos el comando para realizar la busqueda
                comando.CommandText = "SELECT dbo.DetalleVenta.detalles_P,ISNULL(dbo.DetalleVenta.imei, 'Sin Imei') AS imei, dbo.DetalleVenta.SubTotal," +
                    "dbo.DetalleVenta.IdVenta, dbo.DetalleVenta.Cantidad,dbo.DetalleVenta.PrecioUnitario,dbo.DetalleVenta.idProducto,dbo.DetalleVenta.Igv" +
                    " from DetalleVenta WHERE DetalleVenta.IdVenta ='" + txtIdV.Text + "'";
                //especificamos que es de tipo Text
                comando.CommandType = CommandType.Text;
                //se abre la conexion
                con.Open();
                //limpiamos los renglones de la datagridview
                dgvVenta.Rows.Clear();
                //a la variable DataReader asignamos  el la variable de tipo SqlCommand
                dr = comando.ExecuteReader();
                while (dr.Read())
                {
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
                        dgvVenta.Rows[renglon].Cells["ImeiC"].Value = dr.GetString(dr.GetOrdinal("imei"));

                        subtotal += (dr.GetDecimal(dr.GetOrdinal("SubtoTal")) * dr.GetInt32(dr.GetOrdinal("Cantidad")));
                        igv += (dr.GetDecimal(dr.GetOrdinal("Igv")) * dr.GetInt32(dr.GetOrdinal("Cantidad")));
                    }
                }

                Program.ST = subtotal;
                Program.igv = igv;

                con.Close();

                buscaridcaja();
            }
        }

        public int idcaja = 0;
        public void buscaridcaja()
        {
            Cx.conexion.Close();
            Cx.conexion.Open();
            string sql = "select id_caja from Caja where fecha = convert(datetime,CONVERT(varchar(10), @fecha, 103),103)";
            SqlCommand cmd = new SqlCommand(sql, Cx.conexion);
            cmd.Parameters.AddWithValue("@fecha", dateTimePicker1.Value);

            SqlDataReader reade = cmd.ExecuteReader();
            if (reade.Read())
            {
                idcaja = Convert.ToInt32(reade["id_caja"]);
            }

            Cx.conexion.Close();
        }

        private void btnBusquedaProducto_Click(object sender, EventArgs e)
        {
            if (Program.abiertosecundario == false)
            {
                FrmListadoProductos P = new FrmListadoProductos();
                btnAgregar.Visible = true;
                Program.datoscliente = txtDatos.Text;
                Program.abiertosecundario = true;
                P.Show();
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsVenta V = new clsVenta();
            PrecioCompraProducto PCP = new PrecioCompraProducto();
            if (txtDescripcion.Text.Trim() != "")
            {
                if (txtCantidad.Text.Trim() != "")
                {
                    if (Convert.ToInt32(txtCantidad.Text) >= 0)
                    {
                        if (Convert.ToInt32(txtCantidad.Text) <= Convert.ToInt32(txtStock.Text))
                        {
                            V.IdProducto = Convert.ToInt32(txtIdProducto.Text);
                            V.IdVenta = Convert.ToInt32(txtIdVenta.Text);
                            V.Descripcion = (txtDescripcion.Text + "-" + txtMarca.Text).Trim();
                            V.Cantidad = Convert.ToInt32(txtCantidad.Text);
                            if (Convert.ToDecimal(txtIgv.Text) > 0)
                            {
                                V.Igv = Convert.ToDecimal(txtIgv.Text);
                            }
                            V.PrecioCompra = Program.PrecioCompra;

                            V.PrecioVenta = Convert.ToDecimal(txtPVenta.Text);
                            if (txtImei.Text != "")
                            {
                                V.imei = txtImei.Text;
                            }
                            else
                            {
                                V.imei = "Sin Imei";
                            }
                            V.SubTotal = Math.Round((Convert.ToDecimal(txtPVenta.Text) + Convert.ToDecimal(txtIgv.Text)) * Convert.ToInt32(txtCantidad.Text), 2);
                            btnAgregar.Visible = false;
                            lst.Add(V);
                          
                            PCP.ID = Convert.ToInt32(txtIdProducto.Text);
                            PCP.Precio = Program.PrecioCompra;
                            listProducts.Add(PCP);

                            LlenarGri();

                            if (cbidentificacion.Checked == false && txtDatos.Text != "" && Program.IdCliente == 0)
                            {
                                txtDocIdentidad.Text = "Sin identificación";
                                txtDatos.Text = Program.datoscliente;
                            }

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
            Decimal SumaSubTotal = 0; Decimal SumaIgv = 0; Decimal SumaTotal = 0;
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
                dgvVenta.Rows[i].Cells["ImeiC"].Value = lst[i].imei;

                var preciounidad = Convert.ToDecimal(dgvVenta.Rows[i].Cells["PrecioU"].Value);
                var cantidad = Convert.ToInt32(dgvVenta.Rows[i].Cells["cantidadP"].Value);
                var igv = Convert.ToDecimal(dgvVenta.Rows[i].Cells["IGV"].Value);

                SumaSubTotal += preciounidad * cantidad;
                SumaIgv += igv * cantidad;

                SumaTotal += Math.Round(Convert.ToDecimal(dgvVenta.Rows[i].Cells["SubtoTal"].Value), 2);

                lblsubt.Text = Convert.ToString(SumaSubTotal);
                lbligv.Text = Convert.ToString(SumaIgv);
                lbltotal.Text = Convert.ToString(SumaTotal);

                Program.igv = Convert.ToDecimal(lbligv.Text);
                Program.ST = Convert.ToDecimal(lblsubt.Text);
                Program.total = Convert.ToDecimal(lbltotal.Text);
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
            txtImei.Text = "";
            Program.Imei = "";
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
            Program.total = Convert.ToDecimal(lbltotal.Text);
            Program.igv = Convert.ToDecimal(lbligv.Text);
            Program.ST = Convert.ToDecimal(lblsubt.Text);
            pa.txtmonto.Text = lbltotal.Text;

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
                    combo_tipo_NCF.Text = "Ningún Tipo de Comprobante";
                    txtid.Text = "0";
                }
            }

            if (cbidentificacion.Checked == false && Program.IdCliente == 0)
            {
                txtDatos.Text = Program.datoscliente;
                txtDocIdentidad.Text = "Sin Identificación";
            }

            pa.Show();

            Program.tipo = cbtipofactura.Text;
        }
        private void btnEliminarItem_Click(object sender, EventArgs e)
        {
            List<clsVenta> lista = new List<clsVenta>();
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

                    lblsubt.Text = Convert.ToString(SumaSubTotal);
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
            string procedure = "";
            using (SqlConnection con = new SqlConnection(Cx.conet))
            {
                if (Program.IdCliente > 0)
                {
                    procedure = "RegistrarVenta";
                }
                else
                {
                    procedure = "RegistrarVentasinIDcliente";
                }

                using (SqlCommand cmd = new SqlCommand(procedure, con))
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

                    cmd.Parameters.Add("@IdVenta", SqlDbType.Int).Value = Convert.ToInt32(txtIdVenta.Text);
                    cmd.Parameters.Add("@TipoFactura", SqlDbType.NVarChar).Value = cbtipofactura.Text;

                    if (cbtipofactura.Text == "Credito")
                    {
                        restante = Convert.ToDecimal(lbltotal.Text) - Program.pagoRealizado;
                        cmd.Parameters.Add("@Restante", SqlDbType.Decimal).Value = restante;
                    }
                    else
                    {
                        cmd.Parameters.Add("@Restante", SqlDbType.Decimal).Value = 0;
                    }

                    cmd.Parameters.Add("@Serie", SqlDbType.Int).Value = Convert.ToInt32(txtid.Text);
                    cmd.Parameters.Add("@NroDocumento", SqlDbType.NVarChar).Value = txtNCF.Text;
                    cmd.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = txtidEmp.Text;
                    cmd.Parameters.Add("@TipoDocumento", SqlDbType.VarChar).Value = combo_tipo_NCF.Text;
                    cmd.Parameters.Add("@FechaVenta", SqlDbType.DateTime).Value = dateTimePicker1.Text;
                    cmd.Parameters.Add("@Total", SqlDbType.Decimal).Value = Convert.ToDecimal(lbltotal.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                using (SqlCommand cmd1 = new SqlCommand("RegistrarDetalleVenta", con))
                    foreach (DataGridViewRow row in dgvVenta.Rows)
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        int idProducto = Convert.ToInt32(row.Cells["IDP"].Value);
                        decimal preciocompra = listProducts.FirstOrDefault(x => x.ID == idProducto).Precio;
                        decimal subtotal = Convert.ToDecimal(row.Cells["SubtoTal"].Value);
                        int cantidad = Convert.ToInt32(row.Cells["cantidadP"].Value);
                        decimal Ganancia = Math.Round(subtotal - (preciocompra * cantidad));

                        //Tabla detalles ventas
                        cmd1.Parameters.Add("@IdVenta", SqlDbType.Int).Value = Convert.ToInt32(row.Cells["IdD"].Value);
                        cmd1.Parameters.Add("@Cantidad", SqlDbType.Int).Value = Convert.ToInt32(row.Cells["cantidadP"].Value);
                        cmd1.Parameters.Add("@detalles", SqlDbType.NVarChar).Value = Convert.ToString(row.Cells["DescripcionP"].Value);
                        cmd1.Parameters.Add("@PrecioUnitario", SqlDbType.Float).Value = Convert.ToDouble(row.Cells["PrecioU"].Value);
                        cmd1.Parameters.Add("@SubTotal", SqlDbType.Float).Value = Convert.ToDouble(row.Cells["SubtoTal"].Value);
                        cmd1.Parameters.Add("@IdProducto", SqlDbType.Int).Value = idProducto;
                        cmd1.Parameters.Add("@Igv", SqlDbType.Float).Value = Convert.ToDouble(row.Cells["IGV"].Value);
                        cmd1.Parameters.Add("@GananciaVenta", SqlDbType.Float).Value = Ganancia;
                        cmd1.Parameters.Add("@imei", SqlDbType.NVarChar).Value = Convert.ToString(row.Cells["ImeiC"].Value);

                        if (Convert.ToString(row.Cells["ImeiC"].Value) != "Sin Imei")
                        {
                            using (SqlCommand cmd4 = new SqlCommand("Registrarimei", con))
                            {
                                cmd4.CommandType = CommandType.StoredProcedure;
                                cmd4.Parameters.Add("@idImei", SqlDbType.Int).Value = Convert.ToInt32(Program.idImei);
                                cmd4.Parameters.Add("@IdProducto", SqlDbType.Int).Value = Convert.ToInt32(row.Cells["IDP"].Value);
                                cmd4.Parameters.Add("@IMEI", SqlDbType.NVarChar).Value = Convert.ToString(row.Cells["ImeiC"].Value);
                                cmd4.Parameters.Add("@activo", SqlDbType.Char).Value = 0;
                                cmd4.Parameters.Add("@FechaModificacion", SqlDbType.Date).Value = dateTimePicker1.Text;

                                con.Open();
                                cmd4.ExecuteNonQuery();
                                con.Close();
                            }
                        }

                        con.Open();
                        cmd1.ExecuteNonQuery();
                        cmd1.Parameters.Clear();
                        con.Close();
                    }


                foreach (DataGridViewRow row in dgvVenta.Rows)
                {
                    SqlCommand sqlCommand = new SqlCommand("UpdateStock", con);
                    using (SqlCommand cmd3 = sqlCommand)
                    {
                        cmd3.CommandType = CommandType.StoredProcedure;

                        //UpdateStock
                        cmd3.Parameters.Add("@Cantidad", SqlDbType.Int).Value = Convert.ToInt32(row.Cells["cantidadP"].Value);
                        cmd3.Parameters.Add("@IdProducto", SqlDbType.Int).Value = Convert.ToInt32(row.Cells["IDP"].Value);

                        con.Open();
                        cmd3.ExecuteNonQuery();
                        con.Close();
                    }
                }

                using (SqlCommand cmd2 = new SqlCommand("pagos_re", con))
                {
                    cmd2.CommandType = CommandType.StoredProcedure;

                    //Tabla de pago
                    cmd2.Parameters.Add("@IdVenta", SqlDbType.Int).Value = Convert.ToInt32(txtIdVenta.Text);
                    cmd2.Parameters.Add("@id_pago", SqlDbType.Int).Value = Program.idPago;
                    cmd2.Parameters.Add("@id_caja", SqlDbType.Int).Value = Program.idcaja;
                    cmd2.Parameters.Add("@monto", SqlDbType.Decimal).Value = Convert.ToDecimal(lbltotal.Text);
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


                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }
                Program.pagoRealizado = 0;
                MessageBox.Show("Venta Registrada y Pago Confirmado");
            }
        }

        private void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            if (dgvVenta.Rows.Count > 0)
            {
                if (chkComprobante.Checked == true)
                {
                    using (SqlConnection con = new SqlConnection(Cx.conet))
                    {
                        Cx.conexion.Open();
                        string sql = "Select * From Venta Where IdVenta =@IdVenta";
                        SqlCommand Command = new SqlCommand(sql, Cx.conexion);
                        Command.Parameters.AddWithValue("@IdVenta", Convert.ToInt32(txtIdVenta.Text));

                        SqlDataReader reade = Command.ExecuteReader();
                        if (!reade.Read())
                        {
                            using (SqlCommand cmd = new SqlCommand("generar", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@id_ncf", SqlDbType.Int).Value = Convert.ToInt32(txtid.Text);
                                cmd.Parameters.Add("@id_secuencia", SqlDbType.Int).Value = Convert.ToInt32(combo_tipo_NCF.SelectedIndex);
                                cmd.Parameters.Add("@secuencia", SqlDbType.NVarChar).Value = txtNCF.Text;

                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();

                                //llenar_data_ncf();
                                buscarid();
                            }
                        }

                        Cx.conexion.Close();
                    }
                }
                else
                {
                    txtNCF.Text = "Sin NCF";
                    combo_tipo_NCF.Text = "Ningún Tipo de Comprobante";
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
            Program.NroComprobante = "";
            Program.NCF = "";
            txtNCF.Clear();
            lst.Clear();
            txtIgv.Text = "";
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
            ticket.TextoExtremos("CAJA #1", "ID VENTA: " + txtIdVenta.Text);
            ticket.lineasGuio();

            if (Program.datoscliente != "" && Program.IdCliente == 0)
            {
                nombre = Program.datoscliente;
                cedula = "Sin Identificación";
            }
            else
            {
                nombre = txtDatos.Text;
                cedula = txtDocIdentidad.Text;
            }

            //SUB CABECERA.
            ticket.TextoIzquierda("Atendido Por: " + txtUsu.Text);
            ticket.TextoIzquierda("Cliente: " + nombre);
            ticket.TextoIzquierda("Documento de Identificación: " + cedula);
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
                var imeiproducto = "";
                if ((fila.Cells["ImeiC"].Value.ToString()).Trim() is null)
                {
                    imeiproducto = "Sin Imei";
                }
                else
                {
                    imeiproducto = (fila.Cells["ImeiC"].Value.ToString()).Trim();
                }

                ticket.AgregaArticulo((fila.Cells["DescripcionP"].Value.ToString()).Trim(), int.Parse((fila.Cells["cantidadP"].Value.ToString()).Trim()),
                decimal.Parse((fila.Cells["SubtoTal"].Value.ToString()).Trim()), decimal.Parse((fila.Cells["IGV"].Value.ToString()).Trim()), imeiproducto);
            }
            ticket.TextoIzquierda(" ");

            //resumen de la venta
            ticket.AgregarTotales("SUB-TOTAL    : ", decimal.Parse(lblsubt.Text));
            ticket.AgregarTotales("ITBIS     : ", decimal.Parse(lbligv.Text));
            ticket.AgregarTotales("TOTAL A PAGAR    : ", decimal.Parse(lbltotal.Text));

            if (cbtipofactura.Text.ToLower() == "credito")
            {
                ticket.AgregarTotales("RESTANTE : ", decimal.Parse(restante.ToString()));
            }

            ticket.TextoIzquierda(" ");
            ticket.TextoCentro("__________________________________");

            //TEXTO FINAL DEL TICKET
            ticket.TextoIzquierda("EXTRA");
            ticket.TextoIzquierda("FAVOR REVISE SU MERCANCIA AL RECIBIRLA");
            //TEXTO FINAL DEL TICKET
            ticket.TextoIzquierda(" ");
            ticket.TextoCentro("EXTRA");
            ticket.TextoIzquierda("FAVOR REVISE SU MERCANCIA AL RECIBIRLA");
            ticket.TextoIzquierda(" ");
            ticket.TextoIzquierda("Los equipos tienen 30 dias de garantia, la misma aplica con la factura y el equipo encendido");
            ticket.TextoIzquierda(" ");
            ticket.TextoIzquierda("No hacemos devolucion de efectivo");
            ticket.TextoIzquierda(" ");
            ticket.TextoIzquierda("No cubrimos garantia de pantalla, touch, pin de carga o equipos mojados");
            ticket.TextoIzquierda(" ");
            ticket.TextoIzquierda("El Tiempo de espera en caso de inconvenientes es de 24 a 48 horas dias laborales");
            ticket.TextoIzquierda(" ");
            ticket.TextoCentro("!GRACIAS POR SU COMPRA!");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("");
            ticket.CortaTicket();//CORTAR TICKET
            ticket.ImprimirTicket("POS80 Printer");//NOMBRE DE LA IMPRESORA
        }

        private void txtPVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Program.abiertosecundario == false)
            {
                frmListadoVentas F = new frmListadoVentas();
                F.btnCancelar.Visible = false;
                Program.abiertosecundario = false;
                Program.abierto = false;
                F.Show();
            }
        }

        public void buscarid()
        {
            Cx.conexion.Close();
            Cx.conexion.Open();
            string sql = "SELECT id_ncf FROM ncf WHERE descripcion_ncf =@id";
            SqlCommand cmd = new SqlCommand(sql, Cx.conexion);
            cmd.Parameters.AddWithValue("@id", combo_tipo_NCF.Text);

            SqlDataReader reade = cmd.ExecuteReader();
            if (reade.Read())
            {
                txtid.Text = Convert.ToString(reade["id_ncf"]);
            }

            Cx.conexion.Close();
            actualzarestadoscomprobantes();
        }

        private void comboselectNCF(int id_ncf)
        {
            int secuencia = 0;
            try
            {
                SqlDataReader LectorSecuencia;

                Cx.conexion.Open();
                SqlCommand Comando = new SqlCommand();
                Comando.Connection = Cx.conexion;
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

                LectorSecuencia.Close();
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return;
            }
            finally
            {
                Cx.conexion.Close();
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
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Program.abiertosecundario = false;
            Program.abierto = false;
            btnImprimir.Visible = false;
            btnAgregar.Visible = false;
            Program.pagoRealizado = 0;
            Limpiar();
            Limpiar1();

            if (Program.CargoEmpleadoLogueado != "Administrador")
            {
                txtPVenta.Enabled = false;
                txtIgv.Enabled = false;
            }

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
            Image image1 = Image.GetInstance("Logo.png");
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
                    string envio = "Fecha : " +dateTimePicker1.Value.Day + "/" + dateTimePicker1.Value.Month + "/" + dateTimePicker1.Value.Year;

                    Chunk chunk = new Chunk(remito, FontFactory.GetFont("ARIAL", 16, iTextSharp.text.Font.BOLD, color: BaseColor.BLUE));
                    if (Program.ReImpresion != null)
                    {
                        doc.Add(new Paragraph("                                                                                                                                                                                                                                                                                                                                                                                            " + Program.ReImpresion, FontFactory.GetFont("ARIAL", 5, iTextSharp.text.Font.ITALIC, color: BaseColor.RED)));
                    }

                    if (Program.datoscliente != "" && Program.IdCliente == 0)
                    {
                        nombre = Program.datoscliente;
                        cedula = "Sin Identificación";
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
                    doc.Add(new Paragraph("Tipo de Factura: " + cbtipofactura.Text.ToUpper(), FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("Tipo de Comprobante: " + combo_tipo_NCF.Text, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("Numero de Comprobante: " + txtNCF.Text, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("Cliente: " + nombre, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    doc.Add(new Paragraph("Documento de Identificación: " + cedula, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    if (txtImei.Text != null || txtImei.Text != "")
                    {
                        doc.Add(new Paragraph("IMEI del Producto: " + txtImei.Text, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));
                    }
                    doc.Add(new Paragraph(" "));
                    GenerarDocumento(doc);
                    doc.AddCreationDate();
                    if (dgvVenta.Rows.Count >= 1)
                    {
                        int filas = 17 - dgvVenta.Rows.Count;
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
                    decimal total = Convert.ToDecimal(lbltotal.Text);

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
                    doc.Add(new Paragraph("FAVOR REVISE SU MERCANCIA AL RECIBIRLA", FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.ITALIC, color: BaseColor.RED)));
                    doc.Add(new Paragraph("Los equipos tienen 30 dias de garantia, la misma aplica con la factura y el equipo encendido", FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.ITALIC, color: BaseColor.RED)));
                    doc.Add(new Paragraph("No hacemos devolucion de efectivo", FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.ITALIC, color: BaseColor.RED)));
                    doc.Add(new Paragraph("No cubrimos garantía de pantalla, touch, pin de carga o equipos mojados", FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.ITALIC, color: BaseColor.RED)));
                    doc.Add(new Paragraph("El Tiempo de espera en caso de inconvenientes es de 24 a 48 horas dias laborales", FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.ITALIC, color: BaseColor.RED)));
                    doc.Add(new Paragraph("!GRACIAS POR SU COMPRA!", FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.ITALIC, color: BaseColor.RED)));
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
            restante = Convert.ToDecimal(lbltotal.Text) - Program.pagoRealizado;
            using (SqlConnection con = new SqlConnection(Cx.conet))
            {
                using (SqlCommand cmd = new SqlCommand("AbonaraVenta", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    //tabla Ventas
                    cmd.Parameters.Add("@IdVenta", SqlDbType.Int).Value = Program.Id;
                    cmd.Parameters.Add("@Restante", SqlDbType.Decimal).Value = restante;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                using (SqlCommand cmd2 = new SqlCommand("Actualizarpagos_re", con))
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

                    con.Open();
                    cmd2.ExecuteNonQuery();
                    con.Close();
                }
                Program.pagoRealizado = 0;
                MessageBox.Show("Abono Registrado y Pago Confirmado");

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

        bool tienefila = false;
        string idAnterior = "";
        public void llenar_data(string id)
        {
            if ((id != "") && (idAnterior !=id))
            {
                tienefila = false;
                idAnterior = id;
                //declaramos la cadena  de conexion
                string cadenaconexion = Cx.conet;
                //variable de tipo Sqlconnection
                SqlConnection con = new SqlConnection();
                //variable de tipo Sqlcommand
                SqlCommand comando = new SqlCommand();
                //variable SqlDataReader para leer los datos
                SqlDataReader dr;
                con.ConnectionString = cadenaconexion;
                comando.Connection = con;
                //declaramos el comando para realizar la busqueda
                comando.CommandText = "select * from ImeiList where IdProducto =" + id + "and activo=" + 1;
                //especificamos que es de tipo Text
                comando.CommandType = CommandType.Text;
                //se abre la conexion
                con.Open();
                //limpiamos los renglones de la datagridview
                //a la variable DataReader asignamos  el la variable de tipo SqlCommand
                dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    tienefila = true;
                }

                Program.abiertoimei = tienefila;
                con.Close();
            }
        }
    }
}

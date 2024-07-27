using CapaEnlaceDatos;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class cuadredecaja : Form
    {
        public cuadredecaja()
        {
            InitializeComponent();
        }

        clsManejador M = new clsManejador();
        public void limpiar()
        {
            txtde5.Text = "0";
            txtde10.Text = "0";
            txtde25.Text = "0";
            txtde50.Text = "0";
            txtde100.Text = "0";
            txtde200.Text = "0";
            txtde500.Text = "0";
            txtde1000.Text = "0";
            txtde2000.Text = "0";
            txtTarjeta.Text = "0";
            txtTransferencia.Text = "0";
            txtCheques.Text = "0";

            txtde5.ReadOnly = false;
            txtde10.ReadOnly = false;
            txtde25.ReadOnly = false;
            txtde50.ReadOnly = false;
            txtde100.ReadOnly = false;
            txtde200.ReadOnly = false;
            txtde500.ReadOnly = false;
            txtde1000.ReadOnly = false;
            txtde2000.ReadOnly = false;
            txtTarjeta.ReadOnly = false;
            txtTransferencia.ReadOnly = false;
            txtCheques.ReadOnly = false;

            lbldeudas.Text = "...";
            lblmontocuadre.Text = "...";
            lblmontocaja.Text = "...";
            lblmontoingreso.Text = "...";
            lblmontogasto.Text = "...";
            lblEfectivo.Text = "...";

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
        }

        private void btnregistrar_Click(object sender, EventArgs e)
        {
            M.Desconectar();
            if (lblmontocuadre.Text != "..." && !string.IsNullOrWhiteSpace(lblmontocuadre.Text))
            {
                decimal montofinal = Program.GetTwoNumberAfterPointWithOutRound(lblmontocuadre.Text);
                if (montofinal > 0)
                {
                    using (SqlCommand cmd = new SqlCommand("Registrarcuadre", M.conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        //tabla cuadre
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = Program.idcaja;
                        cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar).Value = txtde5.Text + "," + txtde10.Text + "," + txtde25.Text + ","
                            + txtde50.Text + "," + txtde100.Text + "," + txtde200.Text + "," + txtde500.Text + "," + txtde1000.Text + "," + txtde2000.Text + ","
                            + txtTarjeta.Text + "," + txtTransferencia.Text + "," + txtCheques.Text;
                        cmd.Parameters.Add("@monto", SqlDbType.Decimal).Value = montofinal;
                        cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = DateTime.Today;

                        M.Conectar();
                        cmd.ExecuteNonQuery();
                        M.Desconectar();

                        To_pdf();
                        limpiar();
                        label18.Enabled = true;
                        MessageBox.Show("Cuadre Registrado");
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe darle al boton de Sumar antes de registrar un nuevo Cuadre");
            }
        }

        public void llenardeudas(int id)
        {
            M.Desconectar();
            string cadSql = "select deuda from Caja where id_caja =" + id;
            M.Conectar();

            SqlCommand comando = new SqlCommand(cadSql, M.conexion);
            SqlDataReader leer = comando.ExecuteReader();
            if (leer.Read())
            {
                var deuda = !leer.IsDBNull(leer.GetOrdinal("deuda")) ? Program.GetTwoNumberAfterPointWithOutRound(leer["deuda"].ToString()) : 0;
                lbldeudas.Text = deuda.ToString();
            }
            M.Desconectar();
        }

        private void agregargasto_Click(object sender, EventArgs e)
        {
            M.Desconectar();
            limpiar();
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            comando.Connection = M.conexion;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "Select * From Cuadre where fecha = convert(datetime,CONVERT(varchar(10), @fecha, 103),103)";
            comando.Parameters.AddWithValue("@fecha", dpkfechacuadre.Value);
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            M.Conectar();
            //limpiamos los renglones de la datagridview
            dataGridView1.Rows.Clear();
            //a la variable DataReader asignamos  el la variable de tipo SqlCommand
            dr = comando.ExecuteReader();
            if (dr.HasRows == false)
            {
                limpiar();
                MessageBox.Show("No tiene ningun cuadre registrado en esta Fecha");
            }
            else
            {
                //el ciclo while se ejecutará mientras lea registros en la tabla
                while (!dr.IsClosed && dr.Read())
                {
                    //variable de tipo entero para ir enumerando los la filas del datagridview
                    int renglon = dataGridView1.Rows.Add();
                    // especificamos en que fila se mostrará cada registro
                    // nombredeldatagrid.filas[numerodefila].celdas[nombrdelacelda].valor=\
                    if (dr.GetInt32(dr.GetOrdinal("id")) > 0)
                    {
                        dataGridView1.Rows[renglon].Cells[0].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id")));
                        dataGridView1.Rows[renglon].Cells[1].Value = dr.GetString(dr.GetOrdinal("descripcion"));
                        dataGridView1.Rows[renglon].Cells[2].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("monto")));
                        dataGridView1.Rows[renglon].Cells[3].Value = dr.GetDateTime(dr.GetOrdinal("fecha"));

                        llenardeudas(Convert.ToInt32(dataGridView1.Rows[renglon].Cells[0].Value));
                        llenargastos();
                        llenargridpagos(Convert.ToInt32(dataGridView1.Rows[renglon].Cells[0].Value));
                        llenar(Convert.ToInt32(dataGridView1.Rows[renglon].Cells[0].Value));

                        string desglose = dataGridView1.Rows[renglon].Cells[1].Value.ToString();
                        if (desglose != "")
                        {
                            var marca = desglose;
                            string cadena = marca;
                            char delimitador = ',';
                            string[] valores = cadena.Split(delimitador);

                            txtde5.Text = valores[0];
                            txtde10.Text = valores[1];
                            txtde25.Text = valores[2];
                            txtde50.Text = valores[3];
                            txtde100.Text = valores[4];
                            txtde200.Text = valores[5];
                            txtde500.Text = valores[6];
                            txtde1000.Text = valores[7];
                            txtde2000.Text = valores[8];
                            if (valores.Length > 8)
                            {
                                txtTarjeta.Text = valores[9];
                                txtTransferencia.Text = valores[10];
                                txtCheques.Text = valores[11];
                            }

                            lblmensaje.Text = string.Empty;
                        }

                        txtde5.ReadOnly = true;
                        txtde10.ReadOnly = true;
                        txtde25.ReadOnly = true;
                        txtde50.ReadOnly = true;
                        txtde100.ReadOnly = true;
                        txtde200.ReadOnly = true;
                        txtde500.ReadOnly = true;
                        txtde1000.ReadOnly = true;
                        txtde2000.ReadOnly = true;
                        txtTarjeta.ReadOnly = true;
                        txtTransferencia.ReadOnly = true;
                        txtCheques.ReadOnly = true;

                        lblmontocuadre.Text = dataGridView1.Rows[renglon].Cells[2].Value.ToString();

                        btnregistrar.Visible = false;
                        btnimprimir.Visible = true;
                        btnsuma.Visible = false;
                    }
                    else
                    {
                        limpiar();
                        MessageBox.Show("No se encontro ningun registrado de cuadre en esta Fecha");
                    }
                }
            }

            M.Desconectar();
        }

        public void llenargridpagos(int id)
        {
            M.Desconectar();
            decimal pagos = 0, devuelta = 0;
            //variable de tipo Sqlcommand
            SqlCommand comando = new SqlCommand();
            //variable SqlDataReader para leer los datos
            SqlDataReader dr;
            comando.Connection = M.conexion;
            //declaramos el comando para realizar la busqueda
            comando.CommandText = "select id_caja,id_pago,monto,ingresos,egresos From Pagos WHERE id_caja =" + id;
            //especificamos que es de tipo Text
            comando.CommandType = CommandType.Text;
            //se abre la conexion
            M.Conectar();
            //limpiamos los renglones de la datagridview
            dataGridView2.Rows.Clear();
            //a la variable DataReader asignamos  el la variable de tipo SqlCommand
            dr = comando.ExecuteReader();
            while (dr.Read())
            {
                //variable de tipo entero para ir enumerando los la filas del datagridview
                int renglon = dataGridView2.Rows.Add();
                // especificamos en que fila se mostrará cada registro
                // nombredeldatagrid.filas[numerodefila].celdas[nombrdelacelda].valor=\
                //pagos

                if (dr.GetInt32(dr.GetOrdinal("id_caja")) > 0)
                {
                    dataGridView2.Rows[renglon].Cells["id_caja"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id_caja")));
                    dataGridView2.Rows[renglon].Cells["id_pago"].Value = Convert.ToString(dr.GetInt32(dr.GetOrdinal("id_pago")));
                    dataGridView2.Rows[renglon].Cells["montoventa"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("monto")));
                    dataGridView2.Rows[renglon].Cells["ingresos"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("ingresos")));
                    dataGridView2.Rows[renglon].Cells["egresos"].Value = Convert.ToString(dr.GetDecimal(dr.GetOrdinal("egresos")));
                    if (Convert.ToInt32(dataGridView2.Rows[renglon].Cells["id_caja"].Value) == id)
                    {
                        pagos += Program.GetTwoNumberAfterPointWithOutRound(dataGridView2.Rows[renglon].Cells["ingresos"].Value.ToString());
                        devuelta += Program.GetTwoNumberAfterPointWithOutRound(dataGridView2.Rows[renglon].Cells["egresos"].Value.ToString());
                    }
                }
            }

            if (pagos > 0)
                lblmontoingreso.Text = Program.GetTwoNumberAfterPointWithOutRound(pagos.ToString()).ToString();

            M.Desconectar();
        }

        public void llenar(int id)
        {
            llenarTiposDePago();

            M.Desconectar();
            string cadSql = "select montoactual, monto_inicial from Caja where id_caja=" + id;

            M.Conectar();
            SqlCommand comando = new SqlCommand(cadSql, M.conexion);
            SqlDataReader leer = comando.ExecuteReader();
            if (leer.Read())
            {
                decimal montogasto = 0;
                decimal montoactual = !leer.IsDBNull(leer.GetOrdinal("montoactual")) ? Program.GetTwoNumberAfterPointWithOutRound(leer["montoactual"].ToString()) : 0;
                decimal montoinicial = !leer.IsDBNull(leer.GetOrdinal("monto_inicial")) ? Program.GetTwoNumberAfterPointWithOutRound(leer["monto_inicial"].ToString()) : 0;
                lblmontoinicial.Text = montoinicial.ToString();

                if (lblmontogasto.Text != "...")
                {
                    montogasto = Program.GetTwoNumberAfterPointWithOutRound(lblmontogasto.Text);
                }

                var monto = (montoactual - montogasto) + montoinicial;
                lblmontocaja.Text = Program.GetTwoNumberAfterPointWithOutRound(monto.ToString()).ToString();
            }

            M.Desconectar();
        }

        public void llenargastos()
        {
            M.Desconectar();
            decimal totalgasto = 0;
            string cadSql = "select * from Gastos where fecha = convert(datetime,CONVERT(varchar(10), @fecha, 103),103)";
            M.Conectar();
            SqlCommand comando = new SqlCommand(cadSql, M.conexion);
            comando.Parameters.AddWithValue("@fecha", dpkfechacuadre.Value);
            SqlDataReader leer = comando.ExecuteReader();
            while (leer.Read())
            {
                var monto = !leer.IsDBNull(leer.GetOrdinal("monto")) ? Program.GetTwoNumberAfterPointWithOutRound(leer["monto"].ToString()) : 0;
                totalgasto += monto;
            }

            lblmontogasto.Text = Program.GetTwoNumberAfterPointWithOutRound(totalgasto.ToString()).ToString();
            M.Desconectar();
        }

        private void btnimprimir_Click(object sender, EventArgs e)
        {
            To_pdf();
            limpiar();
        }

        private void To_pdf()
        {
            Document doc = new Document(PageSize.LETTER, 10f, 10f, 10f, 0f);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            Image image1 = Image.GetInstance("LogoCepeda.png");
            image1.ScaleAbsoluteWidth(140);
            image1.ScaleAbsoluteHeight(70);
            saveFileDialog1.InitialDirectory = @"C:";
            saveFileDialog1.Title = "Guardar Reporte";
            saveFileDialog1.DefaultExt = "pdf";
            saveFileDialog1.Filter = "pdf Files (*.pdf)|*.pdf| All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            string filename = "Reporte de Cuadre de Caja";
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
                    string envio = "Fecha : " + +DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year;

                    Chunk chunk = new Chunk(remito, FontFactory.GetFont("ARIAL", 16, iTextSharp.text.Font.BOLD, color: BaseColor.BLUE));
                    var fecha = new Paragraph(envio, FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLDITALIC));
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

                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("Reporte de Cuadre de Caja"));
                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("Total de Modenas de 5       : " + txtde5.Text));
                    doc.Add(new Paragraph("Total de Modenas de 10     : " + txtde10.Text));
                    doc.Add(new Paragraph("Total de Modenas de 25     : " + txtde25.Text));
                    doc.Add(new Paragraph("Total de Billetes de 50        : " + txtde50.Text));
                    doc.Add(new Paragraph("Total de Billetes de 100      : " + txtde100.Text));
                    doc.Add(new Paragraph("Total de Billetes de 200      : " + txtde200.Text));
                    doc.Add(new Paragraph("Total de Billetes de 500      : " + txtde500.Text));
                    doc.Add(new Paragraph("Total de Billetes de 1000    : " + txtde1000.Text));
                    doc.Add(new Paragraph("Total de Billetes de 2000    : " + txtde2000.Text));
                    doc.Add(new Paragraph("Monto de Pagos por Tarjeta    : " + txtTarjeta.Text));
                    doc.Add(new Paragraph("Monto de Pagos por Transferencias    : " + txtTransferencia.Text));
                    doc.Add(new Paragraph("Monto de Pagos en Cheques    : " + txtCheques.Text));

                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph("Totales de Ingresos  : " + lblmontoingreso.Text));
                    doc.Add(new Paragraph("Totales de Gastos    : " + lblmontogasto.Text));
                    doc.Add(new Paragraph("Totales Final             : " + lblmontocuadre.Text));
                    doc.Add(new Paragraph("Totales De Deudas del dia : " + lbldeudas.Text));
                    doc.Add(new Paragraph("                       "));
                    doc.Add(new Paragraph(lblmensaje.Text, FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL)));
                    doc.AddCreationDate();
                    doc.Close();
                    Process.Start(filename);//Esta parte se puede omitir, si solo se desea guardar el archivo, y que este no se ejecute al instante
                }
            }
            else
            {
                MessageBox.Show("No guardo el Archivo");
            }
        }

        public void llenardeuda()
        {
            M.Desconectar();
            string cadSql = "select deuda from Caja where id_caja=" + Program.idcaja;

            M.Conectar();
            SqlCommand comando = new SqlCommand(cadSql, M.conexion);

            SqlDataReader leer = comando.ExecuteReader();

            if (leer.Read())
            {
                lbldeudas.Text = !leer.IsDBNull(leer.GetOrdinal("deuda")) ? Program.GetTwoNumberAfterPointWithOutRound(leer["deuda"].ToString()).ToString() : "0";
            }
            M.Desconectar();
        }

        private void cuadredecaja_Load(object sender, EventArgs e)
        {
            label18.Enabled = false;
            btnregistrar.Enabled = false;
            btnsuma.Visible = true;

            llenardeuda();
            llenargastos();

            if (Program.idcaja > 0)
            {
                llenargridpagos(Program.idcaja);
                llenar(Program.idcaja);
            }

            btnimprimir.Visible = false;
        }

        private void label18_Click(object sender, EventArgs e)
        {
            M.Desconectar();
            Program.abiertosecundarias = false;
            Program.abierto = false;

            var dbName = M.conexion.Database;
            var dirs = new DirectoryInfo(@"" + Program.SqlFolder).FullName;
            string fileName = "_" + dbName + "_" + DateTime.Now.ToShortDateString().Replace("/", "-") + ".bak";
            ReintentBackup(dbName, dirs, fileName);
        }

        private void ReintentBackup(string dbName, string dirs, string fileName)
        {
            bool continueLoop = true;

            while (continueLoop)
            {
                try
                {
                    var (save, message) = MakeBackup(dirs, M.conexion.ConnectionString, dbName, fileName);
                    if (save)
                    {
                        var destination = @"" + Program.WindUser + "\\" + fileName;
                        if (File.Exists(destination))
                        {
                            File.Delete(destination);
                        }

                        File.Move(dirs + "\\" + fileName, destination);
                        MessageBox.Show("Copia de seguridad de base de datos realizado y guardado");
                        continueLoop = false;
                        Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show("Error al realizar la Copia de seguridad de base de datos");
                        DialogResult result = MessageBox.Show("¿Desea intentar nuevamente guardar la copia de seguridad?", "Sistema de Ventas", MessageBoxButtons.YesNo);

                        if (result == DialogResult.No)
                        {
                            continueLoop = false;
                            // Exit the application if the user chooses not to continue
                            Application.Exit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    DialogResult result = MessageBox.Show("¿Desea intentar nuevamente guardar la copia de seguridad?", "Sistema de Ventas", MessageBoxButtons.YesNo);

                    if (result == DialogResult.No)
                    {
                        continueLoop = false;
                        // Exit the application if the user chooses not to continue
                        Application.Exit();
                    }
                }
            }
        }

        public (bool success, string message) MakeBackup(string ubicacion, string strConnection, string dbName, string nombre)
        {
            var con = new SqlConnection(strConnection);
            var cmd = new SqlCommand("BACKUP DATABASE " + dbName + " TO DISK='" + ubicacion + "/" + nombre + "'", con);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return (true, "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                con.Close();
                return (false, ex.Message.ToString());
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            FrmMenuPrincipal menu = new FrmMenuPrincipal();
            menu.Show();
            this.Close();
        }

        private void txtde5_KeyPress(object sender, KeyPressEventArgs e)
        {
            validar.solonumeros(e);
        }

        private void btnsuma_Click(object sender, EventArgs e)
        {
            decimal inicial = 0;
            decimal ingresos = 0;
            decimal gastos = 0;

            if (lblmontogasto.Text != "...")
            {
                gastos = Program.GetTwoNumberAfterPointWithOutRound(lblmontogasto.Text);
            }

            if (lblmontoingreso.Text != "...")
            {
                ingresos = Program.GetTwoNumberAfterPointWithOutRound(lblmontoingreso.Text);
            }

            if (lblmontoinicial.Text != "...")
            {
                inicial = Program.GetTwoNumberAfterPointWithOutRound(lblmontoinicial.Text);
            }

            decimal cuadre = (ingresos + inicial) - gastos;

            if (string.IsNullOrWhiteSpace(lbldeudas.Text))
            {
                txtde2000.Text = "0";
            }

            if (string.IsNullOrWhiteSpace(txtde5.Text))
            {
                txtde5.Text = "0";
            }

            if (string.IsNullOrWhiteSpace(txtde10.Text))
            {
                txtde10.Text = "0";
            }

            if (string.IsNullOrWhiteSpace(txtde25.Text))
            {
                txtde25.Text = "0";
            }

            if (string.IsNullOrWhiteSpace(txtde50.Text))
            {
                txtde50.Text = "0";
            }

            if (string.IsNullOrWhiteSpace(txtde100.Text))
            {
                txtde100.Text = "0";
            }

            if (string.IsNullOrWhiteSpace(txtde200.Text))
            {
                txtde200.Text = "0";
            }

            if (string.IsNullOrWhiteSpace(txtde500.Text))
            {
                txtde500.Text = "0";
            }

            if (string.IsNullOrWhiteSpace(txtde1000.Text))
            {
                txtde1000.Text = "0";
            }

            if (string.IsNullOrWhiteSpace(txtde2000.Text))
            {
                txtde2000.Text = "0";
            }
            if (string.IsNullOrWhiteSpace(txtTarjeta.Text))
            {
                txtTarjeta.Text = "0";
            }

            if (string.IsNullOrWhiteSpace(txtTransferencia.Text))
            {
                txtTransferencia.Text = "0";
            }

            if (string.IsNullOrWhiteSpace(txtCheques.Text))
            {
                txtCheques.Text = "0";
            }

            decimal total = Math.Round((5 * Program.GetTwoNumberAfterPointWithOutRound(txtde5.Text)) + (10 * Program.GetTwoNumberAfterPointWithOutRound(txtde10.Text)) + (25 * Program.GetTwoNumberAfterPointWithOutRound(txtde25.Text)) +
                                       (50 * Program.GetTwoNumberAfterPointWithOutRound(txtde50.Text)) + (100 * Program.GetTwoNumberAfterPointWithOutRound(txtde100.Text)) + (200 * Program.GetTwoNumberAfterPointWithOutRound(txtde200.Text)) +
                                       (500 * Program.GetTwoNumberAfterPointWithOutRound(txtde500.Text)) + (1000 * Program.GetTwoNumberAfterPointWithOutRound(txtde1000.Text)) + (2000 * Program.GetTwoNumberAfterPointWithOutRound(txtde2000.Text))
                                       + Program.GetTwoNumberAfterPointWithOutRound(txtTarjeta.Text) + Program.GetTwoNumberAfterPointWithOutRound(txtTransferencia.Text) + Program.GetTwoNumberAfterPointWithOutRound(txtCheques.Text));

            if (cuadre < total)
            {
                var sobrantes = total - cuadre;
                lblmensaje.Text = "Cuadre excitoso Sobran : \n" + sobrantes + " Pesos";
                lblmensaje.ForeColor = System.Drawing.Color.White;
                btnregistrar.Enabled = true;
            }
            else if (cuadre == total)
            {
                lblmensaje.Text = "Cuadre exacto";
                lblmensaje.ForeColor = System.Drawing.Color.MidnightBlue;
                btnregistrar.Enabled = true;
            }
            else
            {
                var faltantes = cuadre - total;
                lblmensaje.Text = "Cuadre defectuoso, Faltan : \n" + faltantes + " Pesos";
                lblmensaje.ForeColor = System.Drawing.Color.Yellow;
                btnregistrar.Enabled = false;
            }

            lblmontocuadre.Text = total.ToString();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void cuadredecaja_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void llenarTiposDePago()
        {
            M.Desconectar();
            string cadSql = @"SELECT
                                 SUM(CASE WHEN TipoPago IS NULL OR TipoPago = '' OR TipoPago = 'Efectivo' THEN Total ELSE 0 END) AS TotalEfectivo,
                                 SUM(CASE WHEN TipoPago = 'Tarjeta' THEN Total ELSE 0 END) AS TotalTarjeta,
                                 SUM(CASE WHEN TipoPago = 'Transferencia' THEN Total ELSE 0 END) AS TotalTransferencia,
                                 SUM(CASE WHEN TipoPago = 'Cheque' THEN Total ELSE 0 END) AS TotalCheque
                             FROM
                                 [dbo].[Venta]
                             WHERE
                                 FechaVenta = CONVERT(date, CONVERT(varchar(10), @fecha, 103), 103)";
            M.Conectar();
            SqlCommand comando = new SqlCommand(cadSql, M.conexion);
            comando.Parameters.AddWithValue("@fecha", dpkfechacuadre.Value);
            SqlDataReader leer = comando.ExecuteReader();
            if (leer.Read())
            {
                var TotalEfectivo = !leer.IsDBNull(leer.GetOrdinal("TotalEfectivo")) ? Program.GetTwoNumberAfterPointWithOutRound(leer["TotalEfectivo"].ToString()) : 0;
                var TotalTarjeta = !leer.IsDBNull(leer.GetOrdinal("TotalTarjeta")) ? Program.GetTwoNumberAfterPointWithOutRound(leer["TotalTarjeta"].ToString()) : 0;
                var TotalTransferencia = !leer.IsDBNull(leer.GetOrdinal("TotalTransferencia")) ? Program.GetTwoNumberAfterPointWithOutRound(leer["TotalTransferencia"].ToString()) : 0;
                var TotalCheque = !leer.IsDBNull(leer.GetOrdinal("TotalCheque")) ? Program.GetTwoNumberAfterPointWithOutRound(leer["TotalCheque"].ToString()) : 0;

                lblEfectivo.Text = TotalEfectivo.ToString();
                txtTarjeta.Text = TotalTarjeta.ToString();
                txtTransferencia.Text = TotalTransferencia.ToString();
                txtCheques.Text = TotalCheque.ToString();
            }
            M.Desconectar();
        }
    }
}

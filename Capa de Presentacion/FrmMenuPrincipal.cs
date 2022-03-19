using CapaEnlaceDatos;
using CapaLogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    public partial class FrmMenuPrincipal : DevComponents.DotNetBar.Metro.MetroForm
    {
        public FrmMenuPrincipal()
        {
            InitializeComponent();
        }

        //Correo c = new Correo();
        clsManejador Cx = new clsManejador();
        clsUsuarios U = new clsUsuarios();
        private void FrmMenuPrincipal_Activated(object sender, EventArgs e)
        {
            lblUsuario.Text = Program.NombreEmpleadoLogueado;
            textBox1.Text = Program.CargoEmpleadoLogueado1;

            if (Program.LoginStatus == "Inventario")
            {
                btnProductos.Visible = true;
                btnClientes.Visible = false;
                btnVentas.Visible = false;

                button5.Visible = false;
                button2.Visible = false;
                btnUsuarios.Visible = false;
                btnEmpleados.Visible = false;

                button6.Visible = false;
                button1.Visible = false;
                button3.Visible = false;
                btnVer.Visible = false;
            }
            else if (Program.LoginStatus == "Ventas")
            {
                btnProductos.Visible = false;
                btnClientes.Visible = false;
                btnVentas.Visible = false;

                button5.Visible = true;
                button2.Visible = false;
                btnUsuarios.Visible = false;
                btnEmpleados.Visible = false;

                button6.Visible = true;
                button1.Visible = false;
                button3.Visible = false;
                btnVer.Visible = false;
            }
            else if (Program.LoginStatus == "NCF")
            {
                btnProductos.Visible = false;
                btnClientes.Visible = false;
                btnVentas.Visible = false;

                button5.Visible = false;
                button2.Visible = true;
                btnUsuarios.Visible = false;
                btnEmpleados.Visible = false;

                button6.Visible = false;
                button1.Visible = false;
                button3.Visible = false;
                btnVer.Visible = false;
            }
            else
            {
                if (!Program.isAdminUser)
                {
                    btnProductos.Visible = true;
                    btnClientes.Visible = true;
                    btnVentas.Visible = true;

                    button5.Visible = false;
                    button2.Visible = false;
                    btnUsuarios.Visible = false;
                    btnEmpleados.Visible = false;

                    button1.Visible = true;
                    button3.Visible = true;
                    btnVer.Visible = true;
                }
                else
                {
                    btnProductos.Visible = true;
                    btnClientes.Visible = true;
                    btnVentas.Visible = true;

                    button5.Visible = true;
                    button2.Visible = true;
                    btnUsuarios.Visible = true;
                    btnEmpleados.Visible = true;

                    button1.Visible = true;
                    button3.Visible = true;
                    btnVer.Visible = false;
                }
            }

            CapturarFechaSistema();

            if (TempData.DateIn.AddHours(1) <= DateTime.Now)
                TempData.tempSalesData = new List<Venta>();

            #region productos vendidos por categoria
            if (clsGenericList.listVentas.Count > 0)
            {
                var fecha1 = !TempData.tempSalesData.Any() ? clsGenericList.listVentas.FirstOrDefault().FechaVenta : TempData.tempSalesData.FirstOrDefault().FechaVenta;
                var fecha2 = !TempData.tempSalesData.Any() ? clsGenericList.listVentas.LastOrDefault().FechaVenta : TempData.tempSalesData.LastOrDefault().FechaVenta;
                clsGenericList.listVentasPorCategoria = clsGenericList.ListaPorCatergoria(fecha1, fecha2, 0);
            }
            #endregion
        }
        private void FrmMenuPrincipal_Load(object sender, EventArgs e)
        {
            timer1.Interval = 500;
            timer1.Start();
            llenar();
        }

        public void llenar()
        {
            Cx.Desconectar();
            string cadSql = "select * from NomEmp";
            SqlCommand comando = new SqlCommand(cadSql, Cx.conexion);
            Cx.Conectar();
            SqlDataReader leer = comando.ExecuteReader();
            if (leer.Read() == true)
            {
                lblDir.Text = leer["DirEmp"].ToString();
                lblLogo.Text = leer["NombreEmp"].ToString();
                lblTel1.Text = leer["Tel1"].ToString();
                lblTel2.Text = leer["Tel2"].ToString();
                lblCorreo.Text = leer["Correo"].ToString();
                lblrnc.Text = leer["RNC"].ToString();
            }
            Cx.Desconectar();
        }
        private void btnProductos_Click(object sender, EventArgs e)
        {
            panel1.Size = new System.Drawing.Size(64, 517);
            button7.Text = ">>";
            if (Program.abierto == false)
            {
                if (Program.LoginStatus == "Inventario")
                {
                    FrmListadoProductos P = new FrmListadoProductos();
                    P.lblLogo.Text = lblLogo.Text;
                    P.lblDir.Text = lblDir.Text;
                    P.button2.Enabled = false;
                    Program.abierto = true;
                    P.Show();
                }
                else
                {
                    if (txtcargo.Text.Trim() != "Administrador")
                    {
                        FrmListadoProductos P = new FrmListadoProductos();
                        P.lblLogo.Text = lblLogo.Text;
                        P.lblDir.Text = lblDir.Text;
                        P.btnNuevo.Enabled = false;
                        P.btnEditar.Enabled = false;
                        P.button2.Enabled = false;
                        Program.abierto = true;
                        P.Show();
                    }
                    else
                    {
                        FrmListadoProductos P = new FrmListadoProductos();
                        P.lblLogo.Text = lblLogo.Text;
                        P.lblDir.Text = lblDir.Text;
                        Program.abierto = true;
                        P.Show();
                    }
                }
            }
        }
        private void btnClientes_Click(object sender, EventArgs e)
        {
            panel1.Size = new System.Drawing.Size(64, 517);
            button7.Text = ">>";
            if (Program.abierto == false)
            {
                FrmListadoClientes C = new FrmListadoClientes();
                C.btnActualizar.Enabled = Program.isAdminUser;

                Program.abierto = true;
                C.Show();
            }
        }
        private void btnVentas_Click(object sender, EventArgs e)
        {
            panel1.Size = new System.Drawing.Size(240, 517);
            button7.Text = "<<";

            if (buttonPanel.Visible == false)
                buttonPanel.Visible = true;
            else
                buttonPanel.Visible = false;
        }
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            panel1.Size = new System.Drawing.Size(64, 517);
            button7.Text = ">>";
            if (Program.abierto == false)
            {
                FrmListadoUsuario U = new FrmListadoUsuario();
                Program.abierto = true;
                U.Show();
            }
        }
        private void CapturarFechaSistema()
        {
            lblFecha.Text = DateTime.Now.ToShortDateString();
            lblHora.Text = DateTime.Now.ToShortTimeString();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            panel1.Size = new System.Drawing.Size(64, 517);
            button7.Text = ">>";
            if (Program.abierto == false)
            {
                FrmListadoEmpleados E = new FrmListadoEmpleados();
                E.btnActualizar.Enabled = Program.isAdminUser;

                Program.abierto = true;
                E.Show();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Program.LoginStatus != "" && Program.LoginStatus != null)
            {
                Program.LoginStatus = "";
                FrmLogin Login = new FrmLogin();
                Login.Show();
                this.Hide();
            }
            else
            {
                cuadredecaja cuadre = new cuadredecaja();
                cuadre.lblLogo.Text = lblLogo.Text;
                cuadre.lblDir.Text = lblDir.Text;
                cuadre.lbltel1.Text = lblTel1.Text;
                cuadre.lbltel.Text = lblTel2.Text;
                cuadre.lblCorreo.Text = lblCorreo.Text;
                cuadre.lblrnc.Text = lblrnc.Text;
                cuadre.Show();
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Size = new System.Drawing.Size(64, 517);
            button7.Text = ">>";
            frmTurno Tu = new frmTurno();
            Tu.lblLogo.Text = lblLogo.Text;
            Tu.textBox2.Text = Program.turno + "";
            Program.abiertosecundarias = false;
            Program.abierto = false;
            Tu.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Size = new System.Drawing.Size(64, 517);
            button7.Text = ">>";
            if (Program.abierto == false)
            {
                frmLimitantesNCF limi = new frmLimitantesNCF();
                Program.abierto = true;
                limi.Show();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Size = new System.Drawing.Size(64, 517);
            button7.Text = ">>";
            if (Program.abierto == false)
            {
                frmCambiarUsu Ca = new frmCambiarUsu();
                Program.abierto = true;
                Ca.Show();
            }
        }
        private void ckbLimite_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbLimite.Checked == true)
            {
                button2.Visible = true;
            }
            else
            {
                button2.Visible = false;
            }
        }
        private void ckbUsu_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbUsu.Checked == true)
            {
                btnUsuarios.Visible = true;
            }
            else
            {
                btnUsuarios.Visible = false;
            }
        }
        private void ckbEmp_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEmp.Checked == true)
            {
                btnEmpleados.Visible = true;
            }
            else
            {
                btnEmpleados.Visible = false;
            }
        }
        private void btnVer_Click(object sender, EventArgs e)
        {
            panel1.Size = new System.Drawing.Size(64, 517);
            button7.Text = ">>";
            usuario.Show();
            btnVer.Hide();
        }
        private void label1_Click(object sender, EventArgs e)
        {
            usuario.Hide();
            btnVer.Show();
        }
        public void button4_Click(object sender, EventArgs e)
        {
            U.User = txtUser.Text;
            U.Password = txtClave.Text;
            RecuperarDatosSesion1();
            MessageBox.Show("Buscando Usuario");
            Refresh();
            if (textBox1.Text.Trim() == "Administrador")
            {
                usuario.Hide();
                panel2.Show();
            }
            else
            {
                MessageBox.Show("No eres Administrador");
            }
            txtUser.Clear();
            txtClave.Clear();
        }
        private void RecuperarDatosSesion1()
        {
            DataRow row;
            DataTable dt = new DataTable();
            dt = U.DevolverDatosSesion(txtUser.Text, txtClave.Text);
            if (dt.Rows.Count == 1)
            {
                row = dt.Rows[0];
                Program.IdEmpleadoLogueado1 = Convert.ToInt32(row[0].ToString());
                Program.NombreEmpleadoLogueado1 = row[1].ToString();
                Program.CargoEmpleadoLogueado1 = row[2].ToString();
            }
        }
        private void label7_Click_1(object sender, EventArgs e)
        {
            Program.CargoEmpleadoLogueado1 = "";
            textBox1.Text = "";
            panel2.Hide();
            usuario.Hide();
            btnVer.Show();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Size = new System.Drawing.Size(64, 517);
            button7.Text = ">>";
            if (Program.abierto == false)
            {
                frmListadoVentas vt = new frmListadoVentas();
                vt.lblLogo.Text = lblLogo.Text;
                vt.lblDir.Text = lblDir.Text;
                Program.abierto = true;
                vt.Show();
            }
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                button4.PerformClick();
            }
        }

        bool val = false;
        private void lblLogo_MouseDown(object sender, MouseEventArgs e)
        {
            val = true;
        }

        private void lblLogo_MouseMove(object sender, MouseEventArgs e)
        {
            if (val == true)
            {
                this.Location = Cursor.Position;
            }
        }

        private void lblLogo_MouseUp(object sender, MouseEventArgs e)
        {
            val = false;
        }

        private void ckbback_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbback.Checked == true)
            {
                button5.Visible = true;
            }
            else
            {
                button5.Visible = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Size = new System.Drawing.Size(64, 517);
            button7.Text = ">>";
            if (Program.abierto == false)
            {
                frmMovimientoCaja move = new frmMovimientoCaja();
                Program.abierto = true;
                move.Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (panel1.Size == new System.Drawing.Size(240, 517))
            {
                panel1.Size = new System.Drawing.Size(64, 517);
                button7.Text = ">>";
            }
            else
            {
                panel1.Size = new System.Drawing.Size(240, 517);
                button7.Text = "<<";
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            if (Program.LoginStatus != "" && Program.LoginStatus != null)
            {
                Program.LoginStatus = "";
                FrmLogin Login = new FrmLogin();
                Login.Show();
                this.Hide();
            }
            else
            {
                cuadredecaja cuadre = new cuadredecaja();
                cuadre.lblLogo.Text = lblLogo.Text;
                cuadre.lblDir.Text = lblDir.Text;
                cuadre.lbltel1.Text = lblTel1.Text;
                cuadre.lbltel.Text = lblTel2.Text;
                cuadre.lblCorreo.Text = lblCorreo.Text;
                cuadre.lblrnc.Text = lblrnc.Text;
                cuadre.Show();
                this.Hide();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void lblLogo_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void button8_Click_1(object sender, EventArgs e)
        {
            panel1.Size = new System.Drawing.Size(64, 517);
            button7.Text = ">>";

            Program.isSaler = true;
            panel1.Size = new System.Drawing.Size(64, 517);
            button7.Text = ">>";
            if (Program.abierto == false)
            {
                FrmRegistroVentas V = new FrmRegistroVentas();
                V.txtUsu.Text = lblUsuario.Text;
                V.txtidEmp.Text = Convert.ToString(Program.IdEmpleadoLogueado);
                V.lblLogo.Text = lblLogo.Text;
                V.lblDir.Text = lblDir.Text;
                V.lblTel1.Text = lblTel1.Text;
                V.lblTel2.Text = lblTel2.Text;
                V.lblCorreo.Text = lblCorreo.Text;
                V.lblrnc.Text = lblrnc.Text;
                V.txtIgv.Enabled = Program.isAdminUser;

                V.button2.Visible = Program.isSaler;
                V.btnRegistrarVenta.Visible = Program.isSaler;
                V.btnSalir.Visible = Program.isSaler;
                V.cotizacionPanel.Visible = !Program.isSaler;
                V.BackColor = Color.CadetBlue;

                buttonPanel.Visible = false;
                Program.abierto = true;
                V.Show();
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            panel1.Size = new System.Drawing.Size(64, 517);
            button7.Text = ">>";

            Program.isSaler = false;
            panel1.Size = new System.Drawing.Size(64, 517);
            button7.Text = ">>";
            if (Program.abierto == false)
            {
                FrmRegistroVentas V = new FrmRegistroVentas();
                V.txtUsu.Text = lblUsuario.Text;
                V.txtidEmp.Text = Convert.ToString(Program.IdEmpleadoLogueado);
                V.lblLogo.Text = lblLogo.Text;
                V.lblDir.Text = lblDir.Text;
                V.lblTel1.Text = lblTel1.Text;
                V.lblTel2.Text = lblTel2.Text;
                V.lblCorreo.Text = lblCorreo.Text;
                V.lblrnc.Text = lblrnc.Text;
                V.txtIgv.Enabled = Program.isAdminUser;

                V.button2.Visible = Program.isSaler;
                V.btnImprimir.Visible = !Program.isSaler;
                V.btnImprimir.Location = new Point(33, 768);
                V.btnRegistrarVenta.Visible = !Program.isSaler;
                V.btnRegistrarVenta.Text = "Cotizar";
                V.btnSalir.Visible = false;
                V.cotizacionPanel.Visible = !Program.isSaler;
                V.BackColor = Color.SlateGray;

                buttonPanel.Visible = false;
                Program.abierto = true;
                V.Show();
            }
        }
    }
}


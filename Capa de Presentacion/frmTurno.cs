using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
	public partial class frmTurno : Form
	{
		public frmTurno()
		{
			InitializeComponent();
		}

		private void frmTurno_Load(object sender, EventArgs e)
		{

		}
		Double B;
		private void button1_Click(object sender, EventArgs e)
		{
			B = Program.turno + 1;
			turno1.Text = B + "";

			Program.turno = B;

			textBox2.Text= turno1.Text;
			tickEstilo();
		}

		private void frmTurno_Activated(object sender, EventArgs e)
		{
			if (textBox2.Text == "")
			{
				textBox2.Text = Program.turno + "";
			}
		}

		private void label2_Click(object sender, EventArgs e)
		{
			textBox2.Text = Program.turno + "";
			this.Close();
		}


		public void tickEstilo()
		{
			CrearTiket ticket = new CrearTiket();

			//cabecera del ticket.
			ticket.textoCentro(lblLogo.Text);
			ticket.textoIzquierda("");
			ticket.lineasGuio();

			//SUB CABECERA.
			ticket.textoIzquierda("");
			ticket.textoIzquierda("FECHA: " + DateTime.Now.ToShortDateString());
			ticket.textoIzquierda("HORA: " + DateTime.Now.ToShortTimeString());
			ticket.lineasGuio();

			ticket.EncabezadoVentas();
			ticket.lineasGuio();

			ticket.textoCentro(label1.Text+""+turno1.Text);
			ticket.textoCentro(textBox1.Text);
			ticket.textoCentro("!GRACIAS POR SU PACIENCIA!");
			ticket.CortaTicket();
			ticket.ImprimirTicket("POS-80");//NOMBRE DE LA IMPRESORA
		}

	}
}

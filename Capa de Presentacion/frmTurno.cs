﻿using System;
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

            textBox2.Text = turno1.Text;
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
            Program.abiertosecundario = false;
            Program.abierto = false;
            textBox2.Text = Program.turno + "";
            this.Close();
        }


        public void tickEstilo()
        {
            CrearTiket ticket = new CrearTiket();

            //cabecera del ticket.
            //Image img = Image.FromFile("logo.jpg");
            //ticket.HeaderImage = img;
            ticket.TextoCentro(lblLogo.Text);
            ticket.TextoIzquierda("");
            ticket.lineasGuio();

            //SUB CABECERA.
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("FECHA: " + DateTime.Now.ToShortDateString());
            ticket.TextoIzquierda("HORA: " + DateTime.Now.ToShortTimeString());
            ticket.lineasGuio();

            ticket.lineasGuio();

            ticket.TextoCentro(label1.Text + "" + turno1.Text);
            ticket.TextoCentro(textBox1.Text);
            ticket.TextoCentro("!GRACIAS POR SU PACIENCIA!");
            ticket.CortaTicket();
            ticket.ImprimirTicket(Program.ImpresonaPeq);//NOMBRE DE LA IMPRESORA
        }
    }
}

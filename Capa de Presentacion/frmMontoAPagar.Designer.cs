namespace Capa_de_Presentacion
{
	partial class frmMontoAPagar
    {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

#pragma warning disable CS0115 // 'frmPagar.Dispose(bool)': no se encontró ningún miembro adecuado para invalidar
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
#pragma warning restore CS0115 // 'frmPagar.Dispose(bool)': no se encontró ningún miembro adecuado para invalidar
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.gbPagar = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtmonto = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRestante = new System.Windows.Forms.TextBox();
            this.btnC = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblrnc = new System.Windows.Forms.Label();
            this.lblTel2 = new System.Windows.Forms.Label();
            this.lblTel1 = new System.Windows.Forms.Label();
            this.lblDir = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.gbPagar.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPagar
            // 
            this.gbPagar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gbPagar.Controls.Add(this.label6);
            this.gbPagar.Controls.Add(this.txtmonto);
            this.gbPagar.Controls.Add(this.label5);
            this.gbPagar.Controls.Add(this.txtRestante);
            this.gbPagar.Controls.Add(this.btnC);
            this.gbPagar.Controls.Add(this.button2);
            this.gbPagar.Controls.Add(this.label4);
            this.gbPagar.Controls.Add(this.txtTotal);
            this.gbPagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPagar.ForeColor = System.Drawing.Color.White;
            this.gbPagar.Location = new System.Drawing.Point(13, 13);
            this.gbPagar.Margin = new System.Windows.Forms.Padding(4);
            this.gbPagar.Name = "gbPagar";
            this.gbPagar.Padding = new System.Windows.Forms.Padding(4);
            this.gbPagar.Size = new System.Drawing.Size(343, 223);
            this.gbPagar.TabIndex = 1;
            this.gbPagar.TabStop = false;
            this.gbPagar.Text = "Pago Por Cliente";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(17, 128);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 16);
            this.label6.TabIndex = 22;
            this.label6.Text = "Monto a Pagar :";
            // 
            // txtmonto
            // 
            this.txtmonto.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmonto.ForeColor = System.Drawing.Color.Black;
            this.txtmonto.Location = new System.Drawing.Point(20, 155);
            this.txtmonto.Margin = new System.Windows.Forms.Padding(4);
            this.txtmonto.Multiline = true;
            this.txtmonto.Name = "txtmonto";
            this.txtmonto.Size = new System.Drawing.Size(144, 47);
            this.txtmonto.TabIndex = 21;
            this.txtmonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtmonto.TextChanged += new System.EventHandler(this.txtmonto_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(175, 36);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 16);
            this.label5.TabIndex = 20;
            this.label5.Text = "Monto Restante :";
            // 
            // txtRestante
            // 
            this.txtRestante.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtRestante.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRestante.ForeColor = System.Drawing.Color.White;
            this.txtRestante.Location = new System.Drawing.Point(178, 63);
            this.txtRestante.Margin = new System.Windows.Forms.Padding(4);
            this.txtRestante.Multiline = true;
            this.txtRestante.Name = "txtRestante";
            this.txtRestante.ReadOnly = true;
            this.txtRestante.Size = new System.Drawing.Size(143, 47);
            this.txtRestante.TabIndex = 19;
            this.txtRestante.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnC
            // 
            this.btnC.FlatAppearance.BorderSize = 0;
            this.btnC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnC.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnC.ForeColor = System.Drawing.Color.Red;
            this.btnC.Location = new System.Drawing.Point(316, 0);
            this.btnC.Margin = new System.Windows.Forms.Padding(4);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(29, 35);
            this.btnC.TabIndex = 18;
            this.btnC.Text = "X";
            this.btnC.UseVisualStyleBackColor = true;
            this.btnC.Click += new System.EventHandler(this.btnC_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.SpringGreen;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button2.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Image = global::Capa_de_Presentacion.Properties.Resources.icons8_dinero_241;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(178, 155);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 47);
            this.button2.TabIndex = 9;
            this.button2.Text = "Pagar";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(17, 36);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Monto Total";
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtTotal.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.ForeColor = System.Drawing.Color.White;
            this.txtTotal.Location = new System.Drawing.Point(20, 63);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotal.Multiline = true;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(144, 47);
            this.txtTotal.TabIndex = 5;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblrnc
            // 
            this.lblrnc.AutoSize = true;
            this.lblrnc.Location = new System.Drawing.Point(465, 109);
            this.lblrnc.Name = "lblrnc";
            this.lblrnc.Size = new System.Drawing.Size(36, 16);
            this.lblrnc.TabIndex = 126;
            this.lblrnc.Text = "RNC";
            this.lblrnc.Visible = false;
            // 
            // lblTel2
            // 
            this.lblTel2.AutoSize = true;
            this.lblTel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTel2.Location = new System.Drawing.Point(465, 134);
            this.lblTel2.Name = "lblTel2";
            this.lblTel2.Size = new System.Drawing.Size(28, 16);
            this.lblTel2.TabIndex = 125;
            this.lblTel2.Text = "tel2";
            // 
            // lblTel1
            // 
            this.lblTel1.AutoSize = true;
            this.lblTel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTel1.Location = new System.Drawing.Point(431, 134);
            this.lblTel1.Name = "lblTel1";
            this.lblTel1.Size = new System.Drawing.Size(28, 16);
            this.lblTel1.TabIndex = 124;
            this.lblTel1.Text = "tel1";
            // 
            // lblDir
            // 
            this.lblDir.AutoSize = true;
            this.lblDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDir.Location = new System.Drawing.Point(437, 107);
            this.lblDir.Name = "lblDir";
            this.lblDir.Size = new System.Drawing.Size(22, 16);
            this.lblDir.TabIndex = 123;
            this.lblDir.Text = "dir";
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.lblLogo.Location = new System.Drawing.Point(425, 75);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(65, 29);
            this.lblLogo.TabIndex = 122;
            this.lblLogo.Text = "logo";
            // 
            // lblUsuario
            // 
            this.lblUsuario.BackColor = System.Drawing.Color.DarkCyan;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.White;
            this.lblUsuario.Location = new System.Drawing.Point(429, 163);
            this.lblUsuario.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(417, 38);
            this.lblUsuario.TabIndex = 127;
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmMontoAPagar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkCyan;
            this.ClientSize = new System.Drawing.Size(371, 249);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.lblrnc);
            this.Controls.Add(this.lblTel2);
            this.Controls.Add(this.lblTel1);
            this.Controls.Add(this.lblDir);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.gbPagar);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmMontoAPagar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cuadres y Pagos";
            this.Load += new System.EventHandler(this.frmMontoAPagar_Load);
            this.gbPagar.ResumeLayout(false);
            this.gbPagar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button button2;
		public System.Windows.Forms.GroupBox gbPagar;
		public System.Windows.Forms.Button btnC;
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox txtmonto;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtRestante;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtTotal;
        public System.Windows.Forms.Label lblrnc;
        public System.Windows.Forms.Label lblTel2;
        public System.Windows.Forms.Label lblTel1;
        public System.Windows.Forms.Label lblDir;
        public System.Windows.Forms.Label lblLogo;
        public System.Windows.Forms.Label lblUsuario;
    }
}
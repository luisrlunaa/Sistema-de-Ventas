namespace Capa_de_Presentacion
{
	partial class frmPagar
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
            this.btnC = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtmonto = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDev = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtpaga = new System.Windows.Forms.TextBox();
            this.txtCaja1 = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtIdp = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.gbPagar.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPagar
            // 
            this.gbPagar.BackColor = System.Drawing.Color.RoyalBlue;
            this.gbPagar.Controls.Add(this.btnC);
            this.gbPagar.Controls.Add(this.label6);
            this.gbPagar.Controls.Add(this.txtmonto);
            this.gbPagar.Controls.Add(this.label5);
            this.gbPagar.Controls.Add(this.txtDev);
            this.gbPagar.Controls.Add(this.button2);
            this.gbPagar.Controls.Add(this.label3);
            this.gbPagar.Controls.Add(this.label4);
            this.gbPagar.Controls.Add(this.txtpaga);
            this.gbPagar.Controls.Add(this.txtCaja1);
            this.gbPagar.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbPagar.ForeColor = System.Drawing.Color.Black;
            this.gbPagar.Location = new System.Drawing.Point(13, 13);
            this.gbPagar.Margin = new System.Windows.Forms.Padding(4);
            this.gbPagar.Name = "gbPagar";
            this.gbPagar.Padding = new System.Windows.Forms.Padding(4);
            this.gbPagar.Size = new System.Drawing.Size(704, 200);
            this.gbPagar.TabIndex = 1;
            this.gbPagar.TabStop = false;
            this.gbPagar.Text = "Realizar Pago";
            // 
            // btnC
            // 
            this.btnC.FlatAppearance.BorderSize = 0;
            this.btnC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnC.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnC.ForeColor = System.Drawing.Color.Red;
            this.btnC.Location = new System.Drawing.Point(662, 19);
            this.btnC.Margin = new System.Windows.Forms.Padding(4);
            this.btnC.Name = "btnC";
            this.btnC.Size = new System.Drawing.Size(29, 35);
            this.btnC.TabIndex = 18;
            this.btnC.Text = "X";
            this.btnC.UseVisualStyleBackColor = true;
            this.btnC.Click += new System.EventHandler(this.btnC_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(199, 36);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "Monto a Pagar :";
            // 
            // txtmonto
            // 
            this.txtmonto.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmonto.ForeColor = System.Drawing.Color.MidnightBlue;
            this.txtmonto.Location = new System.Drawing.Point(201, 63);
            this.txtmonto.Margin = new System.Windows.Forms.Padding(4);
            this.txtmonto.Multiline = true;
            this.txtmonto.Name = "txtmonto";
            this.txtmonto.ReadOnly = true;
            this.txtmonto.Size = new System.Drawing.Size(143, 47);
            this.txtmonto.TabIndex = 12;
            this.txtmonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(542, 36);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Devuelta :";
            // 
            // txtDev
            // 
            this.txtDev.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDev.ForeColor = System.Drawing.Color.MidnightBlue;
            this.txtDev.Location = new System.Drawing.Point(545, 64);
            this.txtDev.Margin = new System.Windows.Forms.Padding(4);
            this.txtDev.Multiline = true;
            this.txtDev.Name = "txtDev";
            this.txtDev.Size = new System.Drawing.Size(143, 47);
            this.txtDev.TabIndex = 10;
            this.txtDev.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDev.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFinal_KeyPress);
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
            this.button2.Location = new System.Drawing.Point(550, 138);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 54);
            this.button2.TabIndex = 9;
            this.button2.Text = "Pagar";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(378, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Pagar con :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 36);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Monto En Caja";
            // 
            // txtpaga
            // 
            this.txtpaga.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpaga.ForeColor = System.Drawing.Color.MidnightBlue;
            this.txtpaga.Location = new System.Drawing.Point(381, 64);
            this.txtpaga.Margin = new System.Windows.Forms.Padding(4);
            this.txtpaga.Multiline = true;
            this.txtpaga.Name = "txtpaga";
            this.txtpaga.Size = new System.Drawing.Size(143, 47);
            this.txtpaga.TabIndex = 6;
            this.txtpaga.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtpaga.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFinal_KeyPress);
            this.txtpaga.Leave += new System.EventHandler(this.txtpaga_Leave);
            // 
            // txtCaja1
            // 
            this.txtCaja1.BackColor = System.Drawing.Color.RoyalBlue;
            this.txtCaja1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCaja1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCaja1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.txtCaja1.Location = new System.Drawing.Point(23, 63);
            this.txtCaja1.Margin = new System.Windows.Forms.Padding(4);
            this.txtCaja1.Multiline = true;
            this.txtCaja1.Name = "txtCaja1";
            this.txtCaja1.ReadOnly = true;
            this.txtCaja1.Size = new System.Drawing.Size(144, 48);
            this.txtCaja1.TabIndex = 5;
            this.txtCaja1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtId
            // 
            this.txtId.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtId.ForeColor = System.Drawing.Color.MidnightBlue;
            this.txtId.Location = new System.Drawing.Point(941, 476);
            this.txtId.Margin = new System.Windows.Forms.Padding(4);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(78, 22);
            this.txtId.TabIndex = 16;
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtIdp
            // 
            this.txtIdp.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdp.Location = new System.Drawing.Point(941, 284);
            this.txtIdp.Margin = new System.Windows.Forms.Padding(4);
            this.txtIdp.Name = "txtIdp";
            this.txtIdp.Size = new System.Drawing.Size(78, 22);
            this.txtIdp.TabIndex = 17;
            this.txtIdp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarForeColor = System.Drawing.Color.MidnightBlue;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(941, 364);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(150, 22);
            this.dateTimePicker1.TabIndex = 18;
            // 
            // frmPagar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(732, 223);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txtIdp);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.gbPagar);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmPagar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cuadres y Pagos";
            this.Load += new System.EventHandler(this.frmPagar_Load_1);
            this.gbPagar.ResumeLayout(false);
            this.gbPagar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtCaja1;
		public System.Windows.Forms.GroupBox gbPagar;
		public System.Windows.Forms.TextBox txtmonto;
		public System.Windows.Forms.TextBox txtDev;
		public System.Windows.Forms.TextBox txtpaga;
		public System.Windows.Forms.Button btnC;
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
		public System.Windows.Forms.TextBox txtId;
		public System.Windows.Forms.TextBox txtIdp;
		public System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}
namespace Capa_de_Presentacion
{
	partial class frmMovimientoCaja
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

#pragma warning disable CS0115 // 'frmMovimientoCaja.Dispose(bool)': no se encontró ningún miembro adecuado para invalidar
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
#pragma warning restore CS0115 // 'frmMovimientoCaja.Dispose(bool)': no se encontró ningún miembro adecuado para invalidar
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.montoini = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ganancias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.perdidas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.montofin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.motivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_pago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ingresos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.egresos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBuscarCaja = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDir = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.btnimprimir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(26, 124);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(730, 439);
            this.panel1.TabIndex = 15;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 29;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.montoini,
            this.ganancias,
            this.perdidas,
            this.montofin,
            this.motivo,
            this.fecha,
            this.id_pago,
            this.monto,
            this.ingresos,
            this.egresos});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(-1, -1);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.MidnightBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(727, 434);
            this.dataGridView1.TabIndex = 0;
            // 
            // id
            // 
            this.id.HeaderText = "ID Caja";
            this.id.MinimumWidth = 6;
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 125;
            // 
            // montoini
            // 
            this.montoini.HeaderText = "Monto Inicial";
            this.montoini.MinimumWidth = 6;
            this.montoini.Name = "montoini";
            this.montoini.ReadOnly = true;
            this.montoini.Width = 150;
            // 
            // ganancias
            // 
            this.ganancias.HeaderText = "Ganancias";
            this.ganancias.MinimumWidth = 6;
            this.ganancias.Name = "ganancias";
            this.ganancias.Width = 125;
            // 
            // perdidas
            // 
            this.perdidas.HeaderText = "Perdidas";
            this.perdidas.MinimumWidth = 6;
            this.perdidas.Name = "perdidas";
            this.perdidas.Width = 125;
            // 
            // montofin
            // 
            this.montofin.HeaderText = "Monto Final";
            this.montofin.MinimumWidth = 6;
            this.montofin.Name = "montofin";
            this.montofin.ReadOnly = true;
            this.montofin.Width = 150;
            // 
            // motivo
            // 
            this.motivo.HeaderText = "Motivo de la Perdida";
            this.motivo.MinimumWidth = 6;
            this.motivo.Name = "motivo";
            this.motivo.Width = 125;
            // 
            // fecha
            // 
            this.fecha.HeaderText = "Fecha";
            this.fecha.MinimumWidth = 6;
            this.fecha.Name = "fecha";
            this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.fecha.Width = 125;
            // 
            // id_pago
            // 
            this.id_pago.HeaderText = "ID Pago";
            this.id_pago.MinimumWidth = 6;
            this.id_pago.Name = "id_pago";
            this.id_pago.ReadOnly = true;
            this.id_pago.Width = 120;
            // 
            // monto
            // 
            this.monto.HeaderText = "Monto Pagado";
            this.monto.MinimumWidth = 6;
            this.monto.Name = "monto";
            this.monto.Width = 125;
            // 
            // ingresos
            // 
            this.ingresos.HeaderText = "Ingresos";
            this.ingresos.MinimumWidth = 6;
            this.ingresos.Name = "ingresos";
            this.ingresos.Width = 125;
            // 
            // egresos
            // 
            this.egresos.HeaderText = "Egresos";
            this.egresos.MinimumWidth = 6;
            this.egresos.Name = "egresos";
            this.egresos.Width = 125;
            // 
            // txtBuscarCaja
            // 
            this.txtBuscarCaja.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscarCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarCaja.Location = new System.Drawing.Point(174, 87);
            this.txtBuscarCaja.Margin = new System.Windows.Forms.Padding(4);
            this.txtBuscarCaja.Name = "txtBuscarCaja";
            this.txtBuscarCaja.Size = new System.Drawing.Size(189, 25);
            this.txtBuscarCaja.TabIndex = 14;
            this.txtBuscarCaja.TextChanged += new System.EventHandler(this.txtBuscarCaja_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 88);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Buscar por ID Caja :";
            // 
            // lblDir
            // 
            this.lblDir.AutoSize = true;
            this.lblDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDir.Location = new System.Drawing.Point(944, 241);
            this.lblDir.Name = "lblDir";
            this.lblDir.Size = new System.Drawing.Size(24, 17);
            this.lblDir.TabIndex = 54;
            this.lblDir.Text = "dir";
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.Location = new System.Drawing.Point(942, 212);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(65, 29);
            this.lblLogo.TabIndex = 53;
            this.lblLogo.Text = "logo";
            // 
            // btnimprimir
            // 
            this.btnimprimir.BackColor = System.Drawing.Color.Khaki;
            this.btnimprimir.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnimprimir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnimprimir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnimprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnimprimir.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnimprimir.ForeColor = System.Drawing.Color.Black;
            this.btnimprimir.Image = global::Capa_de_Presentacion.Properties.Resources.icons8_imprimir;
            this.btnimprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnimprimir.Location = new System.Drawing.Point(634, 83);
            this.btnimprimir.Margin = new System.Windows.Forms.Padding(4);
            this.btnimprimir.Name = "btnimprimir";
            this.btnimprimir.Size = new System.Drawing.Size(119, 33);
            this.btnimprimir.TabIndex = 16;
            this.btnimprimir.Text = "Imprimir";
            this.btnimprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnimprimir.UseVisualStyleBackColor = false;
            this.btnimprimir.Click += new System.EventHandler(this.btnimprimir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial Black", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(734, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 24);
            this.label2.TabIndex = 55;
            this.label2.Text = "X";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Open Sans Semibold", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(251, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(258, 33);
            this.label11.TabIndex = 79;
            this.label11.Text = "Movimientos en Caja";
            // 
            // frmMovimientoCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaGreen;
            this.ClientSize = new System.Drawing.Size(773, 592);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDir);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.btnimprimir);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtBuscarCaja);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMovimientoCaja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movimientos en Caja";
            this.Load += new System.EventHandler(this.frmMovimientoCaja_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btnimprimir;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.TextBox txtBuscarCaja;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridViewTextBoxColumn id;
		private System.Windows.Forms.DataGridViewTextBoxColumn montoini;
		private System.Windows.Forms.DataGridViewTextBoxColumn ganancias;
		private System.Windows.Forms.DataGridViewTextBoxColumn perdidas;
		private System.Windows.Forms.DataGridViewTextBoxColumn montofin;
		private System.Windows.Forms.DataGridViewTextBoxColumn motivo;
		private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
		private System.Windows.Forms.DataGridViewTextBoxColumn id_pago;
		private System.Windows.Forms.DataGridViewTextBoxColumn monto;
		private System.Windows.Forms.DataGridViewTextBoxColumn ingresos;
		private System.Windows.Forms.DataGridViewTextBoxColumn egresos;
		public System.Windows.Forms.Label lblDir;
		public System.Windows.Forms.Label lblLogo;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.Label label11;
	}
}
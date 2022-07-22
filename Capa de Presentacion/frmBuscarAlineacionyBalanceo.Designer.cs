namespace Capa_de_Presentacion
{
    partial class FrmBuscarAlineacionyBalanceo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

#pragma warning disable CS0115 // 'frmBuscarEquipo.Dispose(bool)': no se encontró ningún miembro adecuado para invalidar
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
#pragma warning restore CS0115 // 'frmBuscarEquipo.Dispose(bool)': no se encontró ningún miembro adecuado para invalidar
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipoDeTrabajo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vehiculo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AroGoma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nota = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbtipo = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txttotalG = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblDir = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.agregargasto = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpfecha2 = new System.Windows.Forms.DateTimePicker();
            this.dtpfecha1 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 29;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.tipoDeTrabajo,
            this.vehiculo,
            this.AroGoma,
            this.precio,
            this.nota,
            this.fecha});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(16, 138);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(676, 353);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.MinimumWidth = 6;
            this.id.Name = "id";
            this.id.Visible = false;
            this.id.Width = 125;
            // 
            // tipoDeTrabajo
            // 
            this.tipoDeTrabajo.HeaderText = "Tipo de Trabajo";
            this.tipoDeTrabajo.MinimumWidth = 6;
            this.tipoDeTrabajo.Name = "tipoDeTrabajo";
            this.tipoDeTrabajo.Width = 125;
            // 
            // vehiculo
            // 
            this.vehiculo.HeaderText = "Vehiculo";
            this.vehiculo.MinimumWidth = 6;
            this.vehiculo.Name = "vehiculo";
            this.vehiculo.Width = 125;
            // 
            // AroGoma
            // 
            this.AroGoma.HeaderText = "Gomas Aro No.";
            this.AroGoma.MinimumWidth = 6;
            this.AroGoma.Name = "AroGoma";
            this.AroGoma.Width = 125;
            // 
            // precio
            // 
            this.precio.HeaderText = "Precio";
            this.precio.MinimumWidth = 6;
            this.precio.Name = "precio";
            this.precio.Width = 125;
            // 
            // nota
            // 
            this.nota.HeaderText = "Nota";
            this.nota.MinimumWidth = 10;
            this.nota.Name = "nota";
            this.nota.Visible = false;
            this.nota.Width = 135;
            // 
            // fecha
            // 
            // 
            // 
            // 
            this.fecha.BackgroundStyle.Class = "DataGridViewDateTimeBorder";
            this.fecha.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.fecha.HeaderText = "Fecha";
            this.fecha.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            this.fecha.MinimumWidth = 6;
            // 
            // 
            // 
            this.fecha.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.fecha.MonthCalendar.BackgroundStyle.Class = "";
            this.fecha.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.fecha.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.fecha.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.fecha.MonthCalendar.DisplayMonth = new System.DateTime(2019, 7, 1, 0, 0, 0, 0);
            this.fecha.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.fecha.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.fecha.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.fecha.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.fecha.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.fecha.Name = "fecha";
            this.fecha.Width = 125;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(680, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 22);
            this.label2.TabIndex = 4;
            this.label2.Text = "X";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(218, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(320, 24);
            this.label3.TabIndex = 113;
            this.label3.Text = "Listado de Alineacion y Balanceo";
            // 
            // cbtipo
            // 
            this.cbtipo.BackColor = System.Drawing.Color.White;
            this.cbtipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbtipo.ForeColor = System.Drawing.Color.MidnightBlue;
            this.cbtipo.FormattingEnabled = true;
            this.cbtipo.Location = new System.Drawing.Point(115, 112);
            this.cbtipo.Name = "cbtipo";
            this.cbtipo.Size = new System.Drawing.Size(131, 21);
            this.cbtipo.TabIndex = 114;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(127, 95);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 13);
            this.label11.TabIndex = 115;
            this.label11.Text = "Por Tipo de trabajo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 505);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 117;
            this.label4.Text = "Total Vendido:";
            // 
            // txttotalG
            // 
            this.txttotalG.Location = new System.Drawing.Point(93, 503);
            this.txttotalG.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txttotalG.Name = "txttotalG";
            this.txttotalG.ReadOnly = true;
            this.txttotalG.Size = new System.Drawing.Size(114, 20);
            this.txttotalG.TabIndex = 116;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Khaki;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.Black;
            this.btnCancelar.Image = global::Capa_de_Presentacion.Properties.Resources.icons8_imprimir;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(590, 497);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(103, 32);
            this.btnCancelar.TabIndex = 118;
            this.btnCancelar.Text = "Imprimir";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblDir
            // 
            this.lblDir.AutoSize = true;
            this.lblDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDir.ForeColor = System.Drawing.Color.Black;
            this.lblDir.Location = new System.Drawing.Point(874, 245);
            this.lblDir.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDir.Name = "lblDir";
            this.lblDir.Size = new System.Drawing.Size(61, 13);
            this.lblDir.TabIndex = 122;
            this.lblDir.Text = "Direccion";
            this.lblDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.Location = new System.Drawing.Point(872, 216);
            this.lblLogo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(51, 22);
            this.lblLogo.TabIndex = 121;
            this.lblLogo.Text = "logo";
            // 
            // agregargasto
            // 
            this.agregargasto.BackColor = System.Drawing.Color.Transparent;
            this.agregargasto.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.agregargasto.FlatAppearance.BorderSize = 0;
            this.agregargasto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.agregargasto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.agregargasto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.agregargasto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.agregargasto.ForeColor = System.Drawing.Color.Black;
            this.agregargasto.Image = global::Capa_de_Presentacion.Properties.Resources.icons8_buscar;
            this.agregargasto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.agregargasto.Location = new System.Drawing.Point(664, 107);
            this.agregargasto.Name = "agregargasto";
            this.agregargasto.Size = new System.Drawing.Size(33, 27);
            this.agregargasto.TabIndex = 123;
            this.agregargasto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.agregargasto.UseVisualStyleBackColor = false;
            this.agregargasto.Click += new System.EventHandler(this.agregargasto_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Image = global::Capa_de_Presentacion.Properties.Resources.icons8_buscar;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(250, 107);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 27);
            this.button1.TabIndex = 124;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(13, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 125;
            this.label6.Text = "Tipos de Filtrados";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(293, 113);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(151, 20);
            this.textBox1.TabIndex = 126;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(320, 97);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 127;
            this.label1.Text = "Buscar Por Nombre:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(590, 89);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 110;
            this.label12.Text = "Hasta:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(486, 89);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 109;
            this.label7.Text = "Desde :";
            // 
            // dtpfecha2
            // 
            this.dtpfecha2.CustomFormat = "";
            this.dtpfecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfecha2.Location = new System.Drawing.Point(566, 113);
            this.dtpfecha2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpfecha2.Name = "dtpfecha2";
            this.dtpfecha2.Size = new System.Drawing.Size(92, 20);
            this.dtpfecha2.TabIndex = 107;
            // 
            // dtpfecha1
            // 
            this.dtpfecha1.CustomFormat = "";
            this.dtpfecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfecha1.Location = new System.Drawing.Point(462, 113);
            this.dtpfecha1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpfecha1.Name = "dtpfecha1";
            this.dtpfecha1.Size = new System.Drawing.Size(92, 20);
            this.dtpfecha1.TabIndex = 107;
            // 
            // FrmBuscarAlineacionyBalanceo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(67)))), ((int)(((byte)(204)))));
            this.ClientSize = new System.Drawing.Size(706, 538);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dtpfecha2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dtpfecha1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.agregargasto);
            this.Controls.Add(this.lblDir);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txttotalG);
            this.Controls.Add(this.cbtipo);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmBuscarAlineacionyBalanceo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Buscar Equipo Reparacion";
            this.Load += new System.EventHandler(this.frmBuscarAlineacionyBalanceo_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmBuscarAlineacionyBalanceo_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
#pragma warning disable CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cbtipo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoDeTrabajo;
        private System.Windows.Forms.DataGridViewTextBoxColumn vehiculo;
        private System.Windows.Forms.DataGridViewTextBoxColumn AroGoma;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn nota;
        private DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn fecha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txttotalG;
        public System.Windows.Forms.Button btnCancelar;
        public System.Windows.Forms.Label lblDir;
        public System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Button agregargasto;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpfecha2;
        private System.Windows.Forms.DateTimePicker dtpfecha1;
#pragma warning restore CS0246 // El nombre del tipo o del espacio de nombres 'DevComponents' no se encontró (¿falta una directiva using o una referencia de ensamblado?)
    }
}
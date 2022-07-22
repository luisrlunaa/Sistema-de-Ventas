
namespace Capa_de_Presentacion
{
    partial class frmlistadodecotizacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chknombre = new System.Windows.Forms.CheckBox();
            this.chkid = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpfecha2 = new System.Windows.Forms.DateTimePicker();
            this.dtpfecha1 = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombrecliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idEm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn();
            this.txtBuscarid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.lblrnc = new System.Windows.Forms.Label();
            this.lbltel = new System.Windows.Forms.Label();
            this.lbltel1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDir = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // chknombre
            // 
            this.chknombre.AutoSize = true;
            this.chknombre.Location = new System.Drawing.Point(21, 124);
            this.chknombre.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chknombre.Name = "chknombre";
            this.chknombre.Size = new System.Drawing.Size(15, 14);
            this.chknombre.TabIndex = 142;
            this.chknombre.UseVisualStyleBackColor = true;
            this.chknombre.CheckedChanged += new System.EventHandler(this.chknombre_CheckedChanged);
            // 
            // chkid
            // 
            this.chkid.AutoSize = true;
            this.chkid.Location = new System.Drawing.Point(21, 106);
            this.chkid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkid.Name = "chkid";
            this.chkid.Size = new System.Drawing.Size(15, 14);
            this.chkid.TabIndex = 141;
            this.chkid.UseVisualStyleBackColor = true;
            this.chkid.CheckedChanged += new System.EventHandler(this.chkid_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(36, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 15);
            this.label8.TabIndex = 140;
            this.label8.Text = "Filtrar por Nombre Cliente:";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.dtpfecha2);
            this.groupBox3.Controls.Add(this.dtpfecha1);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(22, 174);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(356, 73);
            this.groupBox3.TabIndex = 139;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filtrar por Fecha";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkGray;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Image = global::Capa_de_Presentacion.Properties.Resources.icons8_buscar;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(242, 32);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 31);
            this.button1.TabIndex = 111;
            this.button1.Text = "Buscar";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(139, 19);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 15);
            this.label12.TabIndex = 110;
            this.label12.Text = "Hasta:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(33, 19);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 15);
            this.label11.TabIndex = 109;
            this.label11.Text = "Desde :";
            // 
            // dtpfecha2
            // 
            this.dtpfecha2.CustomFormat = "";
            this.dtpfecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfecha2.Location = new System.Drawing.Point(115, 43);
            this.dtpfecha2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpfecha2.Name = "dtpfecha2";
            this.dtpfecha2.Size = new System.Drawing.Size(92, 21);
            this.dtpfecha2.TabIndex = 107;
            // 
            // dtpfecha1
            // 
            this.dtpfecha1.CustomFormat = "";
            this.dtpfecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfecha1.Location = new System.Drawing.Point(9, 43);
            this.dtpfecha1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpfecha1.Name = "dtpfecha1";
            this.dtpfecha1.Size = new System.Drawing.Size(92, 21);
            this.dtpfecha1.TabIndex = 107;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(25, 255);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(353, 280);
            this.panel1.TabIndex = 138;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 29;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.nombrecliente,
            this.idEm,
            this.total,
            this.fecha});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(3, 5);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(347, 269);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.MinimumWidth = 6;
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 48;
            // 
            // nombrecliente
            // 
            this.nombrecliente.HeaderText = "Nombre Cliente";
            this.nombrecliente.MinimumWidth = 6;
            this.nombrecliente.Name = "nombrecliente";
            this.nombrecliente.Width = 144;
            // 
            // idEm
            // 
            this.idEm.HeaderText = "Id Empleado";
            this.idEm.MinimumWidth = 6;
            this.idEm.Name = "idEm";
            this.idEm.Visible = false;
            this.idEm.Width = 122;
            // 
            // total
            // 
            this.total.HeaderText = "Total";
            this.total.MinimumWidth = 6;
            this.total.Name = "total";
            this.total.Width = 70;
            // 
            // fecha
            // 
            // 
            // 
            // 
            this.fecha.BackgroundStyle.Class = "DataGridViewDateTimeBorder";
            this.fecha.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.fecha.HeaderText = "Fecha de Cotizacion";
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
            this.fecha.Width = 180;
            // 
            // txtBuscarid
            // 
            this.txtBuscarid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscarid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarid.Location = new System.Drawing.Point(22, 144);
            this.txtBuscarid.Name = "txtBuscarid";
            this.txtBuscarid.Size = new System.Drawing.Size(198, 21);
            this.txtBuscarid.TabIndex = 137;
            this.txtBuscarid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarid_KeyPress);
            this.txtBuscarid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscarid_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 15);
            this.label1.TabIndex = 136;
            this.label1.Text = "Filtrar por ID Cotizacion:";
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
            this.btnCancelar.Location = new System.Drawing.Point(221, 95);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(158, 32);
            this.btnCancelar.TabIndex = 143;
            this.btnCancelar.Text = "Imprimir Reporte";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Firebrick;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Image = global::Capa_de_Presentacion.Properties.Resources.icons8_eliminar;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(273, 137);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(106, 32);
            this.button3.TabIndex = 144;
            this.button3.Text = "Eliminar";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(34, 41);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(338, 24);
            this.label3.TabIndex = 145;
            this.label3.Text = "Listado de Cotizaciones Realizadas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(370, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 22);
            this.label2.TabIndex = 146;
            this.label2.Text = "X";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblCorreo
            // 
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Location = new System.Drawing.Point(632, 119);
            this.lblCorreo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.Size = new System.Drawing.Size(38, 13);
            this.lblCorreo.TabIndex = 164;
            this.lblCorreo.Text = "Correo";
            this.lblCorreo.Visible = false;
            // 
            // lblrnc
            // 
            this.lblrnc.AutoSize = true;
            this.lblrnc.Location = new System.Drawing.Point(632, 93);
            this.lblrnc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblrnc.Name = "lblrnc";
            this.lblrnc.Size = new System.Drawing.Size(30, 13);
            this.lblrnc.TabIndex = 163;
            this.lblrnc.Text = "RNC";
            this.lblrnc.Visible = false;
            // 
            // lbltel
            // 
            this.lbltel.AutoSize = true;
            this.lbltel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltel.Location = new System.Drawing.Point(667, 145);
            this.lbltel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbltel.Name = "lbltel";
            this.lbltel.Size = new System.Drawing.Size(24, 13);
            this.lbltel.TabIndex = 162;
            this.lbltel.Text = "tel2";
            // 
            // lbltel1
            // 
            this.lbltel1.AutoSize = true;
            this.lbltel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltel1.Location = new System.Drawing.Point(598, 145);
            this.lbltel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbltel1.Name = "lbltel1";
            this.lbltel1.Size = new System.Drawing.Size(24, 13);
            this.lbltel1.TabIndex = 161;
            this.lbltel1.Text = "tel1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(480, 145);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 13);
            this.label4.TabIndex = 160;
            this.label4.Text = "Numero Telefonico :";
            // 
            // lblDir
            // 
            this.lblDir.AutoSize = true;
            this.lblDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDir.Location = new System.Drawing.Point(500, 128);
            this.lblDir.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDir.Name = "lblDir";
            this.lblDir.Size = new System.Drawing.Size(18, 13);
            this.lblDir.TabIndex = 159;
            this.lblDir.Text = "dir";
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.Location = new System.Drawing.Point(524, 101);
            this.lblLogo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(47, 24);
            this.lblLogo.TabIndex = 158;
            this.lblLogo.Text = "logo";
            // 
            // frmlistadodecotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(67)))), ((int)(((byte)(204)))));
            this.ClientSize = new System.Drawing.Size(400, 553);
            this.Controls.Add(this.lblCorreo);
            this.Controls.Add(this.lblrnc);
            this.Controls.Add(this.lbltel);
            this.Controls.Add(this.lbltel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblDir);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.chknombre);
            this.Controls.Add(this.chkid);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtBuscarid);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmlistadodecotizacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmlistadodecotizacion_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chknombre;
        private System.Windows.Forms.CheckBox chkid;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpfecha2;
        private System.Windows.Forms.DateTimePicker dtpfecha1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombrecliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn idEm;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn fecha;
        private System.Windows.Forms.TextBox txtBuscarid;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lblCorreo;
        public System.Windows.Forms.Label lblrnc;
        public System.Windows.Forms.Label lbltel;
        public System.Windows.Forms.Label lbltel1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label lblDir;
        public System.Windows.Forms.Label lblLogo;
    }
}
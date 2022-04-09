namespace Capa_de_Presentacion
{
	partial class frmListadoVentas
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

#pragma warning disable CS0115 // 'frmListadoVentas.Dispose(bool)': no se encontró ningún miembro adecuado para invalidar
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
#pragma warning restore CS0115 // 'frmListadoVentas.Dispose(bool)': no se encontró ningún miembro adecuado para invalidar
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombrecliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idcliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idEm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.restante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn();
            this.NCF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nroComprobante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Direccion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rnccliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ultimafecha = new DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn();
            this.txtBuscarid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.id_p = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sub = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCantvend = new System.Windows.Forms.TextBox();
            this.txtidprod = new System.Windows.Forms.TextBox();
            this.lblTV = new System.Windows.Forms.Label();
            this.lblPV = new System.Windows.Forms.Label();
            this.txtprod = new System.Windows.Forms.TextBox();
            this.txtTtal = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.lblDir = new System.Windows.Forms.Label();
            this.lblLogo = new System.Windows.Forms.Label();
            this.txtRepi = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpfecha2 = new System.Windows.Forms.DateTimePicker();
            this.dtpfecha1 = new System.Windows.Forms.DateTimePicker();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGanancias = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.vereliminadas = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.chknombre = new System.Windows.Forms.CheckBox();
            this.chkid = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txttotalpendiente = new System.Windows.Forms.TextBox();
            this.cbPendiente = new System.Windows.Forms.CheckBox();
            this.cbtipofactura = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cktipofactura = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cbtipodocumento = new System.Windows.Forms.CheckBox();
            this.combo_tipo_NCF = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txttotalventaespecifica = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.CategoryOfProducts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantidadOfProducts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioOfProducts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(10, 118);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(704, 326);
            this.panel1.TabIndex = 9;
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
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 29;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.nombrecliente,
            this.idcliente,
            this.Tipo,
            this.idEm,
            this.restante,
            this.total,
            this.fecha,
            this.NCF,
            this.nroComprobante,
            this.Direccion,
            this.rnccliente,
            this.ultimafecha});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(1, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(700, 319);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // id
            // 
            this.id.HeaderText = "Id Venta";
            this.id.MinimumWidth = 6;
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 93;
            // 
            // nombrecliente
            // 
            this.nombrecliente.HeaderText = "Nombre Cliente";
            this.nombrecliente.MinimumWidth = 6;
            this.nombrecliente.Name = "nombrecliente";
            this.nombrecliente.Width = 144;
            // 
            // idcliente
            // 
            this.idcliente.HeaderText = "Id Cliente";
            this.idcliente.MinimumWidth = 6;
            this.idcliente.Name = "idcliente";
            this.idcliente.Visible = false;
            this.idcliente.Width = 101;
            // 
            // Tipo
            // 
            this.Tipo.HeaderText = "Tipo Factura";
            this.Tipo.MinimumWidth = 6;
            this.Tipo.Name = "Tipo";
            this.Tipo.Width = 125;
            // 
            // idEm
            // 
            this.idEm.HeaderText = "Id Empleado";
            this.idEm.MinimumWidth = 6;
            this.idEm.Name = "idEm";
            this.idEm.Visible = false;
            this.idEm.Width = 122;
            // 
            // restante
            // 
            this.restante.HeaderText = "Restante";
            this.restante.MinimumWidth = 6;
            this.restante.Name = "restante";
            this.restante.Width = 98;
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
            this.fecha.HeaderText = "Fecha de Venta";
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
            this.fecha.Width = 147;
            // 
            // NCF
            // 
            this.NCF.HeaderText = "Tipo Comprobante";
            this.NCF.MinimumWidth = 6;
            this.NCF.Name = "NCF";
            this.NCF.Width = 166;
            // 
            // nroComprobante
            // 
            this.nroComprobante.HeaderText = "Numero de Comprobante";
            this.nroComprobante.MinimumWidth = 6;
            this.nroComprobante.Name = "nroComprobante";
            this.nroComprobante.Width = 213;
            // 
            // Direccion
            // 
            this.Direccion.HeaderText = "Direccion";
            this.Direccion.MinimumWidth = 6;
            this.Direccion.Name = "Direccion";
            this.Direccion.Width = 101;
            // 
            // rnccliente
            // 
            this.rnccliente.HeaderText = "RNC Cliente";
            this.rnccliente.MinimumWidth = 6;
            this.rnccliente.Name = "rnccliente";
            this.rnccliente.Width = 120;
            // 
            // ultimafecha
            // 
            // 
            // 
            // 
            this.ultimafecha.BackgroundStyle.Class = "DataGridViewDateTimeBorder";
            this.ultimafecha.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ultimafecha.HeaderText = "Ultimo Pago";
            this.ultimafecha.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            this.ultimafecha.MinimumWidth = 6;
            // 
            // 
            // 
            this.ultimafecha.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.ultimafecha.MonthCalendar.BackgroundStyle.Class = "";
            this.ultimafecha.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ultimafecha.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.ultimafecha.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ultimafecha.MonthCalendar.DisplayMonth = new System.DateTime(2021, 7, 1, 0, 0, 0, 0);
            this.ultimafecha.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.ultimafecha.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.ultimafecha.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.ultimafecha.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ultimafecha.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.ultimafecha.Name = "ultimafecha";
            this.ultimafecha.Width = 120;
            // 
            // txtBuscarid
            // 
            this.txtBuscarid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBuscarid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarid.Location = new System.Drawing.Point(10, 86);
            this.txtBuscarid.Name = "txtBuscarid";
            this.txtBuscarid.Size = new System.Drawing.Size(222, 21);
            this.txtBuscarid.TabIndex = 8;
            this.txtBuscarid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscarid_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(1049, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 22);
            this.label2.TabIndex = 13;
            this.label2.Text = "X";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeight = 29;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_p,
            this.sub,
            this.cant});
            this.dataGridView2.Location = new System.Drawing.Point(1170, 84);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(286, 312);
            this.dataGridView2.TabIndex = 96;
            // 
            // id_p
            // 
            this.id_p.FillWeight = 44.24779F;
            this.id_p.HeaderText = "ID";
            this.id_p.MinimumWidth = 6;
            this.id_p.Name = "id_p";
            // 
            // sub
            // 
            this.sub.FillWeight = 85.64481F;
            this.sub.HeaderText = "Total x Prod";
            this.sub.MinimumWidth = 6;
            this.sub.Name = "sub";
            // 
            // cant
            // 
            this.cant.FillWeight = 170.1074F;
            this.cant.HeaderText = "Cant Vendida x Prod";
            this.cant.MinimumWidth = 6;
            this.cant.Name = "cant";
            // 
            // txtCantvend
            // 
            this.txtCantvend.Location = new System.Drawing.Point(214, 621);
            this.txtCantvend.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCantvend.Name = "txtCantvend";
            this.txtCantvend.ReadOnly = true;
            this.txtCantvend.Size = new System.Drawing.Size(76, 20);
            this.txtCantvend.TabIndex = 99;
            // 
            // txtidprod
            // 
            this.txtidprod.Location = new System.Drawing.Point(1170, 413);
            this.txtidprod.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtidprod.Name = "txtidprod";
            this.txtidprod.Size = new System.Drawing.Size(76, 20);
            this.txtidprod.TabIndex = 101;
            // 
            // lblTV
            // 
            this.lblTV.AutoSize = true;
            this.lblTV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTV.Location = new System.Drawing.Point(8, 622);
            this.lblTV.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTV.Name = "lblTV";
            this.lblTV.Size = new System.Drawing.Size(199, 15);
            this.lblTV.TabIndex = 102;
            this.lblTV.Text = "Total de Veces Vendidas en el Dia :";
            // 
            // lblPV
            // 
            this.lblPV.AutoSize = true;
            this.lblPV.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPV.Location = new System.Drawing.Point(8, 596);
            this.lblPV.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPV.Name = "lblPV";
            this.lblPV.Size = new System.Drawing.Size(189, 15);
            this.lblPV.TabIndex = 104;
            this.lblPV.Text = "Producto Mas Vendido en el Dia :";
            // 
            // txtprod
            // 
            this.txtprod.Location = new System.Drawing.Point(214, 596);
            this.txtprod.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtprod.Name = "txtprod";
            this.txtprod.ReadOnly = true;
            this.txtprod.Size = new System.Drawing.Size(108, 20);
            this.txtprod.TabIndex = 103;
            // 
            // txtTtal
            // 
            this.txtTtal.Location = new System.Drawing.Point(932, 524);
            this.txtTtal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTtal.Name = "txtTtal";
            this.txtTtal.ReadOnly = true;
            this.txtTtal.Size = new System.Drawing.Size(133, 20);
            this.txtTtal.TabIndex = 105;
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
            this.btnCancelar.Location = new System.Drawing.Point(392, 535);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(245, 32);
            this.btnCancelar.TabIndex = 12;
            this.btnCancelar.Text = "Imprimir Reporte de Ventas";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.SpringGreen;
            this.btnNuevo.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnNuevo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevo.ForeColor = System.Drawing.Color.Black;
            this.btnNuevo.Image = global::Capa_de_Presentacion.Properties.Resources.icons8_nuevo;
            this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevo.Location = new System.Drawing.Point(528, 457);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(108, 32);
            this.btnNuevo.TabIndex = 10;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // lblDir
            // 
            this.lblDir.AutoSize = true;
            this.lblDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDir.ForeColor = System.Drawing.Color.Black;
            this.lblDir.Location = new System.Drawing.Point(1190, 474);
            this.lblDir.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDir.Name = "lblDir";
            this.lblDir.Size = new System.Drawing.Size(61, 13);
            this.lblDir.TabIndex = 108;
            this.lblDir.Text = "Direccion";
            this.lblDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.Location = new System.Drawing.Point(1188, 446);
            this.lblLogo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(51, 22);
            this.lblLogo.TabIndex = 107;
            this.lblLogo.Text = "logo";
            // 
            // txtRepi
            // 
            this.txtRepi.Location = new System.Drawing.Point(1281, 413);
            this.txtRepi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRepi.Name = "txtRepi";
            this.txtRepi.Size = new System.Drawing.Size(76, 20);
            this.txtRepi.TabIndex = 109;
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
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(10, 457);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(341, 99);
            this.groupBox3.TabIndex = 110;
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
            this.button1.Location = new System.Drawing.Point(226, 47);
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
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(137, 27);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 15);
            this.label12.TabIndex = 110;
            this.label12.Text = "Hasta:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(33, 27);
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
            this.dtpfecha2.Location = new System.Drawing.Point(113, 51);
            this.dtpfecha2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpfecha2.Name = "dtpfecha2";
            this.dtpfecha2.Size = new System.Drawing.Size(92, 21);
            this.dtpfecha2.TabIndex = 107;
            // 
            // dtpfecha1
            // 
            this.dtpfecha1.CustomFormat = "";
            this.dtpfecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfecha1.Location = new System.Drawing.Point(9, 51);
            this.dtpfecha1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpfecha1.Name = "dtpfecha1";
            this.dtpfecha1.Size = new System.Drawing.Size(92, 21);
            this.dtpfecha1.TabIndex = 107;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DarkGray;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Image = global::Capa_de_Presentacion.Properties.Resources.icons8_limpiar;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(392, 457);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 32);
            this.button2.TabIndex = 111;
            this.button2.Text = "Limpiar";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(434, 18);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(283, 24);
            this.label3.TabIndex = 112;
            this.label3.Text = "Listado de Ventas Realizadas";
            // 
            // txtGanancias
            // 
            this.txtGanancias.Location = new System.Drawing.Point(932, 548);
            this.txtGanancias.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtGanancias.Name = "txtGanancias";
            this.txtGanancias.ReadOnly = true;
            this.txtGanancias.Size = new System.Drawing.Size(133, 20);
            this.txtGanancias.TabIndex = 117;
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
            this.button3.Location = new System.Drawing.Point(529, 498);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(108, 32);
            this.button3.TabIndex = 119;
            this.button3.Text = "Eliminar";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // vereliminadas
            // 
            this.vereliminadas.AutoSize = true;
            this.vereliminadas.Location = new System.Drawing.Point(527, 90);
            this.vereliminadas.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.vereliminadas.Name = "vereliminadas";
            this.vereliminadas.Size = new System.Drawing.Size(95, 17);
            this.vereliminadas.TabIndex = 120;
            this.vereliminadas.Text = "Ver Eliminadas";
            this.vereliminadas.UseVisualStyleBackColor = true;
            this.vereliminadas.CheckedChanged += new System.EventHandler(this.vereliminadas_CheckedChanged);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Image = global::Capa_de_Presentacion.Properties.Resources.icons8_reestablecer;
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(392, 498);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(108, 32);
            this.button4.TabIndex = 121;
            this.button4.Text = "Devolucion";
            this.button4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // chknombre
            // 
            this.chknombre.AutoSize = true;
            this.chknombre.Location = new System.Drawing.Point(10, 67);
            this.chknombre.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chknombre.Name = "chknombre";
            this.chknombre.Size = new System.Drawing.Size(15, 14);
            this.chknombre.TabIndex = 163;
            this.chknombre.UseVisualStyleBackColor = true;
            this.chknombre.CheckedChanged += new System.EventHandler(this.chknombre_CheckedChanged);
            // 
            // chkid
            // 
            this.chkid.AutoSize = true;
            this.chkid.Location = new System.Drawing.Point(10, 49);
            this.chkid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkid.Name = "chkid";
            this.chkid.Size = new System.Drawing.Size(15, 14);
            this.chkid.TabIndex = 162;
            this.chkid.UseVisualStyleBackColor = true;
            this.chkid.CheckedChanged += new System.EventHandler(this.chkid_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(24, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(151, 15);
            this.label8.TabIndex = 161;
            this.label8.Text = "Filtrar por Nombre Cliente:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(723, 503);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 15);
            this.label7.TabIndex = 160;
            this.label7.Text = "Total Pendiente :";
            this.label7.Visible = false;
            // 
            // txttotalpendiente
            // 
            this.txttotalpendiente.Location = new System.Drawing.Point(932, 502);
            this.txttotalpendiente.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txttotalpendiente.Name = "txttotalpendiente";
            this.txttotalpendiente.ReadOnly = true;
            this.txttotalpendiente.Size = new System.Drawing.Size(132, 20);
            this.txttotalpendiente.TabIndex = 159;
            this.txttotalpendiente.Visible = false;
            // 
            // cbPendiente
            // 
            this.cbPendiente.AutoSize = true;
            this.cbPendiente.BackColor = System.Drawing.Color.Transparent;
            this.cbPendiente.ForeColor = System.Drawing.Color.White;
            this.cbPendiente.Location = new System.Drawing.Point(718, 71);
            this.cbPendiente.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbPendiente.Name = "cbPendiente";
            this.cbPendiente.Size = new System.Drawing.Size(79, 17);
            this.cbPendiente.TabIndex = 158;
            this.cbPendiente.Text = "Pendientes";
            this.cbPendiente.UseVisualStyleBackColor = false;
            // 
            // cbtipofactura
            // 
            this.cbtipofactura.BackColor = System.Drawing.Color.White;
            this.cbtipofactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbtipofactura.ForeColor = System.Drawing.Color.MidnightBlue;
            this.cbtipofactura.FormattingEnabled = true;
            this.cbtipofactura.Location = new System.Drawing.Point(266, 88);
            this.cbtipofactura.Name = "cbtipofactura";
            this.cbtipofactura.Size = new System.Drawing.Size(170, 21);
            this.cbtipofactura.TabIndex = 157;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(285, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 15);
            this.label6.TabIndex = 156;
            // 
            // cktipofactura
            // 
            this.cktipofactura.AutoSize = true;
            this.cktipofactura.BackColor = System.Drawing.Color.Transparent;
            this.cktipofactura.ForeColor = System.Drawing.Color.White;
            this.cktipofactura.Location = new System.Drawing.Point(266, 67);
            this.cktipofactura.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cktipofactura.Name = "cktipofactura";
            this.cktipofactura.Size = new System.Drawing.Size(143, 17);
            this.cktipofactura.TabIndex = 155;
            this.cktipofactura.Text = "Filtrar por tipo de Factura";
            this.cktipofactura.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(464, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 15);
            this.label5.TabIndex = 154;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(718, 89);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(95, 17);
            this.checkBox1.TabIndex = 152;
            this.checkBox1.Text = "Ver Eliminadas";
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // cbtipodocumento
            // 
            this.cbtipodocumento.AutoSize = true;
            this.cbtipodocumento.BackColor = System.Drawing.Color.Transparent;
            this.cbtipodocumento.ForeColor = System.Drawing.Color.White;
            this.cbtipodocumento.Location = new System.Drawing.Point(456, 67);
            this.cbtipodocumento.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbtipodocumento.Name = "cbtipodocumento";
            this.cbtipodocumento.Size = new System.Drawing.Size(128, 17);
            this.cbtipodocumento.TabIndex = 151;
            this.cbtipodocumento.Text = "Filtrar por tipo de NCF";
            this.cbtipodocumento.UseVisualStyleBackColor = false;
            // 
            // combo_tipo_NCF
            // 
            this.combo_tipo_NCF.BackColor = System.Drawing.Color.White;
            this.combo_tipo_NCF.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combo_tipo_NCF.ForeColor = System.Drawing.Color.MidnightBlue;
            this.combo_tipo_NCF.FormattingEnabled = true;
            this.combo_tipo_NCF.Location = new System.Drawing.Point(456, 88);
            this.combo_tipo_NCF.Name = "combo_tipo_NCF";
            this.combo_tipo_NCF.Size = new System.Drawing.Size(236, 21);
            this.combo_tipo_NCF.TabIndex = 150;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(724, 548);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 15);
            this.label9.TabIndex = 148;
            this.label9.Text = "Total de Ganancias :";
            // 
            // txttotalventaespecifica
            // 
            this.txttotalventaespecifica.Location = new System.Drawing.Point(988, 578);
            this.txttotalventaespecifica.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txttotalventaespecifica.Name = "txttotalventaespecifica";
            this.txttotalventaespecifica.ReadOnly = true;
            this.txttotalventaespecifica.Size = new System.Drawing.Size(76, 20);
            this.txttotalventaespecifica.TabIndex = 146;
            this.txttotalventaespecifica.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridView3);
            this.panel2.Location = new System.Drawing.Point(719, 114);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(345, 375);
            this.panel2.TabIndex = 145;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.BackgroundColor = System.Drawing.Color.RoyalBlue;
            this.dataGridView3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView3.ColumnHeadersHeight = 29;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CategoryOfProducts,
            this.CantidadOfProducts,
            this.PrecioOfProducts});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView3.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView3.Location = new System.Drawing.Point(4, 3);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView3.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.Size = new System.Drawing.Size(339, 370);
            this.dataGridView3.TabIndex = 0;
            // 
            // CategoryOfProducts
            // 
            this.CategoryOfProducts.HeaderText = "Tipo";
            this.CategoryOfProducts.MinimumWidth = 125;
            this.CategoryOfProducts.Name = "CategoryOfProducts";
            this.CategoryOfProducts.ReadOnly = true;
            this.CategoryOfProducts.Width = 125;
            // 
            // CantidadOfProducts
            // 
            this.CantidadOfProducts.HeaderText = "Cantidad";
            this.CantidadOfProducts.MinimumWidth = 90;
            this.CantidadOfProducts.Name = "CantidadOfProducts";
            this.CantidadOfProducts.ReadOnly = true;
            this.CantidadOfProducts.Width = 90;
            // 
            // PrecioOfProducts
            // 
            this.PrecioOfProducts.HeaderText = "Total";
            this.PrecioOfProducts.MinimumWidth = 80;
            this.PrecioOfProducts.Name = "PrecioOfProducts";
            this.PrecioOfProducts.ReadOnly = true;
            this.PrecioOfProducts.Width = 125;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(723, 525);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(88, 15);
            this.label14.TabIndex = 142;
            this.label14.Text = "Total Vendido :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(24, 49);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(111, 15);
            this.label15.TabIndex = 136;
            this.label15.Text = "Filtrar por ID Venta:";
            // 
            // frmListadoVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(1076, 575);
            this.Controls.Add(this.chknombre);
            this.Controls.Add(this.chkid);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txttotalpendiente);
            this.Controls.Add(this.cbPendiente);
            this.Controls.Add(this.cbtipofactura);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cktipofactura);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cbtipodocumento);
            this.Controls.Add(this.combo_tipo_NCF);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txttotalventaespecifica);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.vereliminadas);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtGanancias);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtRepi);
            this.Controls.Add(this.lblDir);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.txtTtal);
            this.Controls.Add(this.lblPV);
            this.Controls.Add(this.txtprod);
            this.Controls.Add(this.lblTV);
            this.Controls.Add(this.txtidprod);
            this.Controls.Add(this.txtCantvend);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtBuscarid);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmListadoVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listado de Ventas";
            this.Load += new System.EventHandler(this.frmListadoVentas_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmListadoVentas_MouseDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btnNuevo;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox txtBuscarid;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridView dataGridView2;
		private System.Windows.Forms.DataGridViewTextBoxColumn id_p;
		private System.Windows.Forms.DataGridViewTextBoxColumn sub;
		private System.Windows.Forms.DataGridViewTextBoxColumn cant;
		private System.Windows.Forms.TextBox txtidprod;
		public System.Windows.Forms.Button btnCancelar;
		public System.Windows.Forms.Label lblDir;
		public System.Windows.Forms.Label lblLogo;
		public System.Windows.Forms.TextBox txtRepi;
		public System.Windows.Forms.TextBox txtCantvend;
		public System.Windows.Forms.Label lblTV;
		public System.Windows.Forms.Label lblPV;
		public System.Windows.Forms.TextBox txtprod;
		public System.Windows.Forms.TextBox txtTtal;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.DateTimePicker dtpfecha2;
		private System.Windows.Forms.DateTimePicker dtpfecha1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		public System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.TextBox txtGanancias;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox vereliminadas;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox chknombre;
        private System.Windows.Forms.CheckBox chkid;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txttotalpendiente;
        private System.Windows.Forms.CheckBox cbPendiente;
        public System.Windows.Forms.ComboBox cbtipofactura;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cktipofactura;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox cbtipodocumento;
        private System.Windows.Forms.ComboBox combo_tipo_NCF;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox txttotalventaespecifica;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryOfProducts;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantidadOfProducts;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioOfProducts;
        public System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombrecliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn idcliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn idEm;
        private System.Windows.Forms.DataGridViewTextBoxColumn restante;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn NCF;
        private System.Windows.Forms.DataGridViewTextBoxColumn nroComprobante;
        private System.Windows.Forms.DataGridViewTextBoxColumn Direccion;
        private System.Windows.Forms.DataGridViewTextBoxColumn rnccliente;
        private DevComponents.DotNetBar.Controls.DataGridViewDateTimeInputColumn ultimafecha;
    }
}
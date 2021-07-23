
namespace Capa_de_Presentacion
{
    partial class frmlistadoimei
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
            this.dgvimei = new System.Windows.Forms.DataGridView();
            this.idImei = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IMEI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbl = new System.Windows.Forms.Label();
            this.txtIdP = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvimei)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvimei
            // 
            this.dgvimei.AllowUserToAddRows = false;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvimei.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvimei.BackgroundColor = System.Drawing.Color.White;
            this.dgvimei.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvimei.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvimei.ColumnHeadersHeight = 29;
            this.dgvimei.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idImei,
            this.id,
            this.IMEI,
            this.fecha});
            this.dgvimei.EnableHeadersVisualStyles = false;
            this.dgvimei.Location = new System.Drawing.Point(12, 100);
            this.dgvimei.Name = "dgvimei";
            this.dgvimei.RowHeadersWidth = 51;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.dgvimei.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvimei.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvimei.RowTemplate.Height = 24;
            this.dgvimei.Size = new System.Drawing.Size(635, 330);
            this.dgvimei.TabIndex = 158;
            this.dgvimei.DoubleClick += new System.EventHandler(this.dgvimei_DoubleClick);
            // 
            // idImei
            // 
            this.idImei.HeaderText = "idImei";
            this.idImei.MinimumWidth = 6;
            this.idImei.Name = "idImei";
            this.idImei.Visible = false;
            this.idImei.Width = 125;
            // 
            // id
            // 
            this.id.HeaderText = "Id Producto";
            this.id.MinimumWidth = 115;
            this.id.Name = "id";
            this.id.Width = 115;
            // 
            // IMEI
            // 
            this.IMEI.HeaderText = "IMEI";
            this.IMEI.MinimumWidth = 180;
            this.IMEI.Name = "IMEI";
            this.IMEI.Width = 180;
            // 
            // fecha
            // 
            this.fecha.HeaderText = "Fecha";
            this.fecha.MinimumWidth = 50;
            this.fecha.Name = "fecha";
            this.fecha.Width = 125;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.ForeColor = System.Drawing.Color.White;
            this.lbl.Location = new System.Drawing.Point(131, 51);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(368, 29);
            this.lbl.TabIndex = 159;
            this.lbl.Text = "Listado de IMEI de este Producto";
            // 
            // txtIdP
            // 
            this.txtIdP.Location = new System.Drawing.Point(690, 91);
            this.txtIdP.Name = "txtIdP";
            this.txtIdP.Size = new System.Drawing.Size(100, 22);
            this.txtIdP.TabIndex = 160;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Crimson;
            this.label18.Location = new System.Drawing.Point(625, 9);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(22, 23);
            this.label18.TabIndex = 161;
            this.label18.Text = "X";
            this.label18.Click += new System.EventHandler(this.label18_Click);
            // 
            // frmlistadoimei
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Firebrick;
            this.ClientSize = new System.Drawing.Size(662, 456);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.txtIdP);
            this.Controls.Add(this.lbl);
            this.Controls.Add(this.dgvimei);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmlistadoimei";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmlistadoimei";
            this.Load += new System.EventHandler(this.frmlistadoimei_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmlistadoimei_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvimei)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvimei;
        private System.Windows.Forms.DataGridViewTextBoxColumn idImei;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn IMEI;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        public System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.TextBox txtIdP;
    }
}
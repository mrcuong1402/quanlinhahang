namespace QLNhaHang
{
    partial class fCheckOut
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbDept = new System.Windows.Forms.CheckBox();
            this.panel33 = new System.Windows.Forms.Panel();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnExportBill = new System.Windows.Forms.Button();
            this.panel35 = new System.Windows.Forms.Panel();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.panel36 = new System.Windows.Forms.Panel();
            this.txtNameCustomer = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvBillInfoExport = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.panel33.SuspendLayout();
            this.panel35.SuspendLayout();
            this.panel36.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillInfoExport)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chbDept);
            this.groupBox1.Controls.Add(this.panel33);
            this.groupBox1.Controls.Add(this.btnExportBill);
            this.groupBox1.Controls.Add(this.panel35);
            this.groupBox1.Controls.Add(this.panel36);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(644, 115);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin khách hàng";
            // 
            // chbDept
            // 
            this.chbDept.AutoSize = true;
            this.chbDept.Location = new System.Drawing.Point(397, 73);
            this.chbDept.Name = "chbDept";
            this.chbDept.Size = new System.Drawing.Size(63, 19);
            this.chbDept.TabIndex = 5;
            this.chbDept.Text = "Ghi nợ";
            this.chbDept.UseVisualStyleBackColor = true;
            // 
            // panel33
            // 
            this.panel33.Controls.Add(this.txtAddress);
            this.panel33.Controls.Add(this.label17);
            this.panel33.Location = new System.Drawing.Point(67, 65);
            this.panel33.Name = "panel33";
            this.panel33.Size = new System.Drawing.Size(251, 39);
            this.panel33.TabIndex = 2;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(99, 5);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(152, 22);
            this.txtAddress.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(2, 8);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(49, 15);
            this.label17.TabIndex = 0;
            this.label17.Text = "Địa chỉ:";
            // 
            // btnExportBill
            // 
            this.btnExportBill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnExportBill.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportBill.Image = global::QLNhaHang.Properties.Resources.Print_30px;
            this.btnExportBill.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportBill.Location = new System.Drawing.Point(484, 61);
            this.btnExportBill.Name = "btnExportBill";
            this.btnExportBill.Size = new System.Drawing.Size(103, 39);
            this.btnExportBill.TabIndex = 6;
            this.btnExportBill.Text = "In hóa đơn";
            this.btnExportBill.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExportBill.UseVisualStyleBackColor = true;
            this.btnExportBill.Click += new System.EventHandler(this.btnExportBill_Click);
            // 
            // panel35
            // 
            this.panel35.Controls.Add(this.txtPhoneNumber);
            this.panel35.Controls.Add(this.label19);
            this.panel35.Location = new System.Drawing.Point(335, 21);
            this.panel35.Name = "panel35";
            this.panel35.Size = new System.Drawing.Size(251, 39);
            this.panel35.TabIndex = 3;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(99, 3);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(152, 22);
            this.txtPhoneNumber.TabIndex = 1;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(2, 8);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(67, 15);
            this.label19.TabIndex = 0;
            this.label19.Text = "Điện thoại:";
            // 
            // panel36
            // 
            this.panel36.Controls.Add(this.txtNameCustomer);
            this.panel36.Controls.Add(this.label20);
            this.panel36.Location = new System.Drawing.Point(67, 21);
            this.panel36.Name = "panel36";
            this.panel36.Size = new System.Drawing.Size(251, 39);
            this.panel36.TabIndex = 1;
            // 
            // txtNameCustomer
            // 
            this.txtNameCustomer.Location = new System.Drawing.Point(99, 8);
            this.txtNameCustomer.Name = "txtNameCustomer";
            this.txtNameCustomer.Size = new System.Drawing.Size(152, 22);
            this.txtNameCustomer.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(2, 8);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(98, 15);
            this.label20.TabIndex = 0;
            this.label20.Text = "Tên Khách hàng:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvBillInfoExport);
            this.panel1.Location = new System.Drawing.Point(12, 141);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 362);
            this.panel1.TabIndex = 2;
            // 
            // dgvBillInfoExport
            // 
            this.dgvBillInfoExport.AllowUserToAddRows = false;
            this.dgvBillInfoExport.AllowUserToDeleteRows = false;
            this.dgvBillInfoExport.AllowUserToOrderColumns = true;
            this.dgvBillInfoExport.AllowUserToResizeColumns = false;
            this.dgvBillInfoExport.AllowUserToResizeRows = false;
            this.dgvBillInfoExport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBillInfoExport.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvBillInfoExport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBillInfoExport.Location = new System.Drawing.Point(3, 3);
            this.dgvBillInfoExport.Name = "dgvBillInfoExport";
            this.dgvBillInfoExport.ReadOnly = true;
            this.dgvBillInfoExport.RowHeadersVisible = false;
            this.dgvBillInfoExport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvBillInfoExport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBillInfoExport.Size = new System.Drawing.Size(638, 356);
            this.dgvBillInfoExport.TabIndex = 0;
            // 
            // fCheckOut
            // 
            this.AcceptButton = this.btnExportBill;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 515);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "fCheckOut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CheckOut";
            this.Load += new System.EventHandler(this.fCheckOut_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel33.ResumeLayout(false);
            this.panel33.PerformLayout();
            this.panel35.ResumeLayout(false);
            this.panel35.PerformLayout();
            this.panel36.ResumeLayout(false);
            this.panel36.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBillInfoExport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chbDept;
        private System.Windows.Forms.Panel panel33;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel35;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel36;
        private System.Windows.Forms.TextBox txtNameCustomer;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnExportBill;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvBillInfoExport;
        private System.Windows.Forms.BindingSource ExportReportBindingSource;
      //  private QLNhaHangDataSet QLNhaHangDataSet;
       // private QLNhaHangDataSetTableAdapters.ExportReportTableAdapter ExportReportTableAdapter;
    }
}
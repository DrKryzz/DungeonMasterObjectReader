
namespace DMObjectReader.Windows
{
    partial class HexEditor
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
            this.txtb_Contents = new System.Windows.Forms.TextBox();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Import = new System.Windows.Forms.Button();
            this.btn_Export = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtb_Contents
            // 
            this.txtb_Contents.AcceptsReturn = true;
            this.txtb_Contents.BackColor = System.Drawing.SystemColors.Window;
            this.txtb_Contents.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtb_Contents.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_Contents.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtb_Contents.Location = new System.Drawing.Point(9, 10);
            this.txtb_Contents.MaxLength = 0;
            this.txtb_Contents.Multiline = true;
            this.txtb_Contents.Name = "txtb_Contents";
            this.txtb_Contents.ReadOnly = true;
            this.txtb_Contents.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtb_Contents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtb_Contents.Size = new System.Drawing.Size(425, 391);
            this.txtb_Contents.TabIndex = 5;
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(9, 408);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(89, 27);
            this.btn_Ok.TabIndex = 6;
            this.btn_Ok.Text = "Ok";
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Import
            // 
            this.btn_Import.Location = new System.Drawing.Point(153, 408);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(89, 27);
            this.btn_Import.TabIndex = 7;
            this.btn_Import.Text = "&Import";
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // btn_Export
            // 
            this.btn_Export.Location = new System.Drawing.Point(249, 408);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(89, 27);
            this.btn_Export.TabIndex = 8;
            this.btn_Export.Text = "&Export";
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(345, 408);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(89, 27);
            this.btn_Cancel.TabIndex = 9;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // HexEditor
            // 
            this.AcceptButton = this.btn_Ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(442, 444);
            this.Controls.Add(this.txtb_Contents);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.btn_Import);
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.btn_Cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(181, 162);
            this.Name = "HexEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "HexEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtb_Contents;
        public System.Windows.Forms.Button btn_Ok;
        public System.Windows.Forms.Button btn_Import;
        public System.Windows.Forms.Button btn_Export;
        public System.Windows.Forms.Button btn_Cancel;
    }
}
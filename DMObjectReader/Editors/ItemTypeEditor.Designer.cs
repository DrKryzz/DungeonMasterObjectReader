namespace DMObjectReader.Editors
{
    partial class ItemTypeEditor
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
            this.components = new System.ComponentModel.Container();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ItemList = new System.Windows.Forms.ListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ItemName = new System.Windows.Forms.TextBox();
            this.Graphic = new System.Windows.Forms.Panel();
            this._ST_1 = new System.Windows.Forms.Label();
            this._ST_0 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.Ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ItemList
            // 
            this.ItemList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2});
            this.ItemList.FullRowSelect = true;
            this.ItemList.GridLines = true;
            this.ItemList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ItemList.HideSelection = false;
            this.ItemList.Location = new System.Drawing.Point(527, 25);
            this.ItemList.MultiSelect = false;
            this.ItemList.Name = "ItemList";
            this.ItemList.Size = new System.Drawing.Size(169, 202);
            this.ItemList.TabIndex = 7;
            this.ItemList.UseCompatibleStateImageBehavior = false;
            this.ItemList.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Width = 35;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Width = 110;
            // 
            // ItemName
            // 
            this.ItemName.AcceptsReturn = true;
            this.ItemName.BackColor = System.Drawing.SystemColors.Window;
            this.ItemName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ItemName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ItemName.Location = new System.Drawing.Point(527, 250);
            this.ItemName.MaxLength = 0;
            this.ItemName.Name = "ItemName";
            this.ItemName.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ItemName.Size = new System.Drawing.Size(169, 20);
            this.ItemName.TabIndex = 9;
            // 
            // Graphic
            // 
            this.Graphic.BackColor = System.Drawing.SystemColors.Control;
            this.Graphic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Graphic.Cursor = System.Windows.Forms.Cursors.Default;
            this.Graphic.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Graphic.Location = new System.Drawing.Point(7, 8);
            this.Graphic.Name = "Graphic";
            this.Graphic.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Graphic.Size = new System.Drawing.Size(516, 489);
            this.Graphic.TabIndex = 13;
            this.Graphic.TabStop = true;
            // 
            // _ST_1
            // 
            this._ST_1.Location = new System.Drawing.Point(527, 8);
            this._ST_1.Name = "_ST_1";
            this._ST_1.Size = new System.Drawing.Size(169, 18);
            this._ST_1.TabIndex = 12;
            this._ST_1.Text = "items with this graphic";
            this._ST_1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _ST_0
            // 
            this._ST_0.Location = new System.Drawing.Point(527, 233);
            this._ST_0.Name = "_ST_0";
            this._ST_0.Size = new System.Drawing.Size(169, 18);
            this._ST_0.TabIndex = 8;
            this._ST_0.Text = "item name:";
            this._ST_0.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(615, 502);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(89, 27);
            this.Cancel.TabIndex = 11;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(7, 502);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(89, 27);
            this.Ok.TabIndex = 10;
            this.Ok.Text = "Ok";
            this.Ok.UseVisualStyleBackColor = true;
            // 
            // ItemTypeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 536);
            this.Controls.Add(this.ItemList);
            this.Controls.Add(this.ItemName);
            this.Controls.Add(this.Graphic);
            this.Controls.Add(this._ST_1);
            this.Controls.Add(this._ST_0);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Ok);
            this.Name = "ItemTypeEditor";
            this.Text = "ItemTypeEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ToolTip ToolTip1;
        internal System.Windows.Forms.ListView ItemList;
        internal System.Windows.Forms.ColumnHeader ColumnHeader1;
        internal System.Windows.Forms.ColumnHeader ColumnHeader2;
        public System.Windows.Forms.TextBox ItemName;
        public System.Windows.Forms.Panel Graphic;
        public System.Windows.Forms.Label _ST_1;
        public System.Windows.Forms.Label _ST_0;
        internal System.Windows.Forms.Button Cancel;
        internal System.Windows.Forms.Button Ok;
    }
}
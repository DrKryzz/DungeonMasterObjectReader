using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMObjectReader
{
    public partial class ProgressWindow : Form
    {
        public ProgressWindow()
        {
            InitializeComponent();
        }

		public short current;
		public short max;
		public short lastupdate;
		public short updateinterval;


		public string Title 
		{ 
			get { return this.Text; } 
			set { this.Text = value; } 
		}

		private void ProgressWindow_Load(object eventSender,EventArgs e)
		{
			current = 0;
			max = 100;
			lastupdate = 0;
			updateinterval = 8;
		} //End Sub

		public void SetCurrent(short c)
		{
			if (c >= lastupdate + updateinterval || current == max ) 
			{
				ProgressA.Value = c;
				lastupdate = c;
				Refresh();
			}
		} //End Sub

		public void SetCurrent(int c)
		{
			if (c >= lastupdate + updateinterval || current == max)
			{
				ProgressA.Value = c;
				lastupdate = (short)c;
				Refresh();
			}
		} //End Sub

		public void SetMax(short m)
		{
			max = m;
			ProgressA.Maximum = m;
			lastupdate = 0;
		} //End Sub

		public void SetMax(int m)
		{
			max = (short)m;
			ProgressA.Maximum = m;
			lastupdate = 0;
		} //End Sub
		private void ProgressWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			switch (e.CloseReason)
			{
				case CloseReason.UserClosing:
					Hide();
					e.Cancel = true;
					return;
				default:
					return;
			}
		} //End Sub
	}
}

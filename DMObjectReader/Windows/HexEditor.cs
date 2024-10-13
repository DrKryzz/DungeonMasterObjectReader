using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMObjectReader.Windows
{
    public partial class HexEditor : Form
    {
        public short itemnum;
        public string ItemType;
        public MemoryStream itemdata;
        public int itemlen;

        App app;

        public HexEditor(App _app)
        {
            app = _app;
            InitializeComponent();

        }

        public void init()
        {
            ItemType = app.mapfile.gettyperenamed(itemnum);
            itemdata = app.graphics.getgitemdata(itemnum);
            itemlen = app.graphics.getgitemsize(itemnum);
            InitContents();
        }

        public void InitContents()
        {
            int i;
            string s = "";
            int masked; //could also be ushort
            byte data;

            masked = 65536; // the int variable type is 16-bits in your chosen compiler which gives it a range of -32768 to 32767, or 0 to 65535 if unsigned.
            s += masked.ToString("X4") + " :: ";
            for (i = 1; i <= itemlen; i++)
            {
                data = itemdata.ToArray()[i - 1]; //get next byte
                //if (data < 16) //Hex is 0 - 15
                //{
                //    s += "0";
                //}
                s += data.ToString("X2"); //double Hex output, reduce the need for above code in combination of .ToString("X");
                //masked = (i - 1) & 15; //mask out all but the lower bits
                masked = (i - 1) % 16; //switched to modulo for better readability
                if (masked == 15)
                {
                    s += Environment.NewLine;
                    masked = i;
                    masked += 65536;
                    s += masked.ToString("X4") + " :: ";
                }
                else
                {
                    s += " ";
                }
            }
            txtb_Contents.Text = s;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {

            string sfile;

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "Export";
                dlg.OverwritePrompt = true;
                dlg.Filter = "*.dat|*.dat|All Files (*.*)|*.*||";
                dlg.FileName = $"{App.ZeroStr(itemnum, 4)} {app.mapfile.getname(itemnum)}.dat";

                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                if (new DirectoryInfo(dlg.FileName).Name.Length == 0)
                {
                    return;
                }

                sfile = dlg.FileName;
            }

            app.graphics.gexport_raw(itemnum, sfile);
        }

        private void btn_Import_Click(object sender, EventArgs e)
        {

            string f;
            string sfile;

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                try
                {
                    dlg.Title = "Import";
                    dlg.Filter = "*.dat|*.dat|All Files (*.*)|*.*||";

                    dlg.FileName = $"{App.ZeroStr(itemnum, 4)} {app.mapfile.getname(itemnum)}.dat";

                    if (dlg.ShowDialog() != DialogResult.OK) return;

                    if (new DirectoryInfo(dlg.FileName).Name.Length == 0)
                    {
                        return;
                    }

                    f = Path.GetFileName(dlg.FileName);
                    sfile = dlg.FileName;

                    itemlen = app.graphics.import_raw(ref itemdata, sfile);
                }
                catch
                {
                    // Handle the error as needed
                }
            }

            InitContents();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {

            string newtype;

            app.graphics.setgitemdata(itemnum, itemdata);
            app.graphics.setgitemsize(itemnum, itemlen);

            newtype = app.mapfile.gettyperenamed(itemnum);
            if (newtype == "IMG1")
            {
                app.graphics.buildbmp(itemnum);
            }

            this.Close();
        }
    }
}

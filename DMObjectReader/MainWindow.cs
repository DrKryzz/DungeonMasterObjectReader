using DMObjectReader.Helpers;
using DMObjectReader.Windows;
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

namespace DMObjectReader
{
    public partial class MainWindow : Form
    {
        App app;
        public string openfname;
        public MainWindow(App _app)
        {
            
            InitializeComponent();
            app = _app;
            //ADGE kör map read i Forms load, vi kör den här
            app.mapfile.Read();
        }

        public void Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Title = "Open";
            dlg.FileName = "";
            dlg.Filter = "*.dat|*.dat|All Files (*.*)|*.*";
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            if (string.IsNullOrEmpty(dlg.FileName))
            {
                return;
            }

            openfile(dlg.FileName);
        }

        public void openfile(string fname)
        {
            bool ok;
            app.graphics.bytes = (int)new FileInfo(fname).Length;

            app.graphics.file = fname;
            app.dlg_progresswindow.Text = "Loading graphics.dat...";
            app.dlg_progresswindow.updateinterval = 8;
            app.dlg_progresswindow.Show();

            ok = app.graphics.gread();
            if (!ok)
            {
                app.dlg_progresswindow.Hide();
                MessageBox.Show("Could not open file, not a valid atari graphics.dat format.");
                return;
            }

            openfname = fname;

            InitItemList();

            this.Save.Enabled = true;
            this.SaveAs.Enabled = true;
            this.ExportCSBWinObjNames.Enabled = true;

            this.ItemTypes.Enabled = true;
            this.Attacks.Enabled = true;
            this.Combos.Enabled = true;
            this.Objects.Enabled = true;
            this.MonsterDrops.Enabled = true;
            this.Monsters.Enabled = true;
            this.Doors.Enabled = true;
            this.Spells.Enabled = true;
            this.Runes.Enabled = true;
            this.ImportManager.Enabled = true;

            this.PaletteModifierToDisplayItemsInActionAreaToolStripMenuItem.Enabled = true;
            this.IngamePalettesEditorToolStripMenuItem.Enabled = true;
            this.IngamePalettesEditorSubToolStripMenuItem.Enabled = true;

            this.GIMonsters.Enabled = true;
            this.GIItems.Enabled = true;
            this.GIButtons.Enabled = true;
            this.GIClickableButtons.Enabled = true;
            this.GIDoorDecorations.Enabled = true;
            this.GIFloorDecorations.Enabled = true;
            this.GIWallDecorations.Enabled = true;
            this.GIMissiles.Enabled = true;
            this.GICreatures.Enabled = true;
            this.GIFloorItems.Enabled = true;
            this.GITeleporterInfo.Enabled = true;
            this.GIGraphicRectInfo.Enabled = true;

            this.CheckAll.Enabled = true;
            this.CheckGraphicSizes.Enabled = true;
            this.CheckItemSizes.Enabled = true;
        }

        public void InitItemList()
        {
            app.dlg_progresswindow.Title = "Adding items to list...";
            app.dlg_progresswindow.SetCurrent(0);
            app.dlg_progresswindow.SetMax(app.graphics.numitems);
            app.dlg_progresswindow.updateinterval = 10;
            ItemList.Items.Clear();

            for (short i = 0; i < app.graphics.numitems; i++)
            {
                ListViewItem lvi = ItemList.Items.Add(App.ZeroStr(i, 4)); // 4 signs left 0 like PadLeft(nr,4) 0001, 0002 etc
                app.dlg_progresswindow.SetCurrent((int)i);
                lvi.SubItems.Add("0x" + App.ZeroHex(i, 4));
                lvi.SubItems.Add(app.mapfile.getname(i));
                lvi.SubItems.Add(app.graphics.getgitemsize(i).ToString());
                lvi.SubItems.Add(app.mapfile.gettyperenamed(i));

                if (app.mapfile.gettyperenamed(i) == "IMG1")
                {
                    lvi.SubItems.Add(App.Trim(app.graphics.GetGBitmapWidth(i).ToString()) + "x" + App.Trim(app.graphics.GetGBitmapHeight(i).ToString()));
                }
            }

            app.dlg_progresswindow.Hide();
        }

        private void ItemList_Click(object sender, EventArgs e)
        {
            StatusBar.Items[0].Text = "Item #" + App.ZeroStr(ItemList.FocusedItem.Index, 4) + " 0x" + App.ZeroHex((ItemList.FocusedItem.Index), 4);
            StatusBar.Items[1].Text = app.mapfile.getname(ItemList.FocusedItem.Index);
            if (app.mapfile.gettyperenamed(ItemList.FocusedItem.Index) == "IMG1")
            {
                StatusBar.Items[2].Text = App.Trim(App.Str(app.graphics.GetGBitmapWidth(ItemList.FocusedItem.Index))) + "x" + App.Trim(App.Str(app.graphics.GetGBitmapHeight(ItemList.FocusedItem.Index)));
            }
            else
            {
                StatusBar.Items[2].Text = "";
            }
        }

        //TODO: IMPLEMENT
        private void ItemList_DoubleClick(object sender, EventArgs e)
        {
            //short itemnum;
            //string ItemType;
            //TextEditor dlg_texteditor;
            //HexEditor dlg_hexeditor;
            //GraphicEditor dlg_graphiceditor;

            //itemnum = ItemList.FocusedItem.Index;

            //ItemType = app.mapfile.gettyperenamed(itemnum);
            //if (ItemType == "TXT1")
            //{
            //    dlg_texteditor = new TextEditor();
            //    dlg_texteditor.itemnum = itemnum;
            //    dlg_texteditor.init();
            //    dlg_texteditor.Show();
            //}
            //else if (ItemType == "IMG1")
            //{
            //    dlg_graphiceditor = new GraphicEditor();
            //    dlg_graphiceditor.itemnum = itemnum;
            //    dlg_graphiceditor.Show();
            //    dlg_graphiceditor.init();
            //    dlg_graphiceditor.Cancel.Focus(); //stops crashes somehow.
            //}
            //else
            //{
            //    dlg_hexeditor = new HexEditor();
            //    dlg_hexeditor.itemnum = itemnum;
            //    dlg_hexeditor.init();
            //    dlg_hexeditor.Show();
            //}
        }

        private void ItemList_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)13: // vbCr
                    ItemList_DoubleClick(sender, e);
                    break;
                case (char)32:
                    ItemList.ContextMenuStrip.Show(ItemList, 0, 0);
                    break;
                default:
                    break;
            }
        }

        private void ExportIMG1_Click(object sender, EventArgs e)
        {
            int itemnum;

            itemnum = ItemList.FocusedItem.Index;
            string sfile = "";
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "Export IMG";
                dlg.OverwritePrompt = true;
                dlg.Filter = "*.bmp|*.bmp|All Files (*.*)|*.*||";
                dlg.FileName = App.CStr(App.ZeroStr(itemnum, 4) + " " + app.mapfile.getname(itemnum) + (".bmp"));
                try
                {


                    if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    {
                        return;
                    }

                    if (App.Len((new System.IO.DirectoryInfo(dlg.FileName)).Name) == 0)
                    {
                        return;
                    }
                    sfile = dlg.FileName;
                }
                catch (Exception ex)
                {

                }

            }
            app.graphics.kkspec_gexport_img1_type2(itemnum, sfile, 0);

        }

        private void ExportSND1_Click(object sender, EventArgs e)
        {
            int itemnum;

            itemnum = ItemList.FocusedItem.Index;
            string sfile = "";
            //string sck_fil = "";
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "Export SND";
                dlg.OverwritePrompt = true;
                dlg.Filter = "*.wav|*.wav|All Files (*.*)|*.*||";
                dlg.FileName = App.CStr(App.ZeroStr(itemnum, 4) + " " + app.mapfile.getname(itemnum) + (".wav"));
                try
                {


                    if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    {
                        return;
                    }

                    if (App.Len((new System.IO.DirectoryInfo(dlg.FileName)).Name) == 0)
                    {
                        return;
                    }
                    sfile = dlg.FileName;
                    //sck_fil = dlg.FileName + "_sck.wav";
                }
                catch (Exception ex)
                {

                }

            }


            ////SCK stuff
            //DataSND1 sND1 = new DataSND1(new MapItem() { }, 1);
            //sND1.Decode(app.graphics.GetItemdata(itemnum).ToArray());
            //sND1.ToWAV(new FileInfo(sck_fil));

            //my stuff
            SOUND snd = new SOUND(app.graphics.GetItemdata(itemnum));
            byte[] arr = snd.GetPCM8BitMono();
            byte[] header = WAV.GetHeader(arr.Length, 6000L, 8, 1, 1);
            

            //we need the header info also
            System.IO.File.WriteAllBytes(sfile, header.Join(arr));


            //check for likeness
            //byte[] decode_arr = sND1.GetDecodedByteArray();

            //int lowest_length = arr.Length <= decode_arr.Length ? arr.Length : decode_arr.Length;
            //for(int i = 0; i < lowest_length; i++)
            //{
            //    if(arr[i] != decode_arr[i])
            //    {
            //        Console.WriteLine($"Difference {arr[i]} != {decode_arr[i]} on index {i}");
            //    }
            //}
        }

        public void ExportRAW_Click(object sender, EventArgs e)
        {
            int itemnum;

            itemnum = ItemList.FocusedItem.Index;
            string sfile;

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                try
                {
                    dlg.Title = "Export RAW";
                    dlg.OverwritePrompt = true;
                    dlg.Filter = "*.dat|*.dat|All Files (*.*)|*.*";
                    dlg.FileName = App.ZeroStr(itemnum, 4) + " " + app.mapfile.getname(itemnum) + ".dat";
                    if (dlg.ShowDialog() != DialogResult.OK) throw new Exception();
                    if (string.IsNullOrEmpty(dlg.FileName))
                    {
                        return;
                    }
                    sfile = dlg.FileName;

                    app.graphics.gexport_raw(itemnum, sfile);
                }
                catch
                {
                    // Handle cancellation or error
                }
            }
        }

        public void ImportRAW_Click(object sender, EventArgs e)
        {
            int itemnum;
            itemnum = ItemList.FocusedItem.Index;
            string sfile;

            OpenFileDialog dlg = new OpenFileDialog();
            try
            {
                dlg.Title = "Import RAW";
                dlg.Filter = "*.dat|*.dat|All Files (*.*)|*.*||";
                dlg.FileName = $"{App.ZeroStr(itemnum, 4)} {app.mapfile.getname(itemnum)}.dat";
                if (dlg.ShowDialog() != DialogResult.OK) throw new Exception();
                if (string.IsNullOrEmpty(dlg.FileName))
                {
                    return;
                }
                sfile = dlg.FileName;
            }
            catch
            {
                return;
            }

            MemoryStream newstr = new MemoryStream();
            int newlen;
            string newtype;

            newlen = app.graphics.import_raw(ref newstr, sfile);

            newtype = app.mapfile.gettyperenamed(itemnum);
            app.graphics.setgitemdata(itemnum, newstr);

            app.graphics.setgitemsize(itemnum, newlen);
            if (newtype == "IMG1")
            {
                app.graphics.buildbmp(itemnum);
            }

            if (itemnum == 556)
            {
                app.graphics.f556.itemdata = newstr;
                app.graphics.f556.itemlen = (short)newlen;
                app.graphics.f556.read();
            }
            else if (itemnum == 558)
            {
                app.graphics.f558.itemdata = newstr;
                app.graphics.f558.itemloc = 0;
                app.graphics.f558.read();
            }
            else if (itemnum == 559)
            {
                app.graphics.f559.itemdata = newstr;
                app.graphics.f559.itemloc = 0;
                app.graphics.f559.read();
            }
            else if (itemnum == 560)
            {
                app.graphics.f560.itemdata = newstr;
                app.graphics.f560.itemloc = 0;
                app.graphics.f560.read();
            }
            else if (itemnum == 561)
            {
                app.graphics.f561.itemdata = newstr;
                app.graphics.f561.itemloc = 0;
                app.graphics.f561.read();
            }
            else if (itemnum == 562)
            {
                app.graphics.f562.itemdata = newstr;
                app.graphics.f562.read();
            }   

        }

        private void ViewasRAW_Click(object sender, EventArgs e)
        {
            int itemnum;
            HexEditor dlg_hexeditor;
            itemnum = ItemList.FocusedItem.Index;
            dlg_hexeditor = new HexEditor(app);
            dlg_hexeditor.itemnum = (short)itemnum;
            dlg_hexeditor.init();
            dlg_hexeditor.Show();
        }

        private void ItemTypes_Click(object sender, EventArgs e)
        {
            app.dlg_itemtypeeditor.init();
            app.dlg_itemtypeeditor.Show();
        }
    }
}

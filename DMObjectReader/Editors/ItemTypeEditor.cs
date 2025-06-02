using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DMObjectReader.Editors
{
    public partial class ItemTypeEditor : Form
    {
        public short selecteditem;
        private string[] objstrings; // = new string[200]; //orig 200 but f556.objstrings.Length is 201
        App App;
        Panel SelectBox;
        public ItemTypeEditor(App _app)
        {

            InitializeComponent();
            App = _app;
            objstrings = new string[App.graphics.f556.objstrings.Length];

            // SelectBox som en Panel
            SelectBox = new Panel
            {
                Location = new Point(0, 0),
                Name = "SelectBox",
                Size = new Size(32, 32),
                BackColor = Color.Transparent // simulera transparent bakgrund
            };

            // Rita cyan border med Paint
            SelectBox.Paint += (sender, e) =>
            {
                int borderWidth = 2;
                Color borderColor = Color.Cyan;

                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    e.Graphics.DrawRectangle(pen,
                        borderWidth / 2,
                        borderWidth / 2,
                        SelectBox.Width - borderWidth,
                        SelectBox.Height - borderWidth);
                }
            };

            // Lägg till Panelen i formuläret
            this.Graphic.Controls.Add(SelectBox);

            //rita ut bakgrundsbilden
            App.graphics.curPalette = (App.graphics.gPALETTE_MAIN);
            Bitmap offscreen = new Bitmap(Graphic.ClientSize.Width, Graphic.ClientSize.Height);
            using (Graphics cv = Graphics.FromImage(offscreen))
            {
                App.graphics.stretchbmpby(cv, 42, 0, 0, 0, 0, 200, 200);
                App.graphics.stretchbmpby(cv, 43, 0, 64, 0, 0, 200, 200);
                App.graphics.stretchbmpby(cv, 44, 0, 128, 0, 0, 200, 200);
                App.graphics.stretchbmpby(cv, 45, 0, 192, 0, 0, 200, 200);
                App.graphics.stretchbmpby(cv, 46, 0, 256, 0, 0, 200, 200);
                App.graphics.stretchbmpby(cv, 47, 0, 320, 0, 0, 200, 200);
                App.graphics.stretchbmpby(cv, 48, 0, 384, 0, 0, 200, 200);
            }

            Graphic.BackgroundImage = offscreen;
            Graphic.BackgroundImageLayout = ImageLayout.None;


        }

        public void init()
        {
            
            for(int i = 0; i < App.graphics.f556.objstrings.Length; i++)
            {
                objstrings[i] = App.graphics.f556.objstrings[i];
            }

            SelectItem(0);
        }

        void SelectItem(int num)
        {
            SelectItem((short)num);
        }

        void SelectItem(short num)
        {
            short X;
            short Y;

            selecteditem = (short)(num + 1);

            // Maskera de 4 lägsta bitarna (X-position)
            X = (short)(num & 0xF);
            // Beräkna Y-position
            Y = (short)((num - X) / 16);

            SelectBox.Left = X * 32;
            SelectBox.Top = Y * 32;

            ItemName.Text = objstrings[num + 1];
            ItemName.SelectionStart = 0;
            ItemName.SelectionLength = ItemName.Text.Length;
            //ItemName.Focus();  // (kommenterad)

            short i;
            short j = 0;
            ItemList.Items.Clear();

            for (i = 1; i < App.graphics.f559.objs.Length; i++) //original i <= 180
            {
                if (App.graphics.f559.objs[i].ItemType == num)
                {
                    var item = ItemList.Items.Add(App.ZeroStr(i, 2));
                    item.SubItems.Add(objstrings[num + 1]);
                    j++;
                }
            }

            //ItemName.Focus();  // (kommenterad)
        }

        private void Graphic_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X;
                int y = e.Y;

                // Snap to 32x32 grid
                int cx = x - (x % 32);
                int cy = y - (y % 32);

                // Convert to tile index
                int col = cx / 32;
                int row = cy / 32;

                int itemnum = col + (row * 16);

                if (itemnum < 201)
                {
                    SelectItem(itemnum);
                }

                // För debugging (om du vill):
                // MessageBox.Show($"MouseDown on itemnum: {itemnum}");
            }
        }
    }
}

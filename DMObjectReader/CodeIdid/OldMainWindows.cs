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
    public partial class OldMainWindows : Form
    {
        private int[] gitemsizes;
        private int[] gitemsizescompressed;
        private MemoryStream[] gitemdatas;
        public MemoryStream itemdata;

        public OldMainWindows()
        {
            InitializeComponent();
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            lv_objects.Items.Clear();
            using (FileStream dm_graphics = File.OpenRead(txtb_path.Text))
            {
                //get the nr of items in graphics file
                int number_of_items = gread_int(dm_graphics);

                //setup items length for the arrays
                setsize(number_of_items);

                for (int i = 0; i < number_of_items; i++)
                {
                    gitemsizescompressed[i] = gread_int(dm_graphics);
                }
                for (int i = 0; i < number_of_items; i++)
                {
                    gitemsizes[i] = gread_int(dm_graphics);
                }

                //for (int i = 0, loopTo = number_of_items - 1; i <= loopTo; i++)
                for (int i = 0; i < number_of_items; i++)
                {
                    // For j = 1 To itemsizes(i)
                    // items(i, j) = read_byte
                    // Next
                    //dlg_progresswindow.setcurrent(i);
                    //dlg_progresswindow.Title.Text = "Item #" + App.zerostr(i, 4);
                    // dlg_progresswindow.Refresh
                    gitemdatas[i] = new MemoryStream(gread_bytes(gitemsizescompressed[i], dm_graphics));

                    //extract raw file as it is baked into graphics.dat
                    File.WriteAllBytes($"{i}_extracted_raw.dat",gitemdatas[i].ToArray());
                    if (gitemsizes[i] != gitemsizescompressed[i])
                    {

                        App.dprint("decompressing item #" + (i).ToString());
                        Utils util = new Utils();
                        util.itemdata = gitemdatas[i];
                        gitemdatas[i] = util.LZWUncompress();

                        //extract raw uncompressed file as it is baked into graphics.dat
                        File.WriteAllBytes($"{i}_extracted_uncompressed_raw.dat", gitemdatas[i].ToArray());
                        //middle ground
                        //File.WriteAllBytes(i.ToString() + ".dat", gitemdatas[i].ToArray());
                        //Debug.Assert(gitemdatas(i).Length == gitemsizes(i), Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("The item #", i), ", LZW decode may have failed. The expected size (")) + gitemsizes(i) + ") doesn't match actual decoded size(" + gitemdatas(i).Length + ")");
                    }
                }

                InitItemList();


            }
        }

        public void InitItemList()
        {
            //dlg_progresswindow.Title.Text = "Adding items to list...";
            //dlg_progresswindow.SetCurrent(0);
            //dlg_progresswindow.SetMax(graphics.NumItems);
            //dlg_progresswindow.UpdateInterval = 10;
            //lv_objects.Items.Clear();

            for (short i = 0; i < gitemdatas.Length; i++)
            {
                ListViewItem lvi = lv_objects.Items.Add(ZeroStr(i, 4)); // 4 signs left 0 like PadLeft(nr,4) 0001, 0002 etc
                //dlg_progresswindow.SetCurrent(i);
                lvi.SubItems.Add("0x" + ZeroHex(i, 4));
                //lvi.SubItems.Add(mapfile.GetName(i));
                lvi.SubItems.Add(gitemsizes[i].ToString());
                //lvi.SubItems.Add(mapfile.GetTypeRenamed(i));

                gitemdatas[i].Position = 0L;
                int width = SreadWord(gitemdatas[i], 1);
                int height = SreadWord(gitemdatas[i], 3);
                if (i <= 532 || i == 548)
                {
                    lvi.SubItems.Add($"{width}x{height}");
                    lvi.SubItems.Add($"IMAGE");

                    //they are raw and need to be expanded/generated
                    //File.WriteAllBytes(i.ToString() + ".bmp", gitemdatas[i].ToArray());
                }
                else if(i <= 547 || i <= 555)
                {
                    lvi.SubItems.Add($"");
                    lvi.SubItems.Add($"SOUND");

                    //they are raw and need to be expanded/generated
                    //File.WriteAllBytes(i.ToString() + ".wav", gitemdatas[i].ToArray());
                }
                else if(i == 556)
                {
                    lvi.SubItems.Add($"");
                    lvi.SubItems.Add($"TXT");
                }
                else if (i == 557)
                {
                    lvi.SubItems.Add($"");
                    lvi.SubItems.Add($"FNT");
                }
                else
                {
                    lvi.SubItems.Add($"");
                    lvi.SubItems.Add($"RAW");
                }


            }

            //dlg_progresswindow.Hide();
        }

        public string ZeroHex(short num, short count)
        {
            string zerohexRet = default;
            short i;
            zerohexRet = num.ToString("X2");
            i = (short)zerohexRet.Length;
            i = (short)(count - zerohexRet.Length);
            if (i > 0)
            {
                zerohexRet = new string('0', i) + zerohexRet;
            }

            return zerohexRet;
        }

        public string ZeroStr(int nr, int width)
        {
            return nr.ToString().PadLeft(width);
        }

        public byte[] gread_bytes(int length, Stream fnum)
        {
            return new BinaryReader(fnum).ReadBytes(length);
        }

        public byte gread_byte(Stream fnum)
        {
            return new BinaryReader(fnum).ReadByte();
        }

        /// <summary>
        /// Reads 2 bytes as a Word and returns the result as an int
        /// </summary>
        /// <param name="fnum"></param>
        /// <returns></returns>
        public int gread_int(Stream fnum)
        {
            //because of the little endian big endian i dm we read the least significant bit first?
            //could also read as [word] 2 bytes = 1 word and split in bit-shift
            byte b1;
            byte b2;
            b2 = gread_byte(fnum);
            b1 = gread_byte(fnum);

            int result = b2;
            result *= 256;
            result += b1;

            return result;
        }

        private void setsize(int num)
        {
            gitemsizes = new int[num];
            gitemsizescompressed = new int[num];
            gitemdatas = new MemoryStream[num];
            // gbitmaps = new Bitmap[num, 2];
            // gbitmapwidths = new int[num];
            // gbitmapheights = new int[num];
        }

        public byte SReadByte(MemoryStream data, int offset1Based)
        {
            data.Position = offset1Based - 1;
            byte result = new BinaryReader(data).ReadByte();
            return result;
        }

        public int SreadWord(MemoryStream data, int offset1Based)
        {
            int _sreadWord = SReadByte(data, offset1Based) * 256;
            _sreadWord += SReadByte(data, offset1Based + 1);
            return _sreadWord;
        }
    }
}

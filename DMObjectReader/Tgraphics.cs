using DMObjectReader.DTOs;
using DMObjectReader.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DMObjectReader
{
    public class Tgraphics
    {
        //public MainWindow mainwin;
        public int bytesread;
        public int bytes;
        public byte compressed;
        public byte endian; //true = little, false = big
        public byte checksum;
        public string file;
        public Boolean first;
        /*    'various files with data*/
        public file556 f556; // = new file556();
        public file558 f558; // = new file558();
        public file559 f559; // = new file559();
        public file560 f560; // = new file560();
        public file561 f561; // = new file561();
        public file562 f562; // = new file562();
        /*    'size of all the items in the graphics.dat file*/
        private int[] itemsizes = new int[801]; //800 orig
        private int[] itemsizescompressed = new int[801]; //800 orig
        /*    'the data of the items*/
        private MemoryStream[] itemstrs = new MemoryStream[802];//801 orig
        /*    'the standard color palette?*/
        private int[] colorarray = new int[18]; //17 orig
        /*    'the custom modified color palette*/
        private Color[] modifiedcolorarray = new Color[18]; //17 orig
        /*    'god this shit is gay*/
        private Image[,] itempics = new Image[802,6]; //801, 5 orig
        private Boolean[,] itempicsloaded = new Boolean[802,6]; //801,5 orig
        private int[] gitemsizes;
        private int[] gitemsizescompressed;
        private MemoryStream[] gitemdatas;
        /*    'Private gbitmaps(,) As Bitmap*/
        /*    'Private gbitmapwidths() As Integer*/
        /*    'Private gbitmapheights() As Integer*/
        private int gDC;
        private int gMaskDC;
        public int gMirrored;
        /*    'palette info*/
        public Color[,] gpalettes = new Color[16, 256]; //15,255 orig
        public Color[,] ginvertedpalettes = new Color[16, 256];//15, 256 orig
        public int gPALETTE_MAIN;
        public int gPALETTE_TITLE;
        public int gPALETTE_MAINMENU;
        public int gPALETTE_CREDITS;
        public int gPALETTE_CUSTOM;
        public int curPalette;
        public int numitems;

        App app;
        public Tgraphics(App _app)
        {
            app = _app;
            f556 = new file556(app);
            f558 = new file558(app);
            f559 = new file559(app);
            f560 = new file560(app);
            f561 = new file561(app);
            f562 = new file562(app);
        }

        public void setmirrored(int num)
        {
            if (num == 0)
            {
                gMirrored = 0;
            }
            else
            {
                gMirrored = 1;
            }
        } //End Sub
        public int GetGBitmapWidth(int num)
        {
            return gbitmapwidths(num);
        } //End function
        public int GetGBitmapHeight(int num)
        {
            return gbitmapheights(num);
        } //End function

        public int gbitmapwidths(int itemnum)
        {
            return sread_word(gitemdatas[itemnum], 1);
        }
        public int gbitmapheights(int itemnum)
        {
            return sread_word(gitemdatas[itemnum], 3);
        }
        void setpalette(int palettenum, int colornum, Color color)
        {
            gpalettes[palettenum, colornum] = color;
            ginvertedpalettes[palettenum, colornum] = Color.FromArgb(color.B, color.G, color.R);
        } //End Sub
        public Color getpalette(int palettenum, int colornum)
        {
            return gpalettes[palettenum, colornum];
        } //End function
        public Color getinvertedpalette(int palettenum, int colornum)
        {
            return ginvertedpalettes[palettenum, colornum];
        } //End function
        public void copypalette(int src, int dst)
        {
            int i;
            for (i = 0; i <= 255; i++)
            {
                gpalettes[dst, i] = gpalettes[src, i];
                ginvertedpalettes[dst, i] = ginvertedpalettes[src, i];
            }
        } //End Sub
        public void build_invertedpalettes()
        {
            /*        'we have to build an inverted set as well, dunno why*/
            int i;
            int j;
            Color col;
            for (i = 0; i <= 15; i++)
            {
                for (j = 0; j <= 255; j++)
                {
                    col = gpalettes[i, j];
                    ginvertedpalettes[i, j] = Color.FromArgb(col.B, col.G, col.R);
                }
            }
        } //End Sub
        public void build_palettes()
        {
            int i;
            int palettenum;
            /*        'build the main palette, as 0*/
            palettenum = 0;
            gPALETTE_MAIN = palettenum;

            for (i = 0; i <= 15; i++)
            {
                gpalettes[palettenum, i] = MyWordClrUt.WordClrTo1Color(f562.Palette552[1].color[1 + i]);
            }

            for (i = 16; i <= 255; i++)
            {
                gpalettes[palettenum, i] = Color.Black; //set the rest to black
            }
            /*        'title screen palette*/
            palettenum = 1;
            gPALETTE_TITLE = palettenum;
            gpalettes[palettenum, 0] = Color.FromArgb(0, 0, 42);
            gpalettes[palettenum, 1] = Color.FromArgb(109, 109, 109);
            gpalettes[palettenum, 2] = Color.FromArgb(145, 145, 145);
            gpalettes[palettenum, 3] = Color.FromArgb(181, 140, 33);
            gpalettes[palettenum, 4] = Color.FromArgb(140, 66, 33);
            gpalettes[palettenum, 5] = Color.FromArgb(214, 181, 33);
            gpalettes[palettenum, 6] = Color.FromArgb(181, 107, 33);
            gpalettes[palettenum, 7] = Color.FromArgb(0, 0, 66);
            gpalettes[palettenum, 8] = Color.FromArgb(255, 255, 66);
            gpalettes[palettenum, 9] = Color.FromArgb(0, 0, 66);
            gpalettes[palettenum, 10] = Color.FromArgb(0, 0, 0);
            gpalettes[palettenum, 11] = Color.FromArgb(0, 0, 66);
            gpalettes[palettenum, 12] = Color.FromArgb(255, 0, 0);
            gpalettes[palettenum, 13] = Color.FromArgb(0, 0, 66);
            gpalettes[palettenum, 14] = Color.FromArgb(0, 0, 66);
            gpalettes[palettenum, 15] = Color.FromArgb(255, 255, 255);

            for (i = 16; i <= 255; i++)
            {
                gpalettes[palettenum, i] = Color.Black; //set the rest to black
            }
            /*        'Main menu pallette*/
            palettenum = 2;
            gPALETTE_MAINMENU = palettenum;

            for (i = 0; i <= 15; i++)
            {
                gpalettes[palettenum, i] = MyWordClrUt.WordClrTo1Color(f562.Palette360.color[1 + i]);
            }
            for (i = 16; i <= 255; i++)
            {
                gpalettes[palettenum, i] = Color.Black; //set the rest to black
            }
            /*        'credits pallette*/
            palettenum = 3;
            gPALETTE_CREDITS = palettenum;
            for (i = 0; i <= 15; i++)
            {
                gpalettes[palettenum, i] = MyWordClrUt.WordClrTo1Color(f562.Palette328.color[1 + i]);
            }
            for (i = 16; i <= 255; i++)
            {
                gpalettes[palettenum, i] = Color.FromArgb(0, 0, 0); //set the rest to black
            }
            palettenum = 4;
            gPALETTE_CUSTOM = palettenum;
            /*        'reserve the rest for custom palettes.*/
        } //End Sub

        public void setsize(int num)
        {
            /*ReDim gitemsizes(num -1)
        ReDim gitemsizescompressed(num -1)
        ReDim gitemdatas(num -1)*/
            //num-1 kan vara skillnad på c# och hur vbnet kör detta
            gitemsizes = new int[num];
            gitemsizescompressed = new int[num];
            gitemdatas = new MemoryStream[num];
        } //End Sub
        public bool gread()
        {
            int i;
            bool retval = false;
            using (FileStream f135 = File.OpenRead(file))
            {
                app.dlg_progresswindow.SetCurrent((0));
                app.dlg_progresswindow.updateinterval = 8;
                retval = false;
                numitems = App.gread_int(f135);
                if (numitems != 563)
                {
                    MessageBox.Show("CAUTION: Incorrect Dungeon size: " + numitems + " != 563");
                }
                app.dlg_progresswindow.SetMax((short)numitems);
                setsize(numitems);
                for (i = 0; i <= numitems - 1; i++)
                {
                    gitemsizescompressed[i] = App.gread_int(f135);
                }
                for (i = 0; i <= numitems - 1; i++)
                {
                    gitemsizes[i] = App.gread_int(f135);
                }
                /*            'ReDim items(563, highest)*/
                for (i = 0; i <= numitems - 1; i++)
                {

                    app.dlg_progresswindow.SetCurrent(i);
                    app.dlg_progresswindow.Title = "Item #" + App.ZeroStr(i, 4);
                    /*                'dlg_progresswindow.Refresh*/
                    gitemdatas[i] = new MemoryStream(App.gread_bytes(gitemsizescompressed[i], f135));
                    if (gitemsizes[i] != gitemsizescompressed[i])
                    {
                        App.dprint("decompressing item #" + i.ToString());
                        app.itemdata = gitemdatas[i];
                        gitemdatas[i] = app.LZWUncompress();
                        /*                    'middle ground*/
                        /*                    'System.IO.File.WriteAllBytes(i.ToString() + ".dat", gitemdatas(i).ToArray())*/

                        if(gitemdatas[i].Length != gitemsizes[i])
                        {
                            Console.WriteLine("The item #" + i + ", LZW decode may have failed. The expected size (" + gitemsizes[i] + ") does;n//t match actual decoded size(" + gitemdatas[i].Length + ")");
                        }
                        //Debug.Assert(gitemdatas[i].Length == gitemsizes[i], "The item #" + i + ", LZW decode may have failed. The expected size (" + gitemsizes[i] + ") does;n//t match actual decoded size(" & gitemdatas[i].Length + ")");
                    }
                }
            }//end using

            /*        'Food Armor Weapons*/
            app.dlg_progresswindow.SetCurrent((0));
            app.dlg_progresswindow.updateinterval = 1;
            app.dlg_progresswindow.SetMax((6));
            app.dlg_progresswindow.Title = "Loading file 556...";
            app.dlg_progresswindow.Refresh();
            f556.itemdata = gitemdatas[556]; //could be wrongly referenced if vb was starting on 1, original index 556
            f556.itemlen = (short)gitemsizes[556]; //could be wrongly referenced if vb was starting on 1, original index 556
            f556.read();

            /*        'Monsters and decorations*/
            app.dlg_progresswindow.SetCurrent((1));
            app.dlg_progresswindow.Title = "Loading file 558...";
            app.dlg_progresswindow.Refresh();
            f558.itemdata = gitemdatas[558]; //could be wrongly referenced if vb was starting on 1, original index 558
            f558.itemlen = gitemsizes[558];
            f558.read();

            /*        'Monsters weapons and armor*/
            app.dlg_progresswindow.SetCurrent((2));
            app.dlg_progresswindow.Title = "Loading file 559...";
            app.dlg_progresswindow.Refresh();
            f559.itemdata = gitemdatas[559]; //could be wrongly referenced if vb was starting on 1, original index 559
            f559.read();
            /*        'runes spells attacks*/
            app.dlg_progresswindow.SetCurrent((3));
            app.dlg_progresswindow.Title = "Loading file 560...";
            app.dlg_progresswindow.Refresh();
            f560.itemdata = gitemdatas[560]; //could be wrongly referenced if vb was starting on 1, original index 560
            f560.read();
            /*        'Buttons chests*/
            app.dlg_progresswindow.SetCurrent((4));
            app.dlg_progresswindow.Title = "Loading file 561...";
            app.dlg_progresswindow.Refresh();
            f561.itemdata = gitemdatas[561]; //could be wrongly referenced if vb was starting on 1, original index 561
            f561.read();
            /*        'Miscs sound effects?*/
            app.dlg_progresswindow.SetCurrent((5));
            app.dlg_progresswindow.Title = "Loading file 562...";
            app.dlg_progresswindow.Refresh();
            f562.itemdata = gitemdatas[562]; //could be wrongly referenced if vb was starting on 1, original index 562
            f562.read();
            retval = true;
            /*        'init_colorarray*/
            build_palettes();
            build_invertedpalettes();

            return retval;
        } //End function

        public void gsave()
        {
            int i;
            f556.update();
            f558.update();
            f559.update();
            f560.update();
            f561.update();
            f562.update();
            using (FileStream f135 = File.Create(file))
            {
                /*            '    write_swapword 563 '563 items... static*/
                write_swapword(numitems, f135);
                i = 0;
                for (i = 0; i <= numitems - 1; i++)
                {
                    write_swapword(gitemsizes[i], f135);
                }
                for (i = 0; i <= numitems - 1; i++)
                {
                    write_swapword(gitemsizes[i], f135);
                }
                /*            'ReDim items(numitems, highest)*/
                for (i = 0; i <= numitems - 1; i++)
                {
                    gitemdatas[i].WriteTo(f135);
                }
                f135.Close();
            }
        } //End Sub

        public byte gfindcolor(Color color)
        {
            /*        'find a color nearest to the color array*/
            short i;
            int closest;
            int closest_len = 0;
            int length;
            int b1, b2, g2, r2, r1, g1;
            int bd, rd, gd;
            Color l1;
            b2 = color.B;
            g2 = color.G;
            r2 = color.R;
            closest = -1;
            for (i = 0; i <= 15; i++)
            {
                /*            'l1 = modifiedcolorarray(i)*/
                l1 = gpalettes[curPalette, i];
                b1 = l1.B;
                g1 = l1.G;
                r1 = l1.R;
                rd = System.Math.Abs(r2 - r1);
                gd = System.Math.Abs(g2 - g1);
                bd = System.Math.Abs(b2 - b1);
                length = rd * rd + gd * gd + bd * bd;
                if (closest == -1)
                {
                    closest = i;
                    closest_len = length;
                }
                else
                {
                    if (length < closest_len)
                    {
                        closest = i;
                        closest_len = length;
                    }
                }
            }
            return (byte)closest;
        }
        public void read_bytes(short itemnum, int num, Stream f135)
        {
            itemstrs[itemnum] = new MemoryStream(new BinaryReader(f135).ReadBytes(num));
        }
        public byte read_byte(Stream f135)
        {
            bytesread = bytesread + 1;
            return new BinaryReader(f135).ReadByte();
        }
        public int read_int(Stream f135)
        {
            byte b1;
            byte b2;
            int ans = 0;
            if (endian == 1)
            { //little endian

                b1 = read_byte(f135);
                b2 = read_byte(f135);
            }
            else
            {
                b2 = read_byte(f135);
                b1 = read_byte(f135);
            }
            ans = b2;
            ans = ans * 256;

            ans = ans + b1;
            return ans;
        } //End function
        public byte sread_byte(MemoryStream data, int offset1based)
        {
            data.Position = offset1based - 1;
            return new BinaryReader(data).ReadByte();
        } //End function
        public int sread_word(MemoryStream data, int offset1based)
        {
            var ans = sread_byte(data, offset1based) * 0x100;
            return ans + sread_byte(data, offset1based + 1);
        } //End function

        public void swrite_byte(ref MemoryStream data, byte val_Renamed)
        {
            data.Position = data.Length;
            data.WriteByte(val_Renamed);
        } //End Sub

        public void swrite_word(ref MemoryStream data, int val_Renamed)
        {
            byte b1;
            byte b2;
            int l1;
            l1 = val_Renamed & 0xFF00;

            b1 = (byte)(l1 / 0x100);
            b2 = (byte)(val_Renamed & 0xFF);

            swrite_byte(ref data, b1);
            swrite_byte(ref data, b2);
        } //End Sub
        public void write_byte(ref byte num, Stream f135)
        {
            f135.WriteByte(num);
        } //End Sub

        public void write_byte(ref int num, Stream f135)
        {
            f135.WriteByte((byte)num);
        } //End Sub
        public void write_word(short num, Stream f135)
        {
            byte b1;
            byte b2;
            int l1;
            l1 = (num & 0xFF00);
            b1 = (byte)(l1 / 0x100);
            b2 = (byte)(num & 0xFF);
            write_byte(ref b2, f135);
            write_byte(ref b1, f135);
        } //End Sub
        public void write_swapword(int num, Stream f135)
        {
            byte b1;
            byte b2;
            int l1;
            l1 = (num & 0xFF00);
            b1 = (byte)(l1 / 0x100);
            b2 = (byte)(num & 0xFF);
            write_byte(ref b1, f135);
            write_byte(ref b2, f135);
        } //End Sub

        public void write_long(int num, Stream f135)
        {
            int b2, b1, b3;
            byte b4;
            int l1;

            l1 = num;
            l1 = l1 / 0x1000000;
            if (l1 < 0)
            {
                l1 = l1 + 256;
            }
            b1 = l1;
            l1 = num & 16711680;
            b2 = l1 / 0x10000;
            l1 = num & 65280;
            b3 = l1 / 0x100;
            b4 = (byte)(num & 255);
            write_byte(ref b4, f135);
            write_byte(ref b3, f135);
            write_byte(ref b2, f135);
            write_byte(ref b1, f135);
        } //End Sub
        public void write_swaplong(int num, Stream f135)
        {
            int b2, b1, b3;
            byte b4;
            int l1;
            l1 = num;
            l1 = l1 / 0x1000000;
            if (l1 < 0)
            {
                l1 = l1 + 256;
            }
        }
        public MemoryStream getgitemdata(int Index)
        {
            return gitemdatas[Index];
        } //End function
        public void setgitemdata(int Index, MemoryStream data)
        {
            gitemdatas[Index] = data;
        } //End Sub

        public int getgitemsize(int Index)
        {
            return gitemsizes[Index];
        } //End function
        public void setgitemsize(int Index, int num)
        {
            gitemsizes[Index] = num;
        } //End Sub

        public int getgitemsizecompressed(int Index)
        {
            return gitemsizescompressed[Index];
        } //End function
        public void setgitemsizecompressed(int Index, int num)
        {
            gitemsizescompressed[Index] = num;
        } //End Sub

        public void gExport(short itemnum, string fname)
        {
            string ItemType;
            ItemType = app.mapfile.gettyperenamed(itemnum);
            if (ItemType == "IMG1")
            {
                short mirrored = 0; //false 
                gexport_img1(itemnum, fname, ref mirrored); //true = 1, false = 0
            }
            else if (ItemType == "TXT1")
            {
                gexport_txt1(itemnum, fname);
            }
            else
            {
                gexport_raw(itemnum, fname);
            }
        } //End Sub

        public void gexport_txt1(int itemnum, string fname)
        {
        } //End Sub
        public void gexport_raw(int itemnum, string fname)
        {
            File.WriteAllBytes(fname, gitemdatas[itemnum].ToArray());
        } //End Sub

        public int import_raw(ref MemoryStream itemdata, string fname)
        {
            var ans = new FileInfo(fname).Length;
            itemdata = new MemoryStream(File.ReadAllBytes(fname));
            return (int)ans;
        } //End function

        public short CShort(int val)
        {
            return (short)val;
        }

        public MemoryStream encode_img1(ref byte[] pixelarray, int width, int height)
        {
            var ans = new MemoryStream();
            swrite_word(ref ans, width);
            swrite_word(ref ans, height);
            int j, i;
            int n;
            byte b;
            int max;
            int pixelcount_same = 0;
            int pixelcount_line = 0;
            int pixelcolor;
            int nextpixel = 0;
            byte[] nibblearray = new byte[800000];
            max = width;
            max = max * height;
            n = 1;
            for (i = 1; i <= max; i++)
            {
                pixelcolor = pixelarray[i];
                if (pixelarray[i] == 16) //transparent pixel(s)
                {
                    for (j = i; j <= max; j++)
                    {
                        if (pixelarray[j] != pixelcolor || j == max)
                        {
                            pixelcount_same = j - i;
                            j = max + 1;
                        }
                    } //end Next

                    if (pixelcount_same <= 16)
                    {
                        nibblearray[n] = 10;
                        n = n + 1;
                        nibblearray[n] = (byte)pixelcount_same;
                        n = n + 1;
                    }
                    else if (pixelcount_same <= 29)
                    {
                        nibblearray[n] = 14;
                        n = n + 1;
                        nibblearray[n] = (byte)(pixelcount_same - 17);
                        n = n + 1;
                    }
                    else if (pixelcount_same <= 256)
                    {
                        nibblearray[n] = 14;
                        n = n + 1;
                        nibblearray[n] = 13;
                        n = n + 1;
                        nibblearray[n] = (byte)CShort(((pixelcount_same - 1) & 240) / 16);
                        n = n + 1;
                        nibblearray[n] = (byte)((pixelcount_same - 1) & 15);
                        n = n + 1;
                    }
                    else if (pixelcount_same <= 512)
                    {
                        nibblearray[n] = 14;
                        n = n + 1;
                        nibblearray[n] = 14;
                        n = n + 1;
                        nibblearray[n] = (byte)CShort(((pixelcount_same - 257) & 240) / 16);
                        n = n + 1;
                        nibblearray[n] = (byte)((pixelcount_same - 257) & 15);
                        n = n + 1;
                    }
                    else
                    {
                        nibblearray[n] = 14;
                        n = n + 1;
                        nibblearray[n] = 15;
                        n = n + 1;
                        if (pixelcount_same > 65536)
                        {
                            pixelcount_same = 65536;
                        }
                        nibblearray[n] = (byte)CShort(((pixelcount_same - 1) & 61440) / 4096);
                        n = n + 1;
                        nibblearray[n] = (byte)CShort(((pixelcount_same - 1) & 3840) / 256);
                        n = n + 1;
                        nibblearray[n] = (byte)CShort(((pixelcount_same - 1) & 240) / 16);
                        n = n + 1;
                        nibblearray[n] = (byte)((pixelcount_same - 1) & 15);
                        n = n + 1;
                    }

                    i = i + pixelcount_same - 1;
                }//end of transparent
                else
                {
                    pixelcount_same = 1;
                    for (j = i; j <= max; j++)
                    {
                        if (pixelarray[j] != pixelcolor || j == max)
                        {
                            pixelcount_same = j - i;
                            j = max + 1;
                        }
                    } //end Next
                    if (pixelcount_same < 1)
                    {
                        pixelcount_same = 1;
                    }

                    pixelcount_line = 0;

                    if ((i - 1) > width)
                    { //check for copying from above
                        for (j = i; j <= max - 1; j++)
                        {
                            if (pixelarray[j] != pixelarray[j - width] || j == (max - 1))
                            {
                                pixelcount_line = j - i;
                                j = max + 1;
                            }
                        } //end Next
                    }

                    if (pixelcount_line > pixelcount_same)
                    {
                        /*                    'do copybits from above line*/

                        nextpixel = pixelarray[i + pixelcount_line];
                        if (pixelcount_line <= 256)
                        {
                            nibblearray[n] = 11;
                            n = n + 1;
                            nibblearray[n] = (byte)nextpixel;
                            n = n + 1;
                            nibblearray[n] = (byte)CShort(((pixelcount_line - 1) & 240) / 16);
                            n = n + 1;
                            nibblearray[n] = (byte)((pixelcount_line - 1) & 15);
                            n = n + 1;
                        }
                        else
                        {
                            nibblearray[n] = 15;
                            n = n + 1;
                            nibblearray[n] = (byte)nextpixel;
                            n = n + 1;
                            if (pixelcount_line > 65536)
                            {
                                pixelcount_line = 65536;
                            }
                            nibblearray[n] = (byte)CShort(((pixelcount_line - 1) & 61440) / 4096);
                            n = n + 1;
                            nibblearray[n] = (byte)CShort(((pixelcount_line - 1) & 3840) / 256);
                            n = n + 1;
                            nibblearray[n] = (byte)CShort(((pixelcount_line - 1) & 240) / 16);
                            n = n + 1;
                            nibblearray[n] = (byte)((pixelcount_line - 1) & 15);
                            n = n + 1;
                        }

                        i = i + pixelcount_line;
                    }
                    else
                    {
                        if (pixelcount_same <= 8)
                        {
                            nibblearray[n] = (byte)(pixelcount_same - 1);
                            n = n + 1;
                            nibblearray[n] = (byte)pixelcolor;
                            n = n + 1;
                        }
                        else if (pixelcount_same <= 256)
                        {
                            nibblearray[n] = 8;
                            n = n + 1;
                            nibblearray[n] = (byte)pixelcolor;
                            n = n + 1;
                            nibblearray[n] = (byte)CShort(((pixelcount_same - 1) & 240) / 16);
                            n = n + 1;
                            nibblearray[n] = (byte)((pixelcount_same - 1) & 15);
                            n = n + 1;
                        }
                        else
                        {
                            nibblearray[n] = 12;
                            n = n + 1;
                            nibblearray[n] = (byte)pixelcolor;
                            n = n + 1;
                            if (pixelcount_same > 65536)
                            {
                                pixelcount_same = 65536;
                            }
                            nibblearray[n] = (byte)CShort(((pixelcount_same - 1) & 61440) / 4096);
                            n = n + 1;
                            nibblearray[n] = (byte)CShort(((pixelcount_same - 1) & 3840) / 256);
                            n = n + 1;
                            nibblearray[n] = (byte)CShort(((pixelcount_same - 1) & 240) / 16);
                            n = n + 1;
                            nibblearray[n] = (byte)((pixelcount_same - 1) & 15);
                            n = n + 1;
                        }

                        i = i + pixelcount_same - 1;
                    }
                    /*                'swrite_byte encode_img1, pixelarray(i)*/
                }
            } //end Next
            n = n - 1;
            for (i = 1; i <= n; i++)
            {
                b = (byte)(nibblearray[i] * 16);
                i = i + 1;
                b = (byte)(b + nibblearray[i]);
                swrite_byte(ref ans, b);
            } //end Next

            return ans;
        } //End function

        public void gexport_img1(int itemnum, string fname, ref short mirrored)
        {
            /*        ' if mirrored > 0 then mirror it, else dont... dont use that old shit*/
            /*        'construct nibblearray*/
            byte[] nibbles;
            int nibblelen;
            byte[] pixelarray;
            int pwidth;
            int pheight;
            int i;
            int j;
            int p;
            int b;
            int n4, n2, n1, n3, n5;
            byte n6;
            int itemlen;
            itemlen = gitemsizes[itemnum];
            nibbles = new byte[itemlen * 2];
            pwidth = sread_word(gitemdatas[itemnum], 1);
            pheight = sread_word(gitemdatas[itemnum], 3);
            pixelarray = new byte[pwidth * pheight + 2048];

            j = 1;
            for (i = 5; i <= itemlen; i++)
            {
                b = sread_byte(gitemdatas[itemnum], i);
                nibbles[j] = (byte)((b & 0xF0) >> 4); //high nibble orig = (byte)CShort((b & 0xF0) / 16)
                j = j + 1;
                nibbles[j] = (byte)(b & 0xF); //low nibble
                j = j + 1;
            } //end Next
            nibblelen = j - 1;
            p = 1;
            for (i = 1; i <= nibblelen; i++)
            {
                n1 = nibbles[i-1]; //orig i

                i = i + 1;
                n2 = nibbles[i-1]; //orig i

                if (n1 <= 7)
                {

                    n1 = n1 + 1;
                    for (j = 1; j <= n1; j++)
                    {
                        pixelarray[p] = (byte)n2;
                        p = p + 1;
                    } //end Next
                }
                else if (n1 == 8)
                {
                    i = i + 1;
                    n3 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n4 = nibbles[i-1]; //orig i
                    b = (n3 * 16) + n4;
                    b = b + 1;
                    /*                'dprint ("read 2 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + "): " + Str(b))*/
                    /*                'dprint ("write b(" + Str(b) + ") pixels of color n2(" + Str(n2) + ")")*/
                    for (j = 1; j <= b; j++)
                    {
                        pixelarray[p] = (byte)n2;
                        p = p + 1;
                    } //end Next
                }
                else if (n1 == 9)
                {
                    i = i + 1;
                    n3 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n4 = nibbles[i-1]; //orig i
                    b = (n3 * 16) + n4;
                    if (b % 0x2 == 1)
                    { //odd

                        b = b + 1;

                        for (j = 1; j <= b; j++)
                        {
                            i = i + 1;
                            n2 = nibbles[i-1]; //orig i

                            pixelarray[p] = (byte)n2;
                            p = p + 1;
                        } //end Next
                    }
                    else //even
                    {
                        pixelarray[p] = (byte)n2;
                        p = p + 1;
                        for (j = 1; j <= b; j++)
                        {
                            i = i + 1;
                            n2 = nibbles[i-1]; //orig i
                            pixelarray[p] = (byte)n2;
                            p = p + 1;
                        } //end Next
                    }
                }
                else if (n1 == 10)
                {
                    n2 = n2 + 1;

                    for (j = 1; j <= n2; j++)
                    {
                        pixelarray[p] = 16;
                        p = p + 1;
                    } //end Next
                }
                else if (n1 == 11)
                {
                    i = i + 1;
                    n3 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n4 = nibbles[i-1]; //orig i
                    b = (n3 * 16) + n4;
                    b = b + 1;
                    /*                'dprint ("read 2 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + "): " + Str(b))*/
                    /*                'dprint ("write b(" + Str(b) + " pixels by coping them from the row above")*/
                    for (j = 1; j <= b; j++)
                    {
                        /*                    'dprint ("write pixel of color " + Str(pixelarray[p - pwidth]))*/
                        pixelarray[p] = pixelarray[p - pwidth];
                        p = p + 1;
                    } //end Next
                    /*                'dprint ("write pixel of color n2(" + Str(n2) + ")")*/
                    pixelarray[p] = (byte)n2;
                    p = p + 1;
                }
                else if (n1 == 12)
                {
                    i = i + 1;
                    n3 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n4 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n5 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n6 = nibbles[i-1]; //orig i
                    b = (n3 * 4096) + (n4 * 256) + (n5 * 16) + n6;
                    b = b + 1;
                    /*                'dprint ("read 4 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + ",n5: " + Str(n5) + ",n6: " + Str(n6) + "): " + Str(b))*/
                    /*                'dprint ("write " + Str(b) + " pixels of color n2(" + Str(n2) + ")")*/
                    for (j = 1; j <= b; j++)
                    {
                        pixelarray[p] = (byte)n2;
                        p = p + 1;
                    } //end Next
                }
                else if (n1 == 13)
                {

                }
                else if (n1 == 14)
                {
                    if (n2 < 13)
                    {
                        /*                    'dprint ("n2 < 13:")*/
                        n2 = n2 + 17;
                        /*                    'dprint ("write n2+17(" + Str(n2) + ") transparent pixels")*/
                        for (j = 1; j <= n2; j++)
                        {
                            pixelarray[p] = 16;
                            p = p + 1;
                        } //end Next
                    }
                    else if (n2 == 13)
                    {
                        /*                    'dprint ("n2 = 13:")*/
                        i = i + 1;
                        n3 = nibbles[i-1]; //orig i
                        i = i + 1;
                        n4 = nibbles[i-1]; //orig i
                        b = (n3 * 16) + n4;
                        b = b + 1;
                        /*                    'dprint ("read 2 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + "): " + Str(b))*/
                        /*                    'dprint ("write b(" + Str(b) + ") transparent pixels")*/
                        for (j = 1; j <= b; j++)
                        {
                            pixelarray[p] = 16;
                            p = p + 1;
                        } //end Next
                    }
                    else if (n2 == 14)
                    {
                        /*                    'dprint ("n2 = 14:")*/
                        i = i + 1;
                        n3 = nibbles[i-1]; //orig i
                        i = i + 1;
                        n4 = nibbles[i-1]; //orig i
                        b = (n3 * 16) + n4;
                        b = b + 257;
                        /*                    'dprint ("read 2 nibbles + 257 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + "): " + Str(b))*/
                        /*                    'dprint ("write b(" + Str(b) + ") transparent pixels")*/
                        for (j = 1; j <= b; j++)
                        {
                            pixelarray[p] = 16;
                            p = p + 1;
                        } //end Next
                    }
                    else if (n2 == 15)
                    {
                        /*                    'dprint ("n2 = 15:")*/
                        i = i + 1;
                        n3 = nibbles[i-1]; //orig i
                        i = i + 1;
                        n4 = nibbles[i-1]; //orig i
                        i = i + 1;
                        n5 = nibbles[i-1]; //orig i
                        i = i + 1;
                        n6 = nibbles[i-1]; //orig i
                        b = (n3 * 4096) + (n4 * 256) + (n5 * 16) + n6;
                        b = b + 1;
                        /*                    'dprint ("read 4 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + ",n5: " + Str(n5) + ",n6: " + Str(n6) + "): " + Str(b))*/
                        /*                    'dprint ("write b(" + Str(b) + ") transparent pixels")*/
                        for (j = 1; j <= b; j++)
                        {
                            pixelarray[p] = 16;
                            p = p + 1;
                        } //end Next
                    }
                }
                else if (n1 == 15)
                {
                    i = i + 1;
                    n3 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n4 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n5 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n6 = nibbles[i-1]; //orig i
                    b = (n3 * 4096) + (n4 * 256) + (n5 * 16) + n6;
                    b = b + 1;
                    /*                'dprint ("read 4 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + ",n5: " + Str(n5) + ",n6: " + Str(n6) + "): " + Str(b))*/
                    /*                'dprint ("write b(" + Str(b) + ") pixels by coping them from the row above")*/
                    for (j = 1; j <= b; j++)
                    {
                        if (p - pwidth < 0)
                        {
                            /*                        'dprint ("write pixel of color 0 ***not on second line of graphic")*/
                            pixelarray[p] = 0;
                        }
                        else
                        {
                            /*                        'dprint ("write pixel of color " + Str(pixelarray[p - pwidth]))*/
                            pixelarray[p] = pixelarray[p - pwidth];
                        }
                        p = p + 1;
                    } //end Next
                    /*                'dprint ("write pixel of color n2(" + Str(n2) + ")")*/
                    pixelarray[p] = (byte)n2;
                    p = p + 1;
                }
                else
                {
                    MessageBox.Show("Error: 4 bit nibble >= 16! (" + n1.ToString() + ")");
                    return;
                }
            } //end Next
            p = p - 1;
            byte padding;
            padding = (byte)(pwidth & 0x3);
            if (padding > 0)
            {
                padding = (byte)(4 - padding);
            }
            FileStream f135 = File.Create(fname);
            write_word(0x4D42, f135);
            write_long(54 + (256 * 4) + ((pwidth * 8 + padding) * pheight / 8), f135);
            write_word(0, f135);
            write_word(0, f135);
            write_long(54 + (256 * 4), f135);
            write_long(40, f135);
            write_long(pwidth, f135);
            write_long(pheight, f135);
            write_word(1, f135);
            write_word(8, f135);
            write_long(0, f135);
            write_long((pwidth * 8 + padding) * pheight / 8, f135);
            write_long(2835, f135);
            write_long(2835, f135);
            write_long(256, f135);
            write_long(256, f135);

            for (i = 0; i <= 255; i++)
            {
                write_long(Color.FromArgb(0, gpalettes[curPalette, i]).ToArgb(), f135);
            } //end Next
            int line;
            int pixel;

            p = pwidth * pheight;
            for (line = 1; line <= pheight; line++)
            {
                for (j = 1; j <= pwidth; j++)
                {

                    if (mirrored > 0)
                    {
                        pixel = j - 1;
                    }
                    else
                    {
                        pixel = pwidth - j;
                    }
                    write_byte(ref pixelarray[p - ((line - 1) * pwidth) - pixel], f135);
                } //end Next
                for (j = 1; j <= padding; j++)
                {
                    byte zero = 0;
                    write_byte(ref zero, f135);
                } //end Next
            } //end Next
            f135.Close();
        } //End Sub

        
        public void kkspec_gexport_img1_type2(int itemnum, string fname, short mirrored)
        {
            /*        ' if mirrored > 0 then mirror it, else dont... dont use that old shit*/
            /*        'construct nibblearray*/
            byte[] nibbles;
            int nibblelen;
            byte[] pixelarray;
            int pwidth;
            int pheight;
            int i;
            int j;
            int p;
            int b;
            int n4, n2, n1, n3, n5;
            byte n6;
            int itemlen;

            itemlen = gitemsizes[itemnum];
            nibbles = new byte[itemlen * 2 + 1]; //1 st items shows 478 as length i csharp but 479 length in vb, we add 1
            pwidth = sread_word(gitemdatas[itemnum], 1);
            pheight = sread_word(gitemdatas[itemnum], 3);

            pixelarray = new byte[pwidth * pheight + 2048 + 1]; //csharp shows 1 byte less than vb so we add 1

            j = 1; //orig 1
            for (i = 5; i <= itemlen; i++)
            {
                b = sread_byte(gitemdatas[itemnum], i);
                nibbles[j] = (byte)CShort((b & 0xF0) / 16); //skriva till rätt ställe? orig j
                j = j + 1;
                nibbles[j] = (byte)(b & 0xF); //skriva till rätt ställe? orig j
                j = j + 1;
            } //end Next

            //string path = "D:\\temp\\0_objreader_nibbler.dat";
            //File.WriteAllBytes(path, nibbles);

            nibblelen = j - 1; //orig j - 1
            p = 1; //orig 1
            for (i = 1; i <= nibblelen; i++)
            {
                //path = $"D:\\temp\\{i}_objreader_nibbler.dat";
                //File.WriteAllBytes(path, nibbles);
                n1 = nibbles[i]; //orig i
                i = i + 1;
                n2 = nibbles[i]; //orig i
                if (n1 <= 7)
                {
                    n1 = n1 + 1;
                    for (j = 1; j <= n1; j++)
                    {
                        pixelarray[p] = (byte)n2; //orig p
                        p = p + 1;
                    } //end Next
                }
                else if (n1 == 8)
                {
                    i = i + 1;
                    n3 = nibbles[i]; //orig i
                    i = i + 1;
                    n4 = nibbles[i]; //orig i
                    b = (n3 * 16) + n4;
                    b = b + 1;

                    for (j = 1; j <= b; j++)
                    {
                        pixelarray[p] = (byte)n2;
                        p = p + 1;
                    } //end Next
                }
                else if (n1 == 9)
                {
                    i = i + 1;
                    n3 = nibbles[i]; //orig i
                    i = i + 1;
                    n4 = nibbles[i]; //orig i
                    b = (n3 * 16) + n4;

                    if (b % 0x2 == 1)
                    { //odd
                        b = b + 1;
                        for (j = 1; j <= b; j++)
                        {
                            i = i + 1;
                            n2 = nibbles[i]; //orig i
                            pixelarray[p] = (byte)n2;
                            p = p + 1;
                        } //end Next
                    }
                    else
                    {
                        /*                    'dprint ("b is even:")*/
                        pixelarray[p] = (byte)n2;
                        /*                    'dprint ("write pixel of color " + Str(n2))*/
                        p = p + 1;
                        /*                    'dprint ("write b(" + Str(b) + ") pixels by reading one nibble for each color")*/
                        for (j = 1; j <= b; j++)
                        {
                            i = i + 1;
                            n2 = nibbles[i]; //orig i
                            /*                        'dprint ("write pixel of color " + Str(n2))*/
                            pixelarray[p] = (byte)n2;
                            p = p + 1;
                        } //end Next
                    }
                }
                else if (n1 == 10)
                {
                    n2 = n2 + 1;
                    /*                'dprint ("write n2+1(" + Str(n2) + " transparent pixels")*/
                    for (j = 1; j <= n2; j++)
                    {
                        pixelarray[p] = 16;
                        p = p + 1;
                    } //end Next
                }
                else if (n1 == 11)
                {
                    i = i + 1;
                    n3 = nibbles[i]; //orig i
                    i = i + 1;
                    n4 = nibbles[i]; //orig i
                    b = (n3 * 16) + n4;
                    b = b + 1;
                    /*                'dprint ("read 2 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + "): " + Str(b))*/
                    /*                'dprint ("write b(" + Str(b) + " pixels by coping them from the row above")*/
                    for (j = 1; j <= b; j++)
                    {
                        /*                    'dprint ("write pixel of color " + Str(pixelarray[p - pwidth]))*/
                        pixelarray[p] = pixelarray[p - pwidth];
                        p = p + 1;
                    } //end Next
                    /*                'dprint ("write pixel of color n2(" + Str(n2) + ")")*/
                    pixelarray[p] = (byte)n2;
                    p = p + 1;
                }
                else if (n1 == 12)
                {
                    i = i + 1;
                    n3 = nibbles[i]; //orig i
                    i = i + 1;
                    n4 = nibbles[i]; //orig i
                    i = i + 1;
                    n5 = nibbles[i]; //orig i
                    i = i + 1;
                    n6 = nibbles[i]; //orig i
                    b = (n3 * 4096) + (n4 * 256) + (n5 * 16) + n6;
                    b = b + 1;
                    /*                'dprint ("read 4 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + ",n5: " + Str(n5) + ",n6: " + Str(n6) + "): " + Str(b))*/
                    /*                'dprint ("write " + Str(b) + " pixels of color n2(" + Str(n2) + ")")*/
                    for (j = 1; j <= b; j++)
                    {
                        pixelarray[p] = (byte)n2;
                        p = p + 1;
                    } //end Next
                }
                else if (n1 == 13)
                {
                    /*                'dprint ("do nothing")*/
                }
                else if (n1 == 14)
                {
                    if (n2 < 13)
                    {
                        /*                    'dprint ("n2 < 13:")*/
                        n2 = n2 + 17;
                        /*                    'dprint ("write n2+17(" + Str(n2) + ") transparent pixels")*/
                        for (j = 1; j <= n2; j++)
                        {
                            pixelarray[p] = 16;
                            p = p + 1;
                        } //end Next
                    }
                    else if (n2 == 13)
                    {
                        /*                    'dprint ("n2 = 13:")*/
                        i = i + 1;
                        n3 = nibbles[i]; //orig i
                        i = i + 1;
                        n4 = nibbles[i]; //orig i
                        b = (n3 * 16) + n4;
                        b = b + 1;
                        /*                    'dprint ("read 2 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + "): " + Str(b))*/
                        /*                    'dprint ("write b(" + Str(b) + ") transparent pixels")*/
                        for (j = 1; j <= b; j++)
                        {
                            pixelarray[p] = 16;
                            p = p + 1;
                        } //end Next
                    }
                    else if (n2 == 14)
                    {
                        /*                    'dprint ("n2 = 14:")*/
                        i = i + 1;
                        n3 = nibbles[i]; //orig i
                        i = i + 1;
                        n4 = nibbles[i]; //orig i
                        b = (n3 * 16) + n4;
                        b = b + 257;
                        /*                    'dprint ("read 2 nibbles + 257 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + "): " + Str(b))*/
                        /*                    'dprint ("write b(" + Str(b) + ") transparent pixels")*/
                        for (j = 1; j <= b; j++)
                        {
                            pixelarray[p] = 16;
                            p = p + 1;
                        } //end Next
                    }
                    else if (n2 == 15)
                    {
                        /*                    'dprint ("n2 = 15:")*/
                        i = i + 1;
                        n3 = nibbles[i]; //orig i
                        i = i + 1;
                        n4 = nibbles[i]; //orig i
                        i = i + 1;
                        n5 = nibbles[i]; //orig i
                        i = i + 1;
                        n6 = nibbles[i]; //orig i
                        b = (n3 * 4096) + (n4 * 256) + (n5 * 16) + n6;
                        b = b + 1;
                        /*                    'dprint ("read 4 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + ",n5: " + Str(n5) + ",n6: " + Str(n6) + "): " + Str(b))*/
                        /*                    'dprint ("write b(" + Str(b) + ") transparent pixels")*/
                        for (j = 1; j <= b; j++)
                        {
                            pixelarray[p] = 16;
                            p = p + 1;
                        } //end Next
                    }
                }
                else if (n1 == 15)
                {
                    i = i + 1;
                    n3 = nibbles[i]; //orig i
                    i = i + 1;
                    n4 = nibbles[i]; //orig i
                    i = i + 1;
                    n5 = nibbles[i]; //orig i
                    i = i + 1;
                    n6 = nibbles[i]; //orig i
                    b = (n3 * 4096) + (n4 * 256) + (n5 * 16) + n6;
                    b = b + 1;
                    /*                'dprint ("read 4 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + ",n5: " + Str(n5) + ",n6: " + Str(n6) + "): " + Str(b))*/
                    /*                'dprint ("write b(" + Str(b) + ") pixels by coping them from the row above")*/
                    for (j = 1; j <= b; j++)
                    {
                        if (p - pwidth < 0)
                        {
                            /*                        'dprint ("write pixel of color 0 ***not on second line of graphic")*/
                            pixelarray[p] = 0;
                        }
                        else
                        {
                            /*                        'dprint ("write pixel of color " + Str(pixelarray[p - pwidth]))*/
                            pixelarray[p] = pixelarray[p - pwidth];
                        }
                        p = p + 1;
                    } //end Next
                    /*                'dprint ("write pixel of color n2(" + Str(n2) + ")")*/
                    pixelarray[p] = (byte)n2;
                    p = p + 1;
                }
                else
                {
                    MessageBox.Show("Error: 4 bit nibble >= 16! (" + n1.ToString() + ")");
                    return;
                }
            } //end Next
            p = p - 1; //origin p = p - 1
            byte padding;
            padding = (byte)(pwidth & 0x3);
            if (padding > 0)
            {
                padding = (byte)(4 - padding);
            }
            FileStream f135 = File.Create(fname);
            write_word(0x4D42, f135);
            write_long(54 + (256 * 4) + ((pwidth * 8 + padding) * pheight / 8), f135);
            write_word(0, f135);
            write_word(0, f135);
            write_long(54 + (256 * 4), f135);
            write_long(40, f135);
            write_long(pwidth, f135);
            write_long(pheight, f135);
            write_word(1, f135);
            write_word(8, f135);
            write_long(0, f135);
            write_long((pwidth * 8 + padding) * pheight / 8, f135);
            write_long(2835, f135);
            write_long(2835, f135);
            write_long(256, f135);
            write_long(256, f135);

            kkspec_selPal4Item(itemnum);
            for (i = 0; i <= 255; i++)
            {
                write_long(Color.FromArgb(0, gpalettes[curPalette, i]).ToArgb(), f135);
            } //end Next
            int line;
            int pixel;

            p = pwidth * pheight;
            for (line = 1; line <= pheight; line++)
            {
                for (j = 1; j <= pwidth; j++)
                {

                    if (mirrored > 0)
                    {
                        pixel = j - 1;
                    }
                    else
                    {
                        pixel = pwidth - j;
                    }
                    write_byte(ref pixelarray[p - ((line - 1) * pwidth) - pixel], f135);
                } //end Next
                for (j = 1; j <= padding; j++)
                {
                    byte zero = 0;
                    write_byte(ref zero, f135);
                } //end Next
            } //end Next
            f135.Close();

        } //End Sub

        public void kkspec_selPal4Item(int itemnum)
        {

            int imagecount, mon, i, j;
            if (itemnum == 1)
            {
                curPalette = gPALETTE_TITLE;
            }
            else if (itemnum >= 2 && itemnum <= 4)
            {  //Main menu pallette
                curPalette = gPALETTE_MAINMENU;
            }
            else if (itemnum == 5)
            {  //credits pallette
                curPalette = gPALETTE_CREDITS;
            }
            else if (itemnum >= 446 && itemnum < 532)
            {  // creatures

                copypalette(gPALETTE_MAIN, gPALETTE_CUSTOM);
                curPalette = gPALETTE_CUSTOM;
                mon = -1;
                for (i = 1; i <= 27; i++)
                {
                    j = f558.monstergraphicinfo((short)i).graphicnum + 446;
                    imagecount = 1;
                    if (f559.monster((short)i).w4_sideimage() != 0) //0 är false
                    {
                        imagecount = imagecount + 1;
                    }
                    if (f559.monster((short)i).w4_backimage() != 0) //0 är false
                    {
                        imagecount = imagecount + 1;
                    }
                    /*                'If graphics.f559.monster(i).w4_attackimage Then*/
                    imagecount = imagecount + 1;
                    /*                'End If*/
                    if (itemnum >= j && itemnum < j + imagecount)
                    {
                        mon = i;
                        i = 28;
                    }
                } //end Next
                if (mon >= 0)
                {
                    if (f558.monstergraphicinfo((short)mon).colora() > 0)
                    {
                        setpalette(app.graphics.gPALETTE_CUSTOM, 10, f558.palettetocolor(f558.palette(f558.monstergraphicinfo((short)mon).colora(), 1)));
                        /*                    'graphics.setmodifiedcolor 11, graphics.f558.palettetocolor(graphics.f558.palette(graphics.f558.monstergraphicinfo(mon).ColorA, 1))*/
                    }
                    if (f558.monstergraphicinfo((short)mon).colorb() > 0)
                    {
                        setpalette(app.graphics.gPALETTE_CUSTOM, 9, f558.palettetocolor(f558.palette(f558.monstergraphicinfo((short)mon).colorb(), 1)));
                        /*                    'graphics.setmodifiedcolor 10, graphics.f558.palettetocolor(graphics.f558.palette(graphics.f558.monstergraphicinfo(mon).ColorB, 1))*/
                    }
                }
                /*            'MsgBox "Mon: " + Str(mon)*/
            }
            else
            {
                curPalette = gPALETTE_MAIN;
            }
        } //End Sub

        public void buildbmp(int itemnum)
        {

        } //End Sub


        public Bitmap gbitmaps(int itemnum, int isMirrored)
        {
            if (app.mapfile.gettyperenamed(itemnum) != "IMG1")
            {
                return null;
            }

            byte[] nibbles;
            int nibblelen;
            byte[] pixelarray;
            byte[] mirroredpixelarray;
            int pwidth;
            int pheight;
            int i;
            int j;
            int p;
            int b;
            int n4, n2, n1, n3, n5;
            byte n6;
            int itemlen;

            itemlen = getgitemsize(itemnum);
            nibbles = new byte[itemlen * 2];
            pwidth = sread_word(gitemdatas[itemnum], 1);
            pheight = sread_word(gitemdatas[itemnum], 3);

            pixelarray = new byte[pwidth * pheight + 16]; //16 bytes buffer in case of overflow
            mirroredpixelarray = new byte[pwidth * pheight + 16]; //16 bytes buffer in case of overflow

            j = 1;
            for (i = 5; i <= itemlen; i++)
            {
                b = sread_byte(gitemdatas[itemnum], i);
                nibbles[j] = (byte)CShort((b & 0xF0) / 16);
                j = j + 1;
                nibbles[j] = (byte)(b & 0xF);
                j = j + 1;
            } //end Next
            nibblelen = j - 1;
            p = 1;
            for (i = 1; i <= nibblelen; i++)
            {
                n1 = nibbles[i-1]; //orig i
                i = i + 1;
                n2 = nibbles[i-1]; //orig i
                if (n1 <= 7)
                {
                    n1 = n1 + 1;
                    for (j = 1; j <= n1; j++)
                    {
                        pixelarray[p] = (byte)n2;
                        p = p + 1;
                    } //end Next
                }
                else if (n1 == 8)
                {
                    i = i + 1;
                    n3 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n4 = nibbles[i-1]; //orig i
                    b = (n3 * 16) + n4;
                    b = b + 1;
                    /*                'dprint ("read 2 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + "): " + Str(b))*/
                    /*                'dprint ("write b(" + Str(b) + ") pixels of color n2(" + Str(n2) + ")")*/
                    for (j = 1; j <= b; j++)
                    {
                        pixelarray[p] = (byte)n2;
                        p = p + 1;
                    } //end Next
                }
                else if (n1 == 9)
                {
                    i = i + 1;
                    n3 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n4 = nibbles[i-1]; //orig i
                    b = (n3 * 16) + n4;
                    /*                'dprint ("read 2 nibbles as b (n3: " + Str(n3) + ",n4: " + Str(n4) + "): " + Str(b))*/
                    if (b % 0x2 == 1)
                    { //odd
                        /*                    'dprint ("b is odd (b=b+1):")*/
                        b = b + 1;
                        /*                    'dprint ("write b(" + Str(b) + ") pixels by reading one nibble for each color")*/
                        for (j = 1; j <= b; j++)
                        {
                            i = i + 1;
                            n2 = nibbles[i-1]; //orig i
                            /*                        ''dprint ("write pixel of color n2(" + Str(n2) + ")")*/
                            pixelarray[p] = (byte)n2;
                            p = p + 1;
                        } //end Next
                    }
                    else
                    {
                        pixelarray[p] = (byte)n2;

                        p = p + 1;

                        for (j = 1; j <= b; j++)
                        {
                            i = i + 1;
                            n2 = nibbles[i-1]; //orig i
                            /*                        ''dprint ("write pixel of color " + Str(n2))*/
                            pixelarray[p] = (byte)n2;
                            p = p + 1;
                        } //end Next
                    }
                }
                else if (n1 == 10)
                {
                    n2 = n2 + 1;
                    /*                'dprint ("write n2+1(" + Str(n2) + " transparent pixels")*/
                    for (j = 1; j <= n2; j++)
                    {
                        pixelarray[p] = 16;
                        p = p + 1;
                    } //end Next
                }
                else if (n1 == 11)
                {
                    i = i + 1;
                    n3 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n4 = nibbles[i-1]; //orig i
                    b = (n3 * 16) + n4;
                    b = b + 1;
                    /*                'dprint ("read 2 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + "): " + Str(b))*/
                    /*                'dprint ("write b(" + Str(b) + " pixels by coping them from the row above")*/
                    for (j = 1; j <= b; j++)
                    {
                        /*                    ''dprint ("write pixel of color " + Str(pixelarray[p - pwidth]))*/
                        pixelarray[p] = pixelarray[p - pwidth];
                        p = p + 1;
                    } //end Next
                    /*                'dprint ("write pixel of color n2(" + Str(n2) + ")")*/
                    pixelarray[p] = (byte)n2;
                    p = p + 1;
                }
                else if (n1 == 12)
                {
                    i = i + 1;
                    n3 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n4 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n5 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n6 = nibbles[i-1]; //orig i
                    b = (n3 * 4096) + (n4 * 256) + (n5 * 16) + n6;
                    b = b + 1;
                    /*                'dprint ("read 4 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + ",n5: " + Str(n5) + ",n6: " + Str(n6) + "): " + Str(b))*/
                    /*                'dprint ("write " + Str(b) + " pixels of color n2(" + Str(n2) + ")")*/
                    for (j = 1; j <= b; j++)
                    {
                        pixelarray[p] = (byte)n2;
                        p = p + 1;
                    } //end Next
                }
                else if (n1 == 13)
                {
                    /*                'dprint ("do nothing")*/
                }
                else if (n1 == 14)
                {
                    if (n2 < 13)
                    {
                        /*                    'dprint ("n2 < 13:")*/
                        n2 = n2 + 17;
                        /*                    'dprint ("write n2+17(" + Str(n2) + ") transparent pixels")*/
                        for (j = 1; j <= n2; j++)
                        {
                            pixelarray[p] = 16;
                            p = p + 1;
                        } //end Next
                    }
                    else if (n2 == 13)
                    {
                        /*                    'dprint ("n2 = 13:")*/
                        i = i + 1;
                        n3 = nibbles[i-1]; //orig i
                        i = i + 1;
                        n4 = nibbles[i-1]; //orig i
                        b = (n3 * 16) + n4;
                        b = b + 1;
                        /*                    'dprint ("read 2 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + "): " + Str(b))*/
                        /*                    'dprint ("write b(" + Str(b) + ") transparent pixels")*/
                        for (j = 1; j <= b; j++)
                        {
                            pixelarray[p] = 16;
                            p = p + 1;
                        } //end Next
                    }
                    else if (n2 == 14)
                    {
                        /*                    'dprint ("n2 = 14:")*/
                        i = i + 1;
                        n3 = nibbles[i-1]; //orig i
                        i = i + 1;
                        n4 = nibbles[i-1]; //orig i
                        b = (n3 * 16) + n4;
                        b = b + 257;
                        /*                    'dprint ("read 2 nibbles + 257 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + "): " + Str(b))*/
                        /*                    'dprint ("write b(" + Str(b) + ") transparent pixels")*/
                        for (j = 1; j <= b; j++)
                        {
                            pixelarray[p] = 16;
                            p = p + 1;
                        } //end Next
                    }
                    else if (n2 == 15)
                    {
                        /*                    'dprint ("n2 = 15:")*/
                        i = i + 1;
                        n3 = nibbles[i-1]; //orig i
                        i = i + 1;
                        n4 = nibbles[i-1]; //orig i
                        i = i + 1;
                        n5 = nibbles[i-1]; //orig i
                        i = i + 1;
                        n6 = nibbles[i-1]; //orig i
                        b = (n3 * 4096) + (n4 * 256) + (n5 * 16) + n6;
                        b = b + 1;
                        /*                    'dprint ("read 4 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + ",n5: " + Str(n5) + ",n6: " + Str(n6) + "): " + Str(b))*/
                        /*                    'dprint ("write b(" + Str(b) + ") transparent pixels")*/
                        for (j = 1; j <= b; j++)
                        {
                            pixelarray[p] = 16;
                            p = p + 1;
                        } //end Next
                    }
                }
                else if (n1 == 15)
                {
                    i = i + 1;
                    n3 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n4 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n5 = nibbles[i-1]; //orig i
                    i = i + 1;
                    n6 = nibbles[i-1]; //orig i
                    b = (n3 * 4096) + (n4 * 256) + (n5 * 16) + n6;
                    b = b + 1;
                    /*                'dprint ("read 4 nibbles + 1 as b (n3: " + Str(n3) + ",n4: " + Str(n4) + ",n5: " + Str(n5) + ",n6: " + Str(n6) + "): " + Str(b))*/
                    /*                'dprint ("write b(" + Str(b) + ") pixels by coping them from the row above")*/
                    for (j = 1; j <= b; j++)
                    {
                        if (p - pwidth < 0)
                        {
                            /*                        'dprint ("write pixel of color 0 ***not on second line of graphic")*/
                            pixelarray[p] = 0;
                        }
                        else
                        {
                            /*                        'dprint ("write pixel of color " + Str(pixelarray[p - pwidth]))*/
                            pixelarray[p] = pixelarray[p - pwidth];
                        }
                        p = p + 1;
                    } //end Next
                    /*                'dprint ("write pixel of color n2(" + Str(n2) + ")")*/
                    pixelarray[p] = (byte)n2;
                    p = p + 1;
                }
                else
                {
                    MessageBox.Show("Error: 4 bit nibble >= 16! (" + n1 + ")");
                    return null;
                }
            } //end Next
            p = p - 1;
            if (isMirrored == 0)
            {
                return App.CreatePic(pwidth, pheight, ginvertedpalettes.ToOneDimensionArray<Color>(0), pixelarray);
            }
            else
            {

                for (i = 1; i <= pheight; i++)
                {
                    for (j = 1; j <= pwidth; j++)
                    {
                        mirroredpixelarray[1 + (i) * pwidth - j] = pixelarray[(i - 1) * pwidth + j];
                    } //end Next
                } //end Next
                return App.CreatePic(pwidth, pheight, ginvertedpalettes.ToOneDimensionArray<Color>(0), mirroredpixelarray);
            }

        } //End function


        public void drawbmp(Graphics dst, int itemnum)
        {
            Bitmap picin = gbitmaps(itemnum, gMirrored);
            //Debug.Assert(picin.PixelFormat = Imaging.PixelFormat.Format8bppIndexed, "pic#" & itemnum & " is;n//t 8bpp?")
            System.Drawing.Imaging.ColorPalette pal = picin.Palette;
            int i;
            for (i = 0; i <= 255; i++)
            {
                pal.Entries[i] = gpalettes[curPalette, i];
            } //end Next
            picin.Palette = pal;
            dst.DrawImage(picin, 0, 0);
        } //End Sub

        public void drawbmpat(Control dst, int itemnum, int X, int Y)
        {
            Graphics cv = dst.CreateGraphics();
            cv.DrawImage(gbitmaps(itemnum, gMirrored), X, Y);
        } //End Sub

        public void stretchbmp(Graphics dst, int itemnum, int dstx, int dsty, int dstwidth, int dstheight, int srcx, int srcy)
        {
            Bitmap picin = gbitmaps(itemnum, gMirrored);
            //Debug.Assert(picin.PixelFormat = Imaging.PixelFormat.Format8bppIndexed, "pic#" & itemnum & " is;n//t 8bpp?")
            System.Drawing.Imaging.ColorPalette pal = picin.Palette;
            int i;
            for (i = 0; i <= 255; i++)
            {

                pal.Entries[i] = gpalettes[curPalette, i];
            } //end Next
            picin.Palette = pal;
            dst.DrawImage(
                picin,
                new Rectangle(dstx, dsty, dstwidth, dstheight),
                new Rectangle(srcx, srcy, gbitmapwidths(itemnum), gbitmapheights(itemnum)),
                GraphicsUnit.Pixel);
        } //End Sub
        public void stretchbmpby(
                Graphics dst,
                int itemnum,
                int dstx,
                int dsty,
                int srcx,
                int srcy,
                int dstwidth,
                int dstheight)
        {
            dstheight = dstheight * gbitmapheights(itemnum) / 100;
            dstwidth = dstwidth * gbitmapwidths(itemnum) / 100;
            Bitmap picin = gbitmaps(itemnum, gMirrored);
            //Debug.Assert(picin.PixelFormat = Imaging.PixelFormat.Format8bppIndexed, "pic#" & itemnum & " is;n//t 8bpp?")
            System.Drawing.Imaging.ColorPalette pal = picin.Palette;
            int i;
            for (i = 0; i <= 255; i++)
            {

                pal.Entries[i] = gpalettes[curPalette, i];
            } //end Next
            picin.Palette = pal;
            dst.DrawImage(picin,
                new Rectangle(dstx, dsty, dstwidth, dstheight),
                new Rectangle(srcx, srcy, gbitmapwidths(itemnum), gbitmapheights(itemnum)),
                GraphicsUnit.Pixel);
        } //End Sub
        public void drawtransparentbmp(ref Control dst, int itemnum, int TransparentPixel)
        {
            Graphics cv = dst.CreateGraphics();
            cv.DrawImage(gbitmaps(itemnum, gMirrored), 0, 0);
        } //End Sub


        public void drawtransparentbmpat(Graphics dst, int itemnum, int X, int Y, int TransparentPixel)
        {
            Bitmap picin = gbitmaps(itemnum, gMirrored);
            //Debug.Assert(picin.PixelFormat = Imaging.PixelFormat.Format8bppIndexed, "pic#" & itemnum & " is;n//t 8bpp?")
            System.Drawing.Imaging.ColorPalette pal = picin.Palette;
            int i;
            for (i = 0; i <= 255; i++)
            {
                pal.Entries[i] = gpalettes[curPalette, i];
            } //end Next
            pal.Entries[TransparentPixel] = Color.Transparent;
            picin.Palette = pal;
            dst.DrawImage(picin, X, Y);
        } //End Sub

        public void stretchtransparentbmp(Graphics dst,
                int itemnum, int dstx, int dsty, int dstwidth,
                int dstheight, int srcx, int srcy, int TransparentPixel)
        {
            Bitmap picin = gbitmaps(itemnum, gMirrored);
            //Debug.Assert(picin.PixelFormat = Imaging.PixelFormat.Format8bppIndexed, "pic#" & itemnum & " is;n//t 8bpp?")
            System.Drawing.Imaging.ColorPalette pal = picin.Palette;
            int i;
            for (i = 0; i <= 255; i++)
            {

                pal.Entries[i] = gpalettes[curPalette, i];
            } //end Next
            pal.Entries[TransparentPixel] = Color.Transparent;
            picin.Palette = pal;
            dst.DrawImage(picin, new Rectangle(dstx, dsty, dstwidth, dstheight),
                new Rectangle(srcx, srcy, gbitmapwidths(itemnum), gbitmapheights(itemnum)),
                GraphicsUnit.Pixel);
        } //End Sub
        public void stretchtransparentbmpsrc(
            Graphics dst, int itemnum, int dstx, int dsty,
            int dstwidth, int dstheight, int srcx, int srcy,
            int srcwidth, int srcheight, int TransparentPixel)
        {
            Bitmap picin = gbitmaps(itemnum, gMirrored);
            //Debug.Assert(picin.PixelFormat = Imaging.PixelFormat.Format8bppIndexed, "pic#" & itemnum & " is;n//t 8bpp?")
            System.Drawing.Imaging.ColorPalette pal = picin.Palette;
            int i;
            for (i = 0; i <= 255; i++)
            {

                pal.Entries[i] = gpalettes[curPalette, i];
            } //end Next
            pal.Entries[TransparentPixel] = Color.Transparent;
            picin.Palette = pal;
            dst.DrawImage(picin, new Rectangle(dstx, dsty, dstwidth, dstheight),
                new Rectangle(srcx, srcy, srcwidth, srcheight), GraphicsUnit.Pixel);
        } //End Sub
        public void fill(Graphics dst, int dstx, int dsty, int dstwidth, int dstheight, Color color)
        {
            dst.FillRectangle(new SolidBrush(color), dstx, dsty, dstwidth, dstheight);
        } //End Sub
        public void drawgrect(Graphics dst, int grectnum,
            int GraphicNum, int TransparentPixel, int percent)
        {
            if (GraphicNum == -1)
            {
                GraphicNum = f558.graphicrect((short)grectnum).displayedGraphic - 1;
            }
            if (f558.graphicrect((short)grectnum).mirrored != 0)
            {
                setmirrored(1);
            }
            stretchtransparentbmpsrc(dst, GraphicNum, 
                f558.graphicrect((short)grectnum).x1 * percent / 100, 
                f558.graphicrect((short)grectnum).y1 * percent / 100, 
                (f558.graphicrect((short)grectnum).x2 - f558.graphicrect((short)grectnum).x1 + 1) * percent / 100, 
                (f558.graphicrect((short)grectnum).y2 - f558.graphicrect((short)grectnum).y1 + 1) * percent / 100, 
                f558.graphicrect((short)grectnum).srcx, 
                f558.graphicrect((short)grectnum).srcy, 
                f558.graphicrect((short)grectnum).x2 - f558.graphicrect((short)grectnum).x1 + 1, 
                f558.graphicrect((short)grectnum).y2 - f558.graphicrect((short)grectnum).y1 + 1, 
                TransparentPixel);
            setmirrored(0);
        } //End Sub
        public void unload()
        {
            short i;
            int num;
            num = 0;
            for (i = 0; i <= numitems - 1; i++)
            {
                if (!(gbitmaps(i, 0) is null))
                {

                    gbitmaps(i, 0).Dispose();
                    num = num + 1;
                }
                if (!(gbitmaps(i, 1) is null))
                {
                    gbitmaps(i, 1).Dispose();
                    num = num + 1;
                }
            } //end Next
        } //End Sub
    }//end class
}

using DMObjectReader.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DMObjectReader.DTOs
{
    public class file561
    {


        /*    '// Start of graphic 0x231*/
        /*    '  btn Buttons18936[5];   // swapped when read*/
        /*    '  btn Buttons18876[4];   //    ""      ...*/
        /*    '  btn Buttons18828[3];   //    ""      ...*/
        /*    '  btn Buttons18792[2];   //    ""      ...*/
        /*    '  btn Buttons18768[5];   //    ""      ...*/
        /*    '  btn Buttons18708[4];   //    ""      ...*/
        /*    '  btn Buttons18660[3];   //    ""      ...*/
        /*    '  btn Buttons18624[2]; //      ""    ...*/
        /*    ' all above = editable*/
        /*    '  i8  Byte18600[8];    // RectPos swapped when read        ...*/
        /*    '  i8  Byte18592[8];    // RectPos swapped when read        ...*/
        /*    '  i8  Byte18584[8];    // RectPos swapped when read        ...*/
        /*    '  i8  Byte18576[8];    // RectPos swapped when read        ...*/
        /*    '  i16 Word18568[4];    // deltax[dir] NESW swapped when read        ...*/
        /*    '  i16 Word18560[4];    // deltay[dir] NESW swapped when read         ...*/
        /*    '  i16 Word18552[28];   // Swapped when read.  Magic buttons???         ...*/
        /*    '  MOVEBUTN MoveButn18496[4]; // MF, MR, MB, ML Swapped when read*/
        /*    '  ui8 DropAreas[16];//18464 X andY in screen coordinates*/
        /*    '  i8  Byte18448[8];     // KeyXlate swapped when read        ...*/
        /*    '  i8  Byte18440[12];    //  KeyXlate swapped when read        ...*/
        /*    '  KeyXlate Byte18428[7];//  KeyXlate swapped when read        ...*/
        /*    '  i8  Byte18400[28];    //  KeyXlate swapped when read*/
        /*    ' all below = editable*/
        /*    '  btn Buttons18372[4];  //*/
        /*    '  btn Buttons18324[9];  // Swapped when read... chest buttons*/
        /*    '  btn Buttons18216[13]; // Swapped when read*/
        /*    '  btn Buttons18060[9];  // Swapped when read*/
        /*    '  btn Buttons17952[5];  // Swapped when read*/
        /*    '  btn Buttons17892[5];  // Swapped when read*/
        /*    '  btn Buttons17832[3];  // Swapped when read         ...*/
        /*    '  btn Buttons17796[3];  // Swapped when read  [864-4*12]*/
        /*    '  btn Buttons17760[38]; // Swapped when read*/
        /*    '  btn Buttons17304[9];  // Swapped when read         ...*/
        /*    '  btn Buttons17196[20]; // Swapped when read         ...*/
        /*    '  btn Buttons16956[2];  // Swapped when read         ...*/
        /*    '  btn Buttons16932[4];  // Swapped when read*/
        /*    '                        // End of graphic 0x231*/
        public MemoryStream itemdata;
        public int itemloc; //Now 0 based

        private csbButton[] csbbuttons = ArrayUt<csbButton>.NewArray1(152);
        private byte[] byte18600 = new byte[229]; //orig 228
        private int cnt_csbbuttons;

        private App app;
        public file561(App _app)
        {
            app = _app;
        }

        public csbButton csbbutton(short X)
        {
            return csbbuttons[X];
        } //End function
        public byte read_byte()
        {
            itemdata.Position = itemloc;
            byte ans = new BinaryReader(itemdata).ReadByte();
            itemloc = itemloc + 1;

            return ans;
        } //End function
        public int read_int()
        {
            byte b1;
            byte b2;
            b2 = read_byte();
            b1 = read_byte();
            int ans_read_int = b2;
            ans_read_int = ans_read_int * 256;
            ans_read_int = ans_read_int + b1;

            return ans_read_int;
        } //End function
        public void write_byte(byte num)
        {
            itemdata.Position = itemloc;
            itemdata.WriteByte(num);
            itemloc = itemloc + 1;
        } //End Sub
        public void write_int(int num)
        {
            byte b1;
            byte b2;
            int l1;
            l1 = num & 0xFF00;
            b1 = App.CByte(l1 / 0x100);
            b2 = App.CByte(num & 0xFF);
            write_byte(b1);
            write_byte(b2);
        } //End Sub
        public void read()
        {
            int i;
            //REM http;://www.dungeon-master.com/forum/viewtopic.php?p=53531#53531
            if (itemdata.Length == 2004)
            {
                cnt_csbbuttons = 120; // DM
            }
            else
            {
                cnt_csbbuttons = 124; // DM CSB
            }
            itemloc = 0;
            /*        '28 dialog buttons*/
            for (i = 1; i <= 28; i++)
            {
                csbbuttons[i].word0 = read_int();
                csbbuttons[i].x1 = read_int();
                csbbuttons[i].x2 = read_int();
                csbbuttons[i].y1 = read_int();
                csbbuttons[i].y2 = read_int();
                csbbuttons[i].button = read_int();
            } //end Next
            for (i = 1; i <= 228; i++)
            {
                byte18600[i] = read_byte();
            } //end Next
            /*        '124 game buttons*/
            for (i = 1; i <= cnt_csbbuttons; i++)
            {
                csbbuttons[i + 28].word0 = read_int();
                csbbuttons[i + 28].x1 = read_int();
                csbbuttons[i + 28].x2 = read_int();
                csbbuttons[i + 28].y1 = read_int();
                csbbuttons[i + 28].y2 = read_int();
                csbbuttons[i + 28].button = read_int();
            } //end Next
            initcsbbuttonnames();
        } //End Sub
        public void update()
        {
            short i;
            itemdata = new MemoryStream();
            itemloc = 0;
            for (i = 1; i <= 28; i++)
            {
                write_int((csbbuttons[i].word0));
                write_int((csbbuttons[i].x1));
                write_int((csbbuttons[i].x2));
                write_int((csbbuttons[i].y1));
                write_int((csbbuttons[i].y2));
                write_int((csbbuttons[i].button));
            } //end Next
            for (i = 1; i <= 228; i++)
            {
                write_byte(byte18600[i]);
            } //end Next
            for (i = 1; i <= cnt_csbbuttons; i++)
            {
                write_int((csbbuttons[i + 28].word0));
                write_int((csbbuttons[i + 28].x1));
                write_int((csbbuttons[i + 28].x2));
                write_int((csbbuttons[i + 28].y1));
                write_int((csbbuttons[i + 28].y2));
                write_int((csbbuttons[i + 28].button));
            } //end Next
            app.graphics.setgitemsize(561, (int)itemdata.Length);
            app.graphics.setgitemdata(561, itemdata);
        } //End Sub
        public void initcsbbuttonnames()
        {
            short i;
            string nodescription;
            nodescription = "";
            i = 1;
            XmlDocument xmli = new XmlDocument();
            xmli.Load(Path.Combine(Application.StartupPath, "Data\\_csbbuttons.xml"));
            foreach(XmlElement elbtn in xmli.SelectNodes("/csbbuttons/button")) 
            { 
                csbbuttons[i].name = MyXUt.GetElementText((XmlElement)elbtn.SelectSingleNode("name"), "");
                csbbuttons[i].description = MyXUt.GetElementText((XmlElement)elbtn.SelectSingleNode("desc"), "");
                csbbuttons[i].samplepic = MyXUt.GetElementText((XmlElement)elbtn.SelectSingleNode("samplepic"), "");
                i = (short)(i + 1);
                if (i > 152) 
                {
                    return;
                }
            } //End Next
        }
    }//End Class
}

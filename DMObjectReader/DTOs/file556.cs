using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
    public class file556
    {
        public MemoryStream itemdata;
        public short itemlen;
        public short objs = 0;
        private string[] objstrings = new string[201]; //orig 200
        App app;
        public file556(App _app)
        {
            app = _app;
        }

        public string ObjString(short num)
        {
            return objstrings[num];
        }
        //End function
        public void setobjstring(short num, string s)
        {
            objstrings[num] = s;
        } //End Sub
        public string objtype(short num)
        {
            short ItemType;

            ItemType = num; //graphics.f559.obj(num).itemtype
		    if (ItemType >= 157 && ItemType< 165 ) 
            {
    			return "Food";
		    } 
            else if (ItemType >= 70 && ItemType< 128 ) 
            { 
			    return "Armor";
		    } 
            else if (ItemType >= 24 && ItemType < 70)
            {
                return "Weapon";
            }
            else
            {
                return "Misc";
            }
	} //End function

        /*	'text file containing all the names of objects*/

        public void read()
        {
            byte data;
            string s = "";
            int i;
            int j = 1;

            for (i = 1; i <= itemlen; i++)
            {
                itemdata.Position = i - 1; //could be we don't need to do -1 here? original i - 1
                data = new BinaryReader(itemdata).ReadByte();
                s += (char)(data & 0x7F);
                if (data >= 128)
                {
                    objstrings[j] = s;
                    j++;
                    s = "";
                }
            }

            objs = (short)(j - 1);
        }

        void write_byte(ref byte num)
        {
            itemdata.Position = itemdata.Length;
            itemdata.WriteByte(num);
        } //End Sub

        public void update()
        {
            short i;
            short j;
            byte b;
            string chr_Renamed;

            itemdata = new MemoryStream();
            for (i = 1; i <= 200; i++)
            {
                for (j = 1; j <= objstrings[i].Length - 1; j++)
                {
                    b = (byte)Strings.AscW(objstrings[i][j - 1]);
                    write_byte(ref b);
                }
                if (objstrings[i].Length == 0)
                {
                    byte writeback = 128;
                    write_byte(ref writeback); //hack, write a 0 string with 0x80 bit set...
                }
                else
                {
                    chr_Renamed = Strings.Mid(objstrings[i], objstrings[i].Length, 1);
                    b = (byte)Strings.AscW(chr_Renamed);
                    b = (byte)(b & 0x7F);
                    b = (byte)(b + 0x80);
                    write_byte(ref b);
                }
            }
            /*        'graphics.setitemsize 557, LenB(itemdata)*/
            /*        'graphics.setitemstr 557, itemdata*/
            app.graphics.setgitemsize(556, (int)itemdata.Length);
            app.graphics.setgitemdata(556, itemdata);
        } //End Sub
    } //End Class
}

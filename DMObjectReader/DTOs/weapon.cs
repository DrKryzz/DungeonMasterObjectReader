using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
    public class weapon
    {
        public byte Weight;
        public byte Byte1;
        public byte Damage;
        public byte Distance;
        public int Word4;


        /*	'weight = weight*/
        /*	'byte1: if 0x0a, fired by 0x1X, if 0x0b, fired by 0x2X*/
        /*	'if byte1 is 0 or 2, when thrown, use fighter-2 skill*/
        /*	'if byte is 1 or 3-15, when thrown, use ninja 4*/
        /*	'if byte is 16-112, when thrown, use ninja 5*/

        /*	'byte3: added throwing distance (str-independant), ACTUAL DAMAGE*/
        /*	'byte4: added damage when projectile hit,*/
        /*	'       throwing distance when (shot)*/
        /*	'word4:*/
        /*	'0x1F00: number associated with graphic information.*/
        /*	'0x00ff: damage added when "shoot"*/
        /*	' what in gods name does 0x2000 mean?*/

        public short Graphic()
        {
            return App.CShort((Word4 & 0x1F00) / 0x100);
        } //End function
        public short shootdamage()
        {
            return App.CShort((Word4 & 0xFF) / 0x1);
        } //End function

        public short firedby1()
        {
            if ((App.CShort(Byte1 & 0xF0) / 0x10) == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        } //End function
        public short firedby2()
        {
            if ((App.CShort(Byte1 & 0xF0) / 0x10) == 2)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        } //End function
        public short deltaenergy()
        {
            return App.CShort((Byte1 & 0xF) / 0x1);
        } //End function

        public void setgraphic(ref short num)
        {
            if (num > 31)
            {
                num = 31;
            }
            else if (num < 0)
            {
                num = 0;
            }
            Word4 = App.CShort(Word4 & 0xFF) + (num * 0x100);
        } //End Sub
        public void setshootdamage(ref  short num)
        {
            if (num > 255)
            {
                num = 255;
            }
            else if (num < 0)
            {
                num = 0;
            }
            Word4 = App.CShort(Word4 & 0xFF00) + num;
        } //End Sub

        public void setfiredby1(ref short num)
        {
            Byte1 = App.CByte(Byte1 & 0xF);
            if (num == 1)
            {
                Byte1 = App.CByte(Byte1 + 0x10);
            }
        } //End Sub
        public void setfiredby2(ref short num)
        {
            
            Byte1 = App.CByte(Byte1 & 0xF);
            if (num == 1)
            {
                Byte1 = App.CByte(Byte1 + 0x20);
            }
        } //End Sub
        public void setdeltaenergy(ref short num)
        {
            if (num > 15)
            {
                num = 15;
            }
            else if (num < 0)
            {
                num = 0;
            }
            Byte1 = App.CByte(App.CShort(Byte1 & 0xF0) + num);
        } //End Sub
    } //End Class
}

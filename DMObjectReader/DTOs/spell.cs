using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
    public class spell
    {

        public byte Rune1;
        public byte Rune2;
        public byte Rune3;
        public byte Rune4;
        public byte Difficulty;
        public byte Skill;
        public int word6;

        public int SpellType()
        {
            return word6 & 0xF;
        } //End function
        public int Result()
        {
            return App.CUShort((word6 & 0x3F0) / 0x10);
        } //End function
        public int Fatigue()
        {
            return App.CUShort((word6 & 0xFC00) / 0x400);
        } //End function

        public string tostring_Renamed()
        {
            string ans = "";
            if (Rune1 >= 96 && Rune1 < 102)
            {
                ans = App.Trim(App.Str(Rune1 - 95));
            }
            else
            {
                ans = "x";
            }

            if (Rune2 >= 102 && Rune2 < 108)
            {
                ans = ans + App.Trim(App.Str(Rune2 - 101));
            }
            else if (Rune2 == 0)
            {
                ans = ans + "x";
            }
            else
            {
                ans = ans + "?";
            }
            if (Rune3 >= 108 && Rune3 < 114)
            {
                ans = ans + App.Trim(App.Str(Rune3 - 107));
            }
            else if (Rune3 == 0)
            {
                ans = ans + "x";
            }
            else
            {
                ans = ans + "?";
            }
            if (Rune4 >= 114 && Rune4 < 120)
            {
                ans = ans + App.Trim(App.Str(Rune4 - 113));
            }
            else if (Rune4 == 0)
            {
                ans = ans + "x";
            }
            else
            {
                ans = ans + "?";
            }

            ans = App.Trim(ans);

            return ans;
        } //End function
        public void setspelltype(short num)
        {

            int l1;
            if (num > 15)
            {
                num = 15;
            }
            if (num < 0)
            {
                num = 0;
            }
            word6 = (word6 & 0xFFF0);
            l1 = num;
            word6 = word6 + l1;
        } //End Sub

        public void setresult(short num)
        {
            int l1;
            if (num > 63)
            {
                num = 63;
            }
            if (num < 0)
            {
                num = 0;
            }
            word6 = (word6 & 0xFC0F);
            l1 = num;
            l1 = l1 * 0x10;
            word6 = word6 + l1;
        } //End Sub

        public void setfatigue(short num)
        {
            int l1;
            if (num > 64)
            {
                num = 64;
            }
            if (num < 0)
            {
                num = 0;
            }
            word6 = (word6 & 0x3FF);
            l1 = num;
            l1 = l1 * 0x400;
            word6 = word6 + l1;
        } //End Sub
    } //End Class
}

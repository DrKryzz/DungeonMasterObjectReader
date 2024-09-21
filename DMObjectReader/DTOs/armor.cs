using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
    public class armor
    {
        public byte Weight;

        public byte armor_Renamed;
        public byte Byte2;
        public byte Unused1;


        public byte scaledarmor()
        {
            return App.CByte(Byte2 & 0xF);
        } //End function

        public byte noarmor()
        {
            return App.CByte(App.CShort(Byte2 & 0x80) / 0x80);
        } //End function

        public void setscaledarmor(ref short num)
        {
            if (num > 7)
            {
                num = 7;
            }
            if (num < 0)
            {
                num = 0;
            }
            Byte2 = App.CByte(App.CShort(Byte2 & 0x80) + num);
        } //End Sub

        public void setnoarmor(ref short num)
        {
            Byte2 = App.CByte(Byte2 & 0x7F);
            if (num == 1)
            {
                Byte2 = App.CByte(Byte2 + 0x80);
            }
        } //End Sub
    } //End Class
}

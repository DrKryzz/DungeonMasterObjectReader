using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
    public class Tdoor
    {
        public byte Byte0;
        public byte Strength;
        /*    '0x00FF = strength of door, for bashing*/
        /*    '0x0100 = not passable by super natural beings*/
        /*    '0x0200 = passable by projectiles (sometimes)*/
        /*    '0x0400 = mirrored randomly (ra door)*/
        /*    ' rest do nothing as far as i know*/
        public byte pass_nonmaterial()
        {
            return App.CByte(App.CShort(Byte0 & 0x1) / 0x1);
        } //End function
        public byte pass_projectile()
        {
            return App.CByte(App.CShort(Byte0 & 0x2) / 0x2);
        } //End function
        public byte mirrored()
        {
            return App.CByte(App.CShort(Byte0 & 0x4) / 0x4);
        } //End function
        public void set_passnonmaterial(ref short num)
        {
            if (num == 1)
            {
                Byte0 = App.CByte(App.CShort(Byte0 & 0xFE) + 0x1);
            }
            else
            {
                Byte0 = (byte)(Byte0 & 0xFE);
            }
        } //End Sub
        public void set_passprojectile(ref short num)
        {
            if (num == 1)
            {
                Byte0 = App.CByte(App.CShort(Byte0 & 0xFD) + 0x2);
            }
            else
            {
                Byte0 = (byte)(Byte0 & 0xFD);
            }
        } //End Sub
        public void set_mirrored(ref short num)
        {
            if (num == 1)
            {
                Byte0 = App.CByte(App.CShort(Byte0 & 0xFB) + 0x4);
            }
            else
            {
                Byte0 = (byte)(Byte0 & 0xFB);
            }
        } //End Sub
    } //End Class
}

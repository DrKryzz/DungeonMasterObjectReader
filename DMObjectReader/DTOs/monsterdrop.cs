using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
    public class monsterdrop
    {
        public int word0;

        /*	'0x8000 = sometimes drop, sometimes dont*/
        /*	'0x7FFF = number of item to drop*/
        /*	' rest do nothing as far as i know*/

        /*	'droppings:*/
        /*	'0 to 10 = dragon*/
        /*	'11 to 14 = worm*/
        /*	'15 to 17 = screamer*/
        /*	'18 to 20 = rat/hellhound*/
        /*	'21 to 25 = rockpile*/
        /*	'26 to 32 = knight*/
        /*	'^^^-- these items will always be cursed*/
        /*	'33 to 34 = antman*/
        /*	'35 to 36 = stone golem*/
        /*	'37 to 39 = skeleton*/

        public short itemnum()
        {
            return (short)(word0 & 0x7FFF);
        } //End function

        public byte sometimes()
        {
            int l1;

            l1 = (word0 & 0x8000);
            l1 = l1 / 0x8000;
            if (l1 == -1)
            {
                l1 = 1;
            }
            return (byte)l1;
        } //End function

        public void set_itemnum(ref short num)
        {

            if (num > 32767)
            {
                num = 32767;
            }
            if (num < 0)
            {
                num = 0;
            }
            word0 = App.CUShort((word0 & 0x8000) + num);
        } //End Sub

        public void set_sometimes(ref short num)
        {
            if (num == 1)
            {
                word0 = App.CUShort((word0 & 0x7FFF) + 32768);
            }
            else
            {
                word0 = word0 & 0x7FFF;
            }
        } //End Sub
    } //End Class
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
    public class obj
    {
        public int ItemType;
        public byte FloorGraphic;
        public byte Combos;
        public int WearLocs;
        public int wearlocation;



        /*	'word5: mask of locations it can be worn*/
        /*	'0x0001 = consumable*/


        /*	'byte3:number for pointer to graphic information*/
        /*	'byte4:combo slot #*/

        public byte wearlocnum(ref short num)
        {
            return App.CByte(App.CShort(WearLocs & (2 ^ num)) / (2 ^ num));
        } //End function
        public void setwearlocnum(ref short num)
        {


            int mask;
            int add;
            add = 2 ^ wearlocation;
            mask = 65535 - add;
            WearLocs = (WearLocs & mask);
            if (num == 1)
            {
                WearLocs = (WearLocs + add);
            }
        } //End Sub
    } //End Class
}

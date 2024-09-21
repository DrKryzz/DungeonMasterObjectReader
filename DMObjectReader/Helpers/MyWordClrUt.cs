using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.Helpers
{
    public class MyWordClrUt
    {
        //' http://www.dungeon-master.com/forum/viewtopic.php?p=91917#91917

        //' 0     1     2     3     4     5     6     7     8     9     a     b     c     d     e     f
        //' 00 00 03 33 04 44 03 10 00 66 04 20 00 40 00 60 07 00 07 50 06 43 07 70 02 22 05 55 00 07 07 77
        //' 00 00 02 22 03 33 03 10 00 66 04 10 00 30 00 50 06 00 06 40 05 32 07 60 01 11 04 44 00 06 06 66
        //' 00 00 01 11 02 22 02 10 00 66 03 10 00 20 00 40 05 00 05 30 04 21 07 50 00 00 03 33 00 05 05 55
        //' 00 00 00 00 01 11 01 00 00 66 02 10 00 10 00 30 04 00 04 20 03 10 06 40 00 00 02 22 00 04 04 44
        //' 00 00 00 00 00 00 00 00 00 66 01 00 00 00 00 20 03 00 03 10 02 00 05 30 00 00 01 11 00 03 03 33
        //' 00 00 00 00 00 00 00 00 00 66 00 00 00 00 00 10 02 00 02 00 01 00 03 20 00 00 00 00 00 02 02 22

        public static Color WordClrTo1Color(short p)
        {
            byte b1 = (byte)((p >> 8) & 255); // hi byte (0/R)
            byte b2 = (byte)((p >> 0) & 255); // lo byte (G/B)
            return Color.FromArgb(
                Math.Min(255, ((b1 >> 0) & 15) * 36),
                Math.Min(255, ((b2 >> 4) & 15) * 36),
                Math.Min(255, ((b2 >> 0) & 15) * 36));
        }

        public static short OneColorToWordClr(Color clr)
        {
            return (short)(
                ((int)(clr.R / 36) & 15) << 8 |
                ((int)(clr.G / 36) & 15) << 4 |
                ((int)(clr.B / 36) & 15) << 0);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.MiscClasses
{
    public struct BITMAPINFO_256
    {
        public BITMAPINFOHEADER bmiHeader;
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public int[] bmiColors;

        public void Initialize()
        {
            bmiColors = new int[256];
        }
    }
}

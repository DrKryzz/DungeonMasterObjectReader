using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.Helpers
{
    public class CSBinaryWriter
    {
        private BinaryWriter wr;
        public CSBinaryWriter(Stream stream)
        {
            wr = new BinaryWriter(stream);
        } //End Sub
        public void WriteByte(byte v)
        {
            wr.Write(v);
        } //End Sub
        public void WriteInt32(int v)
        {
         
            byte b1 = (byte)((v >> 24) & 255);
            byte b2 = (byte)((v >> 16) & 255);
            byte b3 = (byte)((v >> 8) & 255);
            byte b4 = (byte)((v >> 0) & 255);
            wr.Write((App.CInt(b4) << 24) | (App.CInt(b3) << 16) | (App.CInt(b2) << 8) | App.CInt(b1));
        } //End Sub
        public void WriteInt16(short v)
        {
            byte b1 = (byte)((v >> 8) & 255);
            byte b2 = (byte)((v >> 0) & 255);
            wr.Write((App.CShort(b2) << 8) | App.CShort(b1));
        } //End Sub
        public void WriteRectPos(RectPos v)
        {
            WriteInt16(v.x);
            WriteInt16(v.y);
            WriteInt16(v.cx);
            WriteInt16(v.cy);
        } //End Sub
        public void WriteSOUND(SOUND v)
        {

            WriteInt16(v.word_header_nrOfSamples);
            WriteByte(v.byte2);
            WriteByte(v.byte3);
            WriteByte(v.byte4);
            WriteByte(v.byte5);
            WriteByte(v.byte6);
            WriteByte(v.byte7);
        } //End Sub
        public void WriteICONDISPLAY(ICONDISPLAY v)
        {
            WriteInt16(v.pixelX);
            WriteInt16(v.pixelY);
            WriteInt16(v.objectType);
        } //End Sub
        public void WritePALETTE(PALETTE v)
        {
            int i;
            for (i = 1; i <= 16; i++)
            {
                WriteInt16(v.color[i]);
            } //end Next
        } //End Sub
    } //End Class
}

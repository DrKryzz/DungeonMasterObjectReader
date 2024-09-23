using System.IO;

namespace DMObjectReader.Helpers
{
    public class CSBinaryReader
    {
        private BinaryReader br;

        public CSBinaryReader(Stream stream)
        {
            this.br = new BinaryReader(stream);
        }

        public byte ReadByte()
        {
            return br.ReadByte();
        }

        public int ReadInt32()
        {
            byte b4 = br.ReadByte();
            byte b3 = br.ReadByte();
            byte b2 = br.ReadByte();
            byte b1 = br.ReadByte();
            return (b4 << 24) | (b3 << 16) | (b2 << 8) | b1;
        }

        public short ReadInt16()
        {
            byte b2 = br.ReadByte();
            byte b1 = br.ReadByte();
            return (short)((short)(b2 << 8) | (short)b1);
        }

        public RectPos ReadRectPos() 
        {
            RectPos ReadRectPos = new RectPos();
            ReadRectPos.x = ReadInt16();
            ReadRectPos.y = ReadInt16();
            ReadRectPos.cx = ReadInt16();
            ReadRectPos.cy = ReadInt16();

            return ReadRectPos;
        }

        public SOUND ReadSOUND() 
        {
            SOUND ReadSOUND = new SOUND();
            ReadSOUND.word_header_nrOfSamples = ReadInt16();
            ReadSOUND.byte2 = ReadByte();
            ReadSOUND.byte3 = ReadByte();
            ReadSOUND.byte4 = ReadByte();
            ReadSOUND.byte5 = ReadByte();
            ReadSOUND.byte6 = ReadByte();
            ReadSOUND.byte7 = ReadByte();

            return ReadSOUND;
        }

        public ICONDISPLAY ReadICONDISPLAY() 
        {
            ICONDISPLAY ReadICONDISPLAY = new ICONDISPLAY();
            ReadICONDISPLAY.pixelX = ReadInt16();
            ReadICONDISPLAY.pixelY = ReadInt16();
            ReadICONDISPLAY.objectType = ReadInt16();

            return ReadICONDISPLAY;
        }

        public PALETTE ReadPALETTE() 
        {
            PALETTE ReadPALETTE = new PALETTE();
            int i;
            for (i = 1; i <= 16; i++) 
            {
                ReadPALETTE.color[i] = ReadInt16();
            }

            return ReadPALETTE;
        }
    }// End Class
}
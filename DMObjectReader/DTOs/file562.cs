using DMObjectReader.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.DTOs
{
    public class file562
    {
        /* ' They're 1 BASED ARRAY! ->*/
        public byte[] Bytes1830 = new byte[3]; //orig 2
        public int[] Long1828 = new int[5]; //orig 5
        public int[] Long1812 = new int[5]; //orig 5
        public RectPos wRectPos1796;
        public RectPos wRectPos1788;
        public RectPos wRectPos1780;
        public SOUND[] sound1772 = new SOUND[23]; //orig 22
        public byte[] Byte1596 = new byte[9]; //orig 8
        public short Word1588;
        public short[] DropOrder = new short[31]; //orig 30
        public short[,] Word1526 = new short[5, 4]; //orig 4,3
        public short[,] Word1502 = new short[5, 7]; //orig 4,6
        public RectPos[] wRectPos1454 = new RectPos[5]; //orig 4
        public byte[] SpecialChars = new byte[7]; //orig 6
        public byte[] Byte1416 = new byte[3]; //orig 2
        public byte[] Byte1414 = new byte[3]; //orig 2
        public byte[] Byte1412 = new byte[7]; //orig 6
        public short[] Word1406 = new short[5]; //orig 4
        public short[] Word1398 = new short[5]; //orig 4
        public byte[] Byte1390 = new byte[5]; //orig 4
        public byte[] Byte1386 = new byte[5]; //orig 4
        public byte[] Byte1382 = new byte[17]; //orig 16
        public byte[] Byte1366 = new byte[17]; //orig 16
        public byte[] Byte1350 = new byte[129]; //orig 128
        public byte[] Byte1222 = new byte[129]; //orig 128
        public byte[] Byte1094 = new byte[9]; //orig 8
        public short[] PaletteBrightness = new short[7]; //orig 6
        public short[] Word1074 = new short[17]; //orig 16
        public short[] CarryLocation = new short[39]; //orig 38
        public RectPos wRectPos966;
        public RectPos wRectPos958;
        public RectPos wRectPos950;
        public RectPos wRectPos942;
        public RectPos wRectPos934;
        public RectPos wRectPos926;
        public short Unused918;
        public ICONDISPLAY[] IconDisplay = new ICONDISPLAY[47]; //orig 46
        public byte Byte640;
        public byte[] Reserved639 = new byte[16]; //orig 15
        public RectPos wRectPos624;
        public byte[] Reserved616 = new byte[5]; //orig 4
        public short[] Word612 = new short[8]; //orig 7
        public byte[] Reserved598 = new byte[9]; //orig 8
        public byte[] uByte590 = new byte[5]; //orig 4
        public byte[,] Byte586 = new byte[9, 5]; //orig 8,4
        public short Word554;
        public PALETTE[] Palette552 = new PALETTE[7]; //orig 6
        public PALETTE Palette360;
        public PALETTE Palette328;
        public short[] DefaultGraphicList = new short[71]; //orig 70
        public byte[] Byte156 = new byte[17]; //orig 16
        public short[] Word140 = new short[5]; //orig 4
        public short[] Word132 = new short[5]; //orig 4
        public short[] Word124 = new short[5]; //orig 4
        public short[] Word116 = new short[5]; //orig 4
        public short[] Word108 = new short[5]; //orig 4
        public short[] Word100 = new short[5]; //orig 4
        public short[] Word92 = new short[5]; //orig 4
        public short[] Word84 = new short[5]; //orig 4
        public short[] Word76 = new short[5]; //orig 4
        public short[] Word68 = new short[5]; //orig 4
        public short[] Word60 = new short[5]; //orig 4
        public short[] Word52 = new short[5]; //orig 4
        public short[] Word44 = new short[5]; //orig 4
        public short[] Word36 = new short[5]; //orig 4
        public short[] Word28 = new short[5]; //orig 4
        public short[] Word20 = new short[5]; //orig 4
        public short[] Word12 = new short[5]; //orig 4
        /*    ' <- They're 1 BASED ARRAY!*/
        public MemoryStream itemdata;

        private App app;
        public file562(App _app)
        {
            app = _app;
        }
        public void read()
        {
            short i, j;
            CSBinaryReader br = new CSBinaryReader(itemdata);
            for (i = 1; i <= 2; i++)
            {
                Bytes1830[i] = br.ReadByte();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Long1828[i] = br.ReadInt32();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Long1812[i] = br.ReadInt32();
            } //end Next
            wRectPos1796 = br.ReadRectPos();
            wRectPos1788 = br.ReadRectPos();
            wRectPos1780 = br.ReadRectPos();
            for (i = 1; i <= 22; i++)
            {
                sound1772[i] = br.ReadSOUND();
            } //end Next
            for (i = 1; i <= 8; i++)
            {
                Byte1596[i] = br.ReadByte();
            } //end Next
            Word1588 = br.ReadInt16();
            for (i = 1; i <= 30; i++)
            {
                DropOrder[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                for (j = 1; j <= 3; j++)
                {
                    Word1526[i, j] = br.ReadInt16();
                } //end Next
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                for (j = 1; j <= 6; j++)
                {
                    Word1502[i, j] = br.ReadInt16();
                } //end Next
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                wRectPos1454[i] = br.ReadRectPos();
            } //end Next
            for (i = 1; i <= 6; i++)
            {
                SpecialChars[i] = br.ReadByte();
            } //end Next
            Byte1416[1] = br.ReadByte();
            Byte1416[2] = br.ReadByte();
            Byte1414[1] = br.ReadByte();
            Byte1414[2] = br.ReadByte();
            for (i = 1; i <= 6; i++)
            {
                Byte1412[i] = br.ReadByte();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word1406[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word1398[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Byte1390[i] = br.ReadByte();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Byte1386[i] = br.ReadByte();
            } //end Next
            for (i = 1; i <= 16; i++)
            {
                Byte1382[i] = br.ReadByte();
            } //end Next
            for (i = 1; i <= 16; i++)
            {
                Byte1366[i] = br.ReadByte();
            } //end Next
            for (i = 1; i <= 128; i++)
            {
                Byte1350[i] = br.ReadByte();
            } //end Next
            for (i = 1; i <= 128; i++)
            {
                Byte1222[i] = br.ReadByte();
            } //end Next
            for (i = 1; i <= 8; i++)
            {
                Byte1094[i] = br.ReadByte();
            } //end Next
            for (i = 1; i <= 6; i++)
            {
                PaletteBrightness[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 16; i++)
            {
                Word1074[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 38; i++)
            {
                CarryLocation[i] = br.ReadInt16();
            } //end Next
            wRectPos966 = br.ReadRectPos();
            wRectPos958 = br.ReadRectPos();
            wRectPos950 = br.ReadRectPos();
            wRectPos942 = br.ReadRectPos();
            wRectPos934 = br.ReadRectPos();
            wRectPos926 = br.ReadRectPos();
            Unused918 = br.ReadInt16();
            for (i = 1; i <= 46; i++)
            {
                IconDisplay[i] = br.ReadICONDISPLAY();
            } //end Next
            Byte640 = br.ReadByte();
            for (i = 1; i <= 15; i++)
            {
                Reserved639[i] = br.ReadByte();
            } //end Next
            wRectPos624 = br.ReadRectPos();
            for (i = 1; i <= 4; i++)
            {
                Reserved616[i] = br.ReadByte();
            } //end Next
            for (i = 1; i <= 7; i++)
            {
                Word612[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 8; i++)
            {
                Reserved598[i] = br.ReadByte();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                uByte590[i] = br.ReadByte();
            } //end Next
            for (i = 1; i <= 8; i++)
            {
                for (j = 1; j <= 4; j++)
                {
                    Byte586[i, j] = br.ReadByte();
                } //end Next
            } //end Next
            Word554 = br.ReadInt16();
            for (i = 1; i <= 6; i++)
            {
                Palette552[i] = br.ReadPALETTE();
            } //end Next
            Palette360 = br.ReadPALETTE();
            Palette328 = br.ReadPALETTE();
            for (i = 1; i <= 70; i++)
            {
                DefaultGraphicList[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 16; i++)
            {
                Byte156[i] = br.ReadByte();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word140[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word132[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word124[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word116[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word108[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word100[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word92[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word84[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word76[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word68[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word60[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word52[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word44[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word36[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word28[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word20[i] = br.ReadInt16();
            } //end Next
            for (i = 1; i <= 4; i++)
            {
                Word12[i] = br.ReadInt16();
            } //end Next
        } //End Sub
        public void update()
        {
            itemdata = new MemoryStream();
            CSBinaryWriter wr = new CSBinaryWriter(itemdata);
            int i;
            int j;
            for(i = 1; i <= 2; i++) 
            {
                wr.WriteByte(Bytes1830[i]);
            } //end Next
            for(i = 1; i<=4; i++)
            {
                wr.WriteInt32(Long1828[i]);
            } //end Next
            for(i = 1; i<=4;i++)
            {
                wr.WriteInt32(Long1812[i]);
            } //end Next

            wr.WriteRectPos(wRectPos1796);
            wr.WriteRectPos(wRectPos1788);
            wr.WriteRectPos(wRectPos1780);

            for( i = 1;i<= 22;i++)
            { 
                wr.WriteSOUND(sound1772[i]); 
            } //end Next
            for( i = 1;i<= 8 ;i++)
            { 
                wr.WriteByte(Byte1596[i]); 
            } //end Next
            wr.WriteInt16(Word1588);
            for(i = 1;i<= 30; i++)
            {
                wr.WriteInt16(DropOrder[i]); 
            } //end Next
            for(i = 1;i<= 4 ;i++)
            {
                for(j = 1; j<=3; j++)
                {
                    wr.WriteInt16(Word1526[i, j]); 
                } //end Next :
            } //end Next
            for(i = 1;i<= 4;i++)
            {
                for(j = 1;j<=6;j++)
                {
                    wr.WriteInt16(Word1502[i, j]); 
                } //end Next
            } //end Next
            for(i = 1;i<= 4;i++)
            {
                wr.WriteRectPos(wRectPos1454[i]);
            } //end Next
            for(i = 1;i<= 6;i++)
            {
                wr.WriteByte(SpecialChars[i]); 
            } //end Next
            for(i = 1;i<= 2;i++)
            {
                wr.WriteByte(Byte1416[i]); 
            } //end Next
            for(i = 1;i<= 2;i++)
            { 
                wr.WriteByte(Byte1414[i]); 
            } //end Next
            for(i = 1;i<= 6;i++)
            { 
                wr.WriteByte(Byte1412[i]); 
            } //end Next
            for(i = 1;i<= 4;i++)
            { 
                wr.WriteInt16(Word1406[i]); 
            } //end Next
            for (i = 1; i <= 4;i++)
            { 
                wr.WriteInt16(Word1398[i]); 
            } //end Next
            for (i = 1; i <= 4;i++)
            { 
                wr.WriteByte(Byte1390[i]); 
            } //end Next
            for (i = 1; i <= 4;i++)
            { 
                wr.WriteByte(Byte1386[i]); 
            } //end Next
            for(i = 1; i <= 16;i++)
            { 
                wr.WriteByte(Byte1382[i]); 
            } //end Next
            for(i = 1; i <= 16;i++)
            { 
                wr.WriteByte(Byte1366[i]); 
            } //end Next
            for(i = 1; i <=128;i++)
            { 
                wr.WriteByte(Byte1350[i]);
            } //end Next
            for(i = 1; i <=128;i++)
            { 
                wr.WriteByte(Byte1222[i]); 
            } //end Next
            for(i = 1; i <= 8 ;i++)
            { 
                wr.WriteByte(Byte1094[i]); 
            } //end Next
            for(i = 1; i <= 6 ;i++)
            { 
                wr.WriteInt16(PaletteBrightness[i]); 
            } //end Next
            for(i = 1; i <= 16;i++)
            { 
                wr.WriteInt16(Word1074[i]); 
            } //end Next
            for(i = 1; i <= 38;i++)
            { 
                wr.WriteInt16(CarryLocation[i]); 
            } //end Next
            wr.WriteRectPos(wRectPos966);
            wr.WriteRectPos(wRectPos958);
            wr.WriteRectPos(wRectPos950);
            wr.WriteRectPos(wRectPos942);
            wr.WriteRectPos(wRectPos934);
            wr.WriteRectPos(wRectPos926);
            wr.WriteInt16(Unused918);
            for( i = 1; i <= 46;i++)
            {
                wr.WriteICONDISPLAY(IconDisplay[i]); 
            } //end Next
            wr.WriteByte(Byte640);
            for( i = 1;i <= 15;i++)
            {
                wr.WriteByte(Reserved639[i]); 
            } //end Next
            wr.WriteRectPos(wRectPos624);
            for( i = 1;i<= 4;i++)
            {
                wr.WriteByte(Reserved616[i]);
            } //end Next
            for( i = 1;i<= 7;i++)
            {
                wr.WriteInt16(Word612[i]);
            } //end Next
            for( i = 1;i<= 8;i++)
            {
                wr.WriteByte(Reserved598[i]);
            } //end Next
            for( i = 1;i<= 4;i++)
            {
                wr.WriteByte(uByte590[i]);
            } //end Next
            for( i = 1;i<= 8;i++)
            { 
                for(j = 1;j<=4;j++)
                {
                    wr.WriteByte(Byte586[i, j]);
                } //end Next
            } //end Next
            wr.WriteInt16(Word554);
            for( i = 1;i<=6;i++){
                wr.WritePALETTE(Palette552[i]);
            } //end Next
            wr.WritePALETTE(Palette360);
            wr.WritePALETTE(Palette328);
            for( i = 1;i<= 70;i++)
            { 
                wr.WriteInt16(DefaultGraphicList[i]); 
            } //end Next
            for( i = 1;i<= 16;i++)
            {
                wr.WriteByte(Byte156[i]);
            } //end Next
            for( i = 1;i<= 4 ;i++)
            {
                wr.WriteInt16(Word140[i]);
            } //end Next
            for( i = 1;i<= 4 ;i++)
            {
                wr.WriteInt16(Word132[i]);
            } //end Next
            for( i = 1;i<= 4 ;i++)
            {
                wr.WriteInt16(Word124[i]);
            } //end Next
            for( i = 1;i<= 4 ;i++)
            {
                wr.WriteInt16(Word116[i]);
            } //end Next
            for( i = 1;i<= 4 ;i++)
            {
                wr.WriteInt16(Word108[i]);
            } //end Next
            for( i = 1;i<= 4 ;i++)
            {
                wr.WriteInt16(Word100[i]);
            } //end Next
            for( i = 1;i<= 4 ;i++)
            {
                wr.WriteInt16(Word92[i]);
            } //end Next
            for( i = 1;i<= 4 ;i++)
            {
                wr.WriteInt16(Word84[i]);
            } //end Next
            for( i = 1;i<= 4 ;i++)
            {
                wr.WriteInt16(Word76[i]);
            } //end Next
            for( i = 1;i<= 4 ;i++)
            {
                wr.WriteInt16(Word68[i]);
            } //end Next
            for( i = 1;i<= 4 ;i++)
            {
                wr.WriteInt16(Word60[i]);
            } //end Next
            for( i = 1;i<= 4 ;i++)
            {
                wr.WriteInt16(Word52[i]);
            } //end Next
            for( i = 1;i<= 4 ;i++)
            {
                wr.WriteInt16(Word44[i]);
            } //end Next
            for( i = 1;i<= 4 ;i++)
            {
                wr.WriteInt16(Word36[i]);
            } //end Next
            for( i = 1;i<= 4 ;i++)
            {
                wr.WriteInt16(Word28[i]);
            } //end Next
            for( i = 1;i<= 4 ;i++)
            { 
                wr.WriteInt16(Word20[i]); 
            } //end Next
            for( i = 1;i<= 4 ;i++)
            {
                wr.WriteInt16(Word12[i]);
            } //end Next
            app.graphics.setgitemsize(562, App.CInt(itemdata.Length));
            app.graphics.setgitemdata(562, itemdata);
    } //End Sub
/*    ''' <summary>*/
/*    ''' */
/*    ''' </summary>*/
/*    ''' <param name="i">1 BASED</param>*/
/*    ''' <param name="p"></param>*/
/*    ''' <remarks></remarks>*/
    public void SetPalette552(int i, PALETTE p)
    {
        int x;
        for (x = 1; x <= 16; x++)
        {
            Palette552[i].color[x] = p.color[x];
        } //end Next
    } //End Sub

    public void SetPalette360(PALETTE p)
    {
        int x;
        for (x = 1; x <= 16; x++)
        {
            Palette360.color[x] = p.color[x];
        } //end Next
    } //End Sub

    public void SetPalette328(PALETTE p)
    {
        int x;
        for (x = 1; x <= 16; x++)
        {
            Palette328.color[x] = p.color[x];
        } //end Next
    } //End Sub
    }
}

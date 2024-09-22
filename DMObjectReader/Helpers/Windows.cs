using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DMObjectReader.Helpers
{

    //SCK Windows
    public class Windows
    {
        public const int DWORD_SIZE = 4;
        public const int WORD_SIZE = 2;

        public static byte[] JNumberToWORD(int pValue)
        {
            byte[] lBytes = new byte[2];
            lBytes[0] = (byte)pValue;
            lBytes[1] = (byte)(pValue >> 8);
            return lBytes;
        }

        public static byte[] JNumberToDWORD(long pValue)
        {
            byte[] lBytes = new byte[4];
            lBytes[0] = (byte)(int)pValue;
            lBytes[1] = (byte)(int)(pValue >> 8);
            lBytes[2] = (byte)(int)(pValue >> 16);
            lBytes[3] = (byte)(int)(pValue >> 24);
            return lBytes;
        }

        public static int readWord(Stream pStream, int pEndianMode)
        {
            byte[] lBuffer = new byte[2];
            int lBytesRead = pStream.Read(lBuffer, 0, 2);
            if (lBytesRead != 2)
                throw new IOException();
            return Bytes.BytesToInt(lBuffer[0], lBuffer[1], pEndianMode);
        }

        public static int readWord(byte[] pData, int pStartIndex, int pEndianMode)
        {
            if (pData.Length < pStartIndex + 2)
                throw new IOException();
            return Bytes.BytesToInt(pData[pStartIndex + 0], pData[pStartIndex + 1], pEndianMode);
        }

        public static int readDWord(byte[] pData, int pEndianMode)
        {
            return readDWord(pData, 0, pEndianMode);
        }

        public static int readDWord(byte[] pData, int pStartIndex, int pEndianMode)
        {
            if (pData.Length < pStartIndex + 4)
                throw new IOException();
            return Bytes.BytesToInt(pData[pStartIndex + 0], pData[pStartIndex + 1], pData[pStartIndex + 2], pData[pStartIndex + 3], pEndianMode);
        }

        public static int readDWord(Stream pStream, int pEndianMode)
        {
            byte[] lBuffer = new byte[4];
            int lBytesRead = pStream.Read(lBuffer, 0, 4);
            if (lBytesRead != 4)
                throw new IOException();
            return readDWord(lBuffer, pEndianMode);
        }

        public static byte[] readWordAsByte(Stream pStream, int pEndianMode)
        {
            byte[] lBuffer = new byte[2];
            int lBytesRead = pStream.Read(lBuffer, 0, 2);
            if (lBytesRead != 2)
                throw new IOException();
            if (pEndianMode != 1)
            {
                byte lTmp = lBuffer[0];
                lBuffer[0] = lBuffer[1];
                lBuffer[1] = lTmp;
            }
            return lBuffer;
        }

        public static string dosExtendedToUTF8(byte[] pText)
        {
            StringBuilder lBuffer = new StringBuilder();
            try
            {
                using (var bais = new MemoryStream(pText))
                using (var isr = new StreamReader(bais, Encoding.UTF8))
                {
                    int ch;
                    while ((ch = isr.Read()) > -1)
                        lBuffer.Append((char)ch);
                }
            }
            catch (IOException ioe)
            {
                throw ioe;
            }
            return lBuffer.ToString();
        }

        public static string dosExtendedToUnicode(byte[] pText)
        {
            StringBuilder lBuffer = new StringBuilder();
            try
            {
                using (var bais = new MemoryStream(pText))
                using (var isr = new StreamReader(bais, Encoding.GetEncoding("Cp437")))
                {
                    int ch;
                    while ((ch = isr.Read()) > -1)
                        lBuffer.Append((char)ch);
                }
            }
            catch (IOException ioe)
            {
                //ioe.PrintStackTrace();
                throw ioe;
            }
            return lBuffer.ToString();
        }
    }


}

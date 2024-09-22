using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.Helpers
{
    public class Bytes
    {

        /*
        in a byte 00000000 = 8 bits
        Mask with 0xF0 (11110000): Keeps the upper 4 bits and clears the lower 4 bits.
        Mask with 0x0F (00001111): Keeps the lower 4 bits and clears the upper 4 bits.
        Mask with 0xFF (11111111): Keeps all the bits in a byte

        in a word 11111111 11111111 = 16 bits
        Masking a Word with 0xFF (0x00FF)
        Hexadecimal 0xFF: In binary, this is 00000000 11111111.
        Effect on a Word: A word usually consists of 16 bits (2 bytes). 
        When you mask a 16-bit word with 0xFF, it only keeps the lower 8 bits (the lower byte) and sets the upper 8 bits to zero.
        result with above would be 00000000 11111111

        Masking a Word with 0xFF00
        Hexadecimal 0xFF00: In binary, this is 11111111 00000000.
        Effect on a Word: When you mask a 16-bit word with 0xFF00, you keep the upper 8 bits (the upper byte) and clear the lower 8 bits (the lower byte).
        result with above would be 11111111 00000000


        Left Shift (<<):

        When you left-shift a number, you're effectively multiplying it by 2^𝑛
        where n is the number of positions you shift.
        For example:
        Shifting by 1 (i.e., << 1) multiplies the number by 2.
        Shifting by 2 (i.e., << 2) multiplies the number by 4.
        Shifting by 4 (i.e., << 4) multiplies the number by 16
        Shifting by 8 (i.e., << 8) multiplies the number by 256

        Right Shift (>>):
        When you right-shift a number, you move the bits to the right, which divides the number by powers of two.
        For example:
        Right shifting by 1 (i.e., >> 1) divides the number by 2.
        Right shifting by 2 (i.e., >> 2) divides the number by 4.
        Right shifting by 4 (i.e., >> 4) divides the number by 16.
         */

        protected static char[] lHexaString = new char[] {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f'
        };

        public const int BIG_ENDIAN = 1;
        public const int LITTLE_ENDIAN = 2;
        public const int UNKNOWN_ENDIAN = 3;

        public const bool SIGNED = true;
        public const bool UNSIGNED = false;

        /// <summary>
        /// A byte is 2 nibbles
        /// masks the lower 4 bits to zero, keeps the upper 4 bits
        /// bit-shifts >> 4 the upper 4 bits to the lower part
        /// and returns as a byte
        /// 1111 0001 would be masked to 1111 0000 and then shifted to 
        /// 0000 1111 which is the returned value
        /// </summary>
        /// <param name="pByte"></param>
        /// <returns></returns>
        public static byte GetHighNibble(byte pByte)
        {
            return (byte)((pByte & 0xF0) >> 4);
        }

        /// <summary>
        /// masks the upper part of a byte and returns the value
        /// 1111 0001 would be returned as 0000 0001
        /// </summary>
        /// <param name="pByte"></param>
        /// <returns></returns>
        public static byte GetLowNibble(byte pByte)
        {
            return (byte)(pByte & 0xF);
        }

        public static byte GetNibble(byte pByte, bool pIsHighNibble)
        {
            return pIsHighNibble ? GetHighNibble(pByte) : GetLowNibble(pByte);
        }

        public static int BytesToInt(byte[] b, int pEndianMode)
        {
            int lResult = 0;
            if (pEndianMode == BIG_ENDIAN)
            {
                for (int i = 0; i < b.Length; i++)
                    lResult += (b[i] & 0xFF) << 8 * (b.Length - 1 - i);
            }
            else
            {
                for (int i = 0; i < b.Length; i++)
                    lResult += (b[i] & 0xFF) << 8 * i;
            }
            return lResult;
        }

        public static int BytesToInt(byte b1, byte b2, byte b3, byte b4, int pEndianMode)
        {
            if (pEndianMode == BIG_ENDIAN)
                return ((b1 & 0xFF) << 24) + ((b2 & 0xFF) << 16) + ((b3 & 0xFF) << 8) + (b4 & 0xFF);

            //little endian
            return ((b4 & 0xFF) << 24) + ((b3 & 0xFF) << 16) + ((b2 & 0xFF) << 8) + (b1 & 0xFF);
        }

        public static int BytesToInt(byte b1, byte b2, int pEndianMode)
        {
            return BytesToInt(b1, b2, pEndianMode, false);
        }

        /// <summary>
        /// If the endian mode is set to BIG_ENDIAN:
        /// Shift the first byte (b1) 8 bits to the left to position it as the high byte.
        /// Combine it with the second byte (b2) by adding them together.
        /// Return the combined value as an integer.
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <param name="pEndianMode"></param>
        /// <param name="pIsSigned"></param>
        /// <returns></returns>
        public static int BytesToInt(byte b1, byte b2, int pEndianMode, bool pIsSigned)
        {
            if (pEndianMode == BIG_ENDIAN)
            {
                if (pIsSigned)
                    return (b1 << 8) + b2;
                return ((b1 & 0xFF) << 8) + (b2 & 0xFF);
            }
            if (pIsSigned)
                return (b2 << 8) + b1;
            return ((b2 & 0xFF) << 8) + (b1 & 0xFF);
        }

        public static int BytesToInt(byte b1, byte b2)
        {
            return ((b1 & 0xFF) << 8) + (b2 & 0xFF);
        }

        public static int ByteToInt(byte b1)
        {
            return b1 & 0xFF;
        }

        public static int NibblesToByte(byte n1, byte n2, int pEndianMode)
        {
            if (pEndianMode == 1)
                return ((n1 & 0xF) << 4) + (n2 & 0xF);
            return ((n2 & 0xF) << 4) + (n1 & 0xF);
        }

        public static byte ReadByte(Stream pStream)
        {
            byte[] lBuffer = new byte[1];
            int lBytesRead = pStream.Read(lBuffer, 0, 1);
            if (lBytesRead != 1)
                throw new IOException();
            return lBuffer[0];
        }

        public static string ToChars(byte[] b, int pOffset, int pLen)
        {
            StringBuilder lResult = new StringBuilder();
            for (int i = pOffset; i < pOffset + pLen; i++)
            {
                if (b[i] < 32)
                {
                    lResult.Append(' ');
                }
                else
                {
                    lResult.Append((char)(b[i] & 0xFF));
                }
            }
            return lResult.ToString();
        }

        public static string ToChar(byte b)
        {
            return "" + (char)(b & 0xFF);
        }

        public static string ToString(byte[] pBytes)
        {
            if (pBytes == null)
                return "";
            StringBuilder lString = new StringBuilder(pBytes.Length * 2);
            for (int i = 0; i < pBytes.Length; i++)
            {
                int lByte = pBytes[i] & 0xFF;
                lString.Append(lHexaString[lByte >> 4]);
                lString.Append(lHexaString[lByte & 0xF]);
            }
            return lString.ToString().ToUpper();
        }

        public static string ToString(int[] pBytes)
        {
            if (pBytes == null)
                return "";
            StringBuilder lString = new StringBuilder(pBytes.Length * 2);
            for (int i = 0; i < pBytes.Length; i++)
            {
                int lByte = pBytes[i] & 0xFF;
                lString.Append(lHexaString[lByte >> 4]);
                lString.Append(lHexaString[lByte & 0xF]);
            }
            return lString.ToString().ToUpper();
        }

        public static string ToBinaryString(byte b1)
        {
            StringBuilder lResult = new StringBuilder();
            lResult.Append((b1 & 0x80) == 128 ? "1" : "0");
            lResult.Append((b1 & 0x40) == 64 ? "1" : "0");
            lResult.Append((b1 & 0x20) == 32 ? "1" : "0");
            lResult.Append((b1 & 0x10) == 16 ? "1" : "0");
            lResult.Append((b1 & 0x8) == 8 ? "1" : "0");
            lResult.Append((b1 & 0x4) == 4 ? "1" : "0");
            lResult.Append((b1 & 0x2) == 2 ? "1" : "0");
            lResult.Append((b1 & 0x1) == 1 ? "1" : "0");
            return lResult.ToString();
        }

        public static string ToString(byte pByte)
        {
            StringBuilder lString = new StringBuilder(2);
            int lByte = pByte & 0xFF;
            lString.Append(lHexaString[lByte >> 4]);
            lString.Append(lHexaString[lByte & 0xF]);
            return lString.ToString().ToUpper();
        }

        public static byte ReverseOrder(byte pByte)
        {
            return (byte)((pByte >> 0 & 0x1) << 7 | (pByte >> 1 & 0x1) << 6 | (pByte >> 2 & 0x1) << 5 | (pByte >> 3 & 0x1) << 4 | (pByte >> 4 & 0x1) << 3 | (pByte >> 5 & 0x1) << 2 | (pByte >> 6 & 0x1) << 1 | (pByte >> 7 & 0x1) << 0);
        }

        public static byte ReverseOrder4b(byte pByte)
        {
            return (byte)((pByte << 4 & 0xF0) | (pByte >> 4 & 0xF));
        }

        public static string IntAs2BytesString(int pWord, int pEndianMode)
        {
            byte lByte1 = (byte)(pWord & 0xFF);
            byte lByte2 = (byte)((pWord >> 8) & 0xFF);
            return pEndianMode == 1 ? ToString(lByte2) + ToString(lByte1) : ToString(lByte1) + ToString(lByte2);
        }

        public static byte GetByteFromWord(int pWord, bool pIsHighByte)
        {
            byte lByte1 = (byte)(pWord & 0xFF);
            byte lByte2 = (byte)((pWord >> 8) & 0xFF);
            return pIsHighByte ? lByte2 : lByte1;
        }

        public static byte GetByteFromWord(int pWord, int pEndianMode, bool pIsFirstByte)
        {
            byte lByte1;
            byte lByte2;
            if (pEndianMode == 2)
            {
                lByte1 = (byte)(pWord & 0xFF);
                lByte2 = (byte)((pWord >> 8) & 0xFF);
            }
            else
            {
                lByte2 = (byte)(pWord & 0xFF);
                lByte1 = (byte)((pWord >> 8) & 0xFF);
            }
            return pIsFirstByte ? lByte1 : lByte2;
        }

        public static byte[] BytesToBytes(byte[] pBytes, int pStartIndex, int pLength)
        {
            byte[] lBytes = new byte[pLength];
            Array.Copy(pBytes, pStartIndex, lBytes, 0, lBytes.Length);
            return lBytes;
        }

        public static int[] BytesToInts(byte[] pBytes, int pStartIndex, int pLength)
        {
            int[] lInts = new int[pLength];
            for (int i = 0; i < lInts.Length; i++)
                lInts[i] = pBytes[pStartIndex + i] & 0xFF;
            return lInts;
        }

        public static byte[] WordToBytes(int pWord, int pEndianMode)
        {
            int[] lWord = new int[1];
            lWord[0] = pWord;
            return WordsToBytes(lWord, 0, 1, pEndianMode);
        }

        public static byte[] WordsToBytes(int[] pWords, int pStartIndex, int pLength, int pEndianMode)
        {
            byte[] lArray_bytes = new byte[pLength * 2];
            for (int i = 0; i < pLength; i++)
            {
                lArray_bytes[i * 2] = GetByteFromWord(pWords[i + pStartIndex], pEndianMode, true);
                lArray_bytes[i * 2 + 1] = GetByteFromWord(pWords[i + pStartIndex], pEndianMode, false);
            }
            return lArray_bytes;
        }

        public static byte[] DwordToBytes(int pDWord, int pEndianMode)
        {
            int[] lDWord = new int[1];
            lDWord[0] = pDWord;
            return DwordsToBytes(lDWord, 0, 1, pEndianMode);
        }

        public static byte[] DwordsToBytes(int[] pDWords, int pStartIndex, int pLength, int pEndianMode)
        {
            byte[] lArray_bytes = new byte[pLength * 4];
            for (int i = 0; i < pLength; i++)
            {
                byte b1 = (byte)(pDWords[i + pStartIndex] >> 0 & 0xFF);
                byte b2 = (byte)(pDWords[i + pStartIndex] >> 8 & 0xFF);
                byte b3 = (byte)(pDWords[i + pStartIndex] >> 16 & 0xFF);
                byte b4 = (byte)(pDWords[i + pStartIndex] >> 24 & 0xFF);
                lArray_bytes[i * 2 + 0] = (pEndianMode == 2) ? b1 : b4;
                lArray_bytes[i * 2 + 1] = (pEndianMode == 2) ? b2 : b3;
                lArray_bytes[i * 2 + 2] = (pEndianMode == 2) ? b3 : b2;
                lArray_bytes[i * 2 + 3] = (pEndianMode == 2) ? b4 : b1;
            }
            return lArray_bytes;
        }
    }
}

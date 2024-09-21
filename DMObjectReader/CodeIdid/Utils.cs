﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader
{
    public class Utils
    {
        // Install-Package Microsoft.VisualBasic

        public int BitsInBuffer, LZWByteIndex, BitBuffer;
        public MemoryStream itemdata;
        public int counter = 0;
        public Color[,] gpalettes = new Color[15,255]; //2d array instead of array of arrays https://stackoverflow.com/questions/12567329/multidimensional-array-vs
        public Color[,] ginvertedpalettes = new Color[15, 255];
        public int gPALETTE_MAIN;
        public int gPALETTE_TITLE;
        public int gPALETTE_MAINMENU;
        public int gPALETTE_CREDITS;
        public int gPALETTE_CUSTOM;
        public int curPalette;

        public int GetNextLZWCode(int n)
        {
            int GetNextLZWCodeRet = default;
            while (BitsInBuffer < n & LZWByteIndex < itemdata.Length) // If there are enough bits in the buffer to make a code
            {
                itemdata.Position = LZWByteIndex;
                BitBuffer = BitBuffer + itemdata.ReadByte() * (int)Math.Pow(2, BitsInBuffer); // Add the next character to the left of the BitBuffer (hence the shift to the left)
                BitsInBuffer = BitsInBuffer + 8;             // 8 bits were added to the buffer
                LZWByteIndex = LZWByteIndex + 1;             // Go to the next input character
            }
            // MsgBox     "BitsInBuffer: " & BitsInBuffer & vbCrLf & _
            // "n: " & n & vbCrLf & _
            // "LZWByteIndex: " & LZWByteIndex & vbCrLf & _
            // "Len(ItemData): " & Len(ItemData) & vbCrLf & _
            // "BitBuffer: " & BitBuffer

            if (BitsInBuffer >= n)               // If there are enough bits in the buffer to make a code
            {
                GetNextLZWCodeRet = BitBuffer & (int)(Math.Pow(2d, n) - 1d);  // Add to the list of codes the code made with the BitsInCode most significant bits in the buffer
                BitBuffer = BitBuffer / (int)(Math.Pow(2d, n));         // Remove the output bits from the buffer by shifting the buffer to the right
                BitsInBuffer = BitsInBuffer - n;     // The number of bits in the buffer is decreased
            }
            else
            {
                GetNextLZWCodeRet = -1;
            }

            return GetNextLZWCodeRet;
            // MsgBox "NextLZWCode Is: " & GetNextLZWCode
        }

        public MemoryStream LZWUncompress()
        {
            MemoryStream LZWUncompressRet = default;
            string s = LZWUncompress_Stro();
            var w = new MemoryStream();
            int x;
            var loopTo = s.Length - 1;
            for (x = 0; x <= loopTo; x++)
                w.WriteByte((byte)Strings.AscW(s[x]));
            // Move file pointer to begin of file!
            w.Position = 0L;
            LZWUncompressRet = w;
            return LZWUncompressRet;
        }

        public string LZWUncompress_Stro()
        {
            string LZWUncompress_StroRet = default;
            var Dictionary = new SortedDictionary<int, string>();
            int i;
            string j;
            int k;
            string l;
            string LZWString;
            string LZWChar;
            int BitsInCode;
            int OldCode;
            int NewCode;
            string a;
            string b;
            string c;
            int d;
            int x;
            // Initialize LZW bit buffer
            BitBuffer = 0;           // The BitBuffer is empty
            BitsInBuffer = 0;        // So there are no bits stored in the buffer
            LZWByteIndex = 0;        // Start uncompressing LZW data at the first character in string
            BitsInCode = 9;
            d = 0;

            LZWUncompress_StroRet = "";
            if (itemdata.Length > 1)       // We need at least one 9 bits code, which means at least two bytes
            {
                // Create and populate the dictionary for the LZW algorithm
                for (i = 0; i <= 255; i++)
                    Dictionary.Add(Dictionary.Count, (Strings.ChrW(i)).ToString());
                Dictionary.Add(256, "Flush Dictionary");
                d = Dictionary.Count;
                OldCode = GetNextLZWCode(BitsInCode);                // Get the first code
                LZWUncompress_StroRet = Dictionary[OldCode];            // Output the string (which is a character) associated with the first code
                LZWChar = Dictionary[OldCode];              // Get the string associated with the first code
                NewCode = GetNextLZWCode(BitsInCode);
                while (NewCode != -1)
                {
                    if (NewCode != 256)                  // If the code is not the special value 256
                    {
                        if (Dictionary.ContainsKey(NewCode))      // If the current code is already in the dictionary
                        {
                            LZWString = Dictionary[NewCode];    // Get the string associated with that current code
                        }
                        else
                        {
                            LZWString = Dictionary[OldCode];    // Get the string associated with the previously read code
                            LZWString = LZWString + LZWChar;
                        }     // Add the latest input character read to the string
                        LZWUncompress_StroRet = LZWUncompress_StroRet + LZWString;   // Output the string
                        LZWChar = Strings.Left(LZWString, 1);            // Get the first character of the string
                        Dictionary.Add(d, Dictionary[OldCode] + LZWChar);  // Add the new string to the dictionary
                        d = d + 1;
                        if (BitsInCode < 12)
                        {
                            if (d == Math.Pow(2d, BitsInCode))   // If there are not anymore enough bits in a code to store the code's value
                            {
                                BitsInCode = BitsInCode + 1;     // Add a bit to the code length
                                                                 // MsgBox "BitsInCode increased and is now: " & BitsInCode
                            }
                        }

                        OldCode = NewCode;               // Go on to next code
                    }
                    else
                    {
                        // MsgBox "Code 256 found, flushing dictionary...", vbOKOnly + vbInformation, "Information"
                        Dictionary.Clear();
                        for (i = 0; i <= 255; i++)
                            Dictionary.Add(Dictionary.Count, (Strings.ChrW(i)).ToString());
                        Dictionary.Add(256, "Flush Dictionary");
                        d = Dictionary.Count;
                        BitsInCode = 9;
                    }
                    NewCode = GetNextLZWCode(BitsInCode);
                }

                i = 1;
                while (i < LZWUncompress_StroRet.Length)
                {
                    j = Strings.Mid(LZWUncompress_StroRet, i, 1);
                    if (Strings.AscW(j) == 144)
                    {
                        App.dprint("found 0x90 byte");
                        k = Strings.AscW(Strings.Mid(LZWUncompress_StroRet, i + 1, 1));
                        //int k2 = (int)LZWUncompress_StroRet[i].ToString().ToCharArray()[0];
                        if (k == 0)
                        {
                            App.dprint("item in lzw if k=0 statement");
                            //middle ground
                            //File.WriteAllText(counter.ToString() + ".dat", LZWUncompress_StroRet);

                            // Remove the '00h' byte
                            // LZWUncompress_Stro = LeftB(LZWUncompress_Stro, i) + MidB(LZWUncompress_Stro, i + 2)
                            LZWUncompress_StroRet = LZWUncompress_StroRet.Substring(0, i + 1) + LZWUncompress_StroRet.Substring(i + 2);

                            ////middle ground
                            //File.WriteAllText(counter.ToString() + ".dat", LZWUncompress_StroRet);
                            //    counter += 1;

                            i = i + 1;
                        }
                        else
                        {
                            App.dprint("item in lzw else statement (k=" + k.ToString() + ")");
                            l = Strings.Mid(LZWUncompress_StroRet, i - 1, 1);
                            a = Strings.Left(LZWUncompress_StroRet, i);

                            b = "";
                            var loopTo = k - 1;
                            for (x = 1; x <= loopTo; x++)
                                b = b + l;
                            if ((b).Length != k - 1)
                            {
                                Interaction.MsgBox("lenb(b) != k-1 (" + (b).Length.ToString() + " != " + (k - 1).ToString() + ")");
                            }
                            // b = String(k - 1, AscB(ChrB(l)))
                            c = Strings.Right(LZWUncompress_StroRet, LZWUncompress_StroRet.Length - i - 1);
                            // c = MidB(LZWUncompress_Stro, i + 2)
                            LZWUncompress_StroRet = a + b + c;
                            i = i + k - 1;
                        }
                    }
                    else
                    {
                        i = i + 1;
                    }
                }
            }
            int test = LZWUncompress_StroRet.Length;
            return LZWUncompress_StroRet;
        }

        

        public void write_long(int num, Stream f135) 
        {
            int b2, b1, b3;
            Byte b4;
            int l1;
            //'l1 = num And 4278190080#
            l1 = num;
            l1 = l1 / 0x1000000;
            if (l1 < 0) 
            {
                l1 = l1 + 256;
            }
            b1 = l1;
            l1 = num & 16711680;
            b2 = l1 / 0x10000;
            l1 = num & 65280;
            b3 = l1 / 0x100;
            b4 = (byte)(num & 255);
            write_byte(b4, f135);
            write_byte((byte)b3, f135);
            write_byte((byte)b2, f135);
            write_byte((byte)b1, f135);
        }
        public void write_byte(Byte num, Stream f135)
        {

            f135.WriteByte(num);
        }
        public void write_word(short num, Stream f135)
        {
            Byte b1;
            Byte b2;
            int l1;
            l1 = (num & 0xFF00);
            b1 = (byte)(l1 / 0x100);
            b2 = (byte)(num & 0xFF);
            write_byte(b2, f135);
            write_byte(b1, f135);
        }
        public byte SreadByte(MemoryStream data, int offset1Based)
        {
            data.Position = offset1Based - 1;
            byte result = new BinaryReader(data).ReadByte();
            return result;
        }
        public int SreadWord(MemoryStream data, int offset1Based)
        {
            int _sreadWord = SreadByte(data, offset1Based) * 256;
            _sreadWord += SreadByte(data, offset1Based + 1);
            return _sreadWord;
        }
    }
}

//public static class App
//{
//    public static void dprint(string a) { }
//}

public static class Interaction
{
    public static void MsgBox(string a) { }
}

public static class Strings
{
    /// <summary>
    /// Takes a char or string and returns the int representation of that number
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static int AscW(char input)
    {
        //var first = Microsoft.VisualBasic.Strings.AscW(input);
        var second = Convert.ToInt32(input);
        return second;
    }
    public static int AscW(string input, int num)
    {
        //var first = Microsoft.VisualBasic.Strings.AscW(input);
        var second = Convert.ToInt32(input[num]);
        return second;
    }

    public static int AscW(string input)
    {
        //var first = Microsoft.VisualBasic.Strings.AscW(input);
        //if (input.Length == 0)
        //    return 0;
        var second = Convert.ToInt32(input[0]);
        return second;
    }

    public static char ChrW(int input)
    {
        var MyKeyChr = char.ConvertFromUtf32(input);
        //System.Text.ASCIIEncoding.
        //var another = Microsoft.VisualBasic.Strings.ChrW(input);
        var third = (char)input;
        return third;
    }


    public static string Left(string input, int nCount)
    {
        return input.Substring(0, nCount);
    }

    public static string Mid(string input, int index, int nCount)
    {
        return input.Substring(index, nCount);
    }

    public static string Right(string input, int nCount)
    {
        return input.Substring(input.Length - nCount, nCount);
    }

}

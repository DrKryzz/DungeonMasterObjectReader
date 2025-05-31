using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.AI_Code
{
    /*
     Är detta standard-LZW?
Nej. Det är en modifierad LZW:

Funktion	Standard LZW	Din kod
Kodlängd ökar från 9 till 12 bitar	✅	✅
Flush-dictionary kod (256)	❌ (GIF-only)	✅
0x90 escape-byte hantering	❌	✅
Inläsning via bitbuffert	✅	✅
Output till string via ordbok	✅	✅
     */
    //Första versionen av LZW-dekomprimering med 0x90-escape
    public class LZWDecompressor
    {
        private byte[] data;
        private int byteIndex;
        private int bitBuffer;
        private int bitsInBuffer;

        public LZWDecompressor(byte[] input)
        {
            data = input;
            byteIndex = 0;
            bitBuffer = 0;
            bitsInBuffer = 0;
        }

        /// <summary>
        /// Läser nästa LZW-kod från bitbufferten (n bitar).
        /// </summary>
        private int GetNextCode(int n)
        {
            while (bitsInBuffer < n && byteIndex < data.Length)
            {
                bitBuffer |= data[byteIndex++] << bitsInBuffer;
                bitsInBuffer += 8;
            }

            if (bitsInBuffer < n)
                return -1; // Slut på data

            int code = bitBuffer & ((1 << n) - 1);
            bitBuffer >>= n;
            bitsInBuffer -= n;
            return code;
        }

        /// <summary>
        /// Utför LZW-dekomprimering (inklusive 0x90 specialbehandling).
        /// </summary>
        public string Decompress()
        {
            var dict = new Dictionary<int, string>();
            for (int i = 0; i < 256; i++)
                dict[i] = ((char)i).ToString();

            const int flushCode = 256;
            dict[flushCode] = "FLUSH";

            int bitsInCode = 9;
            int nextCode = flushCode + 1;

            int oldCode = GetNextCode(bitsInCode);
            if (oldCode == -1)
                return "";

            string result = dict[oldCode];
            string lzwChar = result.Substring(0, 1);

            int newCode;
            while ((newCode = GetNextCode(bitsInCode)) != -1)
            {
                if (newCode == flushCode)
                {
                    dict.Clear();
                    for (int i = 0; i < 256; i++)
                        dict[i] = ((char)i).ToString();
                    dict[flushCode] = "FLUSH";
                    bitsInCode = 9;
                    nextCode = flushCode + 1;
                    oldCode = GetNextCode(bitsInCode);
                    if (oldCode == -1)
                        break;
                    result += dict[oldCode];
                    lzwChar = dict[oldCode].Substring(0, 1);
                    continue;
                }

                string entry;
                if (dict.ContainsKey(newCode))
                {
                    entry = dict[newCode];
                }
                else
                {
                    entry = dict[oldCode] + lzwChar;
                }

                result += entry;
                lzwChar = entry.Substring(0, 1);

                if (nextCode < 4096)
                {
                    dict[nextCode++] = dict[oldCode] + lzwChar;
                    if (nextCode == (1 << bitsInCode) && bitsInCode < 12)
                        bitsInCode++;
                }

                oldCode = newCode;
            }

            // Efterbehandling för 0x90
            result = Postprocess90(result);

            return result;
        }

        /// <summary>
        /// Hanterar 0x90 (144) escape-sekvenser enligt den specialvariant du har.
        /// </summary>
        private string Postprocess90(string input)
        {
            var output = new System.Text.StringBuilder();
            int i = 0;
            while (i < input.Length)
            {
                char ch = input[i];
                if (ch == (char)0x90 && i + 1 < input.Length)
                {
                    int k = input[i + 1];
                    if (k == 0)
                    {
                        output.Append((char)0x90);
                        i += 2;
                    }
                    else
                    {
                        char prev = output.Length > 0 ? output[output.Length - 1] : '\0'; //char prev = output.Length > 0 ? output[^1] : '\0';
                        for (int j = 0; j < k - 1; j++)
                            output.Append(prev);
                        i += 2;
                    }
                }
                else
                {
                    output.Append(ch);
                    i++;
                }
            }
            return output.ToString();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.AI_Code
{
    public class LZWUncompressor
    {
        private Dictionary<int, string> dict;
        private int bitsInCode;
        private int nextCode;
        private int bitBuffer;
        private int bitsInBuffer;
        private int byteIndex;
        private byte[] input;

        public LZWUncompressor()
        {
            dict = new Dictionary<int, string>();
        }

        public string Uncompress(byte[] compressed)
        {
            // Avkodning av 0x90-escape
            input = Remove90Escape(compressed);

            bitBuffer = 0;
            bitsInBuffer = 0;
            byteIndex = 0;

            InitializeDictionary();

            int oldCode = ReadCode(bitsInCode);
            if (oldCode == -1) return "";

            string result = dict[oldCode];
            string w = result;

            int newCode;
            while ((newCode = ReadCode(bitsInCode)) != -1)
            {
                if (newCode == 256)
                {
                    InitializeDictionary();
                    oldCode = ReadCode(bitsInCode);
                    if (oldCode == -1) break;
                    result += dict[oldCode];
                    w = dict[oldCode];
                    continue;
                }

                string entry;
                if (dict.ContainsKey(newCode))
                {
                    entry = dict[newCode];
                }
                else
                {
                    entry = w + w[0];
                }

                result += entry;

                if (nextCode < 4096)
                {
                    dict[nextCode++] = w + entry[0];
                    if (nextCode == (1 << bitsInCode) && bitsInCode < 12)
                        bitsInCode++;
                }

                w = entry;
                oldCode = newCode;
            }

            return result;
        }

        private void InitializeDictionary()
        {
            dict.Clear();
            for (int i = 0; i < 256; i++)
                dict[i] = ((char)i).ToString();
            dict[256] = "FLUSH";
            nextCode = 257;
            bitsInCode = 9;
        }

        private int ReadCode(int n)
        {
            while (bitsInBuffer < n && byteIndex < input.Length)
            {
                bitBuffer |= (input[byteIndex++] << bitsInBuffer);
                bitsInBuffer += 8;
            }

            if (bitsInBuffer >= n)
            {
                int code = bitBuffer & ((1 << n) - 1);
                bitBuffer >>= n;
                bitsInBuffer -= n;
                return code;
            }
            else
            {
                return -1;
            }
        }

        private byte[] Remove90Escape(byte[] data)
        {
            List<byte> clean = new List<byte>();
            for (int i = 0; i < data.Length; i++)
            {
                clean.Add(data[i]);
                if (data[i] == 0x90)
                {
                    if (i + 1 < data.Length && data[i + 1] == 0x00)
                    {
                        i++;  // Hoppa över extrabyte
                    }
                }
            }
            return clean.ToArray();
        }
    }

}

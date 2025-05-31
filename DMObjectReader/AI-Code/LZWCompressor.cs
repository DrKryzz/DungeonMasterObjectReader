using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.AI_Code
{
    public class LZWCompressor
    {
        private Dictionary<string, int> dict;
        private int bitsInCode;
        private int nextCode;
        private int bitBuffer;
        private int bitsInBuffer;
        private List<byte> output;

        public LZWCompressor()
        {
            dict = new Dictionary<string, int>();
            output = new List<byte>();
        }

        public byte[] Compress(string input)
        {
            // Initiera dictionary
            InitializeDictionary();

            string w = "";

            foreach (char c in input)
            {
                string wc = w + c;
                if (dict.ContainsKey(wc))
                {
                    w = wc;
                }
                else
                {
                    WriteCode(dict[w], bitsInCode);

                    // Lägg till nytt kodord
                    if (nextCode < 4096)
                    {
                        dict[wc] = nextCode++;
                        if (nextCode == (1 << bitsInCode) && bitsInCode < 12)
                            bitsInCode++;
                    }

                    // Om dictionary är fullt (4096) så flushar vi (kod 256)
                    if (nextCode >= 4096)
                    {
                        WriteCode(256, bitsInCode);  // Flush dictionary
                        InitializeDictionary();
                    }

                    w = c.ToString();
                }
            }

            if (!string.IsNullOrEmpty(w))
            {
                WriteCode(dict[w], bitsInCode);
            }

            // (Valfritt) Skriv flush-kod för att markera dictionary-reset (igen)
            // WriteCode(256, bitsInCode);

            FlushBuffer();

            // Efter att vi byggt output: specialhantering av 0x90
            output = Apply90Escape(output);

            return output.ToArray();
        }

        private void InitializeDictionary()
        {
            dict.Clear();
            for (int i = 0; i < 256; i++)
                dict[((char)i).ToString()] = i;
            dict["FLUSH"] = 256;
            nextCode = 257;
            bitsInCode = 9;
        }

        private void WriteCode(int code, int n)
        {
            bitBuffer |= (code << bitsInBuffer);
            bitsInBuffer += n;

            while (bitsInBuffer >= 8)
            {
                output.Add((byte)(bitBuffer & 0xFF));
                bitBuffer >>= 8;
                bitsInBuffer -= 8;
            }
        }

        private void FlushBuffer()
        {
            if (bitsInBuffer > 0)
            {
                output.Add((byte)(bitBuffer & 0xFF));
                bitBuffer = 0;
                bitsInBuffer = 0;
            }
        }

        private List<byte> Apply90Escape(List<byte> data)
        {
            var escaped = new List<byte>();
            for (int i = 0; i < data.Count; i++)
            {
                escaped.Add(data[i]);
                if (data[i] == 0x90)
                {
                    // Lägg till en '00'-byte efter varje 0x90
                    escaped.Add(0x00);
                }
            }
            return escaped;
        }
    }

}

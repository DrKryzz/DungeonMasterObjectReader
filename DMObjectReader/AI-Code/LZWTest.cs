using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.AI_Code
{
    [TestClass]
    public static class LZWTest
    {
        public static void Run()
        {
            string original = "ABABABAABABABAABABABAABABABAABABABAABABABAABABABAABABABAABABABA";
            Console.WriteLine("Original text:");
            Console.WriteLine(original);

            var compressor = new LZWCompressor();
            var compressed = compressor.Compress(original);
            Console.WriteLine($"Compressed length: {compressed.Length} bytes");

            var decompressor = new LZWUncompressor();
            string decompressed = decompressor.Uncompress(compressed);

            Console.WriteLine("Decompressed text:");
            Console.WriteLine(decompressed);

            Console.WriteLine("Match: " + (original == decompressed));
        }
    }

}

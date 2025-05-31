using DMObjectReader.AI_Code;

namespace DMObjectReaderTest
{
    [TestClass]
    public sealed class LzwTest
    {
        [TestMethod]
        public void TestMethod1()
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

            Assert.AreEqual(original, decompressed, "Decompressed text does not match original text.");
        }
    }
}

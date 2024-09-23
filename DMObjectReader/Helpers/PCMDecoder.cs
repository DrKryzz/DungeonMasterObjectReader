using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DMObjectReader.Helpers
{


    public class PCMDecoder
    {
        // Method to read a word in big-endian format from a stream
        private static ushort ReadBigEndianWord(BinaryReader reader)
        {
            // Read two bytes and combine them in big-endian format
            byte highByte = reader.ReadByte();
            byte lowByte = reader.ReadByte();
            return (ushort)((highByte << 8) | lowByte);
        }

        // Method to decode the PCM sound data
        public static List<byte> DecodePCMData(Stream input)
        {
            using (BinaryReader reader = new BinaryReader(input))
            {
                // Read the number of sound samples from the header (1 word, big-endian)
                ushort numberOfSamples = ReadBigEndianWord(reader);

                List<byte> samples = new List<byte>();
                byte previousSample = 0;

                while (samples.Count < numberOfSamples)
                {
                    // Read the next nibble (4 bits)
                    byte nibble = ReadNibble(reader);

                    if (nibble != 0)
                    {
                        // If nibble is not 0, it's the new sample
                        previousSample = nibble;
                        samples.Add(previousSample);
                    }
                    else
                    {
                        // If nibble is 0, we need to decode the RepeatCount
                        int repeatCount = 0;
                        bool continueReading = true;

                        // Decode RepeatCount in a loop, reading 3 bits from each nibble
                        while (continueReading)
                        {
                            // Read the next nibble
                            nibble = ReadNibble(reader);

                            // The most significant bit indicates if we should continue reading
                            continueReading = (nibble & 0x8) != 0;

                            // Extract the lower 3 bits and append them to RepeatCount
                            repeatCount = (repeatCount << 3) | (nibble & 0x7);
                        }

                        // Minimum repeat count is 3
                        repeatCount += 3;

                        // Repeat the last sample RepeatCount times
                        for (int i = 0; i < repeatCount; i++)
                        {
                            samples.Add(previousSample);
                        }
                    }
                }

                return samples;
            }
        }

        // Method to read a nibble (4 bits) from the stream
        private static byte ReadNibble(BinaryReader reader)
        {
            // Read a byte and extract the nibbles alternately
            int byteRead = reader.ReadByte();
            if (byteRead == -1) throw new EndOfStreamException("End of stream reached.");

            // Keep track of whether we're reading the high or low nibble
            bool readHighNibble = true;

            return readHighNibble ? (byte)((byteRead & 0xF0) >> 4) : (byte)(byteRead & 0x0F);
        }

        //public static void Main()
        //{
        //    // Example usage of the DecodePCMData method
        //    // Here, replace the stream with your actual data source
        //    byte[] pcmData = { /* Add your PCM data bytes here */ };
        //    using (MemoryStream ms = new MemoryStream(pcmData))
        //    {
        //        List<byte> decodedSamples = DecodePCMData(ms);

        //        // Print the decoded samples
        //        Console.WriteLine("Decoded Samples:");
        //        foreach (byte sample in decodedSamples)
        //        {
        //            Console.Write($"{sample} ");
        //        }
        //    }
        //}
    }

}

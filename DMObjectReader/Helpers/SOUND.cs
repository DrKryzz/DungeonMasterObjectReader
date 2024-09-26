using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.Helpers
{
    //ADGE SOUND
    public class SOUND
    {
        /*
The number of bytes in a "word" depends on the architecture of the system:
16-bit architecture: A word is 2 bytes (16 bits).
32-bit architecture: A word is 4 bytes (32 bits).
64-bit architecture: A word is 8 bytes (64 bits).

Big-Endian Format
Definition: In big-endian format, the most significant byte (the "big end") is stored at the lowest memory address. 
In other words, the byte that represents the highest value is stored first.
Example: If you have the 16-bit hexadecimal value 0x1234, it would be stored in memory as:

Address  | Value
----------------
0x00     | 0x12  (most significant byte)
0x01     | 0x34  (least significant byte)

Little-Endian Format
In little-endian format, the least significant byte is stored at the lowest memory address. 
So, using the same example 0x1234, it would be stored as:

Address  | Value
----------------
0x00     | 0x34  (least significant byte)
0x01     | 0x12  (most significant byte)

ATARI = 16 bit for a word = 4 Nibble = 2 bytes
Byte = 8 bit = 2 nibbles
Nibble = 4 bit = 1/2 byte


Example
If b1 = 0x12 and b2 = 0x34, the integer returned would be:
(0x12 << 8) + 0x34 = 0x1200 + 0x34 = 0x1234
This ensures the bytes are interpreted in big-endian order, with b1 as the most significant byte.
bit shifting << 8 is the same as multiplying the value with 256.

Header
1 word (big endian): Number of sound samples
Data
The sound is encoded as PCM, 4 bit, mono, unsigned and is compressed with an RLE algorithm. Here is how to decode these sounds:

Read Nibble
If Nibble <> 0 Then Sample = Nibble
If Nibble = 0 Then
The previously read sample has to be repeated a certain number of times called RepeatCount. That number must be 3 at minimum. 
The value of RepeatCount is encoded in a variable number of nibbles, each of them containing 3 bits of the binary representation of RepeatCount as their least significant bits. 
The most significant bit is set to 1 in each of these nibbles except in the last nibble used to store the RepeatCount value which is marking the end of the cyle.
So each time you read a nibble for RepeatCount, 
you have to shift the bits of RepeatCount by three bits to the left and insert the three less significant bits of the last read nibble to the right 
until the most significant bit of the last read nibble is 0. 
You then need to add 3 to RepeatCount and copy the last sample read RepeatCount times.
Repeat that process until there are no more nibbles to read

Note: if you want to convert the sound data to PCM 8 bit sound, you need to multiply each sample value by 17 --used to be 2^4 (16).

Playback rate: 6000 Hz.
         */
        public short word_header_nrOfSamples; //nr of sound samples in this sound snd1 = Big Endian
        public byte byte2, byte3, byte4, byte5, byte6, byte7;
        private MemoryStream itemdata;

        public SOUND()
        {

        }
        public SOUND(MemoryStream _itemdata)
        {
            itemdata = _itemdata;
        }

        public byte[] GetPCM8BitMono()
        {
            itemdata.Position = 0; //reset position
            List<byte> returns = new List<byte>();
            word_header_nrOfSamples = (short)ReadBigEndianWord(itemdata);
            Queue<byte> nibblesQueu = new Queue<byte>();
            byte previousNibble = 0;
            int i = 0;
            //for each sample in the sound
            for (i = 0; i < word_header_nrOfSamples;) //only add to i if we add an actual sample
            {
                //read nibbles, we always get two nibbles at a time

                if (nibblesQueu.Count == 0) {
                    var tuple = GetNibbles(itemdata, out bool isEOS);
                    if (isEOS)
                        break;

                    nibblesQueu.Enqueue(tuple.upperNibble); //first
                    nibblesQueu.Enqueue(tuple.lowerNibble); //second
                }
                
                byte nibbleToHandle = nibblesQueu.Dequeue();

                //If Nibble<> 0 Then Sample = Nibble
                if (nibbleToHandle != 0)
                {
                    //add sample to byte array
                    returns.Add((byte)((nibbleToHandle * 17) & 0xF0)); //make it 8 bit mono instead of 4, mask high bits
                    i++;
                    previousNibble = (byte)((nibbleToHandle * 17) & 0xF0); //make it 8 bit mono instead of 4, mask high bits
                }
                else //Nibble = 0
                {
                    //The previously read sample has to be repeated a certain number of times called RepeatCount.
                    //That number must be 3 at minimum (is added from the method).
                    int repeatCount = GetRepeatCountPlus3(itemdata, ref nibblesQueu);

                    for(int j = 0; j < repeatCount; j++)
                    {
                        returns.Add(previousNibble);
                        i++;
                    }

                }
            }

            return returns.ToArray();
        }

        // Method to check if the fourth bit (bit 3) is set in a nibble, from the least significant bit
        public static bool IsFourthBitSet(byte nibble)
        {
            // Mask the fourth bit (bit 3) using 0x08 (binary 00001000)
            return (nibble & 0x08) != 0;
        }

        private int GetRepeatCountPlus3(MemoryStream itemdata, ref Queue<byte> nibbles)
        {
            //start reading nibbles and add to list until the we find a nibble where the fourth bit set is 0
            // If nibble is 0, we need to decode the RepeatCount
            int repeatCount = 0;
            bool continueReading = true;
            bool isEOS = false;

            // Decode RepeatCount in a loop, reading 3 bits from each nibble
            while (continueReading && !isEOS)
            {
                if (nibbles.Count == 0)
                {
                    // Read the next nibbles
                    var tuple = GetNibbles(itemdata, out isEOS);
                    nibbles.Enqueue(tuple.upperNibble);
                    nibbles.Enqueue(tuple.lowerNibble);
                }
                byte nibble = nibbles.Dequeue();

                // The most significant bit indicates if we should continue reading
                continueReading = IsFourthBitSet(nibble); // (nibble & 0x8) != 0;

                // Extract the lower 3 bits and append them to RepeatCount
                repeatCount = (repeatCount << 3) | (nibble & 0x7);
            }

            // Minimum repeat count is 3
            repeatCount += 3;

            return repeatCount;
        }

        //no need to pass as a ref, should keep track anyway
        private short ReadBigEndianWord(MemoryStream memoryStream)
        {
            // Create a buffer to hold the two bytes of the word
            byte[] buffer = new byte[2];

            // Read 2 bytes from the MemoryStream
            memoryStream.Read(buffer, 0, 2);

            // Combine the bytes in big-endian order
            // Shift the first byte (high byte) 8 bits to the left and add the second byte (low byte)
            short word = (short)((buffer[0] << 8) | buffer[1]);

            return word;
        }

        //aparently we only use one nibble per byte (half) the rest we toss

        private (byte upperNibble, byte lowerNibble) GetNibbles(MemoryStream memoryStream, out bool isEOS)
        {
            isEOS = false;
            byte b = (byte)memoryStream.ReadByte();

            //if -1 then end of stream, if not EOS then get nibbles
            if (b != -1)
            { 
                return GetNibbles(b);
            }

            //end of stream
            isEOS = true;
            return (0, 0);
        }

        private (byte upperNibble, byte lowerNibble) GetNibbles(byte b)
        {
            // Extract the upper nibble (leftmost 4 bits)
            byte upperNibble = (byte)((b & 0xF0) >> 4);

            // Extract the lower nibble (rightmost 4 bits)
            byte lowerNibble = (byte)(b & 0x0F);

            // Return the nibbles as a tuple
            return (upperNibble, lowerNibble);
        }

    }
}

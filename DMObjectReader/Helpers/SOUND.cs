using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.Helpers
{
    public class SOUND
    {
        /*
The number of bytes in a "word" depends on the architecture of the system:
16-bit architecture: A word is 2 bytes (16 bits).
32-bit architecture: A word is 4 bytes (32 bits).
64-bit architecture: A word is 8 bytes (64 bits).

ATARI = 16 bit for a word = 2 bytes

Header
1 word (big endian): Number of sound samples
Data
The sound is encoded as PCM, 4 bit, mono, unsigned and is compressed with an RLE algorithm. Here is how to decode these sounds:

Read Nibble
If Nibble <> 0 Then Sample = Nibble
If Nibble = 0 Then
The previously read sample has to be repeated a certain number of times called RepeatCount. That number must be 3 at minimum. 
The value of RepeatCount is encoded in a variable number of nibbles, each of them containing 3 bits of the binary representation of RepeatCount as their less significant bits. 
The most significant bit is set to 1 in each of these nibbles except in the last nibble used to store the RepeatCount value which is marking the end of the cyle.
So each time you read a nibble for RepeatCount, you have to shift the bits of RepeatCount by three bits to the left and insert the three less significant bits of the last read nibble to the right until the most significant bit of the last read nibble is 0. You then need to add 3 to RepeatCount and copy the last sample read RepeatCount times.
Repeat that process until there are no more nibbles to read

Note: if you want to convert the sound data to PCM 8 bit sound, you need to multiply each sample value by 2^4 (16).

Playback rate: 6000 Hz.
         */
        public short word0; //nr of sound samples in this sound
        public byte byte2, byte3, byte4, byte5, byte6, byte7;
    }
}

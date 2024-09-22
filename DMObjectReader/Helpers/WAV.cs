using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.Helpers
{
    //SCK WAV
    public class WAV
    {
        protected static readonly int WAVHeaderFileDescription_Size = Windows.DWORD_SIZE;

        protected static readonly int WAVHeaderFileSize_Size = Windows.DWORD_SIZE;

        protected static readonly int WAVHeaderFileWAVDescription_Size = Windows.DWORD_SIZE;

        protected static readonly int WAVHeaderFileFormatDescription_Size = Windows.DWORD_SIZE;

        protected static readonly int WAVHeaderWAVDescriptionSize_Size = Windows.DWORD_SIZE;

        protected static readonly int WAVHeaderWAVFormat_Size = Windows.WORD_SIZE;

        protected static readonly int WAVHeaderWAVChannel_Size = Windows.WORD_SIZE;

        protected static readonly int WAVHeaderWAVRate_Size = Windows.DWORD_SIZE;

        protected static readonly int WAVHeaderWAVBytesPerSecond_Size = Windows.DWORD_SIZE;

        protected static readonly int WAVHeaderWAVBlockAlignment_Size = Windows.WORD_SIZE;

        protected static readonly int WAVHeaderWAVBitsPerSample_Size = Windows.WORD_SIZE;

        protected static readonly int WAVHeaderDataDescription_Size = Windows.DWORD_SIZE;

        protected static readonly int WAVHeaderDataSize_Size = Windows.DWORD_SIZE;

        public static readonly string FILE_EXTENSION = ".wav";

        public static byte[] GetHeader(long pSamplesSize, long pSampleRate, int pBitsPerSample, int pWAVHeaderWAVFormat, int pWAVHeaderChannel)
        {
            long lWAVHeaderFileSize = (WAVHeaderFileWAVDescription_Size + WAVHeaderFileFormatDescription_Size + WAVHeaderWAVDescriptionSize_Size + WAVHeaderWAVFormat_Size + WAVHeaderWAVChannel_Size + WAVHeaderWAVRate_Size + WAVHeaderWAVBytesPerSecond_Size + WAVHeaderWAVBlockAlignment_Size + WAVHeaderWAVBitsPerSample_Size + WAVHeaderDataDescription_Size + WAVHeaderDataSize_Size) + pSamplesSize * pBitsPerSample / 8L;
            long lWAVHeaderWAVDescriptionSize = (WAVHeaderWAVFormat_Size + WAVHeaderWAVChannel_Size + WAVHeaderWAVRate_Size + WAVHeaderWAVBytesPerSecond_Size + WAVHeaderWAVBlockAlignment_Size + WAVHeaderWAVBitsPerSample_Size);
            long lWAVHeaderBytesPerSecond = pWAVHeaderChannel * pSampleRate * pBitsPerSample / 8L;
            int lWAVHeaderBlockAlignment = pWAVHeaderChannel * pBitsPerSample / 8;

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    ms.Write(Encoding.UTF8.GetBytes("RIFF"), 0, 4);
                    ms.Write(Windows.JNumberToDWORD(lWAVHeaderFileSize), 0, 4);
                    ms.Write(Encoding.UTF8.GetBytes("WAVE"), 0, 4);
                    ms.Write(Encoding.UTF8.GetBytes("fmt "), 0, 4);
                    ms.Write(Windows.JNumberToDWORD(lWAVHeaderWAVDescriptionSize), 0, 4);
                    ms.Write(Windows.JNumberToWORD(pWAVHeaderWAVFormat), 0, 2);
                    ms.Write(Windows.JNumberToWORD(pWAVHeaderChannel), 0, 2);
                    ms.Write(Windows.JNumberToDWORD(pSampleRate), 0, 4);
                    ms.Write(Windows.JNumberToDWORD(lWAVHeaderBytesPerSecond), 0, 4);
                    ms.Write(Windows.JNumberToWORD(lWAVHeaderBlockAlignment), 0, 2);
                    ms.Write(Windows.JNumberToWORD(pBitsPerSample), 0, 2);
                    ms.Write(Encoding.UTF8.GetBytes("data"), 0, 4);
                    ms.Write(Windows.JNumberToDWORD(pSamplesSize * pBitsPerSample / 8L), 0, 4);
                }
                catch (Exception)
                {
                    return null;
                }
                return ms.ToArray();
            }
        }
    }

}

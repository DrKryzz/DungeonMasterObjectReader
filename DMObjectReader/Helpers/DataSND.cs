using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.Helpers
{
    //dummy class to handle constructor
    public class MapItem
    {

    }

    //SCK DataSND
    public class DataSND //: DataAdapter
    {
        public int mWAVHeaderWAVFormat = 1;
        public int mWAVHeaderChannel = 1;
        public static readonly int mBitsPerSample_8 = 8;

        public byte[] mSamples = null;
        public long mSamplesSize = 0;
        public long mSampleRate = 6000;
        public int mBitsPerSample = 8;

        public DataSND(MapItem pMapItem, long pSamplesSize, byte[] pSamples, int pBitsPerSample, int pEndianMode)
        //: base(pMapItem, pEndianMode)
        {
            mSamplesSize = pSamplesSize;
            mSamples = pSamples;
            mBitsPerSample = pBitsPerSample;
            mSampleRate = GetPropertyAsLong("SPR", mSampleRate);
        }



        private long GetPropertyAsLong(string propertyName, long mSampleRate)
        {
            //empty implemenation
            return default;
        }

        public void Export(string pOutputPath)
        {
            ToWAV(GetExportFileWAV(pOutputPath));
        }

        public FileInfo GetExportFileWAV(string pOutputPath)
        {
            return new FileInfo(Path.Combine(pOutputPath, WAV.FILE_EXTENSION));
            //return GetExportFile(pOutputPath, WAV.FILE_EXTENSION);
        }

        public FileInfo[] GetExportFileList(string pOutputPath)
        {
            return new FileInfo[] { GetExportFileWAV(pOutputPath) };
        }

        public bool ToWAV(FileInfo pOutputFile)
        {
            if (mSamples == null) return false;
            try
            {
                using (MemoryStream baos = new MemoryStream())
                {
                    baos.Write(WAV.GetHeader(mSamplesSize, mSampleRate, mBitsPerSample, mWAVHeaderWAVFormat, mWAVHeaderChannel), 0, WAV.GetHeader(mSamplesSize, mSampleRate, mBitsPerSample, mWAVHeaderWAVFormat, mWAVHeaderChannel).Length);
                    baos.Write(mSamples, 0, mSamples.Length);

                    using (FileStream fos = new FileStream(pOutputFile.FullName, FileMode.Create, FileAccess.Write))
                    {
                        baos.WriteTo(fos);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }
    }
}

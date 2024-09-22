﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMObjectReader.Helpers
{
    //SCK DataSND1
    public class DataSND1 : DataSND
    {
        private bool mNibbleTmp = false;

        public DataSND1(MapItem pMapItem, int pEndianMode) : base(pMapItem, 0, null, mBitsPerSample_8, pEndianMode)
        {
        }

        public byte GetNibble(byte lByte)
        {
            if (!mNibbleTmp)
            {
                mNibbleTmp = !mNibbleTmp;
                return Bytes.GetHighNibble(lByte);
            }
            mNibbleTmp = !mNibbleTmp;
            return Bytes.GetLowNibble(lByte);
        }

        public bool Decode(byte[] pData)
        {
            //addOffset(pData.Length);
            int lDataIndex = 0;
            if (pData.Length <= 0)
                return false;

            int lSamplesSize = Bytes.BytesToInt(pData[lDataIndex], pData[lDataIndex + 1], Bytes.BIG_ENDIAN);
            lDataIndex += 2;

            if (lSamplesSize <= 0) 
                return false;
            byte[] lSamples = new byte[lSamplesSize];

            int lSampleIndex = 0;
            int lGetNibbleIndex = 0;
            byte lPreviousSample = 0;
            while (lSampleIndex < lSamplesSize && lDataIndex < pData.Length)
            {
                byte lNibble = GetNibble(pData[lDataIndex]);
                lGetNibbleIndex++;
                if (lGetNibbleIndex % 2 == 0) lDataIndex++;
                if (lNibble != 0)
                {
                    lSamples[lSampleIndex] = (byte)((lNibble * 17) & 0xF0);
                    lSampleIndex++;
                    lPreviousSample = (byte)((lNibble * 17) & 0xF0);
                }
                else
                {
                    byte lControlCode = 1;
                    int lRepeatCount = 0;
                    do
                    {
                        if (lDataIndex >= pData.Length) 
                            return false;
                        lNibble = GetNibble(pData[lDataIndex]);
                        lGetNibbleIndex++;
                        if (lGetNibbleIndex % 2 == 0) 
                            lDataIndex++;
                        lRepeatCount = (lRepeatCount << 3) | (lNibble & 0x7);
                    } 
                    while ((lNibble & 0x8) != 0);
                    lRepeatCount += 3;
                    for (int i = 0; i < lRepeatCount; i++)
                    {
                        if (lSampleIndex >= lSamples.Length) 
                            return false;
                        lSamples[lSampleIndex] = lPreviousSample;
                        lSampleIndex++;
                    }
                }
            }

            // Beware: you must pad samples with 0 or 1 "0" sample in order to have the wanted number of samples
            // because if you have a 0 sample at the end, the RLE decoding algorithm thinks it is a repeat code.
            // loop here is not necessary, but it's a precaution
            for (int i = lSampleIndex; i < lSamplesSize; i++)
            {
                lSamples[i] = 0;
            }

            mSamplesSize = lSamplesSize;
            mSamples = lSamples;

            return true;
        }
    }
}

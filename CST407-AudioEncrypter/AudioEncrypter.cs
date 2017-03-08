using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST407_AudioEncrypter
{
    public class AudioEncrypter
    {
        const int HEADER_SIZE = 44;

        byte[] chunkID;
        byte[] fileSize;
        byte[] riffType;
        byte[] fmtID;
        byte[] fmtSize;
        byte[] fmtCode;
        byte[] channels;
        byte[] sampleRate;
        byte[] fmtAverageBPS;
        byte[] fmtBlockAlign;
        byte[] bitDepth;
        //byte[] fmtExtraSize;
        byte[] dataID;
        byte[] dataSize;
        byte[] data;

        bool invalid;

        public AudioEncrypter()
        {
            chunkID = new byte[4];
            fileSize = new byte[4];
            riffType = new byte[4];
            fmtID = new byte[4];
            fmtSize = new byte[4];
            fmtCode = new byte[2];
            channels = new byte[2];
            sampleRate = new byte[4];
            fmtAverageBPS = new byte[4];
            fmtBlockAlign = new byte[2];
            bitDepth = new byte[2];
            dataID = new byte[4];
            dataSize = new byte[4];
            invalid = false;
        }

        public void AddMessage(string message)
        {
            if (invalid) return;

            unchecked
            {
                byte[] temp_data = new byte[data.Length];
                byte[] string_data = Encoding.UTF8.GetBytes(message);
                Buffer.BlockCopy(data, 0, temp_data, 0, data.Length);
                int dat_idx = 0;
                for (int i = 0; i < string_data.Length; i++)
                {
                    for (int b = 0; b < 8 && dat_idx < temp_data.Length; b++, dat_idx++)
                    {
                        if((byte)((string_data[i] >> b) & 1) == 1)
                            temp_data[dat_idx] |= 1;
                        else
                            temp_data[dat_idx] &= (byte)~1;
                    }
                }
                for (int i = 0; i < 8 && dat_idx < temp_data.Length; i++, dat_idx++)
                    temp_data[dat_idx] &= (byte)~1;

                data = new byte[temp_data.Length];
                Buffer.BlockCopy(temp_data, 0, data, 0, temp_data.Length);
            }
        }

        public string RemoveMessage()
        {
            if (invalid) return null;

            string result = string.Empty;

            unchecked
            {
                byte[] temp_data = new byte[data.Length];
                Buffer.BlockCopy(data, 0, temp_data, 0, data.Length);
                int dat_idx = 0;
                byte grabbed;
                do
                {
                    grabbed = 0;
                    for (int i = 0; i < 8 && dat_idx < temp_data.Length; i++, dat_idx++)
                        grabbed |= (byte)((temp_data[dat_idx] & 1) << i);
                    result += (char)grabbed;
                } while (grabbed != 0);
            }

            return result;
        }

        public void WavToBytes(string filePath)
        {
            try
            {
                byte[] wav_bytes = File.ReadAllBytes(filePath);
                Buffer.BlockCopy(wav_bytes, 0, chunkID, 0, 4);
                if (Encoding.UTF8.GetString(chunkID) != "RIFF")
                    throw new Exception("Provided file is not in a RIFF format");
                Buffer.BlockCopy(wav_bytes, 4, fileSize, 0, 4);
                Buffer.BlockCopy(wav_bytes, 8, riffType, 0, 4);
                if (Encoding.UTF8.GetString(riffType) != "WAVE")
                    throw new Exception("Provided file is not in a WAVE format");
                Buffer.BlockCopy(wav_bytes, 12, fmtID, 0, 4);
                Buffer.BlockCopy(wav_bytes, 16, fmtSize, 0, 4);
                Buffer.BlockCopy(wav_bytes, 20, fmtCode, 0, 2);
                Buffer.BlockCopy(wav_bytes, 22, channels, 0, 2);
                Buffer.BlockCopy(wav_bytes, 24, sampleRate, 0, 4);
                Buffer.BlockCopy(wav_bytes, 28, fmtAverageBPS, 0, 4);
                Buffer.BlockCopy(wav_bytes, 32, fmtBlockAlign, 0, 2);
                Buffer.BlockCopy(wav_bytes, 34, bitDepth, 0, 2);
                Buffer.BlockCopy(wav_bytes, 36, dataID, 0, 4);
                Buffer.BlockCopy(wav_bytes, 40, dataSize, 0, 4);

                data = new byte[wav_bytes.Length - HEADER_SIZE];
                Buffer.BlockCopy(wav_bytes, 44, data, 0, wav_bytes.Length - HEADER_SIZE);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                invalid = true;
            }
        }

        public void BytesToWav(string filePath)
        {
            if (invalid) return;

            try
            {
                byte[] wav_bytes = new byte[data.Length + HEADER_SIZE];
                Buffer.BlockCopy(chunkID, 0, wav_bytes, 0, 4);
                Buffer.BlockCopy(fileSize, 0, wav_bytes, 4, 4);
                Buffer.BlockCopy(riffType, 0, wav_bytes, 8, 4);
                Buffer.BlockCopy(fmtID, 0, wav_bytes, 12, 4);
                Buffer.BlockCopy(fmtSize, 0, wav_bytes, 16, 4);
                Buffer.BlockCopy(fmtCode, 0, wav_bytes, 20, 2);
                Buffer.BlockCopy(channels, 0, wav_bytes, 22, 2);
                Buffer.BlockCopy(sampleRate, 0, wav_bytes, 24, 4);
                Buffer.BlockCopy(fmtAverageBPS, 0, wav_bytes, 28, 4);
                Buffer.BlockCopy(fmtBlockAlign, 0, wav_bytes, 32, 2);
                Buffer.BlockCopy(bitDepth, 0, wav_bytes, 34, 2);
                Buffer.BlockCopy(dataID, 0, wav_bytes, 36, 4);
                Buffer.BlockCopy(dataSize, 0, wav_bytes, 40, 4);
                Buffer.BlockCopy(data, 0, wav_bytes, 44, data.Length);

                File.WriteAllBytes(filePath, wav_bytes);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

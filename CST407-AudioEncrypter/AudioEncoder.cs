using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CST407_AudioEncrypter
{
    public class AudioEncoder
    {
        int headerSize;
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
        byte[] fmtExtraBytes;
        byte[] dataID;
        byte[] dataSize;
        byte[] data;

        bool invalid;

        public AudioEncoder()
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

        public byte[] EncryptMessage(string message, byte[] key)
        {
            byte[] encrypted;
            byte[] iv;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();
                iv = aes.IV;

                aes.Mode = CipherMode.CBC;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new StreamWriter(cs))
                        {
                            sw.Write(message);
                        }
                        encrypted = ms.ToArray();
                    }
                }
            }

            var combined = new byte[iv.Length + encrypted.Length];
            Array.Copy(iv, 0, combined, 0, iv.Length);
            Array.Copy(encrypted, 0, combined, iv.Length, encrypted.Length);

            return combined;
        }

        public string DecryptMessage(byte[] combined, byte[] key)
        {
            string plain_text;

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                byte[] iv = new byte[aes.BlockSize / 8];
                byte[] cipher_text = new byte[combined.Length - iv.Length];

                Array.Copy(combined, iv, iv.Length);
                Array.Copy(combined, iv.Length, cipher_text, 0, cipher_text.Length);

                aes.IV = iv;
                aes.Mode = CipherMode.CBC;

                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream(cipher_text))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            plain_text = sr.ReadToEnd();
                        }
                    }
                }
            }

            return plain_text;
        }

        public void AddMessage(byte[] message)
        {
            if (invalid) return;

            unchecked
            {
                byte[] temp_data = new byte[data.Length];
                Buffer.BlockCopy(data, 0, temp_data, 0, data.Length);
                int dat_idx = 0;
                for (int i = 0; i < message.Length; i++)
                {
                    for (int b = 0; b < 8 && dat_idx < temp_data.Length; b++, dat_idx++)
                    {
                        if ((byte)((message[i] >> b) & 1) == 1)
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

        public byte[] RemoveMessage()
        {
            if (invalid) return null;

            List<byte> result = new List<byte>();

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
                    if (grabbed != 0)
                        result.Add(grabbed);
                } while (grabbed != 0);
            }

            return result.ToArray();
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

                int format_size = BitConverter.ToInt32(fmtSize, 0);
                fmtExtraBytes = new byte[format_size - 16];

                Buffer.BlockCopy(wav_bytes, 20, fmtCode, 0, 2);
                Buffer.BlockCopy(wav_bytes, 22, channels, 0, 2);
                Buffer.BlockCopy(wav_bytes, 24, sampleRate, 0, 4);
                Buffer.BlockCopy(wav_bytes, 28, fmtAverageBPS, 0, 4);
                Buffer.BlockCopy(wav_bytes, 32, fmtBlockAlign, 0, 2);
                Buffer.BlockCopy(wav_bytes, 34, bitDepth, 0, 2);
                Buffer.BlockCopy(wav_bytes, 36, fmtExtraBytes, 0, format_size - 16);

                Buffer.BlockCopy(wav_bytes, 36 + format_size - 16, dataID, 0, 4);
                Buffer.BlockCopy(wav_bytes, 40 + format_size - 16, dataSize, 0, 4);

                headerSize = 44 + format_size - 16;

                data = new byte[wav_bytes.Length - headerSize];
                Buffer.BlockCopy(wav_bytes, headerSize, data, 0, wav_bytes.Length - headerSize);
            }
            catch (Exception ex)
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
                byte[] wav_bytes = new byte[data.Length + headerSize];
                Buffer.BlockCopy(chunkID, 0, wav_bytes, 0, 4);
                Buffer.BlockCopy(fileSize, 0, wav_bytes, 4, 4);
                Buffer.BlockCopy(riffType, 0, wav_bytes, 8, 4);
                Buffer.BlockCopy(fmtID, 0, wav_bytes, 12, 4);
                Buffer.BlockCopy(fmtSize, 0, wav_bytes, 16, 4);

                int format_size = BitConverter.ToInt32(fmtSize, 0);

                Buffer.BlockCopy(fmtCode, 0, wav_bytes, 20, 2);
                Buffer.BlockCopy(channels, 0, wav_bytes, 22, 2);
                Buffer.BlockCopy(sampleRate, 0, wav_bytes, 24, 4);
                Buffer.BlockCopy(fmtAverageBPS, 0, wav_bytes, 28, 4);
                Buffer.BlockCopy(fmtBlockAlign, 0, wav_bytes, 32, 2);
                Buffer.BlockCopy(bitDepth, 0, wav_bytes, 34, 2);

                Buffer.BlockCopy(fmtExtraBytes, 0, wav_bytes, 36, format_size - 16);

                Buffer.BlockCopy(dataID, 0, wav_bytes, 36 + format_size - 16, 4);
                Buffer.BlockCopy(dataSize, 0, wav_bytes, 40 + format_size - 16, 4);

                Buffer.BlockCopy(data, 0, wav_bytes, headerSize, data.Length);

                File.WriteAllBytes(filePath, wav_bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

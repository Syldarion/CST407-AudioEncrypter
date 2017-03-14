using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CST407_AudioEncrypter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void loadFileButton_Click(object sender, EventArgs e)
        {
            audioSourceFileDialog.FileName = string.Empty;
            audioSourceFileDialog.Title = "Select Base Audio File...";
            audioSourceFileDialog.Filter = "Wave Files (*.wav)|*.wav";
            audioSourceFileDialog.CheckFileExists = true;
            audioSourceFileDialog.CheckPathExists = true;
            audioSourceFileDialog.FileOk += (object obj, CancelEventArgs args) => loadedFilePathTextBox.Text = audioSourceFileDialog.FileName;
            audioSourceFileDialog.ShowDialog();
        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            encodedAudioSaveDialog.FileName = "encoded_audio";
            encodedAudioSaveDialog.Title = "Selected encoded audio location...";
            encodedAudioSaveDialog.Filter = "Wave Files (*.wav)|*.wav";
            encodedAudioSaveDialog.CheckPathExists = true;
            encodedAudioSaveDialog.FileOk += (object obj, CancelEventArgs args) => EncryptAndEncode(encodedAudioSaveDialog.FileName);
            encodedAudioSaveDialog.ShowDialog();
        }

        private void EncryptAndEncode(string encodedFilePath)
        {
            try
            {
                var random = new RNGCryptoServiceProvider();
                var key = new byte[16];
                random.GetBytes(key);

                string key_text = string.Empty;
                foreach (byte b in key)
                    key_text += string.Format("{0:x2}", b);
                generatedKeyTextBox.Text = key_text;

                AudioEncoder encoder = new AudioEncoder();

                if (string.IsNullOrEmpty(loadedFilePathTextBox.Text))
                    throw new Exception("Base file was not selected");

                encoder.WavToBytes(loadedFilePathTextBox.Text);

                if (string.IsNullOrEmpty(plaintextTextBox.Text))
                    throw new Exception("Message was not provided");

                byte[] encrypted = encoder.EncryptMessage(plaintextTextBox.Text, key);
                encoder.AddMessage(encrypted);
                encoder.BytesToWav(encodedFilePath);
            }
            catch(Exception ex)
            {
                //write message to status bar or whatever
            }
        }

        private void loadEncryptedAudioButton_Click(object sender, EventArgs e)
        {
            audioSourceFileDialog.FileName = string.Empty;
            audioSourceFileDialog.Title = "Select Base Audio File...";
            audioSourceFileDialog.Filter = "Wave Files (*.wav)|*.wav";
            audioSourceFileDialog.CheckFileExists = true;
            audioSourceFileDialog.CheckPathExists = true;
            audioSourceFileDialog.FileOk += (object obj, CancelEventArgs args) => encryptedAudioPathTextBox.Text = audioSourceFileDialog.FileName;
            audioSourceFileDialog.ShowDialog();
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            try
            {
                AudioEncoder decoder = new AudioEncoder();

                if (string.IsNullOrEmpty(encryptedAudioPathTextBox.Text))
                    throw new Exception("Encrypted audio file was not selected");

                decoder.WavToBytes(encryptedAudioPathTextBox.Text);

                byte[] message = decoder.RemoveMessage();

                if (string.IsNullOrEmpty(encryptionKeyTextBox.Text))
                    throw new Exception("Generation key was not provided");

                string key_text = encryptionKeyTextBox.Text.Trim();

                if (key_text.Length != 32)
                    throw new Exception("Invalid generation key provided");

                var key = new byte[16];
                for (int i = 0; i < 32; i += 2)
                    key[i / 2] = Convert.ToByte(key_text.Substring(i, 2), 16);

                string result = decoder.DecryptMessage(message, key);

                recoveredMessageTextBox.Text = result;
            }
            catch(Exception ex)
            {
                //write message to status bar
            }
        }

        private void DecodeAndDecrypt()
        {

        }
    }
}

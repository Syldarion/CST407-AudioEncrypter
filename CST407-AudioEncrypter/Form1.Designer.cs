namespace CST407_AudioEncrypter
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.audioSourceFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.encryptGroupBox = new System.Windows.Forms.GroupBox();
            this.encodedAudioSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.loadedFilePathTextBox = new System.Windows.Forms.TextBox();
            this.plaintextTextBox = new System.Windows.Forms.TextBox();
            this.generatedKeyTextBox = new System.Windows.Forms.TextBox();
            this.encryptButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.decryptButton = new System.Windows.Forms.Button();
            this.recoveredMessageTextBox = new System.Windows.Forms.TextBox();
            this.encryptionKeyTextBox = new System.Windows.Forms.TextBox();
            this.encryptedAudioPathTextBox = new System.Windows.Forms.TextBox();
            this.loadEncryptedAudioButton = new System.Windows.Forms.Button();
            this.encryptGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // audioSourceFileDialog
            // 
            this.audioSourceFileDialog.FileName = "openFileDialog1";
            // 
            // encryptGroupBox
            // 
            this.encryptGroupBox.Controls.Add(this.label2);
            this.encryptGroupBox.Controls.Add(this.label1);
            this.encryptGroupBox.Controls.Add(this.encryptButton);
            this.encryptGroupBox.Controls.Add(this.generatedKeyTextBox);
            this.encryptGroupBox.Controls.Add(this.plaintextTextBox);
            this.encryptGroupBox.Controls.Add(this.loadedFilePathTextBox);
            this.encryptGroupBox.Controls.Add(this.loadFileButton);
            this.encryptGroupBox.Location = new System.Drawing.Point(12, 12);
            this.encryptGroupBox.Name = "encryptGroupBox";
            this.encryptGroupBox.Size = new System.Drawing.Size(596, 132);
            this.encryptGroupBox.TabIndex = 0;
            this.encryptGroupBox.TabStop = false;
            this.encryptGroupBox.Text = "Encrypt + Encode Message";
            // 
            // loadFileButton
            // 
            this.loadFileButton.Location = new System.Drawing.Point(6, 19);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(114, 23);
            this.loadFileButton.TabIndex = 0;
            this.loadFileButton.Text = "Load Audio File...";
            this.loadFileButton.UseVisualStyleBackColor = true;
            this.loadFileButton.Click += new System.EventHandler(this.loadFileButton_Click);
            // 
            // loadedFilePathTextBox
            // 
            this.loadedFilePathTextBox.Location = new System.Drawing.Point(126, 21);
            this.loadedFilePathTextBox.Name = "loadedFilePathTextBox";
            this.loadedFilePathTextBox.ReadOnly = true;
            this.loadedFilePathTextBox.Size = new System.Drawing.Size(464, 20);
            this.loadedFilePathTextBox.TabIndex = 1;
            // 
            // plaintextTextBox
            // 
            this.plaintextTextBox.Location = new System.Drawing.Point(126, 47);
            this.plaintextTextBox.Name = "plaintextTextBox";
            this.plaintextTextBox.Size = new System.Drawing.Size(464, 20);
            this.plaintextTextBox.TabIndex = 2;
            // 
            // generatedKeyTextBox
            // 
            this.generatedKeyTextBox.Location = new System.Drawing.Point(126, 73);
            this.generatedKeyTextBox.Name = "generatedKeyTextBox";
            this.generatedKeyTextBox.ReadOnly = true;
            this.generatedKeyTextBox.Size = new System.Drawing.Size(464, 20);
            this.generatedKeyTextBox.TabIndex = 4;
            // 
            // encryptButton
            // 
            this.encryptButton.Location = new System.Drawing.Point(476, 99);
            this.encryptButton.Name = "encryptButton";
            this.encryptButton.Size = new System.Drawing.Size(114, 23);
            this.encryptButton.TabIndex = 6;
            this.encryptButton.Text = "Encrypt + Encode";
            this.encryptButton.UseVisualStyleBackColor = true;
            this.encryptButton.Click += new System.EventHandler(this.encryptButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(70, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Message";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Generated Key";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.decryptButton);
            this.groupBox1.Controls.Add(this.recoveredMessageTextBox);
            this.groupBox1.Controls.Add(this.encryptionKeyTextBox);
            this.groupBox1.Controls.Add(this.encryptedAudioPathTextBox);
            this.groupBox1.Controls.Add(this.loadEncryptedAudioButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 150);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(596, 133);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Decode + Decrypt Message";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(70, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Message";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Encryption Key";
            // 
            // decryptButton
            // 
            this.decryptButton.Location = new System.Drawing.Point(476, 99);
            this.decryptButton.Name = "decryptButton";
            this.decryptButton.Size = new System.Drawing.Size(114, 23);
            this.decryptButton.TabIndex = 6;
            this.decryptButton.Text = "Decode + Decrypt";
            this.decryptButton.UseVisualStyleBackColor = true;
            this.decryptButton.Click += new System.EventHandler(this.decryptButton_Click);
            // 
            // recoveredMessageTextBox
            // 
            this.recoveredMessageTextBox.Location = new System.Drawing.Point(126, 73);
            this.recoveredMessageTextBox.Name = "recoveredMessageTextBox";
            this.recoveredMessageTextBox.ReadOnly = true;
            this.recoveredMessageTextBox.Size = new System.Drawing.Size(464, 20);
            this.recoveredMessageTextBox.TabIndex = 4;
            // 
            // encryptionKeyTextBox
            // 
            this.encryptionKeyTextBox.Location = new System.Drawing.Point(126, 47);
            this.encryptionKeyTextBox.Name = "encryptionKeyTextBox";
            this.encryptionKeyTextBox.Size = new System.Drawing.Size(464, 20);
            this.encryptionKeyTextBox.TabIndex = 2;
            // 
            // encryptedAudioPathTextBox
            // 
            this.encryptedAudioPathTextBox.Location = new System.Drawing.Point(126, 21);
            this.encryptedAudioPathTextBox.Name = "encryptedAudioPathTextBox";
            this.encryptedAudioPathTextBox.ReadOnly = true;
            this.encryptedAudioPathTextBox.Size = new System.Drawing.Size(464, 20);
            this.encryptedAudioPathTextBox.TabIndex = 1;
            // 
            // loadEncryptedAudioButton
            // 
            this.loadEncryptedAudioButton.Location = new System.Drawing.Point(6, 19);
            this.loadEncryptedAudioButton.Name = "loadEncryptedAudioButton";
            this.loadEncryptedAudioButton.Size = new System.Drawing.Size(114, 23);
            this.loadEncryptedAudioButton.TabIndex = 0;
            this.loadEncryptedAudioButton.Text = "Load Audio File...";
            this.loadEncryptedAudioButton.UseVisualStyleBackColor = true;
            this.loadEncryptedAudioButton.Click += new System.EventHandler(this.loadEncryptedAudioButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 296);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.encryptGroupBox);
            this.Name = "Form1";
            this.Text = "Audio Encoder";
            this.encryptGroupBox.ResumeLayout(false);
            this.encryptGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog audioSourceFileDialog;
        private System.Windows.Forms.GroupBox encryptGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button encryptButton;
        private System.Windows.Forms.TextBox generatedKeyTextBox;
        private System.Windows.Forms.TextBox plaintextTextBox;
        private System.Windows.Forms.TextBox loadedFilePathTextBox;
        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.SaveFileDialog encodedAudioSaveDialog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button decryptButton;
        private System.Windows.Forms.TextBox recoveredMessageTextBox;
        private System.Windows.Forms.TextBox encryptionKeyTextBox;
        private System.Windows.Forms.TextBox encryptedAudioPathTextBox;
        private System.Windows.Forms.Button loadEncryptedAudioButton;
    }
}


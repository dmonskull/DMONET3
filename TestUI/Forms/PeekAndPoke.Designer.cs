namespace DMONET3.Forms
{
    partial class PeekAndPoke
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fillCheckBox = new System.Windows.Forms.CheckBox();
            this.fillValueTextBox = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.peekPokeFeedBackTextBox = new System.Windows.Forms.TextBox();
            this.debugGroupBox = new System.Windows.Forms.GroupBox();
            this.unfreezeButton = new System.Windows.Forms.Button();
            this.freezeButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LabelFloat = new System.Windows.Forms.Label();
            this.NumericFloatTextBox = new System.Windows.Forms.TextBox();
            this.isSigned = new System.Windows.Forms.CheckBox();
            this.LabelInt8 = new System.Windows.Forms.Label();
            this.LabelInt16 = new System.Windows.Forms.Label();
            this.NumericInt32 = new System.Windows.Forms.NumericUpDown();
            this.NumericInt8 = new System.Windows.Forms.NumericUpDown();
            this.NumericInt16 = new System.Windows.Forms.NumericUpDown();
            this.LabelInt32 = new System.Windows.Forms.Label();
            this.labelSelAddress = new System.Windows.Forms.Label();
            this.SelAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.peekLengthTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.peekPokeAddressTextBox2 = new System.Windows.Forms.TextBox();
            this.hexBox = new Be.Windows.Forms.HexBox();
            this.ipAddressTextBox = new DevExpress.XtraEditors.TextEdit();
            this.pokeButton = new DevExpress.XtraEditors.SimpleButton();
            this.newPeekButton = new DevExpress.XtraEditors.SimpleButton();
            this.peekButton = new DevExpress.XtraEditors.SimpleButton();
            this.connectButton = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            this.debugGroupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericInt32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericInt8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericInt16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ipAddressTextBox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.fillCheckBox);
            this.groupBox1.Controls.Add(this.fillValueTextBox);
            this.groupBox1.Location = new System.Drawing.Point(644, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 55);
            this.groupBox1.TabIndex = 88;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fill";
            // 
            // fillCheckBox
            // 
            this.fillCheckBox.AutoSize = true;
            this.fillCheckBox.Location = new System.Drawing.Point(13, 23);
            this.fillCheckBox.Name = "fillCheckBox";
            this.fillCheckBox.Size = new System.Drawing.Size(108, 17);
            this.fillCheckBox.TabIndex = 1;
            this.fillCheckBox.Text = "Memory With 0x:";
            this.fillCheckBox.UseVisualStyleBackColor = true;
            // 
            // fillValueTextBox
            // 
            this.fillValueTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.fillValueTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fillValueTextBox.ForeColor = System.Drawing.SystemColors.Window;
            this.fillValueTextBox.Location = new System.Drawing.Point(146, 21);
            this.fillValueTextBox.Name = "fillValueTextBox";
            this.fillValueTextBox.Size = new System.Drawing.Size(30, 14);
            this.fillValueTextBox.TabIndex = 0;
            // 
            // label27
            // 
            this.label27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(588, 104);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(110, 13);
            this.label27.TabIndex = 87;
            this.label27.Text = "Peek/Poke Feedback:";
            // 
            // peekPokeFeedBackTextBox
            // 
            this.peekPokeFeedBackTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.peekPokeFeedBackTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.peekPokeFeedBackTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.peekPokeFeedBackTextBox.ForeColor = System.Drawing.SystemColors.Window;
            this.peekPokeFeedBackTextBox.Location = new System.Drawing.Point(704, 101);
            this.peekPokeFeedBackTextBox.Name = "peekPokeFeedBackTextBox";
            this.peekPokeFeedBackTextBox.ReadOnly = true;
            this.peekPokeFeedBackTextBox.Size = new System.Drawing.Size(108, 14);
            this.peekPokeFeedBackTextBox.TabIndex = 86;
            // 
            // debugGroupBox
            // 
            this.debugGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.debugGroupBox.Controls.Add(this.unfreezeButton);
            this.debugGroupBox.Controls.Add(this.freezeButton);
            this.debugGroupBox.Location = new System.Drawing.Point(644, 196);
            this.debugGroupBox.Name = "debugGroupBox";
            this.debugGroupBox.Size = new System.Drawing.Size(182, 58);
            this.debugGroupBox.TabIndex = 85;
            this.debugGroupBox.TabStop = false;
            this.debugGroupBox.Text = "Debug Commands";
            // 
            // unfreezeButton
            // 
            this.unfreezeButton.ForeColor = System.Drawing.Color.Black;
            this.unfreezeButton.Location = new System.Drawing.Point(97, 21);
            this.unfreezeButton.Name = "unfreezeButton";
            this.unfreezeButton.Size = new System.Drawing.Size(75, 27);
            this.unfreezeButton.TabIndex = 1;
            this.unfreezeButton.Text = "Un-Freeze";
            this.unfreezeButton.UseVisualStyleBackColor = true;
            this.unfreezeButton.Click += new System.EventHandler(this.unfreezeButton_Click);
            // 
            // freezeButton
            // 
            this.freezeButton.ForeColor = System.Drawing.Color.Black;
            this.freezeButton.Location = new System.Drawing.Point(6, 21);
            this.freezeButton.Name = "freezeButton";
            this.freezeButton.Size = new System.Drawing.Size(75, 27);
            this.freezeButton.TabIndex = 0;
            this.freezeButton.Text = "Freeze";
            this.freezeButton.UseVisualStyleBackColor = true;
            this.freezeButton.Click += new System.EventHandler(this.freezeButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.LabelFloat);
            this.groupBox3.Controls.Add(this.NumericFloatTextBox);
            this.groupBox3.Controls.Add(this.isSigned);
            this.groupBox3.Controls.Add(this.LabelInt8);
            this.groupBox3.Controls.Add(this.LabelInt16);
            this.groupBox3.Controls.Add(this.NumericInt32);
            this.groupBox3.Controls.Add(this.NumericInt8);
            this.groupBox3.Controls.Add(this.NumericInt16);
            this.groupBox3.Controls.Add(this.LabelInt32);
            this.groupBox3.Location = new System.Drawing.Point(644, 260);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(182, 196);
            this.groupBox3.TabIndex = 84;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Value";
            // 
            // LabelFloat
            // 
            this.LabelFloat.AutoSize = true;
            this.LabelFloat.Location = new System.Drawing.Point(20, 158);
            this.LabelFloat.Name = "LabelFloat";
            this.LabelFloat.Size = new System.Drawing.Size(31, 13);
            this.LabelFloat.TabIndex = 19;
            this.LabelFloat.Text = "Float";
            // 
            // NumericFloatTextBox
            // 
            this.NumericFloatTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.NumericFloatTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NumericFloatTextBox.ForeColor = System.Drawing.SystemColors.Window;
            this.NumericFloatTextBox.Location = new System.Drawing.Point(63, 155);
            this.NumericFloatTextBox.Name = "NumericFloatTextBox";
            this.NumericFloatTextBox.Size = new System.Drawing.Size(109, 14);
            this.NumericFloatTextBox.TabIndex = 20;
            this.NumericFloatTextBox.Tag = "text";
            this.NumericFloatTextBox.Text = "0";
            this.NumericFloatTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // isSigned
            // 
            this.isSigned.AutoSize = true;
            this.isSigned.Location = new System.Drawing.Point(39, 21);
            this.isSigned.Name = "isSigned";
            this.isSigned.Size = new System.Drawing.Size(92, 17);
            this.isSigned.TabIndex = 16;
            this.isSigned.Text = "Signed Values";
            this.isSigned.UseVisualStyleBackColor = true;
            // 
            // LabelInt8
            // 
            this.LabelInt8.AutoSize = true;
            this.LabelInt8.Location = new System.Drawing.Point(15, 60);
            this.LabelInt8.Name = "LabelInt8";
            this.LabelInt8.Size = new System.Drawing.Size(42, 13);
            this.LabelInt8.TabIndex = 17;
            this.LabelInt8.Text = "(U)Int8";
            // 
            // LabelInt16
            // 
            this.LabelInt16.AutoSize = true;
            this.LabelInt16.Location = new System.Drawing.Point(6, 92);
            this.LabelInt16.Name = "LabelInt16";
            this.LabelInt16.Size = new System.Drawing.Size(48, 13);
            this.LabelInt16.TabIndex = 17;
            this.LabelInt16.Text = "(U)Int16";
            // 
            // NumericInt32
            // 
            this.NumericInt32.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.NumericInt32.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NumericInt32.ForeColor = System.Drawing.SystemColors.Window;
            this.NumericInt32.Location = new System.Drawing.Point(63, 123);
            this.NumericInt32.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.NumericInt32.Minimum = new decimal(new int[] {
            -2147483648,
            0,
            0,
            -2147483648});
            this.NumericInt32.Name = "NumericInt32";
            this.NumericInt32.Size = new System.Drawing.Size(109, 17);
            this.NumericInt32.TabIndex = 18;
            this.NumericInt32.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // NumericInt8
            // 
            this.NumericInt8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.NumericInt8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NumericInt8.ForeColor = System.Drawing.SystemColors.Window;
            this.NumericInt8.Location = new System.Drawing.Point(63, 59);
            this.NumericInt8.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.NumericInt8.Minimum = new decimal(new int[] {
            128,
            0,
            0,
            -2147483648});
            this.NumericInt8.Name = "NumericInt8";
            this.NumericInt8.Size = new System.Drawing.Size(109, 17);
            this.NumericInt8.TabIndex = 18;
            this.NumericInt8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // NumericInt16
            // 
            this.NumericInt16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.NumericInt16.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NumericInt16.ForeColor = System.Drawing.SystemColors.Window;
            this.NumericInt16.Location = new System.Drawing.Point(63, 91);
            this.NumericInt16.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.NumericInt16.Minimum = new decimal(new int[] {
            32768,
            0,
            0,
            -2147483648});
            this.NumericInt16.Name = "NumericInt16";
            this.NumericInt16.Size = new System.Drawing.Size(109, 17);
            this.NumericInt16.TabIndex = 18;
            this.NumericInt16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelInt32
            // 
            this.LabelInt32.AutoSize = true;
            this.LabelInt32.Location = new System.Drawing.Point(6, 125);
            this.LabelInt32.Name = "LabelInt32";
            this.LabelInt32.Size = new System.Drawing.Size(48, 13);
            this.LabelInt32.TabIndex = 17;
            this.LabelInt32.Text = "(U)Int32";
            // 
            // labelSelAddress
            // 
            this.labelSelAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSelAddress.AutoSize = true;
            this.labelSelAddress.Location = new System.Drawing.Point(601, 72);
            this.labelSelAddress.Name = "labelSelAddress";
            this.labelSelAddress.Size = new System.Drawing.Size(94, 13);
            this.labelSelAddress.TabIndex = 83;
            this.labelSelAddress.Text = "Selected Address:";
            // 
            // SelAddress
            // 
            this.SelAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.SelAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.SelAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.SelAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SelAddress.ForeColor = System.Drawing.SystemColors.Window;
            this.SelAddress.Location = new System.Drawing.Point(704, 70);
            this.SelAddress.Name = "SelAddress";
            this.SelAddress.ReadOnly = true;
            this.SelAddress.Size = new System.Drawing.Size(108, 14);
            this.SelAddress.TabIndex = 82;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(93, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(391, 16);
            this.label7.TabIndex = 81;
            this.label7.Text = "00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 79;
            this.label3.Text = "Length 0x:";
            // 
            // peekLengthTextBox
            // 
            this.peekLengthTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.peekLengthTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.peekLengthTextBox.ForeColor = System.Drawing.SystemColors.Window;
            this.peekLengthTextBox.Location = new System.Drawing.Point(96, 105);
            this.peekLengthTextBox.MaxLength = 255;
            this.peekLengthTextBox.Name = "peekLengthTextBox";
            this.peekLengthTextBox.Size = new System.Drawing.Size(188, 14);
            this.peekLengthTextBox.TabIndex = 75;
            this.peekLengthTextBox.Text = "FFFF";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 76;
            this.label1.Text = "Address 0x:";
            // 
            // peekPokeAddressTextBox2
            // 
            this.peekPokeAddressTextBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.peekPokeAddressTextBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.peekPokeAddressTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.peekPokeAddressTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.peekPokeAddressTextBox2.ForeColor = System.Drawing.SystemColors.Window;
            this.peekPokeAddressTextBox2.Location = new System.Drawing.Point(96, 69);
            this.peekPokeAddressTextBox2.MaxLength = 255;
            this.peekPokeAddressTextBox2.Name = "peekPokeAddressTextBox2";
            this.peekPokeAddressTextBox2.Size = new System.Drawing.Size(188, 14);
            this.peekPokeAddressTextBox2.TabIndex = 74;
            this.peekPokeAddressTextBox2.Text = "C0000000";
            this.peekPokeAddressTextBox2.WordWrap = false;
            // 
            // hexBox
            // 
            this.hexBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hexBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.hexBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            // 
            // 
            // 
            this.hexBox.BuiltInContextMenu.CopyMenuItemText = "";
            this.hexBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.hexBox.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hexBox.LineInfoVisible = true;
            this.hexBox.Location = new System.Drawing.Point(1, 152);
            this.hexBox.Name = "hexBox";
            this.hexBox.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            this.hexBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox.Size = new System.Drawing.Size(637, 470);
            this.hexBox.StringViewVisible = true;
            this.hexBox.TabIndex = 91;
            this.hexBox.UseFixedBytesPerLine = true;
            this.hexBox.VScrollBarVisible = true;
            this.hexBox.SelectionStartChanged += new System.EventHandler(this.hexBox_SelectionStartChanged);
            this.hexBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hexBox_KeyDown);
            this.hexBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.hexBox_MouseUp);
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Location = new System.Drawing.Point(12, 12);
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(140, 20);
            this.ipAddressTextBox.TabIndex = 92;
            // 
            // pokeButton
            // 
            this.pokeButton.Location = new System.Drawing.Point(357, 67);
            this.pokeButton.Name = "pokeButton";
            this.pokeButton.Size = new System.Drawing.Size(61, 23);
            this.pokeButton.TabIndex = 94;
            this.pokeButton.Text = "Poke";
            this.pokeButton.Click += new System.EventHandler(this.pokeButton_Click_1);
            // 
            // newPeekButton
            // 
            this.newPeekButton.Location = new System.Drawing.Point(290, 103);
            this.newPeekButton.Name = "newPeekButton";
            this.newPeekButton.Size = new System.Drawing.Size(61, 23);
            this.newPeekButton.TabIndex = 95;
            this.newPeekButton.Text = "New";
            this.newPeekButton.Click += new System.EventHandler(this.newPeekButton_Click_1);
            // 
            // peekButton
            // 
            this.peekButton.Location = new System.Drawing.Point(290, 67);
            this.peekButton.Name = "peekButton";
            this.peekButton.Size = new System.Drawing.Size(61, 23);
            this.peekButton.TabIndex = 96;
            this.peekButton.Text = "Peek";
            this.peekButton.Click += new System.EventHandler(this.peekButton_Click_1);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(12, 36);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(140, 27);
            this.connectButton.TabIndex = 97;
            this.connectButton.Text = "Connect";
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click_1);
            // 
            // PeekAndPoke
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 624);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.peekButton);
            this.Controls.Add(this.newPeekButton);
            this.Controls.Add(this.pokeButton);
            this.Controls.Add(this.ipAddressTextBox);
            this.Controls.Add(this.hexBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.peekPokeFeedBackTextBox);
            this.Controls.Add(this.debugGroupBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.labelSelAddress);
            this.Controls.Add(this.SelAddress);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.peekLengthTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.peekPokeAddressTextBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PeekAndPoke";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Peek And Poke";
            this.Load += new System.EventHandler(this.PeekAndPoke_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.debugGroupBox.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericInt32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericInt8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericInt16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ipAddressTextBox.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox fillCheckBox;
        private System.Windows.Forms.TextBox fillValueTextBox;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox peekPokeFeedBackTextBox;
        private System.Windows.Forms.GroupBox debugGroupBox;
        private System.Windows.Forms.Button unfreezeButton;
        private System.Windows.Forms.Button freezeButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label LabelFloat;
        private System.Windows.Forms.TextBox NumericFloatTextBox;
        private System.Windows.Forms.CheckBox isSigned;
        private System.Windows.Forms.Label LabelInt8;
        private System.Windows.Forms.Label LabelInt16;
        private System.Windows.Forms.NumericUpDown NumericInt32;
        private System.Windows.Forms.NumericUpDown NumericInt8;
        private System.Windows.Forms.NumericUpDown NumericInt16;
        private System.Windows.Forms.Label LabelInt32;
        private System.Windows.Forms.Label labelSelAddress;
        private System.Windows.Forms.TextBox SelAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox peekLengthTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox peekPokeAddressTextBox2;
        private Be.Windows.Forms.HexBox hexBox;
        private DevExpress.XtraEditors.TextEdit ipAddressTextBox;
        private DevExpress.XtraEditors.SimpleButton pokeButton;
        private DevExpress.XtraEditors.SimpleButton newPeekButton;
        private DevExpress.XtraEditors.SimpleButton peekButton;
        private DevExpress.XtraEditors.SimpleButton connectButton;
    }
}
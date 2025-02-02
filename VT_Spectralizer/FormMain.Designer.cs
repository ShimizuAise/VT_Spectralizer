namespace VT_Spectralizer
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            ConnectButton = new Button();
            UrlTextBox = new TextBox();
            PortTextBox = new TextBox();
            LogTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            TaskButton = new Button();
            labelSubBass = new Label();
            labelBass = new Label();
            labelLowMid = new Label();
            labelMidrange = new Label();
            labelUpperMid = new Label();
            labelPresence = new Label();
            labelBrilliance = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            labelMaxVolume = new Label();
            label10 = new Label();
            IntervalTextBox = new TextBox();
            label11 = new Label();
            label12 = new Label();
            VTSpecParamCombobox = new ComboBox();
            ComboBoxAudioDevices = new ComboBox();
            label13 = new Label();
            linkAbout = new LinkLabel();
            SuspendLayout();
            // 
            // ConnectButton
            // 
            ConnectButton.Location = new Point(12, 228);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.Size = new Size(75, 23);
            ConnectButton.TabIndex = 0;
            ConnectButton.Text = "Connect";
            ConnectButton.UseVisualStyleBackColor = true;
            ConnectButton.Click += ConnectButton_Click;
            // 
            // UrlTextBox
            // 
            UrlTextBox.BackColor = SystemColors.ScrollBar;
            UrlTextBox.Enabled = false;
            UrlTextBox.Location = new Point(109, 229);
            UrlTextBox.Name = "UrlTextBox";
            UrlTextBox.Size = new Size(118, 23);
            UrlTextBox.TabIndex = 1;
            UrlTextBox.Text = "127.0.0.1";
            // 
            // PortTextBox
            // 
            PortTextBox.BackColor = SystemColors.ScrollBar;
            PortTextBox.Enabled = false;
            PortTextBox.Location = new Point(237, 228);
            PortTextBox.MaxLength = 4;
            PortTextBox.Name = "PortTextBox";
            PortTextBox.Size = new Size(96, 23);
            PortTextBox.TabIndex = 2;
            PortTextBox.Text = "8001";
            PortTextBox.TextChanged += PortTextBox_TextChanged;
            // 
            // LogTextBox
            // 
            LogTextBox.BackColor = SystemColors.ScrollBar;
            LogTextBox.Enabled = false;
            LogTextBox.Location = new Point(12, 258);
            LogTextBox.Multiline = true;
            LogTextBox.Name = "LogTextBox";
            LogTextBox.ScrollBars = ScrollBars.Vertical;
            LogTextBox.Size = new Size(410, 166);
            LogTextBox.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(109, 211);
            label1.Name = "label1";
            label1.Size = new Size(93, 15);
            label1.TabIndex = 4;
            label1.Text = "Connection URL";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(237, 211);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 5;
            label2.Text = "Port";
            // 
            // TaskButton
            // 
            TaskButton.Location = new Point(11, 185);
            TaskButton.Name = "TaskButton";
            TaskButton.Size = new Size(156, 23);
            TaskButton.TabIndex = 6;
            TaskButton.Text = "Start Audio Capture";
            TaskButton.UseVisualStyleBackColor = true;
            TaskButton.Click += TaskButton_Click;
            // 
            // labelSubBass
            // 
            labelSubBass.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelSubBass.Location = new Point(292, 22);
            labelSubBass.Name = "labelSubBass";
            labelSubBass.Size = new Size(130, 15);
            labelSubBass.TabIndex = 9;
            labelSubBass.Text = "0%";
            labelSubBass.TextAlign = ContentAlignment.TopRight;
            // 
            // labelBass
            // 
            labelBass.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelBass.Location = new Point(292, 37);
            labelBass.Name = "labelBass";
            labelBass.Size = new Size(130, 15);
            labelBass.TabIndex = 10;
            labelBass.Text = "0%";
            labelBass.TextAlign = ContentAlignment.TopRight;
            // 
            // labelLowMid
            // 
            labelLowMid.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelLowMid.Location = new Point(292, 52);
            labelLowMid.Name = "labelLowMid";
            labelLowMid.Size = new Size(130, 15);
            labelLowMid.TabIndex = 11;
            labelLowMid.Text = "0%";
            labelLowMid.TextAlign = ContentAlignment.TopRight;
            // 
            // labelMidrange
            // 
            labelMidrange.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelMidrange.Location = new Point(292, 67);
            labelMidrange.Name = "labelMidrange";
            labelMidrange.Size = new Size(130, 15);
            labelMidrange.TabIndex = 12;
            labelMidrange.Text = "0%";
            labelMidrange.TextAlign = ContentAlignment.TopRight;
            // 
            // labelUpperMid
            // 
            labelUpperMid.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelUpperMid.Location = new Point(292, 82);
            labelUpperMid.Name = "labelUpperMid";
            labelUpperMid.Size = new Size(130, 15);
            labelUpperMid.TabIndex = 13;
            labelUpperMid.Text = "0%";
            labelUpperMid.TextAlign = ContentAlignment.TopRight;
            // 
            // labelPresence
            // 
            labelPresence.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelPresence.Location = new Point(292, 97);
            labelPresence.Name = "labelPresence";
            labelPresence.Size = new Size(130, 15);
            labelPresence.TabIndex = 14;
            labelPresence.Text = "0%";
            labelPresence.TextAlign = ContentAlignment.TopRight;
            // 
            // labelBrilliance
            // 
            labelBrilliance.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelBrilliance.Location = new Point(292, 112);
            labelBrilliance.Name = "labelBrilliance";
            labelBrilliance.Size = new Size(130, 15);
            labelBrilliance.TabIndex = 15;
            labelBrilliance.Text = "0%";
            labelBrilliance.TextAlign = ContentAlignment.TopRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 22);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 16;
            label3.Text = "Sub-Bass";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 37);
            label4.Name = "label4";
            label4.Size = new Size(30, 15);
            label4.TabIndex = 17;
            label4.Text = "Bass";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 52);
            label5.Name = "label5";
            label5.Size = new Size(55, 15);
            label5.TabIndex = 18;
            label5.Text = "Low-Mid";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 67);
            label6.Name = "label6";
            label6.Size = new Size(58, 15);
            label6.TabIndex = 19;
            label6.Text = "Midrange";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(12, 82);
            label7.Name = "label7";
            label7.Size = new Size(65, 15);
            label7.TabIndex = 20;
            label7.Text = "Upper-Mid";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 97);
            label8.Name = "label8";
            label8.Size = new Size(54, 15);
            label8.TabIndex = 21;
            label8.Text = "Presence";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 112);
            label9.Name = "label9";
            label9.Size = new Size(55, 15);
            label9.TabIndex = 22;
            label9.Text = "Brilliance";
            // 
            // labelMaxVolume
            // 
            labelMaxVolume.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelMaxVolume.Location = new Point(292, 127);
            labelMaxVolume.Name = "labelMaxVolume";
            labelMaxVolume.Size = new Size(130, 15);
            labelMaxVolume.TabIndex = 23;
            labelMaxVolume.Text = "0%";
            labelMaxVolume.TextAlign = ContentAlignment.TopRight;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(11, 127);
            label10.Name = "label10";
            label10.Size = new Size(73, 15);
            label10.TabIndex = 24;
            label10.Text = "Max Volume";
            // 
            // IntervalTextBox
            // 
            IntervalTextBox.BackColor = SystemColors.Window;
            IntervalTextBox.Location = new Point(142, 156);
            IntervalTextBox.MaxLength = 3;
            IntervalTextBox.Name = "IntervalTextBox";
            IntervalTextBox.Size = new Size(48, 23);
            IntervalTextBox.TabIndex = 26;
            IntervalTextBox.Text = "32";
            IntervalTextBox.TextChanged += IntervalTextBox_TextChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(13, 159);
            label11.Name = "label11";
            label11.Size = new Size(114, 15);
            label11.TabIndex = 27;
            label11.Text = "Update Interval (ms)";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(198, 159);
            label12.Name = "label12";
            label12.Size = new Size(128, 15);
            label12.TabIndex = 28;
            label12.Text = "VTSpec_Toggle Param: ";
            // 
            // VTSpecParamCombobox
            // 
            VTSpecParamCombobox.FormattingEnabled = true;
            VTSpecParamCombobox.Items.AddRange(new object[] { "0", "0.25", "0.5", "0.75", "1" });
            VTSpecParamCombobox.Location = new Point(332, 156);
            VTSpecParamCombobox.MaxDropDownItems = 5;
            VTSpecParamCombobox.MaxLength = 4;
            VTSpecParamCombobox.Name = "VTSpecParamCombobox";
            VTSpecParamCombobox.Size = new Size(90, 23);
            VTSpecParamCombobox.TabIndex = 29;
            VTSpecParamCombobox.Text = "0";
            VTSpecParamCombobox.SelectedIndexChanged += VTSpecParamCombobox_SelectedIndexChanged;
            // 
            // ComboBoxAudioDevices
            // 
            ComboBoxAudioDevices.FormattingEnabled = true;
            ComboBoxAudioDevices.Location = new Point(259, 186);
            ComboBoxAudioDevices.Name = "ComboBoxAudioDevices";
            ComboBoxAudioDevices.Size = new Size(163, 23);
            ComboBoxAudioDevices.TabIndex = 30;
            ComboBoxAudioDevices.SelectedIndexChanged += ComboBoxAudioDevices_SelectedIndexChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(173, 189);
            label13.Name = "label13";
            label13.Size = new Size(80, 15);
            label13.TabIndex = 31;
            label13.Text = "Audio Device:";
            // 
            // linkAbout
            // 
            linkAbout.AutoSize = true;
            linkAbout.Location = new Point(335, 427);
            linkAbout.Name = "linkAbout";
            linkAbout.Size = new Size(87, 15);
            linkAbout.TabIndex = 32;
            linkAbout.TabStop = true;
            linkAbout.Text = "About this App";
            linkAbout.LinkClicked += LinkAbout_LinkClicked;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 450);
            Controls.Add(linkAbout);
            Controls.Add(label13);
            Controls.Add(ComboBoxAudioDevices);
            Controls.Add(VTSpecParamCombobox);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(IntervalTextBox);
            Controls.Add(label10);
            Controls.Add(labelMaxVolume);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(labelBrilliance);
            Controls.Add(labelPresence);
            Controls.Add(labelUpperMid);
            Controls.Add(labelMidrange);
            Controls.Add(labelLowMid);
            Controls.Add(labelBass);
            Controls.Add(labelSubBass);
            Controls.Add(TaskButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(LogTextBox);
            Controls.Add(PortTextBox);
            Controls.Add(UrlTextBox);
            Controls.Add(ConnectButton);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormMain";
            Text = "VT Spectralizer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ConnectButton;
        private TextBox UrlTextBox;
        private TextBox PortTextBox;
        private TextBox LogTextBox;
        private Label label1;
        private Label label2;
        private Button TaskButton;
        private Label labelSubBass;
        private Label labelBass;
        private Label labelLowMid;
        private Label labelMidrange;
        private Label labelUpperMid;
        private Label labelPresence;
        private Label labelBrilliance;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label labelMaxVolume;
        private Label label10;
        private TextBox IntervalTextBox;
        private Label label11;
        private Label label12;
        private ComboBox VTSpecParamCombobox;
        private ComboBox ComboBoxAudioDevices;
        private Label label13;
        private LinkLabel linkAbout;
    }
}

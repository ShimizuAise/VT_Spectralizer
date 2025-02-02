using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VTS.Core;
using Newtonsoft.Json;
using VT_Spectralizer.app;
using static VT_Spectralizer.app.SettingsHandler;
using System.Threading;
using System.Security.Policy;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using Microsoft.VisualBasic;
using WebSocketSharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;

namespace VT_Spectralizer
{
    public partial class FormMain : Form
    {
        private const string plugin_name = "VTSpectralizer";
        private const string developer = "ShimizuAise";
        private int updateInterval = 32;
        private long nextSendMS = 0;

        // VTS Stuff
        private bool isConnected = false;
        private float paramToggle = 0f;

        // Handle the Looping Tasks
        private bool isRunning = false;

        //audio capture stuff
        private MMDeviceEnumerator deviceEnumerator;
        private AudioCaptureFromOutput audioCapture;
        private string audioOutputDeviceGuid;

        private const string iconPath = ".\\Resources\\icon_128.png";

        VTubeStudioController vtsController;
        SettingsHandler settingsHandler;

        public FormMain()
        {
            settingsHandler = new SettingsHandler();
            InitializeComponent();
            InitializeAudioDevices();
            LoadSettings();
            linkAbout.Links.Add(0, linkAbout.Text.Length, "https://github.com/ShimizuAise/VT_Spectralizer");
            ConsoleVTSLoggerImpl vtsLogger = new ConsoleVTSLoggerImpl();
            string iconString = string.Empty;
            audioOutputDeviceGuid = string.Empty;
            audioCapture = new AudioCaptureFromOutput(audioOutputDeviceGuid, this);
            if (File.Exists(iconPath))
            {
                var iconBytes = File.ReadAllBytes(iconPath);
                iconString = Convert.ToBase64String(iconBytes);
            }
            vtsController = new VTubeStudioController(vtsLogger, updateInterval, plugin_name, developer, iconString, this);
            PortTextBox.Text = vtsController.GetPort().ToString();
            UrlTextBox.Text = "127.0.0.1";
        }

        public void LoadSettings()
        {
            // UrlTextBox.Text = settings.Url;
            // PortTextBox.Text = settings.Port;

            // Set the Interval Setting.
            string intervalSetting = settingsHandler.LoadSetting("updateInterval");
            string paramFloatString = settingsHandler.LoadSetting("paramToggle");
            string savedGuid = settingsHandler.LoadSetting("audioOutputDeviceGuid");

            if (intervalSetting.IsNullOrEmpty()) intervalSetting = "16";
            updateInterval = Math.Max(16, Int32.Parse(intervalSetting));
            IntervalTextBox.Text = updateInterval.ToString();
            if (paramFloatString.IsNullOrEmpty()) paramFloatString = "0";
            paramToggle = float.Parse(paramFloatString);
            VTSpecParamCombobox.Text = paramFloatString;


            foreach (ComboBoxItem item in ComboBoxAudioDevices.Items)
            {
                if (item.Value == savedGuid)
                {
                    ComboBoxAudioDevices.SelectedItem = item;
                    audioOutputDeviceGuid = savedGuid;
                    return;
                }
            }
            ComboBoxAudioDevices.SelectedIndex = 0;
        }

        public void SaveSettings()
        {
            settingsHandler.UpdateSettings("updateInterval", updateInterval.ToString());
            settingsHandler.UpdateSettings("paramToggle", paramToggle.ToString());
            settingsHandler.UpdateSettings("audioOutputDeviceGuid", audioOutputDeviceGuid);

        }

        private void InitializeAudioDevices()
        {
            deviceEnumerator = new MMDeviceEnumerator();
            var devices = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            ComboBoxAudioDevices.Items.Clear();
            ComboBoxAudioDevices.Items.Add(new ComboBoxItem("Default", string.Empty));
            foreach (var device in devices)
            {
                ComboBoxAudioDevices.Items.Add(new ComboBoxItem(device.FriendlyName, device.ID));
            }
        }

        private async void VTSConnect()
        {
            if (isConnected) return;
            bool connected = await vtsController.ConnectToVtubeStudio();
            if (connected)
            {
                isConnected = true;
                ConnectButton.Text = "Disconnect";
                IntervalTextBox.Enabled = false;
                IntervalTextBox.BackColor = Color.LightGray;
            }
        }

        private void VTSDisconnect()
        {
            if (vtsController.IsConnected) vtsController.Disconnect();
            isConnected = false;
            ConnectButton.Text = "Connect";
            IntervalTextBox.Enabled = true;
            IntervalTextBox.BackColor = Color.White;
        }

        public void StartApp()
        {
            TaskButton.Enabled = false;
            if (audioCapture != null)
            {
                audioCapture.StopCapture(UpdateLog); // Stop any previous capture
            }
            audioCapture = new AudioCaptureFromOutput(audioOutputDeviceGuid, this);
            audioCapture.StartCapture(UpdateLog);
            isRunning = true;
            TaskButton.Text = "Stop Audio Capture";
            TaskButton.Enabled = true;
            ComboBoxAudioDevices.Enabled = false;
        }

        public void StopApp()
        {
            audioCapture.StopCapture(UpdateLog);
            TaskButton.Enabled = false;
            isRunning = false;
            TaskButton.Text = "Start Audio Capture";
            UpdateFrequencyBands(new float[] { 0f, 0f, 0f, 0f, 0f, 0f, 0f });
            TaskButton.Enabled = true;
            ComboBoxAudioDevices.Enabled = true;
        }

        // Update frequency bands UI (you can display these values in labels or a chart)
        public void UpdateFrequencyBands(float[] frequencyVolumes)
        {
            // Display frequency volumes for each band (e.g., update labels or a progress bar)
            labelSubBass.Text = $"{(int)frequencyVolumes[0]}%";
            labelBass.Text = $"{(int)frequencyVolumes[1]}%";
            labelLowMid.Text = $"{(int)frequencyVolumes[2]}%";
            labelMidrange.Text = $"{(int)frequencyVolumes[3]}%";
            labelUpperMid.Text = $"{(int)frequencyVolumes[4]}%";
            labelPresence.Text = $"{(int)frequencyVolumes[5]}%";
            labelBrilliance.Text = $"{(int)frequencyVolumes[6]}%";
            labelMaxVolume.Text = $"{(int)frequencyVolumes.Max()}%";

            long currentTimeMS = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            if (currentTimeMS > nextSendMS)
            {
                vtsController.SendParams(frequencyVolumes, paramToggle);
                nextSendMS = currentTimeMS + updateInterval;
            }
        }

        public void UpdateLog(string message)
        {
            LogTextBox.AppendText(message);
            LogTextBox.AppendText(Environment.NewLine);
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            string url = UrlTextBox.Text;
            string port = PortTextBox.Text;
            if (string.IsNullOrWhiteSpace(url) || string.IsNullOrWhiteSpace(port))
            {
                MessageBox.Show("Please enter both URL and Port.");
                return;
            }
            string fullUrl = $"ws://{url}:{port}/";

            if (isConnected) VTSDisconnect();
            else VTSConnect();
        }

        private void TaskButton_Click(object sender, EventArgs e)
        {
            if (isRunning) StopApp(); else StartApp();
        }

        private void PortTextBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(PortTextBox.Text, "[^0-9]"))
            {
                PortTextBox.Text = PortTextBox.Text.Remove(PortTextBox.Text.Length - 1);
            }
            // SaveSettings();
        }

        private void IntervalTextBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(IntervalTextBox.Text, "[^0-9]"))
            {
                IntervalTextBox.Text = IntervalTextBox.Text.Remove(IntervalTextBox.Text.Length - 1);
            }
            updateInterval = Math.Max(Int32.Parse(IntervalTextBox.Text), 10);
            SaveSettings();
        }

        private void VTSpecParamCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = VTSpecParamCombobox.Text;
            paramToggle = float.Parse(value);
            SaveSettings();
        }

        private void ComboBoxAudioDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = (ComboBoxItem)ComboBoxAudioDevices.SelectedItem;
            audioOutputDeviceGuid = selectedItem.Value;
            audioCapture = new AudioCaptureFromOutput(selectedItem.Value, this);
            SaveSettings();
        }

        private void LinkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Open the URL in the default browser
            Process.Start(new ProcessStartInfo(e.Link.LinkData.ToString()) { UseShellExecute = true });
        }

        // Helper class to store the combo item (display text and value)
        public class ComboBoxItem
        {
            public string DisplayText { get; }
            public string Value { get; }

            public ComboBoxItem(string displayText, string value)
            {
                DisplayText = displayText;
                Value = value;
            }

            public override string ToString()
            {
                return DisplayText; // Display the text in ComboBox
            }
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace VT_Spectralizer.app
{
    public class SettingsHandler
    {
        private string settingsFilePath;
        private Dictionary<string, string> settings;

        public SettingsHandler()
        {
            settingsFilePath = "application.json";

            // Initialize settings dictionary and load settings from file
            settings = LoadSettings();
        }

        // Load settings from the JSON file, if it exists
        private Dictionary<string, string> LoadSettings()
        {
            if (File.Exists(settingsFilePath))
            {
                string json = File.ReadAllText(settingsFilePath);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
            }
            return new Dictionary<string, string>();
        }

        // Save settings to the JSON file
        private void SaveSettings()
        {
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(settingsFilePath, json);
        }

        // Update a setting or add it if it doesn't exist
        public void UpdateSettings(string key, string value)
        {
            if (settings.ContainsKey(key))
            {
                settings[key] = value;
            }
            else
            {
                settings.Add(key, value);
            }

            SaveSettings();
        }

        // Load a setting by key
        public string LoadSetting(string key)
        {
            if (settings.TryGetValue(key, out string value))
            {
                return value;
            }
            return string.Empty; // Or return a default value if preferred
        }
    }
}


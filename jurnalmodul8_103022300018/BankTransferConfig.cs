using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace jurnalmodul8_103022300018
{
    class banktransferconfig
    {
        public string lang { get; set; }
        public Transfer transfer { get; set; }
        public string[] method { get; set; }
        public string confirmation { get; set; }

        private const string ConfigPath = "covid_config.json";

        public void bankconfig()
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            string json = System.IO.File.ReadAllText(ConfigPath);
            banktransferconfig config = JsonConvert.DeserializeObject<banktransferconfig>(json);
            lang = config.lang;
            transfer = config.transfer;
            method = config.method;
            confirmation = config.confirmation;
        }
        private banktransferconfig ReadConfigFile()
        {
            string json = System.IO.File.ReadAllText(ConfigPath);
            banktransferconfig config = JsonConvert.DeserializeObject<banktransferconfig>(json);
            return config;
        }
        private void WriteNewConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            String jsonString = System.Text.Json.JsonSerializer.Serialize(ConfigPath, options);
            File.WriteAllText(ConfigPath, jsonString);
        }
    }
}


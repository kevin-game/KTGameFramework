using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KT
{
    public class ConfigService : IConfigService
    {
        private readonly Dictionary<string, string> _settings = new Dictionary<string, string>();
        public GameApplicationConfig GameApplicationConfig { get; private set; }
        
        public ConfigService()
        {
            LoadGameApplicationConfig();
            Debug.Log(GameApplicationConfig.PlayMode.ToString());
        }

        public string GetSetting(string key)
        {
            if (_settings.TryGetValue(key, out var value))
            {
                return value;
            }
            return null;
        }

        public void SetSetting(string key, string value)
        {
            _settings[key] = value;
        }

        private void LoadGameApplicationConfig()
        {
            GameApplicationConfig = Resources.Load<GameApplicationConfig>("GameApplicationConfig");
        }
    }
}
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace KT
{
    public class ConfigService : IConfigService
    {
        public GameApplicationConfig GameApplicationConfig { get; private set; }
        
        public ConfigService()
        {
            LoadGameApplicationConfig();
            Debug.Log(GameApplicationConfig.PlayMode.ToString());
        }

        private void LoadGameApplicationConfig()
        {
            GameApplicationConfig = Resources.Load<GameApplicationConfig>("GameApplicationConfig");
        }
    }
}
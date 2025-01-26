namespace KT
{
    public class ConfigService : IConfigService
    {
        public GameApplicationConfig GameApplicationConfig { get; private set; }
        
        public ConfigService()
        {
            LoadGameApplicationConfig();
        }

        private void LoadGameApplicationConfig()
        {
            GameApplicationConfig = UnityEngine.Resources.Load<GameApplicationConfig>("__KT__GameApplicationConfig");
        }
    }
}
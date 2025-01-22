namespace KT
{
    
    public interface IConfigService
    {
        GameApplicationConfig GameApplicationConfig { get; }
        string GetSetting(string key);
        void SetSetting(string key, string value);
    }
}
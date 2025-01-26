using YooAsset;

namespace KT
{
    /// <summary>
    /// 远端资源地址查询服务类
    /// </summary>
    public class RemoteCdn : IRemoteServices
    {
        private readonly IConfigService _configService;
        
        private readonly string _defaultHostServer;
        private readonly string _fallbackHostServer;

        public RemoteCdn(IConfigService configService)
        {
            _configService = configService;
            
            _defaultHostServer = GetDefaultServerURL();
            _fallbackHostServer = GetFallbackServerURL();
        }

        string IRemoteServices.GetRemoteMainURL(string fileName)
        {
            return $"{_defaultHostServer}/{fileName}";
        }

        string IRemoteServices.GetRemoteFallbackURL(string fileName)
        {
            return $"{_fallbackHostServer}/{fileName}";
        }

        /// <summary>
        /// 获取资源服务器地址
        /// </summary>
        private string GetDefaultServerURL()
            => GetHostServerURL(_configService.GameApplicationConfig.cdnDefaultHost);
        
        private string GetFallbackServerURL()
            => GetHostServerURL(_configService.GameApplicationConfig.cdnFallbackHost);
        
        private string GetHostServerURL(string server)
        {
            var config = _configService.GameApplicationConfig;
#if UNITY_EDITOR
            if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
                return System.IO.Path.Combine(server, config.cdnAndroidDir);
            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS)
                return System.IO.Path.Combine(server, config.cdnIPhoneDir);
            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.WebGL)
                return System.IO.Path.Combine(server, config.cdnWebGLDir);
            else
                return System.IO.Path.Combine(server, config.cdnPCDir);
#else
            if (Application.platform == RuntimePlatform.Android)
                return System.IO.Path.Combine(server, config.cdnAndroidDir);
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                return System.IO.Path.Combine(server, config.cdnIPhoneDir);
            else if (Application.platform == RuntimePlatform.WebGLPlayer)
                return System.IO.Path.Combine(server, config.cdnWebGLDir);
            else
                return System.IO.Path.Combine(server, config.cdnPCDir);
#endif
        }
    }
}
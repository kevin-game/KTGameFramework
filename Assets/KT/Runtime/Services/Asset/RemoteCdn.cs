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
            string host = null;
#if UNITY_EDITOR
            if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
                host = System.IO.Path.Combine(server, config.cdnRootDir, config.cdnAndroidDir);
            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS)
                host = System.IO.Path.Combine(server, config.cdnRootDir, config.cdnIPhoneDir);
            else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.WebGL)
                host = System.IO.Path.Combine(server, config.cdnRootDir, config.cdnWebGLDir);
            else
                host = System.IO.Path.Combine(server, config.cdnRootDir, config.cdnPCDir);
#else
            if (UnityEngine.Application.platform == UnityEngine.RuntimePlatform.Android)
                host = System.IO.Path.Combine(server, config.cdnRootDir, config.cdnAndroidDir);
            else if (UnityEngine.Application.platform == UnityEngine.RuntimePlatform.IPhonePlayer)
                host = System.IO.Path.Combine(server, config.cdnRootDir, config.cdnIPhoneDir);
            else if (UnityEngine.Application.platform == UnityEngine.RuntimePlatform.WebGLPlayer)
                host = System.IO.Path.Combine(server, config.cdnRootDir, config.cdnWebGLDir);
            else
                host = System.IO.Path.Combine(server, config.cdnRootDir, config.cdnPCDir);
#endif
            return host.Replace('\\', '/');
        }
    }
}
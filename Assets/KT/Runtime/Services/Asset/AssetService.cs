using UnityEngine;
using YooAsset;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace KT
{
    public class AssetService : IAssetService
    {
        private readonly ILogService _logService;
        private readonly IConfigService _configService;

        public AssetService(ILogService logService, IConfigService configService)
        {
            _logService = logService;
            _configService = configService;

            // 初始化资源系统
            YooAssets.Initialize();
        }

        /// <summary>
        /// 获取一个初始化后的package。(相当于YooAsset中的InitPackage)
        /// 获取后后要分别调用UpdateVersionAsync()、UpdateManifestAsync()、DownloadAsync()
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        public async UniTask<IAssetPackage> GetPackageAsync(string packageName = "DefaultPackage")
        {
            IAssetPackage assetPackage = await InitPackageAsync(packageName);
            if (!await assetPackage.UpdateVersionAsync()) return null;
            if (!await assetPackage.UpdateManifestAsync()) return null;
            return assetPackage;
        }
        
        private async UniTask<IAssetPackage> InitPackageAsync(string packageName)
        {
            var playMode = _configService.GameApplicationConfig.playMode;

            // 创建资源包裹类
            var package = YooAssets.TryGetPackage(packageName) ?? YooAssets.CreatePackage(packageName);

            // 编辑器下的模拟模式
            // 在编辑器下，不需要构建资源包，来模拟运行游戏。
            InitializationOperation initializationOperation = null;
            if (playMode == EPlayMode.EditorSimulateMode)
            {
                var buildResult = EditorSimulateModeHelper.SimulateBuild(packageName);
                var packageRoot = buildResult.PackageRootDirectory;
                var createParameters = new EditorSimulateModeParameters();
                createParameters.EditorFileSystemParameters =
                    FileSystemParameters.CreateDefaultEditorFileSystemParameters(packageRoot);
                initializationOperation = package.InitializeAsync(createParameters);
            }

            // 单机运行模式
            // 对于不需要热更新资源的游戏，可以使用单机运行模式。
            if (playMode == EPlayMode.OfflinePlayMode)
            {
                var createParameters = new OfflinePlayModeParameters();
                createParameters.BuildinFileSystemParameters =
                    FileSystemParameters.CreateDefaultBuildinFileSystemParameters(packageRoot:System.IO.Path.Combine(Application.streamingAssetsPath, _configService.GameApplicationConfig.cdnRootDir, packageName));
                initializationOperation = package.InitializeAsync(createParameters);
            }

            // 联机运行模式
            // 对于需要热更新资源的游戏，可以使用联机运行模式。
            if (playMode == EPlayMode.HostPlayMode)
            {
                IRemoteServices remoteServices = new RemoteCdn(_configService);
                var createParameters = new HostPlayModeParameters
                {
                    // 如果有首包文件可以进行设置
                    // BuildinFileSystemParameters = FileSystemParameters.CreateDefaultBuildinFileSystemParameters(packageRoot:System.IO.Path.Combine(Application.streamingAssetsPath, _configService.GameApplicationConfig.cdnRootDir, packageName)), 
                    BuildinFileSystemParameters = null,
                    CacheFileSystemParameters = FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices)
                };
                initializationOperation = package.InitializeAsync(createParameters);
            }

            // WebGL运行模式
            // 针对WebGL平台的专属模式，包括微信小游戏，抖音小游戏都需要选择该模式。
            // 注意：微信小游戏，抖音小游戏请参考解决方案文档介绍。
            // 注意：该模式需要构建资源包
            if (playMode == EPlayMode.WebPlayMode)
            {
                var createParameters = new WebPlayModeParameters();
#if UNITY_WEBGL && WEIXINMINIGAME && !UNITY_EDITOR
                IRemoteServices remoteServices = new RemoteCdn(_configService);
                createParameters.WebServerFileSystemParameters =
     WechatFileSystemCreater.CreateWechatFileSystemParameters(remoteServices);
#else
                createParameters.WebServerFileSystemParameters =
                    FileSystemParameters.CreateDefaultWebServerFileSystemParameters();
#endif
                initializationOperation = package.InitializeAsync(createParameters);
            }

            await initializationOperation.ToUniTask();
            return initializationOperation!.Status == EOperationStatus.Succeed
                ? package.ToAssetPackage()
                : null;
        }

    }
}
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace KT
{
    public class AssetPackage : IAssetPackage
    {
        private readonly ResourcePackage _package;
        private string _packageVersion;
        
        public AssetPackage(ResourcePackage package)
        {
            _package = package;
        }
        
        public async UniTask<bool> UpdateVersionAsync()
        {
            var operation = _package.RequestPackageVersionAsync();
            await operation.ToUniTask();
            if (operation.Status != EOperationStatus.Succeed)
                return false;
            
            _packageVersion = operation.PackageVersion;
            return true;
        }
        
        public async UniTask<bool> UpdateManifestAsync()
        {
            var operation = _package.UpdatePackageManifestAsync(_packageVersion);
            await operation.ToUniTask();
            return operation.Status == EOperationStatus.Succeed;
        }
        
        public async UniTask<bool> DownloadAsync(DownloaderOperation.DownloadUpdate downloadUpdateCallback, DownloaderOperation.DownloadError downloadErrorCallback)
        {
            int downloadingMaxNum = 10;
            int failedTryAgain = 3;
            var downloader = _package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);

            if (downloader.TotalDownloadCount == 0)
                return true;
            
            // 发现新更新文件后，挂起流程系统
            // TODO：需要在下载前检测磁盘空间不足
            int totalDownloadCount = downloader.TotalDownloadCount;
            long totalDownloadBytes = downloader.TotalDownloadBytes;
            
            // 开始下载资源
            downloader.DownloadUpdateCallback = downloadUpdateCallback;
            downloader.DownloadErrorCallback = downloadErrorCallback;
            downloader.BeginDownload();
            await downloader.ToUniTask();

            // 检测下载结果
            if (downloader.Status != EOperationStatus.Succeed)
                return false;

            // 下载结束，清理一下资源
            await _package.ClearCacheFilesAsync(EFileClearMode.ClearUnusedBundleFiles).ToUniTask();
            return true;
        }

        public void Load<T>(string assetPath, Action<AssetHandle> completed) where T : UnityEngine.Object
        {
            AssetHandle handle = _package.LoadAssetAsync<T>(assetPath);
            handle.Completed += completed;
            // return handle.AssetObject as T;
        }

        public async UniTask<T> LoadAsync<T>(string assetPath) where T : UnityEngine.Object
        {
            YooAssets.SetDefaultPackage(_package);
            AssetHandle handle = _package.LoadAssetAsync<T>(assetPath);
            await handle.ToUniTask();
            return handle.AssetObject as T;
        }
        
        public UniTask<GameObject> LoadPrefabAsync(string assetPath)
            => LoadAsync<GameObject>(assetPath);

        public UniTask<AudioClip> LoadAudioClipAsync(string assetPath)
            => LoadAsync<AudioClip>(assetPath);

        public UniTask<Sprite> LoadSpriteAsync(string assetPath)
            => LoadAsync<Sprite>(assetPath);

        public async UniTask<SceneHandle> LoadSceneAsync(string assetPath, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            SceneHandle sceneHandle = _package.LoadSceneAsync(assetPath, mode);
            await sceneHandle.ToUniTask();
            return sceneHandle;
        }

    }
}
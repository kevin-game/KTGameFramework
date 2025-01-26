using Cysharp.Threading.Tasks;
using YooAsset;

namespace KT
{
    public interface IAssetPackage
    {

        public UniTask<bool> UpdateVersionAsync();
        public UniTask<bool> UpdateManifestAsync();
        public UniTask<bool> DownloadAsync(DownloaderOperation.DownloadUpdate downloadUpdateCallback, DownloaderOperation.DownloadError downloadErrorCallback);


        public void Load<T>(string assetPath, System.Action<AssetHandle> completed) where T : UnityEngine.Object;

        public UniTask<T> LoadAsync<T>(string assetPath) where T : UnityEngine.Object;

        public UniTask<UnityEngine.GameObject> LoadPrefabAsync(string assetPath);

        public UniTask<UnityEngine.AudioClip> LoadAudioClipAsync(string assetPath);

        public UniTask<UnityEngine.Sprite> LoadSpriteAsync(string assetPath);

        public UniTask<SceneHandle> LoadSceneAsync(string assetPath,
            UnityEngine.SceneManagement.LoadSceneMode mode);
    }
}
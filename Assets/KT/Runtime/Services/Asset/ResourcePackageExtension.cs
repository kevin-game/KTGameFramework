using YooAsset;

namespace KT
{
    public static class ResourcePackageExtension
    {
        public static IAssetPackage ToAssetPackage(this ResourcePackage resourcePackage)
            => new AssetPackage(resourcePackage);
    }
}
using Cysharp.Threading.Tasks;

namespace KT
{
    public interface IAssetService
    {
        public UniTask<IAssetPackage> GetPackageAsync(string packageName = "DefaultPackage");
    }
}
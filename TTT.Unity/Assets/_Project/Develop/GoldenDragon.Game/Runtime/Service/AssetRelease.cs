using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service
{
    public class AssetRelease
    {
        private AssetCatch _assetCatch;

        public void Initialized(AssetCatch assetCatch)
        {
            _assetCatch = assetCatch;
        }

        public void ReleaseAsset<T>(TypeAsset typeAsset,string nameAsset) where T:class
        {
            AsyncOperationHandle<T> asset = _assetCatch.Release<T>(typeAsset,nameAsset);
            asset.Release();
        }

        public async UniTask ReleaseAssetAsync<T>(TypeAsset typeAsset,string nameAsset) where T:class
        {
            AsyncOperationHandle<T> asset = _assetCatch.Release<T>(typeAsset,nameAsset);
            asset.Release();

            await UniTask.CompletedTask;
        }
    }
}
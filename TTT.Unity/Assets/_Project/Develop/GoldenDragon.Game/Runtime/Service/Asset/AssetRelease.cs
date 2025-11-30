using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset
{
    public class AssetRelease
    {
        private AssetCatch _assetCatch;

        public AssetRelease(AssetCatch assetCatch)
        {
            _assetCatch = assetCatch;
        }

        public void ReleaseAsset<T>(TypeAsset typeAsset,string nameAsset) where T:class
        {
            if (_assetCatch.TryGetRelease<T>(typeAsset,nameAsset, out AsyncOperationHandle<T> asset)) 
                asset.Release();
        }

        public async UniTask ReleaseAssetAsync<T>(TypeAsset typeAsset,string nameAsset) where T:class
        {
            if (_assetCatch.TryGetRelease<T>(typeAsset,nameAsset, out AsyncOperationHandle<T> asset)) 
                asset.Release();

            await UniTask.CompletedTask;
        }
    }
}
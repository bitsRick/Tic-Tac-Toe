using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities
{    
    public enum TypeAsset
    {
        Meta_UI,
        Popup,
    }
        public class AssetLoad
        {
            private readonly AssetCatch _assetCatch = new AssetCatch();

            public T GetAssetAsync<T>(TypeAsset typeAsset,string nameAsset)where T : UnityEngine.Object
            {
                Log.Default.D($"Loading asset[{typeAsset}] path:{nameAsset}");

                AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(nameAsset);
                _assetCatch.Add(typeAsset, nameAsset, handle);
                
                return handle.WaitForCompletion();
            }

            public async UniTask ReleaseAsync<T>(TypeAsset typeAsset,string nameAsset) where T:class
            {
                AsyncOperationHandle<T> asset = _assetCatch.Get<T>(typeAsset,nameAsset);
                asset.Release();

                await UniTask.CompletedTask;
            }
        }
}
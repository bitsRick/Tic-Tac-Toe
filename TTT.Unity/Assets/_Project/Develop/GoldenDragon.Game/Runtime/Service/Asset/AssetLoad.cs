using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset
{
    public class AssetLoad
        {
            private readonly AssetCatch _assetCatch = new AssetCatch();

            public T GetAsset<T>(TypeAsset typeAsset,string nameAsset)where T : UnityEngine.Object
            {
                Log.Default.D($"Loading asset[{typeAsset}] path:{nameAsset}");

                AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(nameAsset);
                _assetCatch.Add(typeAsset, nameAsset, handle);
                
                return handle.WaitForCompletion();
            }

            public async UniTask<T> GetAssetAsync<T>(TypeAsset typeAsset,string tagPopup) where T:UnityEngine.Object
            {
                Log.Default.D($"Loading asset[Popup] path:{tagPopup}");

                AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(tagPopup);
                _assetCatch.Add(typeAsset,tagPopup,handle);
                
                return await handle.ToUniTask();
            }

            public async UniTask ReleaseAssetAsync<T>(TypeAsset typeAsset,string nameAsset) where T:class
            {
                AsyncOperationHandle<T> asset = _assetCatch.Get<T>(typeAsset,nameAsset);
                asset.Release();

                await UniTask.CompletedTask;
            }
        }
}
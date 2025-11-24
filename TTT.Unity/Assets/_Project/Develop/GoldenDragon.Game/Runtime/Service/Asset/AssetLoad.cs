using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset
{
    public class AssetLoad
        {
            private AssetCatch _assetCatch;

            public AssetLoad(AssetCatch assetCatch)
            {
                _assetCatch = assetCatch;
            }

            public T GetAsset<T>(TypeAsset typeAsset,string nameAsset)where T : UnityEngine.Object
            {
                Log.Default.D($"Loading asset[{typeAsset}] path:{nameAsset}");

                if (_assetCatch.TryGet<T>(typeAsset, nameAsset,out AsyncOperationHandle<T> handleOut))
                    return handleOut.WaitForCompletion();

                AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(nameAsset);
                _assetCatch.Add(typeAsset, nameAsset, handle);
                
                return handle.WaitForCompletion();
            }

            public async UniTask<T> GetAssetAsync<T>(TypeAsset typeAsset,string nameAsset) where T:UnityEngine.Object
            {
                Log.Default.D($"Loading asset[Popup] path:{nameAsset}");
                
                if (_assetCatch.TryGet<T>(typeAsset, nameAsset,out AsyncOperationHandle<T> handleOut))
                    return handleOut.WaitForCompletion();

                AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(nameAsset);
                _assetCatch.Add(typeAsset,nameAsset,handle);
                
                return await handle.ToUniTask();
            }
        }
}
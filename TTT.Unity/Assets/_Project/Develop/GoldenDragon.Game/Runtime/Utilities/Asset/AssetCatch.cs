using System.Collections.Generic;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities
{
    public class AssetCatch
    {
        private readonly Dictionary<TypeAsset, Dictionary<string, object>> _assetCollection = new();

        public void Add<T>(TypeAsset typeAsset, string nameAsset, AsyncOperationHandle<T> handle) where T : class
        {
            _assetCollection.Add(typeAsset,new Dictionary<string, object> { { nameAsset, handle } });
        }

        public AsyncOperationHandle<T> Get<T>(TypeAsset typeAsset, string nameAsset) where T:class
        {
            if (_assetCollection.ContainsKey(typeAsset) == false ||
                _assetCollection[typeAsset].ContainsKey(nameAsset) == false)
            {
                Log.Default.W($"Asset not found...[{typeAsset}-{nameAsset}]");
                return default;
            }
            
            
            AsyncOperationHandle<T> asyncOperationHandle = 
                (AsyncOperationHandle<T>)_assetCollection[typeAsset][nameAsset];

            _assetCollection[typeAsset].Remove(nameAsset);

            if (_assetCollection[typeAsset].Count == 0) _assetCollection.Remove(typeAsset);

            return asyncOperationHandle;
        }
    }
}
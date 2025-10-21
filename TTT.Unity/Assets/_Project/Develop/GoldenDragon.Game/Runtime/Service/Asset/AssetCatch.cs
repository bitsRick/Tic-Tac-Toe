using System.Collections.Generic;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset
{
    public class AssetCatch
    {
        private readonly Dictionary<TypeAsset, Dictionary<string, object>> _assetCollection = new();

        public void Add<T>(TypeAsset typeAsset, string nameAsset, AsyncOperationHandle<T> handle) where T : class
        {
            if (_assetCollection.TryGetValue(typeAsset,out Dictionary<string,object> dicVal))
            {
                if (dicVal.ContainsKey(nameAsset))
                {
                    Log.Default.W($"{nameAsset} is asset added in Dictionary:{nameof(_assetCollection)}");
                    Addressables.Release(handle);
                }
                else
                {
                    dicVal.Add(nameAsset,handle);
                }
            }
            else
            {
                _assetCollection.Add(typeAsset,new Dictionary<string, object>(){{nameAsset,handle}});   
            }
        }

        public bool TryGet<T>(TypeAsset typeAsset, string nameAsset, out AsyncOperationHandle<T> handle) where T : Object
        {
            if (_assetCollection.ContainsKey(typeAsset) == false ||
                _assetCollection[typeAsset].ContainsKey(nameAsset) == false)
            {
                Log.Default.W($"Asset not found...[{typeAsset}-{nameAsset}]");
                handle = new AsyncOperationHandle<T>();
                return false;
            }
            
            handle =  (AsyncOperationHandle<T>)_assetCollection[typeAsset][nameAsset];
            return true;
        }

        public AsyncOperationHandle<T> Release<T>(TypeAsset typeAsset, string nameAsset) where T:class
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
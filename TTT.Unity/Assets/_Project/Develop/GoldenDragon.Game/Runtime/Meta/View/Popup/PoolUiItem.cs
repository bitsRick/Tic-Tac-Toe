using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup
{
    public class PoolUiItem<T> : ILoadUnit<DataPullUiItem>,IDisposable 
    where T : class
    {
        private List<T> _listItem = new List<T>();
        private AssetService _assetService;
        private int _index = -1;
        private GameObject _poolRoot;
        
        public List<T> Item => _listItem;

        public PoolUiItem(AssetService assetService)
        {
            _assetService = assetService;
        }

        public async UniTask Load(DataPullUiItem dataPullUiItem)
        {
            _poolRoot = new GameObject(dataPullUiItem.NamePullObjectContainer);
            
            for (int i = 0; i < dataPullUiItem.LenghtPull; i++)
            {
                GameObject gameObject = _assetService.Install.InstallToGameObject<GameObject>(
                    _assetService.Load
                        .GetAsset<GameObject>(TypeAsset.Elements, dataPullUiItem.PrefabPath));
                    
                gameObject.transform.parent = _poolRoot.gameObject.transform;
                gameObject.SetActive(false);
                
                var newItem = gameObject.GetComponent<T>();
                _listItem.Add(newItem);
                
                if (i % 5 == 0) await UniTask.Yield();
            }
            
            await UniTask.CompletedTask;
        }
        
        public void Dispose() => _listItem = null;

        public T GetItem()
        {
            if (_listItem.Count-1 <= _index)
            {
                ResetIndex();
                return null;
            }
            
            _index++;
            return _listItem[_index];
        }

        public void ResetIndex()
        {
            _index = -1;
        }
    }
}
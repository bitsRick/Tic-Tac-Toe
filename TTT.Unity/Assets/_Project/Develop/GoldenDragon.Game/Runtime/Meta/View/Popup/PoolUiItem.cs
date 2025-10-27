using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.Interface;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup
{
    public class PoolUiItem<T> : ILoadUnit<DataPullUiItem>,IDisposable,IEnumerable<T>
        where T : IItem
    {
        public List<T> _item = new List<T>();
        private AssetService _assetService;
        private GameObject _poolRoot;
        private int _index = -1;

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
                _item.Add(newItem);

                await UniTask.Yield();
            }
            
            await UniTask.CompletedTask;
        }
        
        public void Dispose() => _item = null;

        public T Find(string id) => _item.FirstOrDefault(key => key.Id == id);

        public T GetItem()
        {
            if (_index >= _item.Count-1) Reset();

            _index++;
            return _item[_index];
        }
        
        public void Reset()
        {
            _index = -1;
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _item.Count; i++)
            {
                _index++;
                yield return _item[i];
            }
        }

        public int GetIndex()
        {
            return _index;
        }
    }
}
using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup
{
    public class PoolUiElement<T> : ILoadUnit<int>,IDisposable 
    where T : class
    {
        private List<T> _listElement = new List<T>();
        private AssetService _assetService;
        private int _index = -1;
        private GameObject _poolRoot;
        
        public List<T> Elements => _listElement;

        public PoolUiElement(AssetService assetService)
        {
            _assetService = assetService;
        }

        public async UniTask Load(int lenght)
        {
            _poolRoot = new GameObject(Constant.M.Asset.Popup.PoolElementUi);
            
            for (int i = 0; i < lenght; i++)
            {
                var gameObject = _assetService.Install.InstallToGameObject<GameObject>(
                    _assetService.Load
                        .GetAsset<GameObject>(TypeAsset.Elements, Constant.M.Asset.Popup.ShopElementBuyPrefab));
                    
                gameObject.transform.parent = _poolRoot.gameObject.transform;
                gameObject.SetActive(false);
                var newElement = gameObject.GetComponent<T>();
                _listElement.Add(newElement);
                
                if (i % 5 == 0) await UniTask.Yield();
            }
            
            await UniTask.CompletedTask;
        }
        
        public void Dispose() => _listElement = null;

        public T GetElement()
        {
            if (_listElement.Count-1 <= _index)
            {
                ResetIndex();
                return null;
            }
            
            _index++;
            return _listElement[_index];
        }

        public void ResetIndex()
        {
            _index = -1;
        }
    }
}
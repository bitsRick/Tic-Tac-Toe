using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui
{
    public class FactoryUi
    {
        private AssetService _assetService;

        public FactoryUi(AssetService assetService)
        {
            _assetService = assetService;
        }

        public T CreateRootUi<T>(TypeAsset type, string nameAsset)where T: class
        {
            var hudObject = _assetService.Load.GetAsset<GameObject>(type, nameAsset);
            return _assetService.Install.InstallToRoot<GameObject>(hudObject).GetComponent<T>();
        }

        public async UniTask<GameObject> LoadPopupToObject(string tagPopup)
        {
            return await _assetService.Load.GetAssetAsync<GameObject>(TypeAsset.Popup,tagPopup);
        }
    }
}
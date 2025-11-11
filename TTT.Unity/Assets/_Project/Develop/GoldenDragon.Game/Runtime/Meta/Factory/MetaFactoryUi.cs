using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;
using VContainer;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory
{
    public class MetaFactoryUi
    {
        private AssetService _assetService;
        private IObjectResolver _resolver;

        public MetaFactoryUi(AssetService assetService,IObjectResolver resolver)
        {
            _resolver = resolver;
            _assetService = assetService;
        }

        public T CreateMetaRoot<T>()where T: class
        {
            var hudObject = _assetService.Load.GetAsset<GameObject>(TypeAsset.Meta_UI, Constant.M.Asset.Ui.MetaRoot);
            var metaRoot = _assetService.Install.InstallToRoot<GameObject>(hudObject).GetComponent<T>();
            
            _resolver.Inject(metaRoot);
            
            return metaRoot;
        }

        public async UniTask<GameObject> LoadPopupToObject(string tagPopup)
        {
            return await _assetService.Load.GetAssetAsync<GameObject>(TypeAsset.Popup,tagPopup);
        }
    }
}
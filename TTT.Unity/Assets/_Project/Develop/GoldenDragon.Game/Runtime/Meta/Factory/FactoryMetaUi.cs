using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory
{
    public class FactoryMetaUi
    {
        private AssetService _assetService;

        public FactoryMetaUi(AssetService assetService)
        {
            _assetService = assetService;
        }

        public T CreateMetaRoot<T>()where T: class
        {
            var hudObject = _assetService.Load.GetAssetAsync<GameObject>(TypeAsset.Meta_UI, Constant.M.Asset.Ui.MetaRoot);
            var metaRoot = hudObject.GetComponent<T>();
            return metaRoot;
        }
    }
}
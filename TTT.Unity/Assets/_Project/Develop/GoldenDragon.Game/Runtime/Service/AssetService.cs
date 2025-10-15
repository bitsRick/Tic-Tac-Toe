using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service
{

    public class AssetService
    {
        private AssetInstall _assetInstall;
        private AssetLoad _assetLoad;

        public AssetInstall Install => _assetInstall;
        public AssetLoad Load => _assetLoad;

        public AssetService(AssetInstall assetInstall, AssetLoad assetLoad)
        {
            _assetLoad = assetLoad;
            _assetInstall = assetInstall;
        }
    }
}
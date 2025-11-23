using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service
{
    public class AssetService
    {
        private readonly AssetCatch _assetCatch = new AssetCatch();
        
        public AssetInstall Install => new AssetInstall();
        public AssetLoad Load => new AssetLoad();
        public AssetRelease Release => new AssetRelease();
        
        public void Initialized()
        {
            Load.Initialized(_assetCatch);
            Release.Initialized(_assetCatch);
        }
    }
}
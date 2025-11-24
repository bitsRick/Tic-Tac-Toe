using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service
{
    public class AssetService
    {
        public AssetInstall Install;
        public AssetLoad Load;
        public AssetRelease Release;

        public AssetService(AssetInstall install, AssetLoad load, AssetRelease release)
        {
            Install = install;
            Load = load;
            Release = release;
        }
    }
}
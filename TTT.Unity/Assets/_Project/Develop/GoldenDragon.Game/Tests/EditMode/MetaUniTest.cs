using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using NUnit.Framework;

namespace _Project.Develop.GoldenDragon.Game.Tests.EditMode
{
    public class MetaUniTest
    {
        [Test]
        public  void Test_Addressable_Load_ViewHudMeta()
        {
            //arange
            FactoryMetaUi factoryMetaUi = new FactoryMetaUi(new AssetService(new AssetInstall(),new AssetLoad()));
            
            //act
            MetaRoot hud = factoryMetaUi.CreateMetaRoot<MetaRoot>();


            //assert
            Assert.NotNull(hud);
        }
    }
}

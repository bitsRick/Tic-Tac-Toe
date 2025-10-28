using System.Collections;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.InventoryItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.ShopElementItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Style;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace _Project.Develop.GoldenDragon.Game.Tests.EditMode
{
    public class MetaUniTest
    {
        [Test]
        public  void Test_Load_Json_Data_Style()
        {
            // //arange
            // var text =  Addressables.LoadAssetAsync<TextAsset>(Constant.L.Asset.DataStyleJson);
            //
            // // //act
            // StyleData[] styleData = JsonUtility.FromJson<StyleData[]>(text.Result.text);
            //
            //
            // // //assert
            // Assert.NotNull(styleData);
        }
        
        [UnityTest]
        public IEnumerator Test_Style_Data_Load()
        {
            return UniTask.ToCoroutine(async () =>
            {
                //arange
                var assetService = new AssetService(new AssetInstall(),new AssetLoad());
                var styleData = new StyleDataLoad(assetService.Load);
                
                //act
                await styleData.Load();

                //assert
                Assert.NotNull(styleData.GetData());
            });
        }

        [UnityTest]
        public IEnumerator Test_Pool_Item_Inventory()
        {
            return UniTask.ToCoroutine(async () =>
            {
                //arange
                var assetService = new AssetService(new AssetInstall(),new AssetLoad());
                var pull = new PoolUiItem<ItemInventoryStyle>(assetService);
                var styleData = new StyleDataLoad(assetService.Load);
                
                //act
                await styleData.Load();
                DataPullUiItem item = new DataPullUiItem(
                    Constant.M.Asset.Popup.InventoryElementStylePrefab,
                    Constant.M.Asset.Popup.PoolElementUi,
                    styleData.GetData().Length);
                await pull.Load(item);

                //assert
                Assert.NotNull(pull.GetItem());
            });
        }
        
        [UnityTest]
        public IEnumerator Test_Pool_Item_Shop()
        {
            return UniTask.ToCoroutine(async () =>
            {
                //arange
                var assetService = new AssetService(new AssetInstall(),new AssetLoad());
                var pull = new PoolUiItem<ItemShop>(assetService);
                var styleData = new StyleDataLoad(assetService.Load);
                
                //act
                await styleData.Load();
                DataPullUiItem item = new DataPullUiItem(
                    Constant.M.Asset.Popup.ShopElementBuyPrefab,
                    Constant.M.Asset.Popup.PoolElementUi,
                    styleData.GetData().Length);
                await pull.Load(item);

                //assert
                Assert.NotNull(pull.GetItem());
            });
        }
        
        [UnityTest]
        public IEnumerator Test_Meta_UI_Load()
        {
            return UniTask.ToCoroutine(async () =>
            {
                //arange
                var assetService = new AssetService(new AssetInstall(),new AssetLoad());
                var hudObject = assetService.Load.GetAsset<GameObject>(TypeAsset.Meta_UI, Constant.M.Asset.Ui.MetaRoot);
                
                //act
                var metaRoot = assetService.Install.InstallToRoot<GameObject>(hudObject).GetComponent<MetaRoot>();

                //assert
                Assert.NotNull(metaRoot);
            });
        }
    }
}

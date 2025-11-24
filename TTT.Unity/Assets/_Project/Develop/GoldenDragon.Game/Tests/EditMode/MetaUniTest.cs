using System;
using System.Collections;
using System.Text;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.InventoryItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.ShopElementItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Style;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace _Project.Develop.GoldenDragon.Game.Tests.EditMode
{
    public class MetaUniTest
    {
        [Test]
        public void Test_Save_Load_Json_DataPlayer()
        {
            string key = "1024";
            string nick = "Dummy";
            PlayerData playerDataSave = new PlayerData(nick);
            var playerDataJson = JsonConvert.SerializeObject(playerDataSave);
            PlayerPrefs.SetString(key,playerDataJson);
            var load = PlayerPrefs.GetString(key);
            
            var playerDataLoad = JsonConvert.DeserializeObject<PlayerData>(load);
            
            Assert.True(playerDataLoad.Nick == nick);
        }

        [Test]
        public void Test_Save_Code_Base64()
        {
            string key = "1024";
            string nick = "Dummy";
            PlayerData playerDataSave = new PlayerData(nick);
            var playerDataJson = JsonConvert.SerializeObject(playerDataSave);
            byte[] bytes = Encoding.UTF8.GetBytes(playerDataJson);
            var base64String = Convert.ToBase64String(bytes);
            PlayerPrefs.SetString(key,base64String);
            var load = PlayerPrefs.GetString(key);
            byte[] decodedBytes = Convert.FromBase64String(load);
            string decodedText = Encoding.UTF8.GetString(decodedBytes);
            
            var playerDataLoad = JsonConvert.DeserializeObject<PlayerData>(decodedText);
            
            Assert.True(playerDataLoad.Nick == nick);
        }
        
        [UnityTest]
        public IEnumerator Test_Style_Data_Load()
        {
            return UniTask.ToCoroutine(async () =>
            {
                //arange
                AssetCatch assetCatch = new AssetCatch();
                AssetService assetService = new AssetService(new AssetInstall(),new AssetLoad(assetCatch),new AssetRelease(assetCatch));
                var styleData = new StyleDataLoadShop(assetService);
                
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
                AssetCatch assetCatch = new AssetCatch();
                AssetService assetService = new AssetService(new AssetInstall(),new AssetLoad(assetCatch),new AssetRelease(assetCatch));
                var pull = new PoolUiItem<ItemInventoryStyle>(assetService);
                var styleData = new StyleDataLoadShop(assetService);
                
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
                AssetCatch assetCatch = new AssetCatch();
                AssetService assetService = new AssetService(new AssetInstall(),new AssetLoad(assetCatch),new AssetRelease(assetCatch));
                var pull = new PoolUiItem<ItemShop>(assetService);
                var styleData = new StyleDataLoadShop(assetService);
                
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
                AssetCatch assetCatch = new AssetCatch();
                AssetService assetService = new AssetService(new AssetInstall(),new AssetLoad(assetCatch),new AssetRelease(assetCatch));
                var hudObject = assetService.Load.GetAsset<GameObject>(TypeAsset.Meta_Root_Ui, Constant.M.Asset.Ui.MetaRoot);
                
                //act
                var metaRoot = assetService.Install.InstallToRoot<GameObject>(hudObject).GetComponent<MetaRoot>();

                //assert
                Assert.NotNull(metaRoot);
            });
        }
    }
}

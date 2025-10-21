using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Style;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
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
        public IEnumerator Test_Load_Addressable_JsonData_Style()=>
             UniTask.ToCoroutine(async () =>
            {
                // //arange
                // AsyncOperationHandle<TextAsset> handle =  Addressables.LoadAssetAsync<TextAsset>(Constant.L.Asset.DataStyleJson);
                // TextAsset textAsset = await handle.Task;
                //
                // // //act
                // StyleData[] styleData = JsonUtility.FromJson<StyleData[]>(textAsset.text);
                //
                //
                // // //assert
                // Assert.NotNull(styleData);
                // Assert.IsTrue(styleData.Length > 0);
                //
                //
                // Addressables.Release(handle);
            });
    }
}

using System.Collections;
using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace _Project.Develop.GoldenDragon.Game.Tests.EditMode
{
    public class MatchUniTest
    {
        [Test]
        public void Random_First_Round_Character()
        {
            //arrange
            int count = 10;
            MatchMode[] modeArray = new MatchMode[count];
            RandomRound roundManager = new RandomRound();
            
            //act
            for (int i = 0; i < count; i++) modeArray[i] = roundManager.GetFirstCharacterAction();
            int countPlayer = modeArray.Count(key => key == MatchMode.PlayerAction);

            //assert
            Assert.True(modeArray.FirstOrDefault(key => key == MatchMode.BotAction) == MatchMode.BotAction &&
                        modeArray.FirstOrDefault(key => key == MatchMode.PlayerAction) == MatchMode.PlayerAction);
        }
        
        [UnityTest]
        public IEnumerator WhenCondition_ThenExpectation()
        {
            return UniTask.ToCoroutine(async () =>
            {
                //arange
                CharacterMatchData botMatchDataData = new CharacterMatchData("test",TypePlayingField.O,true);
                CharacterMatchData playerMatchDataData = new CharacterMatchData("test",TypePlayingField.X,false);
                IPlayerProgress playerProgress = new ProgressService();
                SaveLoadService saveLoadService = new SaveLoadService(playerProgress);
                RoundManager roundManager = new RoundManager();
                AssetService assetService = new AssetService(new AssetInstall(),new AssetLoad());
                ProviderUiFactory providerUiFactory = new ProviderUiFactory(new FactoryItem(),new FactoryUi(assetService));
                PopupService popupService = new PopupService(saveLoadService);
                MatchUiRoot matchUi = providerUiFactory.FactoryUi.CreateRootUi<MatchUiRoot>(TypeAsset.Match_Root_Ui,Constant.M.Asset.Ui.MatchRoot);
                //matchUi.Constructor(botMatchDataData,playerMatchDataData,popupService,assetService,providerUiFactory,roundManager);
                //await matchUi.InitializedPlayingField();
                
                //act
                foreach (var field in matchUi.PlayingField.Fields)
                {
                    Log.Default.W(field.Position.ToString());
                }

                int f = 12;

                //assert
                Assert.NotNull(f);
            });
        }
    }
}
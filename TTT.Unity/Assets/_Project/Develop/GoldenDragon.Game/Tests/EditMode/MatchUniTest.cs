using System.Linq;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using NUnit.Framework;

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
    }
}
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round
{
    public class RandomRound
    {
        public bool SetFirstMove()
        {
            
        }
    }
    
    public class RoundAction:ILoadUnit
    {
        private IAi _ai;
        private PlayerMatchData _player;
        private Bot _bot;
        
        
        public UniTask Load()
        {
            
        }
        
        public void StartRound()
        {
            BotAction botAction = _ai.MakeBestDecision(_bot);
            botAction.Field.SetElement(_bot.Type);
        }
    }
}
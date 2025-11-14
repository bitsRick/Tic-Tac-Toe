using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class UtilityAi:IAi,ILoadUnit
    {
        private PlayingField _playingField;
        private Calculation _calculation;
        private Brains _brains;
        private IEnumerable<IUtilityFunction> _utilityFunction;
        private readonly MatchUiRoot _matchUiRoot;

        public UtilityAi(MatchUiRoot matchUiRoot) => _matchUiRoot = matchUiRoot;

        public async UniTask Load()
        {
            _playingField = _matchUiRoot.PlayingField;
            
            _calculation = new Calculation(this);
            _brains = new Brains(_calculation);
            
            await _brains.Load();
            
            _utilityFunction = _brains.GetUtilityFunction();
        }

        public PlayingField GetPlayingField() => _matchUiRoot.PlayingField;

        public BotAction MakeBestDecision(CharacterMatchData botMatchDataData)
        {
            IEnumerable<ScoreAction> choisec = GetScoreBotAction(botMatchDataData);

            return choisec.FindMax(x => x.Score);
        }

        private IEnumerable<ScoreAction> GetScoreBotAction(CharacterMatchData botMatchDataData)
        {
            foreach (Field field in _playingField.Fields)
            {
                float score = CalculateScore(botMatchDataData,field);
                yield return new ScoreAction(score,field,botMatchDataData.Field);
            }
        }

        private float CalculateScore(CharacterMatchData botMatchDataData, Field field)
        {
            IEnumerable<ScoreFactor> scoreFactor = 
                (from utilityFunction in _utilityFunction
                where utilityFunction.AppliesTo(field)
                let input = utilityFunction.GetInput(botMatchDataData, field)
                let score = utilityFunction.Score(input, botMatchDataData,field)
                select new ScoreFactor(utilityFunction.Name, score));

            return scoreFactor.Select(x => x.Score).Sum();
        }
    }
}
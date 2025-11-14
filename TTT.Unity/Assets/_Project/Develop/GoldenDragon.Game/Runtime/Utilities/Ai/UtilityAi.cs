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
            _playingField = _matchUiRoot.GetPlayingField();
            
            _calculation = new Calculation(_matchUiRoot);
            _brains = new Brains(_calculation);
            
            await _brains.Load();
            
            _utilityFunction = _brains.GetUtilityFunction();
        }

        public BotAction MakeBestDecision(CharacterMatch botMatchData)
        {
            IEnumerable<ScoreAction> choisec = GetScoreBotAction(botMatchData);

            return choisec.FindMax(x => x.Score);
        }

        private IEnumerable<ScoreAction> GetScoreBotAction(CharacterMatch botMatchData)
        {
            foreach (Field field in _playingField.Fields)
            {
                float score = CalculateScore(botMatchData,field);
                yield return new ScoreAction(score,field,botMatchData.Field);
            }
        }

        private float CalculateScore(CharacterMatch botMatchData, Field field)
        {
            IEnumerable<ScoreFactor> scoreFactor = 
                (from utilityFunction in _utilityFunction
                where utilityFunction.AppliesTo(field)
                let input = utilityFunction.GetInput(botMatchData, field)
                let score = utilityFunction.Score(input, botMatchData,field)
                select new ScoreFactor(utilityFunction.Name, score));

            return scoreFactor.Select(x => x.Score).Sum();
        }
    }
}
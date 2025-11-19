using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class UtilityAi:IAi
    {
        private PlayingField _playingField;
        private IEnumerable<IUtilityFunction> _utilityFunction;
        private Calculation _calculation;
        private Brains _brains;
        private MatchUiRoot _matchUiRoot;

        public UtilityAi(Calculation calculation,Brains brains)
        {
            _brains = brains;
            _calculation = calculation;
        }

        public async UniTask Load(MatchUiRoot matchUiRoot)
        {
            _matchUiRoot = matchUiRoot;
            _playingField = _matchUiRoot.PlayingField;
            
            _calculation = new Calculation();
            await _calculation.Score.Load(this);
            
            _brains = new Brains(_calculation);
            await _brains.Load();
            
            _utilityFunction = _brains.GetUtilityFunction();
        }

        public void SetField(CharacterMatchData botMatchDataData, Field botActionField) => 
            _matchUiRoot.SetTypeInField(botMatchDataData,botActionField);

        public PlayingField GetPlayingField() => _playingField;

        public BotAction MakeBestDecision(CharacterMatchData botMatchDataData)
        {
            IEnumerable<ScoreAction> choisec = GetScoreBotAction(botMatchDataData);
            return choisec.FindMax(x => x.Score);
        }

        private IEnumerable<ScoreAction> GetScoreBotAction(CharacterMatchData botMatchDataData)
        {
            foreach (Field field in _playingField.Fields)
            {
                float? score = CalculateScore(botMatchDataData,field);

                if (!score.HasValue)
                    continue;

                yield return new ScoreAction(score.Value,field,botMatchDataData.Field);
            }
        }
        
        private float? CalculateScore(CharacterMatchData botMatchDataData, Field field)
        {
            List<ScoreFactor> scoreFactor = 
                (from utilityFunction in _utilityFunction
                where utilityFunction.AppliesTo(field)
                let input = utilityFunction.GetInput(botMatchDataData, field)
                let score = utilityFunction.Score(input, botMatchDataData,field)
                select new ScoreFactor(utilityFunction.Name, score)).ToList();

            return scoreFactor.Select(x => x.Score).SumOrNull();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Ai.CalculationParam;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using VContainer;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Ai
{
    public class UtilityAi:IAi
    {
        private readonly Calculation _calculation;
        private readonly Brains _brains;
        private PlayingField _playingField;
        private MatchUiRoot _matchUiRoot;
        private IEnumerable<IUtilityFunction> _utilityFunction;

        [Inject]
        public UtilityAi(Calculation calculation,Brains brains)
        {
            _brains = brains;
            _calculation = calculation;
            _calculation.Resolve(this);
        }

        public async UniTask Load(MatchUiRoot matchUiRoot)
        {
            _matchUiRoot = matchUiRoot;
            _playingField = _matchUiRoot.PlayingField;
            
            await _calculation.Score.Load();
            await _brains.Load();
            
            _utilityFunction = _brains.GetUtilityFunction();
        }

        public void SetField(CharacterMatchData botMatchDataData, Field botActionField) => 
            _matchUiRoot.SetTypeInField(botMatchDataData,botActionField);

        public PlayingField GetPlayingField() => _playingField;

        public BotAction MakeBestDecision(CharacterMatchData botMatchDataData)
        {
            List<ScoreAction> choisec = GetScoreBotAction(botMatchDataData);
            return choisec.FindMax(x => x.Score);
        }

        private List<ScoreAction> GetScoreBotAction(CharacterMatchData botMatchDataData)
        {
            return _playingField.Fields
                .Select(field => new{field,score = CalculateScore(botMatchDataData, field)})
                .Where(x => x.score.HasValue)
                .Select(x => new ScoreAction(x.score.Value, x.field))
                .ToList();
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
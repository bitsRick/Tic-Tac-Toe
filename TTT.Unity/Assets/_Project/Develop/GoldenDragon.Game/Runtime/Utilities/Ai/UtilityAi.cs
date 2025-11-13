using System.Collections.Generic;
using System.Linq;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class UtilityAi:IAi
    {
        private PlayingField _playingField;
        private IEnumerable<IUtilityFunction> _utilityFunction;

        public UtilityAi(PlayingField playingField, Calculation calculation,Brains brains)
        {
            _playingField = playingField;
            _utilityFunction = brains.GetUtilityFunction();
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
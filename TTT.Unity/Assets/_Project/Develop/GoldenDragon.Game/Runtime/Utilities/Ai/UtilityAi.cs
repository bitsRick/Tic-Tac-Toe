using System.Collections.Generic;
using System.Linq;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class UtilityAi:IAi
    {
        private PlayingField _playingField;
        private IEnumerable<IUtilityFunction> _utilityFunction;

        public UtilityAi(PlayingField playingField, Calculation calculation,IEnumerable<IUtilityFunction> utilityFunction)
        {
            _playingField = playingField;
            _utilityFunction = utilityFunction;

            
            _utilityFunction = new Brains(calculation).GetUtilityFunction();
        }

        public BotAction MakeBestDecision(Bot bot)
        {
            IEnumerable<ScoreAction> choisec = GetScoreBotAction(bot);

            return choisec.FindMax(x => x.Score);
        }

        private IEnumerable<ScoreAction> GetScoreBotAction(Bot bot)
        {
            foreach (Field field in _playingField.Fields)
            {
                float score = CalculateScore(bot,field);

                yield return new ScoreAction(score,field,bot.Type);
            }
        }

        private float CalculateScore(Bot bot, Field field)
        {
            IEnumerable<ScoreFactor> scoreFactor = 
                (from utilityFunction in _utilityFunction
                where utilityFunction.AppliesTo(field)
                let input = utilityFunction.GetInput(bot, field)
                let score = utilityFunction.Score(input, bot,field)
                select new ScoreFactor(utilityFunction.Name, score));

            return scoreFactor.Select(x => x.Score).Sum();
        }
    }
}
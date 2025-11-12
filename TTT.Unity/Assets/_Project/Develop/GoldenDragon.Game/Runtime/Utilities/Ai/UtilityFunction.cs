using System;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class UtilityFunction : IUtilityFunction
    {
        private readonly Func< Field, bool> _appliesTo;
        private readonly Func<TypePositionElementWin,BotMatchData, Field, float> _score;
        private readonly Func<BotMatchData, Field, TypePositionElementWin> _getInput;
        public string Name { get; set; }

        public UtilityFunction(
            Func<Field,bool> appliesTo,
            Func<BotMatchData,Field,TypePositionElementWin> getInput,
            Func<TypePositionElementWin,BotMatchData,Field,float> score,
            string name
        )
        {
            _getInput = getInput;
            _appliesTo = appliesTo;
            _score = score;
            Name = name;
        }

        public TypePositionElementWin GetInput(BotMatchData botMatchData, Field field) => _getInput(botMatchData, field);

        public bool AppliesTo(Field field) => _appliesTo( field);

        public float Score(TypePositionElementWin typeAction,BotMatchData botMatchData, Field field) => _score(typeAction,botMatchData, field);
    }
}
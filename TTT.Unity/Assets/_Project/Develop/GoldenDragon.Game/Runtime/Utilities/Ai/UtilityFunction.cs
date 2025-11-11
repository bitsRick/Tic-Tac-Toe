using System;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class UtilityFunction : IUtilityFunction
    {
        private readonly Func< Field, bool> _appliesTo;
        private readonly Func<TypePositionElementWin,Bot, Field, float> _score;
        private readonly Func<Bot, Field, TypePositionElementWin> _getInput;
        public string Name { get; set; }

        public UtilityFunction(
            Func<Field,bool> appliesTo,
            Func<Bot,Field,TypePositionElementWin> getInput,
            Func<TypePositionElementWin,Bot,Field,float> score,
            string name
        )
        {
            _getInput = getInput;
            _appliesTo = appliesTo;
            _score = score;
            Name = name;
        }

        public TypePositionElementWin GetInput(Bot bot, Field field) => _getInput(bot, field);

        public bool AppliesTo(Field field) => _appliesTo( field);

        public float Score(TypePositionElementWin typeAction,Bot bot, Field field) => _score(typeAction,bot, field);
    }
}
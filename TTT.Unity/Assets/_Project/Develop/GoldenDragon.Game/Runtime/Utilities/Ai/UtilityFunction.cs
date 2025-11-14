using System;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class UtilityFunction : IUtilityFunction
    {
        private readonly Func< Field, bool> _appliesTo;
        private readonly Func<TypePositionElementWin,CharacterMatchData, Field, float> _score;
        private readonly Func<CharacterMatchData, Field, TypePositionElementWin> _getInput;
        public string Name { get; set; }

        public UtilityFunction(
            Func<Field,bool> appliesTo,
            Func<CharacterMatchData,Field,TypePositionElementWin> getInput,
            Func<TypePositionElementWin,CharacterMatchData,Field,float> score,
            string name
        )
        {
            _getInput = getInput;
            _appliesTo = appliesTo;
            _score = score;
            Name = name;
        }

        public TypePositionElementWin GetInput(CharacterMatchData botMatchDataData, Field field) => _getInput(botMatchDataData, field);

        public bool AppliesTo(Field field) => _appliesTo( field);

        public float Score(TypePositionElementWin typeAction,CharacterMatchData botMatchDataData, Field field) => _score(typeAction,botMatchDataData, field);
    }
}
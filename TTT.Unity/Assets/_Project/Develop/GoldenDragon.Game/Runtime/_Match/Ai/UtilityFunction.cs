using System;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Ai
{
    public class UtilityFunction : IUtilityFunction
    {
        private readonly Func< Field, bool> _appliesTo;
        private readonly Func<PositionElementWin,CharacterMatchData, Field, float> _score;
        private readonly Func<CharacterMatchData, Field, PositionElementWin> _getInput;
        public string Name { get; set; }

        public UtilityFunction(
            Func<Field,bool> appliesTo,
            Func<CharacterMatchData,Field,PositionElementWin> getInput,
            Func<PositionElementWin,CharacterMatchData,Field,float> score,
            string name
        )
        {
            _getInput = getInput;
            _appliesTo = appliesTo;
            _score = score;
            Name = name;
        }

        public PositionElementWin GetInput(CharacterMatchData botMatchDataData, Field field) => _getInput(botMatchDataData, field);

        public bool AppliesTo(Field field) => _appliesTo( field);

        public float Score(PositionElementWin action,CharacterMatchData botMatchDataData, Field field) => _score(action,botMatchDataData, field);
    }
}
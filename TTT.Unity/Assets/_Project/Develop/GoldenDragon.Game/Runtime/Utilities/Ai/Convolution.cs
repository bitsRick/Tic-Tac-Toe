using System;
using System.Collections.Generic;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class Convolution : List<UtilityFunction>
    {
        public void Add(
            Func<Field, bool> appliesTo,
            Func<CharacterMatchData, Field, PositionElementWin> getInput,
            Func<PositionElementWin,CharacterMatchData, Field, float> score,
            string name
            )
        {
            Add(new UtilityFunction(appliesTo,getInput,score,name));
        }
    }
}
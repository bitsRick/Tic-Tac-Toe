using System;
using System.Collections.Generic;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class Convolution : List<UtilityFunction>
    {
        public void Add(
            Func<Field, bool> appliesTo,
            Func<BotMatchData, Field, TypePositionElementWin> getInput,
            Func<TypePositionElementWin,BotMatchData, Field, float> score,
            string name
            )
        {
            Add(new UtilityFunction(appliesTo,getInput,score,name));
        }
    }
}
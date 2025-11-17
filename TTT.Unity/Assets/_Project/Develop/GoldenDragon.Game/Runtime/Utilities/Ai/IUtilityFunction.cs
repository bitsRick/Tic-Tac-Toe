using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public interface IUtilityFunction
    {
        bool AppliesTo(Field field);
        float Score(PositionElementWin action,CharacterMatchData botMatchDataData, Field field);
        string Name { get; set; }
        PositionElementWin GetInput(CharacterMatchData botMatchDataData, Field field);
    }
}
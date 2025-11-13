using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public interface IUtilityFunction
    {
        bool AppliesTo(Field field);
        float Score(TypePositionElementWin typeAction,CharacterMatch botMatchData, Field field);
        string Name { get; set; }
        TypePositionElementWin GetInput(CharacterMatch botMatchData, Field field);
    }
}
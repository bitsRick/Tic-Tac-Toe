using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public interface IUtilityFunction
    {
        bool AppliesTo(Field field);
        float Score(TypePositionElementWin typeAction,BotMatchData botMatchData, Field field);
        string Name { get; set; }
        TypePositionElementWin GetInput(BotMatchData botMatchData, Field field);
    }
}
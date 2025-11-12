using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public interface IAi
    {
        BotAction MakeBestDecision(BotMatchData botMatchData);
    }
}
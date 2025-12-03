using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Ai
{
    public interface IAi:ILoadUnit<MatchUiRoot>
    {
        BotAction MakeBestDecision(CharacterMatchData botMatchDataData);
        void SetField(CharacterMatchData botMatchDataData, Field botActionField);
    }
}
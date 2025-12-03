using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Ai
{
    public class ScoreAction : BotAction
    {
        public float Score;

        public ScoreAction(float score, Field field)
        {
            Field = field;
            Score = score;
        }
    }
}
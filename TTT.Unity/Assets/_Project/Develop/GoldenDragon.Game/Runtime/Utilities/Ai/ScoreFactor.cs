namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class ScoreFactor
    {
        public string Name { get; }
        public float Score { get; }

        public ScoreFactor(string name, float field)
        {
            Name = name;
            Score = field;
        }
    }
}
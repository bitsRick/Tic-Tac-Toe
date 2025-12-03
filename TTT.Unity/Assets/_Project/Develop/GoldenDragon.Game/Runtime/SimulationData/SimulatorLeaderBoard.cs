using System.Collections.Generic;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData
{
    public class SimulatorLeaderBoard
    {
        private static SimulatorLeaderBoard instance;
        public static SimulatorLeaderBoard S => instance ?? new SimulatorLeaderBoard();
        
        public List<Data> D => new()
        {
            new(){Name = "OboObo Asas", Score = 909021999},
            new(){Name = "Tramp", Score = 100},
            new(){Name = "Furry", Score = 564},
            new(){Name = "Adrey", Score = 234}
        };
    }

    public class Data
    {
        public string Name;
        public int Score;
    }
}
using System.Collections.Generic;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.SimulationData
{
    public class SimulatorLeaderBoard
    {
        private static SimulatorLeaderBoard instance;
        public static SimulatorLeaderBoard S => instance ?? new SimulatorLeaderBoard();
        
        public List<Data> D => new List<Data>()
        {
            new Data(){Name = "OboObo Asas", Score = 909021999},
            new Data(){Name = "Tramp", Score = 100},
            new Data(){Name = "Furry", Score = 564},
            new Data(){Name = "Adrey", Score = 234}
        };
    }

    public class Data
    {
        public string Name;
        public int Score;
    }
}
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service
{
   public interface IPlayerProgress
    {
        PlayerData PlayerData { get; set; }
    }
    
    public class ProgressService:IPlayerProgress
    {
        public PlayerData PlayerData { get; set; }
    }
}
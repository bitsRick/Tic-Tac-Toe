using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service
{
   public interface IPlayerProfile
    {
        ProfileData profileData { get; set; }
    }
    
    public class ProfileService:IPlayerProfile
    {
        public ProfileData profileData { get; set; }
    }
}
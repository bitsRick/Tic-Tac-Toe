using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SessionData
{
    public class SessionDataMatch:ILoadUnit
    {
        private SessionMatch _sessionMatch;
        
        public UniTask Load()
        {
            Reset();
            
            return UniTask.CompletedTask;
        }

        public void SetTypeSession(SessionMatch sessionMatch) => _sessionMatch = sessionMatch;

        public SessionMatch PlayerType() => _sessionMatch;

        private void Reset() => _sessionMatch = SessionMatch.None;
    }
    
}
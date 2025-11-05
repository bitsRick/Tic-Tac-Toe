using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SessionData
{
    public class SessionDataMatch:ILoadUnit
    {
        private CurrentSessionMatch _currentSessionMatch;
        
        public UniTask Load()
        {
            Reset();
            
            return UniTask.CompletedTask;
        }

        public void SetTypeSession(TypeSessionMatch typeSessionMatch) => _currentSessionMatch.TypeSessionMatch = typeSessionMatch;

        private void Reset() => _currentSessionMatch = new CurrentSessionMatch();
    }

    public class CurrentSessionMatch
    {
        public TypeSessionMatch TypeSessionMatch;
    }
}
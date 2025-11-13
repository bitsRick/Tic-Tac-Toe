using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SessionData
{
    public class SessionDataMatch:ILoadUnit
    {
        private TypeSessionMatch _typeSessionMatch;
        
        public UniTask Load()
        {
            Reset();
            
            return UniTask.CompletedTask;
        }

        public void SetTypeSession(TypeSessionMatch typeSessionMatch) => _typeSessionMatch = typeSessionMatch;

        public TypeSessionMatch PlayerType() => _typeSessionMatch;

        private void Reset() => _typeSessionMatch = TypeSessionMatch.None;
    }
    
}
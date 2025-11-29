using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SessionData
{
    public class SessionDataMatch:ILoadUnit
    {
        private TypePlayingField _player;
        private TypePlayingField _bot;

        public TypePlayingField PlayerType => _player;
        public TypePlayingField BotType => _bot;

        public UniTask Load()
        {
            Reset();
            return UniTask.CompletedTask;
        }

        public void SetTypePlayer(TypePlayingField player)
        {
            _player = player;
            _bot = player == TypePlayingField.O ? TypePlayingField.X : TypePlayingField.O;
        }
        
        private void Reset()
        {
            _player = TypePlayingField.None;
            _bot = TypePlayingField.None;
        }
    }
    
}
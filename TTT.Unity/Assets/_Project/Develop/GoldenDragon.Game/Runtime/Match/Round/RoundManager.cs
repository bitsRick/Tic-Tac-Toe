using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round
{
    public class RoundManager:ITickable
    {
        private IAi _ai;
        private PlayerMatchData _player;
        private BotMatchData _botMatchData;
        private bool _isStart;
        private bool _isFinish;
        private bool _isTimerPause;
        private bool _isTurnTimePaused;
        private WinService _winService;

        public MatchMode Mode => MatchMode.Pause;

        public RoundManager(IAi ai, PlayerMatchData player, BotMatchData botMatchData)
        {
            _ai = ai;
            _player = player;
            _botMatchData = botMatchData;
        }

        public void Tick()
        {
            if (_isStart == false ||
                _isFinish)
                return;

            UpdateTurnTimer();
            _winService.ProcessWin();
        }

        private void UpdateTurnTimer()
        {
            if (_isTurnTimePaused)
                return;
            
            
        }

        public void Start() => _isStart = true;

        public void End() => _isFinish = true;

        public void ActionBotRound()
        {
            BotAction botAction = _ai.MakeBestDecision(_botMatchData);
            botAction.Field.TrySetElement(_botMatchData.Type);
        }
    }
}
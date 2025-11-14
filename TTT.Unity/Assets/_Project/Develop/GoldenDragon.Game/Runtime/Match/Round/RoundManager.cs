using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round
{
    public class RoundManager:ITickable
    {
        private IAi _ai;
        private CharacterMatch _player;
        private CharacterMatch _botMatchData;
        private bool _isStart;
        private bool _isFinish;
        private bool _isTimerPause;
        private bool _isTurnTimePaused;
        private WinService _winService;
        private MatchUiRoot _matchUiRoot;

        public MatchMode Mode => MatchMode.Pause;
        
        public void Initialized(IAi ai, CharacterMatch player, CharacterMatch botMatchData,MatchUiRoot matchUiRoot)
        {
            _matchUiRoot = matchUiRoot;
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
            _matchUiRoot.SetField(_botMatchData,botAction.Field);
        }
    }
}
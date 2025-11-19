using System;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.TopInformation;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UniRx;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round
{
    public class RoundData
    {
        public const int MaxWinMatch = 3;
        public int CurrenWin = 0;
        public int CountSetField = -1;

        public bool IsStart;
        public bool IsFinish;
        public bool IsTimerPause;
        public bool IsTurnTimePaused;

        public int MaxWin => MaxWinMatch;

        public void Reset()
        {
            IsStart = false;
            IsFinish = false;
            IsTimerPause = false;
            IsTurnTimePaused = false;
            CountSetField = -1;
        }
        
        public bool IsNotEndWin(CharacterMatchData playerMatchData, CharacterMatchData botMatchData) => 
            playerMatchData.WinCount < MaxWinMatch && botMatchData.WinCount < MaxWinMatch;
    }

    public class RoundManager:IDisposable
    {
        private IAi _ai;
        private CharacterMatchData _player;
        private CharacterMatchData _bot;
        private WinService _winService;
        private RandomRound _randomRound;
        private RoundData _roundData = new RoundData();
        private bool _isPlayerAction = false;
        private MatchMode _mode = MatchMode.Pause;

        public MatchMode Mode => _mode;
        public bool IsPlayerAction =>_isPlayerAction;
        public RoundData RoundData => _roundData;

        public Subject<MatchWin> OnWin = new Subject<MatchWin>();
        public Subject<Unit> OnNextTurn = new Subject<Unit>();
        public Subject<bool> OnButtonInteractive  = new Subject<bool>();

        public void Initialized(IAi ai, CharacterMatchData player, CharacterMatchData botMatchDataData,WinService winService,RandomRound randomRound)
        {
            _randomRound = randomRound;
            _ai = ai;
            _player = player;
            _bot = botMatchDataData;
            _winService = winService;
        }
        
        public void InitializedFirstActionRound()
        {
            _mode = _randomRound.GetFirstCharacterAction();
            Log.Match.D($"[StartRoundMode]:{Mode.ToString()}");
        }

        public void Update()
        {
            if (_roundData.IsStart == false ||
                _roundData.IsFinish ||
                _isPlayerAction)
                return;
            
            if (_winService.TryGetMatchWin(_roundData,out MatchWin characterWin))
                Win(characterWin);
            else
                UpdateTurnTimer();
        }

        private void Win(MatchWin characterWin)
        {
            Log.Match.D($"[Match]:[{characterWin.ToString()}]");
            
            switch (characterWin)
            {
                case MatchWin.Player:
                    _player.AddWin();
                    break;
                
                case MatchWin.Bot:
                    _bot.AddWin();
                    break;
            }
            
            End();
            OnWin.OnNext(characterWin);
        }

        public void Start() => _roundData.IsStart = true;

        public void End() => _roundData.IsFinish = true;

        public void NextTurn()
        {
            switch (Mode)
            {
                case MatchMode.PlayerAction:
                    TurnBot();
                    break;
                
                case MatchMode.BotAction:
                    TurnPlayer();
                    break;
            }

            _roundData.CountSetField++;
            Log.Match.D($"[CurrentRoundMode]:{Mode.ToString()}");
        }

        private void TurnPlayer()
        {
            _mode = MatchMode.PlayerAction;
        }

        private void TurnBot()
        {
            _isPlayerAction = false;
            _mode = MatchMode.BotAction;
        }

        private void ActionBotRound()
        {
            OnButtonInteractive.OnNext(false);
            BotAction botAction = _ai.MakeBestDecision(_bot);
            _ai.SetField(_bot,botAction.Field);
        }

        private void ActionPlayerRound()
        {
            _isPlayerAction = true;
            OnButtonInteractive.OnNext(true);
            Log.Match.D($"[Match]:Flag player action {_isPlayerAction}");
        }

        private void UpdateTurnTimer()
        {
            switch (Mode)
            {
                case MatchMode.PlayerAction:
                    ActionPlayerRound();
                    break;
                
                case MatchMode.BotAction:
                    ActionBotRound();
                    break;
            }
        }

        public void Dispose()
        {
            OnNextTurn.Dispose();
        }

        public void Reset()
        {
            InitializedFirstActionRound();
            _roundData.Reset();
        }
    }
}
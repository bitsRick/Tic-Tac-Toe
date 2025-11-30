using System;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Ai;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UniRx;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round
{
    public class RoundData
    {
        public int CountSetField = -1;
        public bool IsStart;
        public bool IsFinish;
        
        public void Reset()
        {
            IsStart = false;
            IsFinish = false;
            CountSetField = -1;
        }
        
        public bool IsNotEndWin(CharacterMatchData playerMatchData, CharacterMatchData botMatchData) => 
            playerMatchData.WinCount < Constant.M.MaxWinMatch && botMatchData.WinCount < Constant.M.MaxWinMatch;
    }

    public class RoundManager
    {
        private IAi _ai;
        private CharacterMatchData _player;
        private CharacterMatchData _bot;
        private WinService _winService;
        private RoundRandom _roundRandom;
        private readonly RoundData _roundData = new RoundData();
        private bool _isPlayerAction;
        private MatchMode _mode = MatchMode.Pause;

        public MatchMode Mode => _mode;
        public RoundData RoundData => _roundData;

        public Subject<MatchWin> OnWin = new Subject<MatchWin>();
        public Subject<bool> OnButtonInteractive  = new Subject<bool>();

        public UniTask Initialized(IAi ai, CharacterMatchData player, CharacterMatchData botMatchDataData,WinService winService,RoundRandom roundRandom)
        {
            _roundRandom = roundRandom;
            _ai = ai;
            _player = player;
            _bot = botMatchDataData;
            _winService = winService;
            
            return UniTask.CompletedTask;
        }

        public void InitializedEvent(MatchUiRoot matchUiRoot,ModulePlayingField modulePlayingField)
        {
            OnWin.Subscribe(modulePlayingField.SetWinMatch).AddTo(matchUiRoot);
            OnButtonInteractive.Subscribe(modulePlayingField.SetInteractiveFieldButton).AddTo(matchUiRoot);
        }
        
        public void InitializedFirstActionRound()
        {
            _mode = _roundRandom.GetFirstCharacterAction();
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
        
        public void Reset()
        {
            InitializedFirstActionRound();
            _roundData.Reset();
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

        private void TurnPlayer()
        {
            _mode = MatchMode.PlayerAction;
        }

        private void TurnBot()
        {
            _isPlayerAction = false;
            _mode = MatchMode.BotAction;
        }

        private async void ActionBotRound()
        {
            OnButtonInteractive.OnNext(false);
            BotAction botAction = _ai.MakeBestDecision(_bot);

            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
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
    }
}
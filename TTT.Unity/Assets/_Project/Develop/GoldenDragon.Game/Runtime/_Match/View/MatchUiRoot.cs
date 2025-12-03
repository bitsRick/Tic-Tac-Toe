using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.TopInformation;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View
{
    public class MatchUiRoot : MonoBehaviour, IUiRoot, ILoadUnit
    {
        [Header("Playing field container")] 
        [SerializeField]private PlayingField _playingField;
        
        [Header("Settings")]
        [SerializeField] private Button _setting;
        
        [Header("Visual data of players")] 
        [SerializeField]private TopProgressViewWinUi _playerVisualDataLeft;
        [SerializeField]private TopProgressViewWinUi _botVisualDataRight;
        
        [Header("Window container")] 
        [SerializeField]private GameObject _parent;
        
        [Header("Dark screen for windows")] 
        [SerializeField]private BackPopupBackground _popupBackground;

        private CharacterMatchData _botMatchDataData;
        private CharacterMatchData _playerMatchData;
        private PopupService _popupService;
        private ModuleMatchPlayingField _moduleMatchPlayingField;
        private ModuleMatchView _moduleMatchView;
        private MatchFlow _matchFlow;

        public PlayingField PlayingField => _playingField;

        public void Constructor(PopupService popupService,
            ModuleMatchView moduleMatchView,
            ModuleMatchPlayingField moduleMatchPlayingField,
            CharacterMatchData botMatchDataData,
            CharacterMatchData playerMatchDataData, MatchFlow matchFlow)
        {
            _matchFlow = matchFlow;
            _moduleMatchView = moduleMatchView;
            _moduleMatchPlayingField = moduleMatchPlayingField;
            _popupService = popupService;
            _playerMatchData = playerMatchDataData;
            _botMatchDataData = botMatchDataData;
        }

        public async UniTask Initialized()
        {
            await _moduleMatchView.Resolve(_popupBackground,_playerVisualDataLeft,_botVisualDataRight,
                _botMatchDataData,_playerMatchData,_parent,_popupService,this);
            
            await _moduleMatchPlayingField.Resolve(_playingField,_playerMatchData,_botMatchDataData,this);
            await UniTask.CompletedTask;
        }

        public async UniTask Load()
        {
            await InitializedPopup();
            await InitializedEvent();
            
            await UniTask.CompletedTask;
        }

        public UniTask InitializedEvent()
        {
            _setting.onClick.AsObservable().Subscribe((_) => { _moduleMatchView.OpenSetting(); }).AddTo(this);
            _popupBackground.OnEvenPointClickBackground.Subscribe((_) => _popupService.Close()).AddTo(this);
            _moduleMatchView.InitializedEvent();
            
            return UniTask.CompletedTask;
        }

        public async UniTask InitializedPopup()
        {
            await _moduleMatchView.Load();
            await UniTask.CompletedTask;
        }

        public UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public void ToMeta()
        {
            _matchFlow.ToMeta();
        }

        public void NextMatch()
        {
            _matchFlow.NextMatch();
        }

        public void SetViewWin(MatchWin winner)
        {
            _moduleMatchView.SetViewWin(winner);
        }

        public void OpenWinLose(MatchWin winner)
        {
            _moduleMatchView.OpenWinLose(winner);
        }

        public void OpenCharacterStartMatchPopup()
        {
            _moduleMatchView.OpenCharacterStartMatchPopup();
        }

        public void SetTypeInField(CharacterMatchData botMatchDataData, Field botActionField)
        {
            _moduleMatchPlayingField.SetTypeInFieldTurn(botMatchDataData,botActionField);
        }

        public void SetActiveViewColor(CharacterMatchData characterMatchData)
        {
            switch (characterMatchData.IsBot)
            {
                case true:
                    _moduleMatchView.OnBotAction.OnNext(true);
                    break;
                
                case false:
                    _moduleMatchView.OnPlayerAction.OnNext(true);
                    break;
            }
        }
    }
}
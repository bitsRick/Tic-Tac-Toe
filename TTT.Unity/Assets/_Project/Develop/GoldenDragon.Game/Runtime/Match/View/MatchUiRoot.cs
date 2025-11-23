using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round;
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
        [Header("Контейнер игрового поля")] 
        [SerializeField]private PlayingField _playingField;
        
        [Header("Настройки")]
        [SerializeField] private Button _setting;
        
        [Header("Визуальнаые данные игроков")] 
        [SerializeField]private TopProgressViewWinUi _playerVisualDataLeft;
        [SerializeField]private TopProgressViewWinUi _botVisualDataRight;
        
        [Header("Контейнер для окон")] 
        [SerializeField]private GameObject _parent;
        
        [Header("Темный экран для окон")] 
        [SerializeField]private BackPopupBackground _popupBackground;

        private CharacterMatchData _botMatchDataData;
        private CharacterMatchData _playerMatchData;
        private PopupService _popupService;
        private RoundManager _roundManager;
        private ModulePlayingField _modulePlayingField;
        private ModuleView _moduleView;

        public PlayingField PlayingField => _playingField;

        public void Constructor(
            PopupService popupService,
            RoundManager roundManager,
            ModuleView moduleView, 
            ModulePlayingField modulePlayingField,
            CharacterMatchData botMatchDataData,
            CharacterMatchData playerMatchDataData)
        {
            _moduleView = moduleView;
            _modulePlayingField = modulePlayingField;
            _popupService = popupService;
            _roundManager = roundManager;
            _playerMatchData = playerMatchDataData;
            _botMatchDataData = botMatchDataData;
        }

        public async UniTask Initialized()
        {
            await _moduleView.Initialized(_popupBackground,_playerVisualDataLeft,_botVisualDataRight,
                _botMatchDataData,_playerMatchData,_parent,_popupService);
            
            await _modulePlayingField.Initialized(_playingField,_playerMatchData,_botMatchDataData,this);
            
            await UniTask.CompletedTask;
        }
        
        public async UniTask Load()
        {
            await InitializedPopup();
            await InitializedEvent();
            
            await UniTask.CompletedTask;
        }

        public async UniTask InitializedPopup()
        {
            await _moduleView.Load();
            await UniTask.CompletedTask;
        }

        public UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public UniTask InitializedEvent()
        {
            _setting.onClick.AsObservable().Subscribe((_) => { _moduleView.OpenSetting(); }).AddTo(this);
            _popupBackground.OnEvenPointClickBackground.Subscribe((_) => _popupService.Close()).AddTo(this);
            _moduleView.InitializedEvent(this);
            
            return UniTask.CompletedTask;
        }

        public void SetViewWin(MatchWin winner)
        {
            _moduleView.SetViewWin(winner);
        }

        public void OpenWinLose(MatchWin winner)
        {
            _moduleView.OpenWinLose(winner);
        }

        public void OpenCharacterStartMatchPopup()
        {
            _moduleView.OpenCharacterStartMatchPopup();
        }

        public void SetTypeInField(CharacterMatchData botMatchDataData, Field botActionField)
        {
            _modulePlayingField.SetTypeInFieldTurn(botMatchDataData,botActionField);
        }

        public void SetActiveViewColor(CharacterMatchData characterMatchData)
        {
            switch (characterMatchData.IsBot)
            {
                case true:
                    _moduleView.OnBotAction.OnNext(true);
                    break;
                
                case false:
                    _moduleView.OnPlayerAction.OnNext(true);
                    break;
            }
        }
    }
}
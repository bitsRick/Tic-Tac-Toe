using System;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.TopInformation;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
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
        [SerializeField]private GameObject _parrent;
        
        [Header("Темный экран для окон")] 
        [SerializeField]private BackPopupBackground _popupBackground;

        private CharacterMatchData _botMatchDataData;
        private CharacterMatchData _playerMatchData;
        private PopupService _popupService;
        private AssetService _assetService;
        private ProviderUiFactory _providerUiFactory;
        private RoundManager _roundManager;
        
        public PlayingField PlayingField => _playingField;

        public void Constructor(CharacterMatchData botMatchDataData, CharacterMatchData playerMatchDataData,
            PopupService popupService, AssetService assetService,
            ProviderUiFactory providerUiFactory,RoundManager roundManager)
        {
            _providerUiFactory = providerUiFactory;
            _assetService = assetService;
            _popupService = popupService;
            _playerMatchData = playerMatchDataData;
            _botMatchDataData = botMatchDataData;
            _roundManager = roundManager;
        }
        
        public async UniTask Load()
        {
            InitDataView(_playerVisualDataLeft, _playerMatchData);
            InitDataView(_botVisualDataRight, _botMatchDataData);

            await InitializedPlayingField();
            await InitializedPopup();
            await InitializedEvent();
            
            await UniTask.CompletedTask;
        }

        public async UniTask InitializedPopup()
        {
            GameObject settingObject =
                await _providerUiFactory.FactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.Setting);
            SettingPopup settingPopup = _assetService.Install.InstallToUiPopup<SettingPopup>(settingObject, _parrent);

            GameObject winLoseObject =
                await _providerUiFactory.FactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.WinLose);
            WinLosePopup winLosePopup = _assetService.Install.InstallToUiPopup<WinLosePopup>(winLoseObject, _parrent);
            
            GameObject characterViewObject =
                await _providerUiFactory.FactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.StartMatchViewAction);
            CharacterStartMatchPopup characterStartMatchPopup = _assetService.Install.InstallToUiPopup<CharacterStartMatchPopup>(characterViewObject, _parrent);
            
            _popupService.AddPopupInList(TypePopup.Setting, settingPopup);
            _popupService.AddPopupInList(TypePopup.WinLose, winLosePopup);
            _popupService.AddPopupInList(TypePopup.CharacterStartMatch, characterStartMatchPopup);

            await UniTask.CompletedTask;
        }

        public UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public UniTask InitializedEvent()
        {
            _setting.onClick.AsObservable().Subscribe((_) => { OpenSetting(); }).AddTo(this);
            _popupBackground.OnEvenPointClickBackground.Subscribe((_) => _popupService.Close()).AddTo(this);
            _popupBackground.OnEvenPointClickBackground.Subscribe((_) => _popupService.Close()).Dispose();

            _roundManager.OnWin.Subscribe((SetWin)).AddTo(this);
            _roundManager.OnButtonInteractive.Subscribe( SetInteractiveFieldButton).AddTo(this);
            
            return UniTask.CompletedTask;
        }

        private void SetInteractiveFieldButton(bool isFlag)
        {
            foreach (Field field in _playingField.Fields) field.Btn.interactable = isFlag;
        }

        private void SetWin(MatchWin winner)
        {
            ResetFields();

            if (_roundManager.RoundData.IsNotEndWin(_playerMatchData,_botMatchDataData))
            {
                _roundManager.Reset();
                SetViewWin(winner);
                _roundManager.Start();
            }
            else
            {
                OpenWinLose(winner);
            }
        }

        private void SetViewWin(MatchWin winner)
        {
            switch (winner)
            {
                case MatchWin.Player:
                    UpdateProgressWinUi(_playerVisualDataLeft);
                    break;
                
                case MatchWin.Bot:
                    UpdateProgressWinUi(_botVisualDataRight);
                    break;
            }
        }

        private void UpdateProgressWinUi(TopProgressViewWinUi data)
        {
           var winImageUi = new[] { data.WinOne, data.WinTwo, data.WinThree }
                .FirstOrDefault(win => win.IsNotWin);

           if (winImageUi == null)
               return;
            
           winImageUi.Default.gameObject.SetActive(false);
           winImageUi.Win.gameObject.SetActive(true);
           winImageUi.IsNotWin = false;
        }

        private void ResetFields()
        {
            foreach (Field field in _playingField.Fields)
            {
                field.CurrentPlayingField = TypePlayingField.None;
                field.O.gameObject.SetActive(false);
                field.X.gameObject.SetActive(false);
                field.Btn.targetGraphic = field.Empty;
            }
        }

        public void OnMouseEnterField(Field fieldData)
        {
            SetViewField(fieldData, true);
        }

        public void OnMouseExitField(Field fieldData)
        {
            SetViewField(fieldData, false);
        }

        private void OpenSetting()
        {
            AudioPlayer.Click();
            SettingPopup popup;

            if (_popupService.TryOpenPopup(TypePopup.Setting, out SettingPopup settingPopup))
                popup = settingPopup;
            else
                return;

            popup.MusicSlider.value = AudioPlayer.GetSliderValue(TypeValueChange.Music);
            popup.SoundSlider.value = AudioPlayer.GetSliderValue(TypeValueChange.Sound);

            _popupBackground.Show();
            popup.Show();
        }

        private void OpenWinLose(MatchWin matchWin)
        {
            AudioPlayer.Click();
            WinLosePopup popup;
            
            if (_popupService.TryOpenPopup(TypePopup.WinLose, out WinLosePopup winLosePopup))
                popup = winLosePopup;
            else
                return;

            popup.Initialized();

            Log.Match.D($"[Match]:Winner {matchWin.ToString()}");
            
            switch (matchWin)
            {
                case MatchWin.Player:
                    popup.Win.SetActive(true);
                    break;
                case MatchWin.Bot:
                    popup.Lose.SetActive(true);
                    break;
            }

            _popupBackground.Show();
            popup.Show();
        }

        public void OpenCharacterStartMatchPopup()
        {
            AudioPlayer.Click();
            CharacterStartMatchPopup popup;
            
            if (_popupService.TryOpenPopup(TypePopup.CharacterStartMatch, out CharacterStartMatchPopup characterStartMatch))
                popup = characterStartMatch;
            else
                return;

            switch (_roundManager.Mode)
            {
                case MatchMode.PlayerAction:
                    popup.Name.text = _playerMatchData.Name;
                    break;
                
                case MatchMode.BotAction:
                    popup.Name.text = _botMatchDataData.Name;
                    break;
            }

            int countEvent = 1;
            
            _popupBackground.OnEvenPointClickBackground
                .Take(countEvent)
                .Subscribe((_) =>
                {
                    _roundManager.Start();
                });

            _popupBackground.Show();
            popup.Show();
        }

        private void InitDataView(TopProgressViewWinUi viewData, CharacterMatchData matchDataData)
        {
            viewData.Name.text = matchDataData.Name;
        }

        public void SetTypeInField(CharacterMatchData characterMatchData, Field field)
        {
            if (TrySetField(characterMatchData, field)) 
                _roundManager.NextTurn();
        }

        private async UniTask InitializedPlayingField()
        {
            PositionElementToField[] enumValues = Enum.GetValues(typeof(PositionElementToField))
                .Cast<PositionElementToField>()
                .ToArray();

            for (var i = 0; i < _playingField.Fields.GetLength(0); i++)
            {
                PositionElementToField type = enumValues[i];

                Field field = _playingField.Fields[i];
                field.X.gameObject.SetActive(false);
                field.O.gameObject.SetActive(false);

                field.Initialized(type, this);
                field.Btn.onClick.AsObservable().Subscribe((_) =>
                    {
                        SetTypeInField(_playerMatchData, field);
                    })
                    .AddTo(this);

                await Task.CompletedTask;
            }
        }

        private void SetViewField(Field data, bool isView)
        {
            if (data.CurrentPlayingField != TypePlayingField.None)
                return;

            Image typeFieldImage = _playerMatchData.Field == TypePlayingField.X ? data.X : data.O;
            typeFieldImage.gameObject.SetActive(isView);

            data.Btn.targetGraphic = isView ? typeFieldImage : data.Empty;
        }

        private bool TrySetField(CharacterMatchData characterAction, Field field)
        {
            if (field.CurrentPlayingField != TypePlayingField.None)
                return false;

            Image typeFieldImage = characterAction.Field == TypePlayingField.X ? field.X : field.O;
            typeFieldImage.gameObject.SetActive(true);

            field.CurrentPlayingField =
                characterAction.Field == TypePlayingField.X ? TypePlayingField.X : TypePlayingField.O;
            field.Btn.targetGraphic = null;

            return true;
        }
    }
}
using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.TopInformation;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.TopInformation.Win;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using TMPro;
using UniRx;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View
{
    public class ModuleView:IDisposableLoadUnit
    {
        private readonly ProviderUiFactory _providerUiFactory;
        private readonly AssetService _assetService;
        private readonly SaveLoadService _saveLoadService;
        private readonly RoundManager _roundManager;
        private readonly IPlayerProfile _playerProfile;
        private GameObject _parent;
        private PopupService _popupService;
        private BackPopupBackground _popupBackground;
        private TopProgressViewWinUi _playerVisualDataLeft;
        private CharacterMatchData _botMatchDataData;
        private CharacterMatchData _playerMatchData;
        private TopProgressViewWinUi _botVisualDataRight;
        private WinImageUi[] _winImageUiPlayer;
        private WinImageUi[] _winImageUiBot;
        private MatchUiRoot _matchUiRoot;

        public Subject<bool> OnPlayerAction = new Subject<bool>();
        public Subject<bool> OnBotAction = new Subject<bool>();

        public ModuleView(
            ProviderUiFactory providerUiFactory, 
            AssetService assetService, 
            RoundManager roundManager,
            SaveLoadService saveLoadService,IPlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
            _saveLoadService = saveLoadService;
            _providerUiFactory = providerUiFactory;
            _assetService = assetService;
            _roundManager = roundManager;
        }

        public UniTask Resolve(
            BackPopupBackground backPopupBackground,
            TopProgressViewWinUi playerViewData,
            TopProgressViewWinUi botViewData,
            CharacterMatchData botData,
            CharacterMatchData playerData,GameObject parent,PopupService popupService,MatchUiRoot matchUiRoot)
        {
            _matchUiRoot = matchUiRoot;
            _popupBackground = backPopupBackground;
            _playerVisualDataLeft = playerViewData;
            _botVisualDataRight = botViewData;
            _playerMatchData = playerData;
            _botMatchDataData = botData;
            _parent = parent;
            _popupService = popupService;
            return UniTask.CompletedTask;
        }

        public async UniTask Load()
        {
            _popupService.AddPopupInList<SettingPopup>(TypePopup.Setting, RuntimeConstants.Popup.Setting,_parent);
            _popupService.AddPopupInList<WinLosePopup>(TypePopup.WinLose, RuntimeConstants.Popup.WinLose,_parent);
            _popupService.AddPopupInList<CharacterStartMatchPopup>(TypePopup.CharacterStartMatch, RuntimeConstants.Popup.StartMatchViewAction,_parent);

            InitDataView(_playerVisualDataLeft,_playerMatchData);
            InitDataView(_botVisualDataRight,_botMatchDataData);
            
            _winImageUiPlayer = new[] { _playerVisualDataLeft.WinOne, _playerVisualDataLeft.WinTwo, _playerVisualDataLeft.WinThree };
            _winImageUiBot = new[] { _botVisualDataRight.WinOne, _botVisualDataRight.WinTwo, _botVisualDataRight.WinThree };
            
            await UniTask.CompletedTask;
        }

        public void InitializedEvent()
        {
            OnPlayerAction.Subscribe((isFlag) =>
            {
                SetColorText(false, _botVisualDataRight);
                SetColorText(isFlag, _playerVisualDataLeft);
            }).AddTo(_matchUiRoot);
            
            OnBotAction.Subscribe((isFlag) =>
            {
                SetColorText(false, _playerVisualDataLeft);
                SetColorText(isFlag, _botVisualDataRight);
            }).AddTo(_matchUiRoot);

            if (_popupService.TryGetPopup(TypePopup.WinLose,out WinLosePopup winLosePopup))
            {
                winLosePopup.ButtonMenu.onClick.AsObservable().Subscribe(_=>{_matchUiRoot.ToMeta();}).AddTo(winLosePopup);
                winLosePopup.ButtonNextMatch.onClick.AsObservable().Subscribe(_=>{_matchUiRoot.NextMatch();}).AddTo(winLosePopup);
            }

            if (_popupService.TryGetPopup(TypePopup.Setting, out SettingPopup setting))
            {
                setting.ToMeta.onClick.AsObservable().Subscribe((_)=> _matchUiRoot.ToMeta()).AddTo(_matchUiRoot);
            }
        }

        public void Dispose()
        {
            _assetService.Release.ReleaseAsset<GameObject>(TypeAsset.Popup, RuntimeConstants.Popup.WinLose);
            _assetService.Release.ReleaseAsset<GameObject>(TypeAsset.Popup, RuntimeConstants.Popup.StartMatchViewAction);
            _winImageUiPlayer = null;
            _winImageUiBot = null;
        }
        

        public void Reset()
        {
            ResetTopViewDataWin(_winImageUiPlayer);
            ResetTopViewDataWin(_winImageUiBot);
        }

        private void ResetTopViewDataWin(WinImageUi[] winImageUiPlayerArray)
        {
            foreach (var winImageUi in winImageUiPlayerArray)
            {
                winImageUi.Win.gameObject.SetActive(false);
                winImageUi.Default.gameObject.SetActive(true);
                winImageUi.IsNotWin = true;
            }
        }

        private void SetColorText(bool isFlag, TopProgressViewWinUi topProgressViewWinUi)
        {
            TopProgressViewWinUi viewDataTop = topProgressViewWinUi;
            viewDataTop.Name.color = isFlag == false ? viewDataTop.NoActive : viewDataTop.Active;
        }

        public void OpenWinLose(MatchWin matchWin)
        {
            AudioPlayer.S.Click();
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
                    SetWinPlayer(popup);
                    break;
                
                case MatchWin.Bot:
                    popup.Lose.SetActive(true);
                    break;
            }

            _popupService.SetNoClose(true);
            _popupBackground.Show();
            popup.Show();
        }

        public void OpenSetting()
        {
            AudioPlayer.S.Click();
            SettingPopup popup;

            if (_popupService.TryOpenPopup(TypePopup.Setting, out SettingPopup settingPopup))
                popup = settingPopup;
            else
                return;

            popup.Initialized(StateFlow.Match);
            
            popup.MusicSlider.value = AudioPlayer.S.GetSliderValue(TypeValueChange.Music);
            popup.SoundSlider.value = AudioPlayer.S.GetSliderValue(TypeValueChange.Sound);

            _popupBackground.Show();
            popup.Show();
        }

        private void InitDataView(TopProgressViewWinUi viewData, CharacterMatchData matchDataData)
        {
            viewData.Name.text = matchDataData.Name;
        }

        public void OpenCharacterStartMatchPopup()
        {
            CharacterStartMatchPopup popup;
            
            if (_popupService.TryOpenPopup(TypePopup.CharacterStartMatch, out CharacterStartMatchPopup characterStartMatch))
                popup = characterStartMatch;
            else
                return;

            popup.Initialized();
            
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
                    AudioPlayer.S.Click();
                    _roundManager.Start();
                });

            _popupBackground.Show();
            popup.Show();
        }

        public void SetViewWin(MatchWin winner)
        {
            switch (winner)
            {
                case MatchWin.Player:
                    UpdateProgressWinUi(_winImageUiPlayer);
                    break;
                
                case MatchWin.Bot:
                    UpdateProgressWinUi(_winImageUiBot);
                    break;
            }
        }

        private void UpdateProgressWinUi( WinImageUi[] winImageUiPlayer)
        {
            var winImageUi = winImageUiPlayer.FirstOrDefault(win => win.IsNotWin);

            if (winImageUi == null)
                return;
            
            winImageUi.Default.gameObject.SetActive(false);
            winImageUi.Win.gameObject.SetActive(true);
            winImageUi.IsNotWin = false;
        }

        private void SetWinPlayer(WinLosePopup popup)
        {
            int softValue = _playerMatchData.WinCount * RuntimeConstants.Match.SoftValueWin;

            TextMeshProUGUI valueWin = _playerMatchData.Field == TypePlayingField.X ? popup.X : popup.O;
            valueWin.text = softValue.ToString();

            switch (_playerMatchData.Field)
            {
                case TypePlayingField.X:
                    _playerProfile.profileData.SoftValueX = softValue;
                    break;
                
                case TypePlayingField.O:
                    _playerProfile.profileData.SoftValueO = softValue;
                    break;
            }

            _saveLoadService.SaveProgress(true);
            
            popup.Win.SetActive(true);
        }
    }
}
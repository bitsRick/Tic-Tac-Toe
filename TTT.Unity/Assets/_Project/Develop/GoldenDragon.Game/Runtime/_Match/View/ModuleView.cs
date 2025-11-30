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
    public class ModuleView:ILoadUnit
    {
        private ProviderUiFactory _providerUiFactory;
        private AssetService _assetService;
        private GameObject _parent;
        private PopupService _popupService;
        private RoundManager _roundManager;
        private BackPopupBackground _popupBackground;
        private TopProgressViewWinUi _playerVisualDataLeft;
        private CharacterMatchData _botMatchDataData;
        private CharacterMatchData _playerMatchData;
        private TopProgressViewWinUi _botVisualDataRight;
        
        public Subject<bool> OnPlayerAction = new Subject<bool>();
        public Subject<bool> OnBotAction = new Subject<bool>();
        private SaveLoadService _saveLoadService;
        private IPlayerProgress _playerProgress;
        private WinImageUi[] _winImageUiPlayer;
        private WinImageUi[] _winImageUiBot;

        public ModuleView(
            ProviderUiFactory providerUiFactory, 
            AssetService assetService, 
            RoundManager roundManager,
            SaveLoadService saveLoadService,IPlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
            _saveLoadService = saveLoadService;
            _providerUiFactory = providerUiFactory;
            _assetService = assetService;
            _roundManager = roundManager;
        }

        public UniTask Initialized(
            BackPopupBackground backPopupBackground,
            TopProgressViewWinUi playerViewData,
            TopProgressViewWinUi botViewData,
            CharacterMatchData botData,
            CharacterMatchData playerData,GameObject parent,PopupService popupService)
        {
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
            GameObject settingObject = await _providerUiFactory.FactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.Setting);
            SettingPopup settingPopup = _assetService.Install.InstallToUiPopup<SettingPopup>(settingObject, _parent);
            settingPopup.Initialized(StateFlow.Match);
            
            GameObject winLoseObject = await _providerUiFactory.FactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.WinLose);
            WinLosePopup winLosePopup = _assetService.Install.InstallToUiPopup<WinLosePopup>(winLoseObject, _parent);
            
            GameObject characterViewObject = await _providerUiFactory.FactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.StartMatchViewAction);
            CharacterStartMatchPopup characterStartMatchPopup = _assetService.Install.InstallToUiPopup<CharacterStartMatchPopup>(characterViewObject, _parent);
            
            _popupService.AddPopupInList(TypePopup.Setting, settingPopup);
            _popupService.AddPopupInList(TypePopup.WinLose, winLosePopup);
            _popupService.AddPopupInList(TypePopup.CharacterStartMatch, characterStartMatchPopup);

            InitDataView(_playerVisualDataLeft,_playerMatchData);
            InitDataView(_botVisualDataRight,_botMatchDataData);
            
            _winImageUiPlayer = new[] { _playerVisualDataLeft.WinOne, _playerVisualDataLeft.WinTwo, _playerVisualDataLeft.WinThree };
            _winImageUiBot = new[] { _botVisualDataRight.WinOne, _botVisualDataRight.WinTwo, _botVisualDataRight.WinThree };
            
            await UniTask.CompletedTask;
        }

        public void InitializedEvent(MatchUiRoot matchUiRoot)
        {
            OnPlayerAction.Subscribe((isFlag) =>
            {
                SetColorText(false, _botVisualDataRight);
                SetColorText(isFlag, _playerVisualDataLeft);
            }).AddTo(matchUiRoot);
            
            OnBotAction.Subscribe((isFlag) =>
            {
                SetColorText(false, _playerVisualDataLeft);
                SetColorText(isFlag, _botVisualDataRight);
            }).AddTo(matchUiRoot);

            if (_popupService.TryGetPopup(TypePopup.WinLose,out WinLosePopup winLosePopup))
            {
                winLosePopup.ButtonMenu.onClick.AsObservable().Subscribe(_=>{matchUiRoot.ToMeta();}).AddTo(winLosePopup);
                winLosePopup.ButtonNextMatch.onClick.AsObservable().Subscribe(_=>{matchUiRoot.NextMatch();}).AddTo(winLosePopup);
            }

            if (_popupService.TryGetPopup(TypePopup.Setting, out SettingPopup setting))
            {
                setting.ToMeta.onClick.AsObservable().Subscribe((_)=> matchUiRoot.ToMeta()).AddTo(matchUiRoot);
            }
        }

        public async UniTask Release()
        {
            await _assetService.Release.ReleaseAssetAsync<GameObject>(TypeAsset.Popup, Constant.M.Asset.Popup.WinLose);
            await _assetService.Release.ReleaseAssetAsync<GameObject>(TypeAsset.Popup, Constant.M.Asset.Popup.StartMatchViewAction);
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
                    AudioPlayer.Click();
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
            int softValue = _playerMatchData.WinCount * Constant.M.SoftValueWin;

            TextMeshProUGUI valueWin = _playerMatchData.Field == TypePlayingField.X ? popup.X : popup.O;
            valueWin.text = softValue.ToString();

            switch (_playerMatchData.Field)
            {
                case TypePlayingField.X:
                    _playerProgress.PlayerData.SoftValueX = softValue;
                    break;
                
                case TypePlayingField.O:
                    _playerProgress.PlayerData.SoftValueO = softValue;
                    break;
            }

            _saveLoadService.SaveProgress(true);
            
            popup.Win.SetActive(true);
        }
    }
}
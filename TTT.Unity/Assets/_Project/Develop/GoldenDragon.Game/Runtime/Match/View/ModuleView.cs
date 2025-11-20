using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.TopInformation;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
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

        public ModuleView(
            ProviderUiFactory providerUiFactory, 
            AssetService assetService, 
            PopupService popupService,
            RoundManager roundManager)
        {
            _providerUiFactory = providerUiFactory;
            _assetService = assetService;
            _popupService = popupService;
            _roundManager = roundManager;
        }

        public UniTask Initialized(
            BackPopupBackground backPopupBackground,
            TopProgressViewWinUi playerViewData,
            TopProgressViewWinUi botViewData,
            CharacterMatchData botData,
            CharacterMatchData playerData,GameObject parent)
        {
            _popupBackground = backPopupBackground;
            _playerVisualDataLeft = playerViewData;
            _botVisualDataRight = botViewData;
            _playerMatchData = playerData;
            _botMatchDataData = botData;
            _parent = parent;
            return UniTask.CompletedTask;
        }
        
        public async UniTask Load()
        {
            GameObject settingObject = await _providerUiFactory.FactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.Setting);
            SettingPopup settingPopup = _assetService.Install.InstallToUiPopup<SettingPopup>(settingObject, _parent);
            
            GameObject winLoseObject = await _providerUiFactory.FactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.WinLose);
            WinLosePopup winLosePopup = _assetService.Install.InstallToUiPopup<WinLosePopup>(winLoseObject, _parent);
            
            GameObject characterViewObject = await _providerUiFactory.FactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.StartMatchViewAction);
            CharacterStartMatchPopup characterStartMatchPopup = _assetService.Install.InstallToUiPopup<CharacterStartMatchPopup>(characterViewObject, _parent);
            
            _popupService.AddPopupInList(TypePopup.Setting, settingPopup);
            _popupService.AddPopupInList(TypePopup.WinLose, winLosePopup);
            _popupService.AddPopupInList(TypePopup.CharacterStartMatch, characterStartMatchPopup);

            await UniTask.CompletedTask;
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
                    popup.Win.SetActive(true);
                    break;
                case MatchWin.Bot:
                    popup.Lose.SetActive(true);
                    break;
            }

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

        public void InitDataView(TopProgressViewWinUi viewData, CharacterMatchData matchDataData)
        {
            viewData.Name.text = matchDataData.Name;
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

        public void SetViewWin(MatchWin winner)
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
    }
}
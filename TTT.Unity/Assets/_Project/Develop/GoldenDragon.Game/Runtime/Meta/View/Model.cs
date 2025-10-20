using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using VContainer;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View
{
    public class Model
    {
        private PopupService _popupService;
        private MetaBlackPopupBackground _popupBackground;
        private PopupBase _activePopup;
        private bool _isActivePopup;
        private Lang _language;
        private AudioPlayer _audioPlayer;

        [Inject]
        public void Construct(PopupService popupService,Lang language,AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
            _language = language;
            _popupService = popupService;
        }

        public UniTask Initialized(MetaRoot metaRoot)
        {
            _popupBackground = metaRoot.GetPopupBackground();
            _popupBackground.Initialized(this);
            return UniTask.CompletedTask;
        }

        public void OpenPopupSetting()
        {
            _audioPlayer.Click();
            
            if (IsNotOpenPopup(TypePopup.Setting))
            {
                if (_activePopup is SettingPopup popup)
                {
                    popup.Construct(this,_language);
                    popup.Initialized();
                    _popupBackground.Show();
                    
                    popup.Show();
                }
            }
        }

        public void OpenPopupLeaderBoard()
        {
            _audioPlayer.Click();
            
            if (IsNotOpenPopup(TypePopup.LeaderBoard))
            {
                if (_activePopup is LeaderBoardPopup popup)
                {
                    popup.Construct(this,_language);
                    popup.Initialized();
                    _popupBackground.Show();
                    
                    popup.Show();
                }
            }
        }

        public void OpenPopupMatch()
        {
            _audioPlayer.Click();
            
            if (IsNotOpenPopup(TypePopup.Match))
            {
                if (_activePopup is MatchPopup popup)
                {
                    popup.Construct(this,_language);
                    popup.Initialized();
                    _popupBackground.Show();
                    
                    popup.Show();
                }
            }
        }

        public void OpenPopupInventory()
        {
            _audioPlayer.Click();
            
            if (IsNotOpenPopup(TypePopup.Inventory))
            {
                if (_activePopup is InventoryPopup popup)
                {
                    popup.Construct(this,_language);
                    popup.Initialized();
                    _popupBackground.Show();
                    
                    popup.Show();
                }
            }
        }

        public void OpenPopupShop()
        {
            _audioPlayer.Click();
            
            if (IsNotOpenPopup(TypePopup.Shop))
            {
                if (_activePopup is ShopPopup popup)
                {
                    popup.Construct(this,_language);
                    popup.Initialized();
                    
                    _popupBackground.Show();
                    
                    popup.Show();
                }
            }
        }

        public void ClosePopup()
        {
            _audioPlayer.Click();
            
            _isActivePopup = false;
            _activePopup.Hide();
            _popupBackground.Hide();
            _activePopup = null;
        }

        private bool IsNotOpenPopup(TypePopup typePopupOpen)
        {
            if (_isActivePopup == false)
            {
                _isActivePopup = true;
                _activePopup = _popupService.GetPopup(typePopupOpen);

                return true;
            }

            return false;
        }
    }
}
using Cysharp.Threading.Tasks;
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

        [Inject]
        public void Construct(PopupService popupService)
        {
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
            if (IsNotOpenPopup(TypePopup.Setting))
            {
                if (_activePopup is SettingPopup popup)
                {
                    popup.Construct(this);
                    
                    _popupBackground.Show();
                    
                    popup.Show();
                }
            }
        }

        public void OpenPopupLeaderBoard()
        {
            if (IsNotOpenPopup(TypePopup.LeaderBoard))
            {
                if (_activePopup is LeaderBoardPopup popup)
                {
                    popup.Construct(this);
                    
                    _popupBackground.Show();
                    
                    popup.Show();
                }
            }
        }

        public void OpenPopupMatch()
        {
            if (IsNotOpenPopup(TypePopup.Match))
            {
                if (_activePopup is MatchPopup popup)
                {
                    popup.Construct(this);
                    
                    _popupBackground.Show();
                    
                    popup.Show();
                }
            }
        }

        public void OpenPopupInventory()
        {
            if (IsNotOpenPopup(TypePopup.Inventory))
            {
                if (_activePopup is InventoryPopup popup)
                {
                    popup.Construct(this);
                    
                    _popupBackground.Show();
                    
                    popup.Show();
                }
            }
        }

        public void OpenPopupShop()
        {
            if (IsNotOpenPopup(TypePopup.Shop))
            {
                if (_activePopup is ShopPopup popup)
                {
                    popup.Construct(this);
                    
                    _popupBackground.Show();
                    
                    popup.Show();
                }
            }
        }

        public void ClosePopup()
        {
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
using Cysharp.Threading.Tasks;
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
        private bool _isActivePopup;
        private PopupBase _activePopup;

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
            if (_isActivePopup == false)
            {
                _isActivePopup = true;
                _activePopup = _popupService.GetPopup(TypePopup.Setting);
                
                if (_activePopup is SettingPopup popup)
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
    }
}
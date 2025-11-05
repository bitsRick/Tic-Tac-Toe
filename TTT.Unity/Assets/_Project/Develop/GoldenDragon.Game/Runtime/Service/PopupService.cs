using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service
{
    public class PopupService
    {
        private Dictionary<TypePopup, PopupData> _popupLists = new();
        private PopupBase _activePopup;
        private bool _isActivePopup;

        public void AddPopupInList<T>(TypePopup typePopup, T popup) where T: PopupBase => 
            _popupLists.Add(typePopup,new PopupData(popup));

        public async UniTask Release()
        {
            foreach (KeyValuePair<TypePopup,PopupData> popupData in _popupLists) 
                popupData.Value.Dispose();

            _popupLists = null;

            await UniTask.CompletedTask;
        }

        public bool TryGetPopup<T>(TypePopup typePopup, out T popup) where T : class
        {
            popup = default;
    
            if (_popupLists.TryGetValue(typePopup, out PopupData popupData))
            {
                popup = popupData.GetPopup() as T;
                return true;
            }
    
            return false;
        }

        public bool TryOpenPopup<TP>(TypePopup typePopup, out TP popupOut)
            where TP : class
        {
            if (IsNotOpenPopup(typePopup))
            {
                if (_activePopup is TP popup)
                {
                    popupOut = popup;
                    return true;
                }
            }

            popupOut = null;
            return false;
        }

        public void Rest()
        {
            _isActivePopup = false;
            _activePopup.Hide();
            _activePopup = null;
        }

        private bool IsNotOpenPopup(TypePopup typePopupOpen)
        {
            if (_isActivePopup == false)
            {
                _isActivePopup = true;
                _activePopup = GetPopup(typePopupOpen);
                return true;
            }

            return false;
        }

        private PopupBase GetPopup(TypePopup typePopup)
        {
            return _popupLists.TryGetValue(typePopup,out PopupData popup) ? popup.GetPopup() : null;
        }
    }
}
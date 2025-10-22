using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service
{
    public class PopupService
    {
        private Dictionary<TypePopup, PopupData> _popupLists = new();
        
        public void AddPopupInList<T>(TypePopup typePopup, T popup) where T: PopupBase => 
            _popupLists.Add(typePopup,new PopupData(popup));

        public async UniTask Release()
        {
            foreach (KeyValuePair<TypePopup,PopupData> popupData in _popupLists) 
                popupData.Value.Dispose();

            _popupLists = null;

            await UniTask.CompletedTask;
        }

        public PopupBase GetPopup(TypePopup typePopup)
        {
            return _popupLists.TryGetValue(typePopup,out PopupData popup) ? popup.GetPopup() : null;
        }
    }
}
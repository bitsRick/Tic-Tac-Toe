using System;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service
{
    public class PopupData:IDisposable
    {
        private PopupBase _popup;

        public PopupData(PopupBase popup)
        {
            _popup = popup;
        }

        public PopupBase GetPopup() => _popup;
        
        public void Dispose()
        {
            _popup.Dispose();
        }
    }
}
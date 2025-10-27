using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UnityEngine.EventSystems;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View
{
    public class BackPopupBackground:UI.Core.View,IPointerClickHandler
    {
        private Model _model;

        public void Initialized(Model model)
        {
            _model = model;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Log.Meta.D("Close Popup");
            _model.ClosePopup();
        }

        public override UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public override UniTask Hide()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}
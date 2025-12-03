using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UniRx;
using UnityEngine.EventSystems;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup
{
    public class BackPopupBackground:UI.Base.View,IPointerClickHandler
    {
        public Subject<Unit> OnEvenPointClickBackground = new Subject<Unit>();
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Log.Default.D(nameof(BackPopupBackground),"Close Popup");
            Hide();
            OnEvenPointClickBackground.OnNext(Unit.Default);
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
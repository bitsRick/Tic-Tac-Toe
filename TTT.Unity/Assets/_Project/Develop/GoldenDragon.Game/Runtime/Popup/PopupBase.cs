using Cysharp.Threading.Tasks;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup
{
    public abstract class PopupBase:DisposableView
    {
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
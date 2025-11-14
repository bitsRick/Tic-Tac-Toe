using Cysharp.Threading.Tasks;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI
{
    public interface IUiRoot
    {
        UniTask InitializedPopup();
        UniTask InitializedEvent();
        UniTask Show();
    }
}
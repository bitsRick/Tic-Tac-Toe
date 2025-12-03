using Cysharp.Threading.Tasks;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Base
{
    public interface IView
    {
        UniTask Show();
        UniTask Hide();
    }
}
using Cysharp.Threading.Tasks;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Core
{
    public interface IView
    {
        UniTask Show();
        UniTask Hide();
    }
}
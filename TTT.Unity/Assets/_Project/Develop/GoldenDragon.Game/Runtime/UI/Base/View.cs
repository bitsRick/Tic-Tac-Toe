using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Base
{
    public abstract class View:MonoBehaviour, IView
    {
        public abstract UniTask Show();

        public abstract UniTask Hide();
    }
}
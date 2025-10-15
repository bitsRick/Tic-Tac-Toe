using System;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Core;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup
{
    public abstract class DisposableView:View,IDisposable
    {
        public abstract void Dispose();
    }
}
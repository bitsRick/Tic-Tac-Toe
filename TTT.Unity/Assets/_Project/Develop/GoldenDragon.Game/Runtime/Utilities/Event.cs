using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities
{
    public static class EventUniRx
    {
        public static void CreateEventButtonClick(Button btn, GameObject popup, Action showItemShopSell)
        {
            btn.OnClickAsObservable().Subscribe(_ => { showItemShopSell?.Invoke(); }).AddTo(popup);
        }
    }
}
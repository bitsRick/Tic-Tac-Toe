using System;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Popup
{
    public class WinLosePopup:PopupBase
    {
        [SerializeField] private GameObject _win;
        [SerializeField] private GameObject _lose;
        [SerializeField] private Button _btnMenu;
        [SerializeField] private Button _btnNextMath;

        public GameObject Win => _win;
        public GameObject Lose => _lose;
        public Button ButtonMenu => _btnMenu;
        public Button ButtonNextMatch => _btnNextMath;

        public void Initialized()
        {
            _win.SetActive(false);
            _lose.SetActive(false);
        }

        public override void Dispose()
        {
            
        }
    }
}
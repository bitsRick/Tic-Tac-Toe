using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.TopInformation.Win
{
    public class WinImageUi:MonoBehaviour
    {
        [Header("Default")]
        [SerializeField] private Image _noWin;
        [Header("Победа")]
        [SerializeField] private Image _win;
        private bool _isNotWin = true;

        public Image Win => _win;
        public Image Default => _noWin;

        public bool IsNotWin
        {
            get => _isNotWin;
            set => _isNotWin = value;
        }
    }
}
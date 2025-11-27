using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Popup
{
    public class WinLosePopup:PopupBase
    {
        [Header("Win Informer")]
        [SerializeField] private GameObject _win;
        [Header("Lose Informer")]
        [SerializeField] private GameObject _lose;
        [Header("To Meta")]
        [SerializeField] private Button _btnMenu;
        [Header("Next Match")]
        [SerializeField] private Button _btnNextMath;
        [Header("Win Soft Value")]
        [SerializeField] private TextMeshProUGUI _xSoftValue;
        [SerializeField] private TextMeshProUGUI _oSoftValue;

        public GameObject Win => _win;
        public GameObject Lose => _lose;
        public Button ButtonMenu => _btnMenu;
        public Button ButtonNextMatch => _btnNextMath;
        public TextMeshProUGUI X => _xSoftValue;
        public TextMeshProUGUI O => _oSoftValue;

        public void Initialized()
        {
            _win.SetActive(false);
            _lose.SetActive(false);
            X.text = "0";
            O.text = "0";
        }

        public override void Dispose()
        {
            
        }
    }
}
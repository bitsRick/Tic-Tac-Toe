using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup
{
    public class MatchPopup:PopupBase
    {
        [Header("Lang")]
        [SerializeField] private TextMeshProUGUI _playerVsBot;

        [Header("Single player")]
        [SerializeField] private Button _x; 
        [SerializeField] private Button _o;
        [Header("Coop")] 
        [SerializeField] private Button _coop;

        public Button X => _x;
        public Button O => _o;
        public Button Coop => _coop;
            
        public void Initialized()
        {
            _playerVsBot.text = Lang.S.UI.POPUP.MATCH_POPUP.VsBot;
        }

        public override void Dispose()
        {
            UnityEngine.GameObject.Destroy(this);
        }
    }
}
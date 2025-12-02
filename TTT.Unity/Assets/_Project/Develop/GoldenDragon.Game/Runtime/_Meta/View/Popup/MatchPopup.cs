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
        
        public Button X => _x;
        public Button O => _o;
            
        public void Initialized()
        {
            _playerVsBot.text = Lang.S.UI.POPUP.MATCH.VsBot;
        }

        public override void Dispose()
        {
            UnityEngine.GameObject.Destroy(this);
        }
    }
}
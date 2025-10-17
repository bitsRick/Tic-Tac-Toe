using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup;
using TMPro;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup
{
    public class MatchPopup:PopupBase
    {
        [Header("Lang")]
        [SerializeField] private TextMeshProUGUI _playerVsBot;
        [SerializeField] private TextMeshProUGUI _playerVsPlayer;

        private void Awake()
        {
            _playerVsBot.text = Lang.Ui.Popup.MatchPopup.VsBot;
            _playerVsPlayer.text = Lang.Ui.Popup.MatchPopup.VsPlayer;
        }

        public override void Dispose()
        {
            
        }
    }
}
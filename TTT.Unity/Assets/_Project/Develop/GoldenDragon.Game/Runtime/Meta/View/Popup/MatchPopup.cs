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
        private Model _model;
        private Lang _language;

        public void Construct(Model model, Lang language)
        {
            _language = language;
            _model = model;
        }

        public void Initialized()
        {
            _playerVsBot.text = _language.UI.POPUP.MATCH_POPUP.VsBot;
            _playerVsPlayer.text = _language.UI.POPUP.MATCH_POPUP.VsPlayer;
        }

        public override void Dispose()
        {
            UnityEngine.GameObject.Destroy(this);
        }
    }
}
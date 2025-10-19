using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup;
using TMPro;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup
{
    public class LeaderBoardPopup : PopupBase
    {
        [Header("Lang")]
        [SerializeField] private TextMeshProUGUI _namePopupText;

        private Model _model;
        private Lang _language;

        public void Construct(Model model, Lang language)
        {
            _language = language;
            _model = model;
        }

        public void Initialized()
        {
            _namePopupText.text = _language.UI.POPUP.LEADER_BOARD.NameForm;
        }

        public override void Dispose()
        {
            UnityEngine.GameObject.Destroy(this);
        }
    }
}

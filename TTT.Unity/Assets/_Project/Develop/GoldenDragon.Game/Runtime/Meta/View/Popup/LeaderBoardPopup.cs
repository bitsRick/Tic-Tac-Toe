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

        private void Awake()
        {
            _namePopupText.text = Lang.Ui.Popup.LeaderBoard.NameForm;
        }

        public void Construct(Model model)
        {
            _model = model;
        }

        public override void Dispose()
        {
            UnityEngine.GameObject.Destroy(this);
        }
    }
}

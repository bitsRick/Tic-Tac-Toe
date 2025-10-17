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

        private void Awake()
        {
            _namePopupText.text = Lang.Ui.Popup.LeaderBoard.NameForm;
        }

        public override void Dispose()
        {
            
        }
    }
}

using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.LeaderBoardItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup;
using TMPro;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup
{
    public class LeaderBoardPopup : PopupBase
    {
        [Header("Lang")]
        [SerializeField] private TextMeshProUGUI _namePopupText;

        [Header("Массив мест лидеров")]
        [SerializeField] private ItemLeaderBoards[] _leaders;
        
        private Lang _language;
        public ItemLeaderBoards[] Leaders => _leaders;

        public void Construct(Lang language)
        {
            _language = language;
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

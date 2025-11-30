using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.LeaderBoardItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup;
using TMPro;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup
{
    public class LeaderBoardPopup : PopupBase
    {
        [Header("Lang")]
        [SerializeField] private TextMeshProUGUI _namePopupText;

        [Header("Массив мест лидеров")]
        [SerializeField] private Item[] _leaders;
        
        public Item[] Leaders => _leaders;
        
        public void Initialized()
        {
            _namePopupText.text = Lang.S.UI.POPUP.LEADER_BOARD.NameForm;
        }

        public override void Dispose()
        {
            UnityEngine.GameObject.Destroy(this);
        }
    }
}

using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup;
using TMPro;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup
{
    public class InventoryPopup:PopupBase
    {
        [Header("Lang")]
        [SerializeField] private TextMeshProUGUI _nameHeaderForm;
        [SerializeField] private TextMeshProUGUI _nameButtonBoard;
        private string _styleEnter;
        private Model _model;
        private Lang _language;
        
        public void Construct(Model model,Lang l)
        {
            _language = l;
            _model = model;
        }

        public override void Dispose()
        {
            Destroy(this);
        }

        public void Initialized()
        {
            _nameHeaderForm.text = _language.UI.POPUP.INVENTORY.Header;
            _nameButtonBoard.text = _language.UI.POPUP.INVENTORY.BoardButton;
            _styleEnter = _language.UI.POPUP.INVENTORY.StyleEnter;
        }
    }
}
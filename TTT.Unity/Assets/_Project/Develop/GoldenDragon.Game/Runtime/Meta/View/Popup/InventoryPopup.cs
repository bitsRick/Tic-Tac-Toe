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

        private void Awake()
        {
            _nameHeaderForm.text = Lang.Ui.Popup.Inventory.Header;
            _nameButtonBoard.text = Lang.Ui.Popup.Inventory.BoardButton;
            _styleEnter = Lang.Ui.Popup.Inventory.StyleEnter;
        }

        public void Construct(Model model)
        {
            _model = model;
        }

        public override void Dispose()
        {
            Destroy(this);
        }
    }
}
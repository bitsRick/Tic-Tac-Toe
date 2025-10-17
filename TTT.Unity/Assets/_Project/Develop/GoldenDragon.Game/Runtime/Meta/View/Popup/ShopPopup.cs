using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup;
using TMPro;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup
{
    public class ShopPopup:PopupBase
    {
        [Header("Lang")] 
        [SerializeField] private TextMeshProUGUI _nameHeader;
        [SerializeField] private TextMeshProUGUI _buttonBorder;
        
        private Model _model;
        private string _sellStyle;
        private string _buyButton;

        private void Awake()
        {
            _nameHeader.text = Lang.Ui.Popup.Shop.Header;
            _buttonBorder.text = Lang.Ui.Popup.Shop.BoardButton;
            _buyButton = Lang.Ui.Popup.Shop.Buy;
            _sellStyle = Lang.Ui.Popup.Shop.Sell;
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
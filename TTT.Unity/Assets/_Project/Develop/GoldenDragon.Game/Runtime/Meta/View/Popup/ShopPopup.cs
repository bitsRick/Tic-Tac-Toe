using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup;
using TMPro;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup
{
    public class ShopPopup:PopupBase
    {
        [SerializeField] private Transform _root;
        [Header("Lang")] 
        [SerializeField] private TextMeshProUGUI _nameHeader;
        [SerializeField] private TextMeshProUGUI _buttonBorder;
        
        private Model _model;
        private Lang _language;
        private string _sellStyle;
        private string _buyButton;

        public Transform RootInstance => _root;
        
        public void Construct(Model model,Lang language)
        {
            _language = language;
            _model = model;
        }

        public void Initialized()
        {
            _nameHeader.text = _language.UI.POPUP.SHOP.Header;
            _buttonBorder.text = _language.UI.POPUP.SHOP.BoardButton;
            _buyButton = _language.UI.POPUP.SHOP.Buy;
            _sellStyle = _language.UI.POPUP.SHOP.Sell;
        }

        public override void Dispose()
        {
            Destroy(this);
        }
    }
}
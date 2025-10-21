using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup
{
    public class ShopPopup:PopupBase
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private HorizontalLayoutGroup _horizontalLayout;
        [Header("Lang")] 
        [SerializeField] private TextMeshProUGUI _nameHeader;
        [SerializeField] private TextMeshProUGUI _buttonBorder;
        
        private Model _model;
        private Lang _language;
        private string _sellStyle;
        private string _buyButton;

        public GameObject RootInstance => _root;
        public HorizontalLayoutGroup HorizontalLayoutGroup => _horizontalLayout;
        
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
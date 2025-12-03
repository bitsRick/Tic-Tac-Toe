using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.Interface;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup
{
    public class InventoryPopup:PopupBase,IItemContainer
    {
        [Header("Lang")]
        [SerializeField] private TextMeshProUGUI _nameHeaderForm;
        [SerializeField] private TextMeshProUGUI _nameButtonBoard;
        [Header("Button")]
        [SerializeField] private Button _btnBoard;
        [SerializeField] private Button _btnX;
        [SerializeField] private Button _btnO;
        [Header("Container Item")]
        [SerializeField] private GameObject _root;

        public GameObject  Root
        {
            get => _root;
            set => _root = value;
        }

        public Button BtnBoard => _btnBoard;
        public Button BtnX => _btnX;
        public Button BtnO => _btnO;
        

        public override void Dispose()
        {
            Destroy(this);
        }

        public void Initialized()
        {
            _nameHeaderForm.text = Lang.S.UI.POPUP.INVENTORY.Header;
            _nameButtonBoard.text = Lang.S.UI.POPUP.INVENTORY.BoardButton;
        }
    }
}
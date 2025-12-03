using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.InventoryItem
{
    public class ItemStyle:MonoBehaviour,IItem
    {
        [Header("This Rect Transform")]
        [SerializeField] private RectTransform _rectTransform;
        [Header("Active Style Current")]
        [SerializeField] private GameObject activeGameObject;
        [Header("Active style btn")]
        [SerializeField] private Button _btnEnter;
        [Header("View Style")]
        [SerializeField] private Image _image;
        [Header("Lang")]
        [SerializeField] private TextMeshProUGUI _setText;
        
        private string _id;

        public ShowItemStyle Type { get; set; }
        
        public RectTransform RectTransform
        {
            get => _rectTransform;
            set => _rectTransform = value;
        }

        public Transform Transform => transform;

        public GameObject ActiveGameObject
        {
            get => activeGameObject;
            set => activeGameObject = value;
        }

        public Button Btn => _btnEnter;

        public Image Image => _image;

        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public void Initialized()
        {
            _setText.text = Lang.S.UI.POPUP.INVENTORY.StyleEnter;
        }
    }
}
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.InventoryItem
{
    public class ItemInventoryStyle:MonoBehaviour
    {
        [Header("This Rect Transform")]
        [SerializeField] private RectTransform _rectTransform;
        [Header("Active Style Current")]
        [SerializeField] private GameObject _activeStyle;
        [Header("Active style btn")]
        [SerializeField] private Button _btnEnter;
        [Header("View Style")]
        [SerializeField] private Image _image;
        private string _id;
        
        public TypeShowItemShop Type { get; set; }
        
        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public GameObject ActiveStyle => _activeStyle;
        public RectTransform RectTransform  => _rectTransform;
        public Button EnterStyle => _btnEnter;
        public Image ImageStyle => _image;
    }
}
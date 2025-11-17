using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.Interface;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.InventoryItem
{
    public class ItemInventoryStyle:MonoBehaviour,IItem
    {
        [Header("This Rect Transform")]
        [SerializeField] private RectTransform _rectTransform;
        [Header("Active Style Current")]
        [SerializeField] private GameObject activeGameObject;
        [Header("Active style btn")]
        [SerializeField] private Button _btnEnter;
        [Header("View Style")]
        [SerializeField] private Image _image;
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
    }
}
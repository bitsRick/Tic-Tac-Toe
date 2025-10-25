using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.Interface;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.ShopElementItem
{
    public class ItemSell : MonoBehaviour,IItem
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _btn;
        [SerializeField] private TextMeshProUGUI _valueX;
        [SerializeField] private TextMeshProUGUI _valueO;
        [SerializeField] private GameObject _buy;
        [SerializeField] private RectTransform _rectTransform;
        private string _id;
        private bool _isView;

        public TypeShowItemShop Type { get; set; }

        public Image Image => _image;

        public Button Btn => _btn;

        public Transform Transform => transform;

        public GameObject ActiveGameObject
        {
            get => _buy;
            set => _buy = value;
        }

        public RectTransform RectTransform
        {
            get => _rectTransform;
            set => _rectTransform = value;
        }

        public string X
        {
            get => _valueX.text;
            set => _valueX.text = value;
        }

        public string O
        {
            get => _valueO.text;
            set => _valueO.text = value;
        }

        public string Id
        {
            get => _id;
            set => _id = value;
        }
    }
}
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.ShopElementSell
{
    public class ItemSell : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _btn;
        [SerializeField] private TextMeshProUGUI _valueX;
        [SerializeField] private TextMeshProUGUI _valueO;
        [SerializeField] private GameObject _buy;
        [SerializeField] private RectTransform _rectTransform;
        private string _id;
        private bool _isView;
        public TypeShowElementShop Type { get; set; }

        public Image ImageStyle => _image;
        public Button ButtonBuy => _btn;
        public GameObject BuyView => _buy;
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
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.ShopElementSell
{
    public class ElementSell : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _btn;
        [SerializeField] private TextMeshProUGUI _valueX;
        [SerializeField] private TextMeshProUGUI _valueO;
        [SerializeField] private GameObject _buy;
        private Model _model;
        private string _id;
        private bool _isView;

        public Image ImageStyle => _image;
        public Button ButtonBuy => _btn;
        public GameObject BuyView => _buy;
        
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

        public void Constructo(Model model)
        {
            _model = model;
        }
    }
}
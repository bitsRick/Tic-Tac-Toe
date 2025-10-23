using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.InventoryElement
{
    public class InventoryElementStyle:MonoBehaviour
    {
        [SerializeField] private Button _btnEnter;
        [SerializeField] private Image _image;

        public Button EnterStyle => _btnEnter;
        public Image ImageStyle => _image;
    }
}
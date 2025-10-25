using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.Interface
{
    public interface IItem
    {
        public Image Image { get; }
        public Button Btn { get; }
        public Transform Transform { get; }
        public string Id { get; set; }
        public GameObject ActiveGameObject { get; set; }
        public TypeShowItemShop Type { get; set; }
        public RectTransform RectTransform { get; set; }
    }
}
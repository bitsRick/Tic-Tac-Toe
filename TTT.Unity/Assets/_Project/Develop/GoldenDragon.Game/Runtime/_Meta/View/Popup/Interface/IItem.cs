using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.Interface
{
    public interface IItem
    {
        public string Id { get; set; }
        public ShowItemStyle Type { get; set; }
        public Image Image { get; }
        public Button Btn { get; }
        public Transform Transform { get; }
        public GameObject ActiveGameObject { get; set; }
        public RectTransform RectTransform { get; set; }
    }
}
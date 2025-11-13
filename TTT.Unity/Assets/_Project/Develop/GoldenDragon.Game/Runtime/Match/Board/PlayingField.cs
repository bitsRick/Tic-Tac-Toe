using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board
{
    public class PlayingField:MonoBehaviour
    {
        [Header("Массив элементов")]
        [SerializeField] private Field[] _fields;
        [Header("Рамка")]
        [SerializeField] private Image _border;
        [Header("Задний фон Рамки")]
        [SerializeField] private Image _background;
        
        public Field[] Fields => _fields;
        public Subject<Unit> OnPlayerActionEnd = new Subject<Unit>();
        public Image Boder => _border;
        public Image background => _background;
    }
}
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
        
        public Field[] Fields => _fields;
        public Image Border => _border;
    }
}
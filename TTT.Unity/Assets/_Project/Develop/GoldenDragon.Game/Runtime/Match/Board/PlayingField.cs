using System;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
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
        public Image Border => _border;
        public Image background => _background;
    }
}
using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board
{
    public class PlayingField:MonoBehaviour,ILoadUnit
    {
        [Header("Массив элементов")]
        [SerializeField] private Field[] _fields;
        [Header("Рамка")]
        [SerializeField] private Image _border;
        [Header("Задний фон Рамки")]
        [SerializeField] private Image _background;
        private PlayerMatchData _playerMatchData;

        public Field[] Fields => _fields;
        public Subject<Unit> OnPlayerActionEnd = new Subject<Unit>(); 

        public void Construct(PlayerMatchData playerMatchData)
        {
            _playerMatchData = playerMatchData;
        }
        
        public async UniTask Load()
        {
            for (var i = 0; i < _fields.GetLength(0); i++)
            {
                Field field = _fields[i];

                field.Click.onClick.AsObservable().Subscribe((_) =>
                {
                    if (field.TrySetElement(_playerMatchData.Field)) OnPlayerActionEnd.OnNext(Unit.Default);
                }).AddTo(this);

                TypePositionElementToField type =
                    (TypePositionElementToField)Enum.GetValues(typeof(TypePositionElementToField)).GetValue(i);
                
                field.Initialized(type);
                await field.Load();
            }

            await Task.CompletedTask;
        }
    }
}
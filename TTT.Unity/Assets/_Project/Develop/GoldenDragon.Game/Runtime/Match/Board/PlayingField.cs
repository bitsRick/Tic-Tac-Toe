using System;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board
{
    public class PlayingField:MonoBehaviour,ILoadUnit
    {
        [SerializeField] private Field[] _fields;

        public Field[] Fields => _fields;
        
        public async UniTask Load()
        {
            for (var i = 0; i < _fields.GetLength(0); i++)
            {
                Field field = _fields[i];

                TypePositionElementToField type =
                    (TypePositionElementToField)Enum.GetValues(typeof(TypePositionElementToField)).GetValue(i);
                
                field.Initialized(type);
                await field.Load();
            }
        }

        private void FixedUpdate()
        {
            
        }
    }

    public class Field:MonoBehaviour,ILoadUnit
    {
        [SerializeField] private Image _x;
        [SerializeField] private Image _o;
        private TypePlayingField _playingField;

        public TypePlayingField Type => _playingField;
        public TypePositionElementToField Position;
        
        public async UniTask Load()
        {
            _x.gameObject.SetActive(false);
            _o.gameObject.SetActive(false);
            await UniTask.CompletedTask;
        }

        public void SetElement(TypePlayingField type)
        {
            switch (type)
            {
                case TypePlayingField.X:
                    _x.gameObject.SetActive(true);
                    break;
                
                case TypePlayingField.O:
                    _o.gameObject.SetActive(true);
                    break;
            }

            _playingField = type == TypePlayingField.X ? TypePlayingField.X : TypePlayingField.O;
        }

        public void Initialized(TypePositionElementToField typePosition)
        {
            Position = typePosition;
        }
    }
}
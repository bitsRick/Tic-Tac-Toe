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
    public class PlayingField:MonoBehaviour,ILoadUnit
    {
        [Header("Массив элементов")]
        [SerializeField] private Field[] _fields;
        [Header("Рамка")]
        [SerializeField] private Image _border;
        [Header("Задний фон Рамки")]
        [SerializeField] private Image _background;
        private CharacterMatch _playerData;

        public Field[] Fields => _fields;
        public Subject<Unit> OnPlayerActionEnd = new Subject<Unit>();
        public Image Border => _border;
        public Image background => _background;

        public void Initialized(CharacterMatch playerData)
        {
            _playerData = playerData;
        }

        public async UniTask Load()
        {
            await InitializedPlayingField();
            await Task.CompletedTask;
        }

        public void OnMouseEnterField(Field fieldData)
        {
            SetViewField(fieldData, true);
        }

        public void OnMouseExitField(Field fieldData)
        {
            
            SetViewField(fieldData, false);
        }

        public void OnSetTypeInField(CharacterMatch characterMatch, Field field)
        {
            if (TrySetField(characterMatch.Field,field))
                    OnPlayerActionEnd.OnNext(Unit.Default);
        }

        private async UniTask InitializedPlayingField()
        {
            TypePositionElementToField[] enumValues = Enum.GetValues(typeof(TypePositionElementToField))
                .Cast<TypePositionElementToField>()
                .ToArray();
            
            for (var i = 0; i < _fields.GetLength(0); i++)
            {
                TypePositionElementToField type = enumValues[i];
                
                Field field = _fields[i];
                field.X.gameObject.SetActive(false);
                field.O.gameObject.SetActive(false);
                
                field.Initialized(type,this);
                field.Btn.onClick.AsObservable().Subscribe((_) => { OnSetTypeInField(_playerData, field); }).AddTo(this);
                
                await Task.CompletedTask;
            }
        }

        private void SetViewField(Field data, bool isView)
        {
            if (data.CurrentPlayingField != TypePlayingField.None)
                return;

            Image typeFieldImage = _playerData.Field == TypePlayingField.X ? data.X : data.O;
            typeFieldImage.gameObject.SetActive(isView);
            
            data.Btn.targetGraphic = isView ? typeFieldImage : data.Empty;
        }

        private bool TrySetField(TypePlayingField type,Field field)
        {
            if (field.CurrentPlayingField != TypePlayingField.None)
                return false;
            
            Image typeFieldImage = _playerData.Field == TypePlayingField.X ? field.X : field.O;
            typeFieldImage.gameObject.SetActive(true);
            
            field.CurrentPlayingField = type == TypePlayingField.X ? TypePlayingField.X : TypePlayingField.O;
            field.Btn.targetGraphic = null;

            return true;
        }
    }
}
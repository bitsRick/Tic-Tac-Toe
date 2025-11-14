using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board
{
    public class Field:MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _x;
        [SerializeField] private Image _o;
        [SerializeField] private Image _empty;
        [SerializeField] private Button btn;
        
        private TypePositionElementToField _positionElementToField;
        private TypePlayingField _currentCurrentPlayingFieldPlayingField;
        private PlayingField _playingField;
        public Image X => _x;
        public Image O => _o;
        public Image Empty => _empty;
        
        public Button Btn => btn;
        public TypePlayingField CurrentPlayingField
        {
            get => _currentCurrentPlayingFieldPlayingField;
            set => _currentCurrentPlayingFieldPlayingField = value;
        }

        public TypePositionElementToField Position => _positionElementToField; 
        
        public void Initialized(TypePositionElementToField typePosition, PlayingField playingField)
        {
            _playingField = playingField;
            _positionElementToField = typePosition;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _playingField.OnMouseEnterField(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _playingField.OnMouseExitField(this);
        }
    }
}
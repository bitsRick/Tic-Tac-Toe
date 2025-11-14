using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View;
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
        private MatchUiRoot _matchUiRoot;
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
        
        public void Initialized(TypePositionElementToField typePosition, MatchUiRoot matchUiRoot)
        {
            _matchUiRoot = matchUiRoot;
            _positionElementToField = typePosition;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _matchUiRoot.OnMouseEnterField(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _matchUiRoot.OnMouseExitField(this);
        }
    }
}
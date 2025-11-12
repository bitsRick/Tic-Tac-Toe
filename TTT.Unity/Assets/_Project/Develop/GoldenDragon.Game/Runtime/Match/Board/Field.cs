using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board
{
    public class Field:MonoBehaviour,ILoadUnit
    {
        [SerializeField] private Image _x;
        [SerializeField] private Image _o;
        [SerializeField] private Button _click;
        private TypePlayingField _playingField;

        public Button Click => _click;
        public TypePlayingField Type => _playingField;
        
        [field:HideInInspector]public TypePositionElementToField Position;
        
        public async UniTask Load()
        {
            _x.gameObject.SetActive(false);
            _o.gameObject.SetActive(false);
            await UniTask.CompletedTask;
        }

        public bool TrySetElement(TypePlayingField type)
        {
            if (_playingField != TypePlayingField.None)
            {
                return false;
            }

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

            return true;
        }

        public void Initialized(TypePositionElementToField typePosition)
        {
            Position = typePosition;
        }
    }
}
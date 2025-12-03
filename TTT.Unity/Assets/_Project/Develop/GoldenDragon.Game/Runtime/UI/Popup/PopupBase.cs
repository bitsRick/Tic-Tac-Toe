using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup
{
    public abstract class PopupBase:DisposableView
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private void OnValidate()
        {
            if (_canvasGroup == null)
            {
                if (TryGetComponent(out CanvasGroup canvasGroup))
                {
                    _canvasGroup = canvasGroup;
                }
                else
                {
                    CanvasGroup addComponentCanvasGroup = gameObject.AddComponent<CanvasGroup>();
                    _canvasGroup = addComponentCanvasGroup;
                }
            }
        }
        
        public override UniTask Show()
        {
            if (_canvasGroup != null) _canvasGroup.interactable = true;
            
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public override UniTask Hide()
        {
            if (_canvasGroup != null) _canvasGroup.interactable = false;
            
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Base;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using TMPro;
using UnityEngine;

namespace GoldenDragon
{
    public class LoadingView : View
    {
        [Header("Lang")] 
        [SerializeField] private TextMeshProUGUI _loading;
        [SerializeField] private TextMeshProUGUI _nameGame;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private int _timeReduceAlpha;
        private Tween _fadeTween;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public async UniTask Initialized()
        {
            _loading.text = Lang.S.UI.LOADING_SCREEN.Loading;
            _nameGame.text = Lang.S.UI.LOADING_SCREEN.Name;

            await Task.CompletedTask;
        }

        public override UniTask Show()
        {
            _canvasGroup.alpha = 1f;
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public override UniTask Hide()
        {
            if (_fadeTween != null) _fadeTween.Kill();
            
            _fadeTween = _canvasGroup.DOFade(0f, RuntimeConstants.LoadingView.CountNegativeAlpha)
                .SetAutoKill(true)
                .SetTest(() => gameObject.SetActive(false))
                .Play();
            
            return UniTask.CompletedTask;
        }

        private void OnDestroy()
        {
            if (_fadeTween != null) _fadeTween.Kill();
        }
    }
}

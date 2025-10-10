using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Core;
using UnityEngine;

namespace GoldenDragon
{
    public class LoadingView : View
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        private readonly float _countNegativeAlpha = 0.05f;
        private int _timeReduceAlpha;
        private Tween _fadeTween = null;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void OnDestroy()
        {
            if (_fadeTween != null) _fadeTween.Kill();
        }

        public override UniTask Show()
        {
            return UniTask.CompletedTask;
        }

        public override UniTask Hide()
        {
            if (_fadeTween != null) _fadeTween.Kill();
            
            _fadeTween = _canvasGroup.DOFade(0f, 0.75f)
                .SetAutoKill(true)
                .Play();

            return UniTask.CompletedTask;
        }
    }
}

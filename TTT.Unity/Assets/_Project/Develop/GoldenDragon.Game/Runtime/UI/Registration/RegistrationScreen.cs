using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Core;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon
{
    public class RegistrationScreen : View
    {
        [SerializeField] private TMP_InputField  _inputField;
        [SerializeField] private Button _btn;
        [SerializeField] private CanvasGroup _selfGroup;
        private bool _isActive;
        
        public void Initialized(BootstrapFlow bootstrapFlow)
        {
            _btn.OnClickAsObservable().Subscribe(_ =>
            {
                if (_isActive)
                    return;
               
                _isActive = true;
                bootstrapFlow.SetRegistration(_inputField.text);
            }).AddTo(this);
        }

        public override UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public override async UniTask Hide()
        {
            await _selfGroup.DOFade(0f, Constant.B.DurationFadeRegistrationScreen).SetAutoKill(true).Play();
            gameObject.SetActive(false);
        }
    }
}

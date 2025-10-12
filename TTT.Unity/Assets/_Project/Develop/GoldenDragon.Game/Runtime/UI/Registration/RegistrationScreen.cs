using System;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Core;
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
        
        public void Initialized(BootstrapFlow bootstrapFlow)
        {
            _btn.OnClickAsObservable().Subscribe(_ =>  bootstrapFlow.SetRegistration(_inputField.text)).AddTo(this);
        }

        public override UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public override UniTask Hide()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}

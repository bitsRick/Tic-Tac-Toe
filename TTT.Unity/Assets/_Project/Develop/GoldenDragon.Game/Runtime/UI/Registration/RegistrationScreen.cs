using System;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon
{
    public class RegistrationScreen : View
    {
        [SerializeField] private TMP_InputField  _inputField;
        [SerializeField] private Button _btn;
        private BootstrapFlow _bootstrapFlow;

        private void OnEnable()
        {
            _btn.onClick.AddListener(OnClickBtn);
        }

        private void OnDestroy()
        {
            _bootstrapFlow = null;
            _btn.onClick.RemoveListener(OnClickBtn);
        }

        public void Initialized(BootstrapFlow bootstrapFlow)
        {
            _bootstrapFlow = bootstrapFlow;
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

        private void OnClickBtn()
        {
            _bootstrapFlow.SetRegistration(_inputField.text);
        }
    }
}

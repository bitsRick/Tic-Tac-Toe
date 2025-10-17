using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
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
        [Header("Switch Language")]
        [SerializeField] private TMP_Dropdown _switchLanguage;
        
        [Header("Lang")]
        [SerializeField] private TextMeshProUGUI _registration;
        [SerializeField] private TextMeshProUGUI _nick;
        [SerializeField] private TextMeshProUGUI _nickWrite;
        [SerializeField] private TextMeshProUGUI _enter;
        [SerializeField] private TextMeshProUGUI _lang;
        
        [SerializeField] private TMP_InputField  _inputField;
        [SerializeField] private Button _btn;
        [SerializeField] private CanvasGroup _selfGroup;
        private bool _isActive;

        private void Awake()
        {
            _registration.text = Lang.Ui.RegistrationScreen.Registration;
            _nick.text = Lang.Ui.RegistrationScreen.Nick;
            _nickWrite.text = Lang.Ui.RegistrationScreen.NickWrite;
            _enter.text = Lang.Ui.RegistrationScreen.Enter;
            _lang.text = Lang.Ui.RegistrationScreen.Language;
            
            _switchLanguage.options.Clear();
            _switchLanguage.options.Add(new TMP_Dropdown.OptionData(
                Constant.B.Lang.RusLangFile.Remove(
                    Constant.B.Lang.IndexRemove(Constant.B.Lang.RusLangFile.Length))));
            
            _switchLanguage.options.Add(new TMP_Dropdown.OptionData(
                Constant.B.Lang.EngLangFile.Remove(
                    Constant.B.Lang.IndexRemove(Constant.B.Lang.EngLangFile.Length))));
            
            _switchLanguage.value = 0;
        }

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
            await _selfGroup.DOFade(0f, Constant.B.DurationFadeRegistrationScreen)
                .SetAutoKill(true)
                .Play();
            
            gameObject.SetActive(false);
        }
    }
}

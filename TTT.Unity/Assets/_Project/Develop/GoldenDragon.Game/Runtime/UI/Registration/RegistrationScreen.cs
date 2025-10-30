using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
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
            _switchLanguage.options.Clear();
            _switchLanguage.options.Add(new TMP_Dropdown.OptionData(
                Constant.B.Lang.RusLangFile.Remove(
                    Constant.B.Lang.IndexRemove(Constant.B.Lang.RusLangFile.Length))));
            
            _switchLanguage.options.Add(new TMP_Dropdown.OptionData(
                Constant.B.Lang.EngLangFile.Remove(
                    Constant.B.Lang.IndexRemove(Constant.B.Lang.EngLangFile.Length))));
            
            _switchLanguage.value = 0;
        }
        
        public UniTask Initialized(BootstrapFlow bootstrapFlow)
        {
            _btn.OnClickAsObservable().Subscribe(_ =>
            {
                AudioPlayer.Click();
                
                if (_isActive)
                    return;
               
                _isActive = true;
                bootstrapFlow.SetRegistration(_inputField.text);
            }).AddTo(this);
            
            _registration.text = Lang.S.UI.REGISTRATION_SCREEN.Registration;
            _nick.text = Lang.S.UI.REGISTRATION_SCREEN.Nick;
            _nickWrite.text = Lang.S.UI.REGISTRATION_SCREEN.NickWrite;
            _enter.text = Lang.S.UI.REGISTRATION_SCREEN.Enter;
            _lang.text = Lang.S.UI.REGISTRATION_SCREEN.Language;
            
            return UniTask.CompletedTask;
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

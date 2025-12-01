using Cysharp.Threading.Tasks;
using DG.Tweening;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Base;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime._Bootstrap.RegistrationView
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
        private BootstrapFlow _bootstrapFlow;

        private void Awake()
        {
            _switchLanguage.options.Clear();
            _switchLanguage.options.Add(new TMP_Dropdown.OptionData(
                RuntimeConstants.Lang.RusLangFile.Remove(
                    RuntimeConstants.Lang.IndexRemove(RuntimeConstants.Lang.RusLangFile.Length))));
            
            _switchLanguage.options.Add(new TMP_Dropdown.OptionData(
                RuntimeConstants.Lang.EngLangFile.Remove(
                    RuntimeConstants.Lang.IndexRemove(RuntimeConstants.Lang.EngLangFile.Length))));
            
            _switchLanguage.value = 0;
        }

        public void Resolve(BootstrapFlow bootstrapFlow)
        {
            _bootstrapFlow = bootstrapFlow;
        }
        
        public UniTask Initialized()
        {
            _btn.OnClickAsObservable().Subscribe(_ =>
            {
                AudioPlayer.S.Click();
                
                if (_isActive)
                    return;
               
                _isActive = true;
                _bootstrapFlow.SetRegistration(_inputField.text);
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
            await _selfGroup.DOFade(0f,RuntimeConstants.RegistrationScreen.DurationFadeRegistrationScreen)
                .SetAutoKill(true)
                .Play();
            
            gameObject.SetActive(false);
        }
    }
}

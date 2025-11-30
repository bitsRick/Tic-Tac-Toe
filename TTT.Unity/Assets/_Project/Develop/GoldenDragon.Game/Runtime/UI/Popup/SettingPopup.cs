using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup
{
    public class SettingPopup : PopupBase
    {
        [Header("Lang")] 
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _soundText;
        [SerializeField] private TextMeshProUGUI _musicText;

        [Header("SoundImageButton")]
        [SerializeField] private Image _onSoundMute;
        [SerializeField] private Image _offSoundMute;
        
        [Header("MusicImageButton")]
        [SerializeField] private Image _onMusicMute;
        [SerializeField] private Image _offMusicMute;

        [Header("Setting Audio")] 
        [SerializeField]private Button _soundMute;
        [SerializeField]private Button _musicMute;
        [SerializeField]private Slider _musicSlider;
        [SerializeField]private Slider _soundSlider;

        [SerializeField] private Button _toMeta;
        
        public Image SoundOff => _offSoundMute;
        public Image SoundOn => _onSoundMute;
        public Image MusicOff => _offMusicMute;
        public Image MusicOn => _onMusicMute;
        
        public Button SoundMute => _soundMute;
        public Button MusicMute => _musicMute;
        public Slider MusicSlider => _musicSlider;
        public Slider SoundSlider => _soundSlider;
        public Button ToMeta => _toMeta;
        
        public void Initialized(StateFlow stateFlow)
        {
            _headerText.text = Lang.S.UI.POPUP.SETTING.Name;
            _soundText.text = Lang.S.UI.POPUP.SETTING.Sound;
            _musicText.text = Lang.S.UI.POPUP.SETTING.Music;

            switch (stateFlow)
            {
                case StateFlow.Match:
                    _toMeta.gameObject.SetActive(true);
                    break;
                
                case StateFlow.Meta:
                    _toMeta.gameObject.SetActive(false);
                    break;
            }
        }
        
        public override void Dispose()
        {
           Destroy(this);
        }
    }
}

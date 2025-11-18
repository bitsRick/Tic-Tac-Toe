using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
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

        [Header("Setting Audio")] 
        [SerializeField]private Button _soundMute;
        [SerializeField]private Button _musicMute;
        [SerializeField]private Slider _musicSlider;
        [SerializeField]private Slider _soundSlider;
        
        public Button SoundMute => _soundMute;
        public Button MusicMute => _musicMute;
        public Slider MusicSlider => _musicSlider;
        public Slider SoundSlider => _soundSlider;
        
        public void Initialized()
        {
            _headerText.text = Lang.S.UI.POPUP.SETTING.Name;
            _soundText.text = Lang.S.UI.POPUP.SETTING.Sound;
            _musicText.text = Lang.S.UI.POPUP.SETTING.Music;
        }

        public override UniTask Show()
        {
            return base.Show();
        }

        public override UniTask Hide()
        {
            return base.Hide();
        }

        public override void Dispose()
        {
           UnityEngine.GameObject.Destroy(this);
        }
    }
}

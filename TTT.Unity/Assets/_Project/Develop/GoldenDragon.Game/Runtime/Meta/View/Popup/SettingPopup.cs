using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup;
using TMPro;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup
{
    public class SettingPopup : PopupBase
    {
        [Header("Lang")] 
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _soundText;
        [SerializeField] private TextMeshProUGUI _musicText;
        
        private Model _model;

        public void Construct(Model model)
        {
            _model = model;
        }

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

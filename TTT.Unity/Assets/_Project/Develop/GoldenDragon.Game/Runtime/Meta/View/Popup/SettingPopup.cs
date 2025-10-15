using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup;

namespace GoldenDragon
{
    public class SettingPopup : PopupBase
    {
        private Model _model;
        
        public void Construct(Model model)
        {
            _model = model;
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

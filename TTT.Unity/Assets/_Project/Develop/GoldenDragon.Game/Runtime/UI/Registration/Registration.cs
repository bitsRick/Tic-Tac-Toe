using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon
{
    public class Registration : View
    {
        [SerializeField] private TMP_InputField  _inputField;
        [SerializeField] private Button _btn;
        
        public override UniTask Show()
        {
            return UniTask.CompletedTask;
        }

        public override UniTask Hide()
        {
            return UniTask.CompletedTask;
        }
    }
}

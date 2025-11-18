using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup;
using TMPro;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Popup
{
    public class CharacterStartMatchPopup:PopupBase
    {
        [SerializeField] private TextMeshProUGUI _name;

        public TextMeshProUGUI Name => _name; 
        
        public override void Dispose()
        {
            
        }
    }
}
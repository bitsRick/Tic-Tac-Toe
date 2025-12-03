using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup;
using TMPro;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Popup
{
    public class CharacterStartMatchPopup:PopupBase
    {
        [Header("Lang")]
        [SerializeField] private TextMeshProUGUI _firstAction;
        
        [SerializeField] private TextMeshProUGUI _name;

        public TextMeshProUGUI Name => _name;

        public void Initialized()
        {
            _firstAction.text = Lang.S.UI.POPUP.CHARACTER_START_MATCH.FirstAction;
        }

        public override void Dispose()
        {
            
        }
    }
}
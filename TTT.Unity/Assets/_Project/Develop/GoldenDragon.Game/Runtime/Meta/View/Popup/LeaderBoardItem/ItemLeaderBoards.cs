using TMPro;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.LeaderBoardItem
{
    public class ItemLeaderBoards:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;

        public string Name => _name.text;
    }
}
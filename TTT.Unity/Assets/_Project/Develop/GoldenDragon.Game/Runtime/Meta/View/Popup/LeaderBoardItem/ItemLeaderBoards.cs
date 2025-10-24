using TMPro;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.LeaderBoardItem
{
    public class ItemLeaderBoards:MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _currentTop;

        public string Name
        {
            get => _name.text;
            set => _name.text = value;
        }

        public string Score
        {
            get => _score.text;
            set => _score.text = value;
        }

        public string CurrentTop
        {
            get => _currentTop.text;
            set => _currentTop.text = value;
        }
    }
}
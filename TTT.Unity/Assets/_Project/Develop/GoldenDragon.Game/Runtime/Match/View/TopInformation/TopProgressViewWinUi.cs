using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.TopInformation.Win;
using TMPro;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.TopInformation
{
    public class TopProgressViewWinUi:MonoBehaviour
    {
        [Header("имя игрока")]
        [SerializeField] private TextMeshProUGUI _name;
        [Header("подсветка во время хода")]
        [SerializeField] private Color _actionRound;
        [SerializeField] private Color _noActionRound;
        [Header("1 победа")]
        [SerializeField] private WinImageUi _winOne;
        [Header("2 победа")]
        [SerializeField] private WinImageUi _winTwo;
        [Header("3 победа")]
        [SerializeField] private WinImageUi _winThree;

        public TextMeshProUGUI Name => _name;
        public WinImageUi WinOne => _winOne;
        public WinImageUi WinTwo => _winTwo;
        public WinImageUi WinThree => _winThree;
    }
}
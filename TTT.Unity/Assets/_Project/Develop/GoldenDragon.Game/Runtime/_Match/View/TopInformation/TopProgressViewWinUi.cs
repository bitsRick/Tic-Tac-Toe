using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.TopInformation.Win;
using TMPro;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.TopInformation
{
    public class TopProgressViewWinUi:MonoBehaviour
    {
        [Header("Player name")]
        [SerializeField] private TextMeshProUGUI _name;
        [Header("Backlight during operation")]
        [SerializeField] private Color _actionRound;
        [SerializeField] private Color _noActionRound;
        [Header("1 win")]
        [SerializeField] private WinImageUi _winOne;
        [Header("2 win")]
        [SerializeField] private WinImageUi _winTwo;
        [Header("3 win")]
        [SerializeField] private WinImageUi _winThree;

        public TextMeshProUGUI Name => _name;
        public WinImageUi WinOne => _winOne;
        public WinImageUi WinTwo => _winTwo;
        public WinImageUi WinThree => _winThree;

        public Color Active => _actionRound;
        public Color NoActive => _noActionRound;
    }
}
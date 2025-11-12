using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round
{
    public static class RandomRound
    {
        private const int PlayerAction = 0;
        private const int BotAction = 2;

        public static MatchMode GetFirstCharacterAction()
        {
            return Random.Range(PlayerAction, BotAction) == PlayerAction ? MatchMode.PlayerAction : MatchMode.BotAction;
        }
    }
}
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round
{
    public class RoundRandom
    {
        public MatchMode GetFirstCharacterAction() => 
            Random.Range(RuntimeConstants.Match.PlayerAction, RuntimeConstants.Match.BotAction) == RuntimeConstants.Match.PlayerAction 
                ? MatchMode.PlayerAction 
                : MatchMode.BotAction;
    }
}
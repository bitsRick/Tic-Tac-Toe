using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round
{
    public class RoundRandom
    {
        public MatchMode GetFirstCharacterAction() => 
            Random.Range(Constant.M.PlayerAction, Constant.M.BotAction) == Constant.M.PlayerAction 
                ? MatchMode.PlayerAction 
                : MatchMode.BotAction;
    }
}
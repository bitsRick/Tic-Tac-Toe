namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities
{
    public static class SupportMatchAction
    {
        public static TypePlayingField GetBotTypeFieldAction(SessionMatch player)
        {
            switch (player)
            {
                case SessionMatch.X:
                    return TypePlayingField.O;
                case SessionMatch.O:
                    return TypePlayingField.X;
            }

            return TypePlayingField.None;
        }

        public static TypePlayingField GetPlayerTypeFieldAction(SessionMatch player)
        {
            switch (player)
            {
                case SessionMatch.X:
                    return TypePlayingField.X;
                case SessionMatch.O:
                    return TypePlayingField.O;
            }

            return TypePlayingField.None;
        }
    }
}
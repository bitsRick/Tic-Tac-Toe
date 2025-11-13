namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities
{
    public static class Support
    {
        public static TypePlayingField GetBotTypeFieldAction(TypeSessionMatch playerType)
        {
            switch (playerType)
            {
                case TypeSessionMatch.X:
                    return TypePlayingField.O;
                case TypeSessionMatch.O:
                    return TypePlayingField.X;
            }

            return TypePlayingField.None;
        }

        public static TypePlayingField GetPlayerTypeFieldAction(TypeSessionMatch playerType)
        {
            switch (playerType)
            {
                case TypeSessionMatch.X:
                    return TypePlayingField.X;
                case TypeSessionMatch.O:
                    return TypePlayingField.O;
            }

            return TypePlayingField.None;
        }
    }
}
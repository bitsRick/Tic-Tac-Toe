using System;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player
{
    [Serializable]
    public class PlayerData
    {
        public bool IsRegistred;
        public string Nick;
        public int XCount;
        public int YCount;
        public int WinCount;
        public int LoseCount;

        public PlayerData(string nick)
        {
            IsRegistred = true;
            Nick = nick;
            XCount = 0;
            YCount = 0;

            ShopPlayerData = new ShopPlayerData();
        }


        public ShopPlayerData ShopPlayerData;
    }

    [Serializable]
    public class ShopPlayerData
    {
    }
}

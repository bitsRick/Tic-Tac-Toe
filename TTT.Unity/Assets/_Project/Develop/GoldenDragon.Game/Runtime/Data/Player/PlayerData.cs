using System;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

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

            ShopPlayerData = new ShopPlayerData[]
            {
                new ShopPlayerData(){Id = "Default_O",TypeStyleElementShop = TypeShowElementShop.O},
                new ShopPlayerData(){Id = "Default_X",TypeStyleElementShop = TypeShowElementShop.X},
                new ShopPlayerData(){Id = "Default_Board",TypeStyleElementShop = TypeShowElementShop.Board},
            };
        }


        public ShopPlayerData[] ShopPlayerData;
    }

    [Serializable]
    public class ShopPlayerData
    {
        public string Id;
        public TypeShowElementShop TypeStyleElementShop;
    }
}

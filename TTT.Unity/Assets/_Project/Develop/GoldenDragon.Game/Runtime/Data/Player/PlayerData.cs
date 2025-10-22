using System;
using System.Collections.Generic;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player
{
    [Serializable]
    public class PlayerData
    {
        public bool IsRegistred;
        public string Nick;
        public int SoftValueX;
        public int SoftValueO;
        public int WinCount;
        public int LoseCount;

        public PlayerData(string nick)
        {
            IsRegistred = true;
            Nick = nick;
            SoftValueX = 0;
            SoftValueO = 0;

            ShopPlayerData = new List<ShopPlayerData>()
            {
                new(){Id = "Default_O",TypeStyleElementShop = TypeShowElementShop.O},
                new(){Id = "Default_X",TypeStyleElementShop = TypeShowElementShop.X},
                new (){Id = "Default_Board",TypeStyleElementShop = TypeShowElementShop.Board},
            };
        }


        public List<ShopPlayerData> ShopPlayerData;
    }

    [Serializable]
    public class ShopPlayerData
    {
        public string Id;
        public TypeShowElementShop TypeStyleElementShop;
    }
}

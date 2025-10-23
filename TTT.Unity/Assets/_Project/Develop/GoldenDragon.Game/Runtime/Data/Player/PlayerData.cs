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
        public string StyleBoardEnter;
        public string StyleXEnter;
        public string StyleOEnter;

        public PlayerData(string nick)
        {
            IsRegistred = true;
            Nick = nick;
            SoftValueX = 0;
            SoftValueO = 0;

            ShopPlayerData = new List<ShopPlayerData>()
            {
                new(){Id = Constant.StyleData.DefaultO,TypeStyleElementShop = TypeShowElementShop.O},
                new(){Id = Constant.StyleData.DefaultX,TypeStyleElementShop = TypeShowElementShop.X},
                new (){Id = Constant.StyleData.DefaultBoard,TypeStyleElementShop = TypeShowElementShop.Board},
            };

            StyleBoardEnter = Constant.StyleData.DefaultBoard;
            StyleXEnter = Constant.StyleData.DefaultX;
            StyleOEnter = Constant.StyleData.DefaultO;
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

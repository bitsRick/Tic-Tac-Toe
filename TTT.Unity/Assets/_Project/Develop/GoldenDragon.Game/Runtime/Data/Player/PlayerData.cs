using System;
using System.Collections.Generic;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player
{
    [Serializable]
    public class PlayerData
    {
        public StyleData Board;
        public StyleData X;
        public StyleData O;
        public bool IsRegistred;
        public int Score;
        public int SoftValueX;
        public int SoftValueO;
        public int WinCount;
        public int LoseCount;
        public string Nick;

        public PlayerData(string nick)
        {
            IsRegistred = true;
            Nick = nick;
            SoftValueX = 0;
            SoftValueO = 0;
            Score = 0;

            ShopPlayerData = new List<StyleData>()
            {
                new(){Id = Constant.StyleData.DefaultO,Type = TypeShowItemStyle.O},
                new(){Id = Constant.StyleData.DefaultX,Type = TypeShowItemStyle.X},
                new (){Id = Constant.StyleData.DefaultBoard,Type = TypeShowItemStyle.Board},
            };

            Board = new StyleData()
            {
                Id = Constant.StyleData.DefaultBoard,
                Type = TypeShowItemStyle.Board
            };
            
            X = new StyleData()
            {
                Id = Constant.StyleData.DefaultX,
                Type = TypeShowItemStyle.O
            };
            
            O = new StyleData()
            {
                Id = Constant.StyleData.DefaultO,
                Type = TypeShowItemStyle.X
            };
        }
        
        public List<StyleData> ShopPlayerData;
    }

    [Serializable]
    public class StyleData
    {
        public string Id;
        public TypeShowItemStyle Type;
    }
}

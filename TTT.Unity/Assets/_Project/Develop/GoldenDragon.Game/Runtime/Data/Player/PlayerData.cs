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
        public AudioSetting AudioSetting;
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
            SoftValueX = 100;
            SoftValueO = 100;
            Score = 0;

            ShopPlayerData = new List<StyleData>()
            {
                new(){Id = Constant.StyleData.DefaultO,Type = ShowItemStyle.O},
                new(){Id = Constant.StyleData.DefaultX,Type = ShowItemStyle.X},
                new (){Id = Constant.StyleData.DefaultBoard,Type = ShowItemStyle.Board},
            };

            Board = new StyleData()
            {
                Id = Constant.StyleData.DefaultBoard,
                Type = ShowItemStyle.Board
            };
            
            X = new StyleData()
            {
                Id = Constant.StyleData.DefaultX,
                Type = ShowItemStyle.X
            };
            
            O = new StyleData()
            {
                Id = Constant.StyleData.DefaultO,
                Type = ShowItemStyle.O
            };

            AudioSetting = new AudioSetting(false,false,1f,1f);
        }
        
        public List<StyleData> ShopPlayerData;
    }

    [Serializable]
    public class AudioSetting
    {
        public bool IsMusicMute;
        public bool IsSoundMute;
        public float VolumeMusic;
        public float VolumeSound;

        public AudioSetting(bool isMusicMute, bool isSoundMute, float volumeMusic, float volumeSound)
        {
            IsMusicMute = isMusicMute;
            IsSoundMute = isSoundMute;
            VolumeMusic = volumeMusic;
            VolumeSound = volumeSound;
        }
    }

    [Serializable]
    public class StyleData
    {
        public string Id;
        public ShowItemStyle Type;
    }
}

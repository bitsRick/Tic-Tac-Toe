using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player
{
    [Serializable]
    public class ProfileData
    {
        [JsonProperty("Sb")] public StyleData Board;
        [JsonProperty("Xs")] public StyleData X;
        [JsonProperty("Os")] public StyleData O;
        [JsonProperty("As")] public AudioSetting AudioSetting;
        [JsonProperty("Sc")] public int Score;
        [JsonProperty("Sx")]public int SoftValueX;
        [JsonProperty("So")]public int SoftValueO;
        [JsonProperty("Nk")] public string Nick;
        [JsonProperty("Sd")]public List<StyleData> ShopPlayerData;


        public ProfileData(string nick)
        {
            Nick = nick;
            SoftValueX = 100;
            SoftValueO = 100;
            Score = 0;

            ShopPlayerData = new List<StyleData>()
            {
                new(){Id = RuntimeConstants.StyleData.DefaultO,Type = ShowItemStyle.O},
                new(){Id = RuntimeConstants.StyleData.DefaultX,Type = ShowItemStyle.X},
                new (){Id = RuntimeConstants.StyleData.DefaultBoard,Type = ShowItemStyle.Board},
            };

            Board = new StyleData()
            {
                Id = RuntimeConstants.StyleData.DefaultBoard,
                Type = ShowItemStyle.Board
            };
            
            X = new StyleData()
            {
                Id = RuntimeConstants.StyleData.DefaultX,
                Type = ShowItemStyle.X
            };
            
            O = new StyleData()
            {
                Id = RuntimeConstants.StyleData.DefaultO,
                Type = ShowItemStyle.O
            };

            AudioSetting = new AudioSetting(false,false,1f,1f);
        }
    }

    [Serializable]
    public class AudioSetting
    {
       [JsonProperty("Mm")] public bool IsMusicMute;
       [JsonProperty("Sm")] public bool IsSoundMute;
       [JsonProperty("Vm")] public float VolumeMusic;
       [JsonProperty("Vs")] public float VolumeSound;

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
        [JsonProperty("Id")]public string Id;
        [JsonProperty("T")] public ShowItemStyle Type;
    }
}

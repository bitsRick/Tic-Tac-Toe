using System;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities
{
    public static class Constant
    {
        public static class B
        {
            public const float DurationFadeRegistrationScreen = 0.5f;

            public static class Lang
            {
                public const string FolderName = "Lang";
                public const string EngLangFile = "Eng.json";
                public const string RusLangFile = "Rus.json";

                public static Func<int,int> IndexRemove => index => index - 5;
            }
            
            public static class Audio
            {
                public const string AudioClipButtonClick = "Button_click";
                public const string AudioClipBackgroundMeta = "Meta_background";
                public const string AudioConfig = "ConfigSounds";
            }
        }
        
        public static class M
        {
            public static class Asset
            {
                public static class Ui
                {
                    public const string MetaRoot = "MetaRoot";
                } 
                
                public static class Popup
                {
                    public const string Setting = "Popup_Setting";
                    public const string Inventory = "Popup_Inventory";
                    public const string LeaderBoard = "Popup_leaderBoard";
                    public const string Match = "Popup_Match";
                    public const string Shop = "Popup_Shop";
                }
            }
        }
        
        public static class U
        {
            public static class Audio
            {
                public const string MusicMixerExposeName = "MusicValue"; 
                public const string SoundMixerExposeName = "SFXValue"; 
            }
        }
    }
    
    public enum TypePopup
    {
        Setting,
        None,
        LeaderBoard,
        Match,
        Inventory,
        Shop
    }
    
    public enum TypeAsset
    {
        Meta_UI,
        Popup,
        Audio
    }
}
using System;
using UnityEngine.SceneManagement;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime
{
    public static class RuntimeConstants
    {
        public static class Scene
        {
            public static readonly int Bootstrap = SceneUtility.GetBuildIndexByScenePath("0.Bootstrap");
            public static readonly int Loading = SceneUtility.GetBuildIndexByScenePath("1.Loading");
            public static readonly int Meta = SceneUtility.GetBuildIndexByScenePath("2.Meta");
            public static readonly int Match = SceneUtility.GetBuildIndexByScenePath("3.Match");
            public static readonly int Empty = SceneUtility.GetBuildIndexByScenePath("4.Empty");
        }
        
        public static class SaveLoad
        {
            public const string SaveKey = "1024";
        }

        public static class StyleData
        {
            public const string DefaultBoard = "Default_Board";
            public const string DefaultX = "Default_X";
            public const string DefaultO = "Default_O";

            public static string GetDefaultType(ShowItemStyle type)
            {
                return type switch
                {
                    ShowItemStyle.Board => DefaultBoard,
                    ShowItemStyle.X => DefaultX,
                    ShowItemStyle.O => DefaultO,
                    _ => null
                };
            }
        }

        public static class AiScore
        {
            public const int DefaultScore = 100;
            public const int UltraScore = 150;
            public const int SlashUltraScore = 350;
        }

        public static class RegistrationScreen
        {
            public const float DurationFadeRegistrationScreen = 0.5f;
        }

        public static class Lang
        {
            public const string FolderName = "Lang";
            public const string EngLangFile = "Eng.json";
            public const string RusLangFile = "Rus.json";

            public static Func<int, int> IndexRemove => index => index - 5;
        }

        public static class AssetStyle
        {
            public const string DataStyleJson = "DataStyle";
        }

        public static class Match
        {
            public const string BotName = "Bot_Goga";
            public const int SoftValueWin = 10;
            public const int MaxWinMatch = 3;
            public const int MaxCountSetField = 8;
            public const int PlayerAction = 0;
            public const int BotAction = 2;
            public const int Score = 10;
        }

        public static class UiRoot
        {
            public const string MetaRoot = "MetaRoot";
            public const string MatchRoot = "MatchRoot";
        }

        public static class Popup
        {
            public const string Setting = "Popup_Setting";
            public const string Inventory = "Popup_Inventory";
            public const string LeaderBoard = "Popup_leaderBoard";
            public const string Match = "Popup_Match";
            public const string Shop = "Popup_Shop";
            public const string StartMatchViewAction = "Popup_StartMatch";
            public const string WinLose = "Popup_WinLose";
            public const string ShopElementBuyPrefab = "Shop_Element_Buy-Prefab";
            public const string InventoryElementStylePrefab = "Inventory_Element_Style-Prefab";
            public const string PoolElementUi = nameof(PoolElementUi);
        }

        public static class Audio
        {
            public const string MusicMixerExposeName = "MusicValue";
            public const string SoundMixerExposeName = "SFXValue";
            public const string AudioConfig = "ConfigSounds";
        }
    }


    public enum TypeValueChange
    {
        Sound,
        Music
    }
    
    public enum TypeAsset
    {
        Meta_Root_Ui,
        Match_Root_Ui,
        Popup,
        Audio,
        Json,
        Sprite,
        Elements
    }

    public enum ShowItemStyle
    {
        Board,
        X,
        O
    }


    public enum TypePlayingField
    {
        None,
        X,
        O
    }

    public enum MatchMode
    {
        Pause,
        PlayerAction,
        BotAction
    }

    public enum PositionElementWin
    {
        None,

        HorizontalTopLine,
        HorizontalMiddleLine,
        HorizontalBottomLine,

        VerticalLeftLine,
        VerticalCenterLine,
        VerticalRightLine,

        Slash,
        Backslash
    }

    public enum PositionElementToField
    {
        TopLeft = 0,
        TopCenter = 1,
        TopRight = 2,
        MiddleLeft = 3,
        MiddleCenter = 4,
        MiddleRight = 5,
        BottomLeft = 6,
        BottomCenter = 7,
        BottomRight = 8,
    }

    public enum StateFlow
    {
        Match,
        Meta
    }

    public enum MatchWin
    {
        None,
        Player,
        Bot
    }
}
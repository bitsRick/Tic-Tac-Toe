namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities
{
    public static class Constant
    {
        public static class B
        {
            public const float DurationFadeRegistrationScreen = 0.5f;
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
    }
    
    public enum TypePopup
    {
        Setting,
        None
    }
}
using System;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language
{
    [Serializable]
    public class Lang:Singleton<Lang>
    {
        public Ui UI = new Ui();

        [Serializable]
        public class Ui
        {
            [Serializable]
            public class RegistrationScreen
            {
                public string Language = "Язык";
                public string Registration = "Регистрация";
                public string Nick = "Ник";
                public string NickWrite = "Введите Ник";
                public string Enter = "Вход";
            }

            [Serializable]
            public class LoadingScreen
            {
                public string Name = "Крестик и нолики";
                public string Loading = "Загрузка...";
            }

            [Serializable]
            public class Popup
            {
                [Serializable]
                public class Setting
                {
                    public string Name = "Настройки";
                    public string Sound = "Звуки";
                    public string Music = "Музыка";
                }

                [Serializable]
                public class LeaderBoard
                {
                    public string NameForm = "Таблица Лидеров";
                }

                [Serializable]
                public class Inventory
                {
                    public string Header = "Инвентарь стилей";
                    public string BoardButton = "Доска";
                    public string StyleEnter = "Применить";
                }

                [Serializable]
                public class Shop
                {
                    public string Header = "Маназин";
                    public string BoardButton = "Доска";
                    public string Sell = "Куплено";
                    public string Buy = "Купить";
                }

                [Serializable]
                public class MatchPopup
                {
                    public string VsBot = "Игрок Vs Бот";
                }

                [Serializable]
                public class CharacterStartMatchPopup
                {
                    public string FirstAction = "Первым ходит:";
                }
                
                [Serializable]
                public class WinLosePopup
                {
                    public string Win = "Вы победили!";
                    public string Lose = "Вы Проиграли!";
                    public string Reward = "Награда";
                }

                public Setting SETTING = new Setting();
                public LeaderBoard LEADER_BOARD = new ();
                public Inventory INVENTORY = new ();
                public Shop SHOP = new ();
                public MatchPopup MATCH = new ();
                public CharacterStartMatchPopup CHARACTER_START_MATCH = new CharacterStartMatchPopup();
                public WinLosePopup WIN_LOSE = new WinLosePopup();
            }

            public RegistrationScreen REGISTRATION_SCREEN = new RegistrationScreen();
            public LoadingScreen LOADING_SCREEN = new LoadingScreen();
            public Popup POPUP = new ();
        }
    }
}
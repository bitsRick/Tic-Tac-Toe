using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.TopInformation;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Core;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View
{
    public class MatchUiRoot:MonoBehaviour,IView,ILoadUnit
    {
        [Header("Контейнер игрового поля")]
        [SerializeField] private PlayingField _playingField;
        [Header("Настройки")]
        [SerializeField] private Button _setting;
        [Header("Визуальнаые данные игроков")]
        [SerializeField] private DataMatchPlayerUi _playerVisualDataLeft;
        [SerializeField] private DataMatchPlayerUi _playerVisualDataRight;
        private BotMatchData _botMatchData;
        private PlayerMatchData _playerMatchData;

        // public void Constructor(BotMatchData botMatchData,PlayerMatchData playerMatchData)
        // {
        //     _playerMatchData = playerMatchData;
        //     _botMatchData = botMatchData;
        // }
        
        public async UniTask Load()
        {
            //await _playingField.Load();



            await Show();
            await UniTask.CompletedTask;
        }

        public UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public UniTask Hide()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}
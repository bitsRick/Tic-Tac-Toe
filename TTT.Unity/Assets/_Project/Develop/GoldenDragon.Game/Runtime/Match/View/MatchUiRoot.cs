using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.TopInformation;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View
{
    public class MatchUiRoot:MonoBehaviour,ILoadUnit
    {
        [Header("Контейнер игрового поля")]
        [SerializeField] private GameObject _playingField;
        [Header("Настройки")]
        [SerializeField] private Button _setting;
        [Header("Визуальнаые данные игроков")]
        [SerializeField] private DataMatchPlayerUi _playerVisualDataLeft;
        [SerializeField] private DataMatchPlayerUi _playerVisualDataRight;
        
        public UniTask Load()
        {
            return UniTask.CompletedTask;
        }
    }
}
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;
using VContainer;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service
{
    public class SaveLoadService:ILoadUnit
    {
        private const string Key = "1024";
        private IPlayerProgress _playerProgress;
        private bool _isSaveData = false;
        
        public PlayerData PlayerData => _playerProgress.PlayerData;
        public Subject<Unit> OnPlayerDataChanged = new Subject<Unit>();

        [Inject]
        public SaveLoadService(IPlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
        }

        public UniTask Load()
        {
            Log.Loading.D($"{nameof(SaveLoadService)}",$" is loading ...");

            var playerDataCode64 = PlayerPrefs.GetString(Key);
            var encodePlayerData = Coding.GetEncodingBase64(playerDataCode64);
            var convertPlayerData = JsonConvert.DeserializeObject<PlayerData>(encodePlayerData);

            if (convertPlayerData == null)
            {
                Log.Loading.D($"{nameof(SaveLoadService)}","[Player Data]:Empty");
            }
            else
            {
                Log.Loading.D($"{nameof(SaveLoadService)}","[Player Data]:Load");
                _playerProgress.PlayerData = convertPlayerData;
            }

            OnPlayerDataChanged.Subscribe((_) =>
            {
                SetData();
            });
            
            return UniTask.CompletedTask;
        }

        public UniTask SaveProgress(bool isFastSave = false)
        {
            if (isFastSave == false)
                if (_isSaveData == false)
                {
                    Log.Boot.D($"{nameof(SaveLoadService)}",$" is not write data");
                    return UniTask.CompletedTask;
                }

            Log.Boot.D($"{nameof(SaveLoadService)}",$" is save progress");

            var playerDataJson = JsonConvert.SerializeObject(_playerProgress.PlayerData);
            var code64PlayerData = Coding.GetCodingBase64(playerDataJson);
            
            PlayerPrefs.SetString(Key,code64PlayerData);
            _isSaveData = false;
            return UniTask.CompletedTask;
        }

        public async UniTask CreateNewData(string name)
        {
            _playerProgress.PlayerData = new PlayerData(name);
            SetData();
            await SaveProgress();
            
            Log.Boot.D($"{nameof(SaveLoadService)}",$" Create New Player Progress");
        }

        private void SetData() => _isSaveData = true;
    }
}
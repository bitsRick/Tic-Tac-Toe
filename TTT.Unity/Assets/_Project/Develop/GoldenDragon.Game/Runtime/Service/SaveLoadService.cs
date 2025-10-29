using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using Newtonsoft.Json;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service
{
    public class SaveLoadService:ILoadUnit
    {
        private const string Key = "1024";
        
        private IPlayerProgress _playerProgress;

        public SaveLoadService(IPlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
        }

        public UniTask Load()
        {
            Log.Loading.D($"{nameof(SaveLoadService)} is loading ...");

            var playerDataCode64 = PlayerPrefs.GetString(Key);
            var encodePlayerData = Coding.GetEncodingBase64(playerDataCode64);
            var convertPlayerData = JsonConvert.DeserializeObject<PlayerData>(encodePlayerData);

            if (convertPlayerData == null)
            {
                Log.Loading.D("[Player Data]:Empty");
            }
            else
            {
                Log.Loading.D("[Player Data]:Load");
                _playerProgress.PlayerData = convertPlayerData;
            }
            
            return UniTask.CompletedTask;
        }

        public UniTask SaveProgress()
        {
            Log.Loading.D($"{nameof(SaveLoadService)} is save progress");

            var playerDataJson = JsonConvert.SerializeObject(_playerProgress.PlayerData);
            var code64PlayerData = Coding.GetCodingBase64(playerDataJson);
            
            PlayerPrefs.SetString(Key,code64PlayerData);
            
            return UniTask.CompletedTask;
        }

        public async UniTask CreateNewData(string name)
        {
            _playerProgress.PlayerData = new PlayerData(name);
            await SaveProgress();
        }
    }
}
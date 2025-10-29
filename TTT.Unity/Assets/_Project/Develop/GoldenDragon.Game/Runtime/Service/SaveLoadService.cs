using System;
using System.Text;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using Newtonsoft.Json;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service
{
    public class SaveLoadService:ILoadUnit
    {
        private IPlayerProgress _playerProgress;
        private PlayerData _playerData;
        private string _key = "1024";
        
        public SaveLoadService(IPlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
        }

        public UniTask Load()
        {
            Log.Loading.D($"{nameof(SaveLoadService)} is loading ...");

            if (_playerProgress.PlayerData == null)
            {
                Log.Loading.D("[Player Data]:Empty");
            }
            
            return UniTask.CompletedTask;
        }

        public UniTask SaveProgress()
        {
            Log.Loading.D($"{nameof(SaveLoadService)} is save progress");

            var playerDataJson = JsonConvert.SerializeObject(_playerData);
            
            byte[] bytes = Encoding.UTF8.GetBytes(playerDataJson);
            var convert = Convert.ToBase64String(bytes);
            
            
            return UniTask.CompletedTask;
        }

        public async UniTask CreateNewData(string name)
        {
            _playerProgress.PlayerData = new PlayerData(name);
            await SaveProgress();
        }
    }
}
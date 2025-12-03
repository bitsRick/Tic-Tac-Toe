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
        private readonly IPlayerProfile _playerProfile;
        private bool _isSaveData;
        
        public ProfileData profileData => _playerProfile.profileData;
        public Subject<Unit> OnPlayerDataChanged = new Subject<Unit>();

        [Inject]
        public SaveLoadService(IPlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
        }

        public UniTask Load()
        {
            Log.Loading.D($"{nameof(SaveLoadService)}",$" is loading ...");

            string playerDataCode64 = PlayerPrefs.GetString(RuntimeConstants.SaveLoad.SaveKey);
            string encodePlayerData = Coding.GetEncodingBase64(playerDataCode64);
            ProfileData convertProfileData = JsonConvert.DeserializeObject<ProfileData>(encodePlayerData);

            if (convertProfileData == null)
            {
                Log.Loading.D($"{nameof(SaveLoadService)}","[Player Data]:Empty");
            }
            else
            {
                Log.Loading.D($"{nameof(SaveLoadService)}","[Player Data]:Load");
                _playerProfile.profileData = convertProfileData;
            }

            OnPlayerDataChanged.Subscribe((_) =>
            {
                SetDirty();
            });
            
            return UniTask.CompletedTask;
        }

        public UniTask SaveProgress(bool isFastSave = false)
        {
            if (isFastSave == false)
                if (_isSaveData == false)
                {
                    Log.Boot.D($"{nameof(SaveLoadService)}",$" Is not write data");
                    return UniTask.CompletedTask;
                }

            Log.Boot.D($"{nameof(SaveLoadService)}",$" Save progress");

            string playerDataJson = JsonConvert.SerializeObject(_playerProfile.profileData);
            string code64PlayerData = Coding.GetCodingBase64(playerDataJson);
            
            PlayerPrefs.SetString(RuntimeConstants.SaveLoad.SaveKey,code64PlayerData);
            _isSaveData = false;
            return UniTask.CompletedTask;
        }

        public async UniTask CreateNewData(string name)
        {
            _playerProfile.profileData = new ProfileData(name);
            SetDirty();
            await SaveProgress();
            
            Log.Boot.D($"{nameof(SaveLoadService)}",$" Create New Player Progress");
        }

        private void SetDirty() => _isSaveData = true;
    }
}
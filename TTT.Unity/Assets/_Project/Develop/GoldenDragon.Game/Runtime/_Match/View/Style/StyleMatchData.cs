using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.Style
{
    public class StyleMatchData:IDisposableLoadUnit
    {
        private readonly AssetService _assetService;
        private IPlayerProfile _playerProfile;

        public Sprite X { get; private set; } 
        public Sprite O{ get; private set;}
        public Sprite Board { get; private set;}

        public bool IsNotDefaultBoard;
        public bool IsNotDefaultX;
        public bool IsNotDefaultO;

        public StyleMatchData(AssetService assetService,IPlayerProfile playerProfile)
        {
            _playerProfile = playerProfile;
            _assetService = assetService;
        }

        public async UniTask Load()
        {
            var data = _playerProfile.profileData;

            Board = await _assetService.Load.GetAssetAsync<Sprite>(TypeAsset.Sprite,data.Board.Id);
            X = await _assetService.Load.GetAssetAsync<Sprite>(TypeAsset.Sprite,data.X.Id);
            O = await _assetService.Load.GetAssetAsync<Sprite>(TypeAsset.Sprite,data.O.Id);

            IsNotDefaultBoard = data.Board.Id != RuntimeConstants.StyleData.DefaultBoard;
            IsNotDefaultX = data.X.Id != RuntimeConstants.StyleData.DefaultX;
            IsNotDefaultO  = data.O.Id != RuntimeConstants.StyleData.DefaultO;
            

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _assetService.Release.ReleaseAsset<Sprite>(TypeAsset.Sprite,_playerProfile.profileData.Board.Id);
            _assetService.Release.ReleaseAsset<Sprite>(TypeAsset.Sprite,_playerProfile.profileData.X.Id);
            _assetService.Release.ReleaseAsset<Sprite>(TypeAsset.Sprite,_playerProfile.profileData.O.Id);
        }
    }
}
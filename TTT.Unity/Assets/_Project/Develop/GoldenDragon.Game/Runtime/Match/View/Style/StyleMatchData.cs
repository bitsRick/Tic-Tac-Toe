using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UnityEngine;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.Style
{
    public class StyleMatchData:ILoadUnit<IPlayerProgress>
    {
        private AssetService _assetService;

        public Sprite X { get; private set; } 
        public Sprite O{ get; private set;}
        public Sprite Board { get; private set;}

        public StyleMatchData(AssetService assetService) => 
            _assetService = assetService;

        public async UniTask Load(IPlayerProgress playerProgress)
        {
            var data = playerProgress.PlayerData;

            Board = await _assetService.Load.GetAssetAsync<Sprite>(TypeAsset.Sprite,data.Board.Id);
            X = await _assetService.Load.GetAssetAsync<Sprite>(TypeAsset.Sprite,data.X.Id);
            O = await _assetService.Load.GetAssetAsync<Sprite>(TypeAsset.Sprite,data.O.Id);
        }
    }
}
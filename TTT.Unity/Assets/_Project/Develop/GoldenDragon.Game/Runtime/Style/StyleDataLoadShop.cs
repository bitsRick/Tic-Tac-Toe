using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using Newtonsoft.Json;
using UnityEngine;
using VContainer;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Style
{
    public class StyleDataLoadShop:IDisposableLoadUnit
    {
        private StyleData[] _data;
        private AssetService _assetService;

        [Inject]
        public StyleDataLoadShop(AssetService assetService)
        {
            _assetService = assetService;
        }
        
        public UniTask Load()
        {
            TextAsset jsonText = _assetService.Load.GetAsset<TextAsset>(TypeAsset.Json,RuntimeConstants.AssetStyle.DataStyleJson);
            _data = JsonConvert.DeserializeObject<StyleData[]>(jsonText.text);

            if (_data == null)
            {
                Log.Meta.W(nameof(StyleDataLoadShop),$"Not loading json:[{RuntimeConstants.AssetStyle.DataStyleJson}]");
                return UniTask.CompletedTask;
            }

            foreach (StyleData styleData in _data)
            {
                Sprite sprite = _assetService.Load.GetAsset<Sprite>(TypeAsset.Sprite,styleData.Id);

                if (sprite == null)
                {
                    Log.Meta.W(nameof(StyleDataLoadShop),$"Not loading sprite:[{styleData.Id}]");
                    continue;
                }

                styleData.Sprite = sprite;
            }
            
            return UniTask.CompletedTask;
        }
        
        
        public StyleData[] GetData()
        {
            if (_data == null)
            {
                Log.Meta.W(nameof(StyleDataLoadShop),$"DataStyle null");
                return null;
            }
            
            return _data;
        }
        
        public void Dispose()
        {
            if (_data == null)
                return;

            foreach (StyleData data in _data)
            {
                data.Sprite = null;
                data.Id = null;
                data.Type = null;
            }

            _data = null;
            _assetService.Release.ReleaseAsset<TextAsset>(TypeAsset.Json, RuntimeConstants.AssetStyle.DataStyleJson);
        }
    }
}
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service.Asset;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using Newtonsoft.Json;
using UnityEngine;
using VContainer;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Style
{
    public class StyleDataLoad:ILoadUnit
    {
        private AssetLoad _assetLoad;
        private StyleData[] _data;

        [Inject]
        public StyleDataLoad(AssetLoad assetLoad)
        {
            _assetLoad = assetLoad;
        }
        
        public UniTask Load()
        {
            TextAsset jsonText = _assetLoad.GetAsset<TextAsset>(TypeAsset.Json,Constant.L.Asset.DataStyleJson);
            _data = JsonConvert.DeserializeObject<StyleData[]>(jsonText.text);

            if (_data == null)
            {
                Log.Meta.W($"Not loading json:[{Constant.L.Asset.DataStyleJson}]");
                return UniTask.CompletedTask;
            }

            foreach (StyleData styleData in _data)
            {
                Sprite sprite = _assetLoad.GetAsset<Sprite>(TypeAsset.Sprite,styleData.Id);

                if (sprite == null)
                {
                    Log.Meta.W($"Not loading sprite:[{styleData.Id}]");
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
                Log.Meta.W($"DataStyle null");
                return null;
            }
            
            return _data;
        }
    }
}
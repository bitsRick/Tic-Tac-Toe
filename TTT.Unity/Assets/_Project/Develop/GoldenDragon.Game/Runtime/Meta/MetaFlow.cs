using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon.Units;
using UnityEngine;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta
{
    public class MetaFlow:IStartable
    {
        private readonly SceneManager _sceneManager;
        private readonly LoadingService _loadingService;
        private readonly LoadingView _loadingView;
        private AssetService _assetService;

        public MetaFlow(SceneManager sceneManager, LoadingService loadingService,LoadingView loadingView, AssetService assetService)
        {
            _assetService = assetService;
            _loadingView = loadingView;
            _sceneManager = sceneManager;
            _loadingService = loadingService;
        }

        public async void Start()
        {
            // Fabrica
            var rootHud = _assetService.Load.GetAssetAsync<GameObject>(TypeAsset.Meta_UI, Constant.M.Asset.Ui.MetaRoot).GetComponent<ViewHudRoot>();
            await _assetService.Install.InstallToRoot(rootHud);
            
            await _loadingService.BeginLoading(new FooLoadingUnit(3));
            
            await rootHud.Initialized();
            await _loadingView.Hide();
        }

        public async void StartMatch()
        {
            await _sceneManager.LoadScene(RuntimeConstants.Scene.Core);
        }
    }
}
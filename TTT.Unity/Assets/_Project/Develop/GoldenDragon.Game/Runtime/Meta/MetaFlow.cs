using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta
{
    public class MetaFlow:IStartable
    {
        private readonly SceneManager _sceneManager;
        private readonly LoadingService _loadingService;
        private readonly LoadingView _loadingView;
        private AssetService _assetService;
        private FactoryMetaUi _factoryMetaUi;
        private AudioService _audioService;

        public MetaFlow(SceneManager sceneManager, LoadingService loadingService,LoadingView loadingView,
            AssetService assetService,FactoryMetaUi factoryMetaUi,AudioService audioService)
        {
            _audioService = audioService;
            _factoryMetaUi = factoryMetaUi;
            _assetService = assetService;
            _loadingView = loadingView;
            _sceneManager = sceneManager;
            _loadingService = loadingService;
        }

        public async void Start()
        {
            await _audioService.LoadAudioMeta();
            
            MetaRoot metaRoot = _factoryMetaUi.CreateMetaRoot<MetaRoot>();
            await metaRoot.Initialized();
            
            //await _loadingService.BeginLoading(new FooLoadingUnit(3));
            
            await _loadingView.Hide();
            _audioService.PlayBackground(Constant.B.Audio.AudioClipBackgroundMeta);
        }

        public async void StartMatch()
        {
            await _sceneManager.LoadScene(RuntimeConstants.Scene.Core);
        }
    }
}
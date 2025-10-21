using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Style;
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
        private AudioPlayer _audioPlayer;
        private StyleDataLoad _styleDataLoad;
        private Model _modelMetaRoot;

        public MetaFlow(SceneManager sceneManager, LoadingService loadingService,LoadingView loadingView,
            AssetService assetService,FactoryMetaUi factoryMetaUi,AudioPlayer audioPlayer,StyleDataLoad styleDataLoad,Model modelMetaRoot)
        {
            _modelMetaRoot = modelMetaRoot;
            _styleDataLoad = styleDataLoad;
            _audioPlayer = audioPlayer;
            _factoryMetaUi = factoryMetaUi;
            _assetService = assetService;
            _loadingView = loadingView;
            _sceneManager = sceneManager;
            _loadingService = loadingService;
        }

        public async void Start()
        {
            await _loadingService.BeginLoading(_styleDataLoad);
            MetaRoot metaRoot = _factoryMetaUi.CreateMetaRoot<MetaRoot>();
            await _modelMetaRoot.Initialized(metaRoot,_styleDataLoad.GetData());
            await metaRoot.Initialized();
            
            await _loadingView.Hide();
            _audioPlayer.MetaBackground();
        }

        public async void StartMatch()
        {
            await _sceneManager.LoadScene(RuntimeConstants.Scene.Core);
        }
    }
}
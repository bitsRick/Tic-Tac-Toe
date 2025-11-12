using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match
{
    public class MatchFlow:IStartable
    {
        private readonly SceneManager _sceneManager;
        private readonly LoadingService _loadingService;
        private LoadingView _loadingView;
        private ProviderUiFactory _providerUiFactory;

        public MatchFlow(SceneManager sceneManager,
            LoadingService loadingService,LoadingView loadingView,ProviderUiFactory providerUiFactory)
        {
            _providerUiFactory = providerUiFactory;
            _loadingView = loadingView;
            _sceneManager = sceneManager;
            _loadingService = loadingService;
        }

        public async void Start()
        {
            MatchUiRoot matchUi =
                _providerUiFactory.FactoryUi.CreateRootUi<MatchUiRoot>(TypeAsset.Match_Root_Ui,
                    Constant.M.Asset.Ui.MatchRoot);
            
            await _loadingService.BeginLoading(matchUi);
            await _loadingView.Hide();
        }
    }
}
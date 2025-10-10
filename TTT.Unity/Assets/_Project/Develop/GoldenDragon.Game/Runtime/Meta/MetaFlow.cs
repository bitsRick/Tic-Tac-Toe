using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon.Units;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta
{
    public class MetaFlow:IStartable
    {
        private readonly SceneManager _sceneManager;
        private readonly LoadingService _loadingService;
        private LoadingView _loadingView;

        public MetaFlow(SceneManager sceneManager, LoadingService loadingService,LoadingView loadingView)
        {
            _loadingView = loadingView;
            _sceneManager = sceneManager;
            _loadingService = loadingService;
        }

        public async void Start()
        {
            await _loadingService.BeginLoading(new FooLoadingUnit(3));

            await _loadingView.Hide();
            // _sceneManager.LoadScene(RuntimeConstants.Scene.Core).Forget();
        }
    }
}
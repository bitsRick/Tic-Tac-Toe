using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon.Units;
using VContainer.Unity;

namespace GoldenDragon
{
    public class BootstrapFlow:IStartable
    {
        private LoadingService _loadingService;
        private SceneManager _sceneManager;

        public BootstrapFlow(LoadingService loadingService, SceneManager sceneManager)
        {
            _sceneManager = sceneManager;
            _loadingService = loadingService;
        }

        public async void Start()
        {
            FooLoadingUnit fooLoadUnit = new FooLoadingUnit();
            await _loadingService.BeginLoading(fooLoadUnit);

            _sceneManager.LoadScene(RuntimeConstants.Scene.Loading).Forget();
        }
    }
}
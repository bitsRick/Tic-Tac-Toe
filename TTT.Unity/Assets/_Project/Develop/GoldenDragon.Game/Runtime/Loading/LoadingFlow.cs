using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Loading
{
    public class LoadingFlow:IStartable
    {
        private readonly SceneManager _sceneManager;
        private readonly LoadingService _loadingService;

        public LoadingFlow(SceneManager sceneManager, LoadingService loadingService)
        {
            _sceneManager = sceneManager;
            _loadingService = loadingService;
        }

        public async void Start()
        {
           await _sceneManager.LoadScene(RuntimeConstants.Scene.Meta);
        }
    }
}
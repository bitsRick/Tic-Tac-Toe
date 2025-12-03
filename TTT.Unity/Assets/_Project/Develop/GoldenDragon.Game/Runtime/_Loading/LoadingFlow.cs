using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Loading
{
    public class LoadingFlow:IStartable
    {
        private readonly SceneManager _sceneManager;

        public LoadingFlow(SceneManager sceneManager) => 
            _sceneManager = sceneManager;

        public async void Start()
        {
           await _sceneManager.LoadScene(RuntimeConstants.Scene.Meta);
        }
    }
}
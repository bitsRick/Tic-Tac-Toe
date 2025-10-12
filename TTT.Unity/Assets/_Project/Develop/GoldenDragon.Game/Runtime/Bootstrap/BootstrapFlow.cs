using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using VContainer.Unity;

namespace GoldenDragon
{
    public class BootstrapFlow:IStartable
    {
        private LoadingService _loadingService;
        private SceneManager _sceneManager;
        private RegistrationScreen _registrationScreen;
        private SaveLoadService _saveLoadService;

        public BootstrapFlow(LoadingService loadingService, SceneManager sceneManager,RegistrationScreen registrationScreen,SaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _registrationScreen = registrationScreen;
            _sceneManager = sceneManager;
            _loadingService = loadingService;
        }

        public async void Start()
        {
            await _loadingService.BeginLoading(_saveLoadService);

            if (_saveLoadService.Data == null)
            {
                _registrationScreen.Initialized(this);
                await _registrationScreen.Show();
            }
            else
            {
                _sceneManager.LoadScene(RuntimeConstants.Scene.Loading).Forget();
            }
        }

        public async void SetRegistration(string inputFieldText)
        {
            await _registrationScreen.Hide();
            await _saveLoadService.CreateNewData(inputFieldText);
            await UniTask.Delay(Constant.B.DelayTransitionNextScene);   
            await StartLoading();
        }

        private async UniTask StartLoading()
        {
            await _sceneManager.LoadScene(RuntimeConstants.Scene.Loading);
        }
    }
}
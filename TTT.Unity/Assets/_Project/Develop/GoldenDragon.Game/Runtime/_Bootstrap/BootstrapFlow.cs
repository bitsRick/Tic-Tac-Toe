using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime._Bootstrap.RegistrationView;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using VContainer.Unity;

namespace GoldenDragon
{
    public class BootstrapFlow:IStartable
    {
        private readonly LoadingService _loadingService;
        private readonly SceneManager _sceneManager;
        private readonly RegistrationScreen _registrationScreen;
        private readonly SaveLoadService _saveLoadService;
        private readonly LoadingView _loadingView;
        private readonly AudioService _audioService;
        private readonly IPlayerProgress _progress;

        public BootstrapFlow(LoadingService loadingService, SceneManager sceneManager,
            RegistrationScreen registrationScreen,SaveLoadService saveLoadService,
            LoadingView loadingView,IPlayerProgress progress,AudioService audioService)
        {
            _progress = progress;
            _loadingView = loadingView;
            _saveLoadService = saveLoadService;
            _registrationScreen = registrationScreen;
            _sceneManager = sceneManager;
            _loadingService = loadingService;
            _audioService = audioService;
        }

        public async void Start()
        {
            await _loadingService.BeginLoading(_saveLoadService);
            await _loadingService.BeginLoading(_audioService);

            AudioPlayer.Construct(_audioService,_progress);
            AudioPlayer.Initialized();
            
            await _loadingView.Initialized();

            if (_saveLoadService.PlayerData == null)
            {
                await _registrationScreen.Initialized(this);
                await _registrationScreen.Show();
            }
            else
            {
                await StartLoading();
            }
        }

        public async void SetRegistration(string namePlayer)
        {
            await _registrationScreen.Hide();
            await _saveLoadService.CreateNewData(namePlayer);
            await StartLoading();
        }

        private async UniTask StartLoading()
        {
            await _sceneManager.LoadScene(RuntimeConstants.Scene.Loading);
        }
    }
}
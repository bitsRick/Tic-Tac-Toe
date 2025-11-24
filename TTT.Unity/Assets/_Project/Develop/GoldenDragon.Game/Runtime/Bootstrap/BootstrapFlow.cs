using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
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
        private LoadingView _loadingView;
        private AudioService _audioService;
        private IPlayerProgress _progress;
        private AssetService _assetService;

        public BootstrapFlow(LoadingService loadingService, SceneManager sceneManager,
            RegistrationScreen registrationScreen,SaveLoadService saveLoadService,
            LoadingView loadingView,AudioService audioService,IPlayerProgress progress,AssetService assetService)
        {
            _assetService = assetService;
            _progress = progress;
            _audioService = audioService;
            _loadingView = loadingView;
            _saveLoadService = saveLoadService;
            _registrationScreen = registrationScreen;
            _sceneManager = sceneManager;
            _loadingService = loadingService;
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
                _sceneManager.LoadScene(RuntimeConstants.Scene.Loading).Forget();
            }
        }

        public async void SetRegistration(string inputFieldText)
        {
            await _registrationScreen.Hide();
            await _saveLoadService.CreateNewData(inputFieldText);
            await StartLoading();
        }

        private async UniTask StartLoading()
        {
            await _sceneManager.LoadScene(RuntimeConstants.Scene.Loading);
        }
    }
}
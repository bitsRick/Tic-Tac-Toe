using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
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
        private Lang _lang;
        private AudioService _audioService;
        private AudioPlayer _audioPlayer;

        public BootstrapFlow(LoadingService loadingService, SceneManager sceneManager,
            RegistrationScreen registrationScreen,SaveLoadService saveLoadService,
            LoadingView loadingView,Lang lang,AudioService audioService,AudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
            _audioService = audioService;
            _lang = lang;
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
            
            await _loadingView.Initialized(_lang);

            if (_saveLoadService.Data == null)
            {
                _registrationScreen.Construct(_lang,_audioPlayer);
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
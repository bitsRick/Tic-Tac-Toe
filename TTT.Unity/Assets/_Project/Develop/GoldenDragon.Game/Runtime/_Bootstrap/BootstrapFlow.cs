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
        private readonly IPlayerProfile _profile;

        public BootstrapFlow(LoadingService loadingService, SceneManager sceneManager,
            RegistrationScreen registrationScreen,SaveLoadService saveLoadService,
            LoadingView loadingView,IPlayerProfile profile,AudioService audioService)
        {
            _profile = profile;
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

            AudioPlayer.S.Resolve(_audioService,_profile);
            AudioPlayer.S.Initialized();
            
            await _loadingView.Initialized();

            if (_saveLoadService.profileData == null)
            {
                _registrationScreen.Resolve(this);
                await _registrationScreen.Initialized();
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
using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Ai;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.Style;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SessionData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match
{
    public class MatchFlow:IStartable,ITickable,IDisposable
    {
        private readonly SceneManager _sceneManager;
        private readonly LoadingService _loadingService;
        private readonly LoadingView _loadingView;
        private readonly ProviderUiFactory _providerUiFactory;
        private readonly SessionDataMatch _sessionDataMatch;
        private readonly RoundManager _roundManager;
        private readonly ModuleView _moduleView;
        private readonly StyleMatchData _styleMatchData;
        private readonly ModulePlayingField _modulePlayingField;
        private readonly IPlayerProfile _playerProfile;
        private readonly IAi _utilityAi;
        private WinService _winService;
        private CharacterMatchData _botMatchDataData;
        private CharacterMatchData _playerMatchDataData;
        private MatchUiRoot _matchUi;
        private PopupService _popupService;

        public MatchFlow(
            SceneManager sceneManager,
            LoadingService loadingService,
            LoadingView loadingView,
            ProviderUiFactory providerUiFactory,
            SessionDataMatch sessionDataMatch,
            IPlayerProfile playerProfile,
            IAi utilityAi,
            RoundManager roundManager,
            ModuleView moduleView,
            ModulePlayingField modulePlayingField,StyleMatchData styleMatchData,PopupService popupService)
        {
            _popupService = popupService;
            _styleMatchData = styleMatchData;
            _modulePlayingField = modulePlayingField;
            _moduleView = moduleView;
            _utilityAi = utilityAi;
            _roundManager = roundManager;
            _playerProfile = playerProfile;
            _sessionDataMatch = sessionDataMatch;
            _providerUiFactory = providerUiFactory;
            _loadingView = loadingView;
            _sceneManager = sceneManager;
            _loadingService = loadingService;
        }

        public async void Start()
        {
            _botMatchDataData = new CharacterMatchData(RuntimeConstants.Match.BotName,_sessionDataMatch.BotType,true);
            _playerMatchDataData = new CharacterMatchData(_playerProfile.profileData.Nick,_sessionDataMatch.PlayerType,false);
            
            _matchUi = _providerUiFactory.FactoryUi.CreateRootUi<MatchUiRoot>(TypeAsset.Match_Root_Ui,RuntimeConstants.UiRoot.MatchRoot);
            _matchUi.Constructor(_popupService,_moduleView,_modulePlayingField,_botMatchDataData,_playerMatchDataData,this);
            await _matchUi.Initialized();

            await _loadingService.BeginLoading(_styleMatchData, _playerProfile);
            await _loadingService.BeginLoading(_moduleView);
            await _loadingService.BeginLoading(_modulePlayingField,_styleMatchData);
            await _loadingService.BeginLoading(_matchUi);
            await _loadingService.BeginLoading(_utilityAi,_matchUi);

            _winService = new WinService(_botMatchDataData, _playerMatchDataData, _matchUi);
            await _loadingService.BeginLoading(_winService);
            
            await _roundManager.Resolve(_utilityAi,_playerMatchDataData,_botMatchDataData,_winService,new RoundRandom());
            _roundManager.InitializedFirstActionRound();
            _roundManager.InitializedEvent(_matchUi,_modulePlayingField);
            
            _matchUi.Show();
            _matchUi.OpenCharacterStartMatchPopup();
            
            await _loadingView.Hide();
        }

        public void Tick()
        {
            _roundManager.Update();            
        }

        public void NextMatch()
        {
            _loadingView.Show();
            
            _moduleView.Reset();
            _modulePlayingField.Reset();
            _botMatchDataData.Reset();
            _playerMatchDataData.Reset();
            _roundManager.Reset();
            
            _popupService.SetNoClose(false);
            _popupService.Close();
            
            _matchUi.OpenCharacterStartMatchPopup();

            _loadingView.Hide();
        }

        public async void ToMeta()
        {
            _loadingView.Show();
            await _sceneManager.LoadScene(RuntimeConstants.Scene.Meta);
        }
        
        public void Dispose()
        {
            _popupService.Release().Forget();
            _loadingService.DisposableService.Dispose();
        }
    }
}
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SessionData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match
{
    public class MatchFlow:IStartable
    {
        private readonly SceneManager _sceneManager;
        private readonly LoadingService _loadingService;
        private readonly LoadingView _loadingView;
        private readonly ProviderUiFactory _providerUiFactory;
        private SessionDataMatch _sessionDataMatch;
        private PopupService _popupService;
        private IPlayerProgress _playerProgress;
        private AssetService _assetService;
        private SaveLoadService _saveLoadService;
        private RoundManager _roundManager;

        public MatchFlow(SceneManager sceneManager,
            LoadingService loadingService,LoadingView loadingView,
            ProviderUiFactory providerUiFactory,
            SessionDataMatch sessionDataMatch,
            IPlayerProgress playerProgress,
            AssetService assetService,SaveLoadService saveLoadService,RoundManager roundManager)
        {
            _roundManager = roundManager;
            _saveLoadService = saveLoadService;
            _assetService = assetService;
            _playerProgress = playerProgress;
            _sessionDataMatch = sessionDataMatch;
            _providerUiFactory = providerUiFactory;
            _loadingView = loadingView;
            _sceneManager = sceneManager;
            _loadingService = loadingService;
        }

        public async void Start()
        {
            _popupService = new PopupService(_saveLoadService);
            
            CharacterMatch botMatchData = new CharacterMatch(Constant.M.BotName,Support.GetBotTypeFieldAction(_sessionDataMatch.PlayerType()));
            CharacterMatch playerMatchData = new CharacterMatch(_playerProgress.PlayerData.Nick,Support.GetPlayerTypeFieldAction(_sessionDataMatch.PlayerType()));
            
            MatchUiRoot matchUi = _providerUiFactory.FactoryUi.CreateRootUi<MatchUiRoot>(TypeAsset.Match_Root_Ui,Constant.M.Asset.Ui.MatchRoot);
            matchUi.Constructor(botMatchData,playerMatchData,_popupService,_assetService,_providerUiFactory);
            
            await _loadingService.BeginLoading(matchUi);
            
            UtilityAi aiBot =  new UtilityAi(matchUi);
            await aiBot.Load();
            
            _roundManager.Initialized(aiBot,playerMatchData,botMatchData,matchUi);
            
            await _loadingView.Hide();
        }
    }
}
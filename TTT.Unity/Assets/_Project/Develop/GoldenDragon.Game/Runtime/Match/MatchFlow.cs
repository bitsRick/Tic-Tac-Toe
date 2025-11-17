using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SessionData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match
{
    public class MatchFlow:IStartable,ITickable
    {
        private readonly SceneManager _sceneManager;
        private readonly LoadingService _loadingService;
        private readonly LoadingView _loadingView;
        private readonly ProviderUiFactory _providerUiFactory;
        private SessionDataMatch _sessionDataMatch;
        private PopupService _popupService;
        private AssetService _assetService;
        private SaveLoadService _saveLoadService;
        private RoundManager _roundManager;
        private WinService _winService;
        private IAi _utilityAi;
        private IPlayerProgress _playerProgress;

        public MatchFlow(SceneManager sceneManager,
            LoadingService loadingService,LoadingView loadingView,
            ProviderUiFactory providerUiFactory,
            SessionDataMatch sessionDataMatch,
            IPlayerProgress playerProgress,
            AssetService assetService,SaveLoadService saveLoadService,
            RoundManager roundManager,IAi utilityAi)
        {
            _utilityAi = utilityAi;
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
            
            CharacterMatchData botMatchDataData = new CharacterMatchData(Constant.M.BotName,SupportMatchAction.GetBotTypeFieldAction(_sessionDataMatch.PlayerType()),true);
            CharacterMatchData playerMatchDataData = new CharacterMatchData(_playerProgress.PlayerData.Nick,SupportMatchAction.GetPlayerTypeFieldAction(_sessionDataMatch.PlayerType()),false);
            
            MatchUiRoot matchUi = _providerUiFactory.FactoryUi.CreateRootUi<MatchUiRoot>(TypeAsset.Match_Root_Ui,Constant.M.Asset.Ui.MatchRoot);
            matchUi.Constructor(botMatchDataData,playerMatchDataData,_popupService,_assetService,_providerUiFactory,_roundManager);

            await _loadingService.BeginLoading(matchUi);
            await _loadingService.BeginLoading(_utilityAi,matchUi);

            _winService = new WinService(botMatchDataData, playerMatchDataData, matchUi);
            await _loadingService.BeginLoading(_winService);
            
            _roundManager.Initialized(_utilityAi,playerMatchDataData,botMatchDataData,_winService,new RandomRound());
            _roundManager.InitializedFirstActionRound();
            
            matchUi.Show();
            
            _roundManager.Start();
            
            await _loadingView.Hide();
        }

        public void Tick()
        {
            _roundManager.Update();            
        }
    }
}
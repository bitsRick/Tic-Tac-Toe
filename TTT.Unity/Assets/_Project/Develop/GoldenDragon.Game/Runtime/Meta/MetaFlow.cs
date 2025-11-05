using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.InventoryItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.ShopElementItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SessionData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Style;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta
{
    public class MetaFlow:IStartable
    {
        private readonly SceneManager _sceneManager;
        private readonly LoadingService _loadingService;
        private readonly LoadingView _loadingView;
        private AssetService _assetService;
        private AudioService _audioService;
        private StyleDataLoad _styleDataLoad;
        private Model _modelMetaRoot;
        private PoolUiItem<ItemShop> _itemShopPool;
        private PoolUiItem<ItemInventoryStyle> _itemInventoryStyle;
        private MetaProviderFacadeFactory _metaProviderFacadeFactory;
        private SessionDataMatch _sessionDataMatch;

        public MetaFlow(SceneManager sceneManager, LoadingService loadingService,
            LoadingView loadingView,AssetService assetService,
            StyleDataLoad styleDataLoad,Model modelMetaRoot,
            MetaProviderFacadeFactory metaProviderFacadeFactory,SessionDataMatch sessionDataMatch)
        {
            _sessionDataMatch = sessionDataMatch;
            _metaProviderFacadeFactory = metaProviderFacadeFactory;
            _modelMetaRoot = modelMetaRoot;
            _styleDataLoad = styleDataLoad;
            _assetService = assetService;
            _loadingView = loadingView;
            _sceneManager = sceneManager;
            _loadingService = loadingService;
        }

        public async void Start()
        {
            _itemShopPool = new PoolUiItem<ItemShop>(_assetService);
            _itemInventoryStyle = new PoolUiItem<ItemInventoryStyle>(_assetService);
            
            await _loadingService.BeginLoading(_styleDataLoad);
            await _loadingService.BeginLoading(_sessionDataMatch);
            await _loadingService.BeginLoading(_itemShopPool, 
                new DataPullUiItem(
                    Constant.M.Asset.Popup.ShopElementBuyPrefab,
                    Constant.M.Asset.Popup.PoolElementUi,
                    _styleDataLoad.GetData().Length));
            
            await _loadingService.BeginLoading(_itemInventoryStyle, 
                new DataPullUiItem(
                    Constant.M.Asset.Popup.InventoryElementStylePrefab,
                    Constant.M.Asset.Popup.PoolElementUi,
                    _styleDataLoad.GetData().Length));
            
            MetaRoot metaRoot = _metaProviderFacadeFactory.MetaFactoryUi.CreateMetaRoot<MetaRoot>();
            await metaRoot.Initialized();
            await metaRoot.Show();
            
            await _modelMetaRoot.Initialized(metaRoot,_styleDataLoad.GetData(),_itemShopPool,_itemInventoryStyle,this);
            
            await _loadingView.Hide();
            AudioPlayer.MetaBackground();
        }

        public async void StartMatch()
        {
            await _loadingView.Show();
            await _sceneManager.LoadScene(RuntimeConstants.Scene.Core);
        }
    }
}
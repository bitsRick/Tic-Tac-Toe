using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.InventoryItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.ShopElementItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SessionData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Style;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta
{
    public class MetaFlow:IStartable
    {
        private readonly SceneManager _sceneManager;
        private readonly LoadingService _loadingService;
        private readonly LoadingView _loadingView;
        private readonly AssetService _assetService;
        private StyleDataLoad _styleDataLoad;
        private Model _modelMetaRoot;
        private PoolUiItem<ItemShop> _itemShopPool;
        private PoolUiItem<ItemInventoryStyle> _itemInventoryStyle;
        private ProviderUiFactory _providerUiFactory;
        private SessionDataMatch _sessionDataMatch;
        private PopupService _popupService;
        private IPlayerProgress _playerProgress;

        public MetaFlow(SceneManager sceneManager, LoadingService loadingService,
            LoadingView loadingView,AssetService assetService,
            StyleDataLoad styleDataLoad,Model modelMetaRoot,
            ProviderUiFactory providerUiFactory,
            SessionDataMatch sessionDataMatch,PopupService popupService,IPlayerProgress playerProgress)
        {
            _playerProgress = playerProgress;
            _popupService = popupService;
            _sessionDataMatch = sessionDataMatch;
            _providerUiFactory = providerUiFactory;
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
            
            MetaRoot metaRoot = _providerUiFactory.FactoryUi.CreateRootUi<MetaRoot>(TypeAsset.Meta_Root_Ui, Constant.M.Asset.Ui.MetaRoot);
            metaRoot.Constructor(_popupService,_modelMetaRoot,_assetService,_playerProgress,_providerUiFactory);
            
            await metaRoot.Initialized();
            await metaRoot.Show();
            
            await _modelMetaRoot.Initialized(metaRoot,_styleDataLoad.GetData(),_itemShopPool,_itemInventoryStyle,this);
            
            await _loadingView.Hide();
            AudioPlayer.MetaBackground();
        }

        public async void StartMatch()
        {
            await _loadingView.Show();
            await _sceneManager.LoadScene(RuntimeConstants.Scene.Match);
        }
    }
}
using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
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
    public class MetaFlow:IStartable,IDisposable
    {
        private readonly SceneManager _sceneManager;
        private readonly LoadingService _loadingService;
        private readonly LoadingView _loadingView;
        private readonly AssetService _assetService;
        private readonly StyleDataLoadShop _styleDataLoadShop;
        private readonly Model _modelMetaRoot;
        private readonly ProviderUiFactory _providerUiFactory;
        private readonly SessionDataMatch _sessionDataMatch;
        private readonly SaveLoadService _saveLoadService;
        private readonly PopupService _popupService;
        private PoolUiItem<ItemStyle> _itemInventoryStyle;
        private PoolUiItem<ShopItem> _itemShopPool;
        private MetaRoot _metaRoot;

        public MetaFlow(SceneManager sceneManager, LoadingService loadingService,
            LoadingView loadingView,AssetService assetService,
            StyleDataLoadShop styleDataLoadShop,Model modelMetaRoot,
            ProviderUiFactory providerUiFactory,
            SessionDataMatch sessionDataMatch,SaveLoadService saveLoadService,PopupService popupService)
        {
            _popupService = popupService;
            _saveLoadService = saveLoadService;
            _sessionDataMatch = sessionDataMatch;
            _providerUiFactory = providerUiFactory;
            _modelMetaRoot = modelMetaRoot;
            _styleDataLoadShop = styleDataLoadShop;
            _assetService = assetService;
            _loadingView = loadingView;
            _sceneManager = sceneManager;
            _loadingService = loadingService;
        }

        public async void Start()
        {
            _itemShopPool = new PoolUiItem<ShopItem>(_assetService);
            _itemInventoryStyle = new PoolUiItem<ItemStyle>(_assetService);
            
            await _loadingService.BeginLoading(_styleDataLoadShop);
            await _loadingService.BeginLoading(_sessionDataMatch);
            await _loadingService.BeginLoading(_itemShopPool, 
                new DataPullUiItem(
                    RuntimeConstants.Popup.ShopElementBuyPrefab,
                    RuntimeConstants.Popup.PoolElementUi,
                    _styleDataLoadShop.GetData().Length));
            
            await _loadingService.BeginLoading(_itemInventoryStyle, 
                new DataPullUiItem(
                    RuntimeConstants.Popup.InventoryElementStylePrefab,
                    RuntimeConstants.Popup.PoolElementUi,
                    _styleDataLoadShop.GetData().Length));
            
            _metaRoot = _providerUiFactory.FactoryUi.CreateRootUi<MetaRoot>(TypeAsset.Meta_Root_Ui, RuntimeConstants.UiRoot.MetaRoot);
            _metaRoot.Resolve(_popupService,_modelMetaRoot,_assetService,_saveLoadService.profileData,_providerUiFactory);
            
            await _loadingService.BeginLoading(_metaRoot);
            await _metaRoot.Show();

            await _modelMetaRoot.Resolve(_metaRoot,_styleDataLoadShop.GetData(),_itemShopPool,_itemInventoryStyle,this);
            await _loadingService.BeginLoading(_modelMetaRoot);
            
            await _loadingView.Hide();
            AudioPlayer.S.MetaBackground();
        }

        public async void StartMatch()
        {
            await _loadingView.Show();
            await _sceneManager.LoadScene(RuntimeConstants.Scene.Match);
        }
        
        public void Dispose()
        {
             _popupService.Release().Forget();
            _loadingService.DisposableService.Dispose();
        }
    }
}
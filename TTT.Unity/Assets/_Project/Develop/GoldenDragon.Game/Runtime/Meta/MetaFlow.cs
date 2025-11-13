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
using UnityEngine;
using VContainer.Unity;
using Object = System.Object;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta
{
    public class MetaFlow:IStartable
    {
        private readonly SceneManager _sceneManager;
        private readonly LoadingService _loadingService;
        private readonly LoadingView _loadingView;
        private readonly AssetService _assetService;
        private readonly StyleDataLoad _styleDataLoad;
        private readonly Model _modelMetaRoot;
        private readonly ProviderUiFactory _providerUiFactory;
        private readonly SessionDataMatch _sessionDataMatch;
        private PopupService _popupService;
        private PoolUiItem<ItemInventoryStyle> _itemInventoryStyle;
        private PoolUiItem<ItemShop> _itemShopPool;
        private SaveLoadService _saveLoadService;

        public MetaFlow(SceneManager sceneManager, LoadingService loadingService,
            LoadingView loadingView,AssetService assetService,
            StyleDataLoad styleDataLoad,Model modelMetaRoot,
            ProviderUiFactory providerUiFactory,
            SessionDataMatch sessionDataMatch,SaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
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
            _popupService = new PopupService(_saveLoadService);
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
            metaRoot.Constructor(_popupService,_modelMetaRoot,_assetService,_saveLoadService.PlayerData,_providerUiFactory);
            
            await metaRoot.Initialized();
            await metaRoot.Show();
            
            await _modelMetaRoot.Initialized(metaRoot,_styleDataLoad.GetData(),_itemShopPool,_itemInventoryStyle,_popupService,this);
            await _loadingService.BeginLoading(_modelMetaRoot);
            
            await _loadingView.Hide();
            AudioPlayer.MetaBackground();
        }

        public async void StartMatch()
        {
            await _loadingView.Show();
            await Release();
            await _sceneManager.LoadScene(RuntimeConstants.Scene.Match);
        }

        private async UniTask Release()
        {
            foreach (ItemInventoryStyle item in _itemInventoryStyle) UnityEngine.Object.Destroy(item.gameObject);
            foreach (ItemShop item in _itemShopPool) UnityEngine.Object.Destroy(item.gameObject);
            
            _itemInventoryStyle.Dispose();
            _itemShopPool.Dispose();
            
            await _popupService.Release();
            await _modelMetaRoot.Release();

            await Task.CompletedTask;
        }
    }
}
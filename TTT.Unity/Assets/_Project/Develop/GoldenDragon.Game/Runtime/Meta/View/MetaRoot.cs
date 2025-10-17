using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View
{
    public class MetaRoot : MonoBehaviour
    {
        [Header("Popup Background")]
        [SerializeField] private MetaBlackPopupBackground _popupBackground;
        [Header("Popup Parent")] 
        [SerializeField] private GameObject _parent;
        [Header("Button Open Popup")] 
        [SerializeField] private Button _btnOpenSetting;
        [SerializeField] private Button _btnOpenLeaderBoard;
        [SerializeField] private Button _btnOpenMatch;
        [SerializeField] private Button _btnOpenShop;
        [SerializeField] private Button _btnOpenInventory;
        
        private PopupService _popupService;
        private FactoryMetaUi _factoryMetaUi;
        private Model _model;
        private AssetService _assetService;
        
        [Inject]
        public void Constructor(PopupService popupService,FactoryMetaUi factoryMetaUi,Model model,AssetService assetService)
        {
            _assetService = assetService;
            _model = model;
            _factoryMetaUi = factoryMetaUi;
            _popupService = popupService;
        }
        
        public async UniTask Initialized()
        {
            await _model.Initialized(this);
            await InitializedEvent();
            
            GameObject setting = await _factoryMetaUi.LoadPopupToObject(Constant.M.Asset.Popup.Setting);
            GameObject leaderBoard = await _factoryMetaUi.LoadPopupToObject(Constant.M.Asset.Popup.LeaderBoard);
            GameObject shop = await _factoryMetaUi.LoadPopupToObject(Constant.M.Asset.Popup.Shop);
            GameObject inventory = await _factoryMetaUi.LoadPopupToObject(Constant.M.Asset.Popup.Inventory);
            GameObject match = await _factoryMetaUi.LoadPopupToObject(Constant.M.Asset.Popup.Match);
            
            SettingPopup settingPopup = _assetService.Install.InstallToUiPopup<SettingPopup>(setting,_parent);
            LeaderBoardPopup leaderBoardPopup = _assetService.Install.InstallToUiPopup<LeaderBoardPopup>(leaderBoard,_parent);
            ShopPopup shopPopup = _assetService.Install.InstallToUiPopup<ShopPopup>(shop,_parent);
            InventoryPopup inventoryPopup = _assetService.Install.InstallToUiPopup<InventoryPopup>(inventory,_parent);
            MatchPopup matchPopup = _assetService.Install.InstallToUiPopup<MatchPopup>(match,_parent);
            
            _popupService.AddPopupInList(TypePopup.Setting,settingPopup);
            _popupService.AddPopupInList(TypePopup.LeaderBoard,leaderBoardPopup);
            _popupService.AddPopupInList(TypePopup.Shop,shopPopup);
            _popupService.AddPopupInList(TypePopup.Inventory,inventoryPopup);
            _popupService.AddPopupInList(TypePopup.Match,matchPopup);
            
            await UniTask.CompletedTask;
        }

        public MetaBlackPopupBackground GetPopupBackground()
        {
            return _popupBackground;
        }

        public async UniTask Release()
        {
            await _popupService.Release();
            await UniTask.CompletedTask;
        }

        private async UniTask InitializedEvent()
        {
            _btnOpenSetting.OnClickAsObservable().Subscribe(_ => _model.OpenPopupSetting()).AddTo(this);
            _btnOpenLeaderBoard.OnClickAsObservable().Subscribe(_ => _model.OpenPopupLeaderBoard()).AddTo(this);
            _btnOpenMatch.OnClickAsObservable().Subscribe(_ => _model.OpenPopupMatch()).AddTo(this);
            _btnOpenInventory.OnClickAsObservable().Subscribe(_ => _model.OpenPopupInventory()).AddTo(this);
            _btnOpenShop.OnClickAsObservable().Subscribe(_ => _model.OpenPopupShop()).AddTo(this);
            await UniTask.CompletedTask;
        }
    }
}

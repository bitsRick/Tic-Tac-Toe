using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using TMPro;
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
        [Header("SoftValue")]
        [SerializeField] private TextMeshProUGUI _softValueX;
        [SerializeField] private TextMeshProUGUI _softValueO;
        
        private PopupService _popupService;
        private Model _model;
        private AssetService _assetService;
        private IPlayerProgress _playerProgress;
        public Subject<Unit> OnSoftValueChanged = new Subject<Unit>();
        private MetaProviderFacadeFactory _metaProviderFacadeFactory;

        [Inject]
        public void Constructor(PopupService popupService,Model model,AssetService assetService,IPlayerProgress playerProgress,MetaProviderFacadeFactory metaProviderFacadeFactory)
        {
            _metaProviderFacadeFactory = metaProviderFacadeFactory;
            _playerProgress = playerProgress;
            _assetService = assetService;
            _model = model;
            _popupService = popupService;
        }
        
        public async UniTask Initialized()
        {
            await InitializedEvent();
            
            GameObject setting = await _metaProviderFacadeFactory.MetaFactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.Setting);
            GameObject leaderBoard = await _metaProviderFacadeFactory.MetaFactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.LeaderBoard);
            GameObject shop = await _metaProviderFacadeFactory.MetaFactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.Shop);
            GameObject inventory = await _metaProviderFacadeFactory.MetaFactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.Inventory);
            GameObject match = await _metaProviderFacadeFactory.MetaFactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.Match);
            
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

            OnSoftValueChanged
                .Subscribe(_ =>
                {
                    _softValueO.text = _playerProgress.PlayerData.SoftValueO.ToString();
                    _softValueX.text = _playerProgress.PlayerData.SoftValueX.ToString();
                    shopPopup.SoftValueO.text = _playerProgress.PlayerData.SoftValueO.ToString();
                    shopPopup.SoftValueX.text = _playerProgress.PlayerData.SoftValueX.ToString();
                }).AddTo(this);
            
            OnSoftValueChanged.OnNext(Unit.Default);
            
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

        public async UniTask Show()
        {
            gameObject.SetActive(true);
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

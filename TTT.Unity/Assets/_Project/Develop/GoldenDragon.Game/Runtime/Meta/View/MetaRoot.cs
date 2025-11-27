using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View
{
    public class MetaRoot : MonoBehaviour,IUiRoot,ILoadUnit
    {
        [Header("Popup Background")]
        [SerializeField] private BackPopupBackground _popupBackground;
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
        private PlayerData _playerProgress;
        public Subject<Unit> OnSoftValueChanged = new Subject<Unit>();
        private ProviderUiFactory _providerUiFactory;

        [Inject]
        public void Constructor(PopupService popupService,Model model,AssetService assetService,PlayerData playerProgress,ProviderUiFactory providerUiFactory)
        {
            _providerUiFactory = providerUiFactory;
            _playerProgress = playerProgress;
            _assetService = assetService;
            _model = model;
            _popupService = popupService;
        }

        public async UniTask Load()
        {
            await InitializedPopup();
            await InitializedEvent();

            OnSoftValueChanged.OnNext(Unit.Default);
            
            await UniTask.CompletedTask;
        }

        public async UniTask InitializedPopup()
        {
            GameObject setting = await _providerUiFactory.FactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.Setting);
            GameObject leaderBoard = await _providerUiFactory.FactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.LeaderBoard);
            GameObject shop = await _providerUiFactory.FactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.Shop);
            GameObject inventory = await _providerUiFactory.FactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.Inventory);
            GameObject match = await _providerUiFactory.FactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.Match);
            
            SettingPopup settingPopup = _assetService.Install.InstallToUiPopup<SettingPopup>(setting,_parent);
            settingPopup.Initialized();
            
            LeaderBoardPopup leaderBoardPopup = _assetService.Install.InstallToUiPopup<LeaderBoardPopup>(leaderBoard,_parent);
            leaderBoardPopup.Initialized();
            
            ShopPopup shopPopup = _assetService.Install.InstallToUiPopup<ShopPopup>(shop,_parent);
            shopPopup.Initialized();
            
            InventoryPopup inventoryPopup = _assetService.Install.InstallToUiPopup<InventoryPopup>(inventory,_parent);
            inventoryPopup.Initialized();
            
            MatchPopup matchPopup = _assetService.Install.InstallToUiPopup<MatchPopup>(match,_parent);
            matchPopup.Initialized();
            
            _popupService.AddPopupInList(TypePopup.Setting,settingPopup);
            _popupService.AddPopupInList(TypePopup.LeaderBoard,leaderBoardPopup);
            _popupService.AddPopupInList(TypePopup.Shop,shopPopup);
            _popupService.AddPopupInList(TypePopup.Inventory,inventoryPopup);
            _popupService.AddPopupInList(TypePopup.Match,matchPopup);
        }

        public async UniTask InitializedEvent()
        {
            _btnOpenSetting.OnClickAsObservable().Subscribe(_ => _model.OpenPopupSetting()).AddTo(this);
            _btnOpenLeaderBoard.OnClickAsObservable().Subscribe(_ => _model.OpenPopupLeaderBoard()).AddTo(this);
            _btnOpenMatch.OnClickAsObservable().Subscribe(_ => _model.OpenPopupMatch()).AddTo(this);
            _btnOpenInventory.OnClickAsObservable().Subscribe(_ => _model.OpenPopupInventory()).AddTo(this);
            _btnOpenShop.OnClickAsObservable().Subscribe(_ => _model.OpenPopupShop()).AddTo(this);
            
            _popupBackground.OnEvenPointClickBackground.Subscribe((_) => _popupService.Close()).AddTo(this);
            
           if( _popupService.TryGetPopup(TypePopup.Shop,out ShopPopup shopPopup))
            {
                OnSoftValueChanged
                    .Subscribe(_ =>
                    {
                        _softValueO.text = _playerProgress.SoftValueO.ToString();
                        _softValueX.text = _playerProgress.SoftValueX.ToString();
                        shopPopup.SoftValueO.text = _playerProgress.SoftValueO.ToString();
                        shopPopup.SoftValueX.text = _playerProgress.SoftValueX.ToString();
                    }).AddTo(this);
            }
            
            await UniTask.CompletedTask;
        }

        public BackPopupBackground GetPopupBackground()
        {
            return _popupBackground;
        }

        public async UniTask Show()
        {
            gameObject.SetActive(true);
            await UniTask.CompletedTask;
        }

        public void Release()
        {
            _assetService.Release.ReleaseAsset<GameObject>(TypeAsset.Popup,Constant.M.Asset.Popup.LeaderBoard);
            _assetService.Release.ReleaseAsset<GameObject>(TypeAsset.Popup,Constant.M.Asset.Popup.Shop);
            _assetService.Release.ReleaseAsset<GameObject>(TypeAsset.Popup,Constant.M.Asset.Popup.Inventory);
            _assetService.Release.ReleaseAsset<GameObject>(TypeAsset.Popup,Constant.M.Asset.Popup.Match);
        }
    }
}

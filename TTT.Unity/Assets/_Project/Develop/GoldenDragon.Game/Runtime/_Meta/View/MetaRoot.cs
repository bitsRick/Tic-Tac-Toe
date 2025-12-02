using System.Threading.Tasks;
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
    public class MetaRoot : MonoBehaviour,IUiRoot,IDisposableLoadUnit
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
        private ProfileData _profileProgress;
        public Subject<Unit> OnSoftValueChanged = new Subject<Unit>();
        private ProviderUiFactory _providerUiFactory;

        [Inject]
        public void Resolve(PopupService popupService,Model model,AssetService assetService,ProfileData profileProgress,ProviderUiFactory providerUiFactory)
        {
            _providerUiFactory = providerUiFactory;
            _profileProgress = profileProgress;
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
            _popupService.AddPopupInList<SettingPopup>(TypePopup.Setting,RuntimeConstants.Popup.Setting,_parent);
            _popupService.AddPopupInList<LeaderBoardPopup>(TypePopup.LeaderBoard,RuntimeConstants.Popup.LeaderBoard,_parent);
            _popupService.AddPopupInList<ShopPopup>(TypePopup.Shop,RuntimeConstants.Popup.Shop,_parent);
            _popupService.AddPopupInList<InventoryPopup>(TypePopup.Inventory,RuntimeConstants.Popup.Inventory,_parent);
            _popupService.AddPopupInList<MatchPopup>(TypePopup.Match,RuntimeConstants.Popup.Match,_parent);
            await Task.CompletedTask;
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
                        _softValueO.text = _profileProgress.SoftValueO.ToString();
                        _softValueX.text = _profileProgress.SoftValueX.ToString();
                        shopPopup.SoftValueO.text = _profileProgress.SoftValueO.ToString();
                        shopPopup.SoftValueX.text = _profileProgress.SoftValueX.ToString();
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
        
        public void Dispose()
        {
            _assetService.Release.ReleaseAsset<GameObject>(TypeAsset.Popup,RuntimeConstants.Popup.LeaderBoard);
            _assetService.Release.ReleaseAsset<GameObject>(TypeAsset.Popup,RuntimeConstants.Popup.Shop);
            _assetService.Release.ReleaseAsset<GameObject>(TypeAsset.Popup,RuntimeConstants.Popup.Inventory);
            _assetService.Release.ReleaseAsset<GameObject>(TypeAsset.Popup,RuntimeConstants.Popup.Match);
        }
    }
}

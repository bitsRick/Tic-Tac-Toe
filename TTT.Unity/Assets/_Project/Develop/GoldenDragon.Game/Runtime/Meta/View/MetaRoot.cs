using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory;
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
            SettingPopup settingPopup = _assetService.Install.InstallToUiPopup<SettingPopup>(setting,_parent);
            
            _popupService.AddPopupInList(TypePopup.Setting,settingPopup);
            
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
            _btnOpenSetting.OnClickAsObservable().Subscribe(_ => _model.OpenPopupSetting()).AddTo(_btnOpenSetting);
            await UniTask.CompletedTask;
        }
    }
}

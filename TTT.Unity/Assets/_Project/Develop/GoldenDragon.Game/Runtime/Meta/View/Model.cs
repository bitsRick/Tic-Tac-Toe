using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.ShopElementSell;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Style;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UniRx;
using UnityEngine;
using VContainer;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View
{
    public class Model
    {
        private PopupService _popupService;
        private MetaBlackPopupBackground _popupBackground;
        private PopupBase _activePopup;
        private Lang _language;
        private AudioPlayer _audioPlayer;
        private StyleData[] _styleData;
        private bool _isActivePopup;
        private bool _isShopLoadData;
        private AssetService _assetService;
        private PoolUiElement<ElementSell> _poolElementSellUi;

        [Inject]
        public void Construct(PopupService popupService,Lang language,AudioPlayer audioPlayer,AssetService assetService)
        {
            _assetService = assetService;
            _audioPlayer = audioPlayer;
            _language = language;
            _popupService = popupService;
        }

        public UniTask Initialized(MetaRoot metaRoot, StyleData[] styleData,
            PoolUiElement<ElementSell> poolElementSellUi)
        {
            _poolElementSellUi = poolElementSellUi;
            _styleData = styleData;
            _popupBackground = metaRoot.GetPopupBackground();
            _popupBackground.Initialized(this);
            return UniTask.CompletedTask;
        }

        public void OpenPopupSetting()
        {
            _audioPlayer.Click();
            
            if (IsNotOpenPopup(TypePopup.Setting))
            {
                if (_activePopup is SettingPopup popup)
                {
                    popup.Construct(this,_language);
                    popup.Initialized();
                    _popupBackground.Show();
                    
                    popup.Show();
                }
            }
        }

        public void OpenPopupLeaderBoard()
        {
            _audioPlayer.Click();
            
            if (IsNotOpenPopup(TypePopup.LeaderBoard))
            {
                if (_activePopup is LeaderBoardPopup popup)
                {
                    popup.Construct(this,_language);
                    popup.Initialized();
                    _popupBackground.Show();
                    
                    popup.Show();
                }
            }
        }

        public void OpenPopupMatch()
        {
            _audioPlayer.Click();
            
            if (IsNotOpenPopup(TypePopup.Match))
            {
                if (_activePopup is MatchPopup popup)
                {
                    popup.Construct(this,_language);
                    popup.Initialized();
                    _popupBackground.Show();
                    
                    popup.Show();
                }
            }
        }

        public void OpenPopupInventory()
        {
            _audioPlayer.Click();
            
            if (IsNotOpenPopup(TypePopup.Inventory))
            {
                if (_activePopup is InventoryPopup popup)
                {
                    popup.Construct(this,_language);
                    popup.Initialized();
                    _popupBackground.Show();
                    
                    popup.Show();
                }
            }
        }

        public void OpenPopupShop()
        {
            _audioPlayer.Click();
            ShopPopup popup;
            
            if (TryGetPopup(TypePopup.Shop, out ShopPopup shopPopup))
                popup = shopPopup;
            else
                return;
            
            popup.Construct(this,_language);
            popup.Initialized();
            
            foreach (var styleData in _styleData)
            {
                ElementSell element = _poolElementSellUi.GetElement();
                element.Constructo(this);
                element.Id = styleData.Id;
                element.O = styleData.ValueO.ToString();
                element.X = styleData.ValueX.ToString();
                
                element.ImageStyle.preserveAspect = true;
                element.ImageStyle.sprite = styleData.Sprite;
                
                element.ButtonBuy.OnClickAsObservable().Subscribe(_ =>
                {
                    BuyStyle(element.Id,element.BuyView);
                }).AddTo(element);
                
                element.transform.parent = popup.RootInstance.gameObject.transform;
                
                RectTransform rt = element.GetComponent<RectTransform>();
                
                rt.localScale = Vector3.one;
                rt.localPosition = Vector3.zero;
                rt.localRotation = Quaternion.identity;
                
                rt.anchoredPosition = Vector2.zero;
                
                
                element.gameObject.SetActive(true);
            }

            popup.HorizontalLayoutGroup.childScaleHeight = true;
            popup.HorizontalLayoutGroup.childControlHeight = true;
            
            _popupBackground.Show();
            
            popup.Show();
        }

        public void ClosePopup()
        {
            _audioPlayer.Click();
            
            _isActivePopup = false;
            _activePopup.Hide();
            _popupBackground.Hide();
            _activePopup = null;
        }

        public void BuyStyle(string id, GameObject gameObject)
        {
            throw new System.NotImplementedException();
        }

        private bool TryGetPopup<TP>(TypePopup typePopup,out TP popupOut)
            where TP: class
        {
            if (IsNotOpenPopup(typePopup))
            {
                if (_activePopup is TP popup)
                {
                    popupOut = popup;
                    return true;
                }
            }

            popupOut = null;
            return false;
        }

        private bool IsNotOpenPopup(TypePopup typePopupOpen)
        {
            if (_isActivePopup == false)
            {
                _isActivePopup = true;
                _activePopup = _popupService.GetPopup(typePopupOpen);

                return true;
            }

            return false;
        }
    }
}
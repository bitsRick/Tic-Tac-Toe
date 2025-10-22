using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.ShopElementSell;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Style;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
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
        private AssetService _assetService;
        private MetaRoot _metaRoot;
        private StyleData[] _styleData;
        private PoolUiElement<ElementSell> _poolElementSellUi;
        private IPlayerProgress _playerData;
        private bool _isShopLoadData;
        private bool _isActivePopup;
        
        [Inject]
        public void Construct(PopupService popupService,Lang language,AudioPlayer audioPlayer,AssetService assetService,IPlayerProgress playerData)
        {
            _playerData = playerData;
            _assetService = assetService;
            _audioPlayer = audioPlayer;
            _language = language;
            _popupService = popupService;
        }

        public UniTask Initialized(MetaRoot metaRoot, StyleData[] styleData,
            PoolUiElement<ElementSell> poolElementSellUi)
        {
            _metaRoot = metaRoot;
            _poolElementSellUi = poolElementSellUi;
            _styleData = styleData;
            _popupBackground = metaRoot.GetPopupBackground();
            _popupBackground.Initialized(this);
            InitializedShopElement();
            
            return UniTask.CompletedTask;
        }

        private void InitializedShopElement()
        {
            if (_popupService.GetPopup(TypePopup.Shop) is ShopPopup popup)
            {
                for (int i = 0; i < _styleData.Length; i++)
                {
                    ElementSell element = _poolElementSellUi.GetElement();
                    element.Construct(this);
                    element.ImageStyle.preserveAspect = true;
                
                    element.ButtonBuy.OnClickAsObservable().Subscribe(_ =>
                    {
                        BuyStyle(element.Id,element.BuyView,element.Type);
                    }).AddTo(element);
                
                    element.transform.parent = popup.RootInstance.gameObject.transform;
                
                    RectTransform rt = element.RectTransform;
                    rt.localScale = Vector3.one;
                    rt.localPosition = Vector3.zero;
                    rt.localRotation = Quaternion.identity;
                    rt.anchoredPosition = Vector2.zero;
                }
            
                popup.HorizontalLayoutGroup.childScaleHeight = true;
                popup.HorizontalLayoutGroup.childControlHeight = true;
            }
            else
            {
                Log.Meta.W($"Not load popup:{nameof(ShopPopup)}");
            }
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
            ShowShopSellItem(TypeShowElementShop.Board);
            _popupBackground.Show();
            
            popup.Show();
        }


        public void ShowShopSellItem(TypeShowElementShop type)
        {
            _poolElementSellUi.ResetIndex();
            
            foreach (StyleData data in _styleData)
            {
                if (data.Type != type.ToString() ||
                    data.Id == Constant.M.Asset.Popup.NotViewShop+type)
                    continue;

                ElementSell element = _poolElementSellUi.GetElement();
                element.Id = data.Id;
                element.ImageStyle.sprite = data.Sprite;
                element.X = data.ValueX.ToString();
                element.O = data.ValueO.ToString();
                element.Type = type;

                element.BuyView.SetActive(_playerData
                    .PlayerData
                    .ShopPlayerData
                    .FirstOrDefault(key => key.Id == data.Id) != null);

                element.gameObject.SetActive(true);
            }
        }

        public void BuyStyle(string id, GameObject gameObject, TypeShowElementShop typeStyle)
        {
            _metaRoot.OnSoftValueChanged.OnNext(Unit.Default);
            _playerData.PlayerData.ShopPlayerData.Add(new ShopPlayerData(){Id = id,TypeStyleElementShop = typeStyle});
            gameObject.SetActive(true);
        }

        public void ClosePopup()
        {
            _audioPlayer.Click();
            
            _isActivePopup = false;
            _activePopup.Hide();
            _popupBackground.Hide();
            _activePopup = null;
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
using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.InventoryItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.ShopElementItem;
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
        private PoolUiItem<ItemSell> _poolItemSellUi;
        private PoolUiItem<InventoryItemStyle> _itemInventoryStyle;
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

        public UniTask Initialized(MetaRoot metaRoot,
            StyleData[] styleData,
            PoolUiItem<ItemSell> poolItemSellUi,
            PoolUiItem<InventoryItemStyle> itemInventoryStyle)
        {
            _itemInventoryStyle = itemInventoryStyle;
            _metaRoot = metaRoot;
            _poolItemSellUi = poolItemSellUi;
            _styleData = styleData;
            _popupBackground = metaRoot.GetPopupBackground();
            _popupBackground.Initialized(this);
            InitializedShopItem();
            InitializedInventoryItem();
            
            return UniTask.CompletedTask;
        }

        private void InitializedInventoryItem()
        {
            // factory element
        }

        private void InitializedShopItem()
        {
            if (_popupService.GetPopup(TypePopup.Shop) is ShopPopup popup)
            {
                for (int i = 0; i < _styleData.Length; i++)
                {
                    ItemSell item = _poolItemSellUi.GetItem();
                    item.ImageStyle.preserveAspect = true;
                
                    item.ButtonBuy.OnClickAsObservable().Subscribe(_ =>
                    {
                        BuyStyle(item.Id,item.BuyView,item.Type);
                    }).AddTo(item);
                
                    item.transform.parent = popup.RootInstance.gameObject.transform;
                
                    RectTransform rt = item.RectTransform;
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
            InventoryPopup popup;

            if (TryGetPopup(TypePopup.Inventory, out InventoryPopup inventoryPopup))
                popup = inventoryPopup;
            else
                return;
            
            popup.Construct(this,_language);
            popup.Initialized();
            _popupBackground.Show();
                    
            popup.Show();
            
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
            ShowShopSellItem(TypeShowItemShop.Board);
            _popupBackground.Show();
            
            popup.Show();
        }


        public void ShowShopSellItem(TypeShowItemShop type)
        {
            _poolItemSellUi.ResetIndex();
            
            foreach (StyleData data in _styleData)
            {
                if (data.Type != type.ToString() ||
                    data.Id == GetDefaultType(type))
                    continue;

                ItemSell item = _poolItemSellUi.GetItem();
                item.Id = data.Id;
                item.ImageStyle.sprite = data.Sprite;
                item.X = data.ValueX.ToString();
                item.O = data.ValueO.ToString();
                item.Type = type;

                item.BuyView.SetActive(_playerData
                    .PlayerData
                    .ShopPlayerData
                    .FirstOrDefault(key => key.Id == data.Id) != null);

                item.gameObject.SetActive(true);
            }
        }

        public void ClosePopup()
        {
            _audioPlayer.Click();
            
            _isActivePopup = false;
            _activePopup.Hide();
            _popupBackground.Hide();
            _activePopup = null;
        }

        private string GetDefaultType(TypeShowItemShop type)
        {
            switch (type)
            {
                case TypeShowItemShop.Board:
                    return Constant.StyleData.DefaultBoard;
                
                case TypeShowItemShop.X:
                    return Constant.StyleData.DefaultX;
                
                case TypeShowItemShop.O:
                    return Constant.StyleData.DefaultO;
                
            }

            return null;
        }

        private void BuyStyle(string id, GameObject gameObject, TypeShowItemShop typeStyle)
        {
            _metaRoot.OnSoftValueChanged.OnNext(Unit.Default);
            _playerData.PlayerData.ShopPlayerData.Add(new ShopPlayerData(){Id = id,typeStyleItemShop = typeStyle});
            gameObject.SetActive(true);
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
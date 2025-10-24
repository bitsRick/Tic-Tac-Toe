using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.InventoryItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.LeaderBoardItem;
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
        private PoolUiItem<ItemInventoryStyle> _poolItemInventoryStyle;
        private IPlayerProgress _playerData;
        private bool _isShopLoadData;
        private bool _isActivePopup;
        private MetaProviderFacadeFactory _factory;

        [Inject]
        public void Construct(PopupService popupService, Lang language, AudioPlayer audioPlayer,
            AssetService assetService, IPlayerProgress playerData, MetaProviderFacadeFactory metaProviderFacadeFactory)
        {
            _factory = metaProviderFacadeFactory;
            _playerData = playerData;
            _assetService = assetService;
            _audioPlayer = audioPlayer;
            _language = language;
            _popupService = popupService;
        }

        public UniTask Initialized(MetaRoot metaRoot,
            StyleData[] styleData,
            PoolUiItem<ItemSell> poolItemSellUi,
            PoolUiItem<ItemInventoryStyle> poolItemInventoryStyle)
        {
            _poolItemInventoryStyle = poolItemInventoryStyle;
            _metaRoot = metaRoot;
            _poolItemSellUi = poolItemSellUi;
            _styleData = styleData;
            _popupBackground = metaRoot.GetPopupBackground();
            _popupBackground.Initialized(this);
            InitializedShopItem();
            InitializedInventoryItem();

            return UniTask.CompletedTask;
        }

        public void EnterStyle(object id, object buyView, object type)
        {
        }

        public void OpenPopupSetting()
        {
            _audioPlayer.Click();

            if (IsNotOpenPopup(TypePopup.Setting))
            {
                if (_activePopup is SettingPopup popup)
                {
                    popup.Construct(this, _language);
                    popup.Initialized();
                    _popupBackground.Show();

                    popup.Show();
                }
            }
        }

        public void OpenPopupLeaderBoard()
        {
            _audioPlayer.Click();
            LeaderBoardPopup popup;

            if (TryGetPopup(TypePopup.LeaderBoard, out LeaderBoardPopup leaderBoardPopup))
                popup = leaderBoardPopup;
            else
                return;
            
            popup.Construct(_language);
            popup.Initialized();

            foreach (ItemLeaderBoards itemLeader in popup.Leaders)
            {
                itemLeader.Name = _playerData.PlayerData.Nick;
                itemLeader.CurrentTop = "1";
                itemLeader.Score = _playerData.PlayerData.Score.ToString();
            }
            
            _popupBackground.Show();
            popup.Show();
        }

        public void OpenPopupMatch()
        {
            _audioPlayer.Click();

            if (IsNotOpenPopup(TypePopup.Match))
            {
                if (_activePopup is MatchPopup popup)
                {
                    popup.Construct(this, _language);
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

            popup.Construct(_language);
            popup.Initialized();

            ShowItemInventorStyle(TypeShowItemShop.Board);
            
            
            
            _popupBackground.Show();
            popup.Show();
        }

        private void ShowItemInventorStyle(TypeShowItemShop type)
        {
            _poolItemInventoryStyle.ResetIndex();

            foreach (var playerStyleShop in _playerData.PlayerData.ShopPlayerData)
            {
                if (playerStyleShop.typeStyleItemShop != type)
                    continue;
                
                var data = _styleData.FirstOrDefault(key => key.Id == playerStyleShop.Id);

                if (data == null)
                    continue;

                var itemInventory = _poolItemInventoryStyle.GetItem();

                itemInventory.Type = type;
                itemInventory.Id = data.Id;
                itemInventory.ImageStyle.sprite = data.Sprite;

                itemInventory.gameObject.SetActive(true);
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

            popup.Construct(this, _language);
            popup.Initialized();
            ShowItemShopSell(TypeShowItemShop.Board);
            _popupBackground.Show();

            popup.Show();
        }


        public void ShowItemShopSell(TypeShowItemShop type)
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

        private void InitializedInventoryItem()
        {
            if (_popupService.GetPopup(TypePopup.Inventory) is InventoryPopup popup)
                for (int i = 0; i < _styleData.Length; i++)
                    _factory.MetaFactoryItem.CreateItemInventory(this, popup, _poolItemInventoryStyle.GetItem());
            else
                Log.Meta.W($"Not load popup:{nameof(ShopPopup)}");
        }

        private void InitializedShopItem()
        {
            if (_popupService.GetPopup(TypePopup.Shop) is ShopPopup popup)
                for (int i = 0; i < _styleData.Length; i++)
                    _factory.MetaFactoryItem.CreateItemShopSell(this, popup, _poolItemSellUi.GetItem());
            else
                Log.Meta.W($"Not load popup:{nameof(ShopPopup)}");
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

        public void BuyStyle(string id, GameObject gameObject, TypeShowItemShop typeStyle)
        {
            _metaRoot.OnSoftValueChanged.OnNext(Unit.Default);
            _playerData.PlayerData.ShopPlayerData.Add(new ShopPlayerData() { Id = id, typeStyleItemShop = typeStyle });
            gameObject.SetActive(true);
        }

        private bool TryGetPopup<TP>(TypePopup typePopup, out TP popupOut)
            where TP : class
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
using System;
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
using UnityEngine.UI;
using VContainer;
using StyleData = GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player.StyleData;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View
{
    public class Model
    {
        private PopupService _popupService;
        private BackPopupBackground _popupBackground;
        private PopupBase _activePopup;
        private Lang _language;
        private AudioPlayer _audioPlayer;
        private MetaRoot _metaRoot;
        private Style.StyleData[] _styleData;
        private PoolUiItem<ItemSell> _poolItemSellUi;
        private PoolUiItem<ItemInventoryStyle> _poolItemInventoryStyle;
        private IPlayerProgress _playerData;
        private bool _isShopLoadData;
        private bool _isActivePopup;
        private MetaProviderFacadeFactory _factory;

        [Inject]
        public void Construct(PopupService popupService, Lang language, AudioPlayer audioPlayer, IPlayerProgress playerData, MetaProviderFacadeFactory metaProviderFacadeFactory)
        {
            _factory = metaProviderFacadeFactory;
            _playerData = playerData;
            _audioPlayer = audioPlayer;
            _language = language;
            _popupService = popupService;
        }

        public UniTask Initialized(MetaRoot metaRoot,
            Style.StyleData[] styleData,
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
            InitializedEvent();

            return UniTask.CompletedTask;
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

            ShowItemInventor(TypeShowItemStyle.Board);
            
            _popupBackground.Show();
            popup.Show();
        }
        
        private void EnterStyle(ItemInventoryStyle item)
        {
            StyleData currentStylePlayer = GetStyleCurrentPlayer(item.Type);
            
            ItemInventoryStyle currentActiveStyle = _poolItemInventoryStyle.Find(currentStylePlayer.Id);
            currentActiveStyle.ActiveGameObject.SetActive(false);
            currentStylePlayer.Id = item.Id;
            
            item.ActiveGameObject.SetActive(true);
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
            ShowItemShop(TypeShowItemStyle.Board);

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

        private void ShowItemShop(TypeShowItemStyle type)
        {
            _poolItemSellUi.ResetIndex();

            foreach (Style.StyleData data in _styleData)
            {
                if (data.Type != type.ToString() ||
                    data.Id == GetDefaultType(type))
                    continue;

                ItemSell item = _poolItemSellUi.GetItem();
                item.Id = data.Id;
                item.Image.sprite = data.Sprite;
                item.X = data.ValueX.ToString();
                item.O = data.ValueO.ToString();
                item.Type = type;

                item.ActiveGameObject.SetActive(_playerData
                    .PlayerData
                    .ShopPlayerData
                    .FirstOrDefault(key => key.Id == data.Id) != null);

                item.gameObject.SetActive(true);
            }
        }

        private void ShowItemInventor(TypeShowItemStyle type)
        {
            _poolItemInventoryStyle.ResetIndex();

            foreach (var playerStyleShop in _playerData.PlayerData.ShopPlayerData)
            {
                if (playerStyleShop.Type != type)
                    continue;
                
                var data = _styleData.FirstOrDefault(key => key.Id == playerStyleShop.Id);

                if (data == null)
                    continue;

                var itemInventory = _poolItemInventoryStyle.GetItem();

                itemInventory.Type = type;
                itemInventory.Id = data.Id;
                itemInventory.Image.sprite = data.Sprite;

                StyleData currentStylePlayer = GetStyleCurrentPlayer(type);

                if (type == currentStylePlayer.Type)
                    if (data.Id == playerStyleShop.Id)
                        itemInventory.ActiveGameObject.SetActive(true);
                
                itemInventory.gameObject.SetActive(true);
            }
        }

        private StyleData GetStyleCurrentPlayer(TypeShowItemStyle type)
        {
            return type switch
            {
                TypeShowItemStyle.Board => _playerData.PlayerData.Board,
                TypeShowItemStyle.X => _playerData.PlayerData.X,
                TypeShowItemStyle.O => _playerData.PlayerData.O,
                _ => new StyleData()
            };
        }

        private void InitializedEvent()
        {
            if (_popupService.GetPopup(TypePopup.Shop) is ShopPopup popupShop)
            {
                InitEventButtonClick(popupShop.BtnBorder, popupShop.gameObject,() => { ShowItemShop(TypeShowItemStyle.Board);});
                InitEventButtonClick(popupShop.BtnX, popupShop.gameObject,() => { ShowItemShop(TypeShowItemStyle.X);});
                InitEventButtonClick(popupShop.BtnO, popupShop.gameObject,() => { ShowItemShop(TypeShowItemStyle.O);});
            }

            if (_popupService.GetPopup(TypePopup.Inventory) is InventoryPopup popupInventory)
            {
                InitEventButtonClick(popupInventory.BtnBoard, popupInventory.gameObject,() => { ShowItemInventor(TypeShowItemStyle.Board);});
                InitEventButtonClick(popupInventory.BtnX, popupInventory.gameObject,() => { ShowItemInventor(TypeShowItemStyle.X);});
                InitEventButtonClick(popupInventory.BtnO, popupInventory.gameObject,() => { ShowItemInventor(TypeShowItemStyle.O);});
            }
        }

        private void BuyStyle(string id, GameObject gameObject, TypeShowItemStyle typeStyle)
        {
            Style.StyleData data = _styleData.FirstOrDefault(key => key.Id == id);
            
            if (data == null)
                return;

            if (data.ValueX > _playerData.PlayerData.SoftValueX ||
                data.ValueO > _playerData.PlayerData.SoftValueO)
                return;

            _playerData.PlayerData.SoftValueX -= data.ValueX;
            _playerData.PlayerData.SoftValueO -= data.ValueO;

            _metaRoot.OnSoftValueChanged.OnNext(Unit.Default);
            _playerData.PlayerData.ShopPlayerData.Add(new StyleData() { Id = id, Type = typeStyle });
            gameObject.SetActive(true);
        }

        private void InitializedInventoryItem()
        {
            if (_popupService.GetPopup(TypePopup.Inventory) is InventoryPopup popup)
                for (int i = 0; i < _styleData.Length; i++)
                {
                    ItemInventoryStyle item = _factory.MetaFactoryItem.CreateItem(_poolItemInventoryStyle.GetItem(), popup);
                    item.Btn.OnClickAsObservable().Subscribe(_ =>
                    {
                        EnterStyle(item);
                    }).AddTo(popup);
                }
            else
            {
                Log.Meta.W($"Not load popup:{nameof(InventoryPopup)}");
            }
        }

        private void InitializedShopItem()
        {
            if (_popupService.GetPopup(TypePopup.Shop) is ShopPopup popup)
                for (int i = 0; i < _styleData.Length; i++)
                {
                    ItemSell item = _factory.MetaFactoryItem.CreateItem(_poolItemSellUi.GetItem(), popup);
                    item.Btn.OnClickAsObservable().Subscribe(_ =>
                    {
                        BuyStyle(item.Id, item.ActiveGameObject, item.Type);
                    }).AddTo(popup);
                }
            else
            {
                Log.Meta.W($"Not load popup:{nameof(ShopPopup)}");
            }
        }

        private string GetDefaultType(TypeShowItemStyle type)
        {
            return type switch
            {
                TypeShowItemStyle.Board => Constant.StyleData.DefaultBoard,
                TypeShowItemStyle.X => Constant.StyleData.DefaultX,
                TypeShowItemStyle.O => Constant.StyleData.DefaultO,
                _ => null
            };
        }

        private void InitEventButtonClick(Button btn, GameObject popup, Action showItemShopSell)
        {
            btn.OnClickAsObservable().Subscribe(_ => { showItemShopSell?.Invoke(); }).AddTo(popup);
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Language;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.Factory;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.SimulationData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.InventoryItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.LeaderBoardItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.ShopElementItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
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
        private Style.StyleData[] _styleData;
        private PopupService _popupService;
        private BackPopupBackground _popupBackground;
        private MetaRoot _metaRoot;
        private PoolUiItem<ItemShop> _poolItemSellUi;
        private PoolUiItem<ItemInventoryStyle> _poolItemInventoryStyle;
        private MetaProviderFacadeFactory _factory;
        private SaveLoadService _saveLoadService;
        private bool _isShopLoadData;
        private IPlayerProgress _playerData;

        [Inject]
        public void Construct(
            PopupService popupService,
            IPlayerProgress playerData, 
            MetaProviderFacadeFactory metaProviderFacadeFactory,
            SaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _factory = metaProviderFacadeFactory;
            _playerData = playerData;
            _popupService = popupService;
        }

        public UniTask Initialized(MetaRoot metaRoot,
            Style.StyleData[] styleData,
            PoolUiItem<ItemShop> poolItemSellUi,
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
            AudioPlayer.Click();
            SettingPopup popup;

            if (_popupService.TryGetPopup(TypePopup.Setting, out SettingPopup settingPopup))
                popup = settingPopup;
            else
                return;
            
            popup.MusicSlider.value = AudioPlayer.GetSliderValue(TypeValueChange.Music);
            popup.SoundSlider.value = AudioPlayer.GetSliderValue(TypeValueChange.Sound);
            
            _popupBackground.Show();
            popup.Show();
        }

        public void OpenPopupLeaderBoard()
        {
            AudioPlayer.Click();
            LeaderBoardPopup popup;

            if (_popupService.TryGetPopup(TypePopup.LeaderBoard, out LeaderBoardPopup leaderBoardPopup))
                popup = leaderBoardPopup;
            else
                return;

            popup.Initialized();

            PlayerData playerData = _playerData.PlayerData;

            List<SimulationData.Data> dataList = SimulatorLeaderBoard.S.D;
            dataList.Add(new SimulationData.Data() { Score = playerData.Score, Name = playerData.Nick });

            var sortLeader = dataList.OrderByDescending(key => key.Score).ToArray();
            int index = -1;

            foreach (ItemLeaderBoards itemLeader in popup.Leaders)
            {
                index++;

                if (index >= sortLeader.Length)
                {
                    itemLeader.Name = string.Empty;
                    itemLeader.Score = "";
                }
                else
                {
                    itemLeader.Name = sortLeader[index].Name;
                    itemLeader.Score = sortLeader[index].Score.ToString();
                }

                itemLeader.CurrentTop = (index + 1).ToString();
            }

            _popupBackground.Show();
            popup.Show();
        }

        public void OpenPopupMatch()
        {
            AudioPlayer.Click();
            MatchPopup popup;

            if (_popupService.TryGetPopup(TypePopup.Match, out MatchPopup matchPopup))
                popup = matchPopup;
            else
                return;
            
            _popupBackground.Show();
            popup.Show();
        }

        public void OpenPopupInventory()
        {
            AudioPlayer.Click();
            InventoryPopup popup;

            if (_popupService.TryGetPopup(TypePopup.Inventory, out InventoryPopup inventoryPopup))
                popup = inventoryPopup;
            else
                return;
            
            ShowItemInventor(TypeShowItemStyle.Board);

            _popupBackground.Show();
            popup.Show();
        }

        public void OpenPopupShop()
        {
            AudioPlayer.Click();
            ShopPopup popup;

            if (_popupService.TryGetPopup(TypePopup.Shop, out ShopPopup shopPopup))
                popup = shopPopup;
            else
                return;
            
            ShowItemShop(TypeShowItemStyle.Board);

            popup.HorizontalLayoutGroup.childScaleHeight = true;
            popup.HorizontalLayoutGroup.childControlHeight = true;

            _popupBackground.Show();
            popup.Show();
        }

        public void ClosePopup()
        {
            AudioPlayer.Click();
            _popupService.Rest();
            _popupBackground.Hide();
            _saveLoadService.SaveProgress();
        }

        private void ShowItemShop(TypeShowItemStyle type)
        {
            _poolItemSellUi.Reset();

            foreach (Style.StyleData data in _styleData)
            {
                if (data.Type != type.ToString() ||
                    data.Id == GetDefaultType(type))
                    continue;

                ItemShop item = _poolItemSellUi.GetItem();
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
            _poolItemInventoryStyle.Reset();

            var dataStyleArray = _playerData.PlayerData.ShopPlayerData.Where(key => key.Type == type).ToArray();

            foreach (ItemInventoryStyle item in _poolItemInventoryStyle)
            {
                int index = _poolItemInventoryStyle.GetIndex();

                if (index >= dataStyleArray.Length)
                {
                    item.gameObject.SetActive(false);
                    continue;
                }

                var data = _styleData.FirstOrDefault(key =>
                    key.Id == dataStyleArray[index].Id);

                item.Type = type;
                item.Id = data.Id;
                item.Image.sprite = data.Sprite;

                StyleData currentStylePlayer = GetStyleCurrentPlayer(type);

                if (type == currentStylePlayer.Type)
                    item.ActiveGameObject.SetActive(data.Id == currentStylePlayer.Id);

                item.gameObject.SetActive(true);
            }
        }

        private void EnterStyle(ItemInventoryStyle item)
        {
            StyleData currentStylePlayer = GetStyleCurrentPlayer(item.Type);

            ItemInventoryStyle currentActiveStyle = _poolItemInventoryStyle.Find(currentStylePlayer.Id);
            currentActiveStyle.ActiveGameObject.SetActive(false);
            currentStylePlayer.Id = item.Id;

            item.ActiveGameObject.SetActive(true);
            
            _saveLoadService.OnPlayerDataChanged.OnNext(Unit.Default);
        }

        private StyleData GetStyleCurrentPlayer(TypeShowItemStyle type)
        {
            return type switch
            {
                TypeShowItemStyle.Board => _playerData.PlayerData.Board,
                TypeShowItemStyle.X => _playerData.PlayerData.X,
                TypeShowItemStyle.O => _playerData.PlayerData.O,
                _ => throw new DataException($"Type not found {type}")
            };
        }

        private void InitializedEvent()
        {
            if (_popupService.GetPopup(TypePopup.Shop) is ShopPopup popupShop)
            {
                InitEventButtonClick(popupShop.BtnBorder, popupShop.gameObject,
                    () => { ShowItemShop(TypeShowItemStyle.Board); });
                InitEventButtonClick(popupShop.BtnX, popupShop.gameObject,
                    () => { ShowItemShop(TypeShowItemStyle.X); });
                InitEventButtonClick(popupShop.BtnO, popupShop.gameObject,
                    () => { ShowItemShop(TypeShowItemStyle.O); });
            }

            if (_popupService.GetPopup(TypePopup.Inventory) is InventoryPopup popupInventory)
            {
                InitEventButtonClick(popupInventory.BtnBoard, popupInventory.gameObject,
                    () => { ShowItemInventor(TypeShowItemStyle.Board); });
                InitEventButtonClick(popupInventory.BtnX, popupInventory.gameObject,
                    () => { ShowItemInventor(TypeShowItemStyle.X); });
                InitEventButtonClick(popupInventory.BtnO, popupInventory.gameObject,
                    () => { ShowItemInventor(TypeShowItemStyle.O); });
            }
            
            if (_popupService.GetPopup(TypePopup.Setting) is SettingPopup popupSetting)
            {
                popupSetting.MusicSlider.onValueChanged.AsObservable().Subscribe( value =>
                {
                    SetValueChange(value,TypeValueChange.Music);
                }).AddTo(popupSetting);

                popupSetting.MusicMute.onClick.AsObservable().Subscribe((_) =>
                {
                    SetMuteAudio(TypeValueChange.Music,popupSetting.MusicMute);
                }).AddTo(popupSetting);
                
                popupSetting.SoundSlider.onValueChanged.AsObservable().Subscribe( value =>
                {
                    SetValueChange(value,TypeValueChange.Sound);
                }).AddTo(popupSetting);

                popupSetting.SoundMute.onClick.AsObservable().Subscribe((_) =>
                {
                    SetMuteAudio(TypeValueChange.Sound,popupSetting.SoundMute);
                }).AddTo(popupSetting);
            }
        }

        private void SetMuteAudio(TypeValueChange type, Button btn)
        {
            AudioPlayer.Mute(type);
            _saveLoadService.OnPlayerDataChanged.OnNext(Unit.Default);
        }

        private void SetValueChange(float value, TypeValueChange type)
        {
            AudioPlayer.ChangeValue(value, type);
            _saveLoadService.OnPlayerDataChanged.OnNext(Unit.Default);
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
            
            _saveLoadService.SaveProgress(true);
        }

        private void InitializedInventoryItem()
        {
            if (_popupService.GetPopup(TypePopup.Inventory) is InventoryPopup popup)
                for (int i = 0; i < _styleData.Length; i++)
                {
                    ItemInventoryStyle item =_factory.MetaFactoryItem.CreateItem(_poolItemInventoryStyle.GetItem(), popup);
                    InitEventButtonClick(item.Btn, popup.gameObject, (() =>  EnterStyle(item)));
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
                    ItemShop item = _factory.MetaFactoryItem.CreateItem(_poolItemSellUi.GetItem(), popup);
                    InitEventButtonClick(item.Btn, popup.gameObject, (() => BuyStyle(item.Id, item.ActiveGameObject, item.Type)));
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
    }
}
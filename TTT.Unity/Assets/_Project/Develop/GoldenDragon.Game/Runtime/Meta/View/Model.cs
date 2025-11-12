using System.Collections.Generic;
using System.Data;
using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.InventoryItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.LeaderBoardItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup.ShopElementItem;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SessionData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UniRx;
using UnityEngine;
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
        private ProviderUiFactory _uiFactory;
        private SaveLoadService _saveLoadService;
        private SessionDataMatch _sessionDataMatch;
        private MetaFlow _metaFlow;
        private IPlayerProgress _playerData;
        private bool _isShopLoadData;

        [Inject]
        public void Construct(
            PopupService popupService,
            IPlayerProgress playerData, 
            ProviderUiFactory providerUiFactory,
            SaveLoadService saveLoadService,SessionDataMatch sessionDataMatch)
        {
            _sessionDataMatch = sessionDataMatch;
            _saveLoadService = saveLoadService;
            _uiFactory = providerUiFactory;
            _playerData = playerData;
            _popupService = popupService;
        }

        public UniTask Initialized(MetaRoot metaRoot,
            Style.StyleData[] styleData,
            PoolUiItem<ItemShop> poolItemSellUi,
            PoolUiItem<ItemInventoryStyle> poolItemInventoryStyle, MetaFlow metaFlow)
        {
            _metaFlow = metaFlow;
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

            if (_popupService.TryOpenPopup(TypePopup.Setting, out SettingPopup settingPopup))
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

            if (_popupService.TryOpenPopup(TypePopup.LeaderBoard, out LeaderBoardPopup leaderBoardPopup))
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

            if (_popupService.TryOpenPopup(TypePopup.Match, out MatchPopup matchPopup))
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

            if (_popupService.TryOpenPopup(TypePopup.Inventory, out InventoryPopup inventoryPopup))
                popup = inventoryPopup;
            else
                return;
            
            OnShowItemInventor(TypeShowItemStyle.Board);

            _popupBackground.Show();
            popup.Show();
        }

        public void OpenPopupShop()
        {
            AudioPlayer.Click();
            ShopPopup popup;

            if (_popupService.TryOpenPopup(TypePopup.Shop, out ShopPopup shopPopup))
                popup = shopPopup;
            else
                return;
            
            OnShowItemShop(TypeShowItemStyle.Board);

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

        private void OnStartMatch(TypeSessionMatch typeSessionMatch)
        {
            _sessionDataMatch.SetTypeSession(typeSessionMatch);
            _metaFlow.StartMatch();
        }

        private void OnShowItemShop(TypeShowItemStyle type)
        {
            _poolItemSellUi.Reset();

            foreach (Style.StyleData data in _styleData)
            {
                if (data.Type != type.ToString() ||
                    data.Id == Constant.StyleData.GetDefaultType(type))
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

        private void OnShowItemInventor(TypeShowItemStyle type)
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
            if (_popupService.TryGetPopup<ShopPopup>(TypePopup.Shop,out ShopPopup popupShop))
            {
                EventUniRx.CreateEventButtonClick(popupShop.BtnBorder, popupShop.gameObject,
                    () => { OnShowItemShop(TypeShowItemStyle.Board); });
                EventUniRx.CreateEventButtonClick(popupShop.BtnX, popupShop.gameObject,
                    () => { OnShowItemShop(TypeShowItemStyle.X); });
                EventUniRx.CreateEventButtonClick(popupShop.BtnO, popupShop.gameObject,
                    () => { OnShowItemShop(TypeShowItemStyle.O); });
            }

            if (_popupService.TryGetPopup<InventoryPopup>(TypePopup.Inventory,out InventoryPopup popupInventory))
            {
                EventUniRx.CreateEventButtonClick(popupInventory.BtnBoard, popupInventory.gameObject,
                    () => { OnShowItemInventor(TypeShowItemStyle.Board); });
                EventUniRx.CreateEventButtonClick(popupInventory.BtnX, popupInventory.gameObject,
                    () => { OnShowItemInventor(TypeShowItemStyle.X); });
                EventUniRx.CreateEventButtonClick(popupInventory.BtnO, popupInventory.gameObject,
                    () => { OnShowItemInventor(TypeShowItemStyle.O); });
            }
            
            if (_popupService.TryGetPopup<SettingPopup>(TypePopup.Setting,out SettingPopup popupSetting))
            {
                popupSetting.MusicSlider.onValueChanged.AsObservable().Subscribe( value =>
                {
                    OnSetValueChange(value,TypeValueChange.Music);
                }).AddTo(popupSetting);

                popupSetting.MusicMute.onClick.AsObservable().Subscribe((_) =>
                {
                    OnSetMuteAudio(TypeValueChange.Music);
                }).AddTo(popupSetting);
                
                popupSetting.SoundSlider.onValueChanged.AsObservable().Subscribe( value =>
                {
                    OnSetValueChange(value,TypeValueChange.Sound);
                }).AddTo(popupSetting);

                popupSetting.SoundMute.onClick.AsObservable().Subscribe((_) =>
                {
                    OnSetMuteAudio(TypeValueChange.Sound);
                }).AddTo(popupSetting);
            }

            if (_popupService.TryGetPopup<MatchPopup>(TypePopup.Match,out MatchPopup matchPopup))
            {
                matchPopup.X.onClick.AsObservable().Subscribe((_) => { OnStartMatch(TypeSessionMatch.X); }).AddTo(matchPopup);
                matchPopup.O.onClick.AsObservable().Subscribe((_) => { OnStartMatch(TypeSessionMatch.O);}).AddTo(matchPopup);
                matchPopup.Coop.onClick.AsObservable().Subscribe((_) => {OnStartMatch(TypeSessionMatch.Coop); }).AddTo(matchPopup);
            }
        }

        private void OnSetMuteAudio(TypeValueChange type)
        {
            AudioPlayer.Mute(type);
            _saveLoadService.OnPlayerDataChanged.OnNext(Unit.Default);
        }

        private void OnSetValueChange(float value, TypeValueChange type)
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
            if (_popupService.TryGetPopup<InventoryPopup>(TypePopup.Inventory, out InventoryPopup popup))
                for (int i = 0; i < _styleData.Length; i++)
                {
                    ItemInventoryStyle item =_uiFactory.FactoryItem.CreateItem(_poolItemInventoryStyle.GetItem(), popup);
                    EventUniRx.CreateEventButtonClick(item.Btn, popup.gameObject, (() =>  EnterStyle(item)));
                }
            else
            {
                Log.Meta.W($"Not load popup:{nameof(InventoryPopup)}");
            }
        }

        private void InitializedShopItem()
        {
            if (_popupService.TryGetPopup<ShopPopup>(TypePopup.Shop,out ShopPopup popup))
                for (int i = 0; i < _styleData.Length; i++)
                {
                    ItemShop item = _uiFactory.FactoryItem.CreateItem(_poolItemSellUi.GetItem(), popup);
                    EventUniRx.CreateEventButtonClick(item.Btn, popup.gameObject, (() => BuyStyle(item.Id, item.ActiveGameObject, item.Type)));
                }
            else
            {
                Log.Meta.W($"Not load popup:{nameof(ShopPopup)}");
            }
        }
    }
}
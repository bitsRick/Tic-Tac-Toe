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
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using StyleData = GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data.Player.StyleData;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View
{
    public class Model:ILoadUnit
    {
        private Style.StyleData[] _styleData;
        private PopupService _popupService;
        private BackPopupBackground _popupBackground;
        private MetaRoot _metaRoot;
        private PoolUiItem<ShopItem> _poolItemUi;
        private PoolUiItem<ItemStyle> _poolItemInventoryStyle;
        private ProviderUiFactory _uiFactory;
        private SaveLoadService _saveLoadService;
        private SessionDataMatch _sessionDataMatch;
        private MetaFlow _metaFlow;
        private IPlayerProfile _playerData;
        private bool _isShopLoadData;

        [Inject]
        public void Construct(
            IPlayerProfile playerData, 
            ProviderUiFactory providerUiFactory,
            SaveLoadService saveLoadService,SessionDataMatch sessionDataMatch,PopupService popupService)
        {
            _popupService = popupService;
            _sessionDataMatch = sessionDataMatch;
            _saveLoadService = saveLoadService;
            _uiFactory = providerUiFactory;
            _playerData = playerData;
        }
        
        public UniTask Resolve(MetaRoot metaRoot,
            Style.StyleData[] styleData,
            PoolUiItem<ShopItem> poolItemSellUi,
            PoolUiItem<ItemStyle> poolItemInventoryStyle,
            MetaFlow metaFlow)
        {
            _metaFlow = metaFlow;
            _poolItemInventoryStyle = poolItemInventoryStyle;
            _metaRoot = metaRoot;
            _poolItemUi = poolItemSellUi;
            _styleData = styleData;
            _popupBackground = metaRoot.GetPopupBackground();
            
            return UniTask.CompletedTask;
        }

        public UniTask Load()
        {
            InitializedShopItem();
            InitializedInventoryItem();
            InitializedEvent();
            
            return UniTask.CompletedTask;
        }

        public void OpenPopupSetting()
        {
            AudioPlayer.S.Click();
            SettingPopup popup;

            if (_popupService.TryOpenPopup(TypePopup.Setting, out SettingPopup settingPopup))
                popup = settingPopup;
            else
                return;
            
            OnSwitchIcoAudioMute(TypeValueChange.Music,popup,popup.MusicMute,_playerData.profileData.AudioSetting.IsMusicMute);
            OnSwitchIcoAudioMute(TypeValueChange.Sound,popup,popup.SoundMute,_playerData.profileData.AudioSetting.IsSoundMute);
            
            popup.MusicSlider.value = AudioPlayer.S.LoadSliderValue(TypeValueChange.Music);
            popup.SoundSlider.value = AudioPlayer.S.LoadSliderValue(TypeValueChange.Sound);

            _popupBackground.Show();
            popup.Show();
        }

        public void OpenPopupLeaderBoard()
        {
            AudioPlayer.S.Click();
            LeaderBoardPopup popup;

            if (_popupService.TryOpenPopup(TypePopup.LeaderBoard, out LeaderBoardPopup leaderBoardPopup))
                popup = leaderBoardPopup;
            else
                return;

            popup.Initialized();

            ProfileData profileData = _playerData.profileData;

            List<SimulationData.Data> dataList = SimulatorLeaderBoard.S.D;
            dataList.Add(new SimulationData.Data() { Score = profileData.Score, Name = profileData.Nick });

            var sortLeader = dataList.OrderByDescending(key => key.Score).ToArray();
            int index = -1;

            foreach (Item itemLeader in popup.Leaders)
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
            AudioPlayer.S.Click();
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
            AudioPlayer.S.Click();
            InventoryPopup popup;

            if (_popupService.TryOpenPopup(TypePopup.Inventory, out InventoryPopup inventoryPopup))
                popup = inventoryPopup;
            else
                return;
            
            OnShowItemInventor(ShowItemStyle.Board);

            _popupBackground.Show();
            popup.Show();
        }

        public void OpenPopupShop()
        {
            AudioPlayer.S.Click();
            ShopPopup popup;

            if (_popupService.TryOpenPopup(TypePopup.Shop, out ShopPopup shopPopup))
                popup = shopPopup;
            else
                return;
            
            OnShowItemShop(ShowItemStyle.Board);

            popup.HorizontalLayoutGroup.childScaleHeight = true;
            popup.HorizontalLayoutGroup.childControlHeight = true;

            _popupBackground.Show();
            popup.Show();
        }

        private void OnStartMatch(TypePlayingField typePlayingField)
        {
            _sessionDataMatch.SetTypePlayer(typePlayingField);
            _metaFlow.StartMatch();
        }

        private void OnShowItemShop(ShowItemStyle type)
        {
            _poolItemUi.Reset();

            foreach (Style.StyleData data in _styleData)
            {
                if (data.Type != type.ToString() ||
                    data.Id == RuntimeConstants.StyleData.GetDefaultType(type))
                    continue;

                ShopItem shopItem = _poolItemUi.GetItem();
                shopItem.Id = data.Id;
                shopItem.Image.sprite = data.Sprite;
                shopItem.X = data.ValueX.ToString();
                shopItem.O = data.ValueO.ToString();
                shopItem.Type = type;

                shopItem.ActiveGameObject.SetActive(_playerData
                    .profileData
                    .ShopPlayerData
                    .FirstOrDefault(key => key.Id == data.Id) != null);

                shopItem.gameObject.SetActive(true);
            }
        }

        private void OnShowItemInventor(ShowItemStyle type)
        {
            _poolItemInventoryStyle.Reset();

            var dataStyleArray = _playerData.profileData.ShopPlayerData.Where(key => key.Type == type).ToArray();

            foreach (ItemStyle item in _poolItemInventoryStyle)
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

        private void EnterStyle(ItemStyle item)
        {
            StyleData currentStylePlayer = GetStyleCurrentPlayer(item.Type);

            ItemStyle currentActiveStyle = _poolItemInventoryStyle.Find(currentStylePlayer.Id);
            currentActiveStyle.ActiveGameObject.SetActive(false);
            currentStylePlayer.Id = item.Id;

            item.ActiveGameObject.SetActive(true);
            
            _saveLoadService.OnPlayerDataChanged.OnNext(Unit.Default);
        }

        private StyleData GetStyleCurrentPlayer(ShowItemStyle type)
        {
            return type switch
            {
                ShowItemStyle.Board => _playerData.profileData.Board,
                ShowItemStyle.X => _playerData.profileData.X,
                ShowItemStyle.O => _playerData.profileData.O,
                _ => throw new DataException($"Type not found {type}")
            };
        }

        private void InitializedEvent()
        {
            if (_popupService.TryGetPopup<ShopPopup>(TypePopup.Shop,out ShopPopup popupShop))
            {
                EventUniRx.CreateEventButtonClick(popupShop.BtnBorder, popupShop.gameObject,
                    () => { OnShowItemShop(ShowItemStyle.Board); });
                EventUniRx.CreateEventButtonClick(popupShop.BtnX, popupShop.gameObject,
                    () => { OnShowItemShop(ShowItemStyle.X); });
                EventUniRx.CreateEventButtonClick(popupShop.BtnO, popupShop.gameObject,
                    () => { OnShowItemShop(ShowItemStyle.O); });
            }

            if (_popupService.TryGetPopup<InventoryPopup>(TypePopup.Inventory,out InventoryPopup popupInventory))
            {
                EventUniRx.CreateEventButtonClick(popupInventory.BtnBoard, popupInventory.gameObject,
                    () => { OnShowItemInventor(ShowItemStyle.Board); });
                EventUniRx.CreateEventButtonClick(popupInventory.BtnX, popupInventory.gameObject,
                    () => { OnShowItemInventor(ShowItemStyle.X); });
                EventUniRx.CreateEventButtonClick(popupInventory.BtnO, popupInventory.gameObject,
                    () => { OnShowItemInventor(ShowItemStyle.O); });
            }
            
            if (_popupService.TryGetPopup<SettingPopup>(TypePopup.Setting,out SettingPopup popupSetting))
            {
                popupSetting.MusicSlider.onValueChanged.AsObservable().Subscribe( value =>
                {
                    OnSetValueChange(value,TypeValueChange.Music);
                }).AddTo(popupSetting);

                popupSetting.SoundSlider.onValueChanged.AsObservable().Subscribe( value =>
                {
                    OnSetValueChange(value,TypeValueChange.Sound);
                }).AddTo(popupSetting);
                
                popupSetting.MusicMute.onClick.AsObservable().Subscribe((_) =>
                {
                    OnSetMuteAudio(TypeValueChange.Music);
                    OnSwitchIcoAudioMute(TypeValueChange.Music,popupSetting,popupSetting.MusicMute, _playerData.profileData.AudioSetting.IsMusicMute);
                    SetValueSlideAudio(TypeValueChange.Music, popupSetting, _playerData.profileData.AudioSetting.IsMusicMute);
                }).AddTo(popupSetting);
                
                popupSetting.SoundMute.onClick.AsObservable().Subscribe((_) =>
                {
                    OnSetMuteAudio(TypeValueChange.Sound);
                    OnSwitchIcoAudioMute(TypeValueChange.Sound,popupSetting,popupSetting.SoundMute, _playerData.profileData.AudioSetting.IsSoundMute);
                    SetValueSlideAudio(TypeValueChange.Sound, popupSetting, _playerData.profileData.AudioSetting.IsSoundMute);
                }).AddTo(popupSetting);
            }

            if (_popupService.TryGetPopup(TypePopup.Match,out MatchPopup matchPopup))
            {
                matchPopup.X.onClick.AsObservable().Subscribe((_) => { OnStartMatch(TypePlayingField.X); }).AddTo(matchPopup);
                matchPopup.O.onClick.AsObservable().Subscribe((_) => { OnStartMatch(TypePlayingField.O);}).AddTo(matchPopup);
            }
        }

        private void OnSwitchIcoAudioMute(TypeValueChange valueChange, SettingPopup popupSetting,
            Button btn, bool playerAudioStateMute)
        {
            switch (valueChange)
            {
                case TypeValueChange.Sound:
                    SwitchImageButtonAudioMute(popupSetting.SoundOn, popupSetting.SoundOff,btn, playerAudioStateMute);
                    break;
                
                case TypeValueChange.Music:
                    SwitchImageButtonAudioMute(popupSetting.MusicOn, popupSetting.MusicOff,btn, playerAudioStateMute);
                    break;
            }
        }

        private static void SetValueSlideAudio(TypeValueChange valueChange, SettingPopup popupSetting,
            bool playerAudioStateMute)
        {
            Slider slider = valueChange == TypeValueChange.Music
                ? popupSetting.MusicSlider
                : popupSetting.SoundSlider;

            if (playerAudioStateMute == false)
                slider.value = AudioPlayer.S.LoadSliderValue(valueChange);
            else
                slider.value = 0;
        }

        private void SwitchImageButtonAudioMute(Image musicOn, Image musicOff, Button btn, bool playerAudioStateMute)
        {
            musicOn.gameObject.SetActive(false);
            musicOff.gameObject.SetActive(false);
            
            Image imageToActive =
                playerAudioStateMute
                    ? musicOff
                    : musicOn;
            
            btn.targetGraphic = imageToActive;
            imageToActive.gameObject.SetActive(true);
        }

        private void OnSetMuteAudio(TypeValueChange type)
        {
            AudioPlayer.S.Mute(type);
            _saveLoadService.OnPlayerDataChanged.OnNext(Unit.Default);
        }

        private void OnSetValueChange(float value, TypeValueChange type)
        {
            AudioPlayer.S.ChangeValue(value, type);
            _saveLoadService.OnPlayerDataChanged.OnNext(Unit.Default);
        }

        private void BuyStyle(string id, GameObject gameObject, ShowItemStyle style)
        {
            Style.StyleData data = _styleData.FirstOrDefault(key => key.Id == id);

            if (data == null)
                return;

            if (data.ValueX > _playerData.profileData.SoftValueX ||
                data.ValueO > _playerData.profileData.SoftValueO)
                return;

            _playerData.profileData.SoftValueX -= data.ValueX;
            _playerData.profileData.SoftValueO -= data.ValueO;

            _metaRoot.OnSoftValueChanged.OnNext(Unit.Default);
            _playerData.profileData.ShopPlayerData.Add(new StyleData() { Id = id, Type = style });
            gameObject.SetActive(true);
            
            _saveLoadService.SaveProgress(true);
        }

        private void InitializedInventoryItem()
        {
            if (_popupService.TryGetPopup<InventoryPopup>(TypePopup.Inventory, out InventoryPopup popup))
                for (int i = 0; i < _styleData.Length; i++)
                {
                    ItemStyle item =_uiFactory.FactoryItem.CreateItem(_poolItemInventoryStyle.GetItem(), popup);
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
                    ShopItem shopItem = _uiFactory.FactoryItem.CreateItem(_poolItemUi.GetItem(), popup);
                    EventUniRx.CreateEventButtonClick(shopItem.Btn, popup.gameObject, (() => BuyStyle(shopItem.Id, shopItem.ActiveGameObject, shopItem.Type)));
                }
            else
            {
                Log.Meta.W($"Not load popup:{nameof(ShopPopup)}");
            }
        }
    }
}
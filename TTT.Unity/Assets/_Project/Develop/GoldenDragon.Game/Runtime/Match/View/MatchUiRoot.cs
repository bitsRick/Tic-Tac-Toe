using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.TopInformation;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View
{
    public class MatchUiRoot : MonoBehaviour, IUiRoot, ILoadUnit
    {
        [Header("Контейнер игрового поля")]
        [SerializeField] private PlayingField _playingField;

        [Header("Настройки")] 
        [SerializeField] private Button _setting;

        [Header("Визуальнаые данные игроков")] 
        [SerializeField] private DataMatchPlayerUi _playerVisualDataLeft;
        [SerializeField] private DataMatchPlayerUi _playerVisualDataRight;

        [Header("Контейнер для окон")] 
        [SerializeField] private GameObject _parrent;

        [Header("Темный экран для окон")] 
        [SerializeField] private BackPopupBackground _popupBackground;

        private CharacterMatch _botMatchData;
        private CharacterMatch _playerMatchData;
        private PopupService _popupService;
        private AssetService _assetService;
        private ProviderUiFactory _providerUiFactory;
        
        public void Constructor(CharacterMatch botMatchData, CharacterMatch playerMatchData,
            PopupService popupService, AssetService assetService,
            ProviderUiFactory providerUiFactory)
        {
            _providerUiFactory = providerUiFactory;
            _assetService = assetService;
            _popupService = popupService;
            _playerMatchData = playerMatchData;
            _botMatchData = botMatchData;
        }

        public PlayingField GetPlayingField()
        {
            return _playingField;
        }

        public async UniTask Load()
        {
            InitDataView(_playerVisualDataLeft, _playerMatchData);
            InitDataView(_playerVisualDataRight, _botMatchData);
            
            await InitializedPlayingField();
            await InitializedPopup();
            await InitializedEvent();
            
            await UniTask.CompletedTask;
        }

        public async UniTask InitializedPopup()
        {
            GameObject settingObject =
                await _providerUiFactory.FactoryUi.LoadPopupToObject(Constant.M.Asset.Popup.Setting);
            SettingPopup settingPopup = _assetService.Install.InstallToUiPopup<SettingPopup>(settingObject, _parrent);
            _popupService.AddPopupInList(TypePopup.Setting, settingPopup);

            await UniTask.CompletedTask;
        }

        public UniTask InitializedEvent()
        {
            _setting.onClick.AsObservable().Subscribe((_) => { OpenSetting(); });

            _popupBackground.OnEvenPointClickBackground.Subscribe((_) => _popupService.Close());
            return UniTask.CompletedTask;
        }

        public UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        private void OpenSetting()
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

        private void InitDataView(DataMatchPlayerUi viewData, CharacterMatch matchData)
        {
            viewData.Name.text = matchData.Name;
        }

        private async UniTask InitializedPlayingField()
        {
            var fields = _playingField.Fields;

            TypePositionElementToField[] enumValues = Enum.GetValues(typeof(TypePositionElementToField))
                .Cast<TypePositionElementToField>()
                .ToArray();
            
            for (var i = 0; i < fields.GetLength(0); i++)
            {
                Field field = fields[i];

                field.Click.onClick.AsObservable().Subscribe((_) => { OnSetTypeInField(field); }).AddTo(this);

                TypePositionElementToField type = enumValues[i];

                field.Initialized(type);
                await field.Load();
            }
        }

        private void OnSetTypeInField(Field field)
        {
            if (field.TrySetElement(_playerMatchData.Field))
                _playingField.OnPlayerActionEnd.OnNext(Unit.Default);
        }
    }

    public interface IUiRoot
    {
        UniTask InitializedPopup();
        UniTask InitializedEvent();
        UniTask Show();
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Audio;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Factory.Ui;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.TopInformation;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Meta.View.Popup;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Service;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.UI;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View
{
    public class MatchUiRoot : MonoBehaviour, IUiRoot, ILoadUnit
    {
        [Header("Контейнер игрового поля")] 
        [SerializeField]private PlayingField _playingField;
        
        [Header("Настройки")]
        [SerializeField] private Button _setting;
        
        [Header("Визуальнаые данные игроков")] 
        [SerializeField]private DataMatchPlayerUi _playerVisualDataLeft;
        [SerializeField]private DataMatchPlayerUi _playerVisualDataRight;
        
        [Header("Контейнер для окон")] 
        [SerializeField]private GameObject _parrent;
        
        [Header("Темный экран для окон")] 
        [SerializeField]private BackPopupBackground _popupBackground;

        private CharacterMatchData _botMatchDataData;
        private CharacterMatchData _playerMatchData;
        private PopupService _popupService;
        private AssetService _assetService;
        private ProviderUiFactory _providerUiFactory;

        public Subject<Unit> OnPlayerActionEnd = new Subject<Unit>();
        public Subject<Unit> OnBotActionEnd = new Subject<Unit>();

        public PlayingField PlayingField => _playingField;

        public void Constructor(CharacterMatchData botMatchDataData, CharacterMatchData playerMatchDataData,
            PopupService popupService, AssetService assetService,
            ProviderUiFactory providerUiFactory)
        {
            _providerUiFactory = providerUiFactory;
            _assetService = assetService;
            _popupService = popupService;
            _playerMatchData = playerMatchDataData;
            _botMatchDataData = botMatchDataData;
        }
        
        public async UniTask Load()
        {
            InitDataView(_playerVisualDataLeft, _playerMatchData);
            InitDataView(_playerVisualDataRight, _botMatchDataData);

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
            _setting.onClick.AsObservable().Subscribe((_) => { OpenSetting(); }).AddTo(this);
            _popupBackground.OnEvenPointClickBackground.Subscribe((_) => _popupService.Close()).AddTo(this);

            return UniTask.CompletedTask;
        }

        public UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public void SetField(CharacterMatchData character, Field botActionField)
        {
            OnSetTypeInField(character, botActionField);
        }

        public void OnMouseEnterField(Field fieldData)
        {
            SetViewField(fieldData, true);
        }

        public void OnMouseExitField(Field fieldData)
        {
            SetViewField(fieldData, false);
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

        private void InitDataView(DataMatchPlayerUi viewData, CharacterMatchData matchDataData)
        {
            viewData.Name.text = matchDataData.Name;
        }

        private void OnSetTypeInField(CharacterMatchData characterMatchData, Field field)
        {
            if (TrySetField(characterMatchData, field))
            {
                if (characterMatchData.IsBot)
                    OnBotActionEnd.OnNext(Unit.Default);
                else
                    OnPlayerActionEnd.OnNext(Unit.Default);
            }
        }

        private async UniTask InitializedPlayingField()
        {
            TypePositionElementToField[] enumValues = Enum.GetValues(typeof(TypePositionElementToField))
                .Cast<TypePositionElementToField>()
                .ToArray();

            for (var i = 0; i < _playingField.Fields.GetLength(0); i++)
            {
                TypePositionElementToField type = enumValues[i];

                Field field = _playingField.Fields[i];
                field.X.gameObject.SetActive(false);
                field.O.gameObject.SetActive(false);

                field.Initialized(type, this);
                field.Btn.onClick.AsObservable().Subscribe((_) => { OnSetTypeInField(_playerMatchData, field); })
                    .AddTo(this);

                await Task.CompletedTask;
            }
        }

        private void SetViewField(Field data, bool isView)
        {
            if (data.CurrentPlayingField != TypePlayingField.None)
                return;

            Image typeFieldImage = _playerMatchData.Field == TypePlayingField.X ? data.X : data.O;
            typeFieldImage.gameObject.SetActive(isView);

            data.Btn.targetGraphic = isView ? typeFieldImage : data.Empty;
        }

        private bool TrySetField(CharacterMatchData characterAction, Field field)
        {
            if (field.CurrentPlayingField != TypePlayingField.None)
                return false;

            Image typeFieldImage = characterAction.Field == TypePlayingField.X ? field.X : field.O;
            typeFieldImage.gameObject.SetActive(true);

            field.CurrentPlayingField =
                characterAction.Field == TypePlayingField.X ? TypePlayingField.X : TypePlayingField.O;
            field.Btn.targetGraphic = null;

            return true;
        }
    }
}
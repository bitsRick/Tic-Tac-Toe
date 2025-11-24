using System;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View.Style;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using UniRx;
using UnityEngine.UI;
using VContainer;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View
{
    public class ModulePlayingField:ILoadUnit<StyleMatchData>
    {
        private PlayingField _playingField;
        private CharacterMatchData _playerMatchData;
        private CharacterMatchData _botMatchDataData;
        private RoundManager _roundManager;
        private MatchUiRoot _matchUiRoot;

        [Inject]
        public ModulePlayingField(RoundManager roundManager)
        {
            _roundManager = roundManager;
        }

        public UniTask Initialized(PlayingField playingField,CharacterMatchData playerData,CharacterMatchData botData,MatchUiRoot matchUiRoot)
        {
            _playingField = playingField;
            _playerMatchData = playerData;
            _botMatchDataData = botData;
            _matchUiRoot = matchUiRoot;
            
            return UniTask.CompletedTask;
        }

        public void Reset()
        {
            ResetFields();
        }

        public async UniTask Load(StyleMatchData styleMatchData)
        {
            _playingField.Border.sprite = styleMatchData.Board;
            
            PositionElementToField[] enumValues = Enum.GetValues(typeof(PositionElementToField))
                .Cast<PositionElementToField>()
                .ToArray();

            for (var i = 0; i < _playingField.Fields.GetLength(0); i++)
            {
                PositionElementToField type = enumValues[i];

                Field field = _playingField.Fields[i];
                field.X.gameObject.SetActive(false);
                field.O.gameObject.SetActive(false);

                field.X.sprite = styleMatchData.X;
                field.O.sprite = styleMatchData.O;
                
                field.Initialized(type, this);
                field.Btn.onClick.AsObservable().Subscribe((_) =>
                    {
                        SetTypeInFieldTurn(_playerMatchData, field);
                    })
                    .AddTo(_matchUiRoot);
            }
            
            await Task.CompletedTask;
        }

        public void SetInteractiveFieldButton(bool isFlag)
        {
            foreach (Field field in _playingField.Fields) 
                field.Btn.interactable = isFlag;
        }

        public void OnMouseEnterField(Field fieldData) => SetViewField(fieldData, true);

        public void OnMouseExitField(Field fieldData) => SetViewField(fieldData, false);

        public void SetWinMatch(MatchWin winner)
        {
            ResetFields();
            
            if (winner == MatchWin.None)
            {
                _roundManager.Reset();
                _roundManager.Start();
                return;
            }

            if (_roundManager.RoundData.IsNotEndWin(_playerMatchData,_botMatchDataData))
            {
                _roundManager.Reset();
                _matchUiRoot.SetViewWin(winner);
                _roundManager.Start();
            }
            else
            {
                _matchUiRoot.OpenWinLose(winner);
            }
        }

        public void SetTypeInFieldTurn(CharacterMatchData characterMatchData, Field field)
        {
            _matchUiRoot.SetActiveViewColor(characterMatchData);
            
            if (TrySetField(characterMatchData, field)) 
                _roundManager.NextTurn();
        }

        private void ResetFields()
        {
            foreach (Field field in _playingField.Fields)
            {
                field.CurrentPlayingField = TypePlayingField.None;
                field.O.gameObject.SetActive(false);
                field.X.gameObject.SetActive(false);
                field.Btn.targetGraphic = field.Empty;
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
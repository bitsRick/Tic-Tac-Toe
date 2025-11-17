using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round
{
    public class WinService:ILoadUnit
    {
        private const int WinCount = 3;
        private CharacterMatchData _bot;
        private CharacterMatchData _player;
        private MatchUiRoot _matchUiRoot;
        private Field[] _fieldFields;
        private List<Field> _horizontalTopLineFields;
        private List<Field> _horizontalBottomLineFields;
        private List<Field> _horizontalMiddleFields;
        private List<Field> _verticalCenterLineFields;
        private List<Field> _verticalLeftLineFields;
        private List<Field> _verticalRightLineFields;
        private List<Field> _backSlashFields;
        private List<Field> _slashFields;
        private MatchWin _matchWin = MatchWin.None;

        public WinService(CharacterMatchData bot, CharacterMatchData player, MatchUiRoot matchUiRoot)
        {
            _bot = bot;
            _player = player;
            _matchUiRoot = matchUiRoot;
        }
        
        public UniTask Load()
        {            
            _fieldFields = _matchUiRoot.PlayingField.Fields;
            
            _horizontalTopLineFields = _fieldFields.Where(x => MathTypeFind.GetHorizontalTopLine(x.Position)).ToList();
            _horizontalBottomLineFields = _fieldFields.Where(x => MathTypeFind.GetHorizontalBottomLine(x.Position)).ToList();
            _horizontalMiddleFields = _fieldFields.Where(x => MathTypeFind.GetHorizontalMiddleLine(x.Position)).ToList();
            _verticalCenterLineFields = _fieldFields.Where(x => MathTypeFind.GetVerticalCenterLine(x.Position)).ToList();
            _verticalLeftLineFields = _fieldFields.Where(x => MathTypeFind.GetVerticalLeftLine(x.Position)).ToList();
            _verticalRightLineFields = _fieldFields.Where(x => MathTypeFind.GetVerticalRightLine(x.Position)).ToList();
            _backSlashFields = _fieldFields.Where(x => MathTypeFind.GetBackslash(x.Position)).ToList();
            _slashFields = _fieldFields.Where(x => MathTypeFind.GetSlash(x.Position)).ToList();
            
            return UniTask.CompletedTask;
        }

        public bool TryGetMatchWin(out MatchWin matchMode)
        {
            matchMode = GetCharacterMatchWin(
                _horizontalTopLineFields, _horizontalBottomLineFields, _horizontalMiddleFields,
                _verticalCenterLineFields, _verticalLeftLineFields, _verticalRightLineFields,
                _backSlashFields, _slashFields
            );

          return matchMode != MatchWin.None;
        }

        private MatchWin GetCharacterMatchWin(params List<Field>[] listsFields)
        {
            int playerField = 0;
            int botField = 0;
            
            foreach (List<Field> fields in listsFields)
            {
                foreach (Field field in fields)
                {
                    if (field.CurrentPlayingField == _player.Field) playerField++;
                    if (field.CurrentPlayingField == _bot.Field) botField++;
                }

                if (playerField == WinCount)
                    return MatchWin.Player;

                if (botField == WinCount)
                    return MatchWin.Bot;

                botField = 0;
                playerField = 0;
            }

            return MatchWin.None;
        }
    }
}
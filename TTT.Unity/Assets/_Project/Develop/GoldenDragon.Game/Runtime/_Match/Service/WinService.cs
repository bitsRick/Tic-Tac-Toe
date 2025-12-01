using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Service
{
    public class WinService:IDisposableLoadUnit
    {
        private readonly CharacterMatchData _bot;
        private readonly CharacterMatchData _player;
        private readonly MatchUiRoot _matchUiRoot;
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
        
        public void Dispose()
        {
            _horizontalTopLineFields = null;
            _horizontalBottomLineFields = null;
            _horizontalMiddleFields = null;
            _verticalCenterLineFields = null;
            _verticalLeftLineFields = null;
            _verticalRightLineFields = null;
            _backSlashFields = null;
            _slashFields = null;
        }

        public bool TryGetMatchWin(RoundData roundData, out MatchWin matchMode)
        {
            matchMode = GetCharacterMatchWin(
                _horizontalTopLineFields, _horizontalBottomLineFields, _horizontalMiddleFields,
                _verticalCenterLineFields, _verticalLeftLineFields, _verticalRightLineFields,
                _backSlashFields, _slashFields
            );


            if (matchMode == MatchWin.None
                &&roundData.CountSetField >= RuntimeConstants.Match.MaxCountSetField)
            {
                matchMode = MatchWin.None;
                return true;
            }

            if (matchMode != MatchWin.None)
                return true;

            return false;
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

                if (playerField == RuntimeConstants.Match.MaxWinMatch)
                    return MatchWin.Player;

                if (botField == RuntimeConstants.Match.MaxWinMatch)
                    return MatchWin.Bot;

                botField = 0;
                playerField = 0;
            }

            return MatchWin.None;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.View;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class CalcScore
    {
        private const int DefaultScore = 100;
        private const int UltraScore = 150;
        private PlayingField _playingField;

        public CalcScore(UtilityAi matchUi)
        {
            _playingField = matchUi.GetPlayingField();
        }

        public Func<TypePositionElementWin, CharacterMatchData, Field, float> ScaleBy(int scoreUp)
        {
            return (position, bot, field) =>
            {
                float score = 0;

                if (position == TypePositionElementWin.None)
                    return 0;

                foreach (Field fieldLazy in GetFields(position))
                {
                    if (field.Position == fieldLazy.Position)
                        continue;

                    if (fieldLazy.CurrentPlayingField == bot.Field)
                    {
                        score *= UltraScore;
                        continue;
                    }

                    if (fieldLazy.CurrentPlayingField == TypePlayingField.None)
                        score += DefaultScore;
                    else
                        score -= DefaultScore;
                }

                return score * scoreUp;
            };
        }

        private IEnumerable<Field> GetFields(TypePositionElementWin position)
        {
            switch (position)
            {
                case TypePositionElementWin.None:
                    break;

                case TypePositionElementWin.HorizontalTopLine:
                    return _playingField.Fields.Where(x => MathTypeFind.GetHorizontalTopLine(x.Position));

                case TypePositionElementWin.HorizontalMiddleLine:
                    return _playingField.Fields.Where(x => MathTypeFind.GetHorizontalMiddleLine(x.Position));

                case TypePositionElementWin.HorizontalBottomLine:
                    return _playingField.Fields.Where(x => MathTypeFind.GetHorizontalBottomLine(x.Position));

                case TypePositionElementWin.VerticalLeftLine:
                    return _playingField.Fields.Where(x => MathTypeFind.GetVerticalLeftLine(x.Position));

                case TypePositionElementWin.VerticalCenterLine:
                    return _playingField.Fields.Where(x => MathTypeFind.GetVerticalCenterLine(x.Position));

                case TypePositionElementWin.VerticalRightLine:
                    return _playingField.Fields.Where(x => MathTypeFind.GetVerticalRightLine(x.Position));

                case TypePositionElementWin.Slash:
                    return _playingField.Fields.Where(x => MathTypeFind.GetSlash(x.Position));

                case TypePositionElementWin.Backslash:
                    return _playingField.Fields.Where(x => MathTypeFind.GetBackslash(x.Position));
            }

            return null;
        }
    }
}
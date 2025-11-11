using System;
using System.Collections.Generic;
using System.Linq;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class CalcScore
    {
        private const int StandartScore = 100;
        private const int UltraScore = 150;
        private PlayingField _playingField;

        public CalcScore(PlayingField playingField)
        {
            _playingField = playingField;
        }

        public Func<TypePositionElementWin,Bot,Field,float> ScaleBy(int scoreUp)
        {
            return (position, bot, field) =>
            {
                float score = 0;

                foreach (Field fieldLazy in GetFields(position))
                {
                    if (field.Position == fieldLazy.Position)
                        continue;

                    if (fieldLazy.Type == bot.Type)
                    {
                        score += UltraScore;
                    }
                    else
                    {
                        if (fieldLazy.Type == TypePlayingField.None)
                            score += StandartScore;
                        else
                            score -= StandartScore;
                    }
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
                    return _playingField.Fields.Where(x =>
                        x.Position 
                            is TypePositionElementToField.TopCenter 
                            or TypePositionElementToField.TopLeft 
                            or TypePositionElementToField.TopRight
                    );
                
                case TypePositionElementWin.HorizontalMiddleLine:
                    return _playingField.Fields.Where(x =>
                        x.Position 
                            is TypePositionElementToField.MiddleLeft
                            or TypePositionElementToField.MiddleRight 
                            or TypePositionElementToField.MiddleCenter
                    );
                
                case TypePositionElementWin.HorizontalBottomLine:
                    return _playingField.Fields.Where(x =>
                        x.Position 
                            is TypePositionElementToField.BottomLeft 
                            or TypePositionElementToField.BottomRight 
                            or TypePositionElementToField.BottomCenter
                    );
                
                case TypePositionElementWin.VerticalLeftLine:
                    return _playingField.Fields.Where(x =>
                        x.Position 
                            is TypePositionElementToField.TopLeft 
                            or TypePositionElementToField.MiddleLeft 
                            or TypePositionElementToField.BottomLeft
                    );
                
                case TypePositionElementWin.VerticalCenterLine:
                    return _playingField.Fields.Where(x =>
                        x.Position 
                            is TypePositionElementToField.BottomCenter 
                            or TypePositionElementToField.MiddleCenter 
                            or TypePositionElementToField.TopCenter
                    );
                
                case TypePositionElementWin.VerticalRightLine:
                    return _playingField.Fields.Where(x =>
                        x.Position 
                            is TypePositionElementToField.BottomRight 
                            or TypePositionElementToField.MiddleRight 
                            or TypePositionElementToField.TopRight
                    );
                
                case TypePositionElementWin.Slash:
                    return _playingField.Fields.Where(x =>
                        x.Position 
                            is TypePositionElementToField.TopLeft 
                            or TypePositionElementToField.MiddleCenter 
                            or TypePositionElementToField.BottomRight
                    );
                
                case TypePositionElementWin.Backslash:
                    return _playingField.Fields.Where(x =>
                        x.Position 
                            is TypePositionElementToField.TopRight 
                            or TypePositionElementToField.MiddleCenter 
                            or TypePositionElementToField.BottomLeft
                    );
            }

            return null;
        }
    }
}
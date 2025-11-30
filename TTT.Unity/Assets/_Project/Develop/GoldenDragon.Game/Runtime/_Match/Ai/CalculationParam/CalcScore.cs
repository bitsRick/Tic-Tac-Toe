using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Ai.CalculationParam
{
    public class CalcScore:ILoadUnit<UtilityAi>
    {
        private PlayingField _playingField;
        
        public UniTask Load(UtilityAi utilityAi)
        {
            _playingField = utilityAi.GetPlayingField();
            return UniTask.CompletedTask;
        }

        public Func<PositionElementWin, CharacterMatchData, Field, float> ScaleBySlash(int scoreUp)
        {
            return (position, bot, currentField) =>
            {
                if (position == PositionElementWin.None)
                    return 0;

                int score = 1;
                int playerCount = 0;
                TypePlayingField player = bot.Field == TypePlayingField.X ? TypePlayingField.O : TypePlayingField.X;
                
                if (currentField.Position == PositionElementToField.MiddleCenter && 
                    currentField.CurrentPlayingField == TypePlayingField.None) 
                    score *= Constant.Ai.SlashUltraScore;
                
                foreach (Field field in GetFields(position))
                {
                    if (field.Position == currentField.Position)
                        continue;

                    if (field.CurrentPlayingField == TypePlayingField.None) 
                        score += Constant.Ai.SlashUltraScore;

                    if (field.CurrentPlayingField == player)
                    {
                        score -= Constant.Ai.SlashUltraScore;
                        playerCount++;
                    }

                    if (field.CurrentPlayingField == bot.Field) 
                        score += Constant.Ai.SlashUltraScore;

                    if (playerCount == 2) 
                        score += Constant.Ai.SlashUltraScore * 2;
                }

                return score * scoreUp;
            };
        }

        public Func<PositionElementWin, CharacterMatchData, Field, float> ScaleByDefault(int scoreUp)
        {
            return (position, bot, field) =>
            {
                float score = 0;

                if (position == PositionElementWin.None)
                    return 0;

                foreach (Field fieldLazy in GetFields(position))
                {
                    if (field.Position == fieldLazy.Position)
                        continue;

                    if (fieldLazy.CurrentPlayingField == bot.Field)
                    {
                        score *= Constant.Ai.UltraScore;
                        continue;
                    }

                    if (fieldLazy.CurrentPlayingField == TypePlayingField.None)
                        score += Constant.Ai.DefaultScore;
                    else
                        score -= Constant.Ai.DefaultScore;
                }

                return score * scoreUp;
            };
        }

        private List<Field> GetFields(PositionElementWin position)
        {
            switch (position)
            {
                case PositionElementWin.None:
                    break;

                case PositionElementWin.HorizontalTopLine:
                    return _playingField.Fields.Where(x => MathTypeFind.GetHorizontalTopLine(x.Position)).ToList();

                case PositionElementWin.HorizontalMiddleLine:
                    return _playingField.Fields.Where(x => MathTypeFind.GetHorizontalMiddleLine(x.Position)).ToList();

                case PositionElementWin.HorizontalBottomLine:
                    return _playingField.Fields.Where(x => MathTypeFind.GetHorizontalBottomLine(x.Position)).ToList();

                case PositionElementWin.VerticalLeftLine:
                    return _playingField.Fields.Where(x => MathTypeFind.GetVerticalLeftLine(x.Position)).ToList();

                case PositionElementWin.VerticalCenterLine:
                    return _playingField.Fields.Where(x => MathTypeFind.GetVerticalCenterLine(x.Position)).ToList();

                case PositionElementWin.VerticalRightLine:
                    return _playingField.Fields.Where(x => MathTypeFind.GetVerticalRightLine(x.Position)).ToList();

                case PositionElementWin.Slash:
                    return _playingField.Fields.Where(x => MathTypeFind.GetSlash(x.Position)).ToList();

                case PositionElementWin.Backslash:
                    return _playingField.Fields.Where(x => MathTypeFind.GetBackslash(x.Position)).ToList();
            }

            return null;
        }
    }
}
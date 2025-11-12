using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class CalcGetInput
    {
        public TypePositionElementWin HorizontalTopLine(BotMatchData botMatchData, Field field)
        {
            return field.Position is 
                TypePositionElementToField.TopCenter or 
                TypePositionElementToField.TopLeft or
                TypePositionElementToField.TopRight ? TypePositionElementWin.HorizontalTopLine : TypePositionElementWin.None;
        }

        public TypePositionElementWin HorizontalMiddleLine(BotMatchData botMatchData, Field field)
        {
            return field.Position is 
                TypePositionElementToField.MiddleCenter or 
                TypePositionElementToField.MiddleLeft or
                TypePositionElementToField.MiddleRight ? TypePositionElementWin.HorizontalMiddleLine : TypePositionElementWin.None;
        }

        public TypePositionElementWin HorizontalBottomLine(BotMatchData botMatchData, Field field)
        {
            return field.Position is 
                TypePositionElementToField.BottomCenter or 
                TypePositionElementToField.BottomLeft or
                TypePositionElementToField.BottomRight ? TypePositionElementWin.HorizontalBottomLine : TypePositionElementWin.None;
        }

        public TypePositionElementWin VerticalLeftLine(BotMatchData botMatchData, Field field)
        {
            return field.Position is 
                TypePositionElementToField.TopLeft or 
                TypePositionElementToField.BottomLeft or
                TypePositionElementToField.MiddleLeft ? TypePositionElementWin.VerticalLeftLine : TypePositionElementWin.None;
        }

        public TypePositionElementWin VerticalRightLine(BotMatchData botMatchData, Field field)
        {
            return field.Position is 
                TypePositionElementToField.TopRight or 
                TypePositionElementToField.MiddleRight or
                TypePositionElementToField.BottomRight ? TypePositionElementWin.VerticalRightLine : TypePositionElementWin.None;
        }

        public TypePositionElementWin VerticalCenterLine(BotMatchData botMatchData, Field field)
        {
            return field.Position is 
                TypePositionElementToField.TopCenter or 
                TypePositionElementToField.BottomCenter or
                TypePositionElementToField.MiddleCenter ? TypePositionElementWin.VerticalCenterLine : TypePositionElementWin.None;
        }

        public TypePositionElementWin Slash(BotMatchData botMatchData, Field field)
        {
            return field.Position is 
                TypePositionElementToField.TopLeft or 
                TypePositionElementToField.MiddleCenter or
                TypePositionElementToField.BottomRight ? TypePositionElementWin.Slash : TypePositionElementWin.None;
        }

        public TypePositionElementWin BackSlash(BotMatchData botMatchData, Field field)
        {
            return field.Position is 
                TypePositionElementToField.TopRight or 
                TypePositionElementToField.MiddleCenter or
                TypePositionElementToField.BottomLeft ? TypePositionElementWin.Backslash : TypePositionElementWin.None;
        }
    }
}
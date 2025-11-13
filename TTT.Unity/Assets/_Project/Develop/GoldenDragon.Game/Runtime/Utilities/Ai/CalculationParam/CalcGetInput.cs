using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class CalcGetInput
    {
        public TypePositionElementWin HorizontalTopLine(CharacterMatch botMatchData, Field field)
        {
            return field.Position is 
                TypePositionElementToField.TopCenter or 
                TypePositionElementToField.TopLeft or
                TypePositionElementToField.TopRight ? TypePositionElementWin.HorizontalTopLine : TypePositionElementWin.None;
        }

        public TypePositionElementWin HorizontalMiddleLine(CharacterMatch botMatchData, Field field)
        {
            return field.Position is 
                TypePositionElementToField.MiddleCenter or 
                TypePositionElementToField.MiddleLeft or
                TypePositionElementToField.MiddleRight ? TypePositionElementWin.HorizontalMiddleLine : TypePositionElementWin.None;
        }

        public TypePositionElementWin HorizontalBottomLine(CharacterMatch botMatchData, Field field)
        {
            return field.Position is 
                TypePositionElementToField.BottomCenter or 
                TypePositionElementToField.BottomLeft or
                TypePositionElementToField.BottomRight ? TypePositionElementWin.HorizontalBottomLine : TypePositionElementWin.None;
        }

        public TypePositionElementWin VerticalLeftLine(CharacterMatch botMatchData, Field field)
        {
            return field.Position is 
                TypePositionElementToField.TopLeft or 
                TypePositionElementToField.BottomLeft or
                TypePositionElementToField.MiddleLeft ? TypePositionElementWin.VerticalLeftLine : TypePositionElementWin.None;
        }

        public TypePositionElementWin VerticalRightLine(CharacterMatch botMatchData, Field field)
        {
            return field.Position is 
                TypePositionElementToField.TopRight or 
                TypePositionElementToField.MiddleRight or
                TypePositionElementToField.BottomRight ? TypePositionElementWin.VerticalRightLine : TypePositionElementWin.None;
        }

        public TypePositionElementWin VerticalCenterLine(CharacterMatch botMatchData, Field field)
        {
            return field.Position is 
                TypePositionElementToField.TopCenter or 
                TypePositionElementToField.BottomCenter or
                TypePositionElementToField.MiddleCenter ? TypePositionElementWin.VerticalCenterLine : TypePositionElementWin.None;
        }

        public TypePositionElementWin Slash(CharacterMatch botMatchData, Field field)
        {
            return field.Position is 
                TypePositionElementToField.TopLeft or 
                TypePositionElementToField.MiddleCenter or
                TypePositionElementToField.BottomRight ? TypePositionElementWin.Slash : TypePositionElementWin.None;
        }

        public TypePositionElementWin BackSlash(CharacterMatch botMatchData, Field field)
        {
            return field.Position is 
                TypePositionElementToField.TopRight or 
                TypePositionElementToField.MiddleCenter or
                TypePositionElementToField.BottomLeft ? TypePositionElementWin.Backslash : TypePositionElementWin.None;
        }
    }
}
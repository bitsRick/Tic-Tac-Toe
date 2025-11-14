using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class CalcGetInput
    {
        public TypePositionElementWin HorizontalTopLine(CharacterMatchData botMatchDataData, Field field) => 
            MathTypeFind.GetHorizontalTopLine(field.Position) ? TypePositionElementWin.HorizontalTopLine : TypePositionElementWin.None;

        public TypePositionElementWin HorizontalMiddleLine(CharacterMatchData botMatchDataData, Field field) => 
            MathTypeFind.GetHorizontalMiddleLine(field.Position) ? TypePositionElementWin.HorizontalMiddleLine : TypePositionElementWin.None;

        public TypePositionElementWin HorizontalBottomLine(CharacterMatchData botMatchDataData, Field field) => 
            MathTypeFind.GetHorizontalBottomLine(field.Position) ? TypePositionElementWin.HorizontalBottomLine : TypePositionElementWin.None;

        public TypePositionElementWin VerticalLeftLine(CharacterMatchData botMatchDataData, Field field) => 
            MathTypeFind.GetVerticalLeftLine(field.Position)? TypePositionElementWin.VerticalLeftLine : TypePositionElementWin.None;

        public TypePositionElementWin VerticalRightLine(CharacterMatchData botMatchDataData, Field field) => 
            MathTypeFind.GetVerticalRightLine(field.Position) ? TypePositionElementWin.VerticalRightLine : TypePositionElementWin.None;

        public TypePositionElementWin VerticalCenterLine(CharacterMatchData botMatchDataData, Field field) => 
            MathTypeFind.GetVerticalCenterLine(field.Position)? TypePositionElementWin.VerticalCenterLine : TypePositionElementWin.None;

        public TypePositionElementWin Slash(CharacterMatchData botMatchDataData, Field field) => 
            MathTypeFind.GetSlash(field.Position) ? TypePositionElementWin.Slash : TypePositionElementWin.None;

        public TypePositionElementWin BackSlash(CharacterMatchData botMatchDataData, Field field) => 
            MathTypeFind.GetBackslash(field.Position) ? TypePositionElementWin.Backslash : TypePositionElementWin.None;
    }
}
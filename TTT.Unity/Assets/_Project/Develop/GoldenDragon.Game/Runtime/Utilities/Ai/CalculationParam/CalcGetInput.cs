using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class CalcGetInput
    {
        public PositionElementWin HorizontalTopLine(CharacterMatchData botMatchDataData, Field field) => 
            MathTypeFind.GetHorizontalTopLine(field.Position) ? PositionElementWin.HorizontalTopLine : PositionElementWin.None;

        public PositionElementWin HorizontalMiddleLine(CharacterMatchData botMatchDataData, Field field) => 
            MathTypeFind.GetHorizontalMiddleLine(field.Position) ? PositionElementWin.HorizontalMiddleLine : PositionElementWin.None;

        public PositionElementWin HorizontalBottomLine(CharacterMatchData botMatchDataData, Field field) => 
            MathTypeFind.GetHorizontalBottomLine(field.Position) ? PositionElementWin.HorizontalBottomLine : PositionElementWin.None;

        public PositionElementWin VerticalLeftLine(CharacterMatchData botMatchDataData, Field field) => 
            MathTypeFind.GetVerticalLeftLine(field.Position)? PositionElementWin.VerticalLeftLine : PositionElementWin.None;

        public PositionElementWin VerticalRightLine(CharacterMatchData botMatchDataData, Field field) => 
            MathTypeFind.GetVerticalRightLine(field.Position) ? PositionElementWin.VerticalRightLine : PositionElementWin.None;

        public PositionElementWin VerticalCenterLine(CharacterMatchData botMatchDataData, Field field) => 
            MathTypeFind.GetVerticalCenterLine(field.Position)? PositionElementWin.VerticalCenterLine : PositionElementWin.None;

        public PositionElementWin Slash(CharacterMatchData botMatchDataData, Field field) => 
            MathTypeFind.GetSlash(field.Position) ? PositionElementWin.Slash : PositionElementWin.None;

        public PositionElementWin BackSlash(CharacterMatchData botMatchDataData, Field field) => 
            MathTypeFind.GetBackslash(field.Position) ? PositionElementWin.Backslash : PositionElementWin.None;
    }
}
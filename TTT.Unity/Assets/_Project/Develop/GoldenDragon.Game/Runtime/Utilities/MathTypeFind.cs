namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities
{
    public static class MathTypeFind
    {
        public static bool GetHorizontalTopLine(PositionElementToField type)
        {
            return type is PositionElementToField.TopCenter
            or PositionElementToField.TopLeft
            or PositionElementToField.TopRight;
        }
        
        public static bool GetHorizontalMiddleLine(PositionElementToField type)
        {
            return type is PositionElementToField.MiddleLeft
                or PositionElementToField.MiddleRight
                or PositionElementToField.MiddleCenter;
        }
        
        public static bool GetHorizontalBottomLine(PositionElementToField type)
        {
            return type is PositionElementToField.BottomLeft
                or PositionElementToField.BottomRight
                or PositionElementToField.BottomCenter;
        }
        
        public static bool GetVerticalLeftLine(PositionElementToField type)
        {
            return type is PositionElementToField.TopLeft
                or PositionElementToField.MiddleLeft
                or PositionElementToField.BottomLeft;
        }
        
        public static bool GetVerticalCenterLine(PositionElementToField type)
        {
            return type is PositionElementToField.BottomCenter
                or PositionElementToField.MiddleCenter
                or PositionElementToField.TopCenter;
        }
        
        public static bool GetVerticalRightLine(PositionElementToField type)
        {
            return type is PositionElementToField.BottomRight
                or PositionElementToField.MiddleRight
                or PositionElementToField.TopRight;
        }
        
        public static bool GetSlash(PositionElementToField type)
        {
            return type is PositionElementToField.TopLeft
                or PositionElementToField.MiddleCenter
                or PositionElementToField.BottomRight;
        }
        
        public static bool GetBackslash(PositionElementToField type)
        {
            return type is  PositionElementToField.TopRight
                or PositionElementToField.MiddleCenter
                or PositionElementToField.BottomLeft;
        }
    }
}
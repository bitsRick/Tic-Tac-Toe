namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities
{
    public static class MathTypeFind
    {
        public static bool GetHorizontalTopLine(TypePositionElementToField type)
        {
            return type is TypePositionElementToField.TopCenter
            or TypePositionElementToField.TopLeft
            or TypePositionElementToField.TopRight;
        }
        
        public static bool GetHorizontalMiddleLine(TypePositionElementToField type)
        {
            return type is TypePositionElementToField.MiddleLeft
                or TypePositionElementToField.MiddleRight
                or TypePositionElementToField.MiddleCenter;
        }
        
        public static bool GetHorizontalBottomLine(TypePositionElementToField type)
        {
            return type is TypePositionElementToField.BottomLeft
                or TypePositionElementToField.BottomRight
                or TypePositionElementToField.BottomCenter;
        }
        
        public static bool GetVerticalLeftLine(TypePositionElementToField type)
        {
            return type is TypePositionElementToField.TopLeft
                or TypePositionElementToField.MiddleLeft
                or TypePositionElementToField.BottomLeft;
        }
        
        public static bool GetVerticalCenterLine(TypePositionElementToField type)
        {
            return type is TypePositionElementToField.BottomCenter
                or TypePositionElementToField.MiddleCenter
                or TypePositionElementToField.TopCenter;
        }
        
        public static bool GetVerticalRightLine(TypePositionElementToField type)
        {
            return type is TypePositionElementToField.BottomRight
                or TypePositionElementToField.MiddleRight
                or TypePositionElementToField.TopRight;
        }
        
        public static bool GetSlash(TypePositionElementToField type)
        {
            return type is TypePositionElementToField.TopLeft
                or TypePositionElementToField.MiddleCenter
                or TypePositionElementToField.BottomRight;
        }
        
        public static bool GetBackslash(TypePositionElementToField type)
        {
            return type is  TypePositionElementToField.TopRight
                or TypePositionElementToField.MiddleCenter
                or TypePositionElementToField.BottomLeft;
        }
    }
}
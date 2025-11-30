using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Ai.CalculationParam
{
    public  class CalcWhen
    {
        public bool IsNotEmpty(Field field)
        {
            return field.CurrentPlayingField == TypePlayingField.None;
        }
    }
}
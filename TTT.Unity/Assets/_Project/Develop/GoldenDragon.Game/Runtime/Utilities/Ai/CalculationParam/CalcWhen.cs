using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public  class CalcWhen
    {
        public bool IsNotEmpty(Field field)
        {
            return field.CurrentPlayingField == TypePlayingField.None;
        }
    }
}
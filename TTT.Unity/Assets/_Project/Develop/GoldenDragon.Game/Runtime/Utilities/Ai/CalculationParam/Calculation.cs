using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class Calculation
    {
        private CalcWhen _calcWhen;
        private CalcScore _calcScore;
        private CalcGetInput _calcGetInput;

        public CalcWhen When => _calcWhen;
        public CalcScore Score => _calcScore;
        public CalcGetInput GetInput => _calcGetInput;
        
        public Calculation(PlayingField playingField)
        {
            _calcScore = new CalcScore(playingField);
            _calcWhen = new CalcWhen();
            _calcGetInput = new CalcGetInput();
        }
    }
}
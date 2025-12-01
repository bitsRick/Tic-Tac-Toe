using VContainer;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Ai.CalculationParam
{
    public class Calculation
    {
        private CalcWhen _calcWhen;
        private CalcScore _calcScore;
        private CalcGetInput _calcGetInput;

        public CalcWhen When => _calcWhen;
        public CalcScore Score => _calcScore;
        public CalcGetInput GetInput => _calcGetInput;
        
        [Inject]
        public Calculation()
        {
            _calcScore = new CalcScore();
            _calcWhen = new CalcWhen();
            _calcGetInput = new CalcGetInput();
        }

        public void Resolve(UtilityAi utilityAi)
        {
            _calcScore.Resolve(utilityAi);
        }
    }
}
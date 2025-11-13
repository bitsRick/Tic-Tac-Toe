using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Ai
{
    public class Brains: ILoadUnit
    {
        private Convolution _convolution;
        private Calculation _c;
        
        public Brains(Calculation calculation)
        {
            _c = calculation;
        }

        public UniTask Load()
        {
            _convolution = new Convolution()
            {
                {_c.When.IsNotEmpty,_c.GetInput.HorizontalTopLine,_c.Score.ScaleBy(10),"horizontal Top Line"},
                {_c.When.IsNotEmpty,_c.GetInput.HorizontalMiddleLine,_c.Score.ScaleBy(10),"horizontal Middle Line"},
                {_c.When.IsNotEmpty,_c.GetInput.HorizontalBottomLine,_c.Score.ScaleBy(10),"horizontal Bottom Line"},
                
                {_c.When.IsNotEmpty,_c.GetInput.VerticalLeftLine,_c.Score.ScaleBy(10),"Vertical Left Line"},
                {_c.When.IsNotEmpty,_c.GetInput.VerticalRightLine,_c.Score.ScaleBy(10),"Vertical Right Line"},
                {_c.When.IsNotEmpty,_c.GetInput.VerticalCenterLine,_c.Score.ScaleBy(10),"Vertical Center Line"},
                
                {_c.When.IsNotEmpty,_c.GetInput.Slash,_c.Score.ScaleBy(50),"Slash"},
                {_c.When.IsNotEmpty,_c.GetInput.BackSlash,_c.Score.ScaleBy(50),"BackSlash"},
            };
            
            return UniTask.CompletedTask;
        }

        public IEnumerable<IUtilityFunction> GetUtilityFunction() => _convolution;
    }
}
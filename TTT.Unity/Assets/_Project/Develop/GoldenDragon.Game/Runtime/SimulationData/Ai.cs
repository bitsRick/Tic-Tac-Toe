using System.Linq;
using Cysharp.Threading.Tasks;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Board;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities.Logging;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.SimulationData
{
    public class Ai:ILoadUnit
    {
        private const int RowTop = 0;
        private const int RowMiddle = 1;
        private const int RowBottom = 2;
        private const int ColLeft = 0;
        private const int ColMiddle = 1;
        private const int ColRight = 2;
        private Bot _bot;

        public Bot Bot => _bot;
        
        public UniTask Load()
        {
            _bot = new Bot();
            return UniTask.CompletedTask;
        }

        public bool IsActionField(in Field[,] arrayField)
        {
            return IsSetElementToTemplateField(arrayField) || IsSetElementToRandomField(arrayField);
        }

        private bool IsSetElementToTemplateField(in Field[,] field)
        {
            //*-*-*
            //0-0-0
            //0-0-0
            if (IsBotSetElementToField( 
                    field[RowTop, ColLeft], field[RowTop, ColMiddle], field[RowTop, ColRight]))
                return true;

            //0-0-0
            //*-*-*
            //0-0-0
            if (IsBotSetElementToField( 
                    field[RowMiddle, ColLeft], field[RowMiddle, ColMiddle], field[RowMiddle, ColRight]))
                return true;
            
            //0-0-0
            //0-0-0
            //*-*-*
            if (IsBotSetElementToField( 
                    field[RowBottom, ColLeft], field[RowBottom, ColMiddle], field[RowBottom, ColRight]))
                return true;
            
            //*-0-0
            //*-0-0
            //*-0-0
            if (IsBotSetElementToField( 
                    field[RowTop, ColLeft], 
                    field[RowMiddle, ColLeft], 
                    field[RowBottom, ColLeft]))
                return true;
            
            //0-*-0
            //0-*-0
            //0-*-0
            if (IsBotSetElementToField( 
                    field[RowTop, ColMiddle], 
                    field[RowMiddle, ColMiddle], 
                    field[RowBottom, ColMiddle]))
                return true;
            
            //0-0-*
            //0-0-*
            //0-0-*
            
            if (IsBotSetElementToField( 
                    field[RowTop, ColRight], 
                    field[RowMiddle, ColRight], 
                    field[RowBottom, ColRight]))
                return true;
            //0-0-*
            //0-*-0
            //*-0-0
            
            if (IsBotSetElementToField( 
                    field[RowTop, ColRight], 
                    field[RowMiddle, ColMiddle], 
                    field[RowBottom, ColLeft]))
                return true;
            //*-0-0
            //0-*-0
            //0-0-*
            return IsBotSetElementToField( 
                field[RowTop, ColLeft], 
                field[RowMiddle, ColMiddle], 
                field[RowBottom, ColRight]);
        }

        private bool IsSetElementToRandomField(in Field[,] arrayField)
        {
            foreach (Field field in arrayField)
                if (field.Type == TypePlayingField.None)
                {
                    field.SetElement(_bot.Type);
                    return true;
                }

            return false;
        }

        private bool IsBotSetElementToField(in Field fieldOne, in Field fieldTwo, in Field fieldThree)
        {
            if (IsEmpty(fieldOne, fieldTwo, fieldThree))
            {
                Field element = GetEmptyAnyElement(fieldOne, fieldTwo, fieldThree);

                if (element == null)
                {
                    Log.Match.W($"Error {nameof(IsSetElementToTemplateField)}!! element null!");
                    return false;
                }

                element.SetElement(_bot.Type);

                return true;
            }

            return false;
        }

        private Field GetEmptyAnyElement(params Field[] fieldsArray)
        {
            if (fieldsArray.Length == 0)
                Log.Match.W($"Error {nameof(GetEmptyAnyElement)}, array {nameof(Field)} empty!!");
            
            return fieldsArray.FirstOrDefault(field => field.Type == TypePlayingField.None);
        }

        private bool IsEmpty(in Field fieldOne,in Field fieldTwo,in Field fieldThree)
        {
            return fieldOne.Type == TypePlayingField.None || fieldOne.Type.ToString() == _bot.Type.ToString()&&
                fieldTwo.Type == TypePlayingField.None || fieldTwo.Type.ToString() == _bot.Type.ToString()&&
                fieldThree.Type == TypePlayingField.None|| fieldThree.Type.ToString() == _bot.Type.ToString();
        }
    }
}
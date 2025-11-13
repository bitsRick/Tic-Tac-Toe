using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data
{
    public class CharacterMatch
    {
        private  string _name;
        private  bool _isWinOne;
        private  bool _isWinTwo;
        private  bool _isWinThree;
        private TypePlayingField _typeField;

        public string Name => _name;
        public TypePlayingField Field => _typeField;

        public CharacterMatch(string name,TypePlayingField typeField)
        {
            _name = name;
            _typeField = typeField;
            
            Reset();
        }

        public void Reset()
        {
            _isWinOne = false;
            _isWinTwo = false;
            _isWinThree = false;
        }
    }
}
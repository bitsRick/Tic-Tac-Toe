using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data
{
    public class CharacterMatchData
    {
        private  string _name;
        private  bool _isWinOne;
        private  bool _isWinTwo;
        private  bool _isWinThree;
        private TypePlayingField _typeField;
        private bool _isBot;

        public string Name => _name;
        public TypePlayingField Field => _typeField;
        public bool IsBot => _isBot;

        public CharacterMatchData(string name,TypePlayingField typeField, bool isBot)
        {
            _isBot = isBot;
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
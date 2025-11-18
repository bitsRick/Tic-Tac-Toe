using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Match.Round;
using GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Data
{
    public class CharacterMatchData
    {
        private string _name;
        private int _winCount;
        private bool _isBot;
        private TypePlayingField _field;

        public string Name => _name;
        public int WinCount => _winCount;
        public TypePlayingField Field => _field;
        public bool IsBot => _isBot;

        public CharacterMatchData(string name,TypePlayingField field, bool isBot)
        {
            _isBot = isBot;
            _name = name;
            _field = field;
            
            Reset();
        }

        public void AddWin()
        {
            _winCount++;
        }

        public void Reset()
        {
            _winCount = 0;
        }
    }
}
namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities
{
    //CRTP
    public abstract class Singleton<T>  where T : Singleton<T> ,new()
    {
        private static readonly object _lock = new();
        private static T _i;

        public static T S
        {
            get
            {
                if (_i == null)
                {
                    //блокировка от других потоков
                    lock (_lock) 
                        _i = _i ?? new T();
                }

                return _i;
            }
        }
    }
}
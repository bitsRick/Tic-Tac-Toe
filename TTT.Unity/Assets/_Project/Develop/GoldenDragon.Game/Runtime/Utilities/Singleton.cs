namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities
{
    //CRTP
    public abstract class Singleton<T>  where T : Singleton<T> ,new()
    {
        private static readonly object _lock = new object();
        
        private static T _instance;
        public static T S
        {
            get
            {
                if (S == null)
                {
                    //блокировка от других потоков
                    lock (_lock)
                    {
                        if (S == null)
                        {
                            _instance = new T();
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
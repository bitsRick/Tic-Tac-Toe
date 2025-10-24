using DG.Tweening;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities
{
    public static class Extention
    {
        public static T SetTest<T>(this T t, TweenCallback callback) where T : Tween
        {
            t.onComplete += callback;
            return t;
        }
    }
}
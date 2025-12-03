using System;
using System.Collections.Generic;

namespace GoldenDragon._Project.Develop.GoldenDragon.Game.Runtime.Utilities
{
    public static class EnumerableExtensions
    {
        public static T FindMax<T, TResult>(this IEnumerable<T> source,
            Func<T, TResult> selector) where TResult: IComparable<TResult> =>
            Find(source, selector, false);

        private static T Find<T, TComp>(IEnumerable<T> enumerable, Func<T, TComp> selector, bool selectMin)
            where TComp : IComparable<TComp>
        {
            if (enumerable == null)
                return default;
      
            var first = true;
            T selected = default(T);
            TComp selectedComp = default(TComp);

            foreach (T current in enumerable)
            {
                TComp comp = selector(current);
                
                if (first)
                {
                    first = false;
                    selected = current;
                    selectedComp = comp;
                    continue;
                }

                int res = selectMin
                    ? comp.CompareTo(selectedComp)
                    : selectedComp.CompareTo(comp);

                if (res < 0)
                {
                    selected = current;
                    selectedComp = comp;
                }
            }

            return selected;
        }
        
        public static float? SumOrNull(this IEnumerable<float> numbers)
        {
            float? sum = null;
            foreach (float f in numbers)
                sum = f + (sum ?? 0);

            return sum;
        }
    }
}
using System;

namespace Quack.Utils.Functional
{
    public static class FuncExtensions
    {

        /// <summary>
        /// Chain calls
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult1"></typeparam>
        /// <typeparam name="TResult2"></typeparam>
        /// <param name="function1"></param>
        /// <param name="function2"></param>
        /// <returns></returns>
        public static Func<T, TResult2> Then<T, TResult1, TResult2>(
                this Func<T, TResult1> function1, Func<TResult1, TResult2> function2) =>
                    value => function2(function1(value));

        /// <summary>
        /// perform an mapping over T, return TResult.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOutput"></typeparam>
        /// <param name="input"></param>
        /// <param name="mapping"></param>
        public static TOutput Map<T, TOutput>(this T input, Func<T, TOutput> mapping) => mapping(input);

    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Quack.Utils.Tasks
{
    public static class ParallelExtensions
    {
        public static IEnumerable<TOutput> InParallel<TInput, TOutput>(this IEnumerable<TInput> input, Func<TInput, TOutput> selector)
        {
            return input.AsParallel()
                        .Select(selector)
                        .WithDegreeOfParallelism(4)
                        .ToList();
        }

        public static async Task<List<TOutput>> InParallel<TInput, TOutput>(this IEnumerable<TInput> input, Func<TInput, Task<TOutput>> func)
        {
            var result = new ConcurrentBag<TOutput>();

            input.AsParallel()
                 .ForAll(async inp => result.Add(await func(inp)));

            return await Task.FromResult(result.ToList());
        }
    }
}


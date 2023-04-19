using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Quack.Utils.Tasks
{
    public class TaskHelper
    {
        public Action Onstart;
        public Action OnFinish;

        [Obsolete("use TryAsync<T> instead")]
        public async Task<T> Try<T>(Func<Task<T>> work)
        {
            try
            {
                Onstart?.Invoke();
                return await work().ConfigureAwait(false);
            }
            finally
            {
                OnFinish.Invoke();
            }
        }

        public async Task<T> TryAsync<T>(Task<T> task, Action whenStarting, Action whenFinished)
        {
            try
            {
                whenStarting?.Invoke();
                return await task.ConfigureAwait(false);
            }
            finally
            {
                whenFinished?.Invoke();
            }
        }
    }
}

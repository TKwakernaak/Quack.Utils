using System;
using System.Threading.Tasks;

namespace Quack.Utils.Tasks
{
    public class TaskHelper
    {
        public Action Onstart;
        public Action OnFinish;

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
    }
}

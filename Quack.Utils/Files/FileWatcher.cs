using System.Threading.Tasks;
using System.IO;
using Sundio.Player.Sidecar.Domain.Models.PriceReport;
using System;

namespace Quack.Utils.Files
{
    /// <summary>
    /// Simple abstraction over microsofts filewatcher to make it awaitable using TaskCompletionSource
    /// </summary>
    public class FileWatcher
    {
        /// <summary>
        /// Manually controlled task
        /// </summary>
        private readonly TaskCompletionSource<FilePath> _fileCreatedTsc = new TaskCompletionSource<FilePath>();
        private readonly FileSystemWatcher _fileSystemWatcher;

        private readonly TimeSpan _timeOutAfter;

        public FileWatcher(TimeSpan timeOutAfter, FileSystemWatcher fileSystemWatcher)
        {
            _timeOutAfter = timeOutAfter;
            _fileSystemWatcher = fileSystemWatcher;
        }

        /// <summary>
        /// Wait for a file to be created at the specified path, which adheres to the specified parameters
        /// </summary>
        public async Task<FilePath> WaitForCreatedFileAsync()
        {
            _ = Task.Run(() => WaitForChangeOn());
            _ = Task.Run(() => HandleTimeout());

            return await _fileCreatedTsc.Task.ConfigureAwait(false);
        }

        /// <summary>
        /// Watch the specified folder for changes which adhere to the passed filter.
        /// </summary>
        private void WaitForChangeOn()
        {
            _fileSystemWatcher.Created += Watcher_OnFileCreated;
            _fileSystemWatcher.Error += Watcher_OnError;
        }

        private void Watcher_OnFileCreated(object sender, FileSystemEventArgs e)
        {
            var file = new FilePath(e.FullPath);
            _fileCreatedTsc.SetResult(file);
        }

        private void Watcher_OnError(object sender, ErrorEventArgs e)
        {
            var ex = new InvalidOperationException($"error while waiting for a new file to be created {e?.GetException()?.Message}", e.GetException());
            _fileCreatedTsc.SetException(ex);
        }

        private async Task HandleTimeout()
        {
            var end = DateTime.UtcNow.Add(_timeOutAfter);

            while (DateTime.UtcNow < end)
            {
                if (_fileCreatedTsc.Task.IsCompleted || _fileCreatedTsc.Task.IsFaulted)
                {
                    break;
                }

                await Task.Delay(2000).ConfigureAwait(false);
            }
            if (!_fileCreatedTsc.Task.IsCompleted || !_fileCreatedTsc.Task.IsCanceled)
            {
                _fileCreatedTsc.SetCanceled();
            }
        }

        public void Dispose()
        {
            _fileSystemWatcher?.Dispose();
            _fileCreatedTsc?.Task?.Dispose();
        }
    }
}

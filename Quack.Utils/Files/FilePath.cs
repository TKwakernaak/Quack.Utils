using System;
using System.IO;

namespace Quack.Utils.Files
{
    /// <summary>
    /// provides a simple abstraction over file paths.
    /// </summary>
    public class FilePath
    {
        public FilePath(string filePath)
        {
            if (!IsValidFilePath(filePath))
            {
                throw new ArgumentException($"{filePath} is not a valid file path");
            }

            FullPath = filePath;
        }

        public string FullPath { get; }

        public string FileName => Path.GetFileName(FullPath);

        /// <summary>
        /// Directory where the file represented by this class is located
        /// </summary>
        public string FolderPath => Path.GetDirectoryName(FullPath);

        public override string ToString()
        {
            return FullPath;
        }

        public static bool IsValidFilePath(string filePath)
        {
            return !string.IsNullOrEmpty(filePath) && Path.HasExtension(filePath);
        }

        public void EnsureFileExtensionOf(string extension)
        {
            if (!Path.GetExtension(FullPath).Contains(extension, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"destination file path {FullPath} does not have the expected extension {extension}");
            }
        }

        public void EnsureFileExists()
        {
            if (!File.Exists(FullPath))
            {
                throw new InvalidOperationException($"file path {FullPath} does not exist.");
            }
        }
    }
}

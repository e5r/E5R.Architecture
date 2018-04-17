using System;
using System.IO;

namespace E5R.Architecture.Core.Abstractions
{
    /// <summary>
    /// File system abstractions
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Check if file path exists
        /// </summary>
        /// <param name="path">File path</param>
        /// <exception cref="ArgumentNullException">If path is null or empty string</exception>
        /// <returns>True if exists, false otherwise</returns>
        bool FileExists(string path);

        /// <summary>
        /// Get current directory path
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        /// <returns>Current directory path</returns>
        string GetCurrentDirectory();

        /// <summary>
        /// Get a current app base directory
        /// </summary>
        /// <returns>App base directory</returns>
        string GetBaseDirectory();

        /// <summary>
        /// Open a file stream
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <param name="mode">File mode</param>
        /// <exception cref="ArgumentNullException">If path is null or empty string</exception>
        /// <exception cref="FileNotFoundException">If file not exists</exception>
        /// <exception cref="IOException">Other errors</exception>
        /// <returns></returns>
        FileStream OpenFile(string path, FileMode mode);
    }
}

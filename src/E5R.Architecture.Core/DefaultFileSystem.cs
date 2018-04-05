using System;
using System.IO;

namespace E5R.Architecture.Core
{
    using Abstractions;

    /// <summary>
    /// Default implementation of <see cref="IFileSystem"/>
    /// </summary>
    public class DefaultFileSystem : IFileSystem
    {
        public bool FileExists(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return File.Exists(path);
        }

        public string GetCurrentDirectory()
        {
            try
            {
                return Directory.GetCurrentDirectory();
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new NotSupportedException(exception.Message, exception);
            }
        }

        public string GetBaseDirectory()
        {
            return AppContext.BaseDirectory;
        }

        public FileStream OpenFile(string path, FileMode mode)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            try
            {
                return new FileStream(path, mode);
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new IOException(exception.Message, exception);
            }
        }
    }
}

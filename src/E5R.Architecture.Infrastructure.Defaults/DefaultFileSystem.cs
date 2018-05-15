// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.IO;

namespace E5R.Architecture.Infrastructure.Defaults
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
#if NET451 || NET46
            return AppDomain.CurrentDomain.BaseDirectory;
#else
            return AppContext.BaseDirectory;
#endif
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

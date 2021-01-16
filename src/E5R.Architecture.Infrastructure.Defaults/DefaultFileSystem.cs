// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.IO;
using E5R.Architecture.Core;
using E5R.Architecture.Infrastructure.Abstractions;

namespace E5R.Architecture.Infrastructure.Defaults
{
    /// <summary>
    /// Default implementation of <see cref="IFileSystem"/>
    /// </summary>
    public class DefaultFileSystem : IFileSystem
    {
        public bool FileExists(string path)
        {
            Checker.NotNullArgument(path, nameof(path));

            return File.Exists(path);
        }

        public bool DirectoryExists(string path)
        {
            Checker.NotNullArgument(path, nameof(path));

            return Directory.Exists(path);
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

        public string GetBaseDirectory() => AppContext.BaseDirectory;

        public FileStream CreateFileStream(string path, FileMode mode)
        {
            Checker.NotNullArgument(path, nameof(path));

            try
            {
                return new FileStream(path, mode);
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new IOException(exception.Message, exception);
            }
        }

        public void CreateDirectory(string path)
        {
            Checker.NotNullArgument(path, nameof(path));

            Directory.CreateDirectory(path);
        }
    }
}

﻿// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/manifest/blob/master/license/APACHE-2.0.txt

using System;
using System.IO;

namespace E5R.Architecture.Infrastructure.Abstractions
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
        /// Check if directory path exists
        /// </summary>
        /// <param name="path">Directory path</param>
        /// <returns>True if exists, false otherwise</returns>
        bool DirectoryExists(string path);

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
        /// Create a file stream
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <param name="mode">File mode</param>
        /// <exception cref="ArgumentNullException">If path is null or empty string</exception>
        /// <exception cref="FileNotFoundException">If file not exists</exception>
        /// <exception cref="IOException">Other errors</exception>
        /// <returns></returns>
        FileStream CreateFileStream(string path, FileMode mode);

        /// <summary>
        /// Create a directory
        /// </summary>
        /// <param name="path">Path to directory</param>
        void CreateDirectory(string path);
    }
}

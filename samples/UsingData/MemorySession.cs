// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System.Collections.Generic;
using System.IO;
using System.Text;
using E5R.Architecture.Infrastructure.Abstractions;
using E5R.Architecture.Data;
using E5R.Architecture.Infrastructure;
using Newtonsoft.Json;

namespace UsingData
{
    internal static class MemorySession
    {
        private const string DatabaseFileName = "E5R.Architecture.Samples.UsingData.json";

        internal static UnderlyingSession CreateSession(IFileSystem fs)
        {
            var dbSession = new MemoryDatabase();

            string dataString = GetCurrentDataString(fs) ?? "[]";

            foreach (var blog in JsonConvert.DeserializeObject<IList<BlogDataModel>>(dataString))
            {
                dbSession.Blog.Add(blog);
            }

            return new UnderlyingSession(dbSession);
        }

        internal static void SaveSession(UnderlyingSession session, IFileSystem fs)
        {
            string dbFilePath = Path.Combine(fs.GetCurrentDirectory(), DatabaseFileName);

            FileStream file = fs.OpenFile(dbFilePath,
                fs.FileExists(dbFilePath) ? FileMode.Truncate : FileMode.CreateNew);
            string dataString = JsonConvert.SerializeObject(session.Get<MemoryDatabase>().Blog,
                Formatting.Indented);

            using (StreamWriter writer = new StreamWriter(file, Encoding.UTF8))
            {
                writer.Write(dataString);
            }
        }

        private static string GetCurrentDataString(IFileSystem fs)
        {
            string dbFilePath = Path.Combine(fs.GetCurrentDirectory(), DatabaseFileName);

            if (!fs.FileExists(dbFilePath))
            {
                return null;
            }

            FileStream file = fs.OpenFile(dbFilePath, FileMode.Open);

            file.Position = 0;

            using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}

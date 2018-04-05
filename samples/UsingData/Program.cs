﻿using System;
using System.Linq;
using E5R.Architecture.Data;
using E5R.Architecture.Data.Abstractions;

namespace UsingData
{
    using static Console;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var uow = new MemoryUnitOfWork();
            var storage = new BlogStorage(uow);

            WriteLine("Creating a blogs...");
            storage.Create(new BlogDataModel
            {
                BlogUrl = "https://erlimar.wordpress.com",
                BlogTitle = "Erlimar Silva Campos"
            });

            storage.Create(new BlogDataModel
            {
                BlogUrl = "https://blog.jetbrains.com/dotnet",
                BlogTitle = "JetBrains .NET TOOLS BLOG"
            });

            var db = uow.Session.Get<MemoryDatabase>();

            WriteLine($"  - Total of existing blogs: {db.Blog.Count}");
            foreach (var blog in db.Blog)
            {
                WriteLine($"  - {blog.BlogUrl} -> {blog.BlogTitle}");
            }

            WriteLine();
            WriteLine("Updating a blog name...");
            storage.Replace(new BlogDataModel
            {
                BlogUrl = "https://erlimar.wordpress.com",
                BlogTitle = "Erlimar Blog's"
            });

            WriteLine($"  - Total of existing blogs: {db.Blog.Count}");
            foreach (var blog in db.Blog)
            {
                WriteLine($"  - {blog.BlogUrl} -> {blog.BlogTitle}");
            }

            WriteLine();
            WriteLine("Removing a blog...");
            storage.RemoveByUrl("https://blog.jetbrains.com/dotnet");

            WriteLine($"  - Total of existing blogs: {db.Blog.Count}");
            foreach (var blog in db.Blog)
            {
                WriteLine($"  - {blog.BlogUrl} -> {blog.BlogTitle}");
            }
        }
    }
}
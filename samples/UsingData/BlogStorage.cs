// Copyright (c) E5R Development Team. All rights reserved.
// This file is a part of E5R.Architecture.
// Licensed under the Apache version 2.0: https://github.com/e5r/licenses/blob/master/license/APACHE-2.0.txt

using System;
using System.Linq;
using E5R.Architecture.Data;
using E5R.Architecture.Data.Abstractions;

namespace UsingData
{
    public class BlogStorage : IStorageWriter<BlogDataModel>
    {
        private MemoryDatabase _db;

        public void Configure(UnderlyingSession session) => _db = session.Get<MemoryDatabase>();

        public BlogDataModel Create(BlogDataModel blog)
        {
            // TODO: Implementar validação no Storage genérico

            if (_db.Blog.Any(w => w.BlogUrl == blog.BlogUrl))
            {
                throw new Exception($"Blog '{blog.BlogUrl}' already exists!");
            }

            _db.Blog.Add(blog);

            return _db.Blog.SingleOrDefault(w => w.BlogUrl == blog.BlogUrl);
        }

        public BlogDataModel Replace(BlogDataModel blog)
        {
            // TODO: Implementar validação no Storage genérico

            var originalBlog = _db.Blog.SingleOrDefault(w => w.BlogUrl == blog.BlogUrl);

            if (originalBlog == null)
            {
                throw new Exception($"Blog '{blog.BlogUrl}' not found!");
            }

            var blogIdx = _db.Blog.IndexOf(originalBlog);

            _db.Blog.RemoveAt(blogIdx);
            _db.Blog.Add(blog);

            return _db.Blog.SingleOrDefault(w => w.BlogUrl == blog.BlogUrl);
        }

        public void Remove(BlogDataModel blog)
        {
            // TODO: Implementar validação no Storage genérico

            var originalBlog =
                _db.Blog.SingleOrDefault(w => w.BlogUrl.Equals(blog.IdentifierValues[0]));

            if (originalBlog == null)
            {
                throw new Exception($"Blog '{blog.BlogUrl}' not found!");
            }

            var blogIdx = _db.Blog.IndexOf(originalBlog);

            _db.Blog.RemoveAt(blogIdx);
        }
    }
}

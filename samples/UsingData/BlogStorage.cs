using System;
using System.Linq;
using E5R.Architecture.Data;
using E5R.Architecture.Data.Abstractions;

namespace UsingData
{
    public class BlogStorage : IStorageWriter<BlogStorage, BlogDataModel>
    {
        private MemoryDatabase _db;

        public BlogStorage ConfigureSession(UnderlyingSession session)
        {
            _db = session.Get<MemoryDatabase>();

            return this;
        }

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

            var originalBlog = _db.Blog.SingleOrDefault(blog.GetIdenifierCriteria().Compile());

            if (originalBlog == null)
            {
                throw new Exception($"Blog '{blog.BlogUrl}' not found!");
            }

            var blogIdx = _db.Blog.IndexOf(originalBlog);

            _db.Blog.RemoveAt(blogIdx);
        }
    }
}

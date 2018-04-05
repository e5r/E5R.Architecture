using System;
using System.Linq;
using E5R.Architecture.Data;
using E5R.Architecture.Data.Abstractions;

namespace UsingData
{
    // TODO: Criar IStorage's void
    public class BlogStorage : IStorageWriter<BlogDataModel, VoidIdentifier>
    {
        private readonly IUnitOfWork _uow;

        public BlogStorage(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public BlogDataModel Create(BlogDataModel blog)
        {
            // TODO: Implementar validação no Storage genérico

            var db = _uow.Session.Get<MemoryDatabase>();

            if (db.Blog.Any(w => w.BlogUrl == blog.BlogUrl))
            {
                throw new Exception($"Blog '{blog.BlogUrl}' already exists!");
            }

            db.Blog.Add(blog);

            return db.Blog.SingleOrDefault(w => w.BlogUrl == blog.BlogUrl);
        }

        public BlogDataModel Replace(BlogDataModel blog)
        {
            // TODO: Implementar validação no Storage genérico

            var db = _uow.Session.Get<MemoryDatabase>();
            var originalBlog = db.Blog.SingleOrDefault(w => w.BlogUrl == blog.BlogUrl);

            if (originalBlog == null)
            {
                throw new Exception($"Blog '{blog.BlogUrl}' not found!");
            }

            var blogIdx = db.Blog.IndexOf(originalBlog);

            db.Blog.RemoveAt(blogIdx);
            db.Blog.Add(blog);

            return db.Blog.SingleOrDefault(w => w.BlogUrl == blog.BlogUrl);
        }

        public void Remove(VoidIdentifier id)
        {
            throw new NotImplementedException();
        }

        public void RemoveByUrl(string blogUrl)
        {
            // TODO: Implementar validação no Storage genérico

            var db = _uow.Session.Get<MemoryDatabase>();
            var originalBlog = db.Blog.SingleOrDefault(w => w.BlogUrl == blogUrl);

            if (originalBlog == null)
            {
                throw new Exception($"Blog '{blogUrl}' not found!");
            }

            var blogIdx = db.Blog.IndexOf(originalBlog);

            db.Blog.RemoveAt(blogIdx);
        }
    }
}
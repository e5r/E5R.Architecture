using System;
using System.Linq.Expressions;
using E5R.Architecture.Data;

namespace UsingData
{
    public class BlogDataModel : DataModel<BlogDataModel>
    {
        public string BlogUrl { get; set; }
        public string BlogTitle { get; set; }

        public override Expression<Func<BlogDataModel, bool>> GetIdenifierCriteria()
        {
            return m => m.BlogUrl == BlogUrl;
        }
    }
}

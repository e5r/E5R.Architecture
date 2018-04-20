using E5R.Architecture.Data.Abstractions;

namespace UsingData
{
    public class BlogDataModel : DataModel<BlogModel>
    {
        public override object[] IdentifierValues
            => new object[] {Business.BlogUrl};

        public string BlogUrl
        {
            get => Business.BlogUrl;
            set => Business.BlogUrl = value;
        }

        public string BlogTitle
        {
            get => Business.BlogTitle;
            set => Business.BlogTitle = value;
        }
    }
}

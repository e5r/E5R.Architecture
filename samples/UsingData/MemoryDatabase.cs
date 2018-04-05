using System.Collections.Generic;

namespace UsingData
{
    public class MemoryDatabase
    {
        public MemoryDatabase()
        {
            Blog = new List<BlogDataModel>();
        }

        public IList<BlogDataModel> Blog { get; private set; }
    }
}
namespace UsingDataEntityFrameworkCore.Utils
{
    public static class ScapeSqlQuoteUtil
    {
        public static string ScapeQuote(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return str.Replace("'", "''");
        }
    }
}

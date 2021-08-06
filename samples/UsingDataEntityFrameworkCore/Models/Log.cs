using System;
using E5R.Architecture.Core;

namespace UsingDataEntityFrameworkCore.Models
{
    public class Log : IIdentifiable
    {
        public int LogId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public object[] Identifiers => new[] {(object) LogId};
    }
}

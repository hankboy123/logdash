using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogDash.Domain
{
    public class LogQueryCondition
    {
        public string AppId { get; set; }
        public string LogTypeId { get; set; }
        public string LogTypeName { get; set; }
        public string Label { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string OrderId { get; set; }
        public string RelatedId { get; set; }

        public int  skipNum { get; set; }

        public int TakeNum { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogDash.Models
{
    public class LogEntity
    {
        //主键
        public string Id { get; set; }
        public string AppId { get; set; }
        public string LogTypeId { get; set; }
        public string LogTypeName { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string DeleteUserId { get; set; }
        public DateTime? DeleteTime { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string OrderId { get; set; }
        public string RelatedId { get; set; }
        public string Remark { get; set; }

    }
}

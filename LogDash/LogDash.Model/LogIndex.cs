using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogDash.Models
{
    /// <summary>
    /// 日志存储在关系型数据库中，需要日志业务索引（LogIndex）
    /// </summary>
    public class LogIndex
    {
        public string LogLabel { get; set; }
        public string LogIdCollection { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogDash.Models
{
    public class LogType
    {
        public string Id { get; set; }
        public string AppId { get; set; }

        public string Name { get; set; }
        public string Remark { get; set; }
    }
}

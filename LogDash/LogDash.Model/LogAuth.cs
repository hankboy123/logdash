using System;
using System.Collections.Generic;
using System.Text;

namespace LogDash.Model
{
    public class LogAuth
    {
        public string Id { get; set; }

        public string AppId { get; set; }

        public string LogTypeId { get; set; }

        public byte IsAuthorize { get; set; }

        public string Remark { get; set; }
    }
}

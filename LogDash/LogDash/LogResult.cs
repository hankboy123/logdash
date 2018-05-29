using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogDash
{
    public class LogResult
    {
        public ResultCode Code { get; set; }
        public string Msg { get; set; }

        public LogResult(ResultCode code,string msg)
        {
            this.Code = code;
            this.Msg = msg;
        }
    }
    
    public enum ResultCode
    {
        Success,
        Fail
    }
}

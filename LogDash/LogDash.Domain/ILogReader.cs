using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogDash.Domain
{
    public interface ILogReader
    {
        object Read(LogQueryPara para);
    }
}

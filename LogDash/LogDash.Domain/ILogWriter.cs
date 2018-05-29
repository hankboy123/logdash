using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogDash.Models;

namespace LogDash.Domain
{
    public interface ILogWriter
    {
        void Write(LogWritePara para);
    }
}

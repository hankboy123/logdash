using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogDash.Domain;
using LogDash.Models;

namespace LogDash.Infrastructure
{
    public interface ILogRecordDriver        
    {
         void Write(LogEntity entity);
         object Read(LogQueryCondition condition);
    }
}

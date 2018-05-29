using System;
using System.Collections.Generic;
using System.Text;

namespace LogDash.Infrastructure
{
    interface ISplitTableStrategy
    {
        string GetTableName();
    }
}

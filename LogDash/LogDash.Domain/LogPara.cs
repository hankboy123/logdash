using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogDash.Domain
{
    public class LogPara
    {
       private   IDictionary<string, string> paraDic;


        public LogPara(IDictionary<string, string> paraDic)
        {
            this.paraDic = paraDic;
        }
        public IDictionary<string, string> ParaDic
        {
            get
            {
                return paraDic;
            }
        }
    }


    public class LogQueryPara: LogPara
    {
        public LogQueryPara(IDictionary<string, string> paraDic):base(paraDic)
        {
          
        }
    }

    public class LogWritePara : LogPara
    {
        public LogWritePara(IDictionary<string, string> paraDic) : base(paraDic)
        {

        }
    }
}

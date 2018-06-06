using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogDash.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogDash.Controllers
{
    //[Produces("application/json")]
    [Route("api/log")]
    public class LogController : Controller
    {
        public ILogWriter _writer;

        public IServiceProvider _provider;

        public LogController(IServiceProvider provider)
        {
            _provider = provider;
        }

        [HttpPost("record")]
        public LogResult Record()
        {
            LogResult result = new LogResult(ResultCode.Fail,"");
            try
            {
                ILogWriter writer = (ILogWriter)_provider.GetService(typeof(ILogWriter));// new LogWriter();

                writer.Write(new LogWritePara(Filter(Request.Form)));
                result.Code = ResultCode.Success;
            }
            catch (Exception ex)
            {
                //日志
                result.Msg = "SYSTEM ERROR";
            }


            return result;
        }

        private IDictionary<string, string> Filter(IFormCollection col)
        {
            IDictionary<string, string> para = null;
            if (col != null && col.Keys.Count > 0)
            {
                para = new Dictionary<string, string>();
                foreach (var key in col.Keys)
                {
                    para.Add(key, col[key]);
                }
            }
            return para;
        }


    }
}
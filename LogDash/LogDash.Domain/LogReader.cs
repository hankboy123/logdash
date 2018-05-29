using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogDash.Infrastructure;

namespace LogDash.Domain
{
    public class LogReader
    {
        private ILogRecordDriver _recorder = null;
        public  LogReader(ILogRecordDriver recorder)
        {
            _recorder = recorder;
        }

        public object Read(LogQueryPara para)
        {
            LogQueryCondition condition = getQueryCondition(para);
            return _recorder.Read(condition);
        }

        private LogQueryCondition getQueryCondition(LogQueryPara para)
        {
            LogQueryCondition entity = null;
            if (para.ParaDic != null && para.ParaDic.Keys.Count > 0)
            {
                entity = new LogQueryCondition();
                entity.AppId = para.ParaDic["appid"];
                entity.LogTypeId = para.ParaDic["logtypeid"];
                entity.LogTypeName = para.ParaDic["logtypename"];
                entity.Label = para.ParaDic["label"];
                entity.Message = para.ParaDic["message"];
                entity.CreatedTime = DateTime.Now;
                entity.UserId = para.ParaDic["userid"];
                entity.UserName = para.ParaDic["username"];
                entity.OrderId = para.ParaDic["orderid"];
                entity.RelatedId = para.ParaDic["relatedid"];
            }
            return entity;
        }
    }
}

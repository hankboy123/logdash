using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogDash.Infrastructure;
using LogDash.Models;

namespace LogDash.Domain
{
    public class LogWriter:ILogWriter
    {
        private ILogRecordDriver _recorder = null;

        /// <summary>
        /// 默认走mongodb存储日志
        /// </summary>
        public LogWriter()
        {
            _recorder = new MongoLogRecordDriver();

        }

        public LogWriter(ILogRecordDriver recorder)
        {
            _recorder = recorder;
        }

        public void Write(LogWritePara para)
        {
            _recorder.Write(getLogEntity(para));
        }

        private LogEntity getLogEntity(LogWritePara para)
        {
            LogEntity entity = null;
            if (para.ParaDic != null && para.ParaDic.Keys.Count > 0)
            {
                entity = new LogEntity();
                entity.AppId = para.ParaDic["appid"];
                entity.Id = entity.AppId+ entity.LogTypeId+DateTime.Now.ToString("yyyyMMddHHmmss");
                entity.LogTypeId = para.ParaDic["logtypeid"];
                entity.LogTypeName = para.ParaDic["logtypename"];
                entity.Description = para.ParaDic["description"];
                entity.Label = para.ParaDic["label"];
                entity.Message = para.ParaDic["message"];
                entity.CreatedTime =DateTime.Now;
                entity.UserId = para.ParaDic["userid"];
                entity.UserName = para.ParaDic["username"];
                entity.OrderId = para.ParaDic["orderid"];
                entity.RelatedId = para.ParaDic["relatedid"];
                entity.Remark = para.ParaDic["remark"];
            }
            return entity;
        }
    }
}

using LogDash.Domain;
using LogDash.Infrastructure;
using LogDash.Model;
using LogDash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogDash.Admin
{
    public static  class LogBusiness
    {
       
        public static object QueryLogRecordData(LogQueryCondition condition)
        {
            return new MongoLogRecordDriver().Read(condition);
        }

        public static void AddAppInfo(AppInfo entity)
        {
            new MongoDBBaseRepository("AppInfo").Add<AppInfo>(entity);
        }
       
        public static AppInfo GetAppInfo(string appid)
        {
            return new MongoDBBaseRepository("AppInfo").GetBy<AppInfo>(appid).SingleOrDefault();
        }

        public static IList<AppInfo> GetAppInfoList()
        {
            return new MongoDBBaseRepository("AppInfo").GetALLBy<AppInfo>();
        }

        public static void AddLogType(LogType entity)
        {
            new MongoDBBaseRepository("LogType").Add<LogType>(entity);
        }

        public static IList<LogType> GetLogTypeList(string appid,string logtypeid)
        {
            return new MongoDBBaseRepository("LogType").GetBy<LogType>(appid, logtypeid);
        }

        public static void UpdateLogTypeName(string logTypeId, string name, string remark)
        {
             new MongoDBBaseRepository("LogType").UpdateLogTypeName(logTypeId, name, remark);
        }
        public static LogAuth GetLogAuth(string appid,string logtypeid)
        {
            return new MongoDBBaseRepository("LogAuth").GetBy<LogAuth>(appid, logtypeid).SingleOrDefault();
        }

        public static void UpdateAuth(string Id, byte isAuth)
        {

            new MongoDBBaseRepository("LogAuth").UpdateAuth(Id, isAuth);
        }
    }
}

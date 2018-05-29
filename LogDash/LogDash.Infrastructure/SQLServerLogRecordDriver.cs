using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LogDash.Domain;
using LogDash.Models;

namespace LogDash.Infrastructure
{

    /// <summary>
    /// 后续可扩展用
    /// </summary>
    public class SQLServerLogRecordDriver : ILogRecordDriver
    {
        string connectionString = string.Empty;
        public void Write(LogEntity entity)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                 connection.Execute(FormatInsertSql<LogEntity>(), entity);
            }
            
        }
        public object Read(LogQueryCondition condition)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                //appid,logtype查询

                //标签查询
                var idList=connection.Query("select LogIdCollection from LogIndex where LogLabel=@LogLabel");
                var resList = connection.Query("select * from LogEntity where Id=@IdList");

                //orderid,relateid,userid,username查询

            }
            return null;
        }

        protected string FormatInsertSql<T>()
        {
            string propertyLinkStr = string.Empty;
            string paraLinkStr = string.Empty;
            Type type = typeof(T);
            string table_name = type.Name;
            foreach (var property in type.GetProperties())
            {
                propertyLinkStr += property.Name + ",";
                paraLinkStr += "@" + property.Name + ",";
            }
            if (propertyLinkStr.Contains(',')) propertyLinkStr = propertyLinkStr.Substring(0, propertyLinkStr.Length - 1);
            if (paraLinkStr.Contains(',')) paraLinkStr = paraLinkStr.Substring(0, paraLinkStr.Length - 1);
            string sql = "Insert Into " + table_name + "(" + propertyLinkStr + ") Values(" + paraLinkStr + ")";
            return sql;
        }
    }
}

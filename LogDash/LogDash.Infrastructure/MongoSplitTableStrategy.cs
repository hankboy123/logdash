using System;
using System.Collections.Generic;
using System.Text;

namespace LogDash.Infrastructure
{
    public  class MongoSplitTableStrategy: ISplitTableStrategy
    {
        private string appid;

        private string defaultTableName;

        private string splitTimeSpan;

        public MongoSplitTableStrategy(string defaultTableName, string appid, string splitTimeSpan)
        {
            this.appid = appid;
            this.defaultTableName = defaultTableName;
            this.splitTimeSpan = splitTimeSpan;
        }

        public string GetTableName()
        {
            return defaultTableName+appid+splitTimeSpan;
        }
    }



}

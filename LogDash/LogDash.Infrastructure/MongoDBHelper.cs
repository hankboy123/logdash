using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;


namespace LogDash.Infrastructure
{
    public class MongoDBHelper
    {
        //static Octopus.Logging.ILogger logger = Octopus.Logging.LogManager.GetLogger("MongoDBHelper");

        private static string connString = string.Empty;// System.Configuration.ConfigurationManager.AppSettings["MongoConnectionString"];
        private static string dbName = string.Empty;// System.Configuration.ConfigurationManager.AppSettings["MongoDBName"];

        //副本集服务器
        private static string relicaSetName = string.Empty;// System.Configuration.ConfigurationManager.AppSettings["MongoDBRelicaSetName"];
        private static string relicaSetServers = string.Empty;//System.Configuration.ConfigurationManager.AppSettings["MongoDBRelicaSetServers"];


        public static IMongoDatabase GetMongoDB()
        {
            try
            {
                var client = GetMongoClient();
                return client.GetDatabase(dbName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IMongoDatabase GetMongoDB(string datebaseName)
        {
            try
            {
                var client = GetMongoClient();
                return client.GetDatabase(datebaseName);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static MongoClient GetMongoClient()
        {
            
            try
            {
                if (!string.IsNullOrWhiteSpace(relicaSetName) && !string.IsNullOrWhiteSpace(relicaSetServers) && relicaSetServers.Contains(";")) //配置IP列表则做集群连接
                {
                    var serverIps = relicaSetServers.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    var servers = new List<MongoServerAddress>();
                    serverIps.ToList().ForEach(ip =>
                    {
                        servers.Add(new MongoServerAddress(ip));
                    });
                    var setting = new MongoClientSettings();
                    setting.ReplicaSetName = relicaSetName;
                    setting.ConnectTimeout = new TimeSpan(0, 0, 30); //超时时间为30秒
                    //setting.ReadPreference = new ReadPreference(ReadPreferenceMode);
                    setting.Servers = servers;                    

                    MongoClient client = new MongoClient(setting);
                    return client;
                }
                else
                {
                    MongoClient client = new MongoClient(connString);
                    return client;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
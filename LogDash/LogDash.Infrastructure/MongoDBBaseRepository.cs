using System;
using System.Collections.Generic;
using System.Text;
using LogDash.Model;
using LogDash.Models;
using MongoDB.Driver;

namespace LogDash.Infrastructure
{
    public class MongoDBBaseRepository
    {
        private string _colname;
        public MongoDBBaseRepository(string colname)
        {
            _colname = colname;
        }

        public void Add<T>(T entity)
        {
            
            var mongodb = MongoDBHelper.GetMongoDB();
            var collection = mongodb.GetCollection<T>(_colname);
            collection.InsertOne(entity);
        }
        
        public IList<T> GetBy<T>(string appid,string id)
        {

            var mongodb = MongoDBHelper.GetMongoDB();
            var collection = mongodb.GetCollection<T>(_colname);

            FilterDefinition<T> wheres = Builders<T>.Filter.Eq("AppId", appid);
            if (!string.IsNullOrWhiteSpace(id))
            {
                wheres= wheres & Builders<T>.Filter.Eq("Id", id);
            }

            return collection.Find<T>(wheres).ToList<T>();
        }
        public void UpdateLogTypeName(string logTypeId, string name, string remark)
        {

            var mongodb = MongoDBHelper.GetMongoDB();
            var collection = mongodb.GetCollection<LogType>(_colname);
            collection.UpdateOne<LogType>(t => t.Id == logTypeId, Builders<LogType>.Update.Set("Name", name).Set("Remark", remark));
        }

        public void UpdateAuth(string Id, byte isAuth)
        {

            var mongodb = MongoDBHelper.GetMongoDB();
            var collection = mongodb.GetCollection<LogType>(_colname);
            collection.UpdateOne<LogType>(t => t.Id == Id, Builders<LogType>.Update.Set("IsAuthorize", isAuth));
        }

        public LogAuth GetAuthorInfo(string logtypeid)
        {

            var mongodb = MongoDBHelper.GetMongoDB();
            var collection = mongodb.GetCollection<LogAuth>(_colname);

            FilterDefinition<LogAuth> wheres = Builders<LogAuth>.Filter.Eq("LogTypeId", logtypeid);

            return collection.Find<LogAuth>(wheres).SingleOrDefault<LogAuth>();
        }
    }
}

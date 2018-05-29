using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LogDash.Domain;
using LogDash.Models;
using MongoDB.Bson;
using MongoDB.Driver;
//using MongoDB.Driver.Builders

namespace LogDash.Infrastructure
{
    public class MongoLogRecordDriver: ILogRecordDriver
    {
        public void Write(LogEntity entity)
        {
            ISplitTableStrategy splitTableStrategy = new MongoSplitTableStrategy("LogRecord", entity.AppId, DateTime.Now.ToString("yyyyMM"));
            var mongodb = MongoDBHelper.GetMongoDB();
            var collection = mongodb.GetCollection<LogEntity>(splitTableStrategy.GetTableName());
            collection.InsertOne(entity);
        }

        public object Read(LogQueryCondition condition)
        {
            ISplitTableStrategy splitTableStrategy = new MongoSplitTableStrategy("LogRecord", condition.AppId, DateTime.Now.ToString("yyyyMM"));
            var mongodb = MongoDBHelper.GetMongoDB();
            var collection = mongodb.GetCollection<LogEntity>(splitTableStrategy.GetTableName());
            
            return collection.Find<LogEntity>(FormFilterDefinitionBy(condition)).Skip(condition.skipNum).Limit(condition.TakeNum).ToList<LogEntity>();
        }


        /// <summary>
        /// 查询方案一
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private FilterDefinition<LogEntity> FormFilterDefinitionBy(LogQueryCondition condition)
        {
            FilterDefinition<LogEntity> wheres = null;
            if (!string.IsNullOrWhiteSpace(condition.AppId))
            {
                wheres = Builders<LogEntity>.Filter.Eq("AppId", condition.AppId);
            }
            else
            {
                throw new Exception("appid is null!");
            }
            if (!string.IsNullOrWhiteSpace(condition.LogTypeId))
            {
                FilterDefinition<LogEntity> where = Builders<LogEntity>.Filter.Eq("LogTypeId", condition.LogTypeId);
                wheres = wheres == null ? where : wheres& where;
            }
            else
            {
                throw new Exception("LogTypeId is null!");
            }
            if (!string.IsNullOrWhiteSpace(condition.OrderId))
            {
                FilterDefinition<LogEntity> where = Builders<LogEntity>.Filter.Eq("OrderId", condition.OrderId);
                wheres = wheres == null ? where : wheres & where;
            }
            if (!string.IsNullOrWhiteSpace(condition.RelatedId))
            {
                FilterDefinition<LogEntity> where = Builders<LogEntity>.Filter.Eq("RelatedId", condition.RelatedId);
                wheres = wheres == null ? where : wheres & where;
            }
            if (!string.IsNullOrWhiteSpace(condition.UserId))
            {
                FilterDefinition<LogEntity> where = Builders<LogEntity>.Filter.Eq("UserId", condition.UserId);
                wheres = wheres == null ? where : wheres & where;
            }
            if (!string.IsNullOrWhiteSpace(condition.UserName))
            {
                FilterDefinition<LogEntity> where = Builders<LogEntity>.Filter.Eq("UserName", condition.UserName);
                wheres = wheres == null ? where : wheres & where;
            }
            if (!string.IsNullOrWhiteSpace(condition.Label))
            {
                FilterDefinition<LogEntity> where = (Builders<LogEntity>.Filter.Regex("Label", new BsonRegularExpression(new Regex(condition.Label, RegexOptions.IgnoreCase))));
                wheres = wheres == null ? where : wheres & where;
            }

            return wheres;
        }


        /// <summary>
        /// 查询方案二
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        private Expression<Func<LogEntity, bool>> FormWhereExpressionBy(LogQueryCondition condition)
        {
            Expression<Func<LogEntity, bool>> exp = null;
            if (!string.IsNullOrWhiteSpace(condition.AppId))
            {

                Expression<Func<LogEntity, bool>> exp1 = a => a.AppId == condition.AppId;
                exp = exp == null ? exp1 : exp.And<LogEntity>(exp1);
            }
            else
            {
                throw new Exception("appid is null!");
            }
            if (!string.IsNullOrWhiteSpace(condition.LogTypeId))
            {
                Expression<Func<LogEntity, bool>> exp1 = a => a.LogTypeId == condition.LogTypeId;
                exp = exp == null ? exp1 : exp.And<LogEntity>(exp1);
            }
            else
            {
                throw new Exception("LogTypeId is null!");
            }
            if (!string.IsNullOrWhiteSpace(condition.OrderId))
            {
                Expression<Func<LogEntity, bool>> exp1 = a => a.OrderId == condition.OrderId;
                exp = exp == null ? exp1 : exp.And<LogEntity>(exp1);
            }
            if (!string.IsNullOrWhiteSpace(condition.RelatedId))
            {
                Expression<Func<LogEntity, bool>> exp1 = a => a.RelatedId == condition.RelatedId;
                exp = exp == null ? exp1 : exp.And<LogEntity>(exp1);
            }
            if (!string.IsNullOrWhiteSpace(condition.UserId))
            {
                Expression<Func<LogEntity, bool>> exp1 = a => a.UserId == condition.UserId;
                exp = exp == null ? exp1 : exp.And<LogEntity>(exp1);
            }
            if (!string.IsNullOrWhiteSpace(condition.UserName))
            {
                Expression<Func<LogEntity, bool>> exp1 = a => a.UserName == condition.UserName;
                exp = exp == null ? exp1 : exp.And<LogEntity>(exp1);
            }
            if (!string.IsNullOrWhiteSpace(condition.Label))
            {
                Expression<Func<LogEntity, bool>> exp1 = a => a.Label == condition.Label;
                exp = exp == null ? exp1 : exp.And<LogEntity>(exp1);
            }
            return exp;
        }

    }

    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}

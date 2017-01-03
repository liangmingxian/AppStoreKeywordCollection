using GetAppStoreKeyworks.Base;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAppStoreKeyworks.Utils
{
    /// <summary>
    /// MongoDB帮助类
    /// </summary>
    public sealed class MongoDbUtil
    {
        //private static MongoClient client;
        private static volatile MongoDbUtil instance = null;
        private IMongoDatabase _db = null;
        private static object threadSafeLocker = new object();

        /// <summary>
        /// 初始化数据库连接
        /// </summary>
        /// <param name="connectionString">数据库连接串</param>
        /// <param name="dbName">数据库名称</param>
        public static MongoDbUtil GetDBInstance(string connectionString, string dbName)
        {
            if (instance == null)
            {
                lock (threadSafeLocker)
                {
                    if (instance == null)
                    {
                        instance = new MongoDbUtil();
                        MongoClient client = new MongoClient(connectionString);
                        instance._db = client.GetDatabase(dbName);
                    }
                }
            }

            return instance;
        }

        #region 新增

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="model">数据对象</param>
        public async Task Add<T>(string collectionName, T model) where T : EntityBase
        {
            if (model == null)
            {
                throw new ArgumentNullException("model", "待插入数据不能为空");
            }
            IMongoCollection<T> collection = instance._db.GetCollection<T>(collectionName);
            await collection.InsertOneAsync(model);
        }

        /// <summary>
        /// 批量插入记录
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="model">数据对象</param>
        /// <returns></returns>
        public async Task AddBatch<T>(string collectionName, List<T> model) where T : EntityBase
        {
            if (model == null)
            {
                throw new ArgumentNullException("model", "待插入数据不能为空");
            }

            IMongoCollection<T> collection = instance._db.GetCollection<T>(collectionName);
            await collection.InsertManyAsync(model);
        }

        #endregion

        #region 更新
        public async Task UpdateOneAsync(string collectionName, FilterDefinitionBuilder<BsonDocument> filter, Dictionary<string, BsonValue> dictUpdate)
        {

        }

        ///// <summary>
        ///// 更新数据
        ///// </summary>
        ///// <param name="connectionString">数据库连接串</param>
        ///// <param name="dbName">数据库名称</param>
        ///// <param name="collectionName">集合名称</param>
        ///// <param name="query">查询条件</param>
        ///// <param name="dictUpdate">更新字段</param>
        //public static void Update(string collectionName, IMongoQuery query, Dictionary<string, BsonValue> dictUpdate)
        //{
        //    var db = GetDatabase(connectionString, dbName);
        //    var collection = db.GetCollection(collectionName);
        //    var update = new UpdateBuilder();
        //    if (dictUpdate != null && dictUpdate.Count > 0)
        //    {
        //        foreach (var item in dictUpdate)
        //        {
        //            update.Set(item.Key, item.Value);
        //        }
        //    }
        //    var d = collection.Update(query, update, UpdateFlags.Multi);
        //}

        #endregion

        //#region 查询

        ///// <summary>
        ///// 根据ID获取数据对象
        ///// </summary>
        ///// <typeparam name="T">数据类型</typeparam>
        ///// <param name="connectionString">数据库连接串</param>
        ///// <param name="dbName">数据库名称</param>
        ///// <param name="collectionName">集合名称</param>
        ///// <param name="id">ID</param>
        ///// <returns>数据对象</returns>
        //public static T GetById<T>(string connectionString, string dbName, string collectionName, ObjectId id)
        //    where T : EntityBase
        //{
        //    var db = GetDatabase(connectionString, dbName);
        //    var collection = db.GetCollection<T>(collectionName);
        //    return collection.FindOneById(id);
        //}

        ///// <summary>
        ///// 根据查询条件获取一条数据
        ///// </summary>
        ///// <typeparam name="T">数据类型</typeparam>
        ///// <param name="connectionString">数据库连接串</param>
        ///// <param name="dbName">数据库名称</param>
        ///// <param name="collectionName">集合名称</param>
        ///// <param name="query">查询条件</param>
        ///// <returns>数据对象</returns>
        //public static T GetOneByCondition<T>(string connectionString, string dbName, string collectionName, IMongoQuery query)
        //    where T : EntityBase
        //{
        //    var db = GetDatabase(connectionString, dbName);
        //    var collection = db.GetCollection<T>(collectionName);
        //    return collection.FindOne(query);
        //}

        ///// <summary>
        ///// 根据查询条件获取多条数据
        ///// </summary>
        ///// <typeparam name="T">数据类型</typeparam>
        ///// <param name="connectionString">数据库连接串</param>
        ///// <param name="dbName">数据库名称</param>
        ///// <param name="collectionName">集合名称</param>
        ///// <param name="query">查询条件</param>
        ///// <returns>数据对象集合</returns>
        //public static List<T> GetManyByCondition<T>(string connectionString, string dbName, string collectionName, IMongoQuery query)
        //    where T : EntityBase
        //{
        //    var db = GetDatabase(connectionString, dbName);
        //    var collection = db.GetCollection<T>(collectionName);
        //    return collection.Find(query).ToList();
        //}

        ///// <summary>
        ///// 根据集合中的所有数据
        ///// </summary>
        ///// <typeparam name="T">数据类型</typeparam>
        ///// <param name="connectionString">数据库连接串</param>
        ///// <param name="dbName">数据库名称</param>
        ///// <param name="collectionName">集合名称</param>
        ///// <returns>数据对象集合</returns>
        //public static List<T> GetAll<T>(string connectionString, string dbName, string collectionName)
        //    where T : EntityBase
        //{
        //    var db = GetDatabase(connectionString, dbName);
        //    var collection = db.GetCollection<T>(collectionName);
        //    return collection.FindAll().ToList();
        //}

        //#endregion

        //#region 删除

        ///// <summary>
        ///// 删除集合中符合条件的数据
        ///// </summary>
        ///// <param name="connectionString">数据库连接串</param>
        ///// <param name="dbName">数据库名称</param>
        ///// <param name="collectionName">集合名称</param>
        ///// <param name="query">查询条件</param>
        //public static void DeleteByCondition(string connectionString, string dbName, string collectionName, IMongoQuery query)
        //{
        //    var db = GetDatabase(connectionString, dbName);
        //    var collection = db.GetCollection(collectionName);
        //    collection.Remove(query);
        //}

        ///// <summary>
        ///// 删除集合中的所有数据
        ///// </summary>
        ///// <param name="connectionString">数据库连接串</param>
        ///// <param name="dbName">数据库名称</param>
        ///// <param name="collectionName">集合名称</param>
        //public static void DeleteAll(string connectionString, string dbName, string collectionName)
        //{
        //    var db = GetDatabase(connectionString, dbName);
        //    var collection = db.GetCollection(collectionName);
        //    collection.RemoveAll();
        //}

        //#endregion
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GetAppStoreKeyworks.Utils;
using GetAppStoreKeyworks.Common;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;

namespace GetAppStoreKeyworksTest
{
    [TestClass]
    public class DBTest
    {
        private MongoDbUtil _dbUtil;

        public DBTest()
        {
            this._dbUtil = MongoDbUtil.GetDBInstance("mongodb://localhost:27017", "person");
        }

        [TestMethod]
        public async Task TestAdd()
        {
            Item item = new Item();
            item.Name = "mingxian0";
            item.Sex = "Male";
            item.Age = 30;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            await this._dbUtil.Add<Item>("person", item);
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine("total is " + ts2.TotalMilliseconds);
        }

        [TestMethod]
        public async Task TestAddBatch()
        {
            List<Item> items = new List<Item>();

            for (int i = 4; i <= 10000; i++)
            {
                Item item = new Item();
                item.Name = "mingxian" + i;
                item.Sex = "Male";
                item.Age = 30;

                items.Add(item);
            }

            Console.WriteLine("共有" + items.Count + "条数据");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            await this._dbUtil.AddBatch<Item>("person", items);
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            Console.WriteLine("total is " + ts2.TotalMilliseconds);
        }
    }
}

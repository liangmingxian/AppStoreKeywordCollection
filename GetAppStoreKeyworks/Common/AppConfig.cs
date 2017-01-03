using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GetAppStoreKeyworks.Common
{
    public class AppConfig
    {
        private static string _dbConnStr = ConfigurationManager.AppSettings["DBConn"];

        public static string DbConnStr
        {
            get { return AppConfig._dbConnStr; }
        }

        private static string _dbName = ConfigurationManager.AppSettings["DBName"];

        public static string DbName
        {
            get { return AppConfig._dbName; }
        }
    }
}

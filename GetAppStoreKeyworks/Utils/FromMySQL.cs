using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetAppStoreKeyworks.Utils
{
    class FromMySQL
    {
        static void Main()
        {
            string M_str_sqlcon = "server=127.0.0.1;user id=root;password=123456;database=kwcs"; //根据自己的设置
            MySqlConnection mycon = new MySqlConnection();
            mycon.ConnectionString = M_str_sqlcon;

            try
            {
                mycon.Open();

                string sql = "select * from country";
                MySqlDataAdapter mda = new MySqlDataAdapter(sql, mycon);
                DataSet ds = new DataSet();
                mda.Fill(ds, "table1");

                foreach (DataRow row in ds.Tables["table1"].Rows)
                {
                    Console.WriteLine(row["STORE_FRONT"].ToString());  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            mycon.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Data.Sqlite;


namespace DB_1
{
    class DataAccess
    {
        //public async static void InitializeDatabase()
        public static void InitializeDatabase()
        {
            //await ApplicationData.Current.LocalFolder.CreateFileAsync("sqliteSample.db", CreationCollisionOption.OpenIfExists);
            //string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "sqliteSample.db");
            using (SqliteConnection db =
               new SqliteConnection("Filename=sqliteSampleBankgo1.db"))
            {
                db.Open();

                //String tableCommand = "CREATE TABLE IF NOT " +
                //    "EXISTS MyTable (Primary_Key INTEGER PRIMARY KEY, " +
                //    "Text_Entry NVARCHAR(2048) NULL)";

                string cmdCreateTable = "CREATE TABLE IF NOT EXISTS Customers " +
                                "(cust_id INTERGER PRIMARY KEY , " +
                                 "first_name NVARCHAR(50) NOT NULL ," +
                                 "last_name NVARCHAR(50) NOT NULL ," +
                                 "email  NVARCHAR(50) NOT NULL) ";

                SqliteCommand createTable = new SqliteCommand(cmdCreateTable, db);

                createTable.ExecuteReader();
            }
        }

        public static void AddData(string inputFName , string inputLastName)
        {
            using (SqliteConnection db = new SqliteConnection("Filename=sqliteSampleBankgo1.db"))
            {
                db.Open();

                SqliteCommand insertCmd = new SqliteCommand();
                insertCmd.Connection = db ;

                SqliteCommand getMaxID = new SqliteCommand("select max(cust_id) from Customers", db);
                SqliteDataReader queryCmd = getMaxID.ExecuteReader();
                int maxCustID =  0;
                while (queryCmd.Read()) {
                    maxCustID = Convert.ToInt32(queryCmd[0]);
                }



                insertCmd.CommandText = "insert into Customers values (@cust_id,@first_name,@last_name,@email)";
                insertCmd.Parameters.AddWithValue("@cust_id", maxCustID+1);
                insertCmd.Parameters.AddWithValue("@first_name", inputFName);
                insertCmd.Parameters.AddWithValue("@last_name", inputLastName);
                insertCmd.Parameters.AddWithValue("@email", "1");

                insertCmd.ExecuteReader();

                db.Close();
            }
        }

        public static List<String> GetData()
        {
            List<String> entries = new List<string>();
            using (SqliteConnection db = new SqliteConnection("Filename=sqliteSampleBankgo1.db"))
            {
                db.Open();
                SqliteCommand getCmd = new SqliteCommand("select first_name||'  '||last_name from Customers", db);
                SqliteDataReader queryCmd = getCmd.ExecuteReader();

                while (queryCmd.Read())
                {
                    entries.Add(queryCmd.GetString(0));
                }
                db.Close();
            }

                return entries;
        }
    }
}

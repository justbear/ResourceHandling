using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace ResourceDB
{
    /// <summary>
    /// Class for processing DB connection and data exchange
    /// </summary>
    static class MySQLQuieries
    {
        //[TODO]
        //Изменить ли на private?
        public static MySqlConnectionStringBuilder mysqlCSB = new MySqlConnectionStringBuilder();

        static MySQLQuieries()
        {
            mysqlCSB = new MySqlConnectionStringBuilder();
            mysqlCSB.Password = "justbear";
            mysqlCSB.UserID = "h5162_justbear";

            mysqlCSB.Server = "https://sr5.hostyes.ru";

            mysqlCSB.Database = "h5162_straminata";
        }

        /// <summary>
        /// Exequtes a Mysql query from passed string
        /// <param name="q">The query to execute</param>
        /// <returns>DataTable</returns>
        /// </summary>

        private static DataTable ExecQuery(string q)
        {
            string query = q;
            DataTable dt = new DataTable();

            try
            {
                using (MySqlConnection con = new MySqlConnection())
                {
                    con.ConnectionString = mysqlCSB.ConnectionString;
                    MySqlCommand com = new MySqlCommand(query, con);

                    con.Open();

                    using (MySqlDataReader dr = com.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            dt.Load(dr);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            
            return dt;
            
        }

        public static DataTable FethAllResources(ResourceType type)
        {
            string table = "resources";
            switch (type)
            {
                case ResourceType.Texture:
                    table = "Textures";
                    break;
                case ResourceType.Sound:
                    table="Sounds";
                    break;
                default:
                    break;
            }
            return ExecQuery("SELECT * FROM " + table);
        }
    }
}

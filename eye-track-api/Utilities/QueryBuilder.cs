using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eye_track_api.Utilities
{
    public class QueryBuilder
    {
        public static MySqlCommand UpdateBuilder(Dictionary<string, string> patch, string tableName, string idColumn, int id)
        {
            string query = "UPDATE " + tableName + " SET ";

            int current = 0;

            foreach (string key in patch.Keys)
            {
                if (current == 0)
                {
                    query += key + " = @" + key;
                }
                else
                {
                    query += ", " + key + " = @" + key;
                }

                current++;
            }

            query += " WHERE " + idColumn + " = " + id.ToString();

            MySqlCommand command = new MySqlCommand(query);

            foreach (var item in patch)
            {
                command.Parameters.AddWithValue("@" + item.Key, item.Value);
            }

            return command;
        }

    }
}

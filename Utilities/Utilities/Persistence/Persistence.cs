using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.CompilerServices;
using Npgsql;
using Utilities.Collections;

namespace Utilities.Persistence
{
    public static class Persistence
    {
        // used for insert, delete and update
        public static int Execute(string p_connectionString, string p_sql)
        {
            using (NpgsqlConnection dbConnection = new NpgsqlConnection(p_connectionString))
            {
                NpgsqlCommand command = new NpgsqlCommand(p_sql, dbConnection);
                return command.ExecuteNonQuery();
            }
        }

        public static IEnumerable<T> Query<T>(string p_connectionString, string p_sql)
        {
            using (NpgsqlConnection dbConnection = new NpgsqlConnection(p_connectionString))
            {
                dbConnection.Open();
                
                using (NpgsqlCommand command = new NpgsqlCommand(p_sql, dbConnection))
                {
                    using (NpgsqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        Type type = typeof(T);
                        PropertyInfo[] propertyInfos = type.GetProperties();
                        IList<T> result = new List<T>();
                
                        while (sqlDataReader.Read())
                        {
                            object entity = Activator.CreateInstance(type);
                            propertyInfos.Each(p_info =>
                            {
                                Console.WriteLine(p_info.Name);
                                
                                switch (p_info.PropertyType.Name)
                                {
                                    // bool, byte, char, decimal, double, float, int16, int32, int64, string, value
                                    case "bool":
                                        p_info.SetValue(entity, sqlDataReader.GetInt32(p_info.Name));
                                
                                    case "Int32":
                                        p_info.SetValue(entity, sqlDataReader.GetInt32(p_info.Name));
                                        break;
                                    case "String":
                                        p_info.SetValue(entity, sqlDataReader.GetString(p_info.Name));
                                        break;
                                }
                            });
                    
                            result.Add((T) entity);
                        }

                        return result;
                    }
                }
            }
        }
    }
}
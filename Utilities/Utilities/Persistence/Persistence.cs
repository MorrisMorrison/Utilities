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
                                switch (p_info.PropertyType.Name.ToLower())
                                {
                                    case "char":
                                        break;
                                    case "boolean":
                                        p_info.SetValue(entity, sqlDataReader.GetBoolean(p_info.Name));
                                        break;
                                    case "string":
                                        p_info.SetValue(entity, sqlDataReader.GetString(p_info.Name));
                                        break;
                                    case "byte":
                                        p_info.SetValue(entity, sqlDataReader.GetByte(p_info.Name));
                                        break;
                                    case "decimal":
                                        p_info.SetValue(entity, sqlDataReader.GetDecimal(p_info.Name));
                                        break;
                                    case "float":
                                        p_info.SetValue(entity, sqlDataReader.GetFloat(p_info.Name));
                                        break;
                                    case "double":
                                        p_info.SetValue(entity, sqlDataReader.GetDouble(p_info.Name));
                                        break;
                                    case "int16":
                                        p_info.SetValue(entity, sqlDataReader.GetInt16(p_info.Name));
                                        break;
                                    case "int32":
                                        p_info.SetValue(entity, sqlDataReader.GetInt32(p_info.Name));
                                        break;
                                    case "int64":
                                        p_info.SetValue(entity, sqlDataReader.GetInt64(p_info.Name));
                                        break;
                                    default:
                                        p_info.SetValue(entity, sqlDataReader.GetValue(p_info.Name));
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
        public static T QueryFirst<T>(string p_connectionString, string p_sql)
        {
            Type type = typeof(T);
            object entity = Activator.CreateInstance(type);
            
            using (NpgsqlConnection dbConnection = new NpgsqlConnection(p_connectionString))
            {
                dbConnection.Open();
                
                using (NpgsqlCommand command = new NpgsqlCommand(p_sql, dbConnection))
                {
                    using (NpgsqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        PropertyInfo[] propertyInfos = type.GetProperties();
                        
                        while (sqlDataReader.Read())
                        {
                            propertyInfos.Each(p_info =>
                            {
                                switch (p_info.PropertyType.Name.ToLower())
                                {
                                    case "char":
                                        break;
                                    case "boolean":
                                        p_info.SetValue(entity, sqlDataReader.GetBoolean(p_info.Name));
                                        break;
                                    case "string":
                                        p_info.SetValue(entity, sqlDataReader.GetString(p_info.Name));
                                        break;
                                    case "byte":
                                        p_info.SetValue(entity, sqlDataReader.GetByte(p_info.Name));
                                        break;
                                    case "decimal":
                                        p_info.SetValue(entity, sqlDataReader.GetDecimal(p_info.Name));
                                        break;
                                    case "float":
                                        p_info.SetValue(entity, sqlDataReader.GetFloat(p_info.Name));
                                        break;
                                    case "double":
                                        p_info.SetValue(entity, sqlDataReader.GetDouble(p_info.Name));
                                        break;
                                    case "int16":
                                        p_info.SetValue(entity, sqlDataReader.GetInt16(p_info.Name));
                                        break;
                                    case "int32":
                                        p_info.SetValue(entity, sqlDataReader.GetInt32(p_info.Name));
                                        break;
                                    case "int64":
                                        p_info.SetValue(entity, sqlDataReader.GetInt64(p_info.Name));
                                        break;
                                    default:
                                        p_info.SetValue(entity, sqlDataReader.GetValue(p_info.Name));
                                        break;
                                }
                            });
                            return (T) entity;
                        }
                    }
                }
            }

            return default(T);
        }
        public static T QueryFirstOrDefault<T>(string p_connectionString, string p_sql)
        {
            Type type = typeof(T);
            object entity = Activator.CreateInstance(type);
            
            using (NpgsqlConnection dbConnection = new NpgsqlConnection(p_connectionString))
            {
                dbConnection.Open();
                
                using (NpgsqlCommand command = new NpgsqlCommand(p_sql, dbConnection))
                {
                    using (NpgsqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        PropertyInfo[] propertyInfos = type.GetProperties();
                        
                        while (sqlDataReader.Read())
                        {
                            propertyInfos.Each(p_info =>
                            {
                                switch (p_info.PropertyType.Name.ToLower())
                                {
                                    case "char":
                                        break;
                                    case "boolean":
                                        p_info.SetValue(entity, sqlDataReader.GetBoolean(p_info.Name));
                                        break;
                                    case "string":
                                        p_info.SetValue(entity, sqlDataReader.GetString(p_info.Name));
                                        break;
                                    case "byte":
                                        p_info.SetValue(entity, sqlDataReader.GetByte(p_info.Name));
                                        break;
                                    case "decimal":
                                        p_info.SetValue(entity, sqlDataReader.GetDecimal(p_info.Name));
                                        break;
                                    case "float":
                                        p_info.SetValue(entity, sqlDataReader.GetFloat(p_info.Name));
                                        break;
                                    case "double":
                                        p_info.SetValue(entity, sqlDataReader.GetDouble(p_info.Name));
                                        break;
                                    case "int16":
                                        p_info.SetValue(entity, sqlDataReader.GetInt16(p_info.Name));
                                        break;
                                    case "int32":
                                        p_info.SetValue(entity, sqlDataReader.GetInt32(p_info.Name));
                                        break;
                                    case "int64":
                                        p_info.SetValue(entity, sqlDataReader.GetInt64(p_info.Name));
                                        break;
                                    default:
                                        p_info.SetValue(entity, sqlDataReader.GetValue(p_info.Name));
                                        break;
                                }
                            });
                            return (T) entity;
                        }
                    }
                }
            }

            return (T) entity;
        }
    }
}
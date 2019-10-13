using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Npgsql;
using NpgsqlTypes;
using Utilities.Collections;

namespace Utilities.Persistence
{
    public static class Persistence
    {
        public static int Execute(this IDbConnection p_connection, string p_sql)
        {
            p_connection.Open();
            NpgsqlCommand command = new NpgsqlCommand(p_sql, (NpgsqlConnection) p_connection);
            return command.ExecuteNonQuery();
        }

        public static int ExecuteInTransaction(this IDbConnection p_connection, string p_sql)
        {
            int affectedRows = 0;
            p_connection.Open();
            using (NpgsqlTransaction npgsqlTransaction = (NpgsqlTransaction) p_connection.BeginTransaction())
            {
                using (NpgsqlCommand command = new NpgsqlCommand(p_sql, (NpgsqlConnection) p_connection))
                {
                    try
                    {
                        affectedRows = command.ExecuteNonQuery();
                        npgsqlTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        npgsqlTransaction.Rollback();
                        throw;
                    }
                }
            }

            return affectedRows;
        }

        public static IEnumerable<T> Query<T>(this IDbConnection p_connection, string p_sql = "")
        {
            if (string.IsNullOrEmpty(p_sql))
            {
                p_sql = $@"SELECT * FROM ""{typeof(T).Name}""";
            }

            p_connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand(p_sql, (NpgsqlConnection) p_connection))
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
                                    p_info.SetValue(entity, sqlDataReader.GetChar(p_info.Name));
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


        public static T QueryFirst<T>(this IDbConnection p_connection, string p_sql)
        {
            Type type = typeof(T);
            object entity = Activator.CreateInstance(type);

            p_connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand(p_sql, (NpgsqlConnection) p_connection))
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
                                    p_info.SetValue(entity, sqlDataReader.GetChar(p_info.Name));
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

            return default(T);
        }

        public static T QueryFirstOrDefault<T>(this IDbConnection p_connection, string p_sql)
        {
            Type type = typeof(T);
            object entity = Activator.CreateInstance(type);

            p_connection.Open();

            using (NpgsqlCommand command = new NpgsqlCommand(p_sql, (NpgsqlConnection) p_connection))
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
                                    p_info.SetValue(entity, sqlDataReader.GetChar(p_info.Name));
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

            return (T) entity;
        }

        public static void Update<T>(this IDbConnection p_connection, T p_entity)
        {
            p_connection.Open();
            
            string sql = $@"UPDATE ""{typeof(T).Name}"" SET ";

            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            propertyInfos.Each((p_info, p_index) =>
            {
                sql += $@"{p_info.Name} = @{p_info.Name}";

                if (p_index != propertyInfos.Length - 1)
                {
                    sql += ", ";
                }
            });

            if (type.GetProperty("id") == null) return;

            sql += $" WHERE id = {type.GetProperty("id").GetValue(p_entity)};";
            Console.WriteLine(sql);

            using (NpgsqlCommand command = new NpgsqlCommand(sql, (NpgsqlConnection) p_connection))
            {
                propertyInfos.Each(p_info =>
                {
                    switch (p_info.PropertyType.Name.ToLower())
                    {
                        case "char":
                            command.Parameters.Add(new NpgsqlParameter(p_info.Name, NpgsqlDbType.Char)
                            {
                                Value = type.GetProperty(p_info.Name).GetValue(p_entity)
                            });
                            break;
                        case "boolean":
                            command.Parameters.Add(
                                new NpgsqlParameter(p_info.Name, NpgsqlDbType.Boolean)
                                {
                                    Value = type.GetProperty(p_info.Name).GetValue(p_entity)
                                });
                            break;
                        case "string":
                            command.Parameters.Add(
                                new NpgsqlParameter(p_info.Name, NpgsqlDbType.Varchar)
                                {
                                    Size = 200,
                                    Value = type.GetProperty(p_info.Name).GetValue(p_entity)
                                });
                            break;
                        case "byte":
                            command.Parameters.Add(
                                new NpgsqlParameter(p_info.Name, NpgsqlDbType.Bytea)
                                {
                                    Value = type.GetProperty(p_info.Name).GetValue(p_entity)
                                });
                            break;
                        case "decimal":
                            command.Parameters.Add(
                                new NpgsqlParameter(p_info.Name, NpgsqlDbType.Numeric)
                                {
                                    Value = type.GetProperty(p_info.Name).GetValue(p_entity)
                                });
                            break;
                        case "float":
                            command.Parameters.Add(
                                new NpgsqlParameter(p_info.Name, NpgsqlDbType.Numeric)
                                {
                                    Value = type.GetProperty(p_info.Name).GetValue(p_entity)
                                });
                            break;
                        case "double":
                            command.Parameters.Add(
                                new NpgsqlParameter(p_info.Name, NpgsqlDbType.Double)
                                {
                                    Value = type.GetProperty(p_info.Name).GetValue(p_entity)
                                });
                            break;
                        case "int16":
                            command.Parameters.Add(
                                new NpgsqlParameter(p_info.Name, NpgsqlDbType.Smallint)
                                {
                                    Value = type.GetProperty(p_info.Name).GetValue(p_entity)
                                });
                            break;
                        case "int32":
                            command.Parameters.Add(
                                new NpgsqlParameter(p_info.Name, NpgsqlDbType.Integer)
                                {
                                    Value = type.GetProperty(p_info.Name).GetValue(p_entity)
                                });
                            break;
                        case "int64":
                            command.Parameters.Add(
                                new NpgsqlParameter(p_info.Name, NpgsqlDbType.Bigint)
                                {
                                    Value = type.GetProperty(p_info.Name).GetValue(p_entity)
                                });
                            break;
                        default:
                            break;
                    }
                });

                Console.WriteLine(command.CommandText);
                Console.WriteLine(sql);

                command.ExecuteNonQuery();
            }
        }
    }
}
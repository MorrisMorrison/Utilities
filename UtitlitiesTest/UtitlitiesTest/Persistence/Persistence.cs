using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using Npgsql;
using NUnit.Framework;
using Utilities.Persistence;
using UtitlitiesTest.Collections;

namespace UtitlitiesTest.Persistence
{
    [TestFixture]
    public class PersistenceTests
    {
        private string _connectionString =
            "Server=localhost;Port=5432;Database=postgres;User ID=postgres;Password=changeme;";


        [SetUp]
        public void SetUp()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                string sqlScript = File.ReadAllText("../../../../SetupTestData.sql");
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(sqlScript, connection);
                npgsqlCommand.ExecuteNonQuery();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                NpgsqlCommand npgsqlCommand = new NpgsqlCommand(@"DROP TABLE ""Test"";", connection);
                npgsqlCommand.ExecuteNonQuery();
            }
        }


        [Test]
        public void SimpleExecuteTest()
        {
            string sql = @"INSERT INTO ""Test"" (id, name) VALUES(6, 'test6')";

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                int rows = connection.SimpleExecute(sql);
                Assert.AreEqual(1, rows);
            }


            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string select = @"SELECT * FROM ""Test"" WHERE id = 6";
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(select, connection);
                var npgsqlDataReader = command.ExecuteReader();
                while (npgsqlDataReader.Read())
                {
                    Test test = new Test();
                    test.id = npgsqlDataReader.GetInt32("id");
                    test.name = npgsqlDataReader.GetString("name");
                    
                    Assert.AreEqual(6, test.id);
                    Assert.AreEqual("test6", test.name);
                }
            }
        }

        [Test]
        public void SimpleQueryTest()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                IList<Test> result = connection.SimpleQuery<Test>().ToList();

                Assert.AreEqual(5, result.Count);

                for (int i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(i + 1, result[i].id);
                    Assert.AreEqual($@"test{i + 1}", result[i].name);
                }
            }
        }

        [Test]
        public void SimpleQueryFirstTest()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string sql = @"SELECT * FROM ""Test"" WHERE id = 1;";

                Test result = connection.SimpleQueryFirst<Test>(sql);
                
                Assert.AreEqual(1, result.id);
                Assert.AreEqual($@"test1", result.name);
            }
            
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string sql = @"SELECT * FROM ""Test"" WHERE id = 6;";

                Test result = connection.SimpleQueryFirst<Test>(sql);
                
                Assert.IsNull(result);
            }
        }


        [Test]
        public void SimpleQueryFirstOrDefaultTest()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string sql = @"SELECT * FROM ""Test"" WHERE id = 1;";

                Test result = connection.SimpleQueryFirstOrDefault<Test>(sql);
                
                Assert.AreEqual(1, result.id);
                Assert.AreEqual($@"test1", result.name);
            }
            
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string sql = @"SELECT * FROM ""Test"" WHERE id = 6;";

                Test result = connection.SimpleQueryFirstOrDefault<Test>(sql);
                
                Assert.AreEqual(0, result.id);
                Assert.IsNull(result.name);
            }
        }

        [Test]
        public void SimpleUpdateTest()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                Test newTest = new Test()
                {
                    id = 1,
                    name = "test16"
                };

                connection.SimpleUpdate(newTest);
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string sql = @"SELECT * FROM public.""Test"" WHERE id = 1;";
                Test first = connection.SimpleQueryFirst<Test>(sql);

                Assert.AreEqual(1, first.id);
                Assert.AreEqual($@"test16", first.name);
            }
        }

        [Test]
        public void SimpleCreateTest()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                Test newTest = new Test()
                {
                    id = 7,
                    name = "test7"
                };

                connection.SimpleCreate(newTest);
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string sql = @"SELECT * FROM public.""Test"" WHERE id = 7;";
                Test first = connection.SimpleQueryFirst<Test>(sql);

                Assert.AreEqual(7, first.id);
                Assert.AreEqual($@"test7", first.name);
            }
        }
        
    }
}
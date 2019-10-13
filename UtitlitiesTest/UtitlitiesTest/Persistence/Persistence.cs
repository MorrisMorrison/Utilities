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
            "Server=localhost;Port=5432;Database=tests;User ID=postgres;Password=postgres;";


        [OneTimeSetUp]
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

        [OneTimeTearDown]
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
        public void ExecuteTest()
        {
            string sql = @"INSERT INTO ""Test"" (id, name) VALUES(6, 'test6')";

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                int rows = connection.Execute(sql);
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
        public void QueryTest()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                IList<Test> result = connection.Query<Test>().ToList();

                Assert.AreEqual(5, result.Count);

                for (int i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(i + 1, result[i].id);
                    Assert.AreEqual($@"test{i + 1}", result[i].name);
                }
            }
        }

        [Test]
        public void QueryFirstTest()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string sql = @"SELECT * FROM ""Test"" WHERE id = 1;";

                Test result = connection.QueryFirst<Test>(sql);
                
                Assert.AreEqual(1, result.id);
                Assert.AreEqual($@"test1", result.name);
            }
            
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string sql = @"SELECT * FROM ""Test"" WHERE id = 6;";

                Test result = connection.QueryFirst<Test>(sql);
                
                Assert.IsNull(result);
            }
        }


        [Test]
        public void QueryFirstOrDefaultTest()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string sql = @"SELECT * FROM ""Test"" WHERE id = 1;";

                Test result = connection.QueryFirstOrDefault<Test>(sql);
                
                Assert.AreEqual(1, result.id);
                Assert.AreEqual($@"test1", result.name);
            }
            
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string sql = @"SELECT * FROM ""Test"" WHERE id = 6;";

                Test result = connection.QueryFirstOrDefault<Test>(sql);
                
                Assert.AreEqual(0, result.id);
                Assert.IsNull(result.name);
            }
        }

        [Test]
        public void UpdateTest()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                Test newTest = new Test()
                {
                    id = 1,
                    name = "test16"
                };

                connection.Update(newTest);
            }

            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                string sql = @"SELECT * FROM public.""Test"" WHERE id = 1;";
                Test first = connection.QueryFirst<Test>(sql);

                Assert.AreEqual(1, first.id);
                Assert.AreEqual($@"test16", first.name);
            }
        }
    }
}
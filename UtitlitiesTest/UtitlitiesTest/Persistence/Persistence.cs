using System.Linq;
using NUnit.Framework;
using UtitlitiesTest.Collections;

namespace UtitlitiesTest.Persistence
{
    
    [TestFixture]
    public class PersistenceTests
    {

        private string _connectionString = "Server=localhost;Port=5432;Database=tests;User ID=postgres;Password=postgres;";

        [Test]
        public void ExecuteTest()
        {
            Test test = new Test()
            {
                id = 1,
                name = "test1"
            };
        }
        
        [Test]
        public void QueryTest()
        {
            Test test = new Test()
            {
                id = 1,
                name = "test1"
            };

            string sql = @"select * from ""Test"" where id = 1";

            Test result = Utilities.Persistence.Persistence.Query<Test>(_connectionString, sql).ToList().FirstOrDefault();
            
            Assert.AreEqual(test.id, result.id);
            Assert.AreEqual(test.name, result.name);
            
        }
        
        
        
        
    }
}
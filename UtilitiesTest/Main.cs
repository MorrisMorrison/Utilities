using NUnit.Framework;
using NUnit.Framework.Internal;
using UtitlitiesTest.Collections;
using UtitlitiesTest.Persistence;

namespace UtitlitiesTest
{
    
    [TestFixture]
    public class Main
    {

        [Test, Explicit]
        public void RunAllTests()
        {
            CollectionsTests collectionTests = new CollectionsTests();
            collectionTests.EachExtensionTest();
            collectionTests.EachWithIndexTest();

            PersistenceTests persistenceTests = new PersistenceTests();
            
            persistenceTests.SimpleExecuteTest();
            persistenceTests.SimpleQueryTest();
            persistenceTests.SimpleQueryFirstTest();
            persistenceTests.SimpleQueryFirstOrDefaultTest();
        }

    }
}
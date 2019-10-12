using System;
using System.Collections.Generic;
using NUnit.Framework;
using Utilities.Collections;

namespace UtitlitiesTest.Collections
{
    [TestFixture]
    public class CollectionsTests
    {

        [Test]
        public void EachExtensionTest()
        {
            IList<Test> tests = new List<Test>()
            {
                new Test()
                {
                    id = 1,
                    name = "test1"
                },
                new Test()
                {
                    id = 2,
                    name = "test2"
                }
            };
            
            tests.Each(p_test =>
            {
                p_test.id = 0;
            });

            foreach (var test in tests)
            {
                Assert.AreEqual(0, test.id);
            }
        }

        [Test]
        public void EachWithIndexTest()
        {
            IList<Test> tests = new List<Test>()
            {
                new Test()
                {
                    id = 0,
                    name = "test1"
                },
                new Test()
                {
                    id = 1,
                    name = "test2"
                }
            };
            
            tests.Each((p_test, index) =>
            {
                Assert.AreEqual(p_test.id, index);
            });
        }
    }
    
    
    class Test
    {
        public int id { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return $"id = {id} | name = {name}";
        }
    }
}
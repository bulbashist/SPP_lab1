using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.TestClasses;
using System.Collections.Generic;
using Faker.Core;
using Faker.Tests.TestClasses;

namespace Faker.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCycle()
        {
            ClassFaker faker = new ClassFaker();
            try 
            { 
                Test1 t1 = faker.Create<Test1>();
            }
            catch (System.Exception ex)
            {
                Assert.AreEqual("Cycling dependency", ex.Message);
                return;
            }
            Assert.AreEqual(0, 1);
        }

        [TestMethod]
        public void TestDifference()
        {
            ClassFaker faker = new();
            int a = faker.Create<int>(), b = faker.Create<int>();
            Assert.IsTrue(a != b);
            string s1 = faker.Create<string>(), s2 = faker.Create<string>();
            Assert.IsTrue(!string.Equals(s1, s2));
        }

        [TestMethod]
        public void TestList()
        {
            ClassFaker faker = new();
            List<Item> list = faker.Create<List<Item>>();
            Assert.IsTrue((list.Count != 0) && (list[0] != null));

        }

        [TestMethod]
        public void TestDate()
        {
            ClassFaker faker = new();
            System.DateTime? dateTime = faker.Create<System.DateTime>();
            Assert.IsTrue((dateTime != null));
        }

        [TestMethod]
        public void TestConstructor()
        {
            ClassFaker faker = new();
            Test4 t4 = faker.Create<Test4>();
            Assert.IsTrue(t4.b != 0);
        }
    }
}
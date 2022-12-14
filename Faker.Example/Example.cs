using Faker.Core;
using Tests.TestClasses;
using System.Collections.Generic;

namespace Faker.Example 
{
    class Example 
    {   
        public static void Main() 
        {

            ClassFaker faker = new();
           
            int n1 = faker.Create<int>();
            float n2 = faker.Create<float>();
            double n3 = faker.Create<double>();
            string str = faker.Create<string>();

            Console.WriteLine(
                $"{n1}\n" +
                $"{n2}\n" +
                $"{n3}\n" +
                $"{str}\n"
            );
            
            Item codeIdentity = faker.Create<Item>();
            Console.WriteLine(codeIdentity);
               
            List<int> list = faker.Create<List<int>>();
            Console.WriteLine("Integer list:\n");
            list.ForEach(Console.WriteLine);
            Console.WriteLine("\n");

            List<Item> identityList = faker.Create<List<Item>>();
            Console.WriteLine("Item list:\n");
            identityList.ForEach(Console.WriteLine);
            Console.WriteLine("\n");

            DateTime dateTime = faker.Create<DateTime>();
            Console.WriteLine(dateTime);
        }
    }
}
using System.Collections;
using System.Collections.Immutable;
using System.Reflection;

namespace Faker.Core
{
    internal class Generators
    {
        public readonly static Dictionary<string, Func<Type, object>> generators = new();
        protected readonly static Random random = new();

        static Generators()
        {
            generators[typeof(int).Name] = (type) => random.Next();

            generators[typeof(long).Name] = (type) => random.NextInt64();

            generators[typeof(float).Name] = (type) => random.NextSingle();

            generators[typeof(double).Name] = (type) => random.NextDouble();

            generators[typeof(DateTime).Name] = (type) => new DateTime(random.NextInt64(DateTime.Today.Ticks));

            generators[typeof(char).Name] = (type) =>
            {
                string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
                return chars[random.Next(chars.Length)];
            };

            generators[typeof(string).Name] = (type) =>
            {
                string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
                int length = random.Next(10) + 2;
                string str = new("");
                for (int i = 0; i < length; i++) str += chars[random.Next(chars.Length)];
                return str;
            };

            generators[typeof(List<>).Name] = (type) =>
            {
                IList list = (Activator.CreateInstance(type) as IList)!;
                Random rnd = new();
                int length = rnd.Next(5) + 2;
                Type temp = type.GetGenericArguments()[0];
                if (generators.ContainsKey(temp.Name))
                {
                    for (int i = 0; i < length; i++) list.Add(generators[temp.Name](temp));
                }
                else
                {
                    ClassFaker faker = new();
                    for (int i = 0; i < length; i++) list.Add(faker.Create(temp));
                }
                return list;
            };
        }
    }
}
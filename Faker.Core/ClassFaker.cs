using System.Reflection;

namespace Faker.Core
{
    public class ClassFaker
    {
        public T Create<T>() => (T)Create(typeof(T));
        private readonly List<Type> dependences = new();

        public object Create(Type type)
        {
            if (Generators.generators.ContainsKey(type.Name)) return Generators.generators[type.Name](type);

            if (dependences.Contains(type)) throw new Exception("Cycling dependency");
            dependences.Add(type);

            object result = CreateComplexObject(type);

            dependences.Remove(type);
            return result;
        }

        private object CreateComplexObject(Type type)
        {
            var constructors = new List<ConstructorInfo>(type.GetConstructors(BindingFlags.Instance | BindingFlags.Public));
            if (constructors.Count == 0) throw new Exception("No constructors available");

            var constructor = constructors.MaxBy(x => x.GetParameters().Length)!;
            List<object> objparameters = new();
            foreach (var parameter in constructor.GetParameters()) objparameters.Add(this.Create(parameter.ParameterType));
            object res = constructor.Invoke(objparameters.ToArray());

            var publicSetters = type.GetProperties().Where(x => x.SetMethod.IsPublic);
            IEnumerable<FieldInfo> publicFields = type.GetFields().Where(x => x.IsPublic);
            foreach (var field in publicFields) field.SetValue(res, this.Create(field.FieldType));
            foreach (var property in publicSetters) property.SetValue(res, this.Create(property.PropertyType));
            return res; 
        }
    }
}

 
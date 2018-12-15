using System;
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generation.Templates
{
    public class DataLoader : BaseTemplate
    {
        public string ReturnType { get; private set; }

        public string Name { get; private set; }

        public string ClassName { get; private set; }

        public string KeyType { get; private set; }

        public DataLoader(MethodInfo method, Type keyType, Type returnType)
        {
            ReturnType = returnType.GetCSharpTypeName();
            Name = method.Name;
            ClassName = $"{method.DeclaringType.Namespace}.{method.DeclaringType.Name}";
            KeyType = $"{keyType.Namespace}.{keyType.Name}";
        }
    }
}
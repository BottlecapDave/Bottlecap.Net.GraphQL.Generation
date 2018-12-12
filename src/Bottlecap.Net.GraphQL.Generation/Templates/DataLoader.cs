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
            ReturnType = GetClassName(returnType);
            Name = method.Name;
            ClassName = $"{method.DeclaringType.Namespace}.{method.DeclaringType.Name}";
            KeyType = $"{keyType.Namespace}.{keyType.Name}";
        }

        private string GetClassName(Type returnType)
        {
            var className = returnType.Name;

            // If we're a generic, then our name will be something like "IEnumerable`1".
            // We need to extract the type name and then manually add the generics as if it was C# code.
            if (returnType.IsGenericType)
            {
                var genericIndicatorIndex = className.LastIndexOf('`');
                if (genericIndicatorIndex < 0)
                {
                    throw new InvalidOperationException("Unable to determine generic types");
                }

                // Remove the weird generic indicator and start our generic definition
                className = $"{className.Substring(0, genericIndicatorIndex)}<";
                foreach (var genericType in returnType.GenericTypeArguments)
                {
                    className = $"{className}{GetClassName(genericType)}, ";
                }

                // Remove the trailing comma from our last generic and close the generic definition
                className = $"{className.TrimEnd(',', ' ')}>";
            }

            return $"{returnType.Namespace}.{className}";
        }
    }
}
using System;

namespace Bottlecap.Net.GraphQL.Generation.Templates
{
    public class FullTypeName
    {
        private readonly Type _type;

        public FullTypeName(Type type)
        {
            _type = type;
        }

        public override string ToString()
        {
            return GetFullTypeName(_type);
        }

        private string GetFullTypeName(Type returnType)
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
                    className = $"{className}{GetFullTypeName(genericType)}, ";
                }

                // Remove the trailing comma from our last generic and close the generic definition
                className = $"{className.TrimEnd(',', ' ')}>";
            }

            return $"{returnType.Namespace}.{className}";
        }
    }
}

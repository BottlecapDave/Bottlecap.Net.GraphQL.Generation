using GraphQL;
using System;
using System.Collections;
using GraphQLTypes = GraphQL.Types;

namespace Bottlecap.Net.GraphQL.Generation
{
    public static class TypeExtensions
    {
        public static string GetCSharpTypeName(this Type type)
        {
            var className = type.Name;

            // If we're a generic, then our name will be something like "IEnumerable`1".
            // We need to extract the type name and then manually add the generics as if it was C# code.
            if (type.IsGenericType)
            {
                var genericIndicatorIndex = className.LastIndexOf('`');
                if (genericIndicatorIndex < 0)
                {
                    throw new InvalidOperationException("Unable to determine generic types");
                }

                // Remove the weird generic indicator and start our generic definition
                className = $"{className.Substring(0, genericIndicatorIndex)}<";
                foreach (var genericType in type.GenericTypeArguments)
                {
                    className = $"{className}{genericType.GetCSharpTypeName()}, ";
                }

                // Remove the trailing comma from our last generic and close the generic definition
                className = $"{className.TrimEnd(',', ' ')}>";
            }

            return $"{type.Namespace}.{className}";
        }

        public static string GetCSharpGraphType(this Type type, bool? isNullable = null)
        {
            if (type.IsArray)
            {
                return $"ListGraphType<{type.GetElementType().GetCSharpGraphType()}>";
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type) && type.IsGenericType)
            {
                if (type.IsGenericType)
                {
                    return $"ListGraphType<{type.GenericTypeArguments[0].GetCSharpGraphType()}>";
                }

                return $"ListGraphType<{type.GetCSharpGraphType()}>";
            }
            else if (type.IsGenericType)
            {
                var className = type.Name;

                var genericIndicatorIndex = className.LastIndexOf('`');
                if (genericIndicatorIndex < 0)
                {
                    throw new InvalidOperationException("Unable to determine generic types");
                }

                // Remove the weird generic indicator and start our generic definition
                className = $"{className.Substring(0, genericIndicatorIndex)}GraphType<";
                foreach (var genericType in type.GenericTypeArguments)
                {
                    className = $"{className}{genericType.GetCSharpGraphType()}, ";
                }

                // Remove the trailing comma from our last generic and close the generic definition
                return $"{className.TrimEnd(',', ' ')}>";
            }

            try
            {
                var graphType = type.GetGraphTypeFromType(isNullable == true);
                if (typeof(GraphQLTypes.NonNullGraphType).IsAssignableFrom(graphType) && graphType.IsGenericType)
                {
                    return graphType.GenericTypeArguments[0].Name;
                }

                return graphType.Name;
            }
            catch (ArgumentOutOfRangeException)
            {
                // This is thrown if the provided type can't be converted into a native graph type
                return $"{type.Name}GraphType";
            }
        }
    }
}

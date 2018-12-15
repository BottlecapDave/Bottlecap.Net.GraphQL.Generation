using GraphQL;
using System;
using System.Collections;
using GraphQLTypes = GraphQL.Types;

namespace Bottlecap.Net.GraphQL.Generation.Templates
{
    public class GraphTypeName
    {
        private readonly Type _type;
        private bool _isNullable;

        public GraphTypeName(Type type, bool isNullable)
        {
            _type = type;
            _isNullable = isNullable;
        }

        public override string ToString()
        {
            return GetGraphTypeName(_type, _isNullable);
        }

        private string GetGraphTypeName(Type type, bool? isNullable = null)
        {
            if (type.IsArray)
            {
                return $"ListGraphType<{GetGraphTypeName(type.GetElementType())}>";
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type) && type.IsGenericType)
            {
                if (type.IsGenericType)
                {
                    return $"ListGraphType<{GetGraphTypeName(type.GenericTypeArguments[0])}>";
                }

                return $"ListGraphType<{GetGraphTypeName(type)}>";
            }
            else if (type.IsGenericType)
            {

            }

            var graphType = type.GetGraphTypeFromType(isNullable == true);
            if (typeof(GraphQLTypes.NonNullGraphType).IsAssignableFrom(graphType) && graphType.IsGenericType)
            {
                return graphType.GenericTypeArguments[0].Name;
            }

            var graphTypeName = graphType.Name;

            return graphTypeName;
        }
    }
}

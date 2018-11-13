using GraphQL;
using System;
using System.Collections;
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generation.Templates
{
    public class ExplicitFieldType : BaseFieldType
    {
        public string TypeName
        {
            get
            {
                if (_property.PropertyType.IsArray)
                {
                    return $"ListGraphType<{TryExtractGeneric(_property.PropertyType.GetElementType().GetGraphTypeFromType(IsNullable)).Name}>";
                }
                else if (typeof(IEnumerable).IsAssignableFrom(_property.PropertyType))
                {
                    return $"ListGraphType<{TryExtractGeneric(_property.PropertyType).Name}GraphType>";
                }

                return $"{_property.PropertyType.Name}GraphType";
            }
        }

        public ExplicitFieldType(PropertyInfo property, PropertyDefinition definition)
            : base(property, definition)
        {
        }

        private Type TryExtractGeneric(Type type)
        {
            if (type.IsGenericType)
            {
                try
                {
                    return type.GetGenericArguments()[0].GetGraphTypeFromType(IsNullable);
                }
                catch (ArgumentOutOfRangeException)
                {
                    return type.GetGenericArguments()[0];
                }
            }

            return type;
        }
    }
}
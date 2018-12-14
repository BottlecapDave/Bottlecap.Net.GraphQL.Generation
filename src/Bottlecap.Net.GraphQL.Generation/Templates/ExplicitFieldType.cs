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
                if (_definition.Property.PropertyType.IsArray)
                {
                    return $"ListGraphType<{TryExtractGeneric(_definition.Property.PropertyType.GetElementType().GetGraphTypeFromType(IsNullable)).Name}>";
                }
                else if (typeof(IEnumerable).IsAssignableFrom(_definition.Property.PropertyType))
                {
                    return $"ListGraphType<{TryExtractGeneric(_definition.Property.PropertyType).Name}GraphType>";
                }

                return $"{_definition.Property.PropertyType.Name}GraphType";
            }
        }

        public ExplicitFieldType(PropertyDefinition definition)
            : base(definition)
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
using GraphQL;
using System;
using System.Collections;
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generation.Templates
{
    public class ExplicitFieldType : BaseFieldType
    {
        public string GraphTypeName
        {
            get
            {
                return _definition.Property.PropertyType.GetCSharpGraphType(IsNullable);
            }
        }

        public string TypeName
        {
            get
            {
                return _definition.Property.PropertyType.GetCSharpTypeName();
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
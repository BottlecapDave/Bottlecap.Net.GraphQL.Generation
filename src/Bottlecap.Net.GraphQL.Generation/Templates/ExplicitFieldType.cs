using GraphQL;
using System;

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
    }
}
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generator.Templates
{
    public class ExplicitFieldType : BaseFieldType
    {
        public string TypeName
        {
            get
            {
                return _property.PropertyType.Name;
            }
        }

        public ExplicitFieldType(PropertyInfo property)
            : base(property)
        {
        }
    }
}
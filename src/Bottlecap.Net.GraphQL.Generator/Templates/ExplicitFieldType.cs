using System.Collections;
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generator.Templates
{
    public class ExplicitFieldType : BaseFieldType
    {
        public string TypeName
        {
            get
            {
                if (_property.PropertyType.IsArray)
                {
                    return $"ListGraphType<{_property.PropertyType.GetElementType().Name}>";
                }
                else if (typeof(IEnumerable).IsAssignableFrom(_property.PropertyType))
                {
                    return $"ListGraphType<{_property.PropertyType.GetGenericArguments()[0].Name}GraphType>";
                }

                return _property.PropertyType.Name;
            }
        }

        public ExplicitFieldType(PropertyInfo property)
            : base(property)
        {
        }
    }
}
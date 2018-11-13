namespace Bottlecap.Net.GraphQL.Generation
{
    public class PropertyDefinition
    {
        public string Name { get; set;  }

        public bool IsIgnored { get; set; }

        public bool IsNullable { get; set; }

        public PropertyDefinition(string name)
        {
            Name = name;
        }
    }
}

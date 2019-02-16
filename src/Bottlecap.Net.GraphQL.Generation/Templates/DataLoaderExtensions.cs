using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bottlecap.Net.GraphQL.Generation.Templates
{
    public class DataLoaderExtensions : BaseTemplate
    {
        private readonly TypeDefinition _typeDefinition;

        public string Name
        {
            get
            {
                var name = _typeDefinition.Type.GetCSharpTypeName();
                var namespaceIndex = name.LastIndexOf('.');
                if (namespaceIndex >= 0)
                {
                    name = name.Substring(namespaceIndex + 1);
                }

                return name.Replace('<', '_').Replace(',', '_').Replace('>', '_').Replace(" ", "");
            }
        }

        public List<DataLoader> DataLoaders { get; private set; }

        public DataLoaderExtensions(Generator generator, TypeDefinition typeDefinition)
            : base(generator)
        {
            _typeDefinition = typeDefinition;
            DataLoaders = new List<DataLoader>();

            var taskType = typeof(Task);
            var dictionaryType = typeof(IDictionary<,>);
            var methods = _typeDefinition.Type.GetMethods();
            foreach (var method in methods)
            {
                if (taskType.IsAssignableFrom(method.ReturnType) &&
                    method.ReturnType.IsGenericType)
                {
                    var innerType = method.ReturnType.GenericTypeArguments[0];
                    if (innerType.IsGenericType && innerType.GetGenericTypeDefinition() == dictionaryType)
                    {
                        DataLoaders.Add(new DataLoader(generator, method, innerType.GenericTypeArguments[0], innerType.GenericTypeArguments[1]));
                    }
                }
            }
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bottlecap.Net.GraphQL.Generation.Templates
{
    public class DataLoaderExtensions : BaseTemplate
    {
        private readonly TypeDefinition _typeDefinition;

        public string Name {  get { return _typeDefinition.Type.Name; } }

        public List<DataLoader> DataLoaders { get; private set; }

        public DataLoaderExtensions(TypeDefinition typeDefinition)
        {
            _typeDefinition = typeDefinition;
            DataLoaders = new List<DataLoader>();

            var taskType = typeof(Task);
            var dictionaryType = typeof(IDictionary<,>);
            var methods = _typeDefinition.Type.GetMethods();
            foreach (var method in methods)
            {
                if (taskType.IsAssignableFrom(method.ReturnType))
                {
                    var innerType = method.ReturnType.GenericTypeArguments[0];
                    if (innerType.IsGenericType && innerType.GetGenericTypeDefinition() == dictionaryType)
                    {
                        DataLoaders.Add(new DataLoader(method, innerType.GenericTypeArguments[0], innerType.GenericTypeArguments[1]));
                    }
                }
            }
        }
    }
}

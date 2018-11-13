using Bottlecap.Net.GraphQL.Generation.Attributes;
using Bottlecap.Net.GraphQL.Generation.Templates;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generation
{
    public class Generator
    {
        private readonly List<TypeDefinition> _inputGraphTypesToGenerate = new List<TypeDefinition>();
        private readonly List<TypeDefinition> _graphTypesToGenerate = new List<TypeDefinition>();

        public void RegisterTypes(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                var attribute = type.GetCustomAttribute<GraphTypeAttribute>();
                if (attribute != null)
                {
                    if (attribute.IsInput)
                    {
                        _inputGraphTypesToGenerate.Add(new TypeDefinition(type) { IsInput = true });
                    }
                    else
                    {
                        _graphTypesToGenerate.Add(new TypeDefinition(type));
                    }
                }
            }
        }

        public void RegisterInputGraphTypes(params TypeDefinition[] typesToGenerate)
        {
            _inputGraphTypesToGenerate.AddRange(typesToGenerate);
        }

        public void RegisterGraphTypes(params TypeDefinition[] typesToGenerate)
        {
            _graphTypesToGenerate.AddRange(typesToGenerate);
        }

        public void Generate(string outputPath, string targetNamespace)
        {
            var content = Generate(targetNamespace);

            File.WriteAllText(outputPath, content);
        }

        public string Generate(string targetNamespace)
        {
            var shell = new Shell()
            {
                Namespace = targetNamespace
            };

            foreach (var item in _graphTypesToGenerate)
            {
                shell.Classes.Add(new GraphType(item));
            }

            foreach (var item in _inputGraphTypesToGenerate)
            {
                shell.Classes.Add(new GraphType(item, true));
            }

            return shell.ToString();
        }
    }
}

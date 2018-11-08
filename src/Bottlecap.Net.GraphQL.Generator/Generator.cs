using Bottlecap.Net.GraphQL.Generator.Templates;
using System;
using System.Collections.Generic;
using System.IO;

namespace Bottlecap.Net.GraphQL.Generator
{
    public class Generator
    {
        private readonly List<TypeDefinition> _inputGraphTypesToGenerate = new List<TypeDefinition>();
        private readonly List<TypeDefinition> _graphTypesToGenerate = new List<TypeDefinition>();

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
                shell.Classes.Add(new OutputType(item));
            }

            foreach (var item in _inputGraphTypesToGenerate)
            {
                shell.Classes.Add(new InputType(item));
            }

            return shell.ToString();
        }
    }
}

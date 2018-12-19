using Bottlecap.Net.GraphQL.Generation.Attributes;
using Bottlecap.Net.GraphQL.Generation.Templates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generation
{
    public class Generator
    {
        private readonly List<TypeDefinition> _graphTypesToGenerate = new List<TypeDefinition>();
        private readonly List<TypeDefinition> _dataLoaderTypesToGenerate = new List<TypeDefinition>();

        private readonly ILogger _logger;

        public Generator(ILogger logger = null)
        {
            _logger = logger;
        }

        public void RegisterTypes(Assembly assembly)
        {
            var types = GetLoadableTypes(assembly);
            foreach (var type in types)
            {
                var graphTypeAttribute = type.GetCustomAttribute<GraphTypeAttribute>();
                if (graphTypeAttribute != null)
                {
                    _graphTypesToGenerate.Add(new TypeDefinition(type, graphTypeAttribute));
                }
                else
                {
                    var dataLoaderAttribute = type.GetCustomAttribute<DataLoadersAttribute>();
                    if (dataLoaderAttribute != null)
                    {
                        _dataLoaderTypesToGenerate.Add(new TypeDefinition(type));
                    }
                }
            }
        }

        public void RegisterGraphTypes(params TypeDefinition[] typesToGenerate)
        {
            _graphTypesToGenerate.AddRange(typesToGenerate);
        }

        public void RegisterDataLoaders(params TypeDefinition[] typesToGenerate)
        {
            _dataLoaderTypesToGenerate.AddRange(typesToGenerate);
        }

        public void Generate(string outputPath, string targetNamespace)
        {
            var content = Generate(targetNamespace);

            File.WriteAllText(outputPath, content);
        }

        public string Generate(string targetNamespace)
        {
            _logger?.WriteInfo($"Generating classes in namespace '{targetNamespace}");

            var shell = new Shell()
            {
                Namespace = targetNamespace
            };

            foreach (var item in _graphTypesToGenerate)
            {
                _logger?.WriteInfo($"Generating GraphType for '{item.Type.Name}");

                if (item.Type.IsEnum)
                {
                    shell.Classes.Add(new EnumerationGraphType(item));
                }
                else
                {
                    shell.Classes.Add(new GraphType(item));
                }
            }

            foreach (var item in _dataLoaderTypesToGenerate)
            {
                _logger?.WriteInfo($"Generating DataLoaders for '{item.Type.Name}");
                var dataLoaderClass = new DataLoaderExtensions(item);
                if (dataLoaderClass.DataLoaders.Any())
                {
                    shell.Classes.Add(dataLoaderClass);
                }
            }

            return shell.Classes.Any() ? shell.ToString() : null;
        }

        private IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                _logger?.WriteError(e.Message);
                return e.Types.Where(t => t != null);
            }
        }
    }
}

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
        private readonly List<TypeDefinition> _inputGraphTypesToGenerate = new List<TypeDefinition>();
        private readonly List<TypeDefinition> _graphTypesToGenerate = new List<TypeDefinition>();
        private readonly List<TypeDefinition> _dataLoaderTypesToGenerate = new List<TypeDefinition>();

        public void RegisterTypes(Assembly assembly)
        {
            var types = GetLoadableTypes(assembly);
            foreach (var type in types)
            {
                var graphTypeAttribute = type.GetCustomAttribute<GraphTypeAttribute>();
                if (graphTypeAttribute != null)
                {
                    if (graphTypeAttribute.IsInput && type.IsEnum == false)
                    {
                        _inputGraphTypesToGenerate.Add(new TypeDefinition(type) { IsInput = true });
                    }
                    else
                    {
                        _graphTypesToGenerate.Add(new TypeDefinition(type));
                    }
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

        public void RegisterInputGraphTypes(params TypeDefinition[] typesToGenerate)
        {
            _inputGraphTypesToGenerate.AddRange(typesToGenerate);
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
            var shell = new Shell()
            {
                Namespace = targetNamespace
            };

            foreach (var item in _graphTypesToGenerate)
            {
                if (item.Type.IsEnum)
                {
                    shell.Classes.Add(new EnumerationGraphType(item));
                }
                else
                {
                    shell.Classes.Add(new GraphType(item));
                }
            }

            foreach (var item in _inputGraphTypesToGenerate)
            {
                shell.Classes.Add(new GraphType(item, true));
            }

            foreach (var item in _dataLoaderTypesToGenerate)
            {
                shell.Classes.Add(new DataLoaderExtensions(item));
            }

            return shell.ToString();
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
                return e.Types.Where(t => t != null);
            }
        }
    }
}

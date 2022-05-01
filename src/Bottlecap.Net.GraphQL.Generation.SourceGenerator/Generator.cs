using Bottlecap.Net.GraphQL.Generation.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Bottlecap.Net.GraphQL.Generation.SourceGenerator
{
    [Generator]
    public class Generator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var types = context.Compilation.SourceModule.ReferencedAssemblySymbols.SelectMany(a =>
            {
                    try
                    {
                        var main = a.Identity.Name.Split('.').Aggregate(a.GlobalNamespace, (s, c) => s.GetNamespaceMembers().Single(m => m.Name.Equals(c)));

                        return this.GetAllTypes(main);
                    }
                    catch
                    {
                        return Enumerable.Empty<ITypeSymbol>();
                    }
            });

            // var generator = new Bottlecap.Net.GraphQL.Generation.Generator();

            var typeDefs = new List<TypeDefinition>();

            foreach (var type in types)
            {
                if (type.TypeKind == TypeKind.Class || type.TypeKind == TypeKind.Interface)
                {
                    var graphqlType = type.GetAttributes().FirstOrDefault(x => String.Equals(x.AttributeClass.Name, nameof(GraphTypeAttribute), StringComparison.OrdinalIgnoreCase));
                    if (graphqlType != null)
                    {
                        typeDefs.Add(new TypeDefinition(type.GetType(), new GraphTypeAttribute()));
                        //generator.RegisterGraphTypes(new TypeDefinition(type.GetType(), new GraphTypeAttribute()));
                    }
                }
            }

            //var content = generator.Generate(context.Compilation.GlobalNamespace.Name);

            //context.AddSource("GraphTypes.cs", SourceText.From(content, Encoding.UTF8));
        }

        private IEnumerable<ITypeSymbol> GetAllTypes(INamespaceSymbol root)
        {
            foreach (var namespaceOrTypeSymbol in root.GetMembers())
            {
                if (namespaceOrTypeSymbol is INamespaceSymbol @namespace) foreach (var nested in GetAllTypes(@namespace)) yield return nested;

                else if (namespaceOrTypeSymbol is ITypeSymbol type) yield return type;
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }
#endif 
        }
    }
}

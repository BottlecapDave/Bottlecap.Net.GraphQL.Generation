using Bottlecap.Net.GraphQL.Generator.Templates;
using System;
using System.IO;

namespace Bottlecap.Net.GraphQL.Generator
{
    public class Generator
    {
        public void Generate(string outputPath, string targetNamespace, params Type[] typesToGenerate)
        {
            var content = Generate(targetNamespace, typesToGenerate);

            File.WriteAllText(outputPath, content);
        }

        public string Generate(string targetNamespace, params Type[] typesToGenerate)
        {
            var shell = new Shell()
            {
                Namespace = targetNamespace
            };

            foreach (var item in typesToGenerate)
            {
                shell.Classes.Add(new OutputType(item));
            }

            return shell.ToString();
        }
    }
}

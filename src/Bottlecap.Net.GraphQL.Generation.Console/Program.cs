using CommandLine;
using System.IO;
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generation.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o => Generate(o))
                   .WithNotParsed<Options>(errors => {
                       System.Console.WriteLine("Error");
                    });
        }

        private static void Generate(Options options)
        {
            var generator = new Generator();

            foreach (var input in options.Inputs)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), input);
                System.Console.WriteLine($"Loading {path}...");
                var assembly = Assembly.LoadFile(path);
                generator.RegisterTypes(assembly);
            }

            generator.Generate(options.Output, options.Namespace);
        }
    }
}

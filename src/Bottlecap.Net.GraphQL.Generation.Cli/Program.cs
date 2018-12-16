using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Bottlecap.Net.GraphQL.Generation.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CheckLoaded;

            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o => Generate(o))
                   .WithNotParsed<Options>(errors => {
                       System.Console.WriteLine("Error");
                    });
        }

        private static void Generate(Options options)
        {
            var generator = new Generator(options.IsVerbose ? new Logger() : null);

            // Load all of our assemblies into the system first, in case some of the assemblies have some
            // dependencies on each other.
            var assemblies = new List<Assembly>();
            foreach (var input in options.Inputs)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), input);
                assemblies.Add(Assembly.LoadFile(path));
            }

            // Register all our types from our loaded assemblies
            foreach (var assembly in assemblies)
            {
                System.Console.WriteLine($"Loading {assembly.GetName().Name}...");
                generator.RegisterTypes(assembly);
            }

            generator.Generate(options.Output, options.Namespace);
        }
        
        private static Assembly CheckLoaded(object sender, ResolveEventArgs args)
        {
            foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (a.FullName.Equals(args.Name))
                {
                    return a;
                }
            }
            return null;
        }
    }
}

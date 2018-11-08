using Bottlecap.Net.GraphQL.Generator.ConsoleExample.Classes;
using System;

namespace Bottlecap.Net.GraphQL.Generator.ConsoleExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new Generator();

            generator.RegisterGraphTypes(new TypeDefinition(typeof(User)) { PropertiesToIgnore = new string[] { nameof(User.Password) }  },
                                         new TypeDefinition(typeof(ToDoList)));

            generator.RegisterInputGraphTypes(new TypeDefinition(typeof(User)));

            generator.Generate("./Output/Types.generated.cs",
                               "TestNamespace");
        }
    }
}

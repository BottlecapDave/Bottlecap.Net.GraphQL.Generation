using Bottlecap.Net.GraphQL.Generator.ConsoleExample.Classes;
using System;

namespace Bottlecap.Net.GraphQL.Generator.ConsoleExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new Generator();

            generator.Generate("./Output/Types.generated.cs",
                               "TestNamespace", 
                               typeof(User),
                               typeof(ToDoList));
        }
    }
}

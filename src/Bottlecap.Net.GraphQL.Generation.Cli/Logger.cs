using System;

namespace Bottlecap.Net.GraphQL.Generation.Cli
{
    public class Logger : ILogger
    {
        public void WriteError(string message)
        {
            Console.Error.WriteLine(message);
        }

        public void WriteInfo(string message)
        {
            Console.WriteLine(message);
        }
    }
}
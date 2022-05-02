using System;

namespace Bottlecap.Net.GraphQL.Generation.Cli;

public class Logger : ILogger
{
    private readonly bool _isVerbose;

    public Logger(bool isVerbose)
    {
        _isVerbose = isVerbose;
    }

    public void WriteError(string message)
    {
        Console.Error.WriteLine(message);
    }

    public void WriteInfo(string message)
    {
        if (_isVerbose)
        {
            Console.WriteLine(message);
        }
    }
}

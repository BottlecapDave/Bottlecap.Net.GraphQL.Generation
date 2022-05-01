using CommandLine;
using System.Collections.Generic;

namespace Bottlecap.Net.GraphQL.Generation.Cli;

public class Options
{
    [Option('n', "namespace", Required = true, HelpText = "The namespace the generated types classes will be in.")]
    public string Namespace { get; set; }

    [Option('o', "output", Required = true, HelpText = "The output path the generated types classes will be created in")]
    public string Output { get; set; }

    [Option('i', "input", Required = true, HelpText = "The input paths of the dlls that contain the classes to generated GraphQL objects for")]
    public IEnumerable<string> Inputs { get; set; }

    [Option('v', "verbose", Required = false, HelpText = "Determines if generation output should be verbose")]
    public bool IsVerbose { get; set; }

    [Option('t', "templates", Required = false, HelpText = "Optional directory containing overrides for the inbuilt templates")]
    public string TemplateDirectory { get; set; }
}

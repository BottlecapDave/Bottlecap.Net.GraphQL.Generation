using HandlebarsDotNet;
using System;
using System.IO;

namespace Bottlecap.Net.GraphQL.Generation
{
    public abstract class BaseTemplate
    {
        public BaseTemplate()
        {
        }

        public override string ToString()
        {
            var type = this.GetType();

            using (var stream = type.Assembly.GetManifestResourceStream($"{type.FullName}.txt"))
            {
                if (stream == null)
                {
                    throw new InvalidDataException($"Failed to find template for {type.FullName}");
                }

                using (var reader = new StreamReader(stream))
                {
                    var handlerBars = Handlebars.Create();

                    // We need to register a helper to help indent multilined content to ensure everything is indented
                    // correctly
                    handlerBars.RegisterHelper("indent", new HandlebarsHelper((writer, data, parameters) =>
                    {
                        var obj = (object)data;
                        writer.Write(obj.ToString().Replace(Environment.NewLine, $"{Environment.NewLine}{parameters[1]}"));
                    }));

                    var template = handlerBars.Compile(reader.ReadToEnd());
                    return template(this);
                }
            }
        }
    }
}

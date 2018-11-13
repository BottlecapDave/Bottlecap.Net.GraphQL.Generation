using HandlebarsDotNet;
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
                    var template = Handlebars.Compile(reader.ReadToEnd());

                    return template(this);

                    //var template = Template.Parse(reader.ReadToEnd());
                    //return template.Render(Hash.FromAnonymousObject(this));
                }
            }
        }
    }
}

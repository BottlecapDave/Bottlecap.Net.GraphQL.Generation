using Bottlecap.Net.GraphQL.Generation;
using Bottlecap.Net.GraphQL.Generation.Attributes;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Bottlecap.Net.GraphQL.Generation
{
    public class GraphTypeTests : BaseTests
    {
        [Fact]
        public void Generate_When_NameOverrideSpecified_Then_GenerateClassHasNameOverride()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterGraphTypes(new TypeDefinition(typeof(GenerateWhenNameOverrideSpecifiedThenGenerateClassHasNameOverride)));

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_NameOverrideSpecified_Then_GenerateClassHasNameOverride));
        }

        [GraphType]
        public class GenerateWhenNameOverrideSpecifiedThenGenerateClassHasNameOverride
        {
            [GraphTypeProperty(Name="NewName")]
            public string Name { get; set; }
        }
    }
}

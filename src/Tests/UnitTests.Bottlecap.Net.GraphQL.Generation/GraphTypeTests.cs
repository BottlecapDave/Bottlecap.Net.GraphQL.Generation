using Bottlecap.Net.GraphQL.Generation;
using Bottlecap.Net.GraphQL.Generation.Attributes;
using Xunit;

namespace UnitTests.Bottlecap.Net.GraphQL.Generation
{
    public class GraphTypeTests : BaseTests
    {
        [Fact]
        public void Generate_When_GraphTypeIsInput_Then_InputGraphTypeIsGenerated()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterGraphTypes(new TypeDefinition(typeof(GenerateWhenGraphTypeIsInputThenInputGraphTypeIsGenerated))
            {
                IsInput = true
            });

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_GraphTypeIsInput_Then_InputGraphTypeIsGenerated));
        }

        [Fact]
        public void Generate_When_InputGraphTypeReferencesInputGraphType_Then_InputGraphTypeIsGenerated()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterGraphTypes(new TypeDefinition(typeof(GenerateWhenInputGraphTypeReferencesInputGraphTypeThenInputGraphTypeIsGenerated))
            {
                IsInput = true
            });

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_InputGraphTypeReferencesInputGraphType_Then_InputGraphTypeIsGenerated));
        }

        [GraphType(IsInput=true)]
        public class GenerateWhenGraphTypeIsInputThenInputGraphTypeIsGenerated
        {

        }

        [GraphType(IsInput = true)]
        public class GenerateWhenInputGraphTypeReferencesInputGraphTypeThenInputGraphTypeIsGenerated
        {
            public GenerateWhenGraphTypeIsInputThenInputGraphTypeIsGenerated Test { get; set; }
        }
    }
}

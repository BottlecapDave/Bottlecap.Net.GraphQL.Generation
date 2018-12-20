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

        [Fact]
        public void Generate_When_GraphTypeIsEnum_Then_EnumerationGraphTypeIsGenerated()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterGraphTypes(new TypeDefinition(typeof(GenerateWhenGraphTypeIsEnumThenEnumerationGraphTypeIsGenerated)));

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_GraphTypeIsEnum_Then_EnumerationGraphTypeIsGenerated));
        }

        [Fact]
        public void Generate_When_GraphTypeReferencesTypeWithNameOverride_Then_GraphTypeIsGeneratedCorrectly()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterGraphTypes(new TypeDefinition(typeof(GenerateWhenGraphTypeReferencesTypeWithNameOverrideThenGraphTypeIsGeneratedCorrectly))
            {
                Name = "Test"
            });
            
            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_GraphTypeReferencesTypeWithNameOverride_Then_GraphTypeIsGeneratedCorrectly));
        }

        [Fact]
        public void Generate_When_GraphTypeNameIsDefined_Then_GraphTypeIsGeneratedWithSpecifiedName()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterGraphTypes(new TypeDefinition(typeof(GenerateWhenGraphTypeIsInputThenInputGraphTypeIsGenerated))
            {
                Name = "Test"
            });

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_GraphTypeNameIsDefined_Then_GraphTypeIsGeneratedWithSpecifiedName));
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

        [GraphType]
        public enum GenerateWhenGraphTypeIsEnumThenEnumerationGraphTypeIsGenerated
        {
            Test
        }

        [GraphType]
        public class GenerateWhenGraphTypeReferencesTypeWithNameOverrideThenGraphTypeIsGeneratedCorrectly
        {
            public GraphTypeWithNameOverride Test { get; set; }
        }

        [GraphType(Name = "NewName")]
        public class GraphTypeWithNameOverride
        {
        }
    }
}

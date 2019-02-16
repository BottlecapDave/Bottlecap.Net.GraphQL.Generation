using Bottlecap.Net.GraphQL.Generation;
using System.IO;
using Xunit;

namespace IntegrationTests.Bottlecap.Net.GraphQL.Generation
{
    public class OverriddenTemplatesTests : BaseTests
    {
        [Fact]
        public void Generate_When_OverriddenTemplatesProvided_Then_OverriddenTemplateUsed()
        {
            // Arrange
            var templateRootPath = Path.Combine(Directory.GetParent(typeof(OverriddenTemplatesTests).Assembly.Location).FullName, "CustomTemplates");

            var generator = new Generator(templateDirectoryPath: templateRootPath);
            generator.RegisterGraphTypes(new TypeDefinition(typeof(GenerateWhenOverriddenTemplatesProvidedThenOverriddenTemplateUsed))
            {
                IsInput = true
            });

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_OverriddenTemplatesProvided_Then_OverriddenTemplateUsed));
        }
        
        public class GenerateWhenOverriddenTemplatesProvidedThenOverriddenTemplateUsed
        {

        }
    }
}

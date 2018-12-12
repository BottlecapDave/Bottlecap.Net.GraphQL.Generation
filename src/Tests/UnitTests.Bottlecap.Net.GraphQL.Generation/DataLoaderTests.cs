using Bottlecap.Net.GraphQL.Generation;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Bottlecap.Net.GraphQL.Generation
{
    public class DataLoaderTests
    {
        [Fact]
        public void Generate_When_DataLoaderSpecified_NoValidDataLoadersPresent_Then_DataloaderNotGenerated()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterDataLoaders(new TypeDefinition(typeof(IGenerateWhenDataLoaderSpecifiedNoValidDataLoadersPresentThenDataloaderNotGenerated)));

            // Act
            var result = generator.Generate("Tests");

            // Assert
        }

        private interface IGenerateWhenDataLoaderSpecifiedNoValidDataLoadersPresentThenDataloaderNotGenerated
        {
            Task<IDictionary<long, string>> GetByIdsAsync(IEnumerable<long> ids);
        }
    }
}

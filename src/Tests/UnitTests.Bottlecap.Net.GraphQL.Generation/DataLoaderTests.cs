using Bottlecap.Net.GraphQL.Generation;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Bottlecap.Net.GraphQL.Generation
{
    public class DataLoaderTests : BaseTests
    {
        [Fact]
        public void Generate_When_DataLoaderSpecified_NoValidDataLoadersPresent_Then_DataloaderNotGenerated()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterDataLoaders(new TypeDefinition(typeof(INoDataLoaderNoDictionary)));
            generator.RegisterDataLoaders(new TypeDefinition(typeof(INoDataLoaderNoTask)));

            // Act
            var result = generator.Generate("Tests");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Generate_When_DataLoaderSpecified_DataLoadersWithSingleItemPresent_Then_DataloaderGenerated()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterDataLoaders(new TypeDefinition(typeof(IGenerateWhenDataLoaderSpecifiedDataLoadersWithSingleItemPresentThenDataloaderGenerated)));

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_DataLoaderSpecified_DataLoadersWithSingleItemPresent_Then_DataloaderGenerated));
        }

        [Fact]
        public void Generate_When_DataLoaderSpecified_DataLoadersWithCollectionPresent_Then_DataloaderGenerated()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterDataLoaders(new TypeDefinition(typeof(IGenerateWhenDataLoaderSpecifiedDataLoadersWithCollectionPresentThenDataloaderGenerated)));

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_DataLoaderSpecified_DataLoadersWithCollectionPresent_Then_DataloaderGenerated));
        }

        private interface INoDataLoaderNoDictionary
        {
            Task<string> GetByIdAsync(long id);
        }

        private interface INoDataLoaderNoTask
        {
            IDictionary<long, string> GetByIdsAsync(IEnumerable<long> ids);
        }

        private interface IGenerateWhenDataLoaderSpecifiedDataLoadersWithSingleItemPresentThenDataloaderGenerated
        {
            Task<IDictionary<long, string>> GetByIdsAsync(IEnumerable<long> ids);
        }

        private interface IGenerateWhenDataLoaderSpecifiedDataLoadersWithCollectionPresentThenDataloaderGenerated
        {
            Task<IDictionary<long, IEnumerable<string>>> GetByIdsAsync(IEnumerable<long> ids);
        }
    }
}

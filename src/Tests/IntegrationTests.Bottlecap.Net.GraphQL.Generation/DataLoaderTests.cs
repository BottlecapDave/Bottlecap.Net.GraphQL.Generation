using Bottlecap.Net.GraphQL.Generation;
using Bottlecap.Net.GraphQL.Generation.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Bottlecap.Net.GraphQL.Generation
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

        [Fact]
        public void Generate_When_DataLoaderSpecified_GenericDataLoader_Then_DataloaderGenerated()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterDataLoaders(new TypeDefinition(typeof(IGenericDataLoader<>)));

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_DataLoaderSpecified_GenericDataLoader_Then_DataloaderGenerated));
        }

        [Fact]
        public void Generate_When_DataLoaderSpecifiedDataLoadersWithAdditionalParameters_Then_DataloaderGenerated()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterDataLoaders(new TypeDefinition(typeof(IGenerateWhenDataLoaderSpecifiedDataLoadersWithAdditionalParametersThenDataloaderGenerated)));

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_DataLoaderSpecifiedDataLoadersWithAdditionalParameters_Then_DataloaderGenerated));
        }

        public interface INoDataLoaderNoDictionary
        {
            Task<string> GetByIdAsync(long id);
        }

        public interface INoDataLoaderNoTask
        {
            IDictionary<long, string> GetByIds(IEnumerable<long> ids);
        }

        [DataLoaders]
        public interface IGenerateWhenDataLoaderSpecifiedDataLoadersWithSingleItemPresentThenDataloaderGenerated
        {
            Task<IDictionary<long, string>> GetByIdsAsync(IEnumerable<long> ids);
        }

        private interface IGenerateWhenDataLoaderSpecifiedDataLoadersWithCollectionPresentThenDataloaderGenerated
        {
            Task<IDictionary<long, IEnumerable<Foo>>> GetByIdsAsync(IEnumerable<long> ids);

            IDictionary<long, string> GetByIds(IEnumerable<long> ids);

            Task<string> GetByIdAsync(long id);

            Task DoSomethingAsync();
        }

        private interface IGenericDataLoader<TType>
            where TType : class
        {
            Task<IDictionary<long, string>> GetByIdsAsync(IEnumerable<long> ids);
        }

        private class Foo
        {

        }

        [DataLoaders]
        public interface IGenerateWhenDataLoaderSpecifiedDataLoadersWithAdditionalParametersThenDataloaderGenerated
        {
            Task<IDictionary<long, string>> GetByIdsAsync(IEnumerable<long> ids, long additionalRestriction);
        }
    }
}

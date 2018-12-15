using Bottlecap.Net.GraphQL.Generation;
using Bottlecap.Net.GraphQL.Generation.Attributes;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests.Bottlecap.Net.GraphQL.Generation
{
    public class GraphTypePropertyTests : BaseTests
    {
        [Fact]
        public void Generate_When_PropertyIsCollection_Then_PropertyIsDefinedCorrectly()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterGraphTypes(new TypeDefinition(typeof(GenerateWhenPropertyIsCollectionThenPropertyIsDefinedCorrectly)));

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_PropertyIsCollection_Then_PropertyIsDefinedCorrectly));
        }

        [Fact]
        public void Generate_When_PropertyIsClass_Then_PropertyIsDefinedCorrectly()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterGraphTypes(new TypeDefinition(typeof(GenerateWhenPropertyIsClassThenPropertyIsDefinedCorrectly)));

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_PropertyIsClass_Then_PropertyIsDefinedCorrectly));
        }

        [Fact]
        public void Generate_When_PropertyIsGeneric_Then_PropertyIsDefinedCorrectly()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterGraphTypes(new TypeDefinition(typeof(GenerateWhenPropertyIsGenericThenPropertyIsDefinedCorrectly)));

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_PropertyIsGeneric_Then_PropertyIsDefinedCorrectly));
        }

        #region Classes

        public class GenerateWhenPropertyIsCollectionThenPropertyIsDefinedCorrectly
        {
            public IEnumerable<long> Ids { get; set; }
        }

        public class GenerateWhenPropertyIsClassThenPropertyIsDefinedCorrectly
        {
            public GenerateWhenPropertyIsCollectionThenPropertyIsDefinedCorrectly ClassProperty { get; set; }
        }

        public class GenerateWhenPropertyIsGenericThenPropertyIsDefinedCorrectly
        {
            public Tuple<string, string> Generic { get; set; }
        }

        #endregion
    }
}

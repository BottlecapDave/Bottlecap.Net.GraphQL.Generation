using Bottlecap.Net.GraphQL.Generation;
using Bottlecap.Net.GraphQL.Generation.Attributes;
using System;
using System.Collections.Generic;
using IntegrationTests.Bottlecap.Net.GraphQL.Generation.Support;
using Xunit;

namespace IntegrationTests.Bottlecap.Net.GraphQL.Generation
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

        [Fact]
        public void Generate_When_PropertiesAreInherited_Then_PropertyIsDefinedCorrectly()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterGraphTypes(new TypeDefinition(typeof(GenerateWhenPropertiesAreInheritedThenPropertyIsDefinedCorrectly)));

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_PropertiesAreInherited_Then_PropertyIsDefinedCorrectly));
        }

        [Fact]
        public void Generate_When_PropertyDescriptionNotSpecified_PropertyDescriptionGenerationIsTrue_Then_PropertyDescriptionIsGenerated()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterGraphTypes(new TypeDefinition(typeof(GenerateWhenPropertyDescriptionNotSpecifiedPropertyDescriptionGenerationIsTrueThenPropertyDescriptionIsGenerated))
            {
                IsDescriptionGenerated = true
            });

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_PropertyDescriptionNotSpecified_PropertyDescriptionGenerationIsTrue_Then_PropertyDescriptionIsGenerated));
        }

        #region Classes

        [GraphType]
        public class GenerateWhenPropertyIsCollectionThenPropertyIsDefinedCorrectly
        {
            public IEnumerable<long> Ids { get; set; }
        }

        [GraphType]
        public class GenerateWhenPropertyIsClassThenPropertyIsDefinedCorrectly
        {
            public GenerateWhenPropertyIsCollectionThenPropertyIsDefinedCorrectly ClassProperty { get; set; }
        }

        [GraphType]
        public class GenerateWhenPropertyIsGenericThenPropertyIsDefinedCorrectly
        {
            public Tuple<string, string> Generic { get; set; }
        }

        [GraphType]
        public class GenerateWhenPropertiesAreInheritedThenPropertyIsDefinedCorrectly : BaseClass
        {
            public long Id { get; set; }
        }

        [GraphType]
        public class GenerateWhenPropertyDescriptionNotSpecifiedPropertyDescriptionGenerationIsTrueThenPropertyDescriptionIsGenerated
        {
            public string Test { get; set; }
        }

        #endregion
    }
}

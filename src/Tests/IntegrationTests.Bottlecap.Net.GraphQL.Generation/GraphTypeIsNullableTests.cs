using Bottlecap.Net.GraphQL.Generation;
using Bottlecap.Net.GraphQL.Generation.Attributes;
using System.Collections.Generic;
using Xunit;

namespace IntegrationTests.Bottlecap.Net.GraphQL.Generation
{
    public class GraphTypeIsNullableTests : BaseTests
    {
        [Fact]
        public void Generate_When_IsNullableSpecified_IsNullable_Then_GenerateClassHasNullableProperty()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterGraphTypes(new TypeDefinition(typeof(GenerateWhenIsNullableSpecifiedIsNullableThenGenerateClassHasNullableProperty)));

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_IsNullableSpecified_IsNullable_Then_GenerateClassHasNullableProperty));
        }

        [Fact]
        public void Generate_When_IsNullableSpecified_IsNotNullable_Then_GenerateClassHasNonNullableProperty()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterGraphTypes(new TypeDefinition(typeof(GenerateWhenIsNullableSpecifiedIsNotNullableThenGenerateClassHasNonNullableProperty)));

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_IsNullableSpecified_IsNotNullable_Then_GenerateClassHasNonNullableProperty));
        }

        [Fact]
        public void Generate_When_IsNullableNotSpecified_PropertyIsNullable_Then_GenerateClassHasNullableProperty()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterGraphTypes(new TypeDefinition(typeof(GenerateWhenIsNullableNotSpecifiedPropertyIsNullableThenGenerateClassHasNullableProperty)));

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_IsNullableNotSpecified_PropertyIsNullable_Then_GenerateClassHasNullableProperty));
        }

        #region Classes

        [GraphType]
        public class GenerateWhenIsNullableSpecifiedIsNullableThenGenerateClassHasNullableProperty
        {
            [GraphTypeProperty(IsNullable = NullableBoolean.True)]
            public string Name { get; set; }
        }

        [GraphType]
        public class GenerateWhenIsNullableSpecifiedIsNotNullableThenGenerateClassHasNonNullableProperty
        {
            [GraphTypeProperty(IsNullable = NullableBoolean.False)]
            public string Name { get; set; }
        }

        [GraphType]
        public class GenerateWhenIsNullableNotSpecifiedPropertyIsNullableThenGenerateClassHasNullableProperty
        {
            public int? NullableInt { get; set; }
        }

        #endregion
    }
}

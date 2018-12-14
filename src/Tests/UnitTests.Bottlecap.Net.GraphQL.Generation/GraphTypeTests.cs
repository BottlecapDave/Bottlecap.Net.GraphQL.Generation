using Bottlecap.Net.GraphQL.Generation;
using Bottlecap.Net.GraphQL.Generation.Attributes;
using System;
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

        #region Classes

        [GraphType]
        public class GenerateWhenNameOverrideSpecifiedThenGenerateClassHasNameOverride
        {
            [GraphTypeProperty(Name="NewName")]
            public string Name { get; set; }
        }

        public class GenerateWhenIsNullableSpecifiedIsNullableThenGenerateClassHasNullableProperty
        {
            [GraphTypeProperty(IsNullable=true)]
            public string Name { get; set; }
        }

        public class GenerateWhenIsNullableSpecifiedIsNotNullableThenGenerateClassHasNonNullableProperty
        {
            [GraphTypeProperty(IsNullable = false)]
            public string Name { get; set; }
        }

        #endregion
    }
}

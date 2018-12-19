using Bottlecap.Net.GraphQL.Generation;
using Bottlecap.Net.GraphQL.Generation.Attributes;
using System.Collections.Generic;
using Xunit;

namespace UnitTests.Bottlecap.Net.GraphQL.Generation
{
    public class GraphTypeNameOverrideTests : BaseTests
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
        public void Generate_When_NameOverrideSpecified_PropertyComplexType_Then_GenerateClassHasNameOverride()
        {
            // Arrange
            var generator = new Generator();
            generator.RegisterGraphTypes(new TypeDefinition(typeof(GenerateWhenNameOverrideSpecifiedPropertyComplexTypeThenGenerateClassHasNameOverride)));

            // Assert
            ActAndAssertGeneratedResult(generator, nameof(Generate_When_NameOverrideSpecified_PropertyComplexType_Then_GenerateClassHasNameOverride));
        }

        #region Classes

        [GraphType]
        public class GenerateWhenNameOverrideSpecifiedThenGenerateClassHasNameOverride
        {
            [GraphTypeProperty(Name="NewName")]
            public string Name { get; set; }
        }

        [GraphType]
        public class GenerateWhenNameOverrideSpecifiedPropertyComplexTypeThenGenerateClassHasNameOverride
        {
            [GraphTypeProperty(Name = "NewName")]
            public Assert Name { get; set; }
        }

        #endregion
    }
}

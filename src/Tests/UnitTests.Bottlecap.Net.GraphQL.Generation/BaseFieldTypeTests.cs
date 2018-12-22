using Bottlecap.Net.GraphQL.Generation;
using Bottlecap.Net.GraphQL.Generation.Templates;
using Xunit;
using System.Linq;
using Bottlecap.Net.GraphQL.Generation.Attributes;
using System.ComponentModel;

namespace UnitTests.Bottlecap.Net.GraphQL.Generation
{
    public class BaseFieldTypeTests
    {
        private const string BASE_EXPECTED_TEMPLATE = "Field(x => x.<<PropertyName>>, nullable: <<IsNullableString>>).Name(\"<<Name>>\").Description(\"<<Description>>\");";

        [Fact]
        public void Generate_When_PropertyWithNoOverrides_Then_GenerationSuccessful()
        {
            // Arrange
            var typeDef = new TypeDefinition(typeof(TestClass));
            var propertyName = nameof(TestClass.TestProperty);
            var propertyDef = typeDef.PropertyDefinitions.First(x => string.Equals(x.Property.Name, propertyName, System.StringComparison.OrdinalIgnoreCase));

            var expectedTemplate = BASE_EXPECTED_TEMPLATE.Replace("<<PropertyName>>", propertyName)
                                                         .Replace("<<IsNullableString>>", "false")
                                                         .Replace("<<Name>>", propertyName)
                                                         .Replace("<<Description>>", "");
            
            // Act & Assert
            ExecuteTest(propertyDef, expectedTemplate);
        }

        [Fact]
        public void Generate_When_NullablePropertyWithNoOverrides_Then_GenerationSuccessful()
        {
            // Arrange
            var typeDef = new TypeDefinition(typeof(TestClass));
            var propertyName = nameof(TestClass.TestNullableProperty);
            var propertyDef = typeDef.PropertyDefinitions.First(x => string.Equals(x.Property.Name, propertyName, System.StringComparison.OrdinalIgnoreCase));

            var expectedTemplate = BASE_EXPECTED_TEMPLATE.Replace("<<PropertyName>>", propertyName)
                                                         .Replace("<<IsNullableString>>", "true")
                                                         .Replace("<<Name>>", propertyName)
                                                         .Replace("<<Description>>", "");

            // Act & Assert
            ExecuteTest(propertyDef, expectedTemplate);
        }

        [Fact]
        public void Generate_When_PropertyWithNameOverride_Then_GenerationSuccessful()
        {
            // Arrange
            var typeDef = new TypeDefinition(typeof(TestClass));
            var propertyName = nameof(TestClass.TestProperty);
            var propertyDef = typeDef.PropertyDefinitions.First(x => string.Equals(x.Property.Name, propertyName, System.StringComparison.OrdinalIgnoreCase));
            propertyDef.Overrides.Name = "Test";

            var expectedTemplate = BASE_EXPECTED_TEMPLATE.Replace("<<PropertyName>>", propertyName)
                                                         .Replace("<<IsNullableString>>", "false")
                                                         .Replace("<<Name>>", propertyDef.Overrides.Name)
                                                         .Replace("<<Description>>", "");

            // Act & Assert
            ExecuteTest(propertyDef, expectedTemplate);
        }

        [Fact]
        public void Generate_When_PropertyWithNullableOverride_Then_GenerationSuccessful()
        {
            // Arrange
            var typeDef = new TypeDefinition(typeof(TestClass));
            var propertyName = nameof(TestClass.TestProperty);
            var propertyDef = typeDef.PropertyDefinitions.First(x => string.Equals(x.Property.Name, propertyName, System.StringComparison.OrdinalIgnoreCase));
            propertyDef.Overrides.IsNullable = NullableBoolean.True;

            var expectedTemplate = BASE_EXPECTED_TEMPLATE.Replace("<<PropertyName>>", propertyName)
                                                         .Replace("<<IsNullableString>>", "true")
                                                         .Replace("<<Name>>", propertyName)
                                                         .Replace("<<Description>>", "");

            // Act & Assert
            ExecuteTest(propertyDef, expectedTemplate);
        }

        [Fact]
        public void Generate_When_DescriptionIsDefined_DescriptionAttribute_Then_GenerationSuccessful()
        {
            // Arrange
            var typeDef = new TypeDefinition(typeof(TestClass));
            var propertyName = nameof(TestClass.PropertyWithDescription);
            var propertyDef = typeDef.PropertyDefinitions.First(x => string.Equals(x.Property.Name, propertyName, System.StringComparison.OrdinalIgnoreCase));
            typeDef.IsDescriptionGenerated = true;

            var expectedTemplate = BASE_EXPECTED_TEMPLATE.Replace("<<PropertyName>>", propertyName)
                                                         .Replace("<<IsNullableString>>", "false")
                                                         .Replace("<<Name>>", propertyName)
                                                         .Replace("<<Description>>", "This property does something");

            // Act & Assert
            ExecuteTest(propertyDef, expectedTemplate);
        }

        [Fact]
        public void Generate_When_DescriptionIsDefined_GraphTypePropertyAttribute_Then_GenerationSuccessful()
        {
            // Arrange
            var typeDef = new TypeDefinition(typeof(TestClass));
            var propertyName = nameof(TestClass.TestProperty);
            var propertyDef = typeDef.PropertyDefinitions.First(x => string.Equals(x.Property.Name, propertyName, System.StringComparison.OrdinalIgnoreCase));
            propertyDef.Overrides.Description = "Description override";

            var expectedTemplate = BASE_EXPECTED_TEMPLATE.Replace("<<PropertyName>>", propertyName)
                                                         .Replace("<<IsNullableString>>", "false")
                                                         .Replace("<<Name>>", propertyName)
                                                         .Replace("<<Description>>", propertyDef.Overrides.Description);

            // Act & Assert
            ExecuteTest(propertyDef, expectedTemplate);
        }

        [Fact]
        public void Generate_When_DescriptionIsGenerated_Then_GenerationSuccessful()
        {
            // Arrange
            var typeDef = new TypeDefinition(typeof(TestClass));
            var propertyName = nameof(TestClass.TestProperty);
            var propertyDef = typeDef.PropertyDefinitions.First(x => string.Equals(x.Property.Name, propertyName, System.StringComparison.OrdinalIgnoreCase));
            typeDef.IsDescriptionGenerated = true;

            var expectedTemplate = BASE_EXPECTED_TEMPLATE.Replace("<<PropertyName>>", propertyName)
                                                         .Replace("<<IsNullableString>>", "false")
                                                         .Replace("<<Name>>", propertyName)
                                                         .Replace("<<Description>>", "The test property of the test class");

            // Act & Assert
            ExecuteTest(propertyDef, expectedTemplate);
        }

        private void ExecuteTest(PropertyDefinition definition,
                                 string expectedTemplate)
        {
            // Arrange
            var template = new BaseFieldType(definition);

            // Act
            var actualTemplate = template.ToString();

            // Assert
            Assert.NotNull(actualTemplate);
            Assert.Equal(expectedTemplate, actualTemplate);
        }

        private class TestClass
        {
            public string TestProperty { get; set; }
            
            public int? TestNullableProperty{ get; set; }

            [Description("This property does something")]
            public string PropertyWithDescription { get; set; }
        }
    }
}

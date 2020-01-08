using Bottlecap.Net.GraphQL.Generation;
using Bottlecap.Net.GraphQL.Generation.Templates;
using Xunit;
using System.Linq;
using Bottlecap.Net.GraphQL.Generation.Attributes;
using System.ComponentModel;
using System.Collections.Generic;

namespace UnitTests.Bottlecap.Net.GraphQL.Generation
{
    public class ExplicitFieldTypeTests
    {
        private const string BASE_EXPECTED_TEMPLATE = "Field<[[GraphTypeName]], [[TypeName]]>().Name(\"[[Name]]\").Description(\"[[Description]]\").Resolve(context => context.Source.[[PropertyName]]);";

        [Fact]
        public void Generate_When_EnumPropertyWithNoOverrides_Then_GenerationSuccessful()
        {
            // Arrange
            var typeDef = new TypeDefinition(typeof(TestClass));
            var propertyName = nameof(TestClass.EnumProperty);
            var propertyDef = typeDef.PropertyDefinitions.First(x => string.Equals(x.Property.Name, propertyName, System.StringComparison.OrdinalIgnoreCase));

            var expectedTemplate = BASE_EXPECTED_TEMPLATE.Replace("[[PropertyName]]", propertyName)
                                                         .Replace("[[Name]]", propertyName)
                                                         .Replace("[[Description]]", "")
                                                         .Replace("[[TypeName]]", "UnitTests.Bottlecap.Net.GraphQL.Generation.TestEnum")
                                                         .Replace("[[GraphTypeName]]", "TestEnumGraphType");

            // Act & Assert
            ExecuteTest(propertyDef, expectedTemplate);
        }

        [Fact]
        public void Generate_When_EnumerablePropertyWithNoOverrides_Then_GenerationSuccessful()
        {
            // Arrange
            var typeDef = new TypeDefinition(typeof(TestClass));
            var propertyName = nameof(TestClass.EnumerableProperty);
            var propertyDef = typeDef.PropertyDefinitions.First(x => string.Equals(x.Property.Name, propertyName, System.StringComparison.OrdinalIgnoreCase));

            var expectedTemplate = BASE_EXPECTED_TEMPLATE.Replace("[[PropertyName]]", propertyName)
                                                         .Replace("[[Name]]", propertyName)
                                                         .Replace("[[Description]]", "")
                                                         .Replace("[[TypeName]]", "System.Collections.Generic.IEnumerable<System.String>")
                                                         .Replace("[[GraphTypeName]]", "ListGraphType<NonNullGraphType<StringGraphType>>");

            // Act & Assert
            ExecuteTest(propertyDef, expectedTemplate);
        }

        [Fact]
        public void Generate_When_ClassPropertyWithNoOverrides_Then_GenerationSuccessful()
        {
            // Arrange
            var typeDef = new TypeDefinition(typeof(TestClass));
            var propertyName = nameof(TestClass.ReferencedClassProperty);
            var propertyDef = typeDef.PropertyDefinitions.First(x => string.Equals(x.Property.Name, propertyName, System.StringComparison.OrdinalIgnoreCase));

            var expectedTemplate = BASE_EXPECTED_TEMPLATE.Replace("[[PropertyName]]", propertyName)
                                                         .Replace("[[Name]]", propertyName)
                                                         .Replace("[[Description]]", "")
                                                         .Replace("[[TypeName]]", "UnitTests.Bottlecap.Net.GraphQL.Generation.TestReferencedClass")
                                                         .Replace("[[GraphTypeName]]", "TestReferencedClassGraphType");

            // Act & Assert
            ExecuteTest(propertyDef, expectedTemplate);
        }

        [Fact]
        public void Generate_When_InputGraphPropertyWithNoOverrides_Then_GenerationSuccessful()
        {
            // Arrange
            var typeDef = new TypeDefinition(typeof(TestClass));
            var propertyName = nameof(TestClass.ReferencedInputClassProperty);
            var propertyDef = typeDef.PropertyDefinitions.First(x => string.Equals(x.Property.Name, propertyName, System.StringComparison.OrdinalIgnoreCase));

            var expectedTemplate = BASE_EXPECTED_TEMPLATE.Replace("[[PropertyName]]", propertyName)
                                                         .Replace("[[Name]]", propertyName)
                                                         .Replace("[[Description]]", "")
                                                         .Replace("[[TypeName]]", "UnitTests.Bottlecap.Net.GraphQL.Generation.TestInputClass")
                                                         .Replace("[[GraphTypeName]]", "TestInputClassInputGraphType");

            // Act & Assert
            ExecuteTest(propertyDef, expectedTemplate);
        }

        [Fact]
        public void Generate_When_PropertyWithNameOverride_Then_GenerationSuccessful()
        {
            // Arrange
            var typeDef = new TypeDefinition(typeof(TestClass));
            var propertyName = nameof(TestClass.EnumProperty);
            var propertyDef = typeDef.PropertyDefinitions.First(x => string.Equals(x.Property.Name, propertyName, System.StringComparison.OrdinalIgnoreCase));
            propertyDef.Overrides.Name = "Test";

            var expectedTemplate = BASE_EXPECTED_TEMPLATE.Replace("[[PropertyName]]", propertyName)
                                                         .Replace("[[Name]]", propertyDef.Overrides.Name)
                                                         .Replace("[[Description]]", "")
                                                         .Replace("[[TypeName]]", "UnitTests.Bottlecap.Net.GraphQL.Generation.TestEnum")
                                                         .Replace("[[GraphTypeName]]", "TestEnumGraphType");

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

            var expectedTemplate = BASE_EXPECTED_TEMPLATE.Replace("[[PropertyName]]", propertyName)
                                                         .Replace("[[Name]]", propertyName)
                                                         .Replace("[[Description]]", "This property does something")
                                                         .Replace("[[TypeName]]", "UnitTests.Bottlecap.Net.GraphQL.Generation.TestEnum")
                                                         .Replace("[[GraphTypeName]]", "TestEnumGraphType");

            // Act & Assert
            ExecuteTest(propertyDef, expectedTemplate);
        }

        [Fact]
        public void Generate_When_DescriptionIsDefined_GraphTypePropertyAttribute_Then_GenerationSuccessful()
        {
            // Arrange
            var typeDef = new TypeDefinition(typeof(TestClass));
            var propertyName = nameof(TestClass.EnumProperty);
            var propertyDef = typeDef.PropertyDefinitions.First(x => string.Equals(x.Property.Name, propertyName, System.StringComparison.OrdinalIgnoreCase));
            propertyDef.Overrides.Description = "Description override";

            var expectedTemplate = BASE_EXPECTED_TEMPLATE.Replace("[[PropertyName]]", propertyName)
                                                         .Replace("[[Name]]", propertyName)
                                                         .Replace("[[Description]]", propertyDef.Overrides.Description)
                                                         .Replace("[[TypeName]]", "UnitTests.Bottlecap.Net.GraphQL.Generation.TestEnum")
                                                         .Replace("[[GraphTypeName]]", "TestEnumGraphType");

            // Act & Assert
            ExecuteTest(propertyDef, expectedTemplate);
        }

        [Fact]
        public void Generate_When_DescriptionIsGenerated_Then_GenerationSuccessful()
        {
            // Arrange
            var typeDef = new TypeDefinition(typeof(TestClass));
            var propertyName = nameof(TestClass.EnumProperty);
            var propertyDef = typeDef.PropertyDefinitions.First(x => string.Equals(x.Property.Name, propertyName, System.StringComparison.OrdinalIgnoreCase));
            typeDef.IsDescriptionGenerated = true;

            var expectedTemplate = BASE_EXPECTED_TEMPLATE.Replace("[[PropertyName]]", propertyName)
                                                         .Replace("[[Name]]", propertyName)
                                                         .Replace("[[Description]]", "The enum property of the test class")
                                                         .Replace("[[TypeName]]", "UnitTests.Bottlecap.Net.GraphQL.Generation.TestEnum")
                                                         .Replace("[[GraphTypeName]]", "TestEnumGraphType");

            // Act & Assert
            ExecuteTest(propertyDef, expectedTemplate);
        }

        private void ExecuteTest(PropertyDefinition definition,
                                 string expectedTemplate)
        {
            // Arrange
            var generator = new Generator();
            var template = new ExplicitFieldType(generator, definition);

            // Act
            var actualTemplate = template.ToString();

            // Assert
            Assert.NotNull(actualTemplate);
            Assert.Equal(expectedTemplate, actualTemplate);
        }

        private class TestClass
        {
            public TestEnum EnumProperty { get; set; }

            public TestReferencedClass ReferencedClassProperty { get; set; }

            public TestInputClass ReferencedInputClassProperty { get; set; }

            public IEnumerable<string> EnumerableProperty { get; set; }

            [Description("This property does something")]
            public TestEnum PropertyWithDescription { get; set; }
        }

        private enum TestEnum
        {
            Test
        }

        private class TestReferencedClass
        {
        }

        [GraphType(IsInput = true)]
        private class TestInputClass
        {
        }
    }
}

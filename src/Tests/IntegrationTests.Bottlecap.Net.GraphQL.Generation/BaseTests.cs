using Bottlecap.Net.GraphQL.Generation;
using System.IO;
using Xunit;

namespace IntegrationTests.Bottlecap.Net.GraphQL.Generation
{
    public class BaseTests
    {
        protected const string NAMESPACE = "Tests";

        protected void ActAndAssertGeneratedResult(Generator generator, string testName)
        {
            // Act
            var result = generator.Generate(NAMESPACE);

            // Assert
            using (var stream = typeof(BaseTests).Assembly.GetManifestResourceStream($"IntegrationTests.Bottlecap.Net.GraphQL.Generation.ExpectedData.{testName}"))
            {
                Assert.NotNull(stream);

                using (var reader = new StreamReader(stream))
                {
                    var expectedData = reader.ReadToEnd();
                    Assert.Equal(expectedData.Trim(), result.Trim());
                }
            }
        }
    }
}
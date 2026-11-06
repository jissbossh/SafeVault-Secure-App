using Xunit;
using SafeVault.API.Services;
using Microsoft.Extensions.Configuration;
using Moq;

namespace SafeVault.Tests
{
    public class SecurityTests
    {
        private readonly SecurityService _securityService;

        public SecurityTests()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"ConnectionString:DefaultConnection", "Server=myServer;Database=myDb;User Id=myUser;Password=myPass;"}
            };
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings!)
                .Build();

            _securityService = new SecurityService(configuration);
        }

        [Theory]
        [InlineData("<script>alert(1)</script>", "alert(1)")]
        [InlineData("<b>Hola</b>", "Hola")]
        public void SanitizeInput_RemovesHtmlTags(string input, string expected)
        {
            var result = _securityService.SanitizeInput(input);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SanitizeInput_NullInput_ReturnsNull()
        {
            string result = _securityService.SanitizeInput(null!);
            Assert.Null(result);
        }
    }
}

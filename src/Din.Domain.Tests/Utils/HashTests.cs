using FluentAssertions;
using Xunit;
using BC = BCrypt.Net.BCrypt;

namespace Din.Domain.Tests.Utils
{
    public class HashTests
    {
        private const string Password = "HarDtoGuessPassword";

        [Fact]
        public void HashTestSimpleString()
        {
            var hashed = BC.HashPassword(Password);

            hashed.Should().NotBeNullOrEmpty();
            hashed.Should().NotBe(Password);
            BC.Verify(Password, hashed).Should().BeTrue();
        }
    }
}

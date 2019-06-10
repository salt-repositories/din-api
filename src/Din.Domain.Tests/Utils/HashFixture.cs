using NUnit.Framework;
using BC = BCrypt.Net.BCrypt;

namespace Din.Domain.Tests.Utils
{
    [TestFixture]
    public class HashFixture
    {
        private const string Password = "HarDtoGuessPassword";

        [Test]
        public void HashTestSimpleString()
        {
            var hashed = BC.HashPassword(Password);

            Assert.NotNull(hashed);
            Assert.IsNotEmpty(hashed);
            Assert.AreNotEqual(Password, hashed);
            Assert.True(BC.Verify(Password, hashed));
        }
    }
}

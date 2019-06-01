using NUnit.Framework;

namespace Din.Domain.Tests.Utils
{
    [TestFixture]
    public class HashFixture
    {
        private const string Password = "HarDtoGuessPassword";

        [Test]
        public void HashTestSimpleString()
        {
            var hashed = BCrypt.Net.BCrypt.HashPassword(Password);

            Assert.NotNull(hashed);
            Assert.IsNotEmpty(hashed);
            Assert.AreNotEqual(Password, hashed);
            Assert.True(BCrypt.Net.BCrypt.Verify(Password, hashed));
        }
    }
}

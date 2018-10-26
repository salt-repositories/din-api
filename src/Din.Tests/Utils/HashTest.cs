using Xunit;

namespace Din.Tests.Utils
{
    
    public class HashTest
    {
        [Fact]
        public void HashTestSimpleString()
        {
            var hashed = BCrypt.Net.BCrypt.HashPassword(TestConsts.Password);

            Assert.NotNull(hashed);
            Assert.NotEmpty(hashed);
            Assert.NotEqual(TestConsts.Password, hashed);
            Assert.True(BCrypt.Net.BCrypt.Verify(TestConsts.Password, hashed));
        }
    }
}

using System.Security.Cryptography;
using System.Text;

namespace Din.Domain.Helpers.Concrete
{
    public static class RandomCodeGenerator
    {
        private const string Default = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        /// <summary>
        /// Generate secure random codes by length
        /// </summary>
        /// <param name="length">The length of the code</param>
        /// <param name="defaultCharSet">Use upper and lower case characters</param>
        /// <returns></returns>
        public static string GenerateRandomCode(int length, bool defaultCharSet = true)
        {
            var charSet = defaultCharSet ? Default.ToCharArray() : Upper.ToCharArray();

            var data = new byte[length];
            
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(data);
            }

            var stringBuilder = new StringBuilder(length);

            foreach (var b in data)
            {
                stringBuilder.Append(charSet[b % (charSet.Length)]);
            }

            return stringBuilder.ToString();
        }
    }
}
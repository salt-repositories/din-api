using Din.Domain.Models.Entities;
using Din.Domain.Tests.Extensions;
using Din.Domain.Validators.Concrete;
using FluentAssertions;
using NUnit.Framework;

namespace Din.Domain.Tests.Validators
{
    [TestFixture]
    public class AccountValidatorFixture
    {
        private AccountValidator _validator;

        private const string ValidUsername = "Username";
        private const string ValidHash = "2$b49sdfj30aldsjf30";

        [SetUp]
        public void SetUp()
        {
            _validator = new AccountValidator();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("this_username_is_longer_then_fifty_characters")]
        public void Create_account_should_fail_When_invalid_username(string username)
        {
            var account = new Account
            {
                Username = username,
                Hash = ValidHash,
                Role = AccountRole.User
            };

            var result = _validator.Validate(account);
            result.ShouldContainErrorForProperty(nameof(Account.Username));
        }

        [TestCase("abc")]
        [TestCase("Username")]
        [TestCase("58379827")]
        [TestCase("Crazy23023!@!#$!Username")]
        public void Create_account_should_succeed_when_valid_username(string username)
        {
            var account = new Account
            {
                Username = username,
                Hash = ValidHash,
                Role = AccountRole.User
            };

            var result = _validator.Validate(account);
            result.IsValid.Should().BeTrue();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("a")]
        [TestCase("abcd")]
        public void Create_account_should_fail_when_invalid_hash(string hash)
        {
            var account = new Account
            {
                Username = ValidUsername,
                Hash = hash,
                Role = AccountRole.User
            };

            var result = _validator.Validate(account);
            result.ShouldContainErrorForProperty(nameof(Account.Hash));
        }

        [TestCase("abcde")]
        [TestCase("232320023ekjaldskm203owjedolk")]
        [TestCase("2$s9jifsdifjIAJDOSID93-=")]
        public void Create_account_should_succeed_when_valid_hash(string hash)
        {
            var account = new Account
            {
                Username = ValidUsername,
                Hash = hash,
                Role = AccountRole.User
            };

            var result = _validator.Validate(account);
            result.IsValid.Should().BeTrue();
        }

        [TestCase(AccountRole.User)]
        [TestCase(AccountRole.Moderator)]
        [TestCase(AccountRole.Admin)]
        public void Create_account_should_succeed_when_valid_role(AccountRole role)
        {
            var account = new Account
            {
                Username = ValidUsername,
                Hash = ValidHash,
                Role = role
            };

            var result = _validator.Validate(account);
            result.IsValid.Should().BeTrue();
        }
    }
}

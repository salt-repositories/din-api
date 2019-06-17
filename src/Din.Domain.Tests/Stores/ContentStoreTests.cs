using System.Collections.Generic;
using System.Linq;
using Din.Domain.Stores.Concrete;
using Din.Domain.Stores.Interfaces;
using FluentAssertions;
using Xunit;

namespace Din.Domain.Tests.Stores
{
    public class ContentStoreTests
    {
        private readonly IContentStore<ContentTestClass> _contentStore;

        public ContentStoreTests()
        {
            _contentStore = new ContentStore<ContentTestClass>();
        }

        [Fact]
        public void Store_get_all_should_succeed_when_store_filled()
        {
            var data = new ContentTestClass {Title = "Title"};

            _contentStore.Set(new List<ContentTestClass> { data });

            var content = _contentStore.GetAll();

            content.Should().NotBeNullOrEmpty();
            content.Count.Should().Be(1);
        }

        [Fact]
        public void Update_store_should_be_true_when_store_is_empty()
        {
            _contentStore.ShouldUpdate().Should().BeTrue();
        }

        [Fact]
        public void Update_store_should_be_false_when_store_is_filled()
        {
            var data = new ContentTestClass {Title = "Title"};

            _contentStore.Set(new List<ContentTestClass> { data });

            _contentStore.ShouldUpdate().Should().BeFalse();
        }
        

        [Fact]
        public void AddOne_should_succeed_when_store_is_filled()
        {
            var first = new ContentTestClass {Title = "First"};
            var second = new ContentTestClass {Title = "First"};

            _contentStore.Set(new List<ContentTestClass>{ first });
            _contentStore.AddOne(second);

            var content = _contentStore.GetAll().ToList();

            content.Should().NotBeNullOrEmpty();
            content.Count.Should().Be(2);
            content[0].Should().NotBe(content[1]);
        }

        [Fact]
        public void AddOne_should_fail_when_store_is_empty()
        {
            var data = new ContentTestClass {Title = "Title"};

            _contentStore.AddOne(data);

            _contentStore.GetAll().Should().BeNull();
        }

        [Fact]
        public void Get_one_by_id_should_succeed_when_using_the_right_id()
        {
            var data = new ContentTestClass {SystemId = 1};

            _contentStore.Set(new List<ContentTestClass>{ data });

            var content = _contentStore.GetOneById(1);

            content.Should().NotBeNull();
            content.Should().Be(data);
        }

        [Fact]
        public void Get_one_by_id_should_fail_when_using_the_wrong_id()
        {
            var data = new ContentTestClass {SystemId = 1};

            _contentStore.Set(new List<ContentTestClass>{ data });

            var content = _contentStore.GetOneById(2);

            content.Should().BeNull();
        }

        [Theory]
        [InlineData("The")]
        [InlineData("Test")]
        [InlineData("Content")]
        public void Get_multiple_by_title_should_succeed_when_title_contains_search_title(string title)
        {
            var data = new ContentTestClass {Title = "The Test Content"};

            _contentStore.Set(new List<ContentTestClass> { data });

            var content = _contentStore.GetMultipleByTitle(title).ToList();

            content.Should().NotBeNullOrEmpty();
            content[0].Should().Be(data);
        }

        [Theory]
        [InlineData("Interstellar")]
        [InlineData("abcd")]
        [InlineData("Wrong")]
        public void Get_multiple_by_title_should_fail_when_title_does_not_contain_search_title(string title)
        {
            var data = new ContentTestClass {Title = "The Test Content"};

            _contentStore.Set(new List<ContentTestClass> { data });

            var content = _contentStore.GetMultipleByTitle(title).ToList();

            content.Should().BeNullOrEmpty();
        }

        [Theory]
        [InlineData("Iatarasaealar")]
        [InlineData("stallar")]
        [InlineData("OuterStellar")]
        public void Get_multiple_by_title_should_succeed_when_search_title_is_40_percent_similar(string title)
        {
            var data = new ContentTestClass {Title = "Interstellar"};

            _contentStore.Set(new List<ContentTestClass> { data });

            var content = _contentStore.GetMultipleByTitle(title).ToList();

            content.Should().NotBeNullOrEmpty();
            content[0].Should().Be(data);
        }

        [Theory]
        [InlineData("Iaar")]
        [InlineData("star")]
        [InlineData("ante")]
        public void Get_multiple_by_title_should_fail_when_search_title_is_less_then_40_percent_similar(string title)
        {
            var data = new ContentTestClass {Title = "Interstellar"};

            _contentStore.Set(new List<ContentTestClass> { data });

            var content = _contentStore.GetMultipleByTitle(title).ToList();

            content.Should().BeNullOrEmpty();
        }
    }
}
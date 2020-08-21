using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data.Context;
using TodoApp.Data.Models;
using TodoApp.Data.Providers;
using Xunit;
using static TodoApp.Data.Tests.TestClasses;

namespace TodoApp.Data.Tests.Providers
{
    public class CategoryProviderTests
    {
        [Fact]
        public async Task Get_Returns_Category()
        {
            var data = new List<CategoryDAO>
            {
                new CategoryDAO { Id = 1, Name = "BBB" },
                new CategoryDAO { Id = 2, Name = "ZZZ" },
                new CategoryDAO { Id = 3, Name = "AAA" },
            }.AsQueryable()
            .BuildMockDbSet();

            var mockContext = new Mock<TodoAppContext>();
            mockContext.Setup(c => c.Categories).Returns(data.Object);

            var provider = new CategoryProvider(mockContext.Object);
            var category = await provider.Get(1);

            Assert.Equal("BBB", category.Name);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data.Context;
using TodoApp.Data.Models;
using TodoApp.Data.Providers;
using Xunit;
using static TodoApp.Data.Tests.TestClasses;
using TodoApp.Data.Interfaces;
using System;

namespace TodoApp.Data.Tests.Providers
{
    public class CategoryProviderTests
    {
        private readonly Mock<DbSet<CategoryDAO>> mockSet;
        private readonly Mock<TodoAppContext> mockContext;
        public CategoryProviderTests()
        {
            mockSet = new List<CategoryDAO>
            {
                new CategoryDAO { Id = 1, Name = "BBB" },
                new CategoryDAO { Id = 2, Name = "ZZZ" },
                new CategoryDAO { Id = 3, Name = "AAA" },
            }.AsQueryable()
            .BuildMockDbSet();

            mockContext = new Mock<TodoAppContext>();
            mockContext.Setup(c => c.Categories).Returns(mockSet.Object);

        }
        [Fact]
        public async Task Get_Returns_Category()
        {
            IAsyncDataProvider<CategoryDAO> provider = new CategoryProvider(mockContext.Object);
            var category = await provider.Get(1);
            Assert.Equal("BBB", category.Name);
        }
        [Fact]
        public async Task GetAll_Returns_CategoryList()
        {
            IAsyncDataProvider<CategoryDAO> provider = new CategoryProvider(mockContext.Object);
            var categories = await provider.GetAll();
            Assert.Equal(3, categories.Count());
        }
        [Fact]
        public async Task Create_Returns_createdIdAndAddsCategory()
        {
            IAsyncDataProvider<CategoryDAO> provider = new CategoryProvider(mockContext.Object);
            var category = new CategoryDAO { Id = 22, Name = "TestCategory" };

            var addedCategoyId = await provider.Create(category);

            mockSet.Verify(set => set.Add(It.IsAny<CategoryDAO>()), Times.Once);
            mockContext.Verify(context => context.SaveChangesAsync(new CancellationToken()), Times.Once);
            Assert.Equal(category.Id, addedCategoyId);
        }
        [Fact]
        public async Task CreateThrowsExeptionWhenNameIsShorterThan2Letters()
        {
            IAsyncDataProvider<CategoryDAO> provider = new CategoryProvider(mockContext.Object);
            var category = new CategoryDAO { Id = 22, Name = "A" };

            await Assert.ThrowsAsync<ArgumentException>(() => provider.Create(category));
        }
        [Fact]
        public async Task Update_Updates_Category()
        {
            IAsyncDataProvider<CategoryDAO> provider = new CategoryProvider(mockContext.Object);
            var category = new CategoryDAO { Id = 1, Name = "TestUpdateCategory" };

            await provider.Update(category);
            var updatedCategory = await provider.Get(category.Id);

            Assert.Equal("TestUpdateCategory", updatedCategory.Name);
        }
        [Fact]
        public async Task Delete_Removes_Category()
        {
            var dbContext = new Mock<TodoAppContext>();
            IAsyncDataProvider<CategoryDAO> provider = new CategoryProvider(dbContext.Object);
            var categories = new List<CategoryDAO>()
            {
                new CategoryDAO() { Id = 1, Name = "TestCategory1" },
                new CategoryDAO() { Id = 2, Name = "TestCategory2" },
                 new CategoryDAO() { Id = 3, Name = "TestCategory3" }
            };
            dbContext
                .Setup(m => m.Categories.Remove(It.IsAny<CategoryDAO>()))
                .Callback<CategoryDAO>((entity) => categories.Remove(entity));
            int idToDelete = 1;
            dbContext
                .Setup(s => s.Categories.Find(idToDelete))
                .Returns(categories.Single(s => s.Id == idToDelete));

            await provider.Delete(idToDelete);

            Assert.Equal(2, categories.Count());
            dbContext.Verify(s => s.Categories.Find(idToDelete), Times.Once);
            dbContext.Verify(s => s.Categories.Remove(It.IsAny<CategoryDAO>()), Times.Once);
            dbContext.Verify(s => s.SaveChangesAsync(new CancellationToken()), Times.Once);
        }

    }
}

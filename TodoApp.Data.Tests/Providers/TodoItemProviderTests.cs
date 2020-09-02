using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Data.Context;
using TodoApp.Data.Interfaces;
using TodoApp.Data.Models;
using TodoApp.Data.Providers;
using Xunit;

namespace TodoApp.Data.Tests.Providers
{
    public class TodoItemProviderTests
    {
        private readonly Mock<DbSet<TodoItemDAO>> mockSet;
        private readonly Mock<TodoAppContext> mockContext;
        public TodoItemProviderTests()
        {
            mockSet = new List<TodoItemDAO>
            {
                new TodoItemDAO { Id = 1, Name = "TestItem1" },
                new TodoItemDAO { Id = 2, Name = "TestItem2" },
                new TodoItemDAO { Id = 3, Name = "TestItem3" },
            }.AsQueryable()
            .BuildMockDbSet();

            mockContext = new Mock<TodoAppContext>();
            mockContext.Setup(c => c.TodoItems).Returns(mockSet.Object);

        }
        [Fact]
        public async Task Get_Returns_TodoItem()
        {
            IAsyncDataProvider<TodoItemDAO> provider = new TodoItemProvider(mockContext.Object);
            int idItemToGet = 1;

            var TodoItem = await provider.Get(idItemToGet);

            Assert.Equal(idItemToGet, TodoItem.Id);
        }
        [Fact]
        public async Task GetAll_Returns_TodoItemList()
        {
            IAsyncDataProvider<TodoItemDAO> provider = new TodoItemProvider(mockContext.Object);
            var categories = await provider.GetAll();
            Assert.Equal(3, categories.Count());
        }
        [Fact]
        public async Task Create_Returns_createdIdAndAddsTodoItem()
        {
            IAsyncDataProvider<TodoItemDAO> provider = new TodoItemProvider(mockContext.Object);
            var item = new TodoItemDAO { Id = 22, Name = "TestTodoItem" };

            var addedItemId = await provider.Create(item);

            mockSet.Verify(set => set.Add(It.IsAny<TodoItemDAO>()), Times.Once);
            mockContext.Verify(context => context.SaveChangesAsync(new CancellationToken()), Times.Once);
            Assert.Equal(item.Id, addedItemId);
        }

        [Fact]
        public async Task Create_ThrowsExeption_DuplicateName()
        {
            IAsyncDataProvider<TodoItemDAO> provider = new TodoItemProvider(mockContext.Object);
            var item = new TodoItemDAO { Id = 22, Name = "TestItem1" };

            await Assert.ThrowsAsync<ArgumentException>(() => provider.Create(item));

        }
        [Fact]
        public async Task Create_ThrowsExeption_InvalidDeadlineDate()
        {
            IAsyncDataProvider<TodoItemDAO> provider = new TodoItemProvider(mockContext.Object);
            var item = new TodoItemDAO
            {
                Id = 22,
                Name = "TestItem1",
                DeadLineDate = DateTime.UtcNow.Subtract(new TimeSpan(1,0,0))
            };

            await Assert.ThrowsAsync<ArgumentException>(() => provider.Create(item));
        }
        [Fact]
        public async Task Create_ThrowsExeption_PriorityOutOf1to5Range()
        {
            IAsyncDataProvider<TodoItemDAO> provider = new TodoItemProvider(mockContext.Object);
            var item = new TodoItemDAO
            {
                Id = 22,
                Priority = 10
            };

            await Assert.ThrowsAsync<ArgumentException>(() => provider.Create(item));
        }

        [Fact]
        public async Task Update_Updates_TodoItem()
        {
            IAsyncDataProvider<TodoItemDAO> provider = new TodoItemProvider(mockContext.Object);
            var item = new TodoItemDAO { Id = 1, Name = "TestUpdateTodoItem" };

            await provider.Update(item);
            var updatedTodoItem = await provider.Get(item.Id);

            Assert.Equal("TestUpdateTodoItem", updatedTodoItem.Name);
        }
        [Fact]
        public async Task Update_ThrowsExeption_DuplicateName()
        {
            IAsyncDataProvider<TodoItemDAO> provider = new TodoItemProvider(mockContext.Object);
            var item = new TodoItemDAO { Id = 22, Name = "TestItem1" };

            await Assert.ThrowsAsync<ArgumentException>(() => provider.Update(item));

        }
        [Fact]
        public async Task Delete_Removes_TodoItem()
        {
            var dbContext = new Mock<TodoAppContext>();
            IAsyncDataProvider<TodoItemDAO> provider = new TodoItemProvider(dbContext.Object);
            var items = new List<TodoItemDAO>()
            {
                new TodoItemDAO() { Id = 1, Name = "TestItem1" },
                new TodoItemDAO() { Id = 2, Name = "TestItem2" },
                 new TodoItemDAO() { Id = 3, Name = "TestItem3" }
            };
            dbContext
                .Setup(m => m.TodoItems.Remove(It.IsAny<TodoItemDAO>()))
                .Callback<TodoItemDAO>((entity) => items.Remove(entity));
            int idToDelete = 1;
            dbContext
                .Setup(s => s.TodoItems.Find(idToDelete))
                .Returns(items.Single(s => s.Id == idToDelete));

            await provider.Delete(idToDelete);

            Assert.Equal(2, items.Count());
            dbContext.Verify(s => s.TodoItems.Find(idToDelete), Times.Once);
            dbContext.Verify(s => s.TodoItems.Remove(It.IsAny<TodoItemDAO>()), Times.Once);
            dbContext.Verify(s => s.SaveChangesAsync(new CancellationToken()), Times.Once);
        }

    }
}

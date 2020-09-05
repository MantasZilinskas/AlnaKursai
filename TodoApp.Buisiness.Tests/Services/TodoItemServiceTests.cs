using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using TodoApp.Buisiness.Mapping;
using TodoApp.Buisiness.Models;
using TodoApp.Buisiness.Services;
using TodoApp.Commons.Enums;
using TodoApp.Data.Interfaces;
using Xunit;

namespace TodoApp.Buisiness.Tests.Services
{
    public class TodoItemServiceTests
    {
        private readonly TodoItemService _todoItemService;
        private readonly Mock<ITodoItemProvider> _todoItemProvider;

        public TodoItemServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TodoItemVOProfile());
            });
            var mockMapper = config.CreateMapper();

            _todoItemProvider = new Mock<ITodoItemProvider>();

            _todoItemService = new TodoItemService(_todoItemProvider.Object, mockMapper);

        }

        [Fact]
        public async Task Create_ThrowsExeption_ItemOfWipStatusWithPriority1AlreadyExists()
        {
            var item = new TodoItemVO { Id = 22, Priority = 1, Status = Status.Wip };

            _todoItemProvider.Setup(provider => provider.WipStatusWithPriority1Exists() ).Returns(Task.FromResult(true));

            await Assert.ThrowsAsync<ArgumentException>(() => _todoItemService.Create(item));
        }
        [Fact]
        public async Task Create_ThrowsExeption_3ItemsOfWipStatusWithPriority2AlreadyExists()
        {
            var item = new TodoItemVO { Id = 22, Priority = 2, Status = Status.Wip };

            _todoItemProvider.Setup(provider => provider.ThreeItemsOfWipStatusWithPriority2Exists()).Returns(Task.FromResult(true));

            await Assert.ThrowsAsync<ArgumentException>(() => _todoItemService.Create(item));
        }
    }
}

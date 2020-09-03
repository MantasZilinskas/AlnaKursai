using AutoMapper;
using Castle.DynamicProxy.Generators;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Buisiness.Mapping;
using TodoApp.Buisiness.Models;
using TodoApp.Buisiness.Services;
using TodoApp.Data.Interfaces;
using TodoApp.Data.Models;
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
            var item = new TodoItemVO { Id = 22, Priority = 1, Status = Models.Enums.Status.Wip };

            _todoItemProvider.Setup(provider => provider.WipStatusWithPriority1Exists() ).Returns(Task.FromResult(true));

            await Assert.ThrowsAsync<ArgumentException>(() => _todoItemService.Create(item));
        }
    }
}

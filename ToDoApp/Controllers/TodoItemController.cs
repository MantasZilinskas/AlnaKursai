using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Interfaces;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class TodoItemController : Controller
    {
        private readonly IInMemoryTodoItemProvider _inMemoryTodoItemProvider;

        public TodoItemController(IInMemoryTodoItemProvider inMemoryTodoItemProvider)
        {
            _inMemoryTodoItemProvider = inMemoryTodoItemProvider;
        }

        // GET: ToDoItemController
        public ActionResult Index()
        {
            return View(_inMemoryTodoItemProvider.GetAll());
        }

        // GET: ToDoItemController/Details/5
        public ActionResult Details(int id)
        {
            return View(_inMemoryTodoItemProvider.Get(id));
        }

        // GET: ToDoItemController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoItemController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TodoItem data)
        {
            try
            {
                _inMemoryTodoItemProvider.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ToDoItemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_inMemoryTodoItemProvider.Get(id));
        }

        // POST: ToDoItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TodoItem data)
        {
            try
            {
                _inMemoryTodoItemProvider.Update(data, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_inMemoryTodoItemProvider.Get(id));
            }
        }

        // GET: ToDoItemController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_inMemoryTodoItemProvider.Get(id));
        }

        // POST: ToDoItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TodoItem data)
        {
            try
            {
                _inMemoryTodoItemProvider.Delete(data,id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_inMemoryTodoItemProvider.Get(id));
            }
        }
    }
}

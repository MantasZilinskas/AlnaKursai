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

        // GET: ToDoItem
        public ActionResult Index()
        {
            return View(_inMemoryTodoItemProvider.GetAll());
        }

        // GET: ToDoItem/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                return View(_inMemoryTodoItemProvider.Get(id));
            }
            catch (KeyNotFoundException ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ToDoItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TodoItem data)
        {
            try
            {
                _inMemoryTodoItemProvider.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                return View();
            }
        }

        // GET: ToDoItem/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_inMemoryTodoItemProvider.Get(id));
        }

        // POST: ToDoItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TodoItem data)
        {
            try
            {
                _inMemoryTodoItemProvider.Update(data, id);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException ex)
            {
                return View(_inMemoryTodoItemProvider.Get(id));
            }
        }

        // GET: ToDoItem/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_inMemoryTodoItemProvider.Get(id));
        }

        // POST: ToDoItem/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TodoItem data)
        {
            try
            {
                _inMemoryTodoItemProvider.Delete(data, id);
                return RedirectToAction(nameof(Index));
            }
            catch(KeyNotFoundException ex)
            {
                return View(_inMemoryTodoItemProvider.Get(id));
            }
        }
    }
}

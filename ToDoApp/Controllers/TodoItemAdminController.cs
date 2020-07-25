using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Interfaces;
using ToDoApp.Migrations;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class TodoItemAdminController : Controller
    {
        private readonly IAsyncDataProvider<TodoItem> _todoItemDataProvider;
        private readonly IAsyncDataProvider<Category> _categoryDataProvider;

        public TodoItemAdminController(IAsyncDataProvider<TodoItem> todoItemDataProvider, IAsyncDataProvider<Category> categoryDataProvider)
        {
            _todoItemDataProvider = todoItemDataProvider;
            _categoryDataProvider = categoryDataProvider;
        }


        // GET: TodoItemAdmin
        public async Task<IActionResult> Index()
        {
            var items = await _todoItemDataProvider.GetAll();
            return View(items);
        }

        // GET: TodoItemAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var todoItem = await _todoItemDataProvider.Get(id);
                return View(todoItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: TodoItemAdmin/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Categories"] = await _categoryDataProvider.GetAll();
            return View();
        }

        // POST: TodoItemAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _todoItemDataProvider.Create(todoItem);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException)
                {
                    ViewData["Categories"] = await _categoryDataProvider.GetAll();
                    return View(todoItem);
                }

            }
            else
            {
                ViewData["Categories"] = await _categoryDataProvider.GetAll();
                return View(todoItem);
            }
        }

        // GET: TodoItemAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                TodoItem todoItem = await _todoItemDataProvider.Get(id);
                ViewData["Categories"] = await _categoryDataProvider.GetAll();
                return View(todoItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }

        // POST: TodoItemAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TodoItem todoItem)
        {
            todoItem.Id = id;
            if (ModelState.IsValid)
            {
                try
                {
                    await _todoItemDataProvider.Update(todoItem);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["Categories"] = await _categoryDataProvider.GetAll();
                return View(todoItem);
            }
        }

        // GET: TodoItemAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _todoItemDataProvider.Get(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        }

        // POST: TodoItemAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _todoItemDataProvider.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }
    }
}

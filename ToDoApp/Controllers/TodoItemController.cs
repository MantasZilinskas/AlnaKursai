using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Data.Models;

namespace ToDoApp.Controllers
{
    public class TodoItemController : Controller
    {
        private readonly IDataService<TodoItemDAO> _dataService;

        public TodoItemController(IDataService<TodoItemDAO> dataService)
        {
            _dataService = dataService;
        }

        // GET: ToDoItem
        public ActionResult Index()
        {
            return View(_dataService.GetAll());
        }

        // GET: ToDoItem/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                return View(_dataService.Get(id));
            }
            catch (KeyNotFoundException)
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
        public ActionResult Create(TodoItemDAO data)
        {
            try
            {
                _dataService.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View(data);
            }
        }

        // GET: ToDoItem/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_dataService.Get(id));
        }

        // POST: ToDoItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TodoItemDAO data)
        {
            try
            {
                _dataService.Update(data);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return View(data);
            }
        }

        // GET: ToDoItem/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_dataService.Get(id));
        }

        // POST: ToDoItem/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TodoItemDAO data)
        {
            try
            {
                _dataService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch(KeyNotFoundException)
            {
                return View(data);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Interfaces;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class TodoItemController : Controller
    {
        private readonly IDataProvider<TodoItem> _dataProvider;

        public TodoItemController(IDataProvider<TodoItem> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        // GET: ToDoItem
        public ActionResult Index()
        {
            return View(_dataProvider.GetAll());
        }

        // GET: ToDoItem/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                return View(_dataProvider.Get(id));
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
                _dataProvider.Create(data);
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
            return View(_dataProvider.Get(id));
        }

        // POST: ToDoItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TodoItem data)
        {
            try
            {
                _dataProvider.Update(data);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException ex)
            {
                return View(data);
            }
        }

        // GET: ToDoItem/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_dataProvider.Get(id));
        }

        // POST: ToDoItem/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TodoItem data)
        {
            try
            {
                _dataProvider.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch(KeyNotFoundException ex)
            {
                return View(data);
            }
        }
    }
}

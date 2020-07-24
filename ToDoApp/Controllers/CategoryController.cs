using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Interfaces;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IDataProvider<Category> _dataProvider;

        public CategoryController(IDataProvider<Category> dataProvider)
        {
            _dataProvider = dataProvider;
        }

        // GET: Category
        public ActionResult Index()
        {
            return View(_dataProvider.GetAll());
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                return View(_dataProvider.Get(id));
            }
            catch(KeyNotFoundException ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category data)
        {
            try
            {
                _dataProvider.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch(ArgumentException ex)
            {
                return View(data);
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_dataProvider.Get(id));
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category data)
        {
            try
            {
                _dataProvider.Update(data);
                return RedirectToAction(nameof(Index));
            }
            catch(KeyNotFoundException ex)
            {
                return View(data);
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_dataProvider.Get(id));
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category data)
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

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Interfaces;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IInMemoryCategoryProvider _inMemoryCategoryProvider;
        private readonly IInFileCategoryProvider _inFileCategoryProvider;

        public CategoryController(IInMemoryCategoryProvider inMemoryCategoryProvider, IInFileCategoryProvider inFileCategoryProvider)
        {
            _inMemoryCategoryProvider = inMemoryCategoryProvider;
            _inFileCategoryProvider = inFileCategoryProvider;
        }

        // GET: Category
        public ActionResult Index()
        {
            return View(_inFileCategoryProvider.GetAll());
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                return View(_inFileCategoryProvider.Get(id));
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
                _inFileCategoryProvider.Create(data);
                return RedirectToAction(nameof(Index));
            }
            catch(ArgumentException ex)
            {
                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_inFileCategoryProvider.Get(id));
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category data)
        {
            try
            {
                _inFileCategoryProvider.Update(data, id);
                return RedirectToAction(nameof(Index));
            }
            catch(KeyNotFoundException ex)
            {
                return View(_inFileCategoryProvider.Get(id));
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_inFileCategoryProvider.Get(id));
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category data)
        {
            try
            {
                _inFileCategoryProvider.Delete(data, id);
                return RedirectToAction(nameof(Index));
            }
            catch(KeyNotFoundException ex)
            {
                return View(_inFileCategoryProvider.Get(id));
            }
        }
    }
}

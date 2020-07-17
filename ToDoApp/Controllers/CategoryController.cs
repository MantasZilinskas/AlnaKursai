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
    public class CategoryController : Controller
    {

        private readonly IInMemoryCategoryProvider _inMemoryCategoryProvider;

        public CategoryController(IInMemoryCategoryProvider inMemoryCategoryProvider)
        {
            _inMemoryCategoryProvider = inMemoryCategoryProvider;
        }

        // GET: Category
        public ActionResult Index()
        {
            return View(_inMemoryCategoryProvider.GetAll());
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                return View(_inMemoryCategoryProvider.Get(id));
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
                _inMemoryCategoryProvider.Create(data);
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
            return View(_inMemoryCategoryProvider.Get(id));
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category data)
        {
            try
            {
                _inMemoryCategoryProvider.Update(data, id);
                return RedirectToAction(nameof(Index));
            }
            catch(KeyNotFoundException ex)
            {
                return View(_inMemoryCategoryProvider.Get(id));
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_inMemoryCategoryProvider.Get(id));
        }

        // POST: Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category data)
        {
            try
            {
                _inMemoryCategoryProvider.Delete(data, id);
                return RedirectToAction(nameof(Index));
            }
            catch(KeyNotFoundException ex)
            {
                return View(_inMemoryCategoryProvider.Get(id));
            }
        }
    }
}

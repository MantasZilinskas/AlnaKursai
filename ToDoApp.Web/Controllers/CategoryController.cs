using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Web.ViewModels;

namespace TodoApp.Web.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IAsyncDataService<CategoryViewModel> _dataService;

        public CategoryController(IAsyncDataService<CategoryViewModel> dataService)
        {
            _dataService = dataService;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            return View(await _dataService.GetAll());
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                return View(await _dataService.Get(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _dataService.Create(category);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException)
                {
                    return View(category);
                }

            }
            return View(category);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var Category = await _dataService.Get(id);
                return View(Category);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name")] CategoryViewModel category)
        {
            category.Id = id;
            if (ModelState.IsValid)
            {
                try
                {
                    await _dataService.Update(category);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Category = await _dataService.Get(id);
            if (Category == null)
            {
                return NotFound();
            }

            return View(Category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _dataService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }
    }
}

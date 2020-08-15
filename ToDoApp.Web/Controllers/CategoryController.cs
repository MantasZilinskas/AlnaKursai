using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Buisiness.Models;
using TodoApp.Web.ViewModels;

namespace TodoApp.Web.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IAsyncDataService<CategoryVO> _dataService;
        private readonly IMapper _mapper;
        public CategoryController(IAsyncDataService<CategoryVO> dataService, IMapper mapper)
        {
            _mapper = mapper;
            _dataService = dataService;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<CategoryViewModel>>(await _dataService.GetAll()));
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
                return View(_mapper.Map<CategoryViewModel>(await _dataService.Get(id)));
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
                    await _dataService.Create(_mapper.Map<CategoryVO>(category));
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
                return View(_mapper.Map<CategoryViewModel>(await _dataService.Get(id)));
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
                    await _dataService.Update(_mapper.Map<CategoryVO>(category));
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

            var category = await _dataService.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<CategoryViewModel>(category));
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

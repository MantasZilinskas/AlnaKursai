using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Web.ViewModels;

namespace TodoApp.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly IAsyncDataService<TagViewModel> _dataService;

        public TagController(IAsyncDataService<TagViewModel> dataService)
        {
            _dataService = dataService;
        }

        // GET: Tag
        public async Task<IActionResult> Index()
        {
            return View(await _dataService.GetAll());
        }

        // GET: tag/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var Tag = await _dataService.Get(id);
                return View(Tag);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: Tag/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tag/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] TagViewModel tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _dataService.Create(tag);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException)
                {
                    return View(tag);
                }

            }
            return View(tag);
        }

        // GET: Tag/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var Tag = await _dataService.Get(id);
                return View(Tag);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }

        // POST: Tag/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name")] TagViewModel tag)
        {
            tag.Id = id;
            if (ModelState.IsValid)
            {
                try
                {
                    await _dataService.Update(tag);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: Tag/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Tag = await _dataService.Get(id);
            if (Tag == null)
            {
                return NotFound();
            }

            return View(Tag);
        }

        // POST: Tag/Delete/5
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

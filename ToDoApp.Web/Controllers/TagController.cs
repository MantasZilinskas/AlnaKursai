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
    public class TagController : Controller
    {
        private readonly IAsyncDataService<TagVO> _dataService;
        private readonly IMapper _mapper;
        public TagController(IAsyncDataService<TagVO> dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }
        // GET: Tag
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<TagViewModel>>(await _dataService.GetAll()));
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
                return View(_mapper.Map<TagViewModel>(await _dataService.Get(id)));
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
                    await _dataService.Create(_mapper.Map<TagVO>(tag));
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
                return View(_mapper.Map<TagViewModel>(await _dataService.Get(id)));
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
                    await _dataService.Update(_mapper.Map<TagVO>(tag));
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

            var tag = await _dataService.Get(id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<TagViewModel>(tag));
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

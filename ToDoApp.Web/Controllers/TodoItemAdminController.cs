using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Web.ViewModels;

namespace TodoApp.Web.Controllers
{
    public class TodoItemAdminController : Controller
    {
        private readonly IAsyncDataService<TodoItemViewModel> _todoItemdataService;
        private readonly IAsyncDataService<CategoryViewModel> _categorydataService;
        private readonly IAsyncDataService<TagViewModel> _tagdataService;
        private readonly IItemTagService _itemTagService;

        public TodoItemAdminController(
            IAsyncDataService<TodoItemViewModel> todoItemdataService,
            IAsyncDataService<CategoryViewModel> categorydataService,
            IAsyncDataService<TagViewModel> tagdataService,
            IItemTagService itemTagService)
        {
            _todoItemdataService = todoItemdataService;
            _categorydataService = categorydataService;
            _tagdataService = tagdataService;
            _itemTagService = itemTagService;
        }


        // GET: TodoItemAdmin
        public async Task<IActionResult> Index()
        {
            var items = await _todoItemdataService.GetAll();
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
                var todoItem = await _todoItemdataService.Get(id);
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
            ViewData["Categories"] = await _categorydataService.GetAll();
            ViewData["Tags"] = await _tagdataService.GetAll();
            return View();
        }

        // POST: TodoItemAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoItemViewModel todoItem, List<int> tagId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int todoItemId = await _todoItemdataService.Create(todoItem);
                    await _itemTagService.Create(todoItemId, tagId);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ViewData["Categories"] = await _categorydataService.GetAll();
                    ViewData["Tags"] = await _tagdataService.GetAll();
                    ModelState.AddModelError("Name", ex.Message);
                    return View(todoItem);
                }

            }
            else
            {
                ViewData["Categories"] = await _categorydataService.GetAll();
                ViewData["Tags"] = await _tagdataService.GetAll();
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
                TodoItemViewModel todoItem = await _todoItemdataService.Get(id);
                ViewData["Categories"] = await _categorydataService.GetAll();
                ViewData["Tags"] = await _tagdataService.GetAll();
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
        public async Task<IActionResult> Edit(int id, TodoItemViewModel todoItem, List<int> tagId)
        {
            todoItem.Id = id;
            if (ModelState.IsValid)
            {
                try
                {
                    await _todoItemdataService.Update(todoItem);
                    await _itemTagService.Update(id, tagId);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["Categories"] = await _categorydataService.GetAll();
                ViewData["Tags"] = await _tagdataService.GetAll();
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

            var todoItem = await _todoItemdataService.Get(id);
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
                await _todoItemdataService.Delete(id);
                await _itemTagService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

        }
    }
}

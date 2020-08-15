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
    public class TodoItemAdminController : Controller
    {
        private readonly IAsyncDataService<TodoItemVO> _todoItemdataService;
        private readonly IAsyncDataService<CategoryVO> _categorydataService;
        private readonly IAsyncDataService<TagVO> _tagdataService;
        private readonly IItemTagService _itemTagService;
        private readonly IMapper _mapper;

        public TodoItemAdminController(
            IAsyncDataService<TodoItemVO> todoItemdataService,
            IAsyncDataService<CategoryVO> categorydataService,
            IAsyncDataService<TagVO> tagdataService,
            IItemTagService itemTagService,
            IMapper mapper)
        {
            _todoItemdataService = todoItemdataService;
            _categorydataService = categorydataService;
            _tagdataService = tagdataService;
            _itemTagService = itemTagService;
            _mapper = mapper;
        }


        // GET: TodoItemAdmin
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<TodoItemViewModel>>(await _todoItemdataService.GetAll()));
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
                return View(_mapper.Map<TodoItemViewModel>(await _todoItemdataService.Get(id)));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: TodoItemAdmin/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Categories"] = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categorydataService.GetAll());
            ViewData["Tags"] = _mapper.Map<IEnumerable<TagViewModel>>(await _tagdataService.GetAll());
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
                    int todoItemId = await _todoItemdataService.Create(_mapper.Map<TodoItemVO>(todoItem));
                    await _itemTagService.Create(todoItemId, tagId);
                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ViewData["Categories"] = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categorydataService.GetAll());
                    ViewData["Tags"] = _mapper.Map<IEnumerable<TagViewModel>>(await _tagdataService.GetAll());
                    ModelState.AddModelError("Name", ex.Message);
                    return View(todoItem);
                }

            }
            else
            {
                ViewData["Categories"] = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categorydataService.GetAll());
                ViewData["Tags"] = _mapper.Map<IEnumerable<TagViewModel>>(await _tagdataService.GetAll());
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
                ViewData["Categories"] = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categorydataService.GetAll());
                ViewData["Tags"] = _mapper.Map<IEnumerable<TagViewModel>>(await _tagdataService.GetAll());
                return View(_mapper.Map<TodoItemViewModel>(await _todoItemdataService.Get(id)));
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
                    await _todoItemdataService.Update(_mapper.Map<TodoItemVO>(todoItem));
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
                ViewData["Categories"] = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categorydataService.GetAll());
                ViewData["Tags"] = _mapper.Map<IEnumerable<TagViewModel>>(await _tagdataService.GetAll());
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

            var todoItem = _mapper.Map<TodoItemViewModel>(await _todoItemdataService.Get(id));
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

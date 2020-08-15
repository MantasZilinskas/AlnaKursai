using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Buisiness.Interfaces;
using TodoApp.Buisiness.Models;
using TodoApp.Web.ViewModels;

namespace TodoApp.Web.Controllers
{
    public class TodoItemController : Controller
    {
        private readonly IDataService<TodoItemVO> _dataService;
        private readonly IMapper _mapper;

        public TodoItemController(IDataService<TodoItemVO> dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }
        // GET: ToDoItem
        public ActionResult Index()
        {
            return View(_mapper.Map<IEnumerable<TodoItemViewModel>>(_dataService.GetAll()));
        }

        // GET: ToDoItem/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                return View(_mapper.Map<TodoItemViewModel>(_dataService.Get(id)));
            }
            catch (KeyNotFoundException)
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
        public ActionResult Create(TodoItemViewModel data)
        {
            try
            {
                _dataService.Create(_mapper.Map<TodoItemVO>(data));
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
            return View(_mapper.Map<TodoItemViewModel>(_dataService.Get(id)));
        }

        // POST: ToDoItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TodoItemViewModel data)
        {
            try
            {
                _dataService.Update(_mapper.Map<TodoItemVO>(data));
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return View(data);
            }
        }

        // GET: ToDoItem/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_mapper.Map<TodoItemViewModel>(_dataService.Get(id)));
        }

        // POST: ToDoItem/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TodoItemViewModel data)
        {
            try
            {
                _dataService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return View(data);
            }
        }
    }
}

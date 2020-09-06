using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Web.Controllers
{
    public class ProjectApiController : Controller
    {
        // GET: ProjectApiController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProjectApiController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProjectApiController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectApiController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectApiController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProjectApiController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectApiController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProjectApiController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

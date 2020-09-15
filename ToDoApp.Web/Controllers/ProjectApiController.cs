using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.ClientsAndProjects.ApiClient;

namespace TodoApp.Web.Controllers
{
    public class ProjectApiController : Controller
    {
        private readonly Client apiClient;

        public ProjectApiController(Client apiClient)
        {
            this.apiClient = apiClient;
        }

        // GET: ProjectApiController
        public async Task<ActionResult> Index()
        {
            return View(await apiClient.ApiProjectsGetAsync());
        }

        // GET: ProjectApiController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var project = await apiClient.ApiProjectsGetAsync(id);
            return View(project);
        }

        // GET: ProjectApiController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectApiController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProjectDAO project)
        {
            try
            {
                await apiClient.ApiProjectsPostAsync(project);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(project);
            }
        }

        // GET: ProjectApiController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var project = await apiClient.ApiProjectsGetAsync(id);
            return View(project);
        }

        // POST: ProjectApiController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProjectDAO project)
        {
            try
            {
                await apiClient.ApiProjectsPutAsync(id,project);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(project);
            }
        }

        // GET: ProjectApiController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var project = await apiClient.ApiProjectsGetAsync(id);
            return View(project);
        }

        // POST: ProjectApiController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ProjectDAO project)
        {
            try
            {
                await apiClient.ApiProjectsDeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(project);
            }
        }
    }
}

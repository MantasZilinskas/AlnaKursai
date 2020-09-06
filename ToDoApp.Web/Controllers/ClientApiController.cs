using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.ClientsAndProjects.ApiClient;

namespace TodoApp.Web.Controllers
{
    public class ClientApiController : Controller
    {
        private readonly Client apiClient;

        public ClientApiController(Client apiClient)
        {
            this.apiClient = apiClient;
        }

        // GET: ClientApiController
        public async Task<ActionResult> Index()
        {
            return View(await apiClient.ApiClientsGetAsync());
        }

        // GET: ClientApiController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View(await apiClient.ApiClientsGetAsync(id));
        }

        // GET: ClientApiController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientApiController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClientDAO client)
        {
            try
            {
                await apiClient.ApiClientsPostAsync(client);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(client);
            }
        }

        // GET: ClientApiController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var client = await apiClient.ApiClientsGetAsync(id);
            return View(client);
        }

        // POST: ClientApiController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ClientDAO client)
        {
            try
            {
                await apiClient.ApiClientsPutAsync(id, client);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(client);
            }
        }

        // GET: ClientApiController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var client = await apiClient.ApiClientsGetAsync(id);
            return View(client);
        }

        // POST: ClientApiController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ClientDAO client)
        {
            try
            {
                await apiClient.ApiClientsDeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(client);
            }
        }
    }
}

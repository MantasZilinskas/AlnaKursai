using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.ClientsAndProjects.Api.Data.Context;
using TodoApp.ClientsAndProjects.Api.Data.Model;

namespace TodoApp.ClientsAndProjects.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientProjectsController : ControllerBase
    {
        private readonly ClientProjectApiContext _context;

        public ClientProjectsController(ClientProjectApiContext context)
        {
            _context = context;
        }

        [HttpGet("{clientId}")]
        public async Task<IEnumerable<Project>> GetClientProjects(int clientId)
        {
            return await _context.Projects.Where(project => project.ClientId == clientId).ToListAsync();
        }
        [HttpPost("{clientId}")]
        public async Task<IActionResult> CreateClientProject(Project project, int clientId)
        {
            project.ClientId = clientId;
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return Created($"api/Projects/GetProject/{project.Id}", project);
        }
        [HttpDelete("{clientId}/{projectId}")]
        public async Task<ActionResult<Project>> DeleteClientProject(int clientId, int projectId)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(project => project.ClientId == clientId && project.Id == projectId);

            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return project;
        }
        [HttpPut("{clientId}/{projectId}")]
        public async Task<IActionResult> EditProjectClient(int projectId, int clientId, Client client)
        {
            if (clientId != client.Id)
            {
                return BadRequest();
            }
            if (ProjectExists(projectId, clientId))
            {
                _context.Entry(client).State = EntityState.Modified;
            }
            else
            {
                NoContent();
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(projectId, clientId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        private bool ProjectExists(int projectId, int clientId)
        {
            return _context.Projects.Any(e => e.Id == projectId && e.ClientId == clientId);
        }
    }
}

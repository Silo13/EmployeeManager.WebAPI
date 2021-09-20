using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManager.WebAPI.Models;

namespace EmployeeManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesJobCategoriesController : ControllerBase
    {
        private readonly EmployeeManagerStoreContext _context;

        public EmployeesJobCategoriesController(EmployeeManagerStoreContext context)
        {
            _context = context;
        }

        // GET: api/EmployeesJobCategories
        /// <summary>
        /// Výpis zoznamu všetkých zamestnancov a ich pracovných funkcií
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeesJobCategories>>> GetEmployeesJobCategories()
        {
            return await _context.EmployeesJobCategories.ToListAsync();
        }

        // GET: api/EmployeesJobCategories/5
        /// <summary>
        /// Výpis konkrétnej pracovnej funkcie konkrétneho zamestnanca
        /// </summary>
        /// <param name="id">Id kombinácie zamestnanec - pracovná funkcia</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeesJobCategories>> GetEmployeesJobCategories(int id)
        {
            var employeesJobCategories = await _context.EmployeesJobCategories.FindAsync(id);

            if (employeesJobCategories == null)
            {
                return NotFound();
            }

            return employeesJobCategories;
        }

        // PUT: api/EmployeesJobCategories/5
        /// <summary>
        /// Editácia konkrétnej pracovnej funkcie konkrétneho zamestnanca
        /// </summary>
        /// <param name="id">Id kombinácie zamestnanec - pracovná funkcia</param>
        /// <param name="employeesJobCategories">Údaje o kombinácii zamestnanec - pracovná funkcia</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeesJobCategories(int id, EmployeesJobCategories employeesJobCategories)
        {
            if (id != employeesJobCategories.Id)
            {
                return BadRequest();
            }

            _context.Entry(employeesJobCategories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeesJobCategoriesExists(id))
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

        // POST: api/EmployeesJobCategories
        /// <summary>
        /// Pridanie novej kombinácie zamestnanec - pracovná funkcia
        /// </summary>
        /// <param name="employeesJobCategories">Údaje o kombinácii zamestnanec - pracovná funkcia</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EmployeesJobCategories>> PostEmployeesJobCategories(EmployeesJobCategories employeesJobCategories)
        {
            _context.EmployeesJobCategories.Add(employeesJobCategories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeesJobCategories", new { id = employeesJobCategories.Id }, employeesJobCategories);
        }

        // DELETE: api/EmployeesJobCategories/5
        /// <summary>
        /// Zmazanie konkrétnej pracovnej funkcie konkrétnemu zamestnancovi
        /// </summary>
        /// <param name="id">Id kombinácie zamestnanec - pracovná funkcia</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeesJobCategories>> DeleteEmployeesJobCategories(int id)
        {
            var employeesJobCategories = await _context.EmployeesJobCategories.FindAsync(id);
            if (employeesJobCategories == null)
            {
                return NotFound();
            }

            _context.EmployeesJobCategories.Remove(employeesJobCategories);
            await _context.SaveChangesAsync();

            return employeesJobCategories;
        }

        // Privátna metóda na zistenie, či kombinácia zamestnanec - pracovná funkcia s konkrétnym ID existuje alebo nie
        private bool EmployeesJobCategoriesExists(int id)
        {
            return _context.EmployeesJobCategories.Any(e => e.Id == id);
        }
    }
}

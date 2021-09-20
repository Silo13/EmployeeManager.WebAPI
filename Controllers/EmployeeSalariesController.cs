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
    public class EmployeeSalariesController : ControllerBase
    {
        private readonly EmployeeManagerStoreContext _context;

        public EmployeeSalariesController(EmployeeManagerStoreContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeSalaries
        /// <summary>
        /// Výpis zoznamu všetkých platov všetkých zamestnancov
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeSalaries>>> GetEmployeeSalaries()
        {
            return await _context.EmployeeSalaries.ToListAsync();
        }

        // GET: api/EmployeeSalaries/5
        /// <summary>
        /// Výpis konkrétneho platu konkrétneho zamestnanca
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeSalaries>> GetEmployeeSalaries(int id)
        {
            var employeeSalaries = await _context.EmployeeSalaries.FindAsync(id);

            if (employeeSalaries == null)
            {
                return NotFound();
            }

            return employeeSalaries;
        }

        // PUT: api/EmployeeSalaries/5
        /// <summary>
        /// Editácia konkrétneho platu konkrétneho zamestnanca
        /// </summary>
        /// <param name="id">Id kombinácie zamestnanec - plat</param>
        /// <param name="employeeSalaries">Údaje o kombinácii zamestnanec - plat</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeSalaries(int id, EmployeeSalaries employeeSalaries)
        {
            if (id != employeeSalaries.Id)
            {
                return BadRequest();
            }

            _context.Entry(employeeSalaries).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeSalariesExists(id))
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

        // POST: api/EmployeeSalaries
        /// <summary>
        /// Pridanie nového platu pre nového zamestnanca
        /// </summary>
        /// <param name="employeeSalaries">Údaje o kombinácii zamestnanec - plat</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EmployeeSalaries>> PostEmployeeSalaries(EmployeeSalaries employeeSalaries)
        {
            _context.EmployeeSalaries.Add(employeeSalaries);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeSalaries", new { id = employeeSalaries.Id }, employeeSalaries);
        }

        // DELETE: api/EmployeeSalaries/5
        /// <summary>
        /// Zmazanie konkrétneho platu konkrétneho zamestnanca
        /// </summary>
        /// <param name="id">Id kombinácie zamestnanec - plat</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeSalaries>> DeleteEmployeeSalaries(int id)
        {
            var employeeSalaries = await _context.EmployeeSalaries.FindAsync(id);
            if (employeeSalaries == null)
            {
                return NotFound();
            }

            _context.EmployeeSalaries.Remove(employeeSalaries);
            await _context.SaveChangesAsync();

            return employeeSalaries;
        }

        // Privátna metóda na zistenie, či kombinácia zamestnanec - plat s konkrétnym ID existuje alebo nie
        private bool EmployeeSalariesExists(int id)
        {
            return _context.EmployeeSalaries.Any(e => e.Id == id);
        }
    }
}

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
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeManagerStoreContext _context;

        public EmployeesController(EmployeeManagerStoreContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        /// <summary>
        /// Výpis zoznamu všetkých zamestnancov
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            return await _context.Employee.ToListAsync();
        }

        // GET: api/Employees/5
        /// <summary>
        /// Výpis konkrétneho zamestnanca podľa jeho Id
        /// </summary>
        /// <param name="id">Id zamestnaca</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        /// <summary>
        /// Editácia existujúceho zamestnanca
        /// </summary>
        /// <param name="id">Id zamestnanca, ktorý sa má upraviť</param>
        /// <param name="employee">Údaje o zamestnancovi</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        /// <summary>
        /// Pridanie nového zamestnanca
        /// </summary>
        /// <param name="employee">Údaje o zamestnancovi</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        /// <summary>
        /// Zmazanie konkrétneho zamestnanca
        /// </summary>
        /// <param name="id">Id zamestnanca, ktorý sa má zmazať</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        // Privátna metóda na zistenie, či zamestnanec s konkrétnym ID existuje alebo nie
        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}

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
    public class CitiesController : ControllerBase
    {
        private readonly EmployeeManagerStoreContext _context;

        public CitiesController(EmployeeManagerStoreContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        /// <summary>
        /// Výpis zoznamu všetkých miest
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCity()
        {
            return await _context.City.ToListAsync();
        }

        // GET: api/Cities/5
        /// <summary>
        /// Výpis konkrétneho mesta
        /// </summary>
        /// <param name="id">Id mesta</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _context.City.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // PUT: api/Cities/5
        /// <summary>
        /// Editácia existujúceho mesta
        /// </summary>
        /// <param name="id">Id mesta, ktoré sa má upraviť</param>
        /// <param name="city">Údaje o meste</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST: api/Cities
        /// <summary>
        /// Pridanie nového mesta
        /// </summary>
        /// <param name="city">Údaje o meste</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
            _context.City.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = city.Id }, city);
        }

        // DELETE: api/Cities/5
        /// <summary>
        /// Zmazanie konkrétneho mesta podľa jeho ID
        /// </summary>
        /// <param name="id">Id mesta</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<City>> DeleteCity(int id)
        {
            var city = await _context.City.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.City.Remove(city);
            await _context.SaveChangesAsync();

            return city;
        }

        // Privátna metóda na zistenie, či mesto s konkrétnym ID existuje alebo nie
        private bool CityExists(int id)
        {
            return _context.City.Any(e => e.Id == id);
        }
    }
}

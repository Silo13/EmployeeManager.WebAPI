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
    public class CountriesController : ControllerBase
    {
        private readonly EmployeeManagerStoreContext _context;

        public CountriesController(EmployeeManagerStoreContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        /// <summary>
        /// Výpis zoznamu všetkých krajín
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountry()
        {
            return await _context.Country.ToListAsync();
        }

        // GET: api/Countries/5
        /// <summary>
        /// Výpis konkrétnej krajiny podľa jej Id
        /// </summary>
        /// <param name="id">Id krajiny</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Country.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        // PUT: api/Countries/5
        /// <summary>
        /// Editácia konkrétnej krajiny
        /// </summary>
        /// <param name="id">Id krajiny, ktorá sa má upraviť</param>
        /// <param name="country">Údaje krajiny</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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

        // POST: api/Countries
        /// <summary>
        /// Pridanie novej krajiny
        /// </summary>
        /// <param name="country">Údaje o krajine</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(Country country)
        {
            _context.Country.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        /// <summary>
        /// Zmazanie konrétnej krajiny
        /// </summary>
        /// <param name="id">Id krajiny, ktorá sa má zmazať</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Country>> DeleteCountry(int id)
        {
            var country = await _context.Country.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Country.Remove(country);
            await _context.SaveChangesAsync();

            return country;
        }

        // Privátna metóda na zistenie, či krajina s konkrétnym ID existuje alebo nie
        private bool CountryExists(int id)
        {
            return _context.Country.Any(e => e.Id == id);
        }
    }
}

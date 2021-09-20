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
    public class AddressesController : ControllerBase
    {
        private readonly EmployeeManagerStoreContext _context;

        public AddressesController(EmployeeManagerStoreContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        /// <summary>
        /// Výpis zoznamu všetkých adries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            return await _context.Address.ToListAsync();
        }

        /// <summary>
        /// Výpis jednej konkrétnej adresy podľa jej Id
        /// </summary>
        /// <param name="id">Id adresy, ktorá sa má upraviť</param>
        /// <returns></returns>
        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _context.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5
        /// <summary>
        /// Editácia existujúcej adresy
        /// </summary>
        /// <param name="id">Id adresy</param>
        /// <param name="address">Údaje o adrese</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        /// <summary>
        /// Pridanie novej adresy
        /// </summary>
        /// <param name="address">Údaje o adrese</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.Id }, address);
        }

        // DELETE: api/Addresses/5
        /// <summary>
        /// Zmazanie konkrétnej adresy
        /// </summary>
        /// <param name="id">Id adresy, ktorá sa má zmazať</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return address;
        }

        // Privátna metóda na zistenie, či adresa s konkrétnym ID existuje alebo nie
        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.Id == id);
        }
    }
}

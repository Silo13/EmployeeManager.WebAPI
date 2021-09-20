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
    public class JobCategoriesController : ControllerBase
    {
        private readonly EmployeeManagerStoreContext _context;

        public JobCategoriesController(EmployeeManagerStoreContext context)
        {
            _context = context;
        }

        // GET: api/JobCategories
        /// <summary>
        /// Výpis zoznamu všetkých pracovných funkcií
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobCategory>>> GetJobCategory()
        {
            return await _context.JobCategory.ToListAsync();
        }

        // GET: api/JobCategories/5
        /// <summary>
        /// Výpis konkrétnej pracovnej funkcie podľa jej Id
        /// </summary>
        /// <param name="id">Id pracovnej funkcie</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<JobCategory>> GetJobCategory(int id)
        {
            var jobCategory = await _context.JobCategory.FindAsync(id);

            if (jobCategory == null)
            {
                return NotFound();
            }

            return jobCategory;
        }

        // PUT: api/JobCategories/5
        /// <summary>
        /// Editácia konkrétnej pracovnej funkcie
        /// </summary>
        /// <param name="id">Id pracovnej funkcie, ktorá sa má editovať</param>
        /// <param name="jobCategory">Údaje pracovnej funkcie</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobCategory(int id, JobCategory jobCategory)
        {
            if (id != jobCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobCategoryExists(id))
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

        // POST: api/JobCategories
        /// <summary>
        /// Pridanie novej pracovnej funkcie
        /// </summary>
        /// <param name="jobCategory">Údaje o pracovnej funckii</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<JobCategory>> PostJobCategory(JobCategory jobCategory)
        {
            _context.JobCategory.Add(jobCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobCategory", new { id = jobCategory.Id }, jobCategory);
        }

        // DELETE: api/JobCategories/5
        /// <summary>
        /// Zmazanie konkrétnej pracovnej funkcie podľa jej Id
        /// </summary>
        /// <param name="id">Id pracovnej funkcie, ktorá sa má zmazať</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<JobCategory>> DeleteJobCategory(int id)
        {
            var jobCategory = await _context.JobCategory.FindAsync(id);
            if (jobCategory == null)
            {
                return NotFound();
            }

            _context.JobCategory.Remove(jobCategory);
            await _context.SaveChangesAsync();

            return jobCategory;
        }

        // Privátna metóda na zistenie, či pracovná funkcia s konkrétnym ID už existuje alebo nie
        private bool JobCategoryExists(int id)
        {
            return _context.JobCategory.Any(e => e.Id == id);
        }
    }
}

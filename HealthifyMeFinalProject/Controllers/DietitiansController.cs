using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthifyMeFinalProject.Data;
using HealthifyMeFinalProject.Models;

namespace HealthifyMeFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DietitiansController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DietitiansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Dietitians
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dietitian>>> GetDietitians()
        {
            try
            {
                var dietitians = await _context.Dietitians.ToListAsync();

                // Check if data exists in the database
                if (dietitians == null)
                {
                    return NotFound();           // RETURN: No data was found  HTTP 404
                }
                return Ok(dietitians);       // RETURN: OkObjectResult  HTTP 200
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);     // RETURN: BadResult HTTP 404
            }
        }
            
        

        // GET: api/Dietitians/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dietitian>> GetDietitian(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();                // RETURN: BadResult HTTP 404
            }
            try
            {
                var dietitian = await _context.Dietitians.FindAsync(id);

                if (dietitian == null)
                {
                    return NotFound();                  // RETURN: No data was found  HTTP 404
                }
                return Ok(dietitian);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);           // RETURN: BadResult HTTP 404
            }

        }

        // PUT: api/Dietitians/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDietitian(int id, Dietitian dietitian)
        {
            if (id != dietitian.DietitianId)
            {
                return BadRequest();
            }

            _context.Entry(dietitian).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DietitianExists(id))
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

        // POST: api/Dietitians
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Dietitian>> PostDietitian(Dietitian dietitian)
        {
            _context.Dietitians.Add(dietitian);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDietitian", new { id = dietitian.DietitianId }, dietitian);
        }

        // DELETE: api/Dietitians/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dietitian>> DeleteDietitian(int id)
        {
            var dietitian = await _context.Dietitians.FindAsync(id);
            if (dietitian == null)
            {
                return NotFound();
            }

            _context.Dietitians.Remove(dietitian);
            await _context.SaveChangesAsync();

            return dietitian;
        }

        private bool DietitianExists(int id)
        {
            return _context.Dietitians.Any(e => e.DietitianId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthifyMeFinalProject.Data;
using HealthifyMeFinalProject.Models;
using Microsoft.Extensions.Logging;

namespace HealthifyMeFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackFormsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FeedBackFormsController> _logger;

        public FeedBackFormsController(ApplicationDbContext context, ILogger<FeedBackFormsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/FeedBackForms
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<FeedBackForm>>> GetFeedBackForms()
        public async Task<IActionResult> GetFeedBackForms()
        {
            try
            {
                var feedBackForms = await _context.FeedBackForms.ToListAsync();

                // Check if data exists in the database
                if (feedBackForms == null)
                {
                    return NotFound();           // RETURN: No data was found  HTTP 404
                }
                return Ok(feedBackForms);       // RETURN: OkObjectResult  HTTP 200
            }
            catch(Exception exp)
            {
                return BadRequest(exp.Message);     // RETURN: BadResult HTTP 404
            }
        }

        // GET: api/FeedBackForms/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFeedBackForm(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();                // RETURN: BadResult HTTP 404
            }
            try
            {
                var feedBackForm = await _context.FeedBackForms.FindAsync(id);

                if (feedBackForm == null)
                {
                    return NotFound();                   // RETURN: No data was found  HTTP 404
                }

                return Ok(feedBackForm);                  // RETURN: OkObjectResult  HTTP 200
            }
            catch(Exception exp)
            {
                return BadRequest(exp.Message);           // RETURN: BadResult HTTP 404
            }
        }

        // PUT: api/FeedBackForms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedBackForm(int id, FeedBackForm feedBackForm)
        {
            if (id != feedBackForm.FormId)
            {
                return BadRequest();
            }

            _context.Entry(feedBackForm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedBackFormExists(id))
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

        // POST: api/FeedBackForms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult> PostFeedBackForm(FeedBackForm feedBackForm)
        {
            try
            {
                _context.FeedBackForms.Add(feedBackForm);
                await _context.SaveChangesAsync();

                var result = CreatedAtAction("GetFeedBackForm", new { id = feedBackForm.FormId }, feedBackForm);
                return Ok(result);
            }
            catch(System.Exception exp) 
            {
                return BadRequest(exp.Message);
            }
        }

        // DELETE: api/FeedBackForms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFeedBackForm(int? id)
        {
            if(!id.HasValue)
            {
                return BadRequest();
            }
            try
            {
                var feedBackForm = await _context.FeedBackForms.FindAsync(id);
                if (feedBackForm == null)
                {
                    return NotFound();
                }

                _context.FeedBackForms.Remove(feedBackForm);
                await _context.SaveChangesAsync();

                return Ok(feedBackForm);
            }
            catch(System.Exception exp)
            {
                ModelState.AddModelError("DELETE", exp.Message);
                return BadRequest(ModelState);
            }
        }

        private bool FeedBackFormExists(int id)
        {
            return _context.FeedBackForms.Any(e => e.FormId == id);
        }
    }
}

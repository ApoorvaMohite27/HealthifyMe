using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthifyMeFinalProject.Data;
using HealthifyMeFinalProject.Models;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HealthifyMeFinalProject.Areas.HealthifyMeMgmt.Controllers
{
    [Area("HealthifyMeMgmt")]
    [Authorize]
    public class AssignTasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AssignTasksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: HealthifyMeMgmt/AssignTasks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AssignTasks.Include(a => a.CustomerDetail).Include(a => a.Dietitian);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> CurrentUserDetails()
        {
            var user = await _userManager.GetUserAsync(User);

            var customer = _context.CustomerDetails.SingleOrDefault(c => c.CustomerName == user.UserName);

            if(customer == null)
            {
                return Ok("No Task Assign...");
            }
            int customerId = customer.CustomerId;
            var query = await _context.AssignTasks.Where(c => c.CustomerId == customerId)
                .Include(a => a.CustomerDetail)
                .Include(a => a.Dietitian)
                .ToListAsync();

            return View(viewName:"Index", query);
        }


        public async Task<IActionResult> ForDietitian()
        {
            var user = await _userManager.GetUserAsync(User);

            var dietitian = _context.Dietitians.SingleOrDefault(c => c.DietitianUserName == user.UserName);

            if (dietitian == null)
            {
                return Ok("No Task Assign...");
            }
            var customerId = dietitian.DietitianId;
            var query = await _context.AssignTasks.Where(c => c.DietitianId == customerId)
                .Include(a => a.CustomerDetail)
                .Include(a => a.Dietitian)
                .ToListAsync();

            return View(viewName: "Index", query);
        }

        // GET: HealthifyMeMgmt/AssignTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignTask = await _context.AssignTasks
                .Include(a => a.CustomerDetail)
                .Include(a => a.Dietitian)
                .FirstOrDefaultAsync(m => m.AssignTaskId == id);
            if (assignTask == null)
            {
                return NotFound();
            }

            return View(assignTask);
        }

        // GET: HealthifyMeMgmt/AssignTasks/Create
        public IActionResult Create(int id)
        {
            //CustomerDetail customerDetail = new CustomerDetail();
            var query = _context.CustomerDetails.Where(c => c.CustomerId == id);

            ViewData["CustomerId"] = new SelectList(query, "CustomerId", "CustomerName");
            ViewData["DietitianId"] = new SelectList(_context.Dietitians, "DietitianId", "DietitianName");

            AssignTask assignTask = new AssignTask();
            assignTask.Diet = "In Progress...";
            assignTask.Exercise = "In Progress...";
            return View(assignTask);
        }

        // POST: HealthifyMeMgmt/AssignTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssignTaskId,Diet,Exercise,CustomerId,DietitianId")] AssignTask assignTask)
        {
            if (ModelState.IsValid)
            {
                assignTask.Diet = "In Progress...";
                assignTask.Exercise = "In Progress...";
                _context.Add(assignTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustomerId", "CustomerName", assignTask.CustomerId);
            ViewData["DietitianId"] = new SelectList(_context.Dietitians, "DietitianId", "DietitianName", assignTask.DietitianId);
            TempData["AlertMessageDietitian"] = "New Customer is Added Please Assign a Task......!";
            return View(assignTask);
        }

        // GET: HealthifyMeMgmt/AssignTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignTask = await _context.AssignTasks.FindAsync(id);
            if (assignTask == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustomerId", "CustomerName", assignTask.CustomerId);
            ViewData["DietitianId"] = new SelectList(_context.Dietitians, "DietitianId", "DietitianName", assignTask.DietitianId);
            return View(assignTask);
        }

        // POST: HealthifyMeMgmt/AssignTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssignTaskId,Diet,Exercise,CustomerId,DietitianId")] AssignTask assignTask)
        {
            if (id != assignTask.AssignTaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignTaskExists(assignTask.AssignTaskId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerDetails, "CustomerId", "CustomerName", assignTask.CustomerId);
            ViewData["DietitianId"] = new SelectList(_context.Dietitians, "DietitianId", "DietitianName", assignTask.DietitianId);
            return View(assignTask);
        }

        // GET: HealthifyMeMgmt/AssignTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignTask = await _context.AssignTasks
                .Include(a => a.CustomerDetail)
                .Include(a => a.Dietitian)
                .FirstOrDefaultAsync(m => m.AssignTaskId == id);
            if (assignTask == null)
            {
                return NotFound();
            }

            return View(assignTask);
        }

        // POST: HealthifyMeMgmt/AssignTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignTask = await _context.AssignTasks.FindAsync(id);
            _context.AssignTasks.Remove(assignTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignTaskExists(int id)
        {
            return _context.AssignTasks.Any(e => e.AssignTaskId == id);
        }
    }
}

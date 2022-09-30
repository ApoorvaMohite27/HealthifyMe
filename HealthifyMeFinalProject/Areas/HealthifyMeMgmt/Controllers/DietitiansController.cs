using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthifyMeFinalProject.Data;
using HealthifyMeFinalProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace HealthifyMeFinalProject.Areas.HealthifyMeMgmt.Controllers
{
    [Area("HealthifyMeMgmt")]
    [Authorize]
    public class DietitiansController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DietitiansController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HealthifyMeMgmt/Dietitians
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dietitians.ToListAsync());
        }

        // GET: HealthifyMeMgmt/Dietitians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dietitian = await _context.Dietitians
                .FirstOrDefaultAsync(m => m.DietitianId == id);
            if (dietitian == null)
            {
                return NotFound();
            }

            return View(dietitian);
        }

        // GET: HealthifyMeMgmt/Dietitians/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HealthifyMeMgmt/Dietitians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DietitianId,DietitianUserName,DietitianName,Specialist,Description,DietitianPhoto")] Dietitian dietitian)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dietitian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dietitian);
        }

        // GET: HealthifyMeMgmt/Dietitians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dietitian = await _context.Dietitians.FindAsync(id);
            if (dietitian == null)
            {
                return NotFound();
            }
            return View(dietitian);
        }

        // POST: HealthifyMeMgmt/Dietitians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DietitianId,DietitianUserName,DietitianName,Specialist,Description,DietitianPhoto")] Dietitian dietitian)
        {
            if (id != dietitian.DietitianId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dietitian);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DietitianExists(dietitian.DietitianId))
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
            return View(dietitian);
        }

        // GET: HealthifyMeMgmt/Dietitians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dietitian = await _context.Dietitians
                .FirstOrDefaultAsync(m => m.DietitianId == id);
            if (dietitian == null)
            {
                return NotFound();
            }

            return View(dietitian);
        }

        // POST: HealthifyMeMgmt/Dietitians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dietitian = await _context.Dietitians.FindAsync(id);
            _context.Dietitians.Remove(dietitian);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DietitianExists(int id)
        {
            return _context.Dietitians.Any(e => e.DietitianId == id);
        }
    }
}

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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace HealthifyMeFinalProject.Areas.HealthifyMeMgmt.Controllers
{
    [Area("HealthifyMeMgmt")]
    [Authorize]
    public class FeedBackFormsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FeedBackFormsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: HealthifyMeMgmt/FeedBackForms
        public async Task<IActionResult> Index()
        {
            return View(await _context.FeedBackForms.ToListAsync());
        }

        // GET: HealthifyMeMgmt/FeedBackForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedBackForm = await _context.FeedBackForms
                .FirstOrDefaultAsync(m => m.FormId == id);
            if (feedBackForm == null)
            {
                return NotFound();
            }

            return View(feedBackForm);
        }

        // GET: HealthifyMeMgmt/FeedBackForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HealthifyMeMgmt/FeedBackForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormId,CustomerName,Rating,Comments")] FeedBackForm feedBackForm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(this.User);
                feedBackForm.CustomerName = user.UserName;

                _context.Add(feedBackForm);
                await _context.SaveChangesAsync();
                return Redirect("~/demo/home/homepage");
            }
            return View(feedBackForm);
        }

        // GET: HealthifyMeMgmt/FeedBackForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedBackForm = await _context.FeedBackForms.FindAsync(id);
            if (feedBackForm == null)
            {
                return NotFound();
            }
            return View(feedBackForm);
        }

        // POST: HealthifyMeMgmt/FeedBackForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormId,CustomerName,Rating,Comments")] FeedBackForm feedBackForm)
        {
            if (id != feedBackForm.FormId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedBackForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedBackFormExists(feedBackForm.FormId))
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
            return View(feedBackForm);
        }

        // GET: HealthifyMeMgmt/FeedBackForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedBackForm = await _context.FeedBackForms
                .FirstOrDefaultAsync(m => m.FormId == id);
            if (feedBackForm == null)
            {
                return NotFound();
            }

            return View(feedBackForm);
        }

        // POST: HealthifyMeMgmt/FeedBackForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var feedBackForm = await _context.FeedBackForms.FindAsync(id);
            _context.FeedBackForms.Remove(feedBackForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedBackFormExists(int id)
        {
            return _context.FeedBackForms.Any(e => e.FormId == id);
        }
    }
}

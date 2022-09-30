using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HealthifyMeFinalProject.Data;
using HealthifyMeFinalProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace HealthifyMeFinalProject.Areas.HealthifyMeMgmt.Controllers
{
    [Area("HealthifyMeMgmt")]
    [Authorize]
    public class CustomerDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CustomerDetailsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: HealthifyMeMgmt/CustomerDetails
        public async Task<IActionResult> Index()
        {
            var query = await _context.CustomerDetails.ToListAsync();
            return View(viewName: "IndexCopy", model: query);
        }

        // Passing Current Customer Details...
        public async Task<IActionResult> DisplayDetails()
        {
            var user = await _userManager.GetUserAsync(this.User);

            var query = await _context.CustomerDetails.Where(u => u.CustomerName == user.UserName).ToListAsync();

            return View(viewName: "Index", model: query);
        }


        // GET: HealthifyMeMgmt/CustomerDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerDetail = await _context.CustomerDetails
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customerDetail == null)
            {
                return NotFound();
            }

            return View(customerDetail);
        }

        // GET: HealthifyMeMgmt/CustomerDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HealthifyMeMgmt/CustomerDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerName,FirstName,LastName,Gender,DOB,Age,Height,CurrentWeight,TargetWeight,MedicalCondition")] CustomerDetail customerDetail)
        {
            // Assign the Current User's ID to the CreatedByUserId
           
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(this.User);
                customerDetail.CustomerName = user.UserName;

                _context.Add(customerDetail);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Information Submitted Successfully......!";
                TempData["AlertMessageAdmin"] = "New Customer is Added......!";
                return RedirectToAction(actionName: "HomePage",controllerName:"Home",routeValues:new {area="Demo"});                              // Redirecting to Home Page...
            }
            return View(customerDetail);
        }

        // GET: HealthifyMeMgmt/CustomerDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerDetail = await _context.CustomerDetails.FindAsync(id);
            if (customerDetail == null)
            {
                return NotFound();
            }
            return View(customerDetail);
        }

        // POST: HealthifyMeMgmt/CustomerDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,CustomerName,FirstName,LastName,Gender,DOB,Age,Height,CurrentWeight,TargetWeight,MedicalCondition")] CustomerDetail customerDetail)
        {
            if (id != customerDetail.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerDetailExists(customerDetail.CustomerId))
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
            return View(customerDetail);
        }

        // GET: HealthifyMeMgmt/CustomerDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerDetail = await _context.CustomerDetails
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customerDetail == null)
            {
                return NotFound();
            }

            return View(customerDetail);
        }

        // POST: HealthifyMeMgmt/CustomerDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerDetail = await _context.CustomerDetails.FindAsync(id);
            _context.CustomerDetails.Remove(customerDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerDetailExists(int id)
        {
            return _context.CustomerDetails.Any(e => e.CustomerId == id);
        }
    }
}

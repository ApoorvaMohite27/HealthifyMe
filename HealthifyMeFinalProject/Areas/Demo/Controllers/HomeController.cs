using Microsoft.AspNetCore.Mvc;

namespace HealthifyMeFinalProject.Areas.Demo.Controllers
{
    [Area("Demo")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HomePage()
        {
            return View();
        }

        public IActionResult CaloriesTheBasics()
        {
            return View();
        }

        public IActionResult CaloriesAndNutrients()
        {
            return View();
        }

        public IActionResult CaloriesBudgetAndCalculations()
        {
            return View();
        }

        public IActionResult TrackBetterServingAndPortions()
        {
            return View();
        }

        public IActionResult HowToBurnCalories()
        {
            return View();
        }

        public IActionResult DemoPage()
        {
            return View();
        }

        public IActionResult DemoPage1()
        {
            return View();
        }

    }
}

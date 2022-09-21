using JustInTimeCompany.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace JustInTimeCompany.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private SignInManager<ApplicationUser> _signInManager;

        public StatisticsController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var flightStatistics = new FlightStatistics(_context.Flights.ToList(), _context.Helicopters.ToList());
            return View(flightStatistics);
        }

        [ValidateAntiForgeryToken]
        [Produces("application/json")]
        public async Task<IActionResult> GetStats()
        {
            var jsonResult = Json(new FlightStatistics(_context.Flights.ToList(), _context.Helicopters.ToList())
                .FillingRates());
            return Ok(jsonResult);
        }
    }
}
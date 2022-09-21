using JustInTimeCompany.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using JustInTimeCompany.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace JustInTimeCompany.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private List<Airport> _airports;
        private List<Flight> _flights;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _airports = _context.Airports.ToList();
            _flights = _context.Flights.ToList();
            _flights.Sort((f1, f2) => f1.ScheduledDeparture.CompareTo(f2.ScheduledDeparture));
        }

        public async Task<IActionResult> Index()
        {
            var messages = new List<UserMessage>();
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                messages = await _context.UserMessages.Where(m => m.User.Id == user.Id).ToListAsync();
            }

            messages.ForEach(m => _context.UserMessages.Remove(m));
            await _context.SaveChangesAsync();
            var homeViewModel = new FlightListViewModel
            {
                Airports = _airports,
                Flights = _flights,
                UserMessages = messages,
            };
            return View(homeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Guid messageId)
        {
            var homeViewModel = new FlightListViewModel
            {
                Airports = _airports,
                Flights = _flights
            };
            return View(homeViewModel);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
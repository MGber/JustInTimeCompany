using JustInTimeCompany.Models;
using JustInTimeCompany.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JustInTimeCompany.Controllers
{
    public class HelicoptersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HelicoptersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            var helicopters = await _context.Helicopters.ToListAsync();
            return View(helicopters);
        }

        public async Task<IActionResult> Details(Guid helicopterId, Guid flightId)
        {
            var viewModel = new HelicopterDetailsViewModel()
            {
                HelicopterId = helicopterId,
                FlightId = flightId,
                Helicopter = await _context.Helicopters.SingleOrDefaultAsync(h => h.Id == helicopterId)
            };
            return (viewModel.Helicopter != null)
                ? View(viewModel)
                : NotFound(viewModel.Helicopter);
        }

        public async Task<IActionResult> Clear(Guid helicopterId)
        {
            var helicopter = await _context.Helicopters.FindAsync(helicopterId);
            if (helicopter == null) return NotFound($"Could'nt find the requested helicopter with id {helicopterId}");
            helicopter.Clear();
            _context.Update(helicopter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
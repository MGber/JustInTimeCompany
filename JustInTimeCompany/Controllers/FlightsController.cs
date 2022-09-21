using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JustInTimeCompany.Models;
using JustInTimeCompany.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace JustInTimeCompany.Controllers
{
    public class FlightsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FlightsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Flights
        public async Task<IActionResult> Index(string placeMode = "", Guid? placeId = null, string dateMode = "",
            DateTime? selectedDate = null)
        {
            ViewBag.PlaceMode = new List<string>() { "Any place", "From", "To" };
            ViewBag.DateMode = new List<string>() { "Both", "Departure", "Arrival" };
            var airports = await _context.Airports.ToListAsync();
            var flights = _context.Flights.ToList();
            flights.Sort((f1, f2) => f1.ScheduledDeparture.CompareTo(f2.ScheduledDeparture));
            var homeViewModel = new FlightListViewModel();
            homeViewModel.Airports = airports;
            homeViewModel.Flights = flights;
            if (!string.IsNullOrWhiteSpace(placeMode) && placeId is not null)
            {
                homeViewModel.Flights = placeMode switch
                {
                    "From" => flights.Where(f => f.From?.Id == placeId).ToList(),
                    "To" => flights.Where(f => f.To?.Id == placeId).ToList(),
                    _ => homeViewModel.Flights
                };
            }

            if (!string.IsNullOrWhiteSpace(dateMode) && selectedDate.HasValue)
            {
                homeViewModel.Flights = dateMode switch
                {
                    "Departure" => homeViewModel.Flights
                        .Where(f => f.ScheduledDeparture.Date == selectedDate.Value.Date)
                        .ToList(),
                    "Arrival" => homeViewModel.Flights
                        .Where(f => f.ScheduledArrival.Value.Date == selectedDate.Value.Date)
                        .ToList(),
                    _ => homeViewModel.Flights
                        .Where(f => f.ScheduledDeparture.Date == selectedDate.Value.Date ||
                                    f.ScheduledArrival.Value.Date == selectedDate.Value.Date)
                        .ToList(),
                };
            }


            return View(homeViewModel);
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(Guid? flightId)
        {
            if (flightId == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .FirstOrDefaultAsync(m => m.Id == flightId);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create()
        {
            var createFlightViewModel = new FlightViewModel();
            var airports = await _context.Airports.ToListAsync();
            var helicopters = await _context.Helicopters.ToListAsync();
            var pilots = await _userManager.GetUsersInRoleAsync("Pilot");
            createFlightViewModel.Airports = airports;
            createFlightViewModel.Helicopters = helicopters;
            createFlightViewModel.Pilots = pilots;
            return View(createFlightViewModel);
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FlightViewModel flightViewModel)
        {
            var flight = flightViewModel.Flight;
            if (flight == null) return NotFound();
            flight.From = await _context.Airports.SingleOrDefaultAsync(a => a.Id == flight.From.Id);
            flight.To = await _context.Airports.SingleOrDefaultAsync(a => a.Id == flight.To.Id);
            flight.Helicopter =
                await _context.Helicopters.SingleOrDefaultAsync(h => h.Id == flight.Helicopter.Id);
            flight.Pilot = _userManager.FindByIdAsync(flight.Pilot?.Id).Result;
            if (flight.From is null || flight.To is null || flight.Pilot is null || flight.Helicopter is null)
                return NotFound("Couldn't find required airport or pilot or helicopter.");
            var airports = await _context.Airports.ToListAsync();
            var helicopters = await _context.Helicopters.ToListAsync();
            var pilots = await _userManager.GetUsersInRoleAsync("Pilot");
            flightViewModel.Airports = airports;
            flightViewModel.Helicopters = helicopters;
            flightViewModel.Pilots = pilots;
            flightViewModel.Flights = await _context.Flights.ToListAsync();
            ModelState.Clear();
            if (TryValidateModel(flight))
            {
                if (flightViewModel.PilotCanFly())
                {
                    flight.Id = Guid.NewGuid();
                    _context.Add(flight);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                    ModelState.AddModelError("Flight.Pilot", "Sorry, requested pilot is not free at that date.");
            }

            return View(flightViewModel);
        }

        // GET
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateMany()
        {
            ViewBag.Helicopters = await _context.Helicopters.ToListAsync();
            ViewBag.Pilots = await _userManager.GetUsersInRoleAsync("Pilot");
            ViewBag.Airports = await _context.Airports.ToListAsync();
            return View(new ManyFlightsViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMany(ManyFlightsViewModel viewModel)
        {
            viewModel.Pilot = await _userManager.FindByIdAsync(viewModel.Pilot.Id);
            viewModel.Helicopter =
                await _context.Helicopters.SingleOrDefaultAsync(h => h.Id == viewModel.Helicopter!.Id);
            viewModel.From = (await _context.Airports.FindAsync(viewModel.From.Id))!;
            viewModel.To = await _context.Airports.FindAsync(viewModel.To!.Id);
            ModelState.Clear();
            if (TryValidateModel(viewModel))
            {
                var flights = viewModel.GetFlights();
                flights.ForEach(f => _context.Add(f));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Helicopters = await _context.Helicopters.ToListAsync();
            ViewBag.Pilots = await _userManager.GetUsersInRoleAsync("Pilot");
            ViewBag.Airports = await _context.Airports.ToListAsync();
            return View(viewModel);
        }

        // GET: Flights/Edit/5
        [Authorize(Roles = "Administrator, Pilot")]
        public async Task<IActionResult> Edit(Guid? flightId)
        {
            if (User.IsInRole("Pilot")) return RedirectToAction(nameof(EditPilot), new { flightId = flightId });
            if (flightId == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(flightId);
            if (flight == null)
            {
                return NotFound();
            }

            return View(new FlightViewModel()
            {
                Flight = flight,
                Helicopters = await _context.Helicopters.ToListAsync(),
                Airports = await _context.Airports.ToListAsync(),
                Pilots = await _userManager.GetUsersInRoleAsync("Pilot")
            });
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(FlightViewModel viewModel)
        {
            var flight = await _context.Flights.SingleOrDefaultAsync(f => f.Id == viewModel.Flight.Id);
            string oldJson = JsonSerializer.Serialize(flight);
            if (flight == null) return NotFound(flight);
            flight.From = (await _context.Airports.FindAsync(viewModel.Flight.From.Id))!;
            flight.To = (await _context.Airports.FindAsync(viewModel.Flight.To?.Id))!;
            flight.Helicopter = (await _context.Helicopters.FindAsync(viewModel.Flight.Helicopter?.Id))!;
            flight.Pilot = await _userManager.FindByIdAsync(viewModel.Flight.Pilot.Id);
            flight.ScheduledDeparture = viewModel.Flight.ScheduledDeparture;
            if (flight.From is null || flight.To is null || flight.Pilot is null || flight.Helicopter is null)
                return NotFound("Couldn't find required airport or pilot or helicopter.");
            ModelState.Clear();
            if (TryValidateModel(flight))
            {
                try
                {
                    flight.SetScheduledArrival();
                    _context.Update(flight);
                    flight.Reservations.ForEach(r =>
                    {
                        var message = new UserMessage()
                        {
                            User = r.User,
                            Id = Guid.NewGuid(),
                            Message =
                                $"Your flight from {flight.From.Name} to {flight.To.Name} has been update to the date : {flight.ScheduledDeparture}",
                        };
                        _context.Add(message);
                        var newJson = JsonSerializer.Serialize(flight);
                        var log = new FlightLog()
                        {
                            Date = DateTime.Now,
                            Id = Guid.NewGuid(),
                            Flight = flight,
                            OldJson = oldJson,
                            NewJson = newJson,
                        };
                        _context.FlightLog.Add(log);
                    });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(viewModel.Flight.Id))
                    {
                        return NotFound();
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            viewModel.Pilots = await _userManager.GetUsersInRoleAsync("Pilot");
            viewModel.Helicopters = await _context.Helicopters.ToListAsync();
            viewModel.Airports = await _context.Airports.ToListAsync();
            return View(viewModel);
        }

        // GET: Flights/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid? flightId)
        {
            if (flightId == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .FirstOrDefaultAsync(m => m.Id == flightId);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(Flight flight)
        {
            flight = await _context.Flights.FindAsync(flight.Id);
            flight.Reservations.ForEach(r => _context.Remove(r));
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(Guid id)
        {
            return _context.Flights.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Pilot")]
        public async Task<IActionResult> EditPilot(Guid flightId)
        {
            var flight = await _context.Flights.SingleOrDefaultAsync(f => f.Id == flightId);
            if (flight == null) return NotFound();
            var pilotEditFlightViewModel = new PilotEditFlightViewModel
            {
                FlightId = flightId,
                Arrival = flight.ScheduledArrival,
                Departure = flight.ScheduledDeparture,
                WasLate = flight.WasLate,
                DelayReason = flight.DelayReason,
                ScheduledArrival = flight.ScheduledArrival,
            };
            return View(pilotEditFlightViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPilot(PilotEditFlightViewModel viewModel)
        {
            var pilot = await _userManager.GetUserAsync(User);
            var flight = await _context.Flights.SingleOrDefaultAsync(f => f.Id == viewModel.FlightId);
            if (pilot.Id != flight.Pilot.Id)
                ModelState.AddModelError("", "You cannot edit other pilots flight.");
            if (flight == null) return NotFound();

            if (!TryValidateModel(viewModel)) return View(viewModel);
            try
            {
                if (flight.RealArrival == null)
                    flight.Helicopter.IncrementFlightCount();
                flight.RealDeparture = viewModel.Departure;
                flight.RealArrival = viewModel.Arrival is DateTime ? (DateTime)viewModel.Arrival : default;
                flight.DelayReason = viewModel.DelayReason;
                flight.WasLate = viewModel.WasLate;

                _context.Update(flight);
                _context.Update(flight.Helicopter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { flightId = viewModel.FlightId });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(viewModel.FlightId))
                {
                    return NotFound();
                }
            }

            return Problem();
        }


        [Produces("application/json")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetPilotsFor(DateTime date)
        {
            FlightViewModel viewModel = new FlightViewModel();
            viewModel.Flights = await _context.Flights.ToListAsync();
            viewModel.Pilots = await _userManager.GetUsersInRoleAsync("Pilot");
            var pilots = viewModel.PilotsForDate(date)
                .Select(p => new { Name = p.FirstName + " " + p.LastName, p.Id });

            return Ok(Json(pilots));
        }
    }
}
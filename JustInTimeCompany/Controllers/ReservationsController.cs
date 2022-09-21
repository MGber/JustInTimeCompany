using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JustInTimeCompany.Models;
using JustInTimeCompany.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace JustInTimeCompany.Controllers
{
    [Authorize(Roles = "User")]
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: FlightReservations
        public async Task<IActionResult> Index(string reservationFilter = "")
        {
            ViewBag.ReservationTime = new List<string>() { "All", "Past", "Future" };
            var flights = _context.Flights.ToList();
            flights.Sort((f1, f2) => f1.ScheduledDeparture.CompareTo(f2.ScheduledDeparture));
            var filtered = reservationFilter switch
            {
                "Past" => flights.Where(f => f.ScheduledDeparture < DateTime.Now).ToList(),
                "Future" => flights.Where(f => f.ScheduledDeparture >= DateTime.Now).ToList(),
                _ => flights.ToList(),
            };

            var viewModel = new ReservationListViewModel(filtered, await _userManager.GetUserAsync(User));
            return View(viewModel);
        }

        // GET: FlightReservations/Details/5
        public async Task<IActionResult> Details(Guid? reservationId)
        {
            if (reservationId == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.Id == reservationId);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: FlightReservations/Create
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(Guid? flightId)
        {
            var flight = _context.Flights.SingleOrDefault(f => f.Id == flightId);
            var user = await _userManager.GetUserAsync(User);
            if (flightId is not null)
            {
                if (flight!.HaveAlreadyBooked(user))
                {
                    var reservation = flight.ForUser(user);
                    if (reservation != null)
                        return RedirectToAction("Edit", new { reservationId = reservation.Id });
                    return NotFound(reservation);
                }
            }

            ReservationViewModel viewModel = new()
            {
                FlightId = flightId
            };

            return View(viewModel);
        }

        // POST: FlightReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(ReservationViewModel viewModel)
        {
            viewModel.Flight = _context.Flights.SingleOrDefault(f => f.Id == viewModel.FlightId);
            if (viewModel.Flight == null) return NotFound(viewModel.Flight);
            ModelState.Clear();
            if (!TryValidateModel(viewModel))
                return View(viewModel);
            Reservation reservation = new()
            {
                User = await _userManager.GetUserAsync(User),
                SeatAmount = viewModel.SeatAmount,
            };
            if (viewModel.Flight.AddReservation(reservation))
            {
                _context.Reservations.Add(reservation);
                _context.Update(viewModel.Flight);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: FlightReservations/Edit/5
        public async Task<IActionResult> Edit(Guid? reservationId)
        {
            var reservation = await _context.Reservations.SingleOrDefaultAsync(
                r => r.Id == reservationId);
            var flight =
                _context.Flights.SingleOrDefault(f => reservation != null && f.Reservations.Contains(reservation));
            if (reservation is null || flight == null)
                return NotFound();
            return View(new ReservationViewModel()
            {
                Flight = flight,
                ReservationId = reservationId,
                SeatAmount = reservation.SeatAmount,
                FlightId = flight.Id
            });
        }

        //TODO validate seatcount
        // POST: FlightReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReservationViewModel viewModel)
        {
            viewModel.Flight = _context.Flights.SingleOrDefault(f => f.Id == viewModel.FlightId);
            viewModel.Reservation = _context.Reservations.FirstOrDefault(f => f.Id == viewModel.ReservationId);
            if (viewModel.Reservation == null || viewModel.Flight == null) return NotFound();
            ModelState.Clear();
            if (!TryValidateModel(viewModel)) return View(viewModel);
            try
            {
                if (viewModel.Flight.ChangeReservation(viewModel.Reservation, viewModel.SeatAmount))
                {
                    _context.Update(viewModel.Reservation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(viewModel.Reservation.Id)) return NotFound();
            }

            return View(viewModel);
        }

        // GET: FlightReservations/Delete/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(Guid? reservationId)
        {
            if (reservationId == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.Id == reservationId);
            var flight =
                _context.Flights.SingleOrDefault(f => reservation != null && f.Reservations.Contains(reservation));
            if (reservation == null || flight == null)
            {
                return NotFound();
            }

            return View(new ReservationViewModel()
                { Flight = flight, Reservation = reservation, ReservationId = reservationId });
        }

        // POST: FlightReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null) return NotFound($"Could not find reservation with id {reservation}");
            var flight = await _context.Flights.SingleOrDefaultAsync(f => f.Reservations.Contains(reservation));
            if (flight == null) return NotFound($"Could not find flight for reservation : {reservationId}");
            if (!flight.RemoveReservation(reservation))
            {
                TempData["message"] = "Cannot delete a reservation less than 24h before the flight.";
                return View(new ReservationViewModel()
                    { Flight = flight, Reservation = reservation, ReservationId = reservationId });
            }

            _context.Reservations.Remove(reservation);
            _context.Update(flight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(Guid id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
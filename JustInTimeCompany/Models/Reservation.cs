using System.ComponentModel.DataAnnotations;


namespace JustInTimeCompany.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public virtual ApplicationUser? User { get; set; }

        public Reservation()
        {
            Id = Guid.NewGuid();
        }

        [Required(ErrorMessage = "Please select seat amount")]
        [Range(1, int.MaxValue, ErrorMessage = "Minimum 1 seat.")]
        [Display(Name = "Seat amount")]
        public int SeatAmount { get; set; }
    }
}


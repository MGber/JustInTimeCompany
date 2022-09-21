using JustInTimeCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace JustInTimeCompany.Data
{
    public class ReservationSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {

            var pastf1 = new Guid("6809c4c1-3714-4037-8d3d-cf06b3b6555c");
            var pastf2 = new Guid("138d28a4-5cb2-4792-8aaa-2357310cfcc4");
            var futuref3 = new Guid("97550023-4dd5-425b-8285-420b3bd52ad1");
            var futuref4 = new Guid("44198f59-813b-4a96-a930-84f7b77a4eb6");
            var futuref5 = new Guid("c7bdc7a7-8a10-46e2-8058-3f9b645662c6");

            var r1 = new Guid("79a77425-9216-40c3-b6c9-39d1dc097909");
            var r2 = new Guid("bb207b88-d785-4e2b-820d-0c68f27739f5");
            var r3 = new Guid("d58210ef-c2b6-4413-934c-2ae16eab260e");
            var r4 = new Guid("6ae64930-1ba7-4933-96cf-d89fdd4353bd");
            var r5 = new Guid("5b36ccee-3b31-49cb-ab02-3e021779dac4");

            var u1Id = new Guid("bb76dfd3-10d3-4224-b4b3-712aafa7ccaa");
            var u2Id = new Guid("51d08f0c-0c50-4e47-8e8b-55f7125019bc");
            var u3Id = new Guid("4b40d3d6-bd20-43f2-a5b8-082009c13682");

            var res1 = new { Id = r1, UserId = u1Id.ToString(), FlightId = pastf1, SeatAmount = 5 };
            var res2 = new { Id = r2, UserId = u2Id.ToString(), FlightId = pastf2, SeatAmount = 3 };
            var res3 = new { Id = r3, UserId = u3Id.ToString(), FlightId = futuref3, SeatAmount = 3 };
            var res4 = new { Id = r4, UserId = u1Id.ToString(), FlightId = futuref4, SeatAmount = 3 };
            var res5 = new { Id = r5, UserId = u2Id.ToString(), FlightId = futuref4, SeatAmount = 1 };

            modelBuilder.Entity<Reservation>().HasData(
                res1, res2, res3, res4, res5);
        }
    }
}

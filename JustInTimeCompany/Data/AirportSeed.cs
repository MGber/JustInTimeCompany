using JustInTimeCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace JustInTimeCompany.Data
{
    public class AirportSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var a1Id = new Guid("ac8aa908-1fa9-4abd-ad51-32353a4c4a00");
            var a2Id = new Guid("76391ff6-412f-4c1e-b57f-4ccf86648cff");
            var a3Id = new Guid("7172b844-ebcb-45c3-94bc-34c14dbd7b4f");
            var a4Id = new Guid("c2af481a-b71d-473c-8082-e1170b1551de");

            var airPort1 = new Airport()
            {
                Id = a1Id,
                Name = "Liège",
                Latitude = 50.63583079,
                Longitude = 5.439331576,
            };

            var airPort2 = new Airport()
            {
                Id = a2Id,
                Name = "Charleroi",
                Latitude = 50.455998176,
                Longitude = 4.45166486,
            };

            var airPort3 = new Airport()
            {
                Id = a3Id,
                Name = "Bruxelles",
                Latitude = 50.89834,
                Longitude = 4.48237,
            };

            var airPort4 = new Airport()
            {
                Id = a4Id,
                Name = "Oostende",
                Latitude = 51.20040,
                Longitude = 2.87417,
            };
            modelBuilder.Entity<Airport>().HasData(
                airPort1, airPort2, airPort3, airPort4);
        }
    }
}

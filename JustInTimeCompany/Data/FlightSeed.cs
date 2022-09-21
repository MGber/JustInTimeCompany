using System.Globalization;
using JustInTimeCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace JustInTimeCompany.Data
{
    public class FlightSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var h1 = new Guid("f7ad5264-3054-4889-9b9a-a1b598e579d7");
            var h2 = new Guid("05e1ae09-0a16-4e76-8b2d-a70fcdd1c7e3");
            var h3 = new Guid("479e5015-b520-4307-bf25-b75c873f8975");

            var a1Id = new Guid("ac8aa908-1fa9-4abd-ad51-32353a4c4a00");
            var a2Id = new Guid("76391ff6-412f-4c1e-b57f-4ccf86648cff");
            var a3Id = new Guid("7172b844-ebcb-45c3-94bc-34c14dbd7b4f");
            var a4Id = new Guid("c2af481a-b71d-473c-8082-e1170b1551de");

            var adId = new Guid("319a3971-483c-4ed9-a0cc-2bec19a07b06");
            var p1Id = new Guid("821f98b0-ff1e-458a-b15b-a9ea85750e45");
            var p2Id = new Guid("3ac33a35-d3d6-4a04-a79b-aded19a205db");
            var p3Id = new Guid("efb6c53f-48d0-4981-a3c7-ef714fa2b508");
            var u1Id = new Guid("bb76dfd3-10d3-4224-b4b3-712aafa7ccaa");
            var u2Id = new Guid("51d08f0c-0c50-4e47-8e8b-55f7125019bc");
            var u3Id = new Guid("4b40d3d6-bd20-43f2-a5b8-082009c13682");

            CultureInfo provider = new CultureInfo("fr-BE");


            var pastf1 = new Guid("6809c4c1-3714-4037-8d3d-cf06b3b6555c");
            var pastf2 = new Guid("138d28a4-5cb2-4792-8aaa-2357310cfcc4");
            var futuref3 = new Guid("97550023-4dd5-425b-8285-420b3bd52ad1");
            var futuref4 = new Guid("44198f59-813b-4a96-a930-84f7b77a4eb6");
            var futuref5 = new Guid("c7bdc7a7-8a10-46e2-8058-3f9b645662c6");

            var passed1 = new
            {
                Id = pastf1,
                FromId = a1Id,
                ToId = a2Id,
                HelicopterId = h1,
                PilotId = p1Id.ToString(),
                ScheduledDeparture = DateTime.ParseExact("01/08/2022 18:23", "dd/MM/yyyy HH:mm", provider),
                RealDeparture = DateTime.ParseExact("01/08/2022 18:29", "dd/MM/yyyy HH:mm", provider),
                ScheduledArrival = DateTime.ParseExact("01/08/2022 20:23", "dd/MM/yyyy HH:mm", provider),
                RealArrival = DateTime.ParseExact("01/08/2022 20:29", "dd/MM/yyyy HH:mm", provider),
                WasLate = true,
                DelayReason = "The pilot overslept."
            };

            var passed2 = new
            {
                Id = pastf2,
                FromId = a2Id,
                ToId = a3Id,
                HelicopterId = h2,
                PilotId = p2Id.ToString(),
                ScheduledDeparture = DateTime.ParseExact("02/08/2022 18:23", "dd/MM/yyyy HH:mm", provider),
                RealDeparture = DateTime.ParseExact("02/08/2022 18:29", "dd/MM/yyyy HH:mm", provider),
                ScheduledArrival = DateTime.ParseExact("02/08/2022 20:23", "dd/MM/yyyy HH:mm", provider),
                RealArrival = DateTime.ParseExact("02/08/2022 20:29", "dd/MM/yyyy HH:mm", provider),
                WasLate = true,
                DelayReason = "The pilot again overslept."
            };

            var future1 = new
            {
                Id = futuref3,
                FromId = a3Id,
                ToId = a4Id,
                HelicopterId = h3,
                PilotId = p3Id.ToString(),
                ScheduledDeparture = DateTime.ParseExact("10/09/2022 18:23", "dd/MM/yyyy HH:mm", provider),
                ScheduledArrival = DateTime.ParseExact("10/09/2022 20:23", "dd/MM/yyyy HH:mm", provider),
                WasLate = false,
            };

            var future2 = new
            {
                Id = futuref4,
                FromId = a4Id,
                ToId = a1Id,
                HelicopterId = h1,
                PilotId = p1Id.ToString(),
                ScheduledDeparture = DateTime.ParseExact("11/09/2022 18:23", "dd/MM/yyyy HH:mm", provider),
                ScheduledArrival = DateTime.ParseExact("11/09/2022 20:23", "dd/MM/yyyy HH:mm", provider),
                WasLate = false,
            };

            var future3 = new
            {
                Id = futuref5,
                FromId = a1Id,
                ToId = a4Id,
                HelicopterId = h2,
                PilotId = p2Id.ToString(),
                ScheduledDeparture = DateTime.ParseExact("12/09/2022 18:23", "dd/MM/yyyy HH:mm", provider),
                ScheduledArrival = DateTime.ParseExact("12/09/2022 20:23", "dd/MM/yyyy HH:mm", provider),
                WasLate = false,
            };

            modelBuilder.Entity<Flight>()
                .HasData(passed1, passed2, future1, future2, future3);
        }
    }
}
using JustInTimeCompany.Models;
using Microsoft.EntityFrameworkCore;


namespace JustInTimeCompany.Data
{
    public class HelicopterSeed
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var h1Id = new Guid("f7ad5264-3054-4889-9b9a-a1b598e579d7");
            var h2Id = new Guid("05e1ae09-0a16-4e76-8b2d-a70fcdd1c7e3");
            var h3Id = new Guid("479e5015-b520-4307-bf25-b75c873f8975");

            var helicopter1 = new Helicopter()
            {
                Id = h1Id,
                Model = "Eurocopter AS 355 F1/F2 Ecureuil III",
                SeatCount = 6,
                Speed = 220,
                Engine = "Deux turbines du modèle de Rolls Royce 250-C20F",
                FlightCount = 6,
                Status = "DANGER"
            };

            var helicopter2 = new Helicopter()
            {
                Id = h2Id,
                Model = "Bell 206 JetRanger",
                SeatCount = 4,
                Speed = 190,
                Engine = "Une turbine du type Rolls Royce 250-C20B",
                FlightCount = 5,
                Status = "WARNING",
            };

            var helicopter3 = new Helicopter()
            {
                Id = h3Id,
                Model = "Robinson R44 Raven II",
                SeatCount = 3,
                Speed = 190,
                Engine = "Un piston du type Lycoming modèle IO-540",
                FlightCount = 3,
                Status = "OK"
            };
            modelBuilder.Entity<Helicopter>().HasData(
                helicopter1, helicopter2, helicopter3);
        }
    }
}
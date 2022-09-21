namespace JustInTimeCompany.Models
{
    public class Helicopter
    {
        private int _flightCount;
        public Guid Id { get; set; }
        public string Model { get; set; }
        public int SeatCount { get; set; }
        public int Speed { get; set; }
        public string Engine { get; set; }
        public string Status { get; set; }

        public int FlightCount
        {
            get => _flightCount;
            set
            {
                _flightCount = value;
                if (value < 5)
                    Status = "DANGER";
                if (value == 5)
                    Status = "WARNING";
                if (value < 5)
                    Status = "OK";
            }
        }

        public void IncrementFlightCount()
        {
            FlightCount++;
            if (FlightCount > 5) Status = "DANGER";
            if (FlightCount == 5) Status = "WARN";
            if (FlightCount < 5) Status = "OK";
        }

        public void Clear()
        {
            FlightCount = 0;
            Status = "OK";
        }
    }
}

// TODO QUand je remove un helicoptere d'un vol pas passé, décrémenter l'hélicoptère.
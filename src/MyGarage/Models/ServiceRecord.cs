using SQLite;

namespace MyGarage.Models
{
    public class ServiceRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public DateTime Date { get; set; }

        public int Odometer { get; set; }

        public string Description { get; set; } = string.Empty;

        public int Cost { get; set; }

        public string InvoiceImagePath { get; set; } = string.Empty;

        public double? LocationLat { get; set; }

        public double? LocationLon { get; set; }
    }
}

using InsurancePlatform;

    public class MotorRisk
    {
        public int MotorRiskId { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public int YearOfManufacture { get; set; }
        public decimal SumInsured { get; set; }
        public decimal Rate { get; set; }
        public decimal Premium => SumInsured * Rate;

        public int QuoteId { get; set; }
        public Quote Quote { get; set; }
    }

